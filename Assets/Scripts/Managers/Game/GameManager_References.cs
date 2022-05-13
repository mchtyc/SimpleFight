using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollactableTypes { XP, Coin }

public enum GameModes { Endless, Survival, Time, Chapters, Tournament, Gift, Match }

public enum Scenes { Menu, Play }

public enum GameProgress { SettingScene, CountDown, Playing }

public enum NPCIntelligence { Idiot, Silly, Average, Bright, Genius, Unpredictable }

public class GameManager_References : MonoBehaviour
{
    public static GameManager_References instance;
    public static Transform player;

    public GameManager_Master GM_Master;
    public string bulletContainerTag, healthCanvasTag, playerTag, enemyTag, bulletTag, envirementTag, collectablesTag,
        XPName, mainCanvasName, managersName, flameThrowerTag, littleBladesTag, circleBladeTag, woodenTag, cannonTag,
        volcanoTag, swampTag;
    
    [HideInInspector]
    public List<Upgrade> Player_Upgrades = new List<Upgrade>();
    
    [HideInInspector]
    public int xpCount = 0, coinCount = 0;

    [HideInInspector]
    public List<Enemy_AI> NPC_AIs;

    public GameObject[] BulletPrefabs;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
