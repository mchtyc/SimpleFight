using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Leadership : MonoBehaviour
{
    public Character_Stats P_Stats;
    public GameManager_Master GM_Master;

    string[] topFive;
    int[] scores;
    int playerRank;

    void OnEnable()
    {
        GM_Master.EventNPCDeath += Calculate;
    }

    void OnDisable()
    {
        GM_Master.EventNPCDeath -= Calculate;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CalculateTopFive());
    }

    IEnumerator CalculateTopFive()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            Calculate();

            yield return new WaitForSeconds(1f);
        }
    }

    void Calculate(Enemy_AI dummy)    // NPC ölünce çalışan kod
    {
        if (GameManager_References.instance.NPC_AIs != null)
        {
            playerRank = 1;
            List<Enemy_AI> AIs = new List<Enemy_AI>();

            foreach (Enemy_AI ai in GameManager_References.instance.NPC_AIs)
            {
                if (ai != null)
                {
                    AIs.Add(ai);
                }
            }

            AIs.Sort((p1, p2) => p2.Stats.power.CompareTo(p1.Stats.power));

            topFive = new string[5];
            scores = new int[5];

            foreach (Enemy_AI ai in AIs)
            {
                if (ai.Stats.power <= P_Stats.power)
                {
                    break;
                }
                playerRank++;
            }

            for (int i = 0; i < topFive.Length; i++)
            {
                if (i < AIs.Count + 1)
                {
                    if (playerRank <= 5)
                    {
                        if (i < playerRank - 1)
                        {
                            topFive[i] = AIs[i].Stats.charName;
                            scores[i] = AIs[i].Stats.power;
                        }
                        else if (i == playerRank - 1)
                        {
                            topFive[i] = P_Stats.charName;
                            scores[i] = P_Stats.power;
                        }
                        else if (i > playerRank - 1)
                        {
                            topFive[i] = AIs[i - 1].Stats.charName;
                            scores[i] = AIs[i - 1].Stats.power;
                        }
                    }
                    else
                    {
                        if (i < 4)
                        {
                            topFive[i] = AIs[i].Stats.charName;
                            scores[i] = AIs[i].Stats.power;
                        }
                        else
                        {
                            topFive[i] = P_Stats.charName;
                            scores[i] = P_Stats.power;
                        }
                    }
                }
            }
        }

        GM_Master.CallEventTopFiveUpdate(topFive, scores, playerRank);

        topFive = null;
        scores = null;
        playerRank = 0;
    }

    void Calculate()    // Her saniye çalışan kod
    {
        if (GameManager_References.instance.NPC_AIs != null)
        {
            playerRank = 1;
            List<Enemy_AI> AIs = new List<Enemy_AI>();

            foreach (Enemy_AI ai in GameManager_References.instance.NPC_AIs)
            {
                if (ai != null)
                {
                    AIs.Add(ai);
                }
            }

            AIs.Sort((p1, p2) => p2.Stats.power.CompareTo(p1.Stats.power));

            topFive = new string[5];
            scores = new int[5];

            foreach (Enemy_AI ai in AIs)
            {
                if (ai.Stats.power <= P_Stats.power)
                {
                    break;
                }
                playerRank++;
            }

            for (int i = 0; i < topFive.Length; i++)
            {
                if (i < AIs.Count + 1)
                {
                    if (playerRank <= 5)
                    {
                        if (i < playerRank - 1)
                        {
                            topFive[i] = AIs[i].Stats.charName;
                            scores[i] = AIs[i].Stats.power;
                        }
                        else if (i == playerRank - 1)
                        {
                            topFive[i] = P_Stats.charName;
                            scores[i] = P_Stats.power;
                        }
                        else if (i > playerRank - 1)
                        {
                            topFive[i] = AIs[i - 1].Stats.charName;
                            scores[i] = AIs[i - 1].Stats.power;
                        }
                    }
                    else
                    {
                        if (i < 4)
                        {
                            topFive[i] = AIs[i].Stats.charName;
                            scores[i] = AIs[i].Stats.power;
                        }
                        else
                        {
                            topFive[i] = P_Stats.charName;
                            scores[i] = P_Stats.power;
                        }
                    } 
                }
            }
        }

        GM_Master.CallEventTopFiveUpdate(topFive, scores, playerRank);
        
        topFive = null;
        scores = null;
        playerRank = 0;
    }
}
