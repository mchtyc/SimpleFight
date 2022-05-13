using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Master : MonoBehaviour
{
    public delegate void EnemyEventGeneralHandler();
    public delegate void EnemyEventTurretHandler(Transform t);
    public delegate void EnemyEventHealthHandler(float currentHP, float maxHP);
    public delegate void EnemyEventXPHandler(int XP);
    public delegate void EnemyEventPowerHandler(int power);

    public event EnemyEventTurretHandler EventTurretCreated;
    public event EnemyEventHealthHandler EventHealthChanged;
    public event EnemyEventGeneralHandler EventOnDie;
    public event EnemyEventXPHandler EventXPChange;
    public event EnemyEventXPHandler EventXPIncrease;
    public event EnemyEventGeneralHandler EventLevelChanged;
    public event EnemyEventGeneralHandler EventCreateTurret;
    public event EnemyEventPowerHandler EventPowerIncrease;

    public void CallEventTurretCreated(Transform t)
    {
        if (EventTurretCreated != null)
        {
            EventTurretCreated(t);
        }
    }

    public void CallEventHealthChanged(float currentHP, float maxHP)
    {
        if (EventHealthChanged != null)
        {
            EventHealthChanged(currentHP, maxHP);
        }
    }

    public void CallEventOnDie()
    {
        if (EventOnDie != null)
        {
            EventOnDie();
        }
    }

    public void CallEventXPChange(int XP)
    {
        if (EventXPChange != null)
        {
            EventXPChange(XP);
        }
    }

    public void CallEventXPIncrease(int XP)
    {
        if (EventXPIncrease != null)
        {
            EventXPIncrease(XP);
        }
    }

    public void CallEventLevelChanged()
    {
        if (EventLevelChanged != null)
        {
            EventLevelChanged();
        }
    }

    public void CallEventCreateTurret()
    {
        if (EventCreateTurret != null)
        {
            EventCreateTurret();
        }
    }

    public void CallEventPowerIncrease(int power)
    {
        if (EventPowerIncrease != null)
        {
            EventPowerIncrease(power);
        }
    }
}
