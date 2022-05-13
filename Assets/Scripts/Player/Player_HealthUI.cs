using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_HealthUI : MonoBehaviour
{
    public GameObject HealthPre;
    public Transform healthPoint;
    public Player_Master P_Master;
    public Character_Stats P_Stats;

    GameManager_Master GM_Master;
    Transform ui;
    Transform healthCanvas;
    Image healthSlider, healthSlider2;

    void OnEnable()
    {
        healthCanvas = GameObject.FindGameObjectWithTag(GameManager_References.instance.healthCanvasTag).transform;
        GM_Master = GameObject.Find(GameManager_References.instance.managersName).GetComponent<GameManager_Master>();

        ui = Instantiate(HealthPre, healthCanvas).transform;
        healthSlider = ui.GetChild(1).GetComponentInChildren<Image>();
        healthSlider2 = ui.GetChild(0).GetComponentInChildren<Image>();
                
        P_Master.EventHealthChanged += updateHealthUI;
        GM_Master.EventHealthChanged += updateHealthUI;
    }

    void OnDisable()
    {
        P_Master.EventHealthChanged -= updateHealthUI;
        GM_Master.EventHealthChanged -= updateHealthUI;
    }

    public void updateHealthUI()
    {
        float fAmount = P_Stats.currentHP / P_Stats.maxHP;

        healthSlider.fillAmount = fAmount;
        StartCoroutine(fill(fAmount));
    }

    IEnumerator fill(float newFillAmount)
    {
        float t = 0f;
        float duration = 1f;
        float targetAmount = healthSlider.fillAmount;

        while (t <= duration)
        {
            healthSlider2.fillAmount = Mathf.Lerp(healthSlider2.fillAmount, newFillAmount, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        healthSlider2.fillAmount = newFillAmount;
    }

    void LateUpdate()
    {
        ui.position = healthPoint.position;
    }
}
