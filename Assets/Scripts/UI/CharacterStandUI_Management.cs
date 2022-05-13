using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStandUI_Management : MonoBehaviour
{
    MenuManager_Master MM_Master;

    public Game_Options GO;

    void OnEnable()
    {
        MM_Master = GameObject.Find("Managers").GetComponent<MenuManager_Master>();
        MM_Master.EventCharacterSelection += OnCharacterSelection;

        OnCharacterSelection(GO.selectedCharacterIndex);
    }

    void OnDisable()
    {
        MM_Master.EventCharacterSelection -= OnCharacterSelection;
    }

    void OnCharacterSelection(int index)
    {
        Destroy(transform.GetChild(0).GetChild(0).gameObject);
        
        Instantiate(GO.charactersInMenu[index], transform.GetChild(0));
        GO.selectedCharacterIndex = index;
    }
}
