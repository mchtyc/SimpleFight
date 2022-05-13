using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat_Change : MonoBehaviour
{
    public Material[] newMats;

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        Material[] tempMats = rend.materials;
        tempMats[Random.Range(0, tempMats.Length)] = newMats[Random.Range(0, newMats.Length)];
        rend.materials = tempMats;
        Debug.Log("working");
    }
}
