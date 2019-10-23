using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public partial class SettingForm : Form
{
	public SettingForm()
	{
		InitializeComponent();
	}

	/// <summary>
	/// 当窗体加载完毕
	/// </summary>
	private void SettingForm_Load(object sender, EventArgs e)
	{
		// 初始化窗口控件
		InitWindowsForm();
	}

	/// <summary>
	/// 初始化主窗体
	/// </summary>
	private void InitWindowsForm()
	{
		completeCheckBox.Checked = SettingConfig.Instance.EnableAutoCompleteCell;
		completeTextBox.Text = SettingConfig.Instance.AutoCompleteCellContent;
		completeCheckBox_CheckedChanged(null, null);
	}

	private void completeCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		completeTextBox.Enabled = completeCheckBox.Checked;
		SettingConfig.Instance.EnableAutoCompleteCell = completeCheckBox.Checked;
	}

	private void completeTextBox_TextChanged(object sender, EventArgs e)
	{
		SettingConfig.Instance.AutoCompleteCellContent = completeTextBox.Text;
	}
}