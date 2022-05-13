using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CameraSwap : MonoBehaviour
{
    Vector2 startingPos;
    Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                startingPos = t.position;
            }
            else if (t.phase == TouchPhase.Moved)
            {
                float diff = t.position.x - startingPos.x;
                transform.position += new Vector3(diff/200f,0f,0f);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0f, 50f), transform.position.y, transform.position.z);
                startingPos = t.position;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            float diff = Input.mousePosition.x - start.x;
            transform.position -= new Vector3(diff / 500f, 0f, 0f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0f,50f), transform.position.y, transform.position.z);
            start = Input.mousePosition;
        }
    }
}
