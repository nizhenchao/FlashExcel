//**************************************************
// Copyright©2018-2019 何冠峰
// Licensed under the MIT license
//**************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;


public class ExcelData : IDisposable
{
	/// <summary>
	/// Excel文件名称
	/// </summary>
	public string ExcelName { get; }

	/// <summary>
	/// Excel文件路径
	/// </summary>
	public string ExcelPath { get; }

	/// <summary>
	/// 页签列表
	/// </summary>
	public readonly List<SheetData> SheetDataList = new List<SheetData>();

	/// <summary>
	/// 表格类
	/// </summary>
	private IWorkbook _workbook = null;

	/// <summary>
	/// 文件流
	/// </summary>
	private FileStream _stream = null;


	public ExcelData(string excelPath)
	{
		ExcelPath = excelPath;
		ExcelName = Path.GetFileNameWithoutExtension(ExcelPath);
	}

	/// <summary>
	/// Dispose
	/// </summary>
	public void Dispose()
	{
		if (_stream != null)
		{
			_stream.Close();
			_stream = null;
		}

		if (_workbook != null)
		{
			_workbook.Close();
			_workbook = null;
		}

		SheetDataList.Clear();
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// 加载Excel
	/// </summary>
	public bool Load()
	{
		try
		{
			_stream = new FileStream(ExcelPath, FileMode.Open, FileAccess.Read);

			if (ExcelPath.IndexOf(".xlsx") > 0)
				_workbook = new XSSFWorkbook(_stream);
			else if (ExcelPath.IndexOf(".xls") > 0)
				_workbook = new HSSFWorkbook(_stream);
			else
			{
				string extension = Path.GetExtension(ExcelPath);
				throw new Exception($"未支持的Excel文件类型 : {extension}");
			}

			for (int i = 0; i < _workbook.NumberOfSheets; i++)
			{
				ISheet sheet = _workbook.GetSheetAt(i);
				if (sheet.SheetName.StartsWith(ConstDefine.StrSheetLogo))
				{
					SheetData sheetData = new SheetData(sheet.SheetName);
					sheetData.Load(_workbook, sheet);
					SheetDataList.Add(sheetData);
				}
			}

			// 如果没有找到有效的工作页
			if (SheetDataList.Count == 0)
				throw new Exception($"没有发现包含 {ConstDefine.StrSheetLogo} 的页签");
		}
		catch (Exception ex)
		{
			MessageBox.Show($"表格[{ExcelName}]加载错误：{ex}");
			return false;
		}

		return true;
	}

	/// <summary>
	/// 导出Excel
	/// </summary>
	public bool Export()
	{
		try
		{
			for (int i = 0; i < SheetDataList.Count; i++)
			{
				ExportInternal(SheetDataList[i]);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"表格[{ExcelName}]导出错误：{ex}");
			return false;
		}

		return true;
	}
	private void ExportInternal(SheetData sheet)
	{
		// 生成客户端文件
		for(int i=1; i< ExportConfig.Instance.ClientExportInfos.Length; i++)
		{
			ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ClientExportInfos[i];
			if (wrapper.ExporterType != null)
				sheet.Export(wrapper.ExporterType, wrapper.ExportPath, "C");
		}

		// 生成服务器文件
		for (int i = 1; i < ExportConfig.Instance.ServerExportInfos.Length; i++)
		{
			ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ServerExportInfos[i];
			if (wrapper.ExporterType != null)
				sheet.Export(wrapper.ExporterType, wrapper.ExportPath, "S");
		}

		// 生成战服文件
		for (int i = 1; i < ExportConfig.Instance.BattleExportInfos.Length; i++)
		{
			ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.BattleExportInfos[i];
			if (wrapper.ExporterType != null)
				sheet.Export(wrapper.ExporterType, wrapper.ExportPath, "B");
		}
	}
}