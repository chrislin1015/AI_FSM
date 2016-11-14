using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
    protected static AIManager sInstance = null;
    public static AIManager Instance
    {
        get { return sInstance; }

    }

    protected Dictionary<GlobalEnum.MILITARY_TYPE, List<AI>> AIs = new Dictionary<GlobalEnum.MILITARY_TYPE, List<AI>>();

    void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
        }
        else
        {
            Destroy(sInstance);
        }
    }

    void Destroy()
    {
        if (sInstance == this)
        {
            sInstance = null;
        }

        AIs.Clear();
        AIs = null;
    }

    public void RegisterAI(AI iAI, GlobalEnum.MILITARY_TYPE iType)
    {
        if (iAI == null)
            return;

        if (AIs.ContainsKey(iType))
        {
            if (AIs[iType].Contains(iAI))
                return;

            AIs[iType].Add(iAI);
        }
        else
        {
            List<AI> _NewTypeAIList = new List<AI>();
            if (_NewTypeAIList == null)
                return;

            _NewTypeAIList.Add(iAI);
            AIs.Add(iType, _NewTypeAIList);
        }
    }

    public AI SearchTarget(Transform iSource, GlobalEnum.MILITARY_TYPE iType)
    {
        float _Range = float.MaxValue;
        AI _AI = null;
        
        if ((iType & GlobalEnum.MILITARY_TYPE.ARMY) != 0)
        {
            if (AIs.ContainsKey(GlobalEnum.MILITARY_TYPE.ARMY))
            {
                foreach (AI _Temp in AIs[GlobalEnum.MILITARY_TYPE.ARMY])
                {
                    Vector3 _V = iSource.position - _Temp.transform.position;
                    if (_V.magnitude <= _Range)
                    {
                        _Range = _V.magnitude;
                        _AI = _Temp;
                    }
                }
            }
        }

        if ((iType & GlobalEnum.MILITARY_TYPE.AIRFORCE) != 0)
        {
            if (AIs.ContainsKey(GlobalEnum.MILITARY_TYPE.AIRFORCE))
            {
                foreach (AI _Temp in AIs[GlobalEnum.MILITARY_TYPE.AIRFORCE])
                {
                    Vector3 _V = iSource.position - _Temp.transform.position;
                    if (_V.magnitude <= _Range)
                    {
                        _Range = _V.magnitude;
                        _AI = _Temp;
                    }
                }
            }
        }

        return _AI;
    }
}
