using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;

    Vector3 diff;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + diff;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -42.5f, 42.5f), transform.position.y, Mathf.Clamp(transform.position.z, -30f, 3f)); 
        }
    }

    public void setTarget(Transform t)
    {
        target = t;
        diff = transform.position - target.position;
    }
}
