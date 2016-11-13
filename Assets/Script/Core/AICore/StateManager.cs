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
using System;
using System.Collections.Generic;

/*
 * 
 * @author Chris
 * @date 2014.06.01
 */



public class StateManager : MonoBehaviour 
{
    static protected StateManager sInstance;
    static public StateManager Instance
    {
        get { return sInstance; }
    }
    public delegate Dictionary<string, StateMachine> BuildStateMachineDelegate();

    BuildStateMachineDelegate BuildStateMachineAction;
    protected Dictionary<string, StateMachine> StateMachines = new Dictionary<string, StateMachine>();
    protected Dictionary<string, State> States = new Dictionary<string, State>();

    void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnDestroy()
    {
        if (sInstance == this)
        {
            sInstance = null;

            StateMachines.Clear();
            StateMachines = null;

            States.Clear();
            States = null;
        }
    }

    public void Initial(BuildStateMachineDelegate iDelegate)
    {
        BuildStateMachineAction = iDelegate;
    }

    public void BuildStateMachine()
    {
        if (BuildStateMachineAction != null)
        {
            StateMachines = BuildStateMachineAction();
        }
    }

    public StateMachine GetStateMachine(string iID)
    {
        if (StateMachines.ContainsKey(iID) == false)
        {
            return null;
        }

        return StateMachines[iID].Clone();
    }

    public State CrateState(string iStateClassName)
    {
        if (States.ContainsKey(iStateClassName))
            return States[iStateClassName];
        
        System.Type _Type = System.Type.GetType(iStateClassName);
        if (_Type == null)
            return null;

        State _State = (State)Activator.CreateInstance(_Type);
        if (_State == null)
        {
            Debug.LogError("not have this state type : " + iStateClassName);
        }
        else
        {
            States.Add(iStateClassName, _State);
        }

        return _State;
    }
}