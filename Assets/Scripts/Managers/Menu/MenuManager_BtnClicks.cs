using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager_BtnClicks : MonoBehaviour
{
    public Game_Options G_Options;
    public Transform[] menuCanvases, mainBtns;

    Vector3 defaultMainBtnScale, pressedMainBtnScale;

    void Start()
    {
        defaultMainBtnScale = new Vector3(0.7f, 0.7f, 1f);
        pressedMainBtnScale = Vector3.one;
    }

    public void OnClickGameBtn(int index)
    {
        G_Options.gameMod = (GameModes)index;

        SceneManager.LoadScene((int)Scenes.Play);
    }

    public void OnClickMainBtn(int index)
    {
        foreach (Transform t in menuCanvases)
        {
            t.gameObject.SetActive(false);
        }

        menuCanvases[index].gameObject.SetActive(true);

        foreach (Transform t in mainBtns)
        {
            t.localScale = defaultMainBtnScale;
        }

        mainBtns[index].localScale = pressedMainBtnScale;
    }
}
