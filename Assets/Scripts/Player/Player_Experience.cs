using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Experience : MonoBehaviour
{
    public Character_Stats P_Stats;
    public Player_Master P_Master;
    
    GameManager_Master GM_Master;

    void OnEnable()
    {
        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();

        P_Master.EventXPChanged += CalculatePlayerLevel;
        GM_Master.EventPlayerShotEnemy += GainExperience;
        GM_Master.EventPowerIncrease += OnPowerIncrease;
    }

    void OnDisable()
    {
        P_Master.EventXPChanged -= CalculatePlayerLevel;
        GM_Master.EventPlayerShotEnemy -= GainExperience;
        GM_Master.EventPowerIncrease -= OnPowerIncrease;
    }

    public void CalculatePlayerLevel()
    {
        if (P_Stats.XP >= P_Stats.maxXP)
        {
            P_Stats.level++;
            GM_Master.CallEventLevelChanged();

            P_Stats.XP -= P_Stats.maxXP;
            P_Master.CallEventXPChanged();
            P_Stats.maxXP *= 2;
        }
    }

    public void GainExperience(int experience)
    {
        P_Stats.XP += experience;
        P_Master.CallEventXPChanged();
    }

    void OnPowerIncrease(int power)
    {
        P_Stats.power += power;
    }
}
