using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Controller : MonoBehaviour
{
    public MeshRenderer _meshRenderer;
    public GameObject PS_Inpact, PS_Trail;
    public Rigidbody _body;

    float speed = 10f;
    bool stop = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        stop = true;
        PS_Inpact.SetActive(true);
        PS_Trail.SetActive(false);
        _meshRenderer.enabled = false;
        Invoke("StartOver", 1f);
    }

    void StartOver()
    {
        _meshRenderer.enabled = true;
        PS_Inpact.SetActive(false);
        PS_Trail.SetActive(true);
        gameObject.SetActive(false);
        //RestartPosition
    }
}
