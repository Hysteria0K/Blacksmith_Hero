using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Player_Speed;
    private float Col_Speed;
    public bool Col_check;
    // Start is called before the first frame update
    void Start()
    {
        Col_Speed = -500.0f;
        Col_check = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Col_check == true)
        {
            Col_Speed += 1.0f;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(Col_Speed, 0.0f);
        }

        else
        {
            this.transform.Translate(Vector2.right * Player_Speed);
        }

        if (Col_Speed >= 0.0f)
        {
            Col_check = false;
            Col_Speed = 0.0f;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Ãæµ¹");
            Col_Speed = -500.0f;
            Col_check = true;
        }
    }


}
