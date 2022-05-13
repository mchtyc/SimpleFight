using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret : MonoBehaviour
{
    public GameObject turret;
    public Enemy_Master E_Master;
    public Enemy_AI E_AI;

    List<Transform> allTurrets = new List<Transform>();

    void OnEnable()
    {
        E_Master.EventCreateTurret += CreateNewTurret;
    }

    void OnDisable()
    {
        E_Master.EventCreateTurret -= CreateNewTurret;
    }

    // Start is called before the first frame update
    void Start()
    {
        int count = E_AI.Stats.turretCount;
        E_AI.Stats.turretCount = 0;

        for (int i = 0; i <= count; i++)
        {
            CreateNewTurret();
        }
    }

    public void CreateNewTurret()
    {
        Transform newTurret = Instantiate(turret, transform.position, Quaternion.identity, transform.GetChild(0)).transform;
        newTurret.localPosition = new Vector3(0f, 0.35f, 0f);
        //newTurret.GetChild(0).GetComponent<Gun_Rotation>().range = E_AI.Stats.range;
        allTurrets.Add(newTurret);

        float incrementAngle = 360 / allTurrets.Count;

        for (int i = 0; i < allTurrets.Count; i++)
        {
            allTurrets[i].rotation = Quaternion.identity;
            allTurrets[i].Rotate(new Vector3(0f, i * incrementAngle, 0f));
        }

        E_AI.Stats.turretCount++;
        E_Master.CallEventTurretCreated(newTurret);
    }
}
