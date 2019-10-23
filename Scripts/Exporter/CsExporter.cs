using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

[ExportAttribute("导出CS脚本")]
public class CsExporter : BaseExporter
{
	public CsExporter(SheetData sheetData)
		: base(sheetData)
	{
	}

	public override void ExportFile(string path, string createLogo)
	{
		string filePath = StringHelper.MakeSaveFullPath(path, $"Cfg{_sheet.FileName}.cs");
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(fs))
		{
			WriteNamespace(sw);

			// Table类
			WriteTabCalss(sw);
			sw.WriteLine("{");
			WriteTabClassMember(sw, createLogo);
			sw.WriteLine();
			WriteTabClassFunction(sw, createLogo);
			sw.WriteLine("}");
			sw.WriteLine();

			// Config类
			WriteCfgAttribute(sw);
			WriteCfgClass(sw);
			sw.WriteLine("{");
			WriteCfgClassFunction(sw);
			sw.WriteLine("}");
		}
	}
	private void WriteNamespace(StreamWriter sw)
	{
		sw.WriteLine("//--自动生成  请勿修改--");
		sw.WriteLine("//--研发人员实现LANG多语言接口--");
		sw.WriteLine();

		sw.WriteLine("using MotionGame;");
		sw.WriteLine("using MotionEngine.IO;");
		sw.WriteLine("using System.Collections.Generic;");
		sw.WriteLine();
	}
	private void WriteTabCalss(StreamWriter sw)
	{
		sw.WriteLine($"public class Cfg{_sheet.FileName}Tab : ConfigTab");
	}
	private void WriteTabClassMember(StreamWriter sw, string createLogo)
	{
		string tChar = "\t";
		string protectedChar = " { protected set; get; }";

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

			if (head.Type == "int" || head.Type == "List<int>" ||
				head.Type == "long" || head.Type == "List<long>" ||
				head.Type == "float" || head.Type == "List<float>" ||
				head.Type == "double" || head.Type == "List<double>" ||
				head.Type == "string" || head.Type == "List<string>" ||
				head.Type == "bool")
			{
				sw.WriteLine(tChar + $"public {head.Type} " + headName + protectedChar);
			}
			else if (head.Type == "language")
			{
				sw.WriteLine(tChar + $"public string " + headName + protectedChar);
			}
			else if (head.Type == "List<language>")
			{
				sw.WriteLine(tChar + $"public List<string> " + headName + protectedChar);
			}
			else if (head.Type.Contains("enum") || head.Type.Contains("class"))
			{
				string extendType = StringHelper.GetExtendType(head.Type);
				sw.WriteLine(tChar + $"public {extendType} " + headName + protectedChar);
			}
			else
			{
				throw new Exception($"Not support head type {head.Type}");
			}
		}
	}
	private void WriteTabClassFunction(StreamWriter sw, string createLogo)
	{
		string tChar = "\t";
		string tTwoChar = "\t\t";

		sw.WriteLine(tChar + "public override void ReadByte(ByteBuffer byteBuf)");
		sw.WriteLine(tChar + "{");

		for (int i = 0; i < _sheet.Heads.Count; i++)
		{
			HeadWrapper head = _sheet.Heads[i];

			if (head.IsNotes || head.Logo.Contains(createLogo) == false)
				continue;

			// 变量名称首字母大写
			string headName = StringHelper.ToUpperFirstChar(head.Name);

			// HashCode
			if (head.Name == ConstDefine.StrHeadId && head.Type == "string")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadInt();");
				continue;
			}

			if (head.Type == "bool")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadBool();");
			}
			else if(head.Type == "int")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadInt();");
			}
			else if (head.Type == "long")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadLong();");
			}
			else if (head.Type == "float")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadFloat();");
			}
			else if (head.Type == "double")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadDouble();");
			}

			else if (head.Type == "List<int>")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadListInt();");
			}
			else if (head.Type == "List<long>")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadListLong();");
			}
			else if (head.Type == "List<float>")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadListFloat();");
			}	
			else if (head.Type == "List<double>")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadListDouble();");
			}

			else if (head.Type == "string")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadUTF();");
			}
			else if (head.Type == "List<string>")
			{
				sw.WriteLine(tTwoChar + $"{headName} = byteBuf.ReadListUTF();");
			}

			// NOTE：多语言在字节流会是哈希值
			else if (head.Type == "language")
			{
				sw.WriteLine(tTwoChar + $"{headName} =  LANG.Convert(byteBuf.ReadInt());");
			}
			else if (head.Type == "List<language>")
			{
				sw.WriteLine(tTwoChar + $"{headName} = LANG.Convert(byteBuf.ReadListInt());");
			}

			else if (head.Type.Contains("enum"))
			{
				string extendType = StringHelper.GetExtendType(head.Type);
				sw.WriteLine(tTwoChar + $"{headName} = StringConvert.IndexToEnum<{extendType}>(byteBuf.ReadInt());");
			}
			else if (head.Type.Contains("class"))
			{
				string extendType = StringHelper.GetExtendType(head.Type);
				sw.WriteLine(tTwoChar + $"{headName} = {extendType}.Parse(byteBuf);");
			}
			else
			{
				throw new Exception($"Not support head type {head.Type}");
			}
		}

		sw.WriteLine(tChar + "}");
	}
	private void WriteCfgAttribute(StreamWriter sw)
	{
		sw.WriteLine($"[ConfigAttribute(nameof(EConfigType.{_sheet.FileName}))]");
	}
	private void WriteCfgClass(StreamWriter sw)
	{
		sw.WriteLine($"public partial class Cfg{_sheet.FileName} : AssetConfig");
	}
	private void WriteCfgClassFunction(StreamWriter sw)
	{
		string tChar = "\t";
		string tTwoChar = "\t\t";

		sw.WriteLine(tChar + "protected override ConfigTab ReadTab(ByteBuffer byteBuffer)");
		sw.WriteLine(tChar + "{");
		sw.WriteLine(tTwoChar + $"Cfg{_sheet.FileName}Tab tab = new Cfg{_sheet.FileName}Tab" + "{};");
		sw.WriteLine(tTwoChar + "tab.ReadByte(byteBuffer);");
		sw.WriteLine(tTwoChar + "return tab;");
		sw.WriteLine(tChar + "}");
	}
}