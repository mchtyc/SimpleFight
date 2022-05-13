using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType 
{   
    turretCount, damageIncrease, speedIncrease, defenseIncrease, 
    rangeIncrease, critIncrease, fireRateIncrease, currentHPIncrease, 
    maxHPIncrease, bulletSpeedIncrease, recoveryRateIncrease
}

public class Upgrade
{
    public Upgrade(int typeCode)
    {
        totalCount = 8;
        count = 0;
        type = (UpgradeType)typeCode;

        switch (typeCode)
        {
            case 0:
                description = "Turret";
                break;
            case 1:
                description = "Damage";
                break;
            case 2:
                description = "Speed";
                break;
            case 3:
                description = "Defense";
                break;
            case 4:
                description = "Range";
                break;
            case 5:
                description = "Critic Damage";
                break;
            case 6:
                description = "Fire Rate";
                break;
            case 7:
                description = "Current HP";
                break;
            case 8:
                description = "Max HP";
                break;
            case 9:
                description = "Bullet Speed";
                break;
            case 10:
                description = "Recovery Rate";
                break;
        }
    }

    public UpgradeType type;
    
    public string Description 
    {        
        get { return description; } 
    }

    public int count { get; set; }
    public int totalCount { get; }

    string description;
}
