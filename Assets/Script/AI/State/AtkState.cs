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

public class AtkState : State 
{
    public AtkState()
    {
        StateID = "AtkState";
    }

    override public void PreUpdate(AI iAI)
    {
        MyAI _AI = iAI as MyAI;
        if (_AI == null)
            return;

        if (_AI.mTargetAI == null)
            return;

        AttTemplate<GlobalEnum.ATK_MODE> _AtkMode = (AttTemplate<GlobalEnum.ATK_MODE>)iAI.GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.ATK_MODE.ToString());
        if (_AtkMode == null)
            return;

        AttTemplate<int> _Damage = (AttTemplate<int>)iAI.GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.DAMAGE.ToString());
        if (_Damage == null)
            return;

        if (_AtkMode.Current == (int)GlobalEnum.ATK_MODE.MELEE)
        {
            _AI.mTargetAI.Damage(_Damage.Current);
        }
        else
        {
            _AI.Shoot();
        }
    }

    override public void Update(AI iAI)
    {
        if (iAI == null)
            return;

        MyAI _AI = iAI as MyAI;
        if (_AI == null)
            return;

        if (_AI.Ani.isPlaying == false)
        {
            if (_AI.mTargetAI == null)
            {
                _AI.ChangeState("IdleState");
            }
            else
            {
                _AI.ChangeState("PrepareAtkState");
            }
        }
    }

    override public void PostUpdate(AI iAI)
    {
    }
}