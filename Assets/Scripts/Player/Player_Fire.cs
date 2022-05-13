using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fire : MonoBehaviour
{
    public GameObject bulletPrefab, TargetTagPrefab;
    public Character_Stats P_Stats;
    public Player_Master P_Master;

    GameObject targetTag;
    Transform bulletContainer, enemy;
    TargetSearch tSearch;
    List<Transform> shotPoints = new List<Transform>();

    void OnEnable()
    {
        P_Master.EventTurretCreated += GetShotPoint;
        bulletContainer = GameObject.FindGameObjectWithTag(GameManager_References.instance.bulletContainerTag).transform;
    }

    void OnDisable()
    {
        P_Master.EventTurretCreated += GetShotPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetTag = Instantiate(TargetTagPrefab, GameObject.Find("Managers").transform);
        
        tSearch = GetComponent<TargetSearch>();
        StartCoroutine(Fire());
    }

    void Update()
    {
        if (enemy != null)
        {
            targetTag.transform.position = enemy.transform.position - Vector3.up;
            targetTag.SetActive(true);
        }
        else
        {
            if (targetTag.activeSelf)
            {
                targetTag.SetActive(false);
            }
        }
    }

    public void GetShotPoint(Transform turret)
    {
        shotPoints.Add(turret.GetChild(0).GetChild(0));
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            enemy = tSearch.getClosestEnemyInRange(P_Stats.range);
            
            if (enemy != null && shotPoints.Count > 0)
            {
                //targetTag.transform.SetParent(enemy);
                //targetTag.transform.localPosition = new Vector3(0f,-1f,0f);
                

                foreach (Transform sP in shotPoints)
                {
                    Bullet bullet = Instantiate(bulletPrefab, sP.position, sP.rotation, bulletContainer).GetComponent<Bullet>();
                    bullet.ID = gameObject.GetInstanceID();
                    bullet.speed = P_Stats.bulletSpeed;

                    Animator animator = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.SetTrigger("Fire");
                    }
                    else
                    {
                        Debug.Log("animator is null");
                    }

                    if (Random.Range(1, 100) > P_Stats.critDamageRate * 100)
                    {
                        bullet.damage = P_Stats.damage;
                    }
                    else
                    {
                        bullet.damage = P_Stats.damage * P_Stats.critDamageMultiplier;
                    }
                }
                
                yield return new WaitForSeconds(1f / P_Stats.fireRate);
            }
            else
            {
                yield return null;
                enemy = null;
            }
        }
    }
}
