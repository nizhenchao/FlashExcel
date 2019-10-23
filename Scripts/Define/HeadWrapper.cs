//**************************************************
// Copyright©2018-2019 何冠峰
// Licensed under the MIT license
//**************************************************

public class HeadWrapper
{
	/// <summary>
	/// 列号
	/// </summary>
	public int CellNum { get; }

	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// 类型
	/// </summary>
	public string Type { get; }

	/// <summary>
	/// 导出标记
	/// </summary>
	public string Logo { get; }

	/// <summary>
	/// 策划注释列
	/// </summary>
	public bool IsNotes { get; }

	public HeadWrapper(int cellNum, string name, string type, string logo)
	{
		CellNum = cellNum;
		Name = name;
		Type = type;
		Logo = logo;

		// 检测是否为策划注释列
		IsNotes = name.Contains(ConstDefine.StrNotesRow);
	}
}