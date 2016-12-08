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

public class AIManager : MonoBehaviour
{
    protected static AIManager sInstance = null;
    public static AIManager Instance
    {
        get { return sInstance; }

    }

    protected Dictionary<GlobalEnum.CAMP_TYPE, Dictionary<GlobalEnum.MILITARY_TYPE, List<MyAI>>> mAIs = new Dictionary<GlobalEnum.CAMP_TYPE, Dictionary<GlobalEnum.MILITARY_TYPE, List<MyAI>>>();

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

        foreach (KeyValuePair<GlobalEnum.CAMP_TYPE, Dictionary<GlobalEnum.MILITARY_TYPE, List<MyAI>>> _CampClass in mAIs)
        {
            foreach(KeyValuePair<GlobalEnum.MILITARY_TYPE, List<MyAI>> _MilitaryClass in _CampClass.Value)
            {
                for(int i = 0; i < _MilitaryClass.Value.Count; ++i)
                {
                    Destroy(_MilitaryClass.Value[i]);
                }
                _MilitaryClass.Value.Clear();
            }
            _CampClass.Value.Clear();
        }
        mAIs.Clear();
        mAIs = null;
    }

    public void RegisterAI(MyAI iAI, GlobalEnum.CAMP_TYPE iCampType)
    {
        if (iAI == null)
            return;

        iAI.Initial(iCampType);
        GlobalEnum.MILITARY_TYPE _MilitaryType = (GlobalEnum.MILITARY_TYPE)iAI.AIData.Military;

        if (mAIs.ContainsKey(iCampType))
        {
            if (mAIs[iCampType].ContainsKey(_MilitaryType))
            {
                if (mAIs[iCampType][_MilitaryType].Contains(iAI))
                    return;

                mAIs[iCampType][_MilitaryType].Add(iAI);
            }
            else
            {
                List<MyAI> _NewTypeAIList = new List<MyAI>();
                if (_NewTypeAIList == null)
                    return;

                _NewTypeAIList.Add(iAI);
                mAIs[iCampType].Add(_MilitaryType, _NewTypeAIList);
            }
        }
        else
        {
            Dictionary<GlobalEnum.MILITARY_TYPE, List<MyAI>> _NewCamp = new Dictionary<GlobalEnum.MILITARY_TYPE, List<MyAI>>();
            if (_NewCamp == null)
                return;

            List<MyAI> _NewTypeAIList = new List<MyAI>();
            if (_NewTypeAIList == null)
                return;

            _NewTypeAIList.Add(iAI);
            _NewCamp.Add(_MilitaryType, _NewTypeAIList);
            mAIs.Add(iCampType, _NewCamp);
        }
    }

    public void RemoveAI(MyAI iAI, GlobalEnum.CAMP_TYPE iCampType)
    {
        if (iAI == null)
            return;

        GlobalEnum.MILITARY_TYPE _MilitaryType = (GlobalEnum.MILITARY_TYPE)iAI.AIData.Military;
        if (mAIs.ContainsKey(iCampType) &&
            mAIs[iCampType].ContainsKey(_MilitaryType))
        {
            mAIs[iCampType][_MilitaryType].Remove(iAI);
        }
    }

    public List<MyAI> GetAIListByMilitary(GlobalEnum.CAMP_TYPE iCampType, GlobalEnum.MILITARY_TYPE iMilitaryType)
    {
        if (mAIs.ContainsKey(iCampType) &&
            mAIs[iCampType].ContainsKey(iMilitaryType))
        {
            return mAIs[iCampType][iMilitaryType];
        }

        return null;
    }
}
