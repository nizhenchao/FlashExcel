//**************************************************
// Copyright©2018-2019 何冠峰
// Licensed under the MIT license
//**************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;


public class SheetData
{
	/// <summary>
	/// 导出文件名称
	/// </summary>
	public string FileName { get; }

	/// <summary>
	/// 表格头部集合
	/// </summary>
	public readonly List<HeadWrapper> Heads = new List<HeadWrapper>();

	/// <summary>
	/// 表格数据集合
	/// </summary>
	public readonly List<TableWrapper> Tables = new List<TableWrapper>();

	// 导出器列表
	private readonly List<BaseExporter> _exporters = new List<BaseExporter>();

	// 表格类
	private IWorkbook _workbook = null;

	// 页签类
	private ISheet _sheet = null;

	// 公式计算器
	private XSSFFormulaEvaluator _evaluator = null;


	public SheetData(string sheetName)
	{
		FileName = StringHelper.ToUpperFirstChar(sheetName.Replace("t_", ""));
	}

	/// <summary>
	/// 加载页签
	/// </summary>
	public void Load(IWorkbook workbook, ISheet sheet)
	{
		_workbook = workbook;
		_sheet = sheet;

		// 公式计算器
		_evaluator = new XSSFFormulaEvaluator(_workbook);
		
		int firstRowNum = sheet.FirstRowNum;

		// 数据头一共三行
		IRow row1 = sheet.GetRow(firstRowNum); //类型
		IRow row2 = sheet.GetRow(++firstRowNum); //名称
		IRow row3 = sheet.GetRow(++firstRowNum); //CBS

		// 检测策划备注行
		while (true)
		{
			int checkRow = firstRowNum + 1;
			if (checkRow > sheet.LastRowNum)
				break;
			IRow row = sheet.GetRow(checkRow);
			if (IsNotesRow(row))
				++firstRowNum;
			else
				break;
		}

		// 组织头部数据
		for (int cellNum = row1.FirstCellNum; cellNum < row1.LastCellNum; cellNum++)
		{
			ICell row1cell = row1.GetCell(cellNum);
			ICell row2cell = row2.GetCell(cellNum);
			ICell row3cell = row3.GetCell(cellNum);

			// 检测重复的列
			string headName = GetCellValue(row1cell);
			bool isNotesRow= headName.Contains(ConstDefine.StrNotesRow);
			if (isNotesRow == false)
			{
				if (IsContainsHead(headName))
					throw new Exception($"检测到重复列 : {headName}");
			}

			// 创建Wrapper
			string type = GetCellValue(row1cell);
			string name = GetCellValue(row2cell);
			string logo = GetCellValue(row3cell);
			HeadWrapper wrapper = new HeadWrapper(cellNum, name, type, logo);
			Heads.Add(wrapper);
		}

		// 如果没有ID列
		if (IsContainsHead(ConstDefine.StrHeadId) == false)
		{
			throw new Exception("表格必须设立一个 'id' 列.");
		}

		// 所有数据行
		int tableBeginRowNum = ++firstRowNum; //Table初始行
		for (int rowNum = tableBeginRowNum; rowNum <= sheet.LastRowNum; rowNum++)
		{	
			IRow row = sheet.GetRow(rowNum);

			// 如果是结尾行
			if (IsEndRow(row))
				break;

			TableWrapper wrapper = new TableWrapper(rowNum, row);
			wrapper.CacheAllCellValue(this);
			Tables.Add(wrapper);
		}

		// 创建所有注册的导出器
		for(int i=0; i< ExportHandler.ExportTypes.Count; i++)
		{
			Type type = ExportHandler.ExportTypes[i];
			BaseExporter exporter = (BaseExporter)Activator.CreateInstance(type, this);
			_exporters.Add(exporter);
		}
	}

	/// <summary>
	/// 导出页签
	/// </summary>
	/// <param name="type">导出器类型</param>
	/// <param name="path">导出路径</param>
	/// <param name="createLogo">导出标记</param>
	public void Export(Type type, string path, string createLogo)
	{
		BaseExporter exporter = GetExporter(type);
		if(exporter.IsContainsLogo(createLogo))
			exporter.ExportFile(path, createLogo);
	}

	/// <summary>
	/// 获取导出器
	/// </summary>
	public BaseExporter GetExporter(Type type)
	{
		for (int i = 0; i < _exporters.Count; i++)
		{
			BaseExporter exporter = _exporters[i];
			if (exporter.GetType() == type)
				return exporter;
		}
		throw new Exception($"Should never get here. {type}");
	}

	/// <summary>
	/// 是否是备注行
	/// </summary>
	public bool IsNotesRow(IRow row)
	{
		ICell firstCell = row.GetCell(row.FirstCellNum);
		string value = GetCellValue(firstCell);
		return value.ToLower().Contains(ConstDefine.StrNotesRow);
	}

	/// <summary>
	/// 是否是结束行
	/// </summary>
	public bool IsEndRow(IRow row)
	{
		if (row == null)
			return true;

		ICell firstCell = row.GetCell(row.FirstCellNum);
		if (firstCell == null)
			return true;

		string value = GetCellValue(firstCell);
		if (string.IsNullOrEmpty(value))
			return true;

		return false;
	}

	/// <summary>
	/// 检测标记是否存在
	/// </summary>
	public bool IsContainsLogo(string logo)
	{
		for (int i = 0; i < Heads.Count; i++)
		{
			if (Heads[i].Logo.Contains(logo))
				return true;
		}
		return false;
	}

	/// <summary>
	/// 是否包含类型
	/// </summary>
	public bool IsContainsType(string typeName)
	{
		for (int i = 0; i < Heads.Count; i++)
		{
			if (Heads[i].Type == typeName)
				return true;
		}
		return false;
	}

	/// <summary>
	/// 是否包含该头
	/// </summary>
	public bool IsContainsHead(string headName)
	{
		for (int i = 0; i < Heads.Count; i++)
		{
			if (Heads[i].Name == headName)
				return true;
		}
		return false;
	}

	/// <summary>
	/// 获取页签
	/// </summary>
	public ISheet GetSheet()
	{
		return _sheet;
	}

	/// <summary>
	/// 获取格子值
	/// </summary>
	public string GetCellValue(ICell cell)
	{
		// 注意：内容为空的单元格有时候会为空对象
		if (cell == null)
			return string.Empty;

		if (cell.CellType == CellType.Blank)
		{
			return string.Empty;
		}
		else if (cell.CellType == CellType.Numeric)
		{
			return cell.NumericCellValue.ToString();
		}
		else if (cell.CellType == CellType.String)
		{
			return cell.StringCellValue;
		}
		else if (cell.CellType == CellType.Boolean)
		{
			return cell.BooleanCellValue.ToString().ToLower();
		}
		else if (cell.CellType == CellType.Formula)
		{
			// 注意：公式只支持数值和字符串类型
			var formulaValue = _evaluator.Evaluate(cell);
			if (formulaValue.CellType == CellType.Numeric)
				return formulaValue.NumberValue.ToString();
			else if (formulaValue.CellType == CellType.String)
				return formulaValue.StringValue;
			else
				throw new Exception($"未支持的公式类型 : {formulaValue.CellType}");
		}
		else
		{
			throw new Exception($"未支持的单元格类型 : {cell.CellType}");
		}
	}
}