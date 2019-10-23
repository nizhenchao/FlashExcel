//**************************************************
// Copyright©2019 何冠峰
// Licensed under the MIT license
//**************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MotionEngine.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;


/// <summary>
/// 多语言数据类
/// </summary>
public class LanguageWrapper
{
	/// <summary>
	/// 来源自哪个EXCEL
	/// </summary>
	public string Source = string.Empty;

	/// <summary>
	/// 多语言内容
	/// </summary>
	public string Content = string.Empty;
}


/// <summary>
/// 多语言管理器
/// </summary>
public class LanguageMgr
{
	public static LanguageMgr Instance = new LanguageMgr();

	/// <summary>
	/// 多语言总表名称
	/// </summary>
	private const string StrAutoGenerateLanguageExcelName = "AutoGenerateLanguage";

	/// <summary>
	/// 缓存数据
	/// </summary>
	private readonly Dictionary<int, LanguageWrapper> _cacheLanguage = new Dictionary<int, LanguageWrapper>();


	private LanguageMgr()
	{
	}

	/// <summary>
	/// 清空缓存数据
	/// </summary>
	public void ClearCacheLanguage()
	{
		_cacheLanguage.Clear();
	}

	/// <summary>
	/// 缓存多语言数据
	/// </summary>
	public void CacheLanguage(Dictionary<int, LanguageWrapper> data)
	{
		foreach (var pair in data)
		{
			int hashCode = pair.Key;
			LanguageWrapper value = pair.Value;
			if (_cacheLanguage.ContainsKey(hashCode) == false)
			{
				_cacheLanguage.Add(hashCode, value);
			}
		}
	}

	/// <summary>
	/// 导出多语言总表文件
	/// </summary>
	public void ExportAutoGenerateLanguageFile()
	{
		string filePath = ExportConfig.Instance.LastOpenExcelPath + "\\" + StrAutoGenerateLanguageExcelName + ".xlsx";
		if (File.Exists(filePath))
		{
			ExcelData excelFile = new ExcelData(filePath);
			if (excelFile.Load())
				excelFile.Export();
		}
	}

	/// <summary>
	/// 加载多语言总表数据到缓存
	/// </summary>
	public void LoadAutoGenerateLanguageToCache()
	{
		string filePath = ExportConfig.Instance.LastOpenExcelPath + "//" + StrAutoGenerateLanguageExcelName + ".xlsx";
		if (File.Exists(filePath))
		{
			ExcelData excelFile = new ExcelData(filePath);
			if (excelFile.Load())
			{
				for(int i=0; i<excelFile.SheetDataList.Count; i++)
				{
					SheetData sheet = excelFile.SheetDataList[i];
					var data = ParseAutoGenerateLanguageExcel(sheet);
					CacheLanguage(data);
				}
			}
			excelFile.Dispose();
		}
	}
	private Dictionary<int, LanguageWrapper> ParseAutoGenerateLanguageExcel(SheetData sheet)
	{
		Dictionary<int, LanguageWrapper> cacheLanguages = new Dictionary<int, LanguageWrapper>();

		// 遍历所有行
		foreach (var table in sheet.Tables)
		{
			// 获取格子数值
			int firsetCellNum = table.Row.FirstCellNum;
			string cell1Value = table.GetCellValue(firsetCellNum); //id
			string cell2Value = table.GetCellValue(++firsetCellNum); //source
			string cell3Value = table.GetCellValue(++firsetCellNum); //language

			int hashCode = Convert.ToInt32(cell1Value);
			LanguageWrapper wrapper = new LanguageWrapper();
			wrapper.Source = cell2Value;
			wrapper.Content = cell3Value;
			cacheLanguages.Add(hashCode, wrapper);
		}

		// 检测字典里是否有重复值
		var duplicateValues = cacheLanguages.GroupBy(x => x.Value.Content).Where(x => x.Count() > 1);
		foreach (var item in duplicateValues)
		{
			throw new Exception($"多语言总表发现重复值：{item.Key}");
		}

		return cacheLanguages;
	}

	/// <summary>
	/// 创建多语言总表文件
	/// </summary>
	public void CreateAutoGenerateLanguageFile()
	{
		try
		{
			int nextRow = 0;

			List<string> typeList = new List<string>() {"int", "#", "string"};	
			List<string> nameList = new List<string>() {"id", "#", "lang"};
			List<string> flagList = new List<string>() {"C", "#", "C"};

			// 创建工作簿
			XSSFWorkbook workbook = new XSSFWorkbook();

			// 创建Sheet页
			ISheet sheet = workbook.CreateSheet($"t_{StrAutoGenerateLanguageExcelName}");

			// 创建第一行
			IRow firstRow = sheet.CreateRow(nextRow);
			for (int i = 0; i < typeList.Count; i++)
			{
				string value = typeList[i];
				ICell cell = firstRow.CreateCell(i);
				cell.SetCellValue(value);
			}

			// 创建第二行
			IRow secondRow = sheet.CreateRow(++nextRow);
			for (int i = 0; i < nameList.Count; i++)
			{
				string value = nameList[i];
				ICell cell = secondRow.CreateCell(i);
				cell.SetCellValue(value);
			}

			// 创建第三行
			IRow thirdRow = sheet.CreateRow(++nextRow);
			for (int i = 0; i < flagList.Count; i++)
			{
				string value = flagList[i];
				ICell cell = thirdRow.CreateCell(i);
				cell.SetCellValue(value);
			}

			ICellStyle style = workbook.CreateCellStyle();
			style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left; //左对齐  

			// 创建数据行
			foreach (var pair in _cacheLanguage)
			{
				int hashCode = pair.Key;
				LanguageWrapper wrapper = pair.Value;
				IRow row = sheet.CreateRow(++nextRow);

				int cellNum = 0;

				// id
				ICell cell1 = row.CreateCell(cellNum);
				cell1.SetCellValue(hashCode);
				cell1.CellStyle = style;

				// source
				ICell cell2 = row.CreateCell(++cellNum);
				cell2.SetCellValue(wrapper.Source);
				cell2.CellStyle = style;

				// language
				ICell cell3 = row.CreateCell(++cellNum);
				cell3.SetCellValue(wrapper.Content);
				cell3.CellStyle = style;
			}

			// 设置格式
			sheet.SetColumnWidth(0, 15 * 256); //设置列宽为30个字符
			sheet.SetColumnWidth(1, 30 * 256); //设置列宽为30个字符
			sheet.SetColumnWidth(2, 30 * 256); //设置列宽为30个字符

			// 保存Excel文件
			string filePath = ExportConfig.Instance.LastOpenExcelPath + "\\" + StrAutoGenerateLanguageExcelName + ".xlsx";
			using (FileStream file = new FileStream(filePath, FileMode.Create))
			{
				workbook.Write(file);
				file.Close();
			}

			// 销毁句柄
			workbook.Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}


	/// <summary>
	/// 解析普通表格的多语言
	/// </summary>
	public static Dictionary<int, LanguageWrapper> ParseLanguage(ExcelData excel)
	{
		Dictionary<int, LanguageWrapper> cacheLanguages = new Dictionary<int, LanguageWrapper>();

		for(int i=0; i< excel.SheetDataList.Count; i++)
		{
			// 检测所有语言列
			SheetData sheet = excel.SheetDataList[i];
			foreach (var head in sheet.Heads)
			{
				if (head.Type == "language")
					ParseSingleLanguageToCache(cacheLanguages, sheet, head.CellNum);
				if (head.Type == "List<language>")
					ParseListLanguageToCache(cacheLanguages, sheet, head.CellNum);
			}
		}

		return cacheLanguages;
	}
	private static void ParseSingleLanguageToCache(Dictionary<int, LanguageWrapper> cacheLanguages, SheetData sheet, int cellNum)
	{
		foreach (var table in sheet.Tables)
		{
			// 获取格子内容
			string cellValue = table.GetCellValue(cellNum);

			int hashCode = cellValue.GetHashCode();
			if (cacheLanguages.ContainsKey(hashCode) == false)
			{
				LanguageWrapper wrapper = new LanguageWrapper();
				wrapper.Source = sheet.FileName;
				wrapper.Content = cellValue;
				cacheLanguages.Add(hashCode, wrapper);
			}
		}
	}
	private static void ParseListLanguageToCache(Dictionary<int, LanguageWrapper> cacheLanguages, SheetData sheet, int cellNum)
	{
		foreach (var table in sheet.Tables)
		{
			// 获取格子内容
			string cellValue = table.GetCellValue(cellNum);

			List<string> splitValues = StringConvert.StringToStringList(cellValue, ConstDefine.StrSplitChar);
			for (int i = 0; i < splitValues.Count; i++)
			{
				string splitValue = splitValues[i];
				int hashCode = splitValue.GetHashCode();
				if (cacheLanguages.ContainsKey(hashCode) == false)
				{
					LanguageWrapper wrapper = new LanguageWrapper();
					wrapper.Source = sheet.FileName;
					wrapper.Content = splitValue;
					cacheLanguages.Add(hashCode, wrapper);
				}
			}
		}
	}
}