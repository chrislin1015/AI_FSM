using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDataCenter : object 
{
    public class AIData : object
    {
        public string ID;
        public int MinHP;
        public int MaxHP;
        public string StateMachineID;
        public float AtkRange;
        public float AtkSpeed;
        public int Damage;
    }

    /*
     * 獨體物件
     */
    static protected AIDataCenter sInstance;
    static public AIDataCenter Instance
    {
        get { return sInstance; }
    }

    protected XMLLoader<AIData> mDataLoader;

    protected Dictionary<string, AIData> mDataMap;
    public Dictionary<string, AIData> DataMap
    {
        get { return mDataMap; }
    }

    protected AIDataCenter()
    {
        mDataLoader = new XMLLoader<AIData>();
    }

    ~AIDataCenter()
    {
        if (mDataMap != null)
        {
            mDataMap.Clear();
            mDataMap = null;
        }

        mDataLoader = null;
    }

    static public void Initial(string iFilePath, bool iIsContent = false)
    {
        if (sInstance != null)
        {
            return;
        }

        sInstance = new AIDataCenter();

        sInstance.mDataLoader.Initial(iFilePath, iIsContent);

        sInstance.mDataMap = new Dictionary<string, AIData>();
        foreach (AIData _Data in sInstance.mDataLoader.DataList)
        {
            sInstance.mDataMap.Add(_Data.ID, _Data);
        }
    }

    static public void Release()
    {
        sInstance = null;
    }

    public bool IsInitial()
    {
        return mDataLoader.IsInitial();
    }

    public AIData GetData(int iIndex)
    {
        return mDataLoader.GetData(iIndex);
    }

    public AIData GetData(string iID)
    {
        if (IsInitial() == false)
        {
            return null;
        }

        if (string.IsNullOrEmpty(iID))
        {
            return null;
        }

        if (mDataMap.ContainsKey(iID) == false)
        {
            return null;
        }

        return mDataMap[iID];
    }
}
