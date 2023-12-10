using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Equip_Manager : MonoBehaviour
{
    private int Equip_Max_Index;
    private int Module_Max_Index;

    public int Equip_Type; // equip_datatable -> Index
    public int Equip_Main_Status_Type; // equip_datatable -> Main_Stat
    public int Equip_Min_Status; // equip_datatable -> Min_Stat
    public int Equip_Max_Status; // equip_datatable -> Max_Stat
    public int Equip_Status; // 실제 값

    private int Selected_Equip_Type; // 선택된 장비 타입

    // Start is called before the first frame update
    void Start()
    {
        Create_Equip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Create_Equip()
    {
        List<Dictionary<string, object>> Equip_Data = CSVReader.Read("equip_datatable.csv");
        List<Dictionary<string, object>> Module_Data = CSVReader.Read("module_datatable.csv");

        if (Equip_Data[0].ContainsKey("Max_Index")) int.TryParse(Equip_Data[0]["Max_Index"].ToString(), out Equip_Max_Index);

        Selected_Equip_Type = Random.Range(0, Equip_Max_Index); // 장비 결정

        if (Equip_Data[Selected_Equip_Type].ContainsKey("Index")) int.TryParse(Equip_Data[Selected_Equip_Type]["Index"].ToString(), out Equip_Type);
        if (Equip_Data[Selected_Equip_Type].ContainsKey("Main_Stat")) int.TryParse(Equip_Data[Selected_Equip_Type]["Main_Stat"].ToString(), out Equip_Main_Status_Type);
        if (Equip_Data[Selected_Equip_Type].ContainsKey("Min_Stat")) int.TryParse(Equip_Data[Selected_Equip_Type]["Min_Stat"].ToString(), out Equip_Min_Status);
        if (Equip_Data[Selected_Equip_Type].ContainsKey("Max_Stat")) int.TryParse(Equip_Data[Selected_Equip_Type]["Max_Stat"].ToString(), out Equip_Max_Status);

        Equip_Status = Random.Range(Equip_Min_Status, Equip_Max_Status + 1);


    }
}
