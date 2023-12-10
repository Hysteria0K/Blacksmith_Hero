using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    public int Module1;
    public int Module2;
    public int Module3;

    public int Module1_Type;
    public int Module2_Type;
    public int Module3_Type;

    public int Module1_Min_Status;
    public int Module2_Min_Status;
    public int Module3_Min_Status;

    public int Module1_Max_Status;
    public int Module2_Max_Status;
    public int Module3_Max_Status;

    public int Module1_Status;
    public int Module2_Status;
    public int Module3_Status;

    private int Module1_Selected;
    private int Module2_Selected;
    private int Module3_Selected;

    public int Item_Code;

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
        if (Module_Data[0].ContainsKey("Max_Index")) int.TryParse(Module_Data[0]["Max_Index"].ToString(), out Module_Max_Index);

        Selected_Equip_Type = Random.Range(0, Equip_Max_Index); // 장비 결정

        if (Equip_Data[Selected_Equip_Type].ContainsKey("Index")) int.TryParse(Equip_Data[Selected_Equip_Type]["Index"].ToString(), out Equip_Type);
        if (Equip_Data[Selected_Equip_Type].ContainsKey("Main_Stat")) int.TryParse(Equip_Data[Selected_Equip_Type]["Main_Stat"].ToString(), out Equip_Main_Status_Type);
        if (Equip_Data[Selected_Equip_Type].ContainsKey("Min_Stat")) int.TryParse(Equip_Data[Selected_Equip_Type]["Min_Stat"].ToString(), out Equip_Min_Status);
        if (Equip_Data[Selected_Equip_Type].ContainsKey("Max_Stat")) int.TryParse(Equip_Data[Selected_Equip_Type]["Max_Stat"].ToString(), out Equip_Max_Status);

        Equip_Status = Random.Range(Equip_Min_Status, Equip_Max_Status + 1);

        Module1_Selected = Random.Range(0, Module_Max_Index);

        while(true)
        {
            Module2_Selected = Random.Range(0, Module_Max_Index);
            if (Module2_Selected != Module1_Selected) break;
        }

        while (true)
        {
            Module3_Selected = Random.Range(0, Module_Max_Index);
            if (Module3_Selected != Module1_Selected && Module3_Selected != Module2_Selected) break;
        }

        if (Module_Data[Module1_Selected].ContainsKey("Index")) int.TryParse(Module_Data[Module1_Selected]["Index"].ToString(), out Module1);
        if (Module_Data[Module1_Selected].ContainsKey("Type")) int.TryParse(Module_Data[Module1_Selected]["Type"].ToString(), out Module1_Type);
        if (Module_Data[Module1_Selected].ContainsKey("Min_Stat")) int.TryParse(Module_Data[Module1_Selected]["Min_Stat"].ToString(), out Module1_Min_Status);
        if (Module_Data[Module1_Selected].ContainsKey("Max_Stat")) int.TryParse(Module_Data[Module1_Selected]["Max_Stat"].ToString(), out Module1_Max_Status);

        if (Module_Data[Module2_Selected].ContainsKey("Index")) int.TryParse(Module_Data[Module2_Selected]["Index"].ToString(), out Module2);
        if (Module_Data[Module2_Selected].ContainsKey("Type")) int.TryParse(Module_Data[Module2_Selected]["Type"].ToString(), out Module2_Type);
        if (Module_Data[Module2_Selected].ContainsKey("Min_Stat")) int.TryParse(Module_Data[Module2_Selected]["Min_Stat"].ToString(), out Module2_Min_Status);
        if (Module_Data[Module2_Selected].ContainsKey("Max_Stat")) int.TryParse(Module_Data[Module2_Selected]["Max_Stat"].ToString(), out Module2_Max_Status);

        if (Module_Data[Module3_Selected].ContainsKey("Index")) int.TryParse(Module_Data[Module3_Selected]["Index"].ToString(), out Module3);
        if (Module_Data[Module3_Selected].ContainsKey("Type")) int.TryParse(Module_Data[Module3_Selected]["Type"].ToString(), out Module3_Type);
        if (Module_Data[Module3_Selected].ContainsKey("Min_Stat")) int.TryParse(Module_Data[Module3_Selected]["Min_Stat"].ToString(), out Module3_Min_Status);
        if (Module_Data[Module3_Selected].ContainsKey("Max_Stat")) int.TryParse(Module_Data[Module3_Selected]["Max_Stat"].ToString(), out Module3_Max_Status);

        Module1_Status = Random.Range(Module1_Min_Status, Module1_Max_Status + 1);
        Module2_Status = Random.Range(Module2_Min_Status, Module2_Max_Status + 1);
        Module3_Status = Random.Range(Module3_Min_Status, Module3_Max_Status + 1);
    }


}
