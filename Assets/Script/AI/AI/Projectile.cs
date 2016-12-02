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

public class Projectile : MonoBehaviour 
{
    public string mID;
    public GameObject mImpactEffect;
    protected MyAI mTargetAI;
    protected MyAI mSourceAI;
    protected int mDamage;
    protected AIDataCenter.AIData mAIData;
    protected Vector3 mHitPoint;

    // Update is called once per frame
    void Update() 
    {
        Vector3 _TargetPos = mHitPoint;
        if (mTargetAI != null)
        {
            _TargetPos = mTargetAI.mHitPoint.position;
            mHitPoint = mTargetAI.mHitPoint.position;
        }

        Vector3 _Temp = transform.position;
        Vector3 _Dir = _TargetPos - transform.position;
        transform.position += _Dir.normalized * mAIData.MoveSpeed * Time.deltaTime;
        Vector3 _Dir2 = transform.position - _Temp;
        if (_Dir2.magnitude >= _Dir.magnitude)
        {
            transform.position = _TargetPos;
            Hit(transform.position, _Dir);
        }
        else
        {
        }
    }

    public void Initial(MyAI iSourceAI, MyAI iTargetAI)
    {
        mAIData = AIDataCenter.Instance.GetData(mID);
        mSourceAI = iSourceAI;
        mTargetAI = iTargetAI;
        transform.position = iSourceAI.mProjectPoint.position;
        mHitPoint = iTargetAI.mHitPoint.position;

        AttTemplate<int> _Damage = (AttTemplate<int>)mSourceAI.GetAttribute(GlobalEnum.ATTRIBUTE_TYPE.DAMAGE.ToString());
        if (_Damage == null)
            return;

        mDamage = _Damage.Current;
    }

    protected void Hit(Vector3 iHitPoint, Vector3 iImpactNormal)
    {
        if (mTargetAI != null)
        {
            mTargetAI.Damage(mDamage);
        }

        GameObject _ImpactEffect = Instantiate(mImpactEffect, iHitPoint, Quaternion.FromToRotation(Vector3.up, iImpactNormal)) as GameObject;
        Destroy(gameObject);
        Destroy(_ImpactEffect, 3.0f);
    }
}
