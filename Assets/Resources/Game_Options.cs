using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game_Options", menuName = "Game Options", order = 52)]
public class Game_Options : ScriptableObject
{
    public GameModes gameMod;

    public bool[] openedCharacters;

    public GameObject[] charactersInMenu, characters;

    public int selectedCharacterIndex;

}
