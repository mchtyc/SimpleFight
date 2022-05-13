using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano_Controller : MonoBehaviour
{
    public int ID;  //Bu skilli kullanan character verecek

    float damage = 50f, frequency = 1f;

    List<Enemy_Health> NPCToDamage = new List<Enemy_Health>();
    List<float> NPCEnterTime = new List<float>();

    Player_Health PH;
    float playerEnterTime;
    
    // Update is called once per frame
    void Update()
    {
        if (PH != null)
        {
            if (Time.time - playerEnterTime >= frequency)
            {
                PH.TakeDamage(damage);
                playerEnterTime = Time.time;
            }
        }

        for(int i = 0; i < NPCToDamage.Count; i++)
        {
            if (NPCToDamage[i] == null)
            {
                NPCEnterTime.RemoveAt(i);
                NPCToDamage.RemoveAt(i);
                i--;
                continue;
            }

            if (Time.time - NPCEnterTime[i] >= frequency)
            {
                NPCToDamage[i].TakeDamage(damage);
                NPCEnterTime[i] = Time.time;
            }
        }
    }

    //  Daha sonra volcano skiilini kullanan charactere damage vermemesi için ID alınacak...

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetInstanceID() == ID) return;

        if (other.CompareTag(GameManager_References.instance.enemyTag))
        {
            Enemy_Health EH = other.gameObject.GetComponent<Enemy_Health>();
            EH.TakeDamage(damage);
            NPCToDamage.Add(EH);
            NPCEnterTime.Add(Time.time);
        }
        else if (other.CompareTag(GameManager_References.instance.playerTag))
        {
            PH = other.gameObject.GetComponent<Player_Health>();
            PH.TakeDamage(damage);
            playerEnterTime = Time.time;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameManager_References.instance.enemyTag))
        {
            Enemy_Health EH = other.gameObject.GetComponent<Enemy_Health>();
            NPCEnterTime.RemoveAt(NPCToDamage.IndexOf(EH));
            NPCToDamage.Remove(EH);
        }
        else if (other.CompareTag(GameManager_References.instance.playerTag))
        {
            PH = null;
            playerEnterTime = 0f;
        }
    }
}
