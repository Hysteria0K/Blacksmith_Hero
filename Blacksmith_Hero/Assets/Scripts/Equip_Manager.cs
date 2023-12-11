using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Equip_Manager : MonoBehaviour
{
    public GameObject UI_Manager;
    public GameObject Status_Reader;
    public GameObject Equip_Create_UI_Manager;
    public GameObject Equip_Created;

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

    public string Item_Code; // 장비타입(1), 장비스탯타입(2), 장비스탯(5), 모듈1(2), 모듈1값(4), 모듈2(2), 모듈2값(4), 모듈3(2), 모듈3값(4)

    public int Create_Mode = 1; //idle
    public int Require_Token;

    // Start is called before the first frame update
    void Start()
    {
        Set_Mode();
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

        // 아이템 코드 생성

        StringBuilder Get_Code = new StringBuilder();

        Get_Code.Append($"{Equip_Type:D1}");
        Get_Code.Append($"{Equip_Main_Status_Type:D2}");
        Get_Code.Append($"{Equip_Status:D5}");
        Get_Code.Append($"{Module1:D2}");
        Get_Code.Append($"{Module1_Status:D4}");
        Get_Code.Append($"{Module2:D2}");
        Get_Code.Append($"{Module2_Status:D4}");
        Get_Code.Append($"{Module3:D2}");
        Get_Code.Append($"{Module3_Status:D4}");

        Item_Code = Get_Code.ToString();

        Status_Reader.GetComponent<Status_Reader>().Token -= Require_Token;
        CSVWriter.UpdateDataBase("Token", Status_Reader.GetComponent<Status_Reader>().Token.ToString());
        UI_Manager.GetComponent<UI_Manager>().UI_Update();
        Equip_Create_UI_Manager.GetComponent<Equip_Create_UI_Manager>().Equip_Created_UI_Update();
    }

    public void Set_Mode()
    {
        if (Create_Mode == 1) Require_Token = 1;


        UI_Manager.GetComponent<UI_Manager>().UI_Update();
    }

    public void Equip_Wear()
    {
        switch(Equip_Type)
        {
            case 1:
                {
                    CSVWriter.UpdateDataBase("Helmet", Item_Code);
                    break;
                }
            case 2:
                {
                    CSVWriter.UpdateDataBase("Chest", Item_Code);
                    break;
                }
            case 3:
                {
                    CSVWriter.UpdateDataBase("Pants", Item_Code);
                    break;
                }
            case 4:
                {
                    CSVWriter.UpdateDataBase("Gloves", Item_Code);
                    break;
                }
            case 5:
                {
                    CSVWriter.UpdateDataBase("Boots", Item_Code);
                    break;
                }
            default: break;
        }

        Equip_Created.SetActive(false);

    }

}
