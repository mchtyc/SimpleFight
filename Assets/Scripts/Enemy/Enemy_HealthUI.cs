using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HealthUI : MonoBehaviour
{
    public GameObject HealthPre;
    public Transform healthPoint;
    public Enemy_Master E_Master;

    Transform ui;
    Transform healthCanvas;
    Image healthSlider, healthSlider2;

    void OnEnable()
    {
        healthCanvas = GameObject.FindGameObjectWithTag(GameManager_References.instance.healthCanvasTag).transform;

        ui = Instantiate(HealthPre, healthCanvas).transform;
        healthSlider = ui.GetChild(1).GetComponentInChildren<Image>();
        healthSlider2 = ui.GetChild(0).GetComponentInChildren<Image>();

        E_Master.EventHealthChanged += updateHealthUI;
        E_Master.EventOnDie += Die;
    }

    void OnDisable()
    {
        E_Master.EventHealthChanged -= updateHealthUI;
        E_Master.EventOnDie -= Die;
    }

    public void updateHealthUI(float currentHP, float maxHP)
    {
        float fAmount = currentHP / maxHP;

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

        healthSlider2.fillAmount = targetAmount;
    }

    void LateUpdate()
    {
        ui.position = healthPoint.position;
    }

    void Die()
    {
        Destroy(ui.gameObject);
    }
}
