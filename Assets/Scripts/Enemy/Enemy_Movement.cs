using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour
{
    // NPClerin oyun alanına daha homojen dağılmaları için Movement kesinlikle...!!! geliştirilmeli

    public Enemy_AI AI;
    public LayerMask characterLayer;
    public Animator myAnimator;

    Transform target;
    NavMeshAgent agent;
    bool isFollowing, isMoving;
    float followingTime;

    // Start is called before the first frame update
    void Start()
    {
        isFollowing = isMoving = false;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = AI.Stats.speed;   // NOT: speed zamanında ayarlanamadığı için ExeOrder 101 yapıldı.

        StartCoroutine(Movement());
    }

    void Update()
    {
        agent.speed = AI.Stats.speed; //    Şimdilik böyle olsun 

        if (isFollowing)
        {
            followingTime += Time.deltaTime;

            if (followingTime >= 5f)
            {
                isFollowing = false;
                target = null;
            }
        }

        if (agent.velocity != Vector3.zero)
        {
            if (!isMoving)
            {
                myAnimator.SetBool("NPC_Moving", true);
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                myAnimator.SetBool("NPC_Moving", false);
                isMoving = false;
            }
        }
    }

    IEnumerator Movement()  // Movement GELİŞTİRİLMELİ
    {
        while (true)
        {
            SetTarget();

            while (target != null && isFollowing)
            {
                agent.SetDestination(target.position);
                yield return new WaitForSeconds(0.5f);

                if(target != null  && Vector3.Distance(target.position, transform.position) > AI.Stats.range)
                {
                    isFollowing = false;
                    target = null;
                }
            }

            while (target == null)
            {
                agent.SetDestination(new Vector3(Random.Range(-100f, 100f), transform.position.y, Random.Range(-100f, 100f)));

                Vector3 turning = new Vector3(Random.Range(0f,1f), 0f, Random.Range(0f, 1f));
                float t = 0f;
                yield return new WaitForEndOfFrame();

                while (Random.Range(0f, 10f) < 9f)
                {
                    agent.SetDestination(agent.destination + turning);
                    t += Time.deltaTime;

                    if (t >= 5f)
                    {
                        break;
                    }

                    yield return new WaitForEndOfFrame();
                }

                if (Random.Range(1,5) == 1)
                {
                    SetTarget();
                }
            }

            //while (agent.remainingDistance > agent.stoppingDistance)
            //{
            //    yield return null;
            //}
        }
    }

    void SetTarget()
    {
        // Collectable toplama eklenecek

        foreach (Collider col in Physics.OverlapSphere(transform.position, AI.Stats.range, characterLayer))
        {
            if (col.gameObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                continue;
            }

            if (Random.Range(0,100) < 1)   // 10da 1 ihtimalle enemy takip etsin
            {
                target = col.transform;
                isFollowing = true;
                followingTime = 0f;
                break;
            }
            else
            {
                target = null;
                isFollowing = false;
                
            }
        }
    }
}
