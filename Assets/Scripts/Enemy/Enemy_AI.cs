using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    //[HideInInspector]
    public Character_Stats Stats;
    public Enemy_Master E_Master;
    public Game_Options G_Options;

    [HideInInspector] public NPCIntelligence NPC_Intelligence;
    [HideInInspector] public bool isStart; // If game is started just yet.

    NPC_Upgrades upgrades;
    GameManager_Master GM_Master;

    void OnEnable()
    {
        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();

        Stats = ScriptableObject.CreateInstance("Character_Stats") as Character_Stats;

        E_Master.EventXPChange += OnXPChange;
        E_Master.EventXPIncrease += OnXPIncrease;
        E_Master.EventLevelChanged += Upgrade;
        E_Master.EventOnDie += NotifyGameMasterOnDeath;
        E_Master.EventPowerIncrease += OnPowerIncrease;
    }

    void OnDisable()
    {
        E_Master.EventXPChange -= OnXPChange;
        E_Master.EventXPIncrease -= OnXPIncrease;
        E_Master.EventLevelChanged -= Upgrade;
        E_Master.EventOnDie -= NotifyGameMasterOnDeath;
        E_Master.EventPowerIncrease -= OnPowerIncrease;
    }

    void Start()
    {
        // NPC_Intelligence ile ilgili işler en erken burada çünkü anca ulaşılıyor.
        upgrades = new NPC_Upgrades(NPC_Intelligence, E_Master);
        GM_Master.CallEventNPCName(Stats);

        if (G_Options.gameMod == GameModes.Endless && isStart)
        {
            Stats.SetRandomStatsForNPCs();
        }
        else if (G_Options.gameMod == GameModes.Endless && !isStart)
        {
            Stats.SetDefaultStatsForNPCs();
        }
        else if (G_Options.gameMod == GameModes.Survival)
        {
            Stats.SetDefaultStatsForNPCs();
        }
        else if (G_Options.gameMod == GameModes.Time)
        {
            Stats.SetDefaultStatsForNPCs();
        }
        else
        {
            Debug.Log("Choose stat mode to start this gameplay mode...!");
        }
    }

    void OnXPChange(int XP)
    {
        // Direk XP değişimi için eventi neden yazdım hatırlayamadım :)
        // Hatırlayınca yazarım...
    }

    void OnXPIncrease(int XP)
    {
        Stats.XP += XP;

        LevelControl();
    }

    void LevelControl()
    {
        if (Stats.XP >= Stats.maxXP)
        {
            Stats.XP -= Stats.maxXP;
            Stats.level++;
            Stats.maxXP *= 2; // Şimdilik 2 ile çarpılacak
            E_Master.CallEventLevelChanged();

            if (Stats.XP >= Stats.maxXP)
            {
                LevelControl();
            }
        }
    }

    void Upgrade()
    {
        upgrades.Upgrade(Stats);
    }

    void NotifyGameMasterOnDeath()
    {
        GM_Master.CallEventNPCDeath(this);
    }

    void OnPowerIncrease(int power)
    {
        Stats.power += power;
    }
}
