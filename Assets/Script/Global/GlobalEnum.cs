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

public class GlobalEnum : MonoBehaviour 
{
    public enum MILITARY_TYPE
    {
        ARMY,
        AIRFORCE,
        BUILDING,
        MAX
    }

    public enum ATK_MODE
    {
        MELEE,  //近戰
        SHOOT,  //遠程攻擊   
        MAX
    }

    public enum ATK_TYPE
    {
        NONE = 0,
        ARMY = 1, 
        AIRFORCE = 2,
        BUILDING = 4,
        MAX
    }

    public enum ATTRIBUTE_TYPE
    {
        HP,
        ATK_RANGE,
        ATK_SPEED,
        DAMAGE,
        MILITARY,
        ATK_TYPE,
        MOVE_SPEED,
        ATK_MODE,
        MAX
    }
}
