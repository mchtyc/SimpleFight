using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBomb_Controller : MonoBehaviour
{
    public GameObject PS_Inpact;
    public MeshRenderer _meshRenderer;
    public Rigidbody _rigidbody;

    Vector3 defaultPos;
    float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        PS_Inpact.SetActive(false);
        defaultPos = transform.position;
    }

    void OnEnable()
    {
        _meshRenderer.enabled = true;
    }

    void Update()
    {
        _rigidbody.velocity = Vector3.down * Time.deltaTime * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        _rigidbody.velocity = Vector3.zero;
        PS_Inpact.SetActive(true);
        _meshRenderer.enabled = false;
        Invoke("StartOver", 1.1f);
    }

    void StartOver()
    {
        PS_Inpact.SetActive(false);
    }
}
