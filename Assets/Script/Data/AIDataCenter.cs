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
        public int Military;
        public int AtkType;
        public float MoveSpeed;
        public int AtkMode;
        public string StateAnimation1;
        public string StateAnimation2;
        public string StateAnimation3;
        public string StateAnimation4;
        public string StateAnimation5;
        public string StateAnimation6;
        public string StateAnimation7;
        public string StateAnimation8;
        public string StateAnimation9;
        public string StateAnimation10;
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
