using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fire : MonoBehaviour
{
    GameObject bulletPrefab;
    public Enemy_Master E_Master;
    public TargetSearch tSearch;
    public Enemy_AI AI;

    Transform bulletContainer;
    List<Transform> shotPoints = new List<Transform>();

    void OnEnable()
    {
        bulletPrefab = GameManager_References.instance.BulletPrefabs[Random.Range(0,10)];   // Hardcode 10
        E_Master.EventTurretCreated += GetShotPoint;
        bulletContainer = GameObject.FindGameObjectWithTag(GameManager_References.instance.bulletContainerTag).transform;
    }

    void OnDisable()
    {
        E_Master.EventTurretCreated += GetShotPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
    }

    public void GetShotPoint(Transform turret)
    {
        shotPoints.Add(turret.GetChild(0).GetChild(0));
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            Transform enemy = tSearch.getClosestEnemyInRange(AI.Stats.range);

            if (enemy != null && shotPoints.Count > 0)
            {
                foreach (Transform sP in shotPoints)
                {
                    Bullet bullet = Instantiate(bulletPrefab, sP.position, sP.rotation, bulletContainer).GetComponent<Bullet>();
                    bullet.ID = gameObject.GetInstanceID();
                    bullet.damage = AI.Stats.damage;
                    bullet.speed = AI.Stats.bulletSpeed;
                    bullet.E_Master = this.E_Master;
                    
                    Animator animator = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Animator>();
                    if(animator != null)
                    {
                        animator.SetTrigger("Fire");
                    }
                    
                }

                yield return new WaitForSeconds(1 / AI.Stats.fireRate);
            }
            else
            {
                yield return null;
            }
        }
    }
}
