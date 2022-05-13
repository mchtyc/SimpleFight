using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collect : MonoBehaviour
{
    public Character_Stats P_Stats;
    GameManager_Master GM_Master;
    public Player_Master P_Master;

    void OnEnable()
    {
        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager_References.instance.collectablesTag))
        {
            Collactable collectable = other.GetComponent<Collactable>();

            if (collectable.type == CollactableTypes.XP)
            {
                P_Stats.XP += collectable.Value;
                P_Master.CallEventXPChanged();
            }
            else if(collectable.type == CollactableTypes.Coin)
            {
                P_Stats.coin += collectable.Value;
                GM_Master.CallEventCoinChanged();
            }

            Destroy(other.gameObject);
        }
    }
}
