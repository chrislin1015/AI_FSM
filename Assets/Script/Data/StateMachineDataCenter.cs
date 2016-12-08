using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachineDataCenter : object 
{
    public class StateMachineData : object
    {
        public string ID;
        public string State1ID;
        public string State2ID;
        public string State3ID;
        public string State4ID;
        public string State5ID;
        public string State6ID;
        public string State7ID;
        public string State8ID;
        public string State9ID;
        public string State10ID;
    }

    static protected StateMachineDataCenter sInstance;
    static public StateMachineDataCenter Instance
    {
        get { return sInstance; }
    }

    protected XMLLoader<StateMachineData> mDataLoader;

    protected Dictionary<string, StateMachineData> mDataMap;
    public Dictionary<string, StateMachineData> DataMap
    {
        get { return mDataMap; }
    }

    protected StateMachineDataCenter()
    {
        mDataLoader = new XMLLoader<StateMachineData>();
    }

    ~StateMachineDataCenter()
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

        sInstance = new StateMachineDataCenter();

        sInstance.mDataLoader.Initial(iFilePath, iIsContent);

        sInstance.mDataMap = new Dictionary<string, StateMachineData>();
        foreach (StateMachineData _Data in sInstance.mDataLoader.DataList)
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

    public StateMachineData GetData(int iIndex)
    {
        return mDataLoader.GetData(iIndex);
    }

    public StateMachineData GetData(string iID)
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
