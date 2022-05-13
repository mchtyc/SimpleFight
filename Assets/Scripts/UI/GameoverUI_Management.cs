using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverUI_Management : MonoBehaviour
{
    public GameManager_Master GM_Master;

    void OnEnable()
    {
        GM_Master = GameObject.Find(GameManager_References.instance.managersName).GetComponent<GameManager_Master>();
    }

    public void OnClickStartAgainBtn()
    {
        GM_Master.CallEventStartAgain();
    }
}
