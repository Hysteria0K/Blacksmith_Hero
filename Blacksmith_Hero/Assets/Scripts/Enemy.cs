using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Enemy_Speed;
    private bool Col_check;

    private float Max_Speed;


    public GameObject Game_Manager;
    public GameObject Status_Reader;
    public GameObject Player;
    public GameObject Enemy_Hp_Bar;
    public GameObject Enemy_Name;

    public int Hp;
    public int Atk;
    public string Name;

    public int origin_Hp;

    // Start is called before the first frame update
    void Start()
    {
        Load_Enemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp > 0) // Hp가 0보다 많을 때
        {
            // 이동
            if (Col_check == true)
            {
                Enemy_Speed -= 10.0f;
            }

            if (Enemy_Speed <= Max_Speed)
            {
                Col_check = false;
                Enemy_Speed = Max_Speed;
            }

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(Enemy_Speed, 0.0f);
        }

        else
        {

            Status_Reader.GetComponent<Status_Reader>().Stage += 1;
            CSVWriter.UpdateDataBase("Stage", Status_Reader.GetComponent<Status_Reader>().Stage.ToString());
            Game_Manager.GetComponent<Game_Manager>().Enemy_Defeat();

            this.gameObject.SetActive(false);

        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enemy_Speed = 500.0f;
            Col_check = true;
            Hp -= Player.GetComponent<Player>().Atk;
            Hp_Bar_Update();

            if (Game_Manager.GetComponent<Game_Manager>().Wall_check == false) Game_Manager.GetComponent<Game_Manager>().Wall_Set(true);
        }
    }
    public void Hp_Bar_Update()
    {
        Enemy_Hp_Bar.GetComponent<Image>().fillAmount = (float)Hp / origin_Hp;
        Enemy_Name.GetComponent<Text>().text = $"{Name} ({Hp} / {origin_Hp})";
    }

    public void Load_Enemy()
    {
        Col_check = true;

        Max_Speed = -400.0f;

        Enemy_Speed = Max_Speed;

        origin_Hp = Status_Reader.GetComponent<Status_Reader>().Enemy_Hp;

        Name = Status_Reader.GetComponent<Status_Reader>().Enemy_Name;

        Hp = origin_Hp;

        Atk = Status_Reader.GetComponent<Status_Reader>().Enemy_Atk;

        Enemy_Name.GetComponent<Text>().text = $"{Name} ({Hp} / {origin_Hp})";
    }
}
        