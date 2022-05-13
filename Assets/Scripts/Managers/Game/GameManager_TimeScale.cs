using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_TimeScale : MonoBehaviour
{
    public GameManager_Master GM_Master;
    readonly float stopScale = 0f, fullScale = 1f; 

    void OnEnable()
    {
        GM_Master.EventTimeIsUp += PauseGame;
        GM_Master.EventPlayerDied += PauseGame;
        GM_Master.EventMenuOpened += PauseGame;
        GM_Master.EventStartAgain += RestartGame;
        GM_Master.EventMenuClosed += RestartGame;
    }

    void OnDisable()
    {
        GM_Master.EventTimeIsUp -= PauseGame;
        GM_Master.EventPlayerDied -= PauseGame;
        GM_Master.EventMenuOpened -= PauseGame;
        GM_Master.EventStartAgain -= RestartGame;
        GM_Master.EventMenuClosed -= RestartGame;
    }

    void PauseGame()
    {
        Time.timeScale = stopScale;
    }

    void RestartGame()
    {
        Time.timeScale = fullScale; // Scale yavaş yavaş arttırılabilir
    }
}
