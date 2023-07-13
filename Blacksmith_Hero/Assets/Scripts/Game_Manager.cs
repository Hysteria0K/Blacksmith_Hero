using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public GameObject Status_Reader;
    public GameObject UI_Manager;

    public GameObject Right_Wall;

    public GameObject Player;
    public GameObject Enemy;

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
        CSVWriter.UpdateDataBase("Gold", Status_Reader.GetComponent<Status_Reader>().Gold.ToString());
        UI_Manager.GetComponent<UI_Manager>().UI_Update();
        Right_Wall.SetActive(false);
    }

    public void ResetStage()
    {
        Player.transform.position = new Vector3(-124, 988, 1);
        Enemy.transform.position = new Vector3(642, 1018, 1);

        Player.GetComponent<Player>().Hp = Player.GetComponent<Player>().origin_Hp;
        Enemy.GetComponent<Enemy>().Hp = Enemy.GetComponent<Enemy>().origin_Hp;

        Player.GetComponent<Player>().Hp_Bar_Update();
        Enemy.GetComponent<Enemy>().Hp_Bar_Update();

        Enemy.GetComponent<Enemy>().Enemy_Speed = 80.0f;
        Enemy.SetActive(true);
    }
}
