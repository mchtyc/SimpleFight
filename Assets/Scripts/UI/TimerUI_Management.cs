using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI_Management : MonoBehaviour
{
    public Image fillImage;
    public TextMeshProUGUI timeText;

    GameManager_Master GM_Master;
    float totalTime, remainingTime;
    int secondCounter  = 180;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = remainingTime = 181f;
        GM_Master = GameObject.Find(GameManager_References.instance.managersName).GetComponent<GameManager_Master>();
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime > 0)
        {
            if (remainingTime <= secondCounter)
            {
                fillImage.fillAmount = remainingTime / totalTime;
                timeText.text = timeFormat();
                secondCounter--;
            }
        }
        else
        {
            GM_Master.CallEventTimeIsUp();
        }
        
    }

    string timeFormat()
    {
        int second = (int)Mathf.Floor(remainingTime % 60f);
        int h = (int)remainingTime / 60;
        string s;

        if (second < 10)
        {
            s = "0" + second;
        }
        else
        {
            s = second.ToString();
        }

        if (h == 0)
        {
            return s;
        }
        else
        {
            return "" + h + ":" + s;
        }
    }
    
}
