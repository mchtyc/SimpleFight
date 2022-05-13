using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI_Management : MonoBehaviour
{
    public Character_Stats P_Stats;
    public TextMeshProUGUI button1, button2, button3;
    public Button btn1, btn2, btn3;
    public Animator animator;

    GameObject BackImage;
    GameManager_Master GM_Master;
    Upgrade[] newUpgrades = new Upgrade[3];
    float increaseRate = 1.2f;
    int[] newUpgradeIndexes = new int[3];

    void OnEnable()
    {
        BackImage = transform.GetChild(0).gameObject;

        GM_Master = GameObject.Find("Managers").GetComponent<GameManager_Master>();
        
        GM_Master.EventUpgradeReady += OpenUpgradeUI;
    }

    void OnDisable()
    {
        GM_Master.EventUpgradeReady -= OpenUpgradeUI;
    }

    void OpenUpgradeUI()
    {
        getNewUpgrades();

        //set button texts
        button1.text = newUpgrades[0].Description;
        button2.text = newUpgrades[1].Description;
        button3.text = newUpgrades[2].Description;

        btn1.interactable = true;
        btn2.interactable = true;
        btn3.interactable = true;

        BackImage.SetActive(true);
        animator.SetTrigger("Open");

        //set UI pos
        //StartCoroutine(openUpgradeUI());
    }

    void getNewUpgrades()
    {
        for (int i = 0; i < newUpgrades.Length; i++)
        {
            int rand = Random.Range(0, GameManager_References.instance.Player_Upgrades.Count);
            Upgrade upgrade = GameManager_References.instance.Player_Upgrades[rand];

            newUpgrades[i] = upgrade;
            newUpgradeIndexes[i] = (int)upgrade.type;
        }
    }

    //IEnumerator openUpgradeUI()
    //{
    //    RectTransform rect = GetComponent<RectTransform>();

    //    while (rect.anchorMin.x > 0.8f)
    //    {
    //        rect.anchorMin -= new Vector2(Time.deltaTime * panelSpeed, 0f);
    //        panelSpeed += 0.1f;
    //        yield return null;

    //        btn1.interactable = true;
    //        btn2.interactable = true;
    //        btn3.interactable = true;
    //        panelSpeed = 0.2f;
    //    }
    //}

    //IEnumerator closeUpgradeUI()
    //{
    //    //yield return new WaitForSeconds(0.5f);

    //    RectTransform rect = GetComponent<RectTransform>();

    //    while (rect.anchorMin.x < 1f)
    //    {
    //        rect.anchorMin += new Vector2(Time.deltaTime * panelSpeed, 0f);
    //        panelSpeed += 0.1f;
    //        yield return null;
    //    }

    //    panelSpeed = 0.2f;
    //    upgradeCount--;
    //    upgradeOnProgress = false;

    //    if(upgradeCount > 0)
    //    {
    //        OpenUpgradeUI();
    //    }
    //}

    public void UpgradeButtonClick(int index)
    {
        btn1.interactable = false;
        btn2.interactable = false;
        btn3.interactable = false;

        int i = (int)newUpgrades[index].type;

        newUpgrades[index].count++;

        if (newUpgrades[index].count == newUpgrades[index].totalCount)
        {
            GameManager_References.instance.Player_Upgrades.RemoveAt(newUpgradeIndexes[index]);
        }

        switch (i)
        {
            case 0:
                GM_Master.CallEventCreateTurret();
                P_Stats.speed /= increaseRate;  //Turret sayısı arttıkça hız azalacak
                break;
            case 1:
                P_Stats.damage *= increaseRate;
                break;
            case 2:
                P_Stats.speed *= increaseRate;
                break;
            case 3:
                P_Stats.defense *= increaseRate;
                break;
            case 4:
                P_Stats.range *= increaseRate;
                break;
            case 5:
                P_Stats.critDamageMultiplier *= increaseRate;
                break;
            case 6:
                P_Stats.fireRate *= increaseRate;
                break;
            case 7:
                P_Stats.SetCurrentHP(P_Stats.currentHP * increaseRate);
                GM_Master.CallEventHealthChanged();
                break;
            case 8:
                float diff = P_Stats.maxHP;
                P_Stats.maxHP *= increaseRate;
                diff = P_Stats.maxHP - diff;
                P_Stats.SetCurrentHP(P_Stats.currentHP + diff);
                GM_Master.CallEventHealthChanged();
                break;
            case 9:
                P_Stats.bulletSpeed *= increaseRate;
                break;
            case 10:
                P_Stats.recoveryRate *= increaseRate;
                break;
        }

        // Burası infoPanele aktarılıp oradan çağırılacak
        GM_Master.CallEventShowInfo(newUpgrades[index].Description + " increased.");

        animator.SetTrigger("Close");
        //StartCoroutine(closeUpgradeUI());
    }

    public void CloseUpgradeMenu()
    {
        BackImage.SetActive(false);
        GM_Master.CallEventMenuClosed();
        GM_Master.CallEventNewUpgradeChosen();
    }
}
