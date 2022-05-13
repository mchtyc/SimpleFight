using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_UI : MonoBehaviour
{
    public GameManager_Master GM_Master;
    public Transform mainCanvas;
    public GameObject GOUI;

    GameObject UI;

    void OnEnable()
    {
        GM_Master.EventPlayerDied += OpenGOPanel;
        GM_Master.EventTimeIsUp += OpenGOPanel;
        GM_Master.EventStartAgain += StartAgain;
    }

    void OnDisable()
    {
        GM_Master.EventPlayerDied -= OpenGOPanel;
        GM_Master.EventTimeIsUp -= OpenGOPanel;
        GM_Master.EventStartAgain -= StartAgain;
    }

    void OpenGOPanel()
    {
        if(UI == null)
        {
            UI = Instantiate(GOUI, mainCanvas);
        }
    }

    public void StartAgain()
    {
        UI = null;
        SceneManager.LoadScene((int)Scenes.Play);
        // Şimdilik sadece Scene değiştiği için destroy etmeye gerek yok...
        // İlerde yeniden oyuna devam ete eklendiğinde yapılacak
    }
}
