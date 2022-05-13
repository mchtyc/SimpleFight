using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Ball_Controller : MonoBehaviour
{
    public float damage = 50f;
    public GameObject Splash_PS, Trail_PS;
    public MeshRenderer _renderer;
    public Cannon_Controller Cannon_C;
    public Transform defaultParent;

    float speed = 10f, defaultSpeed = 10f;
    Vector3 defaultPos, defaultScale;
    Quaternion defaultRot;

    void Awake()
    {
        defaultPos = transform.localPosition;
        defaultScale = transform.localScale;
        defaultRot = transform.localRotation;

        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        Invoke("SelfDestruct", 4f);
    }

    void SelfDestruct()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(StartOver());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        Splash_PS.SetActive(true);
        Trail_PS.SetActive(false);
        speed = 0f;
        _renderer.enabled = false;
        StartCoroutine(StartOver());
    }

    IEnumerator StartOver()
    {
        yield return new WaitForSeconds(0.7f);

        transform.SetParent(defaultParent);
        Splash_PS.SetActive(false);
        Trail_PS.SetActive(true);
        transform.localRotation = defaultRot;
        transform.localPosition = defaultPos;
        transform.localScale = defaultScale;
        _renderer.enabled = true;
        speed = defaultSpeed;

        Cannon_C.isBallReady = true;
        gameObject.SetActive(false);
    }
}
