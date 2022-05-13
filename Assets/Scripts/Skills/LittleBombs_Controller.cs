using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBombs_Controller : MonoBehaviour
{
    public GameObject[] bombs;

    public bool fire;

    Vector3[] poss;

    // Start is called before the first frame update
    void Start()
    {
        poss = new Vector3[bombs.Length];

        for (int i = 0; i < poss.Length; i++)
        {
            poss[i] = bombs[i].transform.position;
        }
    }

    private void Update()
    {
        //Test
        if (fire)
        {
            StartCoroutine(ToggleBombs());
            fire = false;
        }

    }

    IEnumerator ToggleBombs()
    {
        foreach (GameObject bomb in bombs)
        {
            bomb.SetActive(true);
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < poss.Length; i++)
        {
            bombs[i].transform.position = poss[i];
            bombs[i].SetActive(false);
        }
    }
}
