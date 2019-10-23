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

public class SettingConfig
{
	public static readonly SettingConfig Instance = new SettingConfig();

	/// <summary>
	/// 存储在本地的配置文件名称
	/// </summary>
	private const string StrConfigFileName = "ExcelSetting.data";

	/// <summary>
	/// 是否开启数值单元格自动补全
	/// </summary>
	public bool EnableAutoCompleteCell = false;

	/// <summary>
	/// 数值单元格自动补全的内容
	/// </summary>
	public string AutoCompleteCellContent = "0";


	private SettingConfig()
	{
	}

	/// <summary>
	/// 初始化
	/// </summary>
	public void Init()
	{
	}

	/// <summary>
	/// 读取配置文件
	/// </summary>
	public void ReadConfig()
	{
		string appPath = Application.StartupPath;
		string configPath = Path.Combine(appPath, StrConfigFileName);

		// 如果配置文件不存在
		if (!File.Exists(configPath))
			return;

		FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.Read);
		try
		{
			StreamReader sr = new StreamReader(fs);

			EnableAutoCompleteCell = sr.ReadLine() == "true";
			AutoCompleteCellContent = sr.ReadLine();

			sr.Dispose();
			sr.Close();
		}
		catch (Exception e)
		{
			throw e;
		}
		finally
		{
			fs.Dispose();
			fs.Close();
		}
	}

	/// <summary>
	/// 存储配置文件
	/// </summary>
	public void SaveConfig()
	{
		string appPath = Application.StartupPath;
		string configPath = Path.Combine(appPath, StrConfigFileName);

		// 删除旧文件
		if (File.Exists(configPath))
			File.Delete(configPath);

		FileStream fs = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
		try
		{
			StreamWriter sw = new StreamWriter(fs);
			sw.Flush();

			sw.WriteLine(EnableAutoCompleteCell ? "true" : "false");
			sw.WriteLine(AutoCompleteCellContent);

			sw.Flush();
			sw.Dispose();
			sw.Close();
		}
		catch (Exception e)
		{
			throw e;
		}
		finally
		{
			fs.Dispose();
			fs.Close();
		}
	}
}