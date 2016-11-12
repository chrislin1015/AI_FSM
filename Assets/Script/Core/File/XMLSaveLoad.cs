/*
 * MIT License
 * 
 * Copyright (c) [2016] [Chris Lin]
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System;

public static class XMLSaveLoad<T> 
{	
	public static T LoadXml(string iFilePath)
	{
#if !UNITY_WEBPLAYER
        FileInfo _FI = new FileInfo(iFilePath);
        if (_FI == null || !_FI.Exists)
			return default(T);
		
        StreamReader _Reader = _FI.OpenText();
        string _XMLData = _Reader.ReadToEnd();
        _Reader.Close();
		
        return (T)DeserializeObject(_XMLData);
#else
		return default(T);
#endif
	}

    public static T LoadXMLBuffer(string iBuffer)
    {
        return (T)DeserializeObject(iBuffer);
    }
	
    public static T LoadXmlFromResources(string iFilePath)
	{
        TextAsset _Text = (TextAsset)Resources.Load(iFilePath);
        if (_Text == null)
		{
			return default(T);
		}

		XmlDocument _XML = new XmlDocument();
        _XML.LoadXml(_Text.text);
        Resources.UnloadAsset(_Text);
		
        return (T)DeserializeObject(_XML.InnerXml);
	}
	
	public static void SaveXML(string iFilePath, T iObject)
	{
#if !UNITY_WEBPLAYER
        string _XMLData = SerializeObject(iObject); 
		StreamWriter _Writer;
		
		FileInfo _FI = new FileInfo(iFilePath);
        if (!_FI.Exists)
        {
            _Writer = _FI.CreateText();
        } 
        else
        {
            _FI.Delete();
            _Writer = _FI.CreateText();
        }
        _Writer.Write(_XMLData);
        _Writer.Close();

        Debug.Log("XML File Written : " + _FI.FullName);
#endif
	}
	
	static public string SerializeObject(object iObj)
	{
		string _XMLString = null;
		MemoryStream _MemoryStream = new MemoryStream();
		XmlSerializer _XS = new XmlSerializer(typeof(T));
        XmlTextWriter _XMLTextWriter = new XmlTextWriter(_MemoryStream, Encoding.UTF8);
        _XMLTextWriter.Formatting = Formatting.Indented;
        _XS.Serialize(_XMLTextWriter, iObj);
        _MemoryStream = (MemoryStream)_XMLTextWriter.BaseStream;

        _XMLString = UTF8ByteArrayToString(_MemoryStream.ToArray());
		
        return _XMLString;
	}
	
	static public object DeserializeObject(string iXmlizedstring)
	{
		XmlSerializer _XS = new XmlSerializer(typeof(T));
		
		MemoryStream _Memorystream = null;
        _Memorystream = new MemoryStream(StringToUTF8ByteArray(iXmlizedstring));
		
        return _XS.Deserialize(_Memorystream);
	}
	
    static public string UTF8ByteArrayToString(byte[] iBytes)
	{
		UTF8Encoding _Encoding = new UTF8Encoding();
        string _String = _Encoding.GetString(iBytes);
        return _String;
	}
	
	static public byte[] StringToUTF8ByteArray(string iXmlstring)
	{
		UTF8Encoding _Encoding = new UTF8Encoding();
        byte[] _Bytes = _Encoding.GetBytes(iXmlstring);
		return _Bytes;
	}
}
