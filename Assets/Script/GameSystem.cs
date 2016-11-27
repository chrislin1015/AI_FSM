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
using System.Reflection;
using System;

public class GameSystem : MonoBehaviour 
{
    static protected GameSystem sInstance;
    static public GameSystem Instance
    {
        get { return sInstance; }
    }

    void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
            LoadXML();
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
        }
    }

    void Start() 
    {
        StateManager.Instance.Initial(BuildStateMachineAction);
        StateManager.Instance.BuildStateMachine();
    }

    void Update() 
    {
    }

    void LoadXML()
    {
        StateMachineDataCenter.Initial("xml/StateMachine");
        AIDataCenter.Initial("xml/AI");
    }

    Dictionary<string, StateMachine> BuildStateMachineAction()
    {
        Dictionary<string, StateMachine> _StateMachines = new Dictionary<string, StateMachine>();
        foreach(KeyValuePair<string, StateMachineDataCenter.StateMachineData> _KV in StateMachineDataCenter.Instance.DataMap)
        {
            StateMachine _StateMachine = new StateMachine(_KV.Value.ID);
            if (_StateMachine == null)
            {
                continue;
            }

            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State1ID));
            _StateMachine.AddState(_KV.Value.State2ID, StateManager.Instance.CrateState(_KV.Value.State2ID));
            _StateMachine.AddState(_KV.Value.State3ID, StateManager.Instance.CrateState(_KV.Value.State3ID));
            _StateMachine.AddState(_KV.Value.State4ID, StateManager.Instance.CrateState(_KV.Value.State4ID));
            _StateMachine.AddState(_KV.Value.State5ID, StateManager.Instance.CrateState(_KV.Value.State5ID));
            _StateMachine.AddState(_KV.Value.State6ID, StateManager.Instance.CrateState(_KV.Value.State6ID));
            _StateMachine.AddState(_KV.Value.State7ID, StateManager.Instance.CrateState(_KV.Value.State7ID));
            _StateMachine.AddState(_KV.Value.State8ID, StateManager.Instance.CrateState(_KV.Value.State8ID));
            _StateMachine.AddState(_KV.Value.State9ID, StateManager.Instance.CrateState(_KV.Value.State9ID));
            _StateMachine.AddState(_KV.Value.State10ID, StateManager.Instance.CrateState(_KV.Value.State10ID));

            _StateMachines.Add(_KV.Value.ID, _StateMachine);
        }

        return _StateMachines;
    }


}
