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

    // Use this for initialization
    void Start() 
    {
        StateManager.Instance.Initial(BuildStateMachineAction);
        StateManager.Instance.BuildStateMachine();
    }
	
    // Update is called once per frame
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
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State2ID));
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State3ID));
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State4ID));
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State5ID));
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State6ID));
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State7ID));
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State8ID));
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State9ID));
            _StateMachine.AddState(_KV.Value.State1ID, StateManager.Instance.CrateState(_KV.Value.State10ID));

            _StateMachines.Add(_KV.Value.ID, _StateMachine);
        }

        return _StateMachines;
    }


}
