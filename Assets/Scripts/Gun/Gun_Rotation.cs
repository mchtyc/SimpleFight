using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Rotation : MonoBehaviour
{
    public Character_Stats Stats;

    TargetSearch tSearch;
    Quaternion lookRotation;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        tSearch = GetComponentInParent<TargetSearch>();

        if (transform.parent.parent.parent.CompareTag("Enemy"))
        {
            Stats = transform.parent.parent.parent.GetComponent<Enemy_AI>().Stats;
        }
    }

    // Update is called once per frame
    void Update()
    {
        target = tSearch.getClosestEnemyInRange(Stats.range);

        if (target != null)
        {
            transform.LookAt(target);
        }
        else
        {
            lookRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
