using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float Enemy_Speed;
    private bool Col_check;

    private float Max_Speed;
    // Start is called before the first frame update
    void Start()
    {
        Col_check = false;

        Max_Speed = -400.0f;

        Enemy_Speed = Max_Speed;
    }

    // Update is called once per frame
    void Update()
    {

        // 이동
        if (Col_check == true)
        {
            Enemy_Speed -= 1.0f;
        }

        if (Enemy_Speed <= Max_Speed)
        {
            Col_check = false;
            Enemy_Speed = Max_Speed;
        }

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Enemy_Speed, 0.0f);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("충돌");
            Enemy_Speed = 500.0f;
            Col_check = true;
        }
    }
}
        