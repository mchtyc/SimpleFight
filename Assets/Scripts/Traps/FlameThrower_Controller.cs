using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower_Controller : MonoBehaviour
{
    public GameObject PS;
    public BoxCollider _collider;
    public Animator _animator;

    [HideInInspector]public float damage = 50f;

    // Start is called before the first frame update
    void Start()
    {
        _collider.enabled = false;
        PS.SetActive(false);

        StartCoroutine(flameThrow());
    }

    IEnumerator flameThrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            PS.SetActive(true);
            _collider.enabled = true;
            _animator.enabled = true;
            _animator.SetTrigger("Rotate");

            yield return new WaitForSeconds(5f);

            PS.SetActive(false);
            _collider.enabled = false;
            _animator.enabled = false;
        }
    }
}
