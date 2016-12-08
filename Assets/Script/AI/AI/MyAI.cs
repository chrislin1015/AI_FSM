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
using System;

public class MyAI : AI 
{
    public enum DAMAGE_TYPE
    {
        NORMAL,
        IN_HIT,
        FINISH,
        MAX
    }

    protected AIDataCenter.AIData mAIData;
    public AIDataCenter.AIData AIData
    {
        get { return mAIData; }
    }

    public string mInitialStateID;
    public GameObject mProjectile;
    [HideInInspector]
    public MyAI mTargetAI;
    public Transform mProjectPoint;
    public Transform mHitPoint;
    public Color mRimColor;
    public float mRimPower;

    protected List<MyAI> mObserverList = new List<MyAI>();
    protected Rigidbody mRigibody;
    protected Dictionary<string, string> mAnimations = new Dictionary<string, string>();
    protected Renderer[] mRenderers;
    protected GlobalEnum.CAMP_TYPE eCameType;
    public GlobalEnum.CAMP_TYPE CampType
    {
        get { return eCameType; }
    }
    protected float mHitEffectFactor = 0.0f;
    protected DAMAGE_TYPE eDamageType = DAMAGE_TYPE.NORMAL;

    void Start()
    {
        mRigibody = GetComponent<Rigidbody>();
        mRenderers = GetComponentsInChildren<Renderer>();
    }

    void FixedUpdate()
    {
        if (mRigibody != null)
        {
            mRigibody.velocity = Vector3.zero;
        }
    }

    void LateUpdate()
    {
        if (mTargetAI != null)
        {
            Vector3 _Dir = mTargetAI.transform.position - transform.position;
            _Dir = Vector3.Lerp(transform.forward, _Dir.normalized, Time.deltaTime * 10.0f);
            transform.forward = _Dir;
        }

        AttTemplate<float> _ATKSpeed = (AttTemplate<float>)GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.ATK_SPEED.ToString());
        if (_ATKSpeed != null)
        {
            _ATKSpeed.Current = Math.Max(_ATKSpeed.Current - Time.deltaTime, 0.0f);
        }

        switch (eDamageType)
        {
            case DAMAGE_TYPE.NORMAL:
                mHitEffectFactor = 0.0f;
                break;
            case DAMAGE_TYPE.IN_HIT:
                mHitEffectFactor += Time.deltaTime * 5.0f;
                mHitEffectFactor = Mathf.Min(mHitEffectFactor, 1.0f);
                if (mHitEffectFactor >= 1.0f)
                    eDamageType = DAMAGE_TYPE.FINISH;
                break;
            case DAMAGE_TYPE.FINISH:
                mHitEffectFactor -= Time.deltaTime * 5.0f;
                mHitEffectFactor = Mathf.Max(mHitEffectFactor, 0.0f);
                if (mHitEffectFactor <= 0.0f)
                    eDamageType = DAMAGE_TYPE.NORMAL;
                break;
        }

        foreach (Renderer _R in mRenderers)
        {
            if (_R != null)
            {
                Color _RimColor = Color.Lerp(mRimColor, Color.white, mHitEffectFactor);
                float _Pow = Mathf.Lerp(mRimPower, 0.5f, mHitEffectFactor);
                _R.material.SetColor("_RimColor", _RimColor);
                _R.material.SetFloat("_RimPower", _Pow);
            }
        }
    }

    void OnDestroy()
    {
        mObserverList.Clear();
        mObserverList = null;

        mAnimations.Clear();
        mAnimations = null;
    }

    public void Initial(GlobalEnum.CAMP_TYPE iCampType)
    {
        eCameType = iCampType;

        mAIData = AIDataCenter.Instance.GetData(MyID);
        MyStateMachine = StateManager.Instance.GetStateMachine(mAIData.StateMachineID);
        if (MyStateMachine != null)
        {
            MyStateMachine.BindAI(this);
        }
        CreateAttribute();
        StateMappingAnimation();
        ChangeState(mInitialStateID);
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

        AttTemplate<float> _ATKRange = new AttTemplate<float>(GlobalEnum.ATTRIBUTE_TYPE.ATK_RANGE.ToString(), AIData.AtkRange, AIData.AtkRange);
        Attributes.Add(_ATKRange.GetID(), _ATKRange);

        AttTemplate<float> _ATKSpeed = new AttTemplate<float>(GlobalEnum.ATTRIBUTE_TYPE.ATK_SPEED.ToString(), AIData.AtkSpeed, AIData.AtkSpeed);
        Attributes.Add(_ATKSpeed.GetID(), _ATKSpeed);

        AttTemplate<int> _Damage = new AttTemplate<int>(GlobalEnum.ATTRIBUTE_TYPE.DAMAGE.ToString(), AIData.Damage, AIData.Damage);
        Attributes.Add(_Damage.GetID(), _Damage);

        AttTemplate<GlobalEnum.MILITARY_TYPE> _Military = new AttTemplate<GlobalEnum.MILITARY_TYPE>(GlobalEnum.ATTRIBUTE_TYPE.MILITARY.ToString(), (GlobalEnum.MILITARY_TYPE)AIData.Military, (GlobalEnum.MILITARY_TYPE)AIData.Military);
        Attributes.Add(_Military.GetID(), _Military);

        AttTemplate<GlobalEnum.ATK_TYPE> _AtkType = new AttTemplate<GlobalEnum.ATK_TYPE>(GlobalEnum.ATTRIBUTE_TYPE.ATK_TYPE.ToString(), (GlobalEnum.ATK_TYPE)AIData.AtkType, (GlobalEnum.ATK_TYPE)AIData.AtkType);
        Attributes.Add(_AtkType.GetID(), _AtkType);

        AttTemplate<float> _MoveSpeed = new AttTemplate<float>(GlobalEnum.ATTRIBUTE_TYPE.MOVE_SPEED.ToString(), AIData.MoveSpeed, AIData.MoveSpeed);
        Attributes.Add(_MoveSpeed.GetID(), _MoveSpeed);

        AttTemplate<GlobalEnum.ATK_MODE> _AtkMode = new AttTemplate<GlobalEnum.ATK_MODE>(GlobalEnum.ATTRIBUTE_TYPE.ATK_MODE.ToString(), (GlobalEnum.ATK_MODE)AIData.AtkMode, (GlobalEnum.ATK_MODE)AIData.AtkMode);
        Attributes.Add(_AtkMode.GetID(), _AtkMode);
    }

    void StateMappingAnimation()
    {
        if (AIData == null)
            return;

        StateMappingAnimation(AIData.StateAnimation1);
        StateMappingAnimation(AIData.StateAnimation2);
        StateMappingAnimation(AIData.StateAnimation3);
        StateMappingAnimation(AIData.StateAnimation4);
        StateMappingAnimation(AIData.StateAnimation5);
        StateMappingAnimation(AIData.StateAnimation6);
        StateMappingAnimation(AIData.StateAnimation7);
        StateMappingAnimation(AIData.StateAnimation8);
        StateMappingAnimation(AIData.StateAnimation9);
        StateMappingAnimation(AIData.StateAnimation10);
    }

    void StateMappingAnimation(string iData)
    {
        if (iData.Equals("None"))
            return;

        string[] _Separators = {",", ".", "!", "?", ";", ":", " "};
        string[] _Separats = iData.Split(_Separators, StringSplitOptions.RemoveEmptyEntries);
        if (_Separats == null || _Separats.Length <= 1)
            return;

        mAnimations.Add(_Separats[0], _Separats[1]);
    }

    override public void PlayAnimaiton(string iID)
    {
        if (Ani == null)
            return;

        if (mAnimations.ContainsKey(iID) == false)
            return;

        Ani.CrossFade(mAnimations[iID]);
    }

    public void SetObserver(MyAI iObserver)
    {
        if (iObserver == null)
            return;

        mObserverList.Add(iObserver);
    }

    public void Damage(int iDamage)
    {
        AttTemplate<int> _HP = (AttTemplate<int>)GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.HP.ToString());
        if (_HP == null)
            return;

        _HP.Current = Mathf.Max(0, _HP.Current - iDamage);
        if (_HP.Current <= 0)
        {
            foreach (MyAI _Observer in mObserverList)
            {
                _Observer.mTargetAI = null;
            }

            ChangeState("DeathState");
        }

        PlayHitEffect();
    }

    protected void PlayHitEffect()
    {
        mHitEffectFactor = 0.0f;
        eDamageType = DAMAGE_TYPE.IN_HIT;
    }

    public void Shoot()
    {
        if (mProjectile == null || mProjectPoint == null)
            return;

        GameObject _New = Instantiate(mProjectile) as GameObject;
        if (_New == null)
            return;

        Projectile _Proj = _New.GetComponent<Projectile>();
        if (_Proj == null)
            return;

        _Proj.Initial(this, this.mTargetAI);
    }

    /*public void AtkTarget(MyAI iTarget, int iDamage)
    {
        if (iTarget == null)
            return;

        if (AIData.AtkMode == 0)
        {
            iTarget.Damage(iDamage);
        }
        else if (AIData.AtkMode == 1)
        {
            GameObject _New = Instantiate(mProjectile) as GameObject;
            if (_New == null)
                return;
        }
    }*/

    public void SearchTarget()
    {
        float _Range = float.MaxValue;

        AttTemplate<GlobalEnum.ATK_TYPE> _AtkType = (AttTemplate<GlobalEnum.ATK_TYPE>)GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.ATK_TYPE.ToString());
        if (_AtkType == null)
            return;

        GlobalEnum.ATK_TYPE _TargetType = (GlobalEnum.ATK_TYPE)_AtkType.Current;

        GlobalEnum.CAMP_TYPE _CampType;
        if (eCameType == GlobalEnum.CAMP_TYPE.PLAYER)
            _CampType = GlobalEnum.CAMP_TYPE.ENEMY;
        else
            _CampType = GlobalEnum.CAMP_TYPE.PLAYER;

        mTargetAI = null;
        if ((_TargetType & GlobalEnum.ATK_TYPE.ARMY) != 0)
        {
            GetNearestAI(_CampType, GlobalEnum.MILITARY_TYPE.ARMY, ref _Range);
        }

        if ((_TargetType & GlobalEnum.ATK_TYPE.AIRFORCE) != 0)
        {
            GetNearestAI(_CampType, GlobalEnum.MILITARY_TYPE.AIRFORCE, ref _Range);
        }

        if (mTargetAI != null)
        {
            mTargetAI.SetObserver(this);
        }
    }

    protected void GetNearestAI(GlobalEnum.CAMP_TYPE iCampType, GlobalEnum.MILITARY_TYPE iMilitaryType, ref float iDist)
    {
        List<MyAI> _AIList = AIManager.Instance.GetAIListByMilitary(iCampType, iMilitaryType);
        if (_AIList == null)
            return;

        foreach (MyAI _Temp in _AIList)
        {
            if (_Temp == null)
                continue;
            
            Vector3 _V = transform.position - _Temp.transform.position;
            if (_V.magnitude <= iDist)
            {
                iDist = _V.magnitude;
                mTargetAI = _Temp;
            }
        }
    }
}
