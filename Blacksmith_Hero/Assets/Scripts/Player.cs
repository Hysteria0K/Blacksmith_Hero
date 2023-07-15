using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private float Player_Speed;
    private float Jump_Speed;
    private bool Col_check;
    private bool Jump_check;

    private float Max_Speed;
    private float Max_Jump;

    public GameObject Game_Manager;
    public GameObject Status_Reader;
    public GameObject Enemy;
    public GameObject Player_Hp_Bar;
    public GameObject Player_Text;

    public int Hp;
    public int Atk;

    public int origin_Hp;
    // Start is called before the first frame update
    void Start()
    {
        Load_Player();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp > 0) // Hp가 0보다 많을 때
        {
            // 이동
            if (Col_check == true)
            {
                Player_Speed += 10.0f;
            }

            if (Player_Speed >= Max_Speed)
            {
                Col_check = false;
                Player_Speed = Max_Speed;
            }

            // 점프
            if (Jump_check == false)
            {
                Jump_Speed -= 10.0f;
            }

            if (Jump_Speed <= -2 * Max_Jump && Jump_check == false)
            {
                Jump_Speed = -2 * Max_Jump;
                Jump_check = true;
            }

            if (Jump_Speed <= Max_Jump && Jump_check == true)
            {
                Jump_Speed += 10.0f;
            }

            if (Jump_Speed > Max_Jump)
            {
                Jump_Speed = Max_Jump;
                Jump_check = false;
            }

            // 최종 이동
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(Player_Speed, Jump_Speed);
        }

        else // 전투 패배시
        {
            Status_Reader.GetComponent<Status_Reader>().Stage -= 1;
            CSVWriter.UpdateDataBase("Stage", Status_Reader.GetComponent<Status_Reader>().Stage.ToString());
            Game_Manager.GetComponent<Game_Manager>().Player_Defeat();

            this.gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Player_Speed = -500.0f; //-500.0f
            Col_check = true;

            Hp -= Enemy.GetComponent<Enemy>().Atk;
            Hp_Bar_Update();
        }
    }
    public void Hp_Bar_Update()
    {
        Player_Hp_Bar.GetComponent<Image>().fillAmount = (float)Hp / origin_Hp;
        Player_Text.GetComponent<Text>().text = $"플레이어 ({Hp} / {origin_Hp})";
    }

    public void Load_Player()
    {
        origin_Hp = Status_Reader.GetComponent<Status_Reader>().Player_Hp;

        Jump_Speed = 0.0f;

        Col_check = true;
        Jump_check = false;

        Max_Speed = 400.0f;
        Max_Jump = 100.0f;

        Player_Speed = Max_Speed;
        Hp = origin_Hp;
        Atk = Status_Reader.GetComponent<Status_Reader>().Player_Atk;

        Player_Text.GetComponent<Text>().text = $"플레이어 ({Hp} / {origin_Hp})";
    }

    private void Wall_Set()
    {

    }
}
