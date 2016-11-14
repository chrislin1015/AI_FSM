using UnityEngine;
using System.Collections;

public class MyAI : AI 
{
    protected AIDataCenter.AIData AIData;
    public AIDataCenter.AIData Data
    {
        get { return AIData; }
    }

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
}
