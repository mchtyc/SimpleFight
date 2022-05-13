using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnDestroy()
    {
        GameManager_References.instance.coinCount--;
    }
}
