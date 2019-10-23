//**************************************************
// Copyright©2018-2019 何冠峰
// Licensed under the MIT license
//**************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

public class TableWrapper
{
	/// <summary>
	/// 行号
	/// </summary>
	public int RowNum { get; }

	/// <summary>
	/// 整行数据类
	/// </summary>
	public IRow Row { get; }

	/// <summary>
	/// 单元格数据
	/// Key为cellNum
	/// Value为cellValue
	/// </summary>
	private Dictionary<int, string> _cellValues = new Dictionary<int, string>();


	public TableWrapper(int rowNum, IRow row)
	{
		RowNum = rowNum;
		Row = row;
	}

	/// <summary>
	/// 缓存单元格数据
	/// </summary>
	public void CacheAllCellValue(SheetData sheet)
	{
		for (int i = 0; i < sheet.Heads.Count; i++)
		{
			HeadWrapper head = sheet.Heads[i];

			// 如果是备注列
			if (head.IsNotes)
			{
				_cellValues.Add(head.CellNum, string.Empty);
				continue;
			}

			// 获取单元格字符串
			ICell cell = Row.GetCell(head.CellNum);
			string value = sheet.GetCellValue(cell);

			// 检测数值单元格是否为空值		
			if (string.IsNullOrEmpty(value))
			{
				if (head.Type == "int" || head.Type == "long" || head.Type == "float" || head.Type == "double"
					|| head.Type == "enum" || head.Type == "bool")
				{
					// 如果开启了自动补全功能
					if (SettingConfig.Instance.EnableAutoCompleteCell)
						value = SettingConfig.Instance.AutoCompleteCellContent;
					else
						throw new Exception($"数值单元格不能为空，请检查{head.Name}列");
				}
			}

			_cellValues.Add(head.CellNum, value);
		}
	}

	/// <summary>
	/// 获取单元格数据
	/// </summary>
	public string GetCellValue(int cellNum)
	{
		return _cellValues[cellNum];
	}
}