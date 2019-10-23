using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MotionEngine.IO;

[ExportAttribute("导出ILR脚本")]
public class ILRExporter : BaseExporter
{
	public ILRExporter(SheetData sheetData)
		: base(sheetData)
	{
	}

	public override void ExportFile(string path, string createLogo)
	{
		string filePath = StringHelper.MakeSaveFullPath(path, $"Cfg{_sheet.FileName}.cs");
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(fs))
		{
			string tChar = "\t";

			WriteNamespace(sw);

			// Table类
			WriteTabCalss(sw);
			sw.WriteLine(tChar + "{");
			WriteTabClassMember(sw, createLogo);
			sw.WriteLine(tChar + "}");
			sw.WriteLine();

			// Config类
			WriteCfgClass(sw);
			sw.WriteLine(tChar + "{");
			WriteCfgClassInstance(sw);
			WriteCfgClassData(sw, createLogo);
			sw.WriteLine(tChar + "}");

			WriteNamespaceEnd(sw);
		}
	}
	private string[] GetDataLines(string createLogo)
	{
		List<string> allLines = new List<string>();
		StringBuilder sb = new StringBuilder();
		StringBuilder content = new StringBuilder();

		bool isStringID = IsStringID(createLogo);
		for (int i = 0; i < _sheet.Tables.Count; i++)
		{
			TableWrapper table = _sheet.Tables[i];

			int idCellNum = GetIDCellNum(createLogo);
			string id = table.GetCellValue(idCellNum);
			if (isStringID)
				id = id.GetHashCode().ToString();

			sb.Clear();
			content.Clear();

			for (int j = 0; j < _sheet.Heads.Count; j++)
			{
				HeadWrapper head = _sheet.Heads[j];
				if (head.IsNotes || head.Logo.Contains(createLogo) == false)
					continue;

				string cellValue = table.GetCellValue(head.CellNum);

				if (head.Type == "float")
					cellValue = $"{cellValue}f";

				if (head.Type == "bool")
					cellValue = StringConvert.StringToBool(cellValue).ToString().ToLower();

				if (head.Type == "string")
				{
					cellValue = $"\"{cellValue}\"";
				}

				if (head.Type == "language")
				{
					int hashCode = cellValue.GetHashCode();
					cellValue = $"LANG.Convert({hashCode})";
				}

				if (head.Type.Contains("enum"))
				{
					string extendType = StringHelper.GetExtendType(head.Type);
					cellValue = $"({extendType}){cellValue}"; //TODO 因为是热更层，这里对枚举进行强转
				}

				if (head.Type.Contains("class"))
				{
					string extendType = StringHelper.GetExtendType(head.Type);
					cellValue = $"{extendType}.Parse(\"{cellValue}\")";
				}

				if (head.Type.Contains("List"))
				{
					List<string> splitValues = StringConvert.StringToStringList(cellValue, ConstDefine.StrSplitChar);
					if (splitValues.Count == 0)
					{
						if (head.Type.Contains("language"))
							cellValue = $"new List<string>()";
						else
							cellValue = $"new {head.Type}()";
					}
					else
					{
						// 多语言LIST
						bool isLanguageList = head.Type.Contains("language");
						if (isLanguageList)
						{
							cellValue = "new List<string>()";
							cellValue += "{";
							for (int k = 0; k < splitValues.Count; k++)
							{
								int hashCode = splitValues[k].GetHashCode();
								cellValue += $"LANG.Convert({hashCode})";
								if (k < splitValues.Count - 1) cellValue += ",";
							}
							cellValue += "}";
						}

						// 字符串LIST
						bool isStringList = head.Type.Contains("string");
						if (isStringList)
						{
							cellValue = $"new {head.Type}()";
							cellValue += "{";
							for (int k = 0; k < splitValues.Count; k++)
							{
								cellValue += "\"";
								cellValue += splitValues[k];
								cellValue += "\"";
								if (k < splitValues.Count - 1) cellValue += ",";
							}
							cellValue += "}";
						}

						// 浮点数LIST
						bool isFloatList = head.Type.Contains("float");
						if (isFloatList)
						{
							cellValue = $"new {head.Type}()";
							cellValue += "{";
							for (int k = 0; k < splitValues.Count; k++)
							{
								cellValue += splitValues[k];
								cellValue += "f";
								if (k < splitValues.Count - 1) cellValue += ",";
							}
							cellValue += "}";
						}

						// 其它List
						if (isLanguageList == false && isStringList == false && isFloatList == false)
						{
							cellValue = $"new {head.Type}()";
							cellValue += "{";
							for (int k = 0; k < splitValues.Count; k++)
							{
								cellValue += splitValues[k];
								if (k < splitValues.Count - 1) cellValue += ",";
							}
							cellValue += "}";
						}
					}
				}

				content.Append(cellValue);
				if (j < _sheet.Heads.Count - 1) content.Append(", ");
			}

			sb.Append($"AddElement({id}, new Cfg{_sheet.FileName}Tab({content.ToString()}));");
			allLines.Add(sb.ToString());
		}

		return allLines.ToArray();
	}
	private void WriteNamespace(StreamWriter sw)
	{
		sw.WriteLine("//--自动生成  请勿修改--");
		sw.WriteLine("//--研发人员实现LANG多语言接口--");
		sw.WriteLine();

		sw.WriteLine("using MotionEngine;");
		sw.WriteLine("using MotionEngine.IO;");
		sw.WriteLine("using System.Collections.Generic;");
		sw.WriteLine();

		sw.WriteLine("namespace Hotfix");
		sw.WriteLine("{");
	}
	private void WriteTabCalss(StreamWriter sw)
	{
		string tChar = "\t";
		sw.WriteLine(tChar + $"public class Cfg{_sheet.FileName}Tab : ConfigTab");
	}
	private void WriteTabClassMember(StreamWriter sw, string createLogo)
	{
		string tTwoChar = "\t\t";
		string tThreeChar = "\t\t\t";
		string protectedChar = " { protected set; get; }";

		List<string> headTypeList = new List<string>();
		headTypeList.Add("int");

		List<string> headNameList = new List<string>();
		headNameList.Add("Id");

		for (int i = 0; i < _sheet.Heads.Count; i++)
		{
			HeadWrapper head = _sheet.Heads[i];

			if (head.IsNotes || head.Logo.Contains(createLogo) == false)
				continue;

			// 跳过ID
			if (head.Name == ConstDefine.StrHeadId)
				continue;

			// 变量名称首字母大写
			string headName = StringHelper.ToUpperFirstChar(head.Name);
			headNameList.Add(headName);

			if (head.Type == "int" || head.Type == "List<int>" ||
				head.Type == "long" || head.Type == "List<long>" ||
				head.Type == "float" || head.Type == "List<float>" ||
				head.Type == "double" || head.Type == "List<double>" ||
				head.Type == "string" || head.Type == "List<string>" ||
				head.Type == "bool")
			{
				sw.WriteLine(tTwoChar + $"public {head.Type} {headName}" + protectedChar);
				headTypeList.Add(head.Type);
			}
			else if (head.Type == "language")
			{
				sw.WriteLine(tTwoChar + $"public string {headName}" + protectedChar);
				headTypeList.Add("string");
			}
			else if (head.Type == "List<language>")
			{
				sw.WriteLine(tTwoChar + $"public List<string> {headName}" + protectedChar);
				headTypeList.Add("List<string>");
			}
			else if (head.Type.Contains("enum") || head.Type.Contains("class"))
			{
				string extendType = StringHelper.GetExtendType(head.Type);
				sw.WriteLine(tTwoChar + $"public {extendType} {headName}" + protectedChar);
				headTypeList.Add(extendType);
			}
			else
			{
				throw new Exception($"Not support head type {head.Type}");
			}
		}

		// 构造函数
		sw.WriteLine();
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < headNameList.Count; i++)
		{
			sb.Append($"{headTypeList[i]} {StringHelper.ToLowerFirstChar(headNameList[i])}");
			if (i < headNameList.Count - 1) sb.Append(", ");
		}
		sw.WriteLine(tTwoChar + $"public Cfg{_sheet.FileName}Tab({sb.ToString()})");
		sw.WriteLine(tTwoChar + "{");
		for (int i = 0; i < headNameList.Count; i++)
		{
			string name = headNameList[i];
			sw.WriteLine(tThreeChar + $"{StringHelper.ToUpperFirstChar(name)} = {StringHelper.ToLowerFirstChar(name)};");
		}
		sw.WriteLine(tTwoChar + "}");
	}
	private void WriteCfgClass(StreamWriter sw)
	{
		string tChar = "\t";
		sw.WriteLine(tChar + $"public partial class Cfg{_sheet.FileName} : AssetConfig");
	}
	private void WriteCfgClassInstance(StreamWriter sw)
	{
		string tTwoChar = "\t\t";
		string tThreeChar = "\t\t\t";
		string tFourChar = "\t\t\t\t";
		sw.WriteLine(tTwoChar + $"private static Cfg{_sheet.FileName} _instance;");
		sw.WriteLine(tTwoChar + $"public static Cfg{_sheet.FileName} Instance");
		sw.WriteLine(tTwoChar + "{");
		sw.WriteLine(tThreeChar + "get");
		sw.WriteLine(tThreeChar + "{");
		sw.WriteLine(tFourChar + $"if (_instance == null) {{ _instance = new Cfg{_sheet.FileName}(); _instance.Create(); }}");
		sw.WriteLine(tFourChar + "return _instance;");
		sw.WriteLine(tThreeChar + "}");
		sw.WriteLine(tTwoChar + "}");

		sw.WriteLine();
		sw.WriteLine(tTwoChar + $"private Cfg{_sheet.FileName}() {{ }}");

		sw.WriteLine(tTwoChar + $"public Cfg{_sheet.FileName}Tab GetCfgTab(int key)");
		sw.WriteLine(tTwoChar + "{");
		sw.WriteLine(tThreeChar + $"return GetTab(key) as Cfg{_sheet.FileName}Tab;");
		sw.WriteLine(tTwoChar + "}");
	}
	private void WriteCfgClassData(StreamWriter sw, string createLogo)
	{
		string tTwoChar = "\t\t";
		string tThreeChar = "\t\t\t";
		string[] lines = GetDataLines(createLogo);

		sw.WriteLine(tTwoChar + "public void Create()");
		sw.WriteLine(tTwoChar + "{");
		for (int i = 0; i < lines.Length; i++)
		{
			sw.WriteLine(tThreeChar + lines[i]);
		}
		sw.WriteLine(tTwoChar + "}");
	}
	private void WriteNamespaceEnd(StreamWriter sw)
	{
		sw.WriteLine("}");
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