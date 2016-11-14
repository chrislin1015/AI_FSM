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

        AttTemplate<float> _ATKRange = new AttTemplate<float>(GlobalEnum.ATTRIBUTE_TYPE.ATK_RANGE.ToString(), AIData.AtkRange, AIData.AtkRange);
        Attributes.Add(_ATKRange.GetID(), _ATKRange);

        AttTemplate<float> _ATKSpeed = new AttTemplate<float>(GlobalEnum.ATTRIBUTE_TYPE.ATK_SPEED.ToString(), AIData.AtkSpeed, AIData.AtkSpeed);
        Attributes.Add(_ATKSpeed.GetID(), _ATKSpeed);

        AttTemplate<int> _Damage = new AttTemplate<int>(GlobalEnum.ATTRIBUTE_TYPE.DAMAGE.ToString(), AIData.Damage, AIData.Damage);
        Attributes.Add(_Damage.GetID(), _Damage);
    }
}
