using UnityEngine;

public class GameManager_Master : MonoBehaviour
{
    public delegate void GameManagerEventPlayerXPHandler(int XP);
    public delegate void GameManagerEventGeneralHandler();
    public delegate void GameManagerEventNPCDeath(Enemy_AI ai);
    public delegate void GameManagerEventNPCHandler(Character_Stats stat);
    public delegate void GameManagerEventRank(string[] ranks, int[] scores, int playerRank);
    public delegate void GameManagerEventPlayerPowerHandler(int power);
    public delegate void GameManagerEventInfoPanelHandler(string info);

    public event GameManagerEventPlayerXPHandler EventPlayerShotEnemy;
    public event GameManagerEventGeneralHandler EventPlayerCreated;

    public event GameManagerEventGeneralHandler EventCoinChanged;
    public event GameManagerEventGeneralHandler EventLevelChanged;
    public event GameManagerEventGeneralHandler EventUpgradeReady;

    public event GameManagerEventGeneralHandler EventCreateTurret;

    public event GameManagerEventNPCDeath EventNPCDeath;

    public event GameManagerEventNPCHandler EventNPCName;
    public event GameManagerEventRank EventTopFiveUpdate;

    public event GameManagerEventPlayerPowerHandler EventPowerIncrease;

    public event GameManagerEventGeneralHandler EventPlayerDied;
    public event GameManagerEventGeneralHandler EventStartAgain;
    public event GameManagerEventGeneralHandler EventHealthChanged;
    public event GameManagerEventGeneralHandler EventMenuOpened;
    public event GameManagerEventGeneralHandler EventMenuClosed;

    public event GameManagerEventInfoPanelHandler EventShowInfo;

    public event GameManagerEventGeneralHandler EventNewUpgradeChosen;

    public event GameManagerEventGeneralHandler EventTimeIsUp;

    public void CallEventPlayerCreated()
    {
        if(EventPlayerCreated != null)
        {
            EventPlayerCreated();
        }
    }

    public void CallEventPlayerShotEnemy(int XP)
    {
        if (EventPlayerShotEnemy != null)
        {
            EventPlayerShotEnemy(XP);
        }
    }

    public void CallEventCoinChanged()
    {
        if (EventCoinChanged != null)
        {
            EventCoinChanged();
        }
    }

    public void CallEventLevelChanged()
    {
        if (EventLevelChanged != null)
        {
            EventLevelChanged();
        }
    }

    public void CallEventUpgradeReady()
    {
        if (EventUpgradeReady != null)
        {
            EventUpgradeReady();
        }
    }

    public void CallEventCreateTurret()
    {
        if (EventCreateTurret != null)
        {
            EventCreateTurret();
        }
    }

    public void CallEventNPCDeath(Enemy_AI ai)
    {
        if (EventNPCDeath != null)
        {
            EventNPCDeath(ai);
        }
    }

    public void CallEventNPCName(Character_Stats stat)
    {
        if (EventNPCName != null)
        {
            EventNPCName(stat);
        }
    }

    public void CallEventTopFiveUpdate(string[] ranks, int[] scores, int playerRank)
    {
        if (EventTopFiveUpdate != null)
        {
            EventTopFiveUpdate(ranks, scores, playerRank);
        }
    }

    public void CallEventPowerIncrease(int power)
    {
        if (EventPowerIncrease != null)
        {
            EventPowerIncrease(power);
        }
    }

    public void CallEventPlayerDied()
    {
        if (EventPlayerDied != null)
        {
            EventPlayerDied();
        }
    }

    public void CallEventStartAgain()
    {
        if (EventStartAgain != null)
        {
            EventStartAgain();
        }
    }

    public void CallEventHealthChanged()
    {
        if (EventHealthChanged != null)
        {
            EventHealthChanged();
        }
    }

    public void CallEventMenuOpened()
    {
        if (EventMenuOpened != null)
        {
            EventMenuOpened();
        }
    }

    public void CallEventMenuClosed()
    {
        if (EventMenuClosed != null)
        {
            EventMenuClosed();
        }
    }

    public void CallEventShowInfo(string info) 
    {
        if (EventShowInfo != null)
        {
            EventShowInfo(info);
        }
    }

    public void CallEventNewUpgradeChosen()
    {
        if (EventNewUpgradeChosen != null)
        {
            EventNewUpgradeChosen();
        }
    }

    public void CallEventTimeIsUp()
    {
        if (EventTimeIsUp != null)
        {
            EventTimeIsUp();
        }
    }
}
