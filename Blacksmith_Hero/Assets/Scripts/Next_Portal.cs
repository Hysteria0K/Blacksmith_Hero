using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Portal : MonoBehaviour
{
    public GameObject UI_Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UI_Manager.GetComponent<UI_Manager>().Fade();
        }
    }
}
