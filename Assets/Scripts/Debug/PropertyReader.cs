using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyReader
{
	public struct Variable
	{
		public string name;
		public Type type;
	}

	// For instances of class that inherit PropertyReader
	private Variable[] _fieldsCache;
	private Variable[] _propsCache;

	public Variable[] GetFields()
	{
		if(_fieldsCache == null)
		{
			_fieldsCache = GetFields(this.GetType());
		}
		return _fieldsCache;
	}

	public Variable[] GetProperties()
	{
		if(_propsCache == null)
		{
			_propsCache = GetProperties(this.GetType());
		}
		return _propsCache;
	}

	public object GetValue(string name)
	{
		return this.GetType().GetProperty(name).GetValue(this,null);
	}

	public void SetValue(string name, object value)
	{
		this.GetType().GetProperty(name).SetValue(this,value,null);
	}

	// Static Field and Property Getters
	public static Variable[] GetFields(Type type)
	{
		var fieldValues = type.GetFields();
		var result = new Variable[fieldValues.Length];
		
		for(int ii = 0; ii < fieldValues.Length; ii++)
		{
			result[ii].name = fieldValues[ii].Name;
			result[ii].type = fieldValues[ii].FieldType;
		}
		return result;
	}


	public static Variable[] GetProperties(Type type)
	{
		var propertyValues = type.GetProperties();
		var result = new Variable[propertyValues.Length];
		
		for(int ii = 0; ii < propertyValues.Length; ii++)
		{
			result[ii].name = propertyValues[ii].Name;
			result[ii].type = propertyValues[ii].PropertyType;
		}
		return result;
	}

	public static void SetField(Type type, string name, object value)
	{
		UnityEngine.Object[] objs = GameObject.FindObjectsOfType(type);
		
		foreach(UnityEngine.Object obj in objs)
		{
			Type objType = obj.GetType();
			if(objType != type) return;
			System.Reflection.FieldInfo info = objType.GetField(name);
			if(info == null) return;
			info.SetValue(obj,value);
		};
	}

	public static void SetProperty(Type type, string name, object value)
	{
		UnityEngine.Object[] objs = GameObject.FindObjectsOfType(type);
		foreach(UnityEngine.Object obj in objs)
		{
			Type objType = obj.GetType();
			if(objType != type) return;
			System.Reflection.PropertyInfo info = objType.GetProperty(name);
			if(info == null) return;
			info.SetValue(obj,value,null);
		};
	}
}
