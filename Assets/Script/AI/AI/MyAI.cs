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
using System.Collections.Generic;

public class MyAI : AI 
{
    protected AIDataCenter.AIData AIData;
    public AIDataCenter.AIData Data
    {
        get { return AIData; }
    }

    [HideInInspector]
    public MyAI TargetAI;

    public List<MyAI> ObserverList = new List<MyAI>(); 

	// Use this for initialization
	void Awake() 
    {
        AIData = AIDataCenter.Instance.GetData(MyID);
        MyStateMachine = StateManager.Instance.GetStateMachine(AIData.StateMachineID);
        if (MyStateMachine != null)
        {
            MyStateMachine.BindAI(this);
        }
        CreateAttribute();
	}

    void OnDestroy()
    {
        ObserverList.Clear();
        ObserverList = null;
    }

    void Update()
    {
        if (MyStateMachine == null)
            return;

        MyStateMachine.UpdateState();
    }

    void CreateAttribute()
    {
        AttTemplate<int> _HPAttribute = new AttTemplate<int>(GlobalEnum.ATTRIBUTE_TYPE.HP.ToString(), AIData.MaxHP, AIData.MinHP);
        Attributes.Add(_HPAttribute.GetID(), _HPAttribute);

        //Attribute _A = GetAttribute(_HPAttribute.GetID());
        //AttTemplate<int> _AT = (AttTemplate<int>)_A;

        AttTemplate<float> _ATKRange = new AttTemplate<float>(GlobalEnum.ATTRIBUTE_TYPE.ATK_RANGE.ToString(), AIData.AtkRange, AIData.AtkRange);
        Attributes.Add(_ATKRange.GetID(), _ATKRange);

        AttTemplate<float> _ATKSpeed = new AttTemplate<float>(GlobalEnum.ATTRIBUTE_TYPE.ATK_SPEED.ToString(), AIData.AtkSpeed, AIData.AtkSpeed);
        Attributes.Add(_ATKSpeed.GetID(), _ATKSpeed);

        AttTemplate<int> _Damage = new AttTemplate<int>(GlobalEnum.ATTRIBUTE_TYPE.DAMAGE.ToString(), AIData.Damage, AIData.Damage);
        Attributes.Add(_Damage.GetID(), _Damage);

        AttTemplate<GlobalEnum.MILITARY_TYPE> _Military = new AttTemplate<GlobalEnum.MILITARY_TYPE>(GlobalEnum.ATTRIBUTE_TYPE.MILITARY.ToString(), (GlobalEnum.MILITARY_TYPE)AIData.Military, (GlobalEnum.MILITARY_TYPE)AIData.Military);
        Attributes.Add(_Military.GetID(), _Military);

        AttTemplate<GlobalEnum.MILITARY_TYPE> _AtkType = new AttTemplate<GlobalEnum.MILITARY_TYPE>(GlobalEnum.ATTRIBUTE_TYPE.ATK_TYPE.ToString(), (GlobalEnum.MILITARY_TYPE)AIData.AtkType, (GlobalEnum.MILITARY_TYPE)AIData.AtkType);
        Attributes.Add(_AtkType.GetID(), _AtkType);

        AttTemplate<float> _MoveSpeed = new AttTemplate<float>(GlobalEnum.ATTRIBUTE_TYPE.MOVE_SPEED.ToString(), AIData.MoveSpeed, AIData.MoveSpeed);
        Attributes.Add(_MoveSpeed.GetID(), _MoveSpeed);
    }

    public void SetObserver(MyAI iObserver)
    {
        if (iObserver == null)
            return;

        ObserverList.Add(iObserver);
    }

    public void Damage(int iDamage)
    {
        AttTemplate<int> _HP = (AttTemplate<int>)GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.HP.ToString());
        if (_HP == null)
            return;

        _HP.Current = Mathf.Max(0, _HP.Current - iDamage);
        if (_HP.Current <= 0)
        {
            foreach (MyAI _Observer in ObserverList)
            {
                _Observer.TargetAI = null;
            }

            ChangeState("DeathState");
        }
    }

    public void SearchTarget()
    {
        float _Range = float.MaxValue;
        MyAI _AI = null;

        AttTemplate<int> _AtkType = (AttTemplate<int>)GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.ATK_TYPE.ToString());
        if (_AtkType == null)
            return;

        GlobalEnum.MILITARY_TYPE _TargetType = (GlobalEnum.MILITARY_TYPE)_AtkType.Current;
        
        if ((_TargetType & GlobalEnum.MILITARY_TYPE.ARMY) != 0)
        {
            _AI = GetNearestAI(GlobalEnum.MILITARY_TYPE.ARMY, ref _Range);
        }

        if ((_TargetType & GlobalEnum.MILITARY_TYPE.AIRFORCE) != 0)
        {
            _AI = GetNearestAI(GlobalEnum.MILITARY_TYPE.AIRFORCE, ref _Range);
        }

        if (_AI != null)
        {
            TargetAI = _AI;
            TargetAI.SetObserver(this);
        }
    }

    protected MyAI GetNearestAI(GlobalEnum.MILITARY_TYPE iType, ref float iDist)
    {
        List<MyAI> _AIList = AIManager.Instance.GetAIListByMilitary(iType);
        if (_AIList == null)
            return null;
        
        MyAI _Target = null;
        foreach (MyAI _Temp in _AIList)
        {
            Vector3 _V = transform.position - _Temp.transform.position;
            if (_V.magnitude <= iDist)
            {
                iDist = _V.magnitude;
                _Target = _Temp;
            }
        }

        return _Target;
    }
}
