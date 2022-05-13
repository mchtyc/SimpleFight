using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_getPlayer : MonoBehaviour
{
    GM gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GM>() as GM_SetScene;

        if (gm != null) { Debug.Log("ok"); }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
