using UnityEngine;
using System.Collections;

public class GlobalEnum : MonoBehaviour 
{
    public enum MILITARY_TYPE
    {
        NONE = 0,
        ARMY = 1, 
        AIRFORCE = 2,
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
        MAX
    }
}
