using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using MotionEngine.IO;

public class ConfigDefine
{
	public const int CfgStreamMaxLen = 1024 * 1024 * 128; //最大128MB
	public const int TabStreamMaxLen = 1024 * 256; //最大256K
	public const short TabStreamHead = 0x2B2B; //文件标记
}

[ExportAttribute("导出Byte文件")]
public class BytesExporter : BaseExporter
{
	public BytesExporter(SheetData sheetData)
		: base(sheetData)
	{
	}

	public override void ExportFile(string path, string createLogo)
	{
		ByteBuffer fileBuffer = new ByteBuffer(ConfigDefine.CfgStreamMaxLen);
		ByteBuffer tableBuffer = new ByteBuffer(ConfigDefine.TabStreamMaxLen);

		for (int i = 0; i < _sheet.Tables.Count; i++)
		{
			TableWrapper table = _sheet.Tables[i];

			// 写入行标记
			fileBuffer.WriteShort(ConfigDefine.TabStreamHead);

			// 清空缓存
			tableBuffer.Clear();

			// 写入数据
			for (int j = 0; j < _sheet.Heads.Count; j++)
			{
				HeadWrapper head = _sheet.Heads[j];
				string value = table.GetCellValue(head.CellNum);
				WriteCell(tableBuffer, head, value, createLogo);
			}

			// 检测数据大小有效性
			int tabSize = tableBuffer.ReadableBytes();
			if (tabSize == 0)
				throw new Exception($"{_sheet.FileName} tableBuffer readable bytes is zero.");
			
			// 写入到总缓存
			fileBuffer.WriteInt(tabSize);
			fileBuffer.WriteBytes(tableBuffer.ReadBytes(tabSize));
		}

		// 创建文件
		string filePath = StringHelper.MakeSaveFullPath(path, $"{_sheet.FileName}.bytes");
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		{
			byte[] data = fileBuffer.Buf;
			int length = fileBuffer.ReadableBytes();
			fs.Write(data, 0, length);
		}
	}
	private void WriteCell(ByteBuffer buffer, HeadWrapper head, string value, string createLogo)
	{
		if (head.IsNotes || head.Logo.Contains(createLogo) == false)
			return;

		if (head.Type == "int")
		{
			buffer.WriteInt(StringConvert.StringToValue<int>(value));
		}
		else if (head.Type == "long")
		{
			buffer.WriteLong(StringConvert.StringToValue<long>(value));
		}
		else if (head.Type == "float")
		{
			buffer.WriteFloat(StringConvert.StringToValue<float>(value));
		}
		else if (head.Type == "double")
		{
			buffer.WriteDouble(StringConvert.StringToValue<double>(value));
		}

		else if (head.Type == "List<int>")
		{
			buffer.WriteListInt(StringConvert.StringToValueList<int>(value, ConstDefine.StrSplitChar));
		}
		else if (head.Type == "List<long>")
		{
			buffer.WriteListLong(StringConvert.StringToValueList<long>(value, ConstDefine.StrSplitChar));
		}
		else if (head.Type == "List<float>")
		{
			buffer.WriteListFloat(StringConvert.StringToValueList<float>(value, ConstDefine.StrSplitChar));
		}
		else if (head.Type == "List<double>")
		{
			buffer.WriteListDouble(StringConvert.StringToValueList<double>(value, ConstDefine.StrSplitChar));
		}

		// bool
		else if (head.Type == "bool")
		{
			buffer.WriteBool(StringConvert.StringToBool(value));
		}

		// string
		else if (head.Type == "string")
		{
			buffer.WriteUTF(value);
		}
		else if (head.Type == "List<string>")
		{
			buffer.WriteListUTF(StringConvert.StringToStringList(value, ConstDefine.StrSplitChar));
		}

		// NOTE：多语言在字节流会是哈希值
		else if (head.Type == "language")
		{
			buffer.WriteInt(value.GetHashCode());
		}
		else if (head.Type == "List<language>")
		{
			List<string> langList = StringConvert.StringToStringList(value, ConstDefine.StrSplitChar);
			List<int> hashList = new List<int>();
			for(int i=0; i< langList.Count; i++)
			{
				hashList.Add(langList[i].GetHashCode());
			}
			buffer.WriteListInt(hashList);
		}

		// wrapper
		else if (head.Type.Contains("class"))
		{
			buffer.WriteUTF(value);
		}

		// enum
		else if (head.Type.Contains("enum"))
		{
			buffer.WriteInt(StringConvert.StringToValue<int>(value));
		}

		else
		{
			throw new Exception($"Not support head type {head.Type}");
		}
	}
}