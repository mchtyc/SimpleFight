using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Controller : MonoBehaviour
{
    public GameObject Ball;
    public Transform rotationMill;
    public Animator _animator;

    [HideInInspector]   public bool isBallReady;

    int dir;

    // Start is called before the first frame update
    void Start()
    {
        isBallReady = true;
        Invoke("CannonStart", 3f);
    }

    void CannonStart()
    {
        StartCoroutine(Shoot());
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        dir = Random.Range(-1, 2);
        float maxTime = Random.Range(1f, 3f), speed = Random.Range(10f, 50f), t = 0f;

        while (t < maxTime)
        {
            rotationMill.Rotate(Quaternion.Euler(0f, 0f, Time.deltaTime * speed * dir).eulerAngles);
            yield return null;

            t += Time.deltaTime;
        }

        StartCoroutine(Rotate());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 7f));

            if (isBallReady)
            {
                dir = 0;
                _animator.SetTrigger("Shoot");
                isBallReady = false;
            }
        }
    }

    public void FireBall()
    {
        Ball.transform.SetParent(transform);
        Ball.SetActive(true);
    }
}
