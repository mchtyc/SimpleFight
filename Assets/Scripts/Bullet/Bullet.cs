using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bu değişkenleri bulletın vurduğu diğer characterler kullanıyor.
    [HideInInspector] public float damage;
    [HideInInspector] public int ID;
    [HideInInspector] public float speed;
    [HideInInspector] public Enemy_Master E_Master;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager_References.instance.envirementTag))
        {
            Destroy(gameObject);
        }
    }
}
