using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterMenu : MonoBehaviour
{
    public MenuManager_Master MM_Master;
    public Game_Options GO;
    public Sprite onImage, offImage;
    public int index;

    void OnEnable()
    {
        if (GO.openedCharacters[index])
        {
            gameObject.GetComponent<Image>().sprite = onImage;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = offImage;
            GetComponent<Button>().interactable = false;
        }
    }

    public void OnClickCharacter()
    {
        MM_Master.CallEventCharacterSelection(index);
    }
}
