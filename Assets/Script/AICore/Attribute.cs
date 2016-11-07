﻿/*
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

/*
 * 
 * @author Chris
 * @date 2016.11.01
 */

public class Attribute : object
{
    protected string AttributeID;
    protected System.Type Type;

    public Attribute(string iID)
    {
        AttributeID = iID;
    }

    ~Attribute()
    {
    }

    public string GetID()
    {
        return AttributeID;
    }

    public System.Type GetAttType()
    {
        return Type;
    }
}

public class AttTemplate<T> : Attribute
{
    protected T MaxValue;
    public T Max
    {
        get { return MaxValue; }
    }

    protected T MinValue;
    public T Min
    {
        get { return MinValue; }
    }

    protected T CurrentValue;
    public T Current
    {
        get { return CurrentValue; }
        set { CurrentValue = value; }
    }

    public AttTemplate(string iID, T iMax, T iMin) : base(iID)
    {
        MaxValue = CurrentValue = iMax;
        MinValue = iMin;
        Type = this.GetType();
    }

    ~AttTemplate()
    {
    }
}