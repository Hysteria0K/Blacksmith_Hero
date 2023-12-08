using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mine : MonoBehaviour
{
    public GameObject Status_Reader;
    public GameObject UI_Manager;
    public GameObject Mine_Gauge_Bar;
    public GameObject Mine_Text;

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

        Mine_Bar_Update();
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

    public void Mine_Bar_Update()
    {
        Mine_Gauge_Bar.GetComponent<Image>().fillAmount = (float)Status_Reader.GetComponent<Status_Reader>().Mining_Time / (float)Status_Reader.GetComponent<Status_Reader>().Mine_Time;
        Mine_Text.GetComponent<Text>().text = $"({Status_Reader.GetComponent<Status_Reader>().Mining_Time} / {Status_Reader.GetComponent<Status_Reader>().Mine_Time})";
    }

    public void Mine_Upgrade()
    {
        Status_Reader.GetComponent<Status_Reader>().Gold -= Status_Reader.GetComponent<Status_Reader>().Mine_Upgrade_Cost;
        CSVWriter.UpdateDataBase("Gold", Status_Reader.GetComponent<Status_Reader>().Gold.ToString());

        Status_Reader.GetComponent<Status_Reader>().Mine_Level += 1;
        CSVWriter.UpdateDataBase("Mine_Level", Status_Reader.GetComponent<Status_Reader>().Mine_Level.ToString());

        Status_Reader.GetComponent<Status_Reader>().Read_Status();
        UI_Manager.GetComponent<UI_Manager>().UI_Update();

    }

}