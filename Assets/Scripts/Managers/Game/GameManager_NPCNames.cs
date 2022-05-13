using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
//using UnityEditor;

public class GameManager_NPCNames : MonoBehaviour
{
    public GameManager_Master GM_Master;

    NPCNames allNames = new NPCNames();
    string path, json;

    void OnEnable()
    {
        GM_Master.EventNPCName += CreateName;
    }

    void OnDisable()
    {
        GM_Master.EventNPCName -= CreateName;
    }

    void Start()
    {
        path = "Game Data/NPC_Names";

        json = Resources.Load<TextAsset>(path).text;

        allNames = JsonUtility.FromJson<NPCNames>(json);
        allNames.GetNames();
    }

    public void CreateName(Character_Stats stat)
    {
        string names = allNames.names[Random.Range(0, allNames.names.Length)];

        int startIndex = Random.Range(0, (1 + names.LastIndexOf(" ")));
        startIndex = names.IndexOf(" ", startIndex) + 1;

        int lastIndex = names.IndexOf(" ", startIndex);
        if (lastIndex == -1)
        {
            lastIndex = names.Length;
        }

        stat.charName = names.Substring((startIndex), lastIndex - startIndex);
    }


    // Bütün isimleri kaydetmek için bir seferliğine kullanılmış oldu...

    //void SavingJson()
    //{

    //    string json = JsonUtility.ToJson(allNames);
    //    string path = "Assets/Resources/Game Data/NPC_Names.json";

    //    using (FileStream fs = new FileStream(path, FileMode.Create))
    //    {
    //        using (StreamWriter writer = new StreamWriter(fs))
    //        {
    //            writer.Write(json);
    //        }
    //    }
    //    AssetDatabase.Refresh();
    //}
}
