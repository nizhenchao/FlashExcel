
public abstract class BaseExporter
{
	protected readonly SheetData _sheet;

	public BaseExporter(SheetData sheetData)
	{
		_sheet = sheetData;
	}
	
	/// <summary>
	/// 导出文件
	/// </summary>
	public abstract void ExportFile(string path, string createLogo);

	/// <summary>
	/// 页签里是否包含导出符号
	/// </summary>
	/// <param name="createLogo">导出符号</param>
	/// <returns></returns>
	public bool IsContainsLogo(string createLogo)
	{
		return _sheet.IsContainsLogo(createLogo);		
	}
}