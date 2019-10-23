//**************************************************
// Copyright©2018-2019 何冠峰
// Licensed under the MIT license
//**************************************************
using System;
using System.Collections.Generic;
using System.Linq;

[AttributeUsage(AttributeTargets.Class)]
public class ExportAttribute : Attribute
{
	public string ExportName { private set; get; }

	public ExportAttribute(string name)
	{
		ExportName = name;
	}
}