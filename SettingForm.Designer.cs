
partial class SettingForm
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			this.completeCheckBox = new System.Windows.Forms.CheckBox();
			this.completeTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// completeCheckBox
			// 
			this.completeCheckBox.AutoSize = true;
			this.completeCheckBox.Location = new System.Drawing.Point(18, 20);
			this.completeCheckBox.Name = "completeCheckBox";
			this.completeCheckBox.Size = new System.Drawing.Size(156, 16);
			this.completeCheckBox.TabIndex = 0;
			this.completeCheckBox.Text = "开启数值单元格自动补全";
			this.completeCheckBox.UseVisualStyleBackColor = true;
			this.completeCheckBox.CheckedChanged += new System.EventHandler(this.completeCheckBox_CheckedChanged);
			// 
			// completeTextBox
			// 
			this.completeTextBox.Location = new System.Drawing.Point(180, 18);
			this.completeTextBox.Name = "completeTextBox";
			this.completeTextBox.Size = new System.Drawing.Size(129, 21);
			this.completeTextBox.TabIndex = 1;
			this.completeTextBox.Text = "0";
			this.completeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.completeTextBox.TextChanged += new System.EventHandler(this.completeTextBox_TextChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.completeCheckBox);
			this.groupBox1.Controls.Add(this.completeTextBox);
			this.groupBox1.Location = new System.Drawing.Point(21, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(656, 86);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "单元格";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.label1.Location = new System.Drawing.Point(16, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(245, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "提示：数值单元格为空，工具在导出时会报错";
			// 
			// SettingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(702, 350);
			this.Controls.Add(this.groupBox1);
			this.Name = "SettingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "设置";
			this.Load += new System.EventHandler(this.SettingForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.CheckBox completeCheckBox;
	private System.Windows.Forms.TextBox completeTextBox;
	private System.Windows.Forms.GroupBox groupBox1;
	private System.Windows.Forms.Label label1;
}