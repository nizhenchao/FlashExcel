using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

[ExportAttribute("导出TXT文件")]
class TextExporter : BaseExporter
{
	public TextExporter(SheetData sheetData)
		: base(sheetData)
	{
	}

	public override void ExportFile(string path, string createLogo)
	{
		string[] lines = GetAllLines(createLogo);
		if (lines.Length == 0)
			throw new Exception("Write text file lines is empty.");

		// 创建文件
		string filePath = StringHelper.MakeSaveFullPath(path, $"{_sheet.FileName}.txt");
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(fs))
		{
			for (int i = 0; i < lines.Length; i++)
			{
				sw.WriteLine(lines[i]);
			}
		}
	}
	private string[] GetAllLines(string createLogo)
	{
		List<string> allLines = new List<string>();
		StringBuilder sb = new StringBuilder();

		// 写入表头类型
		sb.Clear();
		for (int i = 0; i < _sheet.Heads.Count; i++)
		{
			HeadWrapper head = _sheet.Heads[i];
			if (head.IsNotes) continue;
			if (head.Logo.Contains(createLogo))
			{
				string content;
				if (head.Type == "int")
					content = "int";
				else if (head.Type == "long")
					content = "long";
				else if (head.Type == "float")
					content = "float";
				else if (head.Type == "double")
					content = "double";
				else if (head.Type == "string")
					content = "string";
				else if (head.Type == "bool")
					content = "bool";
				else if (head.Type == "language")
					content = "string";
				else if (head.Type.Contains("enum"))
					content = "int";
				else if (head.Type.Contains("class"))
					content = "string";
				else if (head.Type.Contains("List"))
					content = "string"; //注意：列表直接导出为字符串
				else
					throw new Exception($"Not support head type {head.Type}");

				sb.Append(content);
				sb.Append("\t");
			}
		}
		sb.Remove(sb.Length - 1, 1); //移除最后一个的换行符
		allLines.Add(sb.ToString());

		// 写入表头名称
		sb.Clear();
		for (int i = 0; i < _sheet.Heads.Count; i++)
		{
			HeadWrapper head = _sheet.Heads[i];
			if (head.IsNotes) continue;
			if (head.Logo.Contains(createLogo))
			{
				sb.Append(head.Name);
				sb.Append("\t");
			}
		}
		sb.Remove(sb.Length - 1, 1); //移除最后一个的换行符
		allLines.Add(sb.ToString());

		// 写入数据
		for (int i = 0; i < _sheet.Tables.Count; i++)
		{
			sb.Clear();
			TableWrapper table = _sheet.Tables[i];
			for (int j = 0; j < _sheet.Heads.Count; j++)
			{
				HeadWrapper head = _sheet.Heads[j];
				if (head.IsNotes) continue;
				if (head.Logo.Contains(createLogo))
				{
					string cellValue = table.GetCellValue(head.CellNum);
					sb.Append(cellValue);
					sb.Append("\t");
				}
			}
			sb.Remove(sb.Length - 1, 1); //移除最后一个的换行符
			allLines.Add(sb.ToString());
		}

		return allLines.ToArray();
	}
}