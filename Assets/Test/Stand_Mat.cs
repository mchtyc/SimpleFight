using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand_Mat : MonoBehaviour
{
    public MeshRenderer standTopRend, standBottomRend;
    public Material mainMat, otherMat;
    int counter;


    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        InvokeRepeating("ChangeMats", 1f, 0.2f);
    }

    void ChangeMats()
    {
        List<Material> tempMats = new List<Material>();

        foreach (Material mat in standTopRend.materials)
        {
            tempMats.Add(mat);
        }

        foreach (Material mat in standBottomRend.materials)
        {
            tempMats.Add(mat);
        }

        if (counter == 0)
        {
            tempMats[tempMats.Count - 1] = mainMat;
        }
        else
        {
            tempMats[counter - 1] = mainMat;
        }

        tempMats[counter] = otherMat;

        Material[] tempTop = new Material[4];
        Material[] tempBottom = new Material[7];

        tempMats.CopyTo(0, tempTop, 0, 4);
        tempMats.CopyTo(4, tempBottom, 0, 7);

        standTopRend.materials = tempTop;
        standBottomRend.materials = tempBottom;

        counter++;
        if(counter == tempMats.Count)
        {
            counter = 0;
        }
    }
}
