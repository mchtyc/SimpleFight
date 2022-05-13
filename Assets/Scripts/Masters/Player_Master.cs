using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Master : MonoBehaviour
{
    public delegate void PlayerEventGeneralHandler();
    public delegate void PlayerEventTurretHandler(Transform t);

    public event PlayerEventTurretHandler EventTurretCreated;

    public event PlayerEventGeneralHandler EventHealthChanged;

    public event PlayerEventGeneralHandler EventXPChanged;

    public void CallEventTurretCreated(Transform t)
    {
        if (EventTurretCreated != null)
        {
            EventTurretCreated(t);
        }
    }

    public void CallEventHealthChanged()
    {
        if (EventHealthChanged != null)
        {
            EventHealthChanged();
        }
    }

    public void CallEventXPChanged()
    {
        if (EventXPChanged != null)
        {
            EventXPChanged();
        }
    }
}
