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

public class StateMachine : object 
{
    protected string ID;
    //
    protected Dictionary<string, State> States = new Dictionary<string, State>();
    //
    protected State CurrentState = null;
    //
    protected AI MyAI = null;

    public StateMachine(string iID)
    {
        ID = iID;
    }

    ~StateMachine()
    {
        if (States != null)
        {
            States.Clear();
            States = null;
        }

        CurrentState = null;
        MyAI = null;
    }

    public void BindAI(AI iAI)
    {
        MyAI = iAI;
    }

    public void AddState(string iID, State iState)
    {
        if (iState == null)
            return;

        if (States.ContainsKey(iID))
        {
            Debug.LogWarning("has contain this state : " + iID);
            return;
        }

        States.Add(iID, iState);
    }

    public void RemoveState(string iID)
    {
        if (States.ContainsKey(iID))
        {
            States.Remove(iID);
        }
    }

    public void ChangeState(string iID)
    {
        if (MyAI == null)
            return;
        
        if (States.ContainsKey(iID) == false)
            return;

        if (CurrentState != null)
        {
            CurrentState.PostUpdate(MyAI);
        }

        CurrentState = States[iID];
        CurrentState.PreUpdate(MyAI);
    }

    public void UpdateState()
    {
        if (CurrentState == null || MyAI == null)
            return;

        CurrentState.Update(MyAI);
    }

    public StateMachine Clone()
    {
        StateMachine _New = new StateMachine(ID);
        if (_New == null)
            return null;

        foreach(KeyValuePair<string, State> _KV in States)
        {
            _New.AddState(_KV.Key, _KV.Value);
        }

        return _New;
    }
}
