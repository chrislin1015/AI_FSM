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

public class AI : MonoBehaviour 
{
    public Animation Ani; 
    public string MyID;
    protected StateMachine MyStateMachine = null;
    protected Dictionary<string, Attribute> Attributes = new Dictionary<string, Attribute>();

    void OnDestroy()
    {
        MyStateMachine = null;
        Attributes.Clear();
        Attributes = null;
    }

    void Update()
    {
        if (MyStateMachine == null)
            return;

        MyStateMachine.UpdateState();
    }

    public void ChangeState(string iID)
    {
        if (MyStateMachine == null)
            return;

        PlayAnimaiton(iID);
        MyStateMachine.ChangeState(iID);
    }

    virtual public void PlayAnimaiton(string iID)
    {
        if (Ani == null)
            return;

        Ani.Play(iID);
    }

    public Attribute GetAttribute(string iID)
    {
        if (Attributes.ContainsKey(iID) == false)
            return null;

        return Attributes[iID];
    }
}
