using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    void OnDestroy()
    {
        GameManager_References.instance.xpCount--; 
    }
}
