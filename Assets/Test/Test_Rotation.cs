using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Rotation : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
        else
        {
            Quaternion lookRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
