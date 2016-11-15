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

    protected Dictionary<GlobalEnum.MILITARY_TYPE, List<MyAI>> AIs = new Dictionary<GlobalEnum.MILITARY_TYPE, List<MyAI>>();

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

    public void RegisterAI(MyAI iAI, GlobalEnum.MILITARY_TYPE iType)
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
            List<MyAI> _NewTypeAIList = new List<MyAI>();
            if (_NewTypeAIList == null)
                return;

            _NewTypeAIList.Add(iAI);
            AIs.Add(iType, _NewTypeAIList);
        }
    }

    public List<MyAI> GetAIListByMilitary(GlobalEnum.MILITARY_TYPE iType)
    {
        if (AIs.ContainsKey(iType))
        {
            return AIs[iType];
        }

        return null;
    }
}
