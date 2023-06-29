using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private float Player_Speed;
    private float Jump_Speed;
    private bool Col_check;
    private bool Jump_check;

    private float Max_Speed;
    private float Max_Jump;

    public GameObject Status_Reader;
    public GameObject Enemy;

    public int Hp;
    public int Atk;

    // Start is called before the first frame update
    void Start()
    {

        Jump_Speed = 0.0f;

        Col_check = false;
        Jump_check = false;

        Max_Speed = 400.0f;
        Max_Jump = 100.0f;

        Player_Speed = Max_Speed;
        Hp = Status_Reader.GetComponent<Status_Reader>().Player_Hp;
        Atk = Status_Reader.GetComponent<Status_Reader>().Player_Atk;
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
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Player_Speed = -500.0f;
            Col_check = true;

            Hp -= Enemy.GetComponent<Enemy>().Atk;
        }
    }
}
