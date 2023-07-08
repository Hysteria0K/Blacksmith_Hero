using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.Search;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public GameObject Status_Reader;
    public GameObject UI_Manager;

    public bool Enemy_Defeat;
    // Start is called before the first frame update

    void Start()
    {
        Enemy_Defeat = false;
    }

    // Update is called once per frame
    void Update()
    {
        //적이 쓰러졌을때
        if (Enemy_Defeat == true)
        {
            Status_Reader.GetComponent<Status_Reader>().Gold += Status_Reader.GetComponent<Status_Reader>().Enemy_Gold;
            UI_Manager.GetComponent<UI_Manager>().UI_Call = true;
            Enemy_Defeat = false;
        }
    }
}
