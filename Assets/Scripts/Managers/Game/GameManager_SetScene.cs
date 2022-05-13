using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager_SetScene : MonoBehaviour
{
    public Game_Options GO;
    public GameManager_Master GM_Master;
    public GameObject XP, Coin, AbilityUI, CoinUI, UpgradeUI, LeadershipUI, TimerUI;
    public GameObject[] EnemyPrefabs;
    public Transform[] spawnPoints;
    public Transform playerSpawnPoint, enemyContainer, collectableContainer, mainCanvas;
    public int maxXP, maxCoin;
    public Material[] bigMats, smallMats;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        if (GO.gameMod == GameModes.Endless)
        {
            Instantiate(LeadershipUI, mainCanvas);
            Invoke("InsPlayer", 1f);
            StartCoroutine(settingEnemies(9, true));
        }
        else if(GO.gameMod == GameModes.Survival)
        {
            Instantiate(LeadershipUI, mainCanvas);
            Invoke("InsPlayer", 1f);
            StartCoroutine(settingEnemies(9, true));
        }
        else if (GO.gameMod == GameModes.Time)
        {
            Instantiate(LeadershipUI, mainCanvas);
            Instantiate(TimerUI, mainCanvas);
            Invoke("InsPlayer", 1f);
            StartCoroutine(settingEnemies(9, true));
        }
        else if (GO.gameMod == GameModes.Chapters)
        {

        }
        else if (GO.gameMod == GameModes.Tournament)
        {

        }
        else if (GO.gameMod == GameModes.Gift)
        {

        }
        else if (GO.gameMod == GameModes.Match)
        {

        }

        //EnvirementSetup();
        UISetup();

        //Invoke("InsPlayer", 1f);
        //InvokeRepeating("InsEnemiesInTransfrms", 2f, 30f);
        //InvokeRepeating("InsXP", 3f, 8f);
        //InvokeRepeating("InsCoin", 3f, 12f);

        UpgradesSetup();
    }

    void OnEnable()
    {
        GM_Master.EventNPCDeath += NPCDeath;
    }

    void OnDisable()
    {
        GM_Master.EventNPCDeath -= NPCDeath;
    }

    void EnvirementSetup()
    {
        //Dağlar taşlar duvarlar tuzaklar
    }

    void UISetup()
    {
        Instantiate(AbilityUI, mainCanvas);
        Instantiate(CoinUI, mainCanvas);
        Instantiate(UpgradeUI, mainCanvas);
    }

    void UpgradesSetup()
    {
        for (int i = 0; i < 11; i++)
        {
            GameManager_References.instance.Player_Upgrades.Add(new Upgrade(i));
        }
    }

    void InsPlayer()
    {
        Transform playerT = Instantiate(GO.characters[GO.selectedCharacterIndex], playerSpawnPoint.position, Quaternion.identity).transform;
        GameManager_References.player = playerT;
        GM_Master.CallEventPlayerCreated();
    }

    void InsEnemiesInTransforms()
    {
        foreach(Transform t in spawnPoints)
        {
            Instantiate(EnemyPrefabs[0], t.position, Quaternion.identity, enemyContainer);
        }
    }

    void InsEnemy(bool isStart)
    {
        // Daha sonra oyun alanı bölümlere ayrılıp her bölümde eşit sayıda oyuncu oluşturulabilir
        GameObject NPC = Instantiate(EnemyPrefabs[Random.Range(0,EnemyPrefabs.Length)], new Vector3(Random.Range(-48f, 48f), 1.5f, Random.Range(-23f, 23f)), Quaternion.identity, enemyContainer);
        // Şimdilik NPCler rasgele zekada olacak ama daha sonra her zekadan en az bir tane olacak şekilde ayarlanabilir.

        NPC.GetComponentInChildren<Renderer>().materials = ChangeMaterials(NPC.GetComponentInChildren<Renderer>().materials);
        
        Enemy_AI ai = NPC.GetComponent<Enemy_AI>();
        SetNPCIntelligence(ai);
        ai.isStart = isStart; // NPC default statları randommı yoksa default mu alacağına buna göre karar verecek
        GameManager_References.instance.NPC_AIs.Add(ai);
    }

    Material[] ChangeMaterials(Material[] targetMats)
    {
        Material[] tempMats;
        tempMats = targetMats;
        tempMats[1] = smallMats[Random.Range(0, smallMats.Length)];
        tempMats[2] = bigMats[Random.Range(0, bigMats.Length)];
        //tempMats[1] = smallMats[0];
        //tempMats[2] = bigMats[0];

        return tempMats;
    }

    void SetNPCIntelligence(Enemy_AI AI)
    {
        float rand = Random.Range(0f, 10f);

        if (rand < 2f)
        {
            AI.NPC_Intelligence = NPCIntelligence.Idiot;
        }
        else if (rand < 4f)
        {
            AI.NPC_Intelligence = NPCIntelligence.Silly;
        }
        else if (rand < 7f)
        {
            AI.NPC_Intelligence = NPCIntelligence.Average;
        }
        else if (rand < 8f)
        {
            AI.NPC_Intelligence = NPCIntelligence.Bright;
        }
        else if (rand < 9f)
        {
            AI.NPC_Intelligence = NPCIntelligence.Genius;
        }
        else if (rand < 10f)
        {
            AI.NPC_Intelligence = NPCIntelligence.Unpredictable;
        }
    }

    IEnumerator settingEnemies(int count, bool isStart)
    {
        if (isStart)
        {
            yield return new WaitForSeconds(1.1f);  // Start için bekleme zamanı
        }
        
        for (int i = 0; i < count; i++)
        {
            InsEnemy(isStart);

            yield return new WaitForEndOfFrame();
        }
    }

    void InsXP()
    {
        if (GameManager_References.instance.xpCount < maxXP)
        {
            for (int i = 0; i < UnityEngine.Random.Range(2, 5); i++)
            {
                Instantiate(XP, new Vector3(UnityEngine.Random.Range(-50f, 50f), 1f, UnityEngine.Random.Range(-25f, 25f)), Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f), collectableContainer);
                GameManager_References.instance.xpCount++;
            }
        }
    }

    void InsCoin()
    {
        if (GameManager_References.instance.coinCount < maxCoin)
        {
            for (int i = 0; i < UnityEngine.Random.Range(2, 5); i++)
            {
                Instantiate(Coin, new Vector3(UnityEngine.Random.Range(-50f, 50f), 1f, UnityEngine.Random.Range(-25f, 25f)), Quaternion.Euler(90f, UnityEngine.Random.Range(0f, 360f), 0f), collectableContainer);
                GameManager_References.instance.coinCount++;
            }
        }
    }

    void NPCDeath(Enemy_AI ai)
    {
        RemoveAI(ai);

        if (GO.gameMod == GameModes.Endless)
        {
            StartCoroutine(settingEnemies(1, false));
        }
    }

    void RemoveAI(Enemy_AI ai)
    {
        foreach (Enemy_AI e in GameManager_References.instance.NPC_AIs)
        {
            if (e == ai)
            {
                GameManager_References.instance.NPC_AIs.Remove(e);
                break;
            }
        }
    }
}
