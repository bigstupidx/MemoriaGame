//
// SignalAttribute.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;


public class SignalAttribute : System.Attribute
{
	public string name;
	
	public SignalAttribute()
	{
		
	}
	public SignalAttribute(string name)
	{
		this.name = name;
	}
}

