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

public class ExportConfig
{
	public class ExportWrapper
	{
		/// <summary>
		/// 导出器类型
		/// </summary>
		public Type ExporterType;

		/// <summary>
		/// 导出路径
		/// </summary>
		public string ExportPath;

		public void ReadConfig(StreamReader sr)
		{
			string typeName = sr.ReadLine();
			ExporterType = Type.GetType(typeName, false);

			string path = sr.ReadLine();
			if (Directory.Exists(path))
				ExportPath = path;
		}
		public void SaveConfig(StreamWriter sw)
		{
			if (ExporterType == null)
				sw.WriteLine("NONE");
			else
				sw.WriteLine(ExporterType.Name);

			sw.WriteLine(ExportPath);
		}
	}

	public static ExportConfig Instance = new ExportConfig();

	/// <summary>
	/// 存储在本地的配置文件名称
	/// </summary>
	private const string StrConfigFileName = "ExcelExport.data";

	/// <summary>
	/// 上次打开的Excel文件夹路径
	/// </summary>
	public string LastOpenExcelPath;

	// 导出配置
	public readonly ExportWrapper[] ClientExportInfos = new ExportWrapper[5];
	public readonly ExportWrapper[] ServerExportInfos = new ExportWrapper[5];
	public readonly ExportWrapper[] BattleExportInfos = new ExportWrapper[5];


	private ExportConfig()
	{
	}

	/// <summary>
	/// 初始化
	/// </summary>
	public void Init()
	{
		string appPath = Application.StartupPath;

		LastOpenExcelPath = appPath;

		for (int i = 1; i < ClientExportInfos.Length; i++)
		{
			ClientExportInfos[i] = new ExportWrapper();
			ClientExportInfos[i].ExportPath = appPath;
		}

		for (int i = 1; i < ServerExportInfos.Length; i++)
		{
			ServerExportInfos[i] = new ExportWrapper();
			ServerExportInfos[i].ExportPath = appPath;
		}

		for (int i = 1; i < BattleExportInfos.Length; i++)
		{
			BattleExportInfos[i] = new ExportWrapper();
			BattleExportInfos[i].ExportPath = appPath;
		}
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

			string excelPath = sr.ReadLine();
			if (Directory.Exists(excelPath))
				LastOpenExcelPath = excelPath;

			ClientExportInfos[1].ReadConfig(sr);
			ClientExportInfos[2].ReadConfig(sr);
			ClientExportInfos[3].ReadConfig(sr);
			ClientExportInfos[4].ReadConfig(sr);

			ServerExportInfos[1].ReadConfig(sr);
			ServerExportInfos[2].ReadConfig(sr);
			ServerExportInfos[3].ReadConfig(sr);
			ServerExportInfos[4].ReadConfig(sr);

			BattleExportInfos[1].ReadConfig(sr);
			BattleExportInfos[2].ReadConfig(sr);
			BattleExportInfos[3].ReadConfig(sr);
			BattleExportInfos[4].ReadConfig(sr);

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

			sw.WriteLine(LastOpenExcelPath);

			ClientExportInfos[1].SaveConfig(sw);
			ClientExportInfos[2].SaveConfig(sw);
			ClientExportInfos[3].SaveConfig(sw);
			ClientExportInfos[4].SaveConfig(sw);

			ServerExportInfos[1].SaveConfig(sw);
			ServerExportInfos[2].SaveConfig(sw);
			ServerExportInfos[3].SaveConfig(sw);
			ServerExportInfos[4].SaveConfig(sw);

			BattleExportInfos[1].SaveConfig(sw);
			BattleExportInfos[2].SaveConfig(sw);
			BattleExportInfos[3].SaveConfig(sw);
			BattleExportInfos[4].SaveConfig(sw);

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