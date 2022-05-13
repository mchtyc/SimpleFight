using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTag_Rotation : MonoBehaviour
{
    [SerializeField]
    Vector3 rotateAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAmount);
    }
}
