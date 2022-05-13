using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_Stats", menuName = "Create Stat", order = 51)]
public class Character_Stats : ScriptableObject
{
    public string charName;
    public int turretCount, maxTurretCount, level, XP, maxXP, coin, power;
    public float speed, damage, defense, range, critDamageMultiplier, critDamageRate, fireRate, currentHP, maxHP;
    public float recoveryRepeatTime, recoveryRate, timePastBeforeRecovery, bulletSpeed;

    public void SetCurrentHP(float hp)
    {
        currentHP = hp;

        if (currentHP > maxHP) 
        {
            currentHP = maxHP;
        }
    }

    public void SetDefaultStatsForNPCs()
    {
        turretCount = 0;
        maxTurretCount = 8;
        level = 1;
        XP = 0;
        maxXP = 200;
        speed = 2f;
        damage = 15f;
        defense = 3f;
        range = 10f;
        fireRate = 1f;
        currentHP = maxHP = 400f;
        bulletSpeed = 15f;
        power = 0;
    }

    public void SetRandomStatsForNPCs()
    {
        turretCount = Random.Range(0, 2);
        maxTurretCount = 8;
        level = 1;
        XP = 0;
        maxXP = 200;
        speed = Random.Range(1f, 5f);
        damage = Random.Range(10f, 20f);
        defense = Random.Range(2f, 5f);
        range = Random.Range(10f, 15f);
        fireRate = Random.Range(1f, 2f);
        currentHP = maxHP = Random.Range(300f, 600f);
        bulletSpeed = Random.Range(10f, 20f);
        power = 0;
    }
}
