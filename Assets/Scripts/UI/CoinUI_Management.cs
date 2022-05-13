using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI_Management : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public Character_Stats P_Stats;
    GameManager_Master GM_Master;

    private void OnEnable()
    {
        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();
        GM_Master.EventCoinChanged += UpdateCoinText;
    }

    void OnDisable()
    {
        GM_Master.EventCoinChanged -= UpdateCoinText;
    }

    void UpdateCoinText()
    {
        coinText.text = P_Stats.coin.ToString();
    }
}
