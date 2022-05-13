using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeadershipUI_Management : MonoBehaviour
{
    public TextMeshProUGUI[] texts;

    GameManager_Master GM_Master;
    Color playerColor = Color.green, normalColor = Color.white;

    void OnEnable()
    {
        GM_Master = GameObject.Find(GameManager_References.instance.managersName).GetComponent<GameManager_Master>();

        GM_Master.EventTopFiveUpdate += BoardUpdate;
    }

    void OnDisable()
    {
        GM_Master.EventTopFiveUpdate -= BoardUpdate;
    }

    void BoardUpdate(string[] ranks, int[] scores, int playerRank)
    {
        for (int i = 0; i < texts.Length; i++)  // Renklendirme
        {
            texts[i].color = normalColor;

            if (playerRank <= 5)
            {
                if (i == playerRank - 1)
                {
                    texts[i].color = playerColor;
                }
            }
            else
            {
                if (i == 4)
                {
                    texts[i].color = playerColor;
                }
            }
            
        }

        for (int i = 0; i < texts.Length; i++)
        {
            if (ranks[i] == null)
            {
                texts[i].text = "";
            }
            else if (playerRank > texts.Length && i == texts.Length - 1)
            {
                texts[i].text = playerRank + ". " + ranks[i] + "  " + scores[i].ToString();
            }
            else
            {
                texts[i].text = (i + 1) + ". " + ranks[i] + "  " + scores[i].ToString();
            }
        }
    }
}
