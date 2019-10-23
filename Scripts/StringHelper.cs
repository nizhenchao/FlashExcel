using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public static class StringHelper
{
	/// <summary>
	/// 获取存储文件完整的路径
	/// </summary>
	public static string MakeSaveFullPath(string path, string fileName)
	{
		return path + Path.DirectorySeparatorChar + fileName;
	}

	/// <summary>
	/// 首字母大写
	/// </summary>
	public static string ToUpperFirstChar(string content)
	{
		return char.ToUpper(content[0]) + content.Substring(1);
	}

	/// <summary>
	/// 首字母小写
	/// </summary>
	public static string ToLowerFirstChar(string content)
	{
		return char.ToLower(content[0]) + content.Substring(1);
	}

	/// <summary>
	/// 获取扩展类型
	/// </summary>
	public static string GetExtendType(string content)
	{
		int indexOfA = content.IndexOf('[');
		int indexOfB = content.IndexOf(']');
		return content.Substring(indexOfA + 1, indexOfB - indexOfA - 1);
	}
}