using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Upgrades
{
    List<Upgrade> allUpgrades = new List<Upgrade>();
    Upgrade[] randomUpgrades = new Upgrade[3];
    NPCIntelligence intelligence;
    Upgrade firstU, secondU, thirdU;
    float increaseRate = 1.2f;
    Enemy_Master E_Master;

    public NPC_Upgrades(NPCIntelligence i, Enemy_Master e)
    {
        intelligence = i;
        this.E_Master = e;

        //Önem sırasına göre listelendi
        allUpgrades.Add(new Upgrade(0));
        allUpgrades.Add(new Upgrade(3));
        allUpgrades.Add(new Upgrade(1));
        allUpgrades.Add(new Upgrade(8));
        allUpgrades.Add(new Upgrade(2));
        allUpgrades.Add(new Upgrade(6));
        allUpgrades.Add(new Upgrade(4));
        allUpgrades.Add(new Upgrade(7));
        allUpgrades.Add(new Upgrade(9));
        allUpgrades.Add(new Upgrade(5));
        allUpgrades.Add(new Upgrade(10));
    }

    void GenerateRandomUpgrades()
    {
        for (int i = 0; i < 3; i++)
        {
            randomUpgrades[i] = new Upgrade(Random.Range(0, 11));
        }
    }

    void IdentifyUpgrades()
    {
        foreach (Upgrade upgrade in allUpgrades)
        {
            foreach (Upgrade randomUpgrade in randomUpgrades)
            {
                if (upgrade.type == randomUpgrade.type)
                {
                    if (firstU == null)
                    {
                        firstU = upgrade;
                    }
                    else if (secondU == null)
                    {
                        secondU = upgrade;
                    }
                    else
                    {
                        thirdU = upgrade;
                    }
                }
            }
        }
    }

    Upgrade SelectUpgrade()
    {
        GenerateRandomUpgrades();
        IdentifyUpgrades();

        float rand = Random.Range(0f, 12f);
        NPCUpgradeChances Chances = new NPCUpgradeChances(intelligence);
        Upgrade selectedUpgrade;

        if (rand <= Chances.First())
        {
            selectedUpgrade = firstU;
        }
        else if (rand <= Chances.Second())
        {
            selectedUpgrade = secondU;
        }
        else if(rand <= Chances.Third())
        {
            selectedUpgrade = thirdU;
        }
        else
        {
            selectedUpgrade = null;
        }

        return selectedUpgrade;
    }

    public void Upgrade(Character_Stats C_Stats)
    {
        Upgrade up = SelectUpgrade();

        if (up != null)
        {
            up.count++;

            if (up.count >= up.totalCount)
            {
                allUpgrades.Remove(up);
            }

            if (up.type == UpgradeType.bulletSpeedIncrease)
            {
                C_Stats.bulletSpeed *= increaseRate;
            }
            else if (up.type == UpgradeType.critIncrease)
            {
                C_Stats.critDamageMultiplier *= increaseRate;
            }
            else if (up.type == UpgradeType.currentHPIncrease)
            {
                C_Stats.SetCurrentHP(C_Stats.currentHP *= increaseRate);
                E_Master.CallEventHealthChanged(C_Stats.currentHP, C_Stats.maxHP);
            }
            else if (up.type == UpgradeType.damageIncrease)
            {
                C_Stats.damage *= increaseRate;
            }
            else if (up.type == UpgradeType.defenseIncrease)
            {
                C_Stats.defense *= increaseRate;
            }
            else if (up.type == UpgradeType.fireRateIncrease)
            {
                C_Stats.fireRate *= increaseRate;
            }
            else if (up.type == UpgradeType.maxHPIncrease)
            {
                float diff = C_Stats.maxHP;
                C_Stats.maxHP *= increaseRate;
                diff = C_Stats.maxHP - diff;
                C_Stats.SetCurrentHP(C_Stats.currentHP + diff);
                E_Master.CallEventHealthChanged(C_Stats.currentHP, C_Stats.maxHP);
            }
            else if (up.type == UpgradeType.rangeIncrease)
            {
                C_Stats.range *= increaseRate;
            }
            else if (up.type == UpgradeType.recoveryRateIncrease)
            {
                C_Stats.recoveryRate *= increaseRate;
            }
            else if (up.type == UpgradeType.speedIncrease)
            {
                C_Stats.speed *= increaseRate;
            }
            else if (up.type == UpgradeType.turretCount)
            {
                E_Master.CallEventCreateTurret();
                C_Stats.speed /= increaseRate;
            }
        }
    }
}
