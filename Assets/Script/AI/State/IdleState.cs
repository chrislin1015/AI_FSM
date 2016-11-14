using UnityEngine;
using System.Collections;

public class IdleState : State 
{
	IdleState()
	{
		StateID = "IdleState";
	}

	virtual public void PreUpdate(AI iAI)
	{
	}

	virtual public void Update(AI iAI)
	{
        if (iAI == null)
            return;

        //AIManager.Instance.SearchTarget(iAI.transform, 
	}

	virtual public void PostUpdate(AI iAI)
	{
	}
}
