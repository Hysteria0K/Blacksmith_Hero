using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.Search;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public GameObject Status_Reader;
    public GameObject UI_Manager;

    public GameObject Right_Wall;

    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void Player_Defeat()
    {
        //
    }
    public void Enemy_Defeat()
    {
        Status_Reader.GetComponent<Status_Reader>().Gold += Status_Reader.GetComponent<Status_Reader>().Enemy_Gold;
        UI_Manager.GetComponent<UI_Manager>().UI_Update();
        Right_Wall.SetActive(false);
    }
}
