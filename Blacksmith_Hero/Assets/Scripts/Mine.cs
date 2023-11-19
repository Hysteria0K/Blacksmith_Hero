using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mine : MonoBehaviour
{
    public GameObject Status_Reader;
    public GameObject UI_Manager;
    // Start is called before the first frame update
    void Start()
    {
        Mining();
    }

    // Update is called once per frame
    void Update()
    {
        if (Status_Reader.GetComponent<Status_Reader>().Mining_Time >= Status_Reader.GetComponent<Status_Reader>().Mine_Time)
        {
            Mine_Get_Token();
            UI_Manager.GetComponent<UI_Manager>().UI_Update();
        }
    }


    public void Mining()
    {
        StartCoroutine(MiningCoroutine());

    }
    IEnumerator MiningCoroutine()
    {
        while (true)
        {

            Status_Reader.GetComponent<Status_Reader>().Mining_Time += 1;
            CSVWriter.UpdateDataBase("Mining_Time", Status_Reader.GetComponent<Status_Reader>().Mining_Time.ToString());
            yield return new WaitForSeconds(1.0f);

        }

    }

    public void Mine_Get_Token()
    {
        Status_Reader.GetComponent<Status_Reader>().Token += Status_Reader.GetComponent<Status_Reader>().Get_Token;
        CSVWriter.UpdateDataBase("Token", Status_Reader.GetComponent<Status_Reader>().Token.ToString());

        Status_Reader.GetComponent<Status_Reader>().Mining_Time = 0;
        CSVWriter.UpdateDataBase("Mining_Time", Status_Reader.GetComponent<Status_Reader>().Mining_Time.ToString());
    }

}