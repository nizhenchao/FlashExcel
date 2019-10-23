using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

public partial class MainForm : Form
{
	private class ComboItem : object
	{
		public Type ExportType { private set; get; }
		public string ExportName { private set; get; }

		public ComboItem(Type type, string name)
		{
			ExportType = type;
			ExportName = name;
		}
	}


	public MainForm()
	{
		InitializeComponent();
	}

	/// <summary>
	/// 当窗体加载完毕
	/// </summary>
	private void MainForm_Load(object sender, EventArgs e)
	{
		// 初始化配置
		ExportConfig.Instance.Init();
		SettingConfig.Instance.Init();

		// 读取配置
		ExportConfig.Instance.ReadConfig();
		SettingConfig.Instance.ReadConfig();

		// 初始化窗口控件
		InitWindowsForm();
	}

	/// <summary>
	/// 当窗体关闭的时候
	/// </summary>
	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);

		// 退出前存储配置
		ExportConfig.Instance.SaveConfig();
		SettingConfig.Instance.SaveConfig();
	}

	/// <summary>
	/// 初始化主窗体
	/// </summary>
	private void InitWindowsForm()
	{
		// 客户端
		{
			InitComboBox(clientComboBox1, ExportConfig.Instance.ClientExportInfos[1].ExporterType);
			InitComboBox(clientComboBox2, ExportConfig.Instance.ClientExportInfos[2].ExporterType);
			InitComboBox(clientComboBox3, ExportConfig.Instance.ClientExportInfos[3].ExporterType);
			InitComboBox(clientComboBox4, ExportConfig.Instance.ClientExportInfos[4].ExporterType);

			clientPathLabel1.Text = ExportConfig.Instance.ClientExportInfos[1].ExportPath;
			clientPathLabel2.Text = ExportConfig.Instance.ClientExportInfos[2].ExportPath;
			clientPathLabel3.Text = ExportConfig.Instance.ClientExportInfos[3].ExportPath;
			clientPathLabel4.Text = ExportConfig.Instance.ClientExportInfos[4].ExportPath;
		}

		// 服务器
		{
			InitComboBox(serverComboBox1, ExportConfig.Instance.ServerExportInfos[1].ExporterType);
			InitComboBox(serverComboBox2, ExportConfig.Instance.ServerExportInfos[2].ExporterType);
			InitComboBox(serverComboBox3, ExportConfig.Instance.ServerExportInfos[3].ExporterType);
			InitComboBox(serverComboBox4, ExportConfig.Instance.ServerExportInfos[4].ExporterType);

			serverPathLabel1.Text = ExportConfig.Instance.ServerExportInfos[1].ExportPath;
			serverPathLabel2.Text = ExportConfig.Instance.ServerExportInfos[2].ExportPath;
			serverPathLabel3.Text = ExportConfig.Instance.ServerExportInfos[3].ExportPath;
			serverPathLabel4.Text = ExportConfig.Instance.ServerExportInfos[4].ExportPath;
		}

		// 战服
		{
			InitComboBox(battleComboBox1, ExportConfig.Instance.BattleExportInfos[1].ExporterType);
			InitComboBox(battleComboBox2, ExportConfig.Instance.BattleExportInfos[2].ExporterType);
			InitComboBox(battleComboBox3, ExportConfig.Instance.BattleExportInfos[3].ExporterType);
			InitComboBox(battleComboBox4, ExportConfig.Instance.BattleExportInfos[4].ExporterType);

			battlePathLabel1.Text = ExportConfig.Instance.BattleExportInfos[1].ExportPath;
			battlePathLabel2.Text = ExportConfig.Instance.BattleExportInfos[2].ExportPath;
			battlePathLabel3.Text = ExportConfig.Instance.BattleExportInfos[3].ExportPath;
			battlePathLabel4.Text = ExportConfig.Instance.BattleExportInfos[4].ExportPath;
		}
	}
	private void InitComboBox(ComboBox cb, Type defaultExportType)
	{
		ComboItem noneItem = new ComboItem(null, "NONE");
		List<ComboItem> collects = new List<ComboItem>();
		collects.Add(noneItem);

		// 默认选项
		object selectedItem = noneItem;

		// 创建所有注册的导出器元素
		for (int i = 0; i < ExportHandler.ExportTypes.Count; i++)
		{
			Type type = ExportHandler.ExportTypes[i];
			ExportAttribute attr = Attribute.GetCustomAttribute(type, typeof(ExportAttribute)) as ExportAttribute;	
			ComboItem item = new ComboItem(type, attr.ExportName);
			collects.Add(item);

			if (type == defaultExportType)
				selectedItem = item;	
		}

		cb.DisplayMember = nameof(noneItem.ExportName);
		cb.ValueMember = nameof(noneItem.ExportType);
		cb.DataSource = collects;
		cb.SelectedItem = selectedItem;
	}

	/// <summary>
	/// 导出路径辅助接口
	/// </summary>
	private string FindExportSavePath(string defaultPath)
	{
		selectDirDialog.SelectedPath = defaultPath;

		DialogResult result = selectDirDialog.ShowDialog();
		if (result == DialogResult.OK)
		{
			return selectDirDialog.SelectedPath;
		}

		return defaultPath;
	}


	#region 主按钮相关
	/// <summary>
	/// 选择文件按钮
	/// </summary>
	private void selectButton_Click(object sender, EventArgs e)
	{
		selectFileDialog.InitialDirectory = ExportConfig.Instance.LastOpenExcelPath;
		selectFileDialog.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";

		DialogResult result = this.selectFileDialog.ShowDialog();
		if (result == DialogResult.OK)
		{
			// 清空文件列表
			fileListBox.Items.Clear();

			// 记录最近一次打开的Excel文件目录
			int lastIndex = selectFileDialog.FileName.LastIndexOf("\\");
			ExportConfig.Instance.LastOpenExcelPath = selectFileDialog.FileName.Substring(0, lastIndex);

			for (int i = 0; i < selectFileDialog.FileNames.Length; i++)
			{
				string fileName = selectFileDialog.FileNames[i];
				fileListBox.Items.Add(fileName);
			}
		}
	}

	/// <summary>
	/// 导出生成按钮
	/// </summary>
	private void createButton_Click(object sender, EventArgs e)
	{
		// 清空多语言管理器的缓存数据
		LanguageMgr.Instance.ClearCacheLanguage();
		// 加载多语言总表
		LanguageMgr.Instance.LoadAutoGenerateLanguageToCache();

		// 加载选择的Excel文件列表
		for (int i = 0; i < fileListBox.Items.Count; i++)
		{
			string filePath = (string)fileListBox.Items[i];
			ExcelData excelFile = new ExcelData(filePath);
			if (excelFile.Load())
			{
				if (excelFile.Export())
				{
					// 导出成功后，我们解析表格的多语言数据
					var data = LanguageMgr.ParseLanguage(excelFile);
					LanguageMgr.Instance.CacheLanguage(data);
				}
			}
			excelFile.Dispose();
		}

		// 创建新的多语言总表文件
		LanguageMgr.Instance.CreateAutoGenerateLanguageFile();
		// 导出多语言总表文件
		LanguageMgr.Instance.ExportAutoGenerateLanguageFile();

		MessageBox.Show("导表完成.");
	}

	/// <summary>
	/// 多语言自动化按钮
	/// 自动处理所有表格里的多语言数据
	/// </summary>
	private void langAutoButton_Click(object sender, EventArgs e)
	{
		// 清空文件列表
		fileListBox.Items.Clear();

		// 获取文件
		string[] files = Directory.GetFiles(ExportConfig.Instance.LastOpenExcelPath, "*.xls");
		for (int i = 0; i < files.Length; i++)
		{
			string fileName = files[i];
			if (fileName.Contains("$"))
				continue;
			fileListBox.Items.Add(fileName);
		}

		// 清空多语言管理器的缓存数据
		LanguageMgr.Instance.ClearCacheLanguage();
		// 加载多语言总表
		LanguageMgr.Instance.LoadAutoGenerateLanguageToCache();

		// 加载所有的EXCEL文件列表
		for (int i = 0; i < fileListBox.Items.Count; i++)
		{
			string filePath = (string)fileListBox.Items[i];
			ExcelData excelFile = new ExcelData(filePath);
			if (excelFile.Load())
			{
				var data = LanguageMgr.ParseLanguage(excelFile);
				LanguageMgr.Instance.CacheLanguage(data);
			}
			excelFile.Dispose();
		}

		// 创建新的多语言总表文件
		LanguageMgr.Instance.CreateAutoGenerateLanguageFile();
		// 导出多语言总表文件
		LanguageMgr.Instance.ExportAutoGenerateLanguageFile();

		MessageBox.Show($"自动化完成.");
	}

	/// <summary>
	/// 设置按钮
	/// </summary>
	private void settingButton_Click(object sender, EventArgs e)
	{
		SettingForm winform = new SettingForm();
		winform.ShowDialog();
	}
	#endregion

	#region 客户端相关
	private void clientComboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		clientFindButton1.Enabled = item.ExportType != null;
		clientPathLabel1.Enabled = item.ExportType != null;
		ExportConfig.Instance.ClientExportInfos[1].ExporterType = item.ExportType;
	}
	private void clientComboBox2_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		clientFindButton2.Enabled = item.ExportType != null;
		clientPathLabel2.Enabled = item.ExportType != null;
		ExportConfig.Instance.ClientExportInfos[2].ExporterType = item.ExportType;
	}
	private void clientComboBox3_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		clientFindButton3.Enabled = item.ExportType != null;
		clientPathLabel3.Enabled = item.ExportType != null;
		ExportConfig.Instance.ClientExportInfos[3].ExporterType = item.ExportType;
	}
	private void clientComboBox4_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		clientFindButton4.Enabled = item.ExportType != null;
		clientPathLabel4.Enabled = item.ExportType != null;
		ExportConfig.Instance.ClientExportInfos[4].ExporterType = item.ExportType;
	}
	private void clientFindButton1_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ClientExportInfos[1];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		clientPathLabel1.Text = wrapper.ExportPath;
	}
	private void clientFindButton2_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ClientExportInfos[2];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		clientPathLabel2.Text = wrapper.ExportPath;
	}
	private void clientFindButton3_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ClientExportInfos[3];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		clientPathLabel3.Text = wrapper.ExportPath;
	}
	private void clientFindButton4_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ClientExportInfos[4];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		clientPathLabel4.Text = wrapper.ExportPath;
	}
	#endregion

	#region 服务器相关
	private void serverComboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		serverFindButton1.Enabled = item.ExportType != null;
		serverPathLabel1.Enabled = item.ExportType != null;
		ExportConfig.Instance.ServerExportInfos[1].ExporterType = item.ExportType;
	}
	private void serverComboBox2_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		serverFindButton2.Enabled = item.ExportType != null;
		serverPathLabel2.Enabled = item.ExportType != null;
		ExportConfig.Instance.ServerExportInfos[2].ExporterType = item.ExportType;
	}
	private void serverComboBox3_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		serverFindButton3.Enabled = item.ExportType != null;
		serverPathLabel3.Enabled = item.ExportType != null;
		ExportConfig.Instance.ServerExportInfos[3].ExporterType = item.ExportType;
	}
	private void serverComboBox4_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		serverFindButton4.Enabled = item.ExportType != null;
		serverPathLabel4.Enabled = item.ExportType != null;
		ExportConfig.Instance.ServerExportInfos[4].ExporterType = item.ExportType;
	}
	private void serverFindButton1_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ServerExportInfos[1];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		serverPathLabel1.Text = wrapper.ExportPath;
	}
	private void serverFindButton2_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ServerExportInfos[2];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		serverPathLabel2.Text = wrapper.ExportPath;
	}
	private void serverFindButton3_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ServerExportInfos[3];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		serverPathLabel3.Text = wrapper.ExportPath;
	}
	private void serverFindButton4_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.ServerExportInfos[4];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		serverPathLabel4.Text = wrapper.ExportPath;
	}
	#endregion

	#region 战服相关
	private void battleComboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		battleFindButton1.Enabled = item.ExportType != null;
		battlePathLabel1.Enabled = item.ExportType != null;
		ExportConfig.Instance.BattleExportInfos[1].ExporterType = item.ExportType;
	}
	private void battleComboBox2_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		battleFindButton2.Enabled = item.ExportType != null;
		battlePathLabel2.Enabled = item.ExportType != null;
		ExportConfig.Instance.BattleExportInfos[2].ExporterType = item.ExportType;
	}
	private void battleComboBox3_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		battleFindButton3.Enabled = item.ExportType != null;
		battlePathLabel3.Enabled = item.ExportType != null;
		ExportConfig.Instance.BattleExportInfos[3].ExporterType = item.ExportType;
	}
	private void battleComboBox4_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox cb = sender as ComboBox;
		ComboItem item = cb.SelectedItem as ComboItem;
		battleFindButton4.Enabled = item.ExportType != null;
		battlePathLabel4.Enabled = item.ExportType != null;
		ExportConfig.Instance.BattleExportInfos[4].ExporterType = item.ExportType;
	}
	private void battleFindButton1_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.BattleExportInfos[1];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		battlePathLabel1.Text = wrapper.ExportPath;
	}
	private void battleFindButton2_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.BattleExportInfos[2];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		battlePathLabel2.Text = wrapper.ExportPath;
	}
	private void battleFindButton3_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.BattleExportInfos[3];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		battlePathLabel3.Text = wrapper.ExportPath;
	}
	private void battleFindButton4_Click(object sender, EventArgs e)
	{
		ExportConfig.ExportWrapper wrapper = ExportConfig.Instance.BattleExportInfos[4];
		wrapper.ExportPath = FindExportSavePath(wrapper.ExportPath);
		battlePathLabel4.Text = wrapper.ExportPath;
	}
	#endregion
}