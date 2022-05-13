using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI_Management : MonoBehaviour
{
    public Character_Stats P_Stats;
    public Image fillImage;
    public Button abilityButton;
    public GameObject animationImage;
    public Animator animator;

    Player_Master P_Master;
    GameManager_Master GM_Master;
    bool isAbilityReady = false;
    int abilityCount = 0;

    void OnEnable()
    {
        abilityButton.interactable = false;

        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();
        
        GM_Master.EventPlayerCreated += getPlayer;
        GM_Master.EventLevelChanged += NewAbilityReady;
        GM_Master.EventNewUpgradeChosen += CheckAbilityCount;

        fillImage.fillAmount = P_Stats.XP / P_Stats.maxXP;
    }

    void OnDisable()
    {
        GM_Master.EventPlayerCreated -= getPlayer;
        P_Master.EventXPChanged -= ExperienceUpdate; 
        GM_Master.EventLevelChanged -= NewAbilityReady;
        GM_Master.EventNewUpgradeChosen -= CheckAbilityCount;
    }

    public void getPlayer()
    {
        P_Master = GameManager_References.player.gameObject.GetComponent<Player_Master>();
        P_Master.EventXPChanged += ExperienceUpdate;
    }

    public void ExperienceUpdate()
    {
        if (!isAbilityReady)
        {
            float fAmount = (float)P_Stats.XP / (float)P_Stats.maxXP;
            StopAllCoroutines();
            StartCoroutine(fill(fAmount));
        }
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
    }

    void NewAbilityReady()
    {
        if (!isAbilityReady)
        {
            StopAllCoroutines();
            fillImage.fillAmount = 1f;
            abilityButton.interactable = true;
            animationImage.SetActive(true);
            isAbilityReady = true;
        }
        else
        {
            abilityCount++;
        }
    }

    public void OnClickAbilityButton()
    {
        GM_Master.CallEventUpgradeReady();
        isAbilityReady = false;
        abilityButton.interactable = false;
        animationImage.SetActive(false);
        GM_Master.CallEventMenuOpened();
    }

    void CheckAbilityCount()
    {
        ExperienceUpdate();

        if (abilityCount > 0)
        {
            NewAbilityReady();
            abilityCount--;
        }
    }
}
