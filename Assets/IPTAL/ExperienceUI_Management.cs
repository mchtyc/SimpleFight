using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceUI_Management : MonoBehaviour
{
    public Character_Stats P_Stats;
    public TextMeshProUGUI levelText1, levelText2;
    public Image fillImage;

    Player_Master P_Master;
    GameManager_Master GM_Master;

    void OnEnable()
    {
        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();
        GM_Master.EventPlayerCreated += getPlayer;
    }

    void OnDisable()
    {
        GM_Master.EventPlayerCreated -= getPlayer;
        GM_Master.EventLevelChanged -= LevelUpdate;
        P_Master.EventXPChanged -= ExperienceUpdate;
    }

    void Start()
    {
        LevelUpdate();
        fillImage.fillAmount = P_Stats.XP / P_Stats.maxXP;
    }

    public void getPlayer()
    {
        P_Master = GameManager_References.player.gameObject.GetComponent<Player_Master>();
        GM_Master.EventLevelChanged += LevelUpdate;
        P_Master.EventXPChanged += ExperienceUpdate;
    }

    public void LevelUpdate()
    {
        levelText1.text = P_Stats.level.ToString();
        levelText2.text = (P_Stats.level + 1).ToString();
    }

    public void ExperienceUpdate()
    {
        float fAmount = (float)P_Stats.XP / (float)P_Stats.maxXP;
        StartCoroutine(fill(fAmount));
    }

    IEnumerator fill(float fAmount)
    {
        float t = 0f;
        float duration = 1f;

        while (t <= duration)
        {
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, fAmount, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        fillImage.fillAmount = fAmount;
    }
}
