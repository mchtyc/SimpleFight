using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Turret : MonoBehaviour
{
    public GameObject turret;
    public Player_Master P_Master;
    public Character_Stats P_Stats;

    GameManager_Master GM_Master;
    List<Transform> allTurrets = new List<Transform>();

    void OnEnable()
    {
        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();
        GM_Master.EventCreateTurret += CreateNewTurret;
    }

    void OnDisable()
    {
        GM_Master.EventCreateTurret -= CreateNewTurret;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateNewTurret();
    }

    public void CreateNewTurret()
    {
        if (P_Stats.turretCount < P_Stats.maxTurretCount)
        {
            Transform newTurret = Instantiate(turret, transform.position, Quaternion.identity, transform.GetChild(0)).transform;
            newTurret.localPosition = new Vector3(0f, 0.35f, 0f);
            //newTurret.GetChild(0).GetComponent<Gun_Rotation>().range = P_Stats.range;
            allTurrets.Add(newTurret);
            
            float incrementAngle = 360 / (allTurrets.Count);

            for (int i = 0; i < allTurrets.Count; i++)
            {
                allTurrets[i].rotation = Quaternion.identity;
                allTurrets[i].Rotate(new Vector3(0f, i * incrementAngle, 0f));
            }
            P_Stats.turretCount++;
            P_Master.CallEventTurretCreated(newTurret); 
        }
    }
}
