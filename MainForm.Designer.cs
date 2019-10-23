
partial class MainForm
{
	/// <summary>
	/// 必需的设计器变量。
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// 清理所有正在使用的资源。
	/// </summary>
	/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows 窗体设计器生成的代码

	/// <summary>
	/// 设计器支持所需的方法 - 不要修改
	/// 使用代码编辑器修改此方法的内容。
	/// </summary>
	private void InitializeComponent()
	{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.selectButton = new System.Windows.Forms.Button();
			this.selectFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.selectDirDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.fileListBox = new System.Windows.Forms.ListBox();
			this.createButton = new System.Windows.Forms.Button();
			this.clientGroupBox = new System.Windows.Forms.GroupBox();
			this.clientPathLabel4 = new System.Windows.Forms.Label();
			this.clientPathLabel3 = new System.Windows.Forms.Label();
			this.clientFindButton4 = new System.Windows.Forms.Button();
			this.clientFindButton3 = new System.Windows.Forms.Button();
			this.clientComboBox4 = new System.Windows.Forms.ComboBox();
			this.clientComboBox3 = new System.Windows.Forms.ComboBox();
			this.clientComboBox2 = new System.Windows.Forms.ComboBox();
			this.clientComboBox1 = new System.Windows.Forms.ComboBox();
			this.clientFindButton2 = new System.Windows.Forms.Button();
			this.clientFindButton1 = new System.Windows.Forms.Button();
			this.clientPathLabel2 = new System.Windows.Forms.Label();
			this.clientPathLabel1 = new System.Windows.Forms.Label();
			this.serverGroupBox = new System.Windows.Forms.GroupBox();
			this.serverPathLabel4 = new System.Windows.Forms.Label();
			this.serverPathLabel3 = new System.Windows.Forms.Label();
			this.serverFindButton4 = new System.Windows.Forms.Button();
			this.serverFindButton3 = new System.Windows.Forms.Button();
			this.serverComboBox4 = new System.Windows.Forms.ComboBox();
			this.serverComboBox3 = new System.Windows.Forms.ComboBox();
			this.serverComboBox2 = new System.Windows.Forms.ComboBox();
			this.serverComboBox1 = new System.Windows.Forms.ComboBox();
			this.serverFindButton2 = new System.Windows.Forms.Button();
			this.serverFindButton1 = new System.Windows.Forms.Button();
			this.serverPathLabel2 = new System.Windows.Forms.Label();
			this.serverPathLabel1 = new System.Windows.Forms.Label();
			this.battleGroupBox = new System.Windows.Forms.GroupBox();
			this.battlePathLabel4 = new System.Windows.Forms.Label();
			this.battlePathLabel3 = new System.Windows.Forms.Label();
			this.battleFindButton4 = new System.Windows.Forms.Button();
			this.battleFindButton3 = new System.Windows.Forms.Button();
			this.battleComboBox4 = new System.Windows.Forms.ComboBox();
			this.battleComboBox3 = new System.Windows.Forms.ComboBox();
			this.battleComboBox2 = new System.Windows.Forms.ComboBox();
			this.battleComboBox1 = new System.Windows.Forms.ComboBox();
			this.battleFindButton2 = new System.Windows.Forms.Button();
			this.battleFindButton1 = new System.Windows.Forms.Button();
			this.battlePathLabel2 = new System.Windows.Forms.Label();
			this.battlePathLabel1 = new System.Windows.Forms.Label();
			this.langAutoButton = new System.Windows.Forms.Button();
			this.settingButton = new System.Windows.Forms.Button();
			this.clientGroupBox.SuspendLayout();
			this.serverGroupBox.SuspendLayout();
			this.battleGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// selectButton
			// 
			this.selectButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.selectButton.Location = new System.Drawing.Point(21, 12);
			this.selectButton.Name = "selectButton";
			this.selectButton.Size = new System.Drawing.Size(133, 41);
			this.selectButton.TabIndex = 0;
			this.selectButton.Text = "选择文件";
			this.selectButton.UseVisualStyleBackColor = true;
			this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
			// 
			// selectFileDialog
			// 
			this.selectFileDialog.Multiselect = true;
			// 
			// fileListBox
			// 
			this.fileListBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.fileListBox.FormattingEnabled = true;
			this.fileListBox.ItemHeight = 20;
			this.fileListBox.Location = new System.Drawing.Point(174, 12);
			this.fileListBox.Name = "fileListBox";
			this.fileListBox.Size = new System.Drawing.Size(598, 204);
			this.fileListBox.TabIndex = 4;
			// 
			// createButton
			// 
			this.createButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.createButton.Location = new System.Drawing.Point(21, 59);
			this.createButton.Name = "createButton";
			this.createButton.Size = new System.Drawing.Size(133, 43);
			this.createButton.TabIndex = 1;
			this.createButton.Text = "生成";
			this.createButton.UseVisualStyleBackColor = true;
			this.createButton.Click += new System.EventHandler(this.createButton_Click);
			// 
			// clientGroupBox
			// 
			this.clientGroupBox.Controls.Add(this.clientPathLabel4);
			this.clientGroupBox.Controls.Add(this.clientPathLabel3);
			this.clientGroupBox.Controls.Add(this.clientFindButton4);
			this.clientGroupBox.Controls.Add(this.clientFindButton3);
			this.clientGroupBox.Controls.Add(this.clientComboBox4);
			this.clientGroupBox.Controls.Add(this.clientComboBox3);
			this.clientGroupBox.Controls.Add(this.clientComboBox2);
			this.clientGroupBox.Controls.Add(this.clientComboBox1);
			this.clientGroupBox.Controls.Add(this.clientFindButton2);
			this.clientGroupBox.Controls.Add(this.clientFindButton1);
			this.clientGroupBox.Controls.Add(this.clientPathLabel2);
			this.clientGroupBox.Controls.Add(this.clientPathLabel1);
			this.clientGroupBox.Location = new System.Drawing.Point(21, 232);
			this.clientGroupBox.Name = "clientGroupBox";
			this.clientGroupBox.Size = new System.Drawing.Size(751, 159);
			this.clientGroupBox.TabIndex = 6;
			this.clientGroupBox.TabStop = false;
			this.clientGroupBox.Text = "客户端 (C)";
			// 
			// clientPathLabel4
			// 
			this.clientPathLabel4.AutoSize = true;
			this.clientPathLabel4.Location = new System.Drawing.Point(222, 126);
			this.clientPathLabel4.Name = "clientPathLabel4";
			this.clientPathLabel4.Size = new System.Drawing.Size(65, 20);
			this.clientPathLabel4.TabIndex = 19;
			this.clientPathLabel4.Text = "存储路径";
			// 
			// clientPathLabel3
			// 
			this.clientPathLabel3.AutoSize = true;
			this.clientPathLabel3.Location = new System.Drawing.Point(222, 93);
			this.clientPathLabel3.Name = "clientPathLabel3";
			this.clientPathLabel3.Size = new System.Drawing.Size(65, 20);
			this.clientPathLabel3.TabIndex = 18;
			this.clientPathLabel3.Text = "存储路径";
			// 
			// clientFindButton4
			// 
			this.clientFindButton4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clientFindButton4.Location = new System.Drawing.Point(175, 126);
			this.clientFindButton4.Name = "clientFindButton4";
			this.clientFindButton4.Size = new System.Drawing.Size(41, 23);
			this.clientFindButton4.TabIndex = 17;
			this.clientFindButton4.Text = "Find";
			this.clientFindButton4.UseVisualStyleBackColor = true;
			this.clientFindButton4.Click += new System.EventHandler(this.clientFindButton4_Click);
			// 
			// clientFindButton3
			// 
			this.clientFindButton3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clientFindButton3.Location = new System.Drawing.Point(175, 93);
			this.clientFindButton3.Name = "clientFindButton3";
			this.clientFindButton3.Size = new System.Drawing.Size(41, 23);
			this.clientFindButton3.TabIndex = 16;
			this.clientFindButton3.Text = "Find";
			this.clientFindButton3.UseVisualStyleBackColor = true;
			this.clientFindButton3.Click += new System.EventHandler(this.clientFindButton3_Click);
			// 
			// clientComboBox4
			// 
			this.clientComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.clientComboBox4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clientComboBox4.FormattingEnabled = true;
			this.clientComboBox4.Location = new System.Drawing.Point(9, 124);
			this.clientComboBox4.Name = "clientComboBox4";
			this.clientComboBox4.Size = new System.Drawing.Size(150, 27);
			this.clientComboBox4.TabIndex = 15;
			this.clientComboBox4.SelectedIndexChanged += new System.EventHandler(this.clientComboBox4_SelectedIndexChanged);
			// 
			// clientComboBox3
			// 
			this.clientComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.clientComboBox3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clientComboBox3.FormattingEnabled = true;
			this.clientComboBox3.Location = new System.Drawing.Point(9, 91);
			this.clientComboBox3.Name = "clientComboBox3";
			this.clientComboBox3.Size = new System.Drawing.Size(150, 27);
			this.clientComboBox3.TabIndex = 14;
			this.clientComboBox3.SelectedIndexChanged += new System.EventHandler(this.clientComboBox3_SelectedIndexChanged);
			// 
			// clientComboBox2
			// 
			this.clientComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.clientComboBox2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clientComboBox2.FormattingEnabled = true;
			this.clientComboBox2.Location = new System.Drawing.Point(9, 58);
			this.clientComboBox2.Name = "clientComboBox2";
			this.clientComboBox2.Size = new System.Drawing.Size(150, 27);
			this.clientComboBox2.TabIndex = 13;
			this.clientComboBox2.SelectedIndexChanged += new System.EventHandler(this.clientComboBox2_SelectedIndexChanged);
			// 
			// clientComboBox1
			// 
			this.clientComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.clientComboBox1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clientComboBox1.FormattingEnabled = true;
			this.clientComboBox1.Location = new System.Drawing.Point(9, 25);
			this.clientComboBox1.Name = "clientComboBox1";
			this.clientComboBox1.Size = new System.Drawing.Size(150, 27);
			this.clientComboBox1.TabIndex = 12;
			this.clientComboBox1.SelectedIndexChanged += new System.EventHandler(this.clientComboBox1_SelectedIndexChanged);
			// 
			// clientFindButton2
			// 
			this.clientFindButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clientFindButton2.Location = new System.Drawing.Point(175, 60);
			this.clientFindButton2.Name = "clientFindButton2";
			this.clientFindButton2.Size = new System.Drawing.Size(41, 23);
			this.clientFindButton2.TabIndex = 3;
			this.clientFindButton2.Text = "Find";
			this.clientFindButton2.UseVisualStyleBackColor = true;
			this.clientFindButton2.Click += new System.EventHandler(this.clientFindButton2_Click);
			// 
			// clientFindButton1
			// 
			this.clientFindButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clientFindButton1.Location = new System.Drawing.Point(175, 27);
			this.clientFindButton1.Name = "clientFindButton1";
			this.clientFindButton1.Size = new System.Drawing.Size(41, 23);
			this.clientFindButton1.TabIndex = 2;
			this.clientFindButton1.Text = "Find";
			this.clientFindButton1.UseVisualStyleBackColor = true;
			this.clientFindButton1.Click += new System.EventHandler(this.clientFindButton1_Click);
			// 
			// clientPathLabel2
			// 
			this.clientPathLabel2.AutoSize = true;
			this.clientPathLabel2.Location = new System.Drawing.Point(222, 61);
			this.clientPathLabel2.Name = "clientPathLabel2";
			this.clientPathLabel2.Size = new System.Drawing.Size(65, 20);
			this.clientPathLabel2.TabIndex = 1;
			this.clientPathLabel2.Text = "存储路径";
			// 
			// clientPathLabel1
			// 
			this.clientPathLabel1.AutoSize = true;
			this.clientPathLabel1.Location = new System.Drawing.Point(222, 29);
			this.clientPathLabel1.Name = "clientPathLabel1";
			this.clientPathLabel1.Size = new System.Drawing.Size(65, 20);
			this.clientPathLabel1.TabIndex = 0;
			this.clientPathLabel1.Text = "存储路径";
			// 
			// serverGroupBox
			// 
			this.serverGroupBox.Controls.Add(this.serverPathLabel4);
			this.serverGroupBox.Controls.Add(this.serverPathLabel3);
			this.serverGroupBox.Controls.Add(this.serverFindButton4);
			this.serverGroupBox.Controls.Add(this.serverFindButton3);
			this.serverGroupBox.Controls.Add(this.serverComboBox4);
			this.serverGroupBox.Controls.Add(this.serverComboBox3);
			this.serverGroupBox.Controls.Add(this.serverComboBox2);
			this.serverGroupBox.Controls.Add(this.serverComboBox1);
			this.serverGroupBox.Controls.Add(this.serverFindButton2);
			this.serverGroupBox.Controls.Add(this.serverFindButton1);
			this.serverGroupBox.Controls.Add(this.serverPathLabel2);
			this.serverGroupBox.Controls.Add(this.serverPathLabel1);
			this.serverGroupBox.Location = new System.Drawing.Point(21, 407);
			this.serverGroupBox.Name = "serverGroupBox";
			this.serverGroupBox.Size = new System.Drawing.Size(751, 166);
			this.serverGroupBox.TabIndex = 7;
			this.serverGroupBox.TabStop = false;
			this.serverGroupBox.Text = "服务器 (S)";
			// 
			// serverPathLabel4
			// 
			this.serverPathLabel4.AutoSize = true;
			this.serverPathLabel4.Location = new System.Drawing.Point(222, 124);
			this.serverPathLabel4.Name = "serverPathLabel4";
			this.serverPathLabel4.Size = new System.Drawing.Size(65, 20);
			this.serverPathLabel4.TabIndex = 27;
			this.serverPathLabel4.Text = "存储路径";
			// 
			// serverPathLabel3
			// 
			this.serverPathLabel3.AutoSize = true;
			this.serverPathLabel3.Location = new System.Drawing.Point(222, 93);
			this.serverPathLabel3.Name = "serverPathLabel3";
			this.serverPathLabel3.Size = new System.Drawing.Size(65, 20);
			this.serverPathLabel3.TabIndex = 26;
			this.serverPathLabel3.Text = "存储路径";
			// 
			// serverFindButton4
			// 
			this.serverFindButton4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.serverFindButton4.Location = new System.Drawing.Point(175, 124);
			this.serverFindButton4.Name = "serverFindButton4";
			this.serverFindButton4.Size = new System.Drawing.Size(41, 23);
			this.serverFindButton4.TabIndex = 25;
			this.serverFindButton4.Text = "Find";
			this.serverFindButton4.UseVisualStyleBackColor = true;
			this.serverFindButton4.Click += new System.EventHandler(this.serverFindButton4_Click);
			// 
			// serverFindButton3
			// 
			this.serverFindButton3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.serverFindButton3.Location = new System.Drawing.Point(175, 93);
			this.serverFindButton3.Name = "serverFindButton3";
			this.serverFindButton3.Size = new System.Drawing.Size(41, 23);
			this.serverFindButton3.TabIndex = 24;
			this.serverFindButton3.Text = "Find";
			this.serverFindButton3.UseVisualStyleBackColor = true;
			this.serverFindButton3.Click += new System.EventHandler(this.serverFindButton3_Click);
			// 
			// serverComboBox4
			// 
			this.serverComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.serverComboBox4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.serverComboBox4.FormattingEnabled = true;
			this.serverComboBox4.Location = new System.Drawing.Point(9, 124);
			this.serverComboBox4.Name = "serverComboBox4";
			this.serverComboBox4.Size = new System.Drawing.Size(150, 27);
			this.serverComboBox4.TabIndex = 23;
			this.serverComboBox4.SelectedIndexChanged += new System.EventHandler(this.serverComboBox4_SelectedIndexChanged);
			// 
			// serverComboBox3
			// 
			this.serverComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.serverComboBox3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.serverComboBox3.FormattingEnabled = true;
			this.serverComboBox3.Location = new System.Drawing.Point(9, 91);
			this.serverComboBox3.Name = "serverComboBox3";
			this.serverComboBox3.Size = new System.Drawing.Size(150, 27);
			this.serverComboBox3.TabIndex = 22;
			this.serverComboBox3.SelectedIndexChanged += new System.EventHandler(this.serverComboBox3_SelectedIndexChanged);
			// 
			// serverComboBox2
			// 
			this.serverComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.serverComboBox2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.serverComboBox2.FormattingEnabled = true;
			this.serverComboBox2.Location = new System.Drawing.Point(9, 58);
			this.serverComboBox2.Name = "serverComboBox2";
			this.serverComboBox2.Size = new System.Drawing.Size(150, 27);
			this.serverComboBox2.TabIndex = 21;
			this.serverComboBox2.SelectedIndexChanged += new System.EventHandler(this.serverComboBox2_SelectedIndexChanged);
			// 
			// serverComboBox1
			// 
			this.serverComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.serverComboBox1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.serverComboBox1.FormattingEnabled = true;
			this.serverComboBox1.Location = new System.Drawing.Point(9, 25);
			this.serverComboBox1.Name = "serverComboBox1";
			this.serverComboBox1.Size = new System.Drawing.Size(150, 27);
			this.serverComboBox1.TabIndex = 20;
			this.serverComboBox1.SelectedIndexChanged += new System.EventHandler(this.serverComboBox1_SelectedIndexChanged);
			// 
			// serverFindButton2
			// 
			this.serverFindButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.serverFindButton2.Location = new System.Drawing.Point(175, 60);
			this.serverFindButton2.Name = "serverFindButton2";
			this.serverFindButton2.Size = new System.Drawing.Size(41, 23);
			this.serverFindButton2.TabIndex = 5;
			this.serverFindButton2.Text = "Find";
			this.serverFindButton2.UseVisualStyleBackColor = true;
			this.serverFindButton2.Click += new System.EventHandler(this.serverFindButton2_Click);
			// 
			// serverFindButton1
			// 
			this.serverFindButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.serverFindButton1.Location = new System.Drawing.Point(175, 27);
			this.serverFindButton1.Name = "serverFindButton1";
			this.serverFindButton1.Size = new System.Drawing.Size(41, 23);
			this.serverFindButton1.TabIndex = 4;
			this.serverFindButton1.Text = "Find";
			this.serverFindButton1.UseVisualStyleBackColor = true;
			this.serverFindButton1.Click += new System.EventHandler(this.serverFindButton1_Click);
			// 
			// serverPathLabel2
			// 
			this.serverPathLabel2.AutoSize = true;
			this.serverPathLabel2.Location = new System.Drawing.Point(222, 60);
			this.serverPathLabel2.Name = "serverPathLabel2";
			this.serverPathLabel2.Size = new System.Drawing.Size(65, 20);
			this.serverPathLabel2.TabIndex = 1;
			this.serverPathLabel2.Text = "存储路径";
			// 
			// serverPathLabel1
			// 
			this.serverPathLabel1.AutoSize = true;
			this.serverPathLabel1.Location = new System.Drawing.Point(222, 27);
			this.serverPathLabel1.Name = "serverPathLabel1";
			this.serverPathLabel1.Size = new System.Drawing.Size(65, 20);
			this.serverPathLabel1.TabIndex = 0;
			this.serverPathLabel1.Text = "存储路径";
			// 
			// battleGroupBox
			// 
			this.battleGroupBox.Controls.Add(this.battlePathLabel4);
			this.battleGroupBox.Controls.Add(this.battlePathLabel3);
			this.battleGroupBox.Controls.Add(this.battleFindButton4);
			this.battleGroupBox.Controls.Add(this.battleFindButton3);
			this.battleGroupBox.Controls.Add(this.battleComboBox4);
			this.battleGroupBox.Controls.Add(this.battleComboBox3);
			this.battleGroupBox.Controls.Add(this.battleComboBox2);
			this.battleGroupBox.Controls.Add(this.battleComboBox1);
			this.battleGroupBox.Controls.Add(this.battleFindButton2);
			this.battleGroupBox.Controls.Add(this.battleFindButton1);
			this.battleGroupBox.Controls.Add(this.battlePathLabel2);
			this.battleGroupBox.Controls.Add(this.battlePathLabel1);
			this.battleGroupBox.Location = new System.Drawing.Point(21, 588);
			this.battleGroupBox.Name = "battleGroupBox";
			this.battleGroupBox.Size = new System.Drawing.Size(751, 162);
			this.battleGroupBox.TabIndex = 8;
			this.battleGroupBox.TabStop = false;
			this.battleGroupBox.Text = "战服 (B)";
			// 
			// battlePathLabel4
			// 
			this.battlePathLabel4.AutoSize = true;
			this.battlePathLabel4.Location = new System.Drawing.Point(222, 126);
			this.battlePathLabel4.Name = "battlePathLabel4";
			this.battlePathLabel4.Size = new System.Drawing.Size(65, 20);
			this.battlePathLabel4.TabIndex = 35;
			this.battlePathLabel4.Text = "存储路径";
			// 
			// battlePathLabel3
			// 
			this.battlePathLabel3.AutoSize = true;
			this.battlePathLabel3.Location = new System.Drawing.Point(222, 93);
			this.battlePathLabel3.Name = "battlePathLabel3";
			this.battlePathLabel3.Size = new System.Drawing.Size(65, 20);
			this.battlePathLabel3.TabIndex = 34;
			this.battlePathLabel3.Text = "存储路径";
			// 
			// battleFindButton4
			// 
			this.battleFindButton4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.battleFindButton4.Location = new System.Drawing.Point(175, 126);
			this.battleFindButton4.Name = "battleFindButton4";
			this.battleFindButton4.Size = new System.Drawing.Size(41, 23);
			this.battleFindButton4.TabIndex = 33;
			this.battleFindButton4.Text = "Find";
			this.battleFindButton4.UseVisualStyleBackColor = true;
			this.battleFindButton4.Click += new System.EventHandler(this.battleFindButton4_Click);
			// 
			// battleFindButton3
			// 
			this.battleFindButton3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.battleFindButton3.Location = new System.Drawing.Point(175, 93);
			this.battleFindButton3.Name = "battleFindButton3";
			this.battleFindButton3.Size = new System.Drawing.Size(41, 23);
			this.battleFindButton3.TabIndex = 32;
			this.battleFindButton3.Text = "Find";
			this.battleFindButton3.UseVisualStyleBackColor = true;
			this.battleFindButton3.Click += new System.EventHandler(this.battleFindButton3_Click);
			// 
			// battleComboBox4
			// 
			this.battleComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.battleComboBox4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.battleComboBox4.FormattingEnabled = true;
			this.battleComboBox4.Location = new System.Drawing.Point(9, 124);
			this.battleComboBox4.Name = "battleComboBox4";
			this.battleComboBox4.Size = new System.Drawing.Size(150, 27);
			this.battleComboBox4.TabIndex = 31;
			this.battleComboBox4.SelectedIndexChanged += new System.EventHandler(this.battleComboBox4_SelectedIndexChanged);
			// 
			// battleComboBox3
			// 
			this.battleComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.battleComboBox3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.battleComboBox3.FormattingEnabled = true;
			this.battleComboBox3.Location = new System.Drawing.Point(9, 91);
			this.battleComboBox3.Name = "battleComboBox3";
			this.battleComboBox3.Size = new System.Drawing.Size(150, 27);
			this.battleComboBox3.TabIndex = 30;
			this.battleComboBox3.SelectedIndexChanged += new System.EventHandler(this.battleComboBox3_SelectedIndexChanged);
			// 
			// battleComboBox2
			// 
			this.battleComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.battleComboBox2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.battleComboBox2.FormattingEnabled = true;
			this.battleComboBox2.Location = new System.Drawing.Point(9, 58);
			this.battleComboBox2.Name = "battleComboBox2";
			this.battleComboBox2.Size = new System.Drawing.Size(150, 27);
			this.battleComboBox2.TabIndex = 29;
			this.battleComboBox2.SelectedIndexChanged += new System.EventHandler(this.battleComboBox2_SelectedIndexChanged);
			// 
			// battleComboBox1
			// 
			this.battleComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.battleComboBox1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.battleComboBox1.FormattingEnabled = true;
			this.battleComboBox1.Location = new System.Drawing.Point(9, 25);
			this.battleComboBox1.Name = "battleComboBox1";
			this.battleComboBox1.Size = new System.Drawing.Size(150, 27);
			this.battleComboBox1.TabIndex = 28;
			this.battleComboBox1.SelectedIndexChanged += new System.EventHandler(this.battleComboBox1_SelectedIndexChanged);
			// 
			// battleFindButton2
			// 
			this.battleFindButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.battleFindButton2.Location = new System.Drawing.Point(175, 60);
			this.battleFindButton2.Name = "battleFindButton2";
			this.battleFindButton2.Size = new System.Drawing.Size(41, 23);
			this.battleFindButton2.TabIndex = 6;
			this.battleFindButton2.Text = "Find";
			this.battleFindButton2.UseVisualStyleBackColor = true;
			this.battleFindButton2.Click += new System.EventHandler(this.battleFindButton2_Click);
			// 
			// battleFindButton1
			// 
			this.battleFindButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.battleFindButton1.Location = new System.Drawing.Point(175, 27);
			this.battleFindButton1.Name = "battleFindButton1";
			this.battleFindButton1.Size = new System.Drawing.Size(41, 23);
			this.battleFindButton1.TabIndex = 5;
			this.battleFindButton1.Text = "Find";
			this.battleFindButton1.UseVisualStyleBackColor = true;
			this.battleFindButton1.Click += new System.EventHandler(this.battleFindButton1_Click);
			// 
			// battlePathLabel2
			// 
			this.battlePathLabel2.AutoSize = true;
			this.battlePathLabel2.Location = new System.Drawing.Point(222, 60);
			this.battlePathLabel2.Name = "battlePathLabel2";
			this.battlePathLabel2.Size = new System.Drawing.Size(65, 20);
			this.battlePathLabel2.TabIndex = 1;
			this.battlePathLabel2.Text = "存储路径";
			// 
			// battlePathLabel1
			// 
			this.battlePathLabel1.AutoSize = true;
			this.battlePathLabel1.Location = new System.Drawing.Point(222, 27);
			this.battlePathLabel1.Name = "battlePathLabel1";
			this.battlePathLabel1.Size = new System.Drawing.Size(65, 20);
			this.battlePathLabel1.TabIndex = 0;
			this.battlePathLabel1.Text = "存储路径";
			// 
			// langAutoButton
			// 
			this.langAutoButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.langAutoButton.Location = new System.Drawing.Point(21, 131);
			this.langAutoButton.Name = "langAutoButton";
			this.langAutoButton.Size = new System.Drawing.Size(133, 41);
			this.langAutoButton.TabIndex = 9;
			this.langAutoButton.Text = "多语言 自动化";
			this.langAutoButton.UseVisualStyleBackColor = true;
			this.langAutoButton.Click += new System.EventHandler(this.langAutoButton_Click);
			// 
			// settingButton
			// 
			this.settingButton.Location = new System.Drawing.Point(58, 187);
			this.settingButton.Name = "settingButton";
			this.settingButton.Size = new System.Drawing.Size(56, 29);
			this.settingButton.TabIndex = 10;
			this.settingButton.Text = "设置";
			this.settingButton.UseVisualStyleBackColor = true;
			this.settingButton.Click += new System.EventHandler(this.settingButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(784, 762);
			this.Controls.Add(this.settingButton);
			this.Controls.Add(this.langAutoButton);
			this.Controls.Add(this.battleGroupBox);
			this.Controls.Add(this.serverGroupBox);
			this.Controls.Add(this.clientGroupBox);
			this.Controls.Add(this.createButton);
			this.Controls.Add(this.fileListBox);
			this.Controls.Add(this.selectButton);
			this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "闪电导表工具";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.clientGroupBox.ResumeLayout(false);
			this.clientGroupBox.PerformLayout();
			this.serverGroupBox.ResumeLayout(false);
			this.serverGroupBox.PerformLayout();
			this.battleGroupBox.ResumeLayout(false);
			this.battleGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion
	
	private System.Windows.Forms.Button selectButton;
	private System.Windows.Forms.OpenFileDialog selectFileDialog;
	private System.Windows.Forms.FolderBrowserDialog selectDirDialog;
	private System.Windows.Forms.ListBox fileListBox;
	private System.Windows.Forms.Button createButton;
	private System.Windows.Forms.GroupBox clientGroupBox;
	private System.Windows.Forms.Label clientPathLabel2;
	private System.Windows.Forms.Label clientPathLabel1;
	private System.Windows.Forms.GroupBox serverGroupBox;
	private System.Windows.Forms.Label serverPathLabel2;
	private System.Windows.Forms.Label serverPathLabel1;
	private System.Windows.Forms.GroupBox battleGroupBox;
	private System.Windows.Forms.Label battlePathLabel2;
	private System.Windows.Forms.Label battlePathLabel1;
	private System.Windows.Forms.Button clientFindButton2;
	private System.Windows.Forms.Button clientFindButton1;
	private System.Windows.Forms.Button serverFindButton2;
	private System.Windows.Forms.Button serverFindButton1;
	private System.Windows.Forms.Button battleFindButton2;
	private System.Windows.Forms.Button battleFindButton1;
	private System.Windows.Forms.Button langAutoButton;
	private System.Windows.Forms.ComboBox clientComboBox1;
	private System.Windows.Forms.ComboBox clientComboBox4;
	private System.Windows.Forms.ComboBox clientComboBox3;
	private System.Windows.Forms.ComboBox clientComboBox2;
	private System.Windows.Forms.Label clientPathLabel4;
	private System.Windows.Forms.Label clientPathLabel3;
	private System.Windows.Forms.Button clientFindButton4;
	private System.Windows.Forms.Button clientFindButton3;
	private System.Windows.Forms.Label serverPathLabel4;
	private System.Windows.Forms.Label serverPathLabel3;
	private System.Windows.Forms.Button serverFindButton4;
	private System.Windows.Forms.Button serverFindButton3;
	private System.Windows.Forms.ComboBox serverComboBox4;
	private System.Windows.Forms.ComboBox serverComboBox3;
	private System.Windows.Forms.ComboBox serverComboBox2;
	private System.Windows.Forms.ComboBox serverComboBox1;
	private System.Windows.Forms.ComboBox battleComboBox4;
	private System.Windows.Forms.ComboBox battleComboBox3;
	private System.Windows.Forms.ComboBox battleComboBox2;
	private System.Windows.Forms.ComboBox battleComboBox1;
	private System.Windows.Forms.Label battlePathLabel4;
	private System.Windows.Forms.Label battlePathLabel3;
	private System.Windows.Forms.Button battleFindButton4;
	private System.Windows.Forms.Button battleFindButton3;
	private System.Windows.Forms.Button settingButton;
}