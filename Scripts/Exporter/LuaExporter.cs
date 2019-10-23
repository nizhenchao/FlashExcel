using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MotionEngine.IO;

[ExportAttribute("导出LUA脚本")]
class LuaExporter : BaseExporter
{
	private bool _hasLanguageHead = false;

	public LuaExporter(SheetData sheetData)
		: base(sheetData)
	{
		// 检测是否有多语言列
		if (_sheet.IsContainsType("language") || _sheet.IsContainsType("List<language>"))
			_hasLanguageHead = true;
	}

	public override void ExportFile(string path, string createLogo)
	{
		string filePath = StringHelper.MakeSaveFullPath(path, $"Cfg{_sheet.FileName}.lua.txt");
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(fs))
		{
			WriteLUADesc(sw);
			WriteLUALang(sw);
			WriteLUAData(sw, createLogo);
			WriteLUAKeys(sw, createLogo);
			WriteLUAReimport(sw);
			WriteLUAEnd(sw);
			WriteLUAFun(sw);
		}
	}
	private string[] GetDataLines(string createLogo)
	{
		List<string> allLines = new List<string>();
		StringBuilder sb = new StringBuilder();
		string tChar = "\t";

		bool isStringID = IsStringID(createLogo);
		for (int i = 0; i < _sheet.Tables.Count; i++)
		{
			TableWrapper table = _sheet.Tables[i];

			sb.Clear();
			sb.Append(tChar);

			int idCellNum = GetIDCellNum(createLogo);
			string id = table.GetCellValue(idCellNum);
			if (isStringID)
				sb.Append($"{id} = ");
			else
				sb.Append($"[{id}] = ");

			sb.Append("{");

			for (int j = 0; j < _sheet.Heads.Count; j++)
			{
				HeadWrapper head = _sheet.Heads[j];
				if (head.IsNotes) continue;
				if (head.Logo.Contains(createLogo))
				{
					string cellValue = table.GetCellValue(head.CellNum);

					if (head.Type == "bool")
					{
						cellValue = StringConvert.StringToBool(cellValue).ToString().ToLower();
					}

					if (head.Type == "string")
					{
						cellValue = $"\"{cellValue}\"";
					}

					if (head.Type == "language")
					{
						int hashCode = cellValue.GetHashCode();
						cellValue = $"L({hashCode})";
					}

					if (head.Type.Contains("class"))
					{
						cellValue = $"\"{cellValue}\"";
					}

					if (head.Type.Contains("List"))
					{
						List<string> splitValues = StringConvert.StringToStringList(cellValue, ConstDefine.StrSplitChar);
						if (splitValues.Count == 0)
						{
							cellValue = "nil";
						}
						else
						{
							// 多语言LIST
							bool isLanguageList = head.Type.Contains("language");
							if (isLanguageList)
							{
								cellValue = "{";
								for (int k = 0; k < splitValues.Count; k++)
								{
									int hashCode = splitValues[k].GetHashCode();
									cellValue += $"L({hashCode})";
									cellValue += ",";
								}
								cellValue += "}";
							}

							// 字符串LIST
							bool isStringList = head.Type.Contains("string");
							if (isStringList)
							{
								cellValue = "{";
								for (int k = 0; k < splitValues.Count; k++)
								{
									cellValue += "\"";
									cellValue += splitValues[k];
									cellValue += "\"";
									cellValue += ",";
								}
								cellValue += "}";
							}

							// 其它List
							if (isLanguageList == false && isStringList == false)
							{
								cellValue = "{";
								for (int k = 0; k < splitValues.Count; k++)
								{
									cellValue += splitValues[k];
									cellValue += ",";
								}
								cellValue += "}";
							}
						}
					}

					sb.Append(cellValue);
					sb.Append(",");
				}
			}

			sb.Append("},");
			allLines.Add(sb.ToString());
		}

		return allLines.ToArray();
	}
	private string GetLuaKeys(string createLogo)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("local keys = {");

		for (int i = 0; i < _sheet.Heads.Count; i++)
		{
			HeadWrapper head = _sheet.Heads[i];
			if (head.IsNotes) continue;
			if (head.Logo.Contains(createLogo))
			{
				string name = $"\"{head.Name}\"";
				sb.Append(name);
				sb.Append(",");
			}
		}

		sb.Append("}");
		return sb.ToString();
	}
	private void WriteLUADesc(StreamWriter sw)
	{
		sw.WriteLine("--自动生成  请勿修改--");
		sw.WriteLine();
	}
	private void WriteLUALang(StreamWriter sw)
	{
		if (_hasLanguageHead)
		{
			sw.WriteLine("--研发人员实现多语言接口--");
			sw.WriteLine("local L = GetLanguageData");
			sw.WriteLine();
		}
	}
	private void WriteLUAData(StreamWriter sw, string createLogo)
	{
		sw.WriteLine($"local OriginCfg{_sheet.FileName} = ");
		sw.WriteLine("{");
		string[] lines = GetDataLines(createLogo);
		for (int i = 0; i < lines.Length; i++)
		{
			sw.WriteLine(lines[i]);
		}
		sw.WriteLine("}");
		sw.WriteLine();
	}
	private void WriteLUAKeys(StreamWriter sw, string createLogo)
	{
		string keys = GetLuaKeys(createLogo);
		sw.WriteLine(keys);
		sw.WriteLine();
	}
	private void WriteLUAReimport(StreamWriter sw)
	{
		string tChar = "\t";
		string tTwoChar = "\t\t";

		sw.WriteLine("local DATA = {}");
		sw.WriteLine($"for i,v in pairs(OriginCfg{_sheet.FileName}) do");
		sw.WriteLine(tChar + "local tmp = { }");
		sw.WriteLine(tChar + "for j,k in ipairs(keys) do");
		sw.WriteLine(tTwoChar + "tmp[k] = v[j]");
		sw.WriteLine(tChar + "end");
		sw.WriteLine(tChar + "DATA[v[1]] = tmp");
		sw.WriteLine("end");
		sw.WriteLine();
	}
	private void WriteLUAEnd(StreamWriter sw)
	{
		sw.WriteLine($"OriginCfg{_sheet.FileName} = nil");
		sw.WriteLine($"_G.Cfg{_sheet.FileName}_Len = {_sheet.Tables.Count}");
		sw.WriteLine($"_G.Cfg{_sheet.FileName} = DATA");
	}
	private void WriteLUAFun(StreamWriter sw)
	{
		string tChar = "\t";
		string tTwoChar = "\t\t";

		sw.WriteLine();
		sw.WriteLine($"function GetCfg{_sheet.FileName}Value(key)");
		sw.WriteLine(tChar + $"local value = Cfg{_sheet.FileName}[key]");
		sw.WriteLine(tChar + "if(value == nil) then");
		sw.WriteLine(tTwoChar + $"Debug.Warning(\"Not found Cfg{_sheet.FileName} id : \", key)");
		sw.WriteLine(tChar + "end");
		sw.WriteLine(tChar + "return value");
		sw.WriteLine("end");
	}

	/// <summary>
	/// 检测ID列类型是否是字符串
	/// </summary>
	/// <param name="createLogo"></param>
	private bool IsStringID(string createLogo)
	{
		for (int i = 0; i < _sheet.Heads.Count; i++)
		{
			HeadWrapper head = _sheet.Heads[i];
			if (head.IsNotes) continue;
			if (head.Logo.Contains(createLogo))
			{
				if (head.Name == ConstDefine.StrHeadId)
					return head.Type == "string";
			}
		}

		throw new Exception($"id列的导出标记需要增加 {createLogo} ");
	}

	/// <summary>
	/// 获取ID的列号
	/// </summary>
	private int GetIDCellNum(string createLogo)
	{
		for (int i = 0; i < _sheet.Heads.Count; i++)
		{
			HeadWrapper head = _sheet.Heads[i];
			if (head.IsNotes) continue;
			if (head.Logo.Contains(createLogo))
			{
				if (head.Name == ConstDefine.StrHeadId)
					return head.CellNum;
			}
		}

		throw new Exception($"id列的导出标记需要增加 {createLogo} ");
	}
}