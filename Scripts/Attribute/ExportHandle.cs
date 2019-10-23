//**************************************************
// Copyright©2018-2019 何冠峰
// Licensed under the MIT license
//**************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExportHandler
{
	public static List<Type> ExportTypes = new List<Type>();

	static ExportHandler()
	{
		Type[] types = typeof(BaseExporter).Assembly.GetTypes();
		for (int i = 0; i < types.Length; i++)
		{
			Type type = types[i];
			if (Attribute.IsDefined(type, typeof(ExportAttribute)))
			{
				// 判断继承关系
				if (!typeof(BaseExporter).IsAssignableFrom(type))
					throw new Exception($"class {type} does not inherit from BaseExporter.");

				// 添加到列表
				ExportTypes.Add(type);
			}
		}
	}
}