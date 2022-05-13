using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_SetCharacterStats : MonoBehaviour
{
    public Character_Stats P_Stats;

    // Start is called before the first frame update
    void Awake()
    {
        P_Stats.coin = 0;
        P_Stats.currentHP = P_Stats.maxHP = 400f;
        P_Stats.level = 1;
        P_Stats.turretCount = 0;
        P_Stats.XP = 0;
        P_Stats.maxXP = 100;
        P_Stats.bulletSpeed = 15f;
        P_Stats.critDamageMultiplier = 3f;
        P_Stats.damage = 15f;
        P_Stats.defense = 3f;
        P_Stats.fireRate = 1f;
        P_Stats.range = 10f;
        P_Stats.recoveryRate = 0.2f;
        P_Stats.speed = 5f;
        P_Stats.power = 0;

    }
}
