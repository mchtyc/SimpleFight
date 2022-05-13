using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Player_Movement : MonoBehaviour
{
    public Animator myAnimator;
    public NavMeshAgent _navMeshAgent;
    public Rigidbody _rigidbody;
    public Character_Stats P_Stats;
    float horizontal, vertical;
    Vector3 dir, defaultTouchpadPos;

    Transform Touchpad, TouchpadBtn;
    Image touchpadImage;
    [SerializeField]
    Color clickedColor, defaultColor;

    bool isClickReady = false, isTouchReady = false;

    void Start()
    {
        Touchpad = GameObject.FindGameObjectWithTag("Touchpad").transform;
        touchpadImage = Touchpad.gameObject.GetComponent<Image>();
        defaultTouchpadPos = Touchpad.position;
        TouchpadBtn = Touchpad.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TestMovement();
        TouchMovement();
        EditorMovement();
    }

    void EditorMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //transform.position += new Vector3(horizontal, 0f, vertical) * Time.deltaTime * P_Stats.speed;
        _rigidbody.velocity = new Vector3(horizontal, 0f, vertical) * Time.deltaTime * P_Stats.speed * 50f;

        dir = new Vector3(horizontal, 0f, vertical).normalized;

        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
            transform.rotation = lookRotation;
            myAnimator.SetBool("isMoving", true);
        }
        else
        {
            if (myAnimator.GetBool("isMoving") == true)
            {
                myAnimator.SetTrigger("endMove");
            }

            myAnimator.SetBool("isMoving", false);
            _rigidbody.velocity = Vector3.zero;
        }
    }
        
    void TouchMovement()
    {
        Touch t;

        if(Input.touchCount > 0)
        {
            t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x < Screen.width*4/5 && t.position.y < Screen.height * 3 / 4)
                {
                    Touchpad.position = t.position;
                    touchpadImage.color = clickedColor;
                    isTouchReady = true;
                }
            }
            else if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                if (isTouchReady)
                {
                    if (Vector3.Distance(t.position, Touchpad.position) >= 40)
                    {
                        Vector3 dirAndroid = (new Vector3(t.position.x, t.position.y, 0f) - Touchpad.position).normalized;

                        if (Vector3.Distance(t.position, Touchpad.position) < 120)
                        {
                            TouchpadBtn.position = t.position;
                        }
                        else
                        {
                            TouchpadBtn.localPosition = dirAndroid * 120f;
                        }

                        //transform.position += new Vector3(dirAndroid.x, 0f, dirAndroid.y) * Time.deltaTime * P_Stats.speed;
                        _rigidbody.velocity = new Vector3(dirAndroid.x, 0f, dirAndroid.y) * Time.deltaTime * P_Stats.speed * 50;

                        if (dirAndroid != Vector3.zero)
                        {
                            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dirAndroid.x, 0f, dirAndroid.y));
                            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
                            transform.rotation = lookRotation;
                        }
                    } 
                }
            }
            else
            {
                Touchpad.position = defaultTouchpadPos;
                TouchpadBtn.localPosition = Vector3.zero;
                touchpadImage.color = defaultColor;
                isTouchReady = false;
                _rigidbody.velocity = Vector3.zero;
            }
        }
    }

    void TestMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
                if (Input.mousePosition.x < Screen.width * 4 / 5 && Input.mousePosition.y < Screen.height * 3 / 4)
                {
                    Touchpad.position = Input.mousePosition;
                    touchpadImage.color = clickedColor;
                
                    isClickReady = true;
                }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isClickReady)
            {
                if (Vector3.Distance(Input.mousePosition, Touchpad.position) < 120f)
                {
                    TouchpadBtn.position = Input.mousePosition;
                }
                else
                {
                    TouchpadBtn.localPosition = (Input.mousePosition - TouchpadBtn.position).normalized * 120f;
                } 
            }
        }
        else
        {
            if (touchpadImage.color.a > 0.8f)
            {
                touchpadImage.color = defaultColor;
                Touchpad.position = defaultTouchpadPos;
                TouchpadBtn.localPosition = Vector3.zero;
                isClickReady = false;
            }
        }
    }
}