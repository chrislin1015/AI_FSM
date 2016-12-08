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
using System.Collections.Generic;

public class XMLLoader<T> : object
{
    protected List<T> m_DataList;
    public List<T> DataList
    {
        get { return m_DataList; }
    }
    protected bool m_IsInitial = false;
    
    public XMLLoader()
    {
    }
    
    ~XMLLoader()
    {
        if (m_DataList != null)
        {
            m_DataList.Clear();
            m_DataList = null;
        }
    }
    
    public void Initial(string iFilePath, bool iIsContent = false)
    {
        if (m_DataList != null)
        {
            m_DataList.Clear();
            m_DataList = null;
        }

        if (iIsContent)
        {
            m_DataList = XMLSaveLoad<List<T>>.LoadXMLBuffer(iFilePath);
        }
        else
        {
            m_DataList = XMLSaveLoad<List<T>>.LoadXmlFromResources(iFilePath);
        }
        m_IsInitial = true;
    }

    public bool IsInitial()
    {
        return m_IsInitial;
    }
    
    public T GetData(int iIndex)
    {
        if (m_IsInitial == false)
            return default(T);

        if (m_DataList == null)
            return default(T);
        
        if (iIndex < 0 || iIndex >= m_DataList.Count)
            return default(T);
        
        T _Data = m_DataList[iIndex];
        
        return _Data;
    }
}
