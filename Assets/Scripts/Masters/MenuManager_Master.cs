using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager_Master : MonoBehaviour
{
    public delegate void MenuManagerEventCharacterSelectionHandler(int index);

    public event MenuManagerEventCharacterSelectionHandler EventCharacterSelection;

    public void CallEventCharacterSelection(int index)
    {
        if (EventCharacterSelection != null)
        {
            EventCharacterSelection(index);
        }
    }
}
