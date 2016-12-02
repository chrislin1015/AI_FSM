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

public class MoveState : State 
{
    public MoveState()
    {
        StateID = "MoveState";
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

        if (_AI.TargetAI == null)
        {
            _AI.ChangeState("IdleState");
        }
        else
        {
            _AI.SearchTarget();
            if (_AI.TargetAI != null)
            {
                if (MoveToTarget(_AI))
                {
                    _AI.ChangeState("PrepareAtkState");
                }
            }
        }
    }

    override public void PostUpdate(AI iAI)
    {
    }

    public bool MoveToTarget(MyAI iAI)
    {
        if (iAI.TargetAI == null)
            return false;

        Vector3 _Dir = iAI.TargetAI.transform.position - iAI.transform.position;

        AttTemplate<float> _Speed = (AttTemplate<float>)iAI.GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.MOVE_SPEED.ToString());
        if (_Speed == null)
            return false;

        AttTemplate<float> _Range = (AttTemplate<float>)iAI.GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.ATK_RANGE.ToString());
        if (_Range == null)
            return false;

        if (_Dir.magnitude <= _Range.Current)
        {
            return true;
        }
        else
        {
            iAI.transform.position += _Dir.normalized * _Speed.Current * Time.deltaTime;
        }

        return false;
    }
}
