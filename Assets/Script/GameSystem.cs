using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour 
{
    static protected GameSystem sInstance;
    static public GameSystem Instance
    {
        get { return sInstance; }
    }

    void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnDestroy()
    {
        if (sInstance == this)
        {
            sInstance = null;
        }
    }

    // Use this for initialization
    void Start() 
    {
        
    }
	
    // Update is called once per frame
    void Update() 
    {
    }
}
