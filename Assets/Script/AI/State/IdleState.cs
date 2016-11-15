using UnityEngine;
using System.Collections;

public class IdleState : State 
{
	IdleState()
	{
		StateID = "IdleState";
	}

    override public void PreUpdate(AI iAI)
	{
	}

    override public void Update(AI iAI)
	{
        if (iAI == null)
            return;

        MyAI _AI = iAI as MyAI;
        if (_AI == null)
            return;
        
        _AI.SearchTarget();
        if (_AI.TargetAI != null)
        {
            _AI.ChangeState("MoveState");
        }
	}

    override public void PostUpdate(AI iAI)
	{
	}
}
