using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDataCenter : object 
{
    public class AIData : object
    {
        //單位的ID
        public string ID;
        //HP最小值
        public int MinHP;
        //HP最大值
        public int MaxHP;
        //狀態機ID
        public string StateMachineID;
        //攻擊範圍
        public float AtkRange;
        //攻擊速度
        public float AtkSpeed;
        //攻擊力
        public int Damage;
        //軍種，0 = 地面單位，1 = 空中單位，2 = 建築物
        public int Military;
        //攻擊類型，1 = 地面部隊，2 = 空中部隊，4 = 建築物。用位元印射法對應如果要全部單位都可以打，就要設定為1+2+4=7
        public int AtkType;
        //移動速度
        public float MoveSpeed;
        //攻擊模式，0 = 一般攻擊，1 = 遠程投射
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
