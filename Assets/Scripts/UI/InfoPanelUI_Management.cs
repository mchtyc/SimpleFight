using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanelUI_Management : MonoBehaviour
{
    public GameManager_Master GM_Master;

    TextMeshProUGUI InfoText;
    Animator animator;

    void OnEnable()
    {
        GM_Master.EventShowInfo += OpenInfoPanel;

        InfoText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        GM_Master.EventShowInfo -= OpenInfoPanel;
    }

    void OpenInfoPanel(string info)
    {
        InfoText.text = info;
        animator.enabled = true;
        InfoText.gameObject.SetActive(true);
        animator.SetTrigger("OpenInfo");
    }

    public void CloseInfoPanel()
    {
        InfoText.text = "";
        InfoText.gameObject.SetActive(false);
        animator.enabled = false;
    }
}
