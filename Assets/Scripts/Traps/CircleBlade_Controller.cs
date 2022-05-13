using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBlade_Controller : MonoBehaviour
{
    public Transform Blade, LeftTip, RightTip;

    [HideInInspector] public float damage = 50f;

    bool isLeft;
    float moveSpeed = 5f, angularSpeed = 500f;

    // Start is called before the first frame update
    void Start()
    {
        isLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLeft)
        {
            transform.localPosition -= Vector3.forward * Time.deltaTime * moveSpeed;
            Blade.Rotate(Vector3.left, Time.deltaTime * angularSpeed);

            if (Vector3.Distance(transform.position, LeftTip.position) <= 1f)
            {
                isLeft = true;
            }
        }
        else
        {
            transform.localPosition += Vector3.forward * Time.deltaTime * moveSpeed;
            Blade.Rotate(Vector3.right, Time.deltaTime * angularSpeed);

            if (Vector3.Distance(transform.position, RightTip.position) <= 1f)
            {
                isLeft = false;
            }
        }
    }
}
