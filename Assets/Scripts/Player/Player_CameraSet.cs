using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CameraSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Camera.main.GetComponent<CameraFollow>().setTarget(transform);
        Destroy(this);
    }
}
