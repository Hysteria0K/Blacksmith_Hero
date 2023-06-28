using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {

        Jump_Speed = 0.0f;

        Col_check = false;
        Jump_check = false;

        Max_Speed = 400.0f;
        Max_Jump = 100.0f;

        Player_Speed = Max_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        // 이동
        if (Col_check == true)
        {
            Player_Speed += 1.0f;
        }

        if (Player_Speed >= Max_Speed)
        {
            Col_check = false;
            Player_Speed = Max_Speed;
        }

        // 점프
        if (Jump_check == false)
        {
            Jump_Speed -= 1.0f;
        }

        if (Jump_Speed <= -2 * Max_Jump && Jump_check == false)
        {
            Jump_Speed = -2 * Max_Jump;
            Jump_check = true;
        }

        if (Jump_Speed <= Max_Jump && Jump_check == true)
        {
            Jump_Speed += 1.0f;
        }

        if (Jump_Speed > Max_Jump)
        {
            Jump_Speed = Max_Jump;
            Jump_check = false;
        }

        // 최종 이동
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Player_Speed, Jump_Speed);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("충돌");
            Player_Speed = -500.0f;
            Col_check = true;
        }
    }


}
