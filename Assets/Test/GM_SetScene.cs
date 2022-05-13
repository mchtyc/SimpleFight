using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_SetScene : GM
{
    public GameObject mat_Wall;
    public Material changeMat;
    public Texture changeSprite;
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("change");
        //mat = mat_Wall.GetComponent<Renderer>().sharedMaterial;
        //string[] names = mat.GetTexturePropertyNames();

        //foreach (string s in names)
        //{
        //    Debug.Log(s);
        //}

        //mat.SetTexture("_MainTex", changeSprite);

        changeMat.SetTexture("_MainTex", changeSprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator change()
    {

        yield return new WaitForSeconds(5f);
        Screen.orientation = ScreenOrientation.Portrait;
        Debug.Log("changed");

        
    }

    public void GoRun()
    {
        Debug.Log("GM worked");
    }
}
