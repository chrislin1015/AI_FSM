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

public class Generator : MonoBehaviour 
{
    [SerializeField]
    public class GeneratorInfo
    {
        public GameObject AISource;
        public int Percent;
    }

    public List<GeneratorInfo> GeneratorList = new List<GeneratorInfo>();
    public float GeneratorTime;

    void Start() 
    {
        StartCoroutine(GeneratorAI());
    }

    IEnumerator GeneratorAI()
    {
        while(true)
        {
            yield return new WaitForSeconds(GeneratorTime);

            int _Rate = 0;
            int _Random = Random.Range(0, 10000);
            foreach(GeneratorInfo _Info in GeneratorList)
            {
                if (_Random >= _Rate && _Random <= _Rate + _Info.Percent)
                {
                    GameObject _NewAI = Instantiate(_Info.AISource) as GameObject;
                    if (_NewAI != null)
                    {
                        _NewAI.transform.position = transform.position;
                        break;
                    }
                }
            }

            yield return null;
        }
    }
}
