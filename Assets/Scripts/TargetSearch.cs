using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearch : MonoBehaviour
{
    public LayerMask character;

    public Transform getClosestEnemyInRange(float range)
    {
        float distance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider col in Physics.OverlapSphere(transform.position, range, character))
        {
            if (col.gameObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                continue;
            }

            Transform t = col.transform;
            float d = Vector3.Distance(t.position, transform.position);
            if (d < distance)
            {
                distance = d;
                closestEnemy = t;
            }
        }

        return closestEnemy;
    }
}
