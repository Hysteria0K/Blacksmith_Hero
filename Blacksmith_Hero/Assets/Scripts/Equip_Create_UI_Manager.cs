using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Equip_Create_UI_Manager : MonoBehaviour
{
    public GameObject Equip_Manager;

    public GameObject Equip_Created;

    public GameObject Equip_Type;
    public GameObject Equip_Main_Status;
    public GameObject Equip_Module1;
    public GameObject Equip_Module2;
    public GameObject Equip_Module3;

    public string Equip_Type_Text;
    public string Equip_Main_Status_Text;
    public string Equip_Module1_Text;
    public string Equip_Module2_Text;
    public string Equip_Module3_Text;


    // Start is called before the first frame update
    void Start()
    {
        Equip_Created.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip_Created_UI_Update()
    {
        Equip_Created.SetActive(true);
        Equip_Type_Update();
        Equip_Main_Status_Update();
        Equip_Module_Update();
    }

    private void Equip_Type_Update()
    {
        switch(Equip_Manager.GetComponent<Equip_Manager>().Equip_Type)
        {
            case 1:
                {
                    Equip_Type_Text = "헬멧";
                    break;
                }
            case 2:
                {
                    Equip_Type_Text = "상의";
                    break;
                }
            case 3:
                {
                    Equip_Type_Text = "하의";
                    break;
                }
            case 4:
                {
                    Equip_Type_Text = "장갑";
                    break;
                }
            case 5:
                {
                    Equip_Type_Text = "신발";
                    break;
                }
            default: break;
        }

        Equip_Type.GetComponent<Text>().text = Equip_Type_Text;
    }

    private void Equip_Main_Status_Update()
    {
        StringBuilder str = new StringBuilder();

        switch (Equip_Manager.GetComponent<Equip_Manager>().Equip_Main_Status_Type)
        { 
            case 1:
                {
                    str.Append("HP");
                    break;
                }
            case 2:
                {
                    str.Append("HP%");
                    break;
                }
            case 3:
                {
                    str.Append("ATK");
                    break;
                }
            case 4:
                {
                    str.Append("ATK%");
                    break;
                }
            case 5:
                {
                    str.Append("DEF");
                    break;
                }
            case 6:
                {
                    str.Append("DEF%");
                    break;
                }
            default: break;
        }

        str.Append(" - ");
        str.Append($"{Equip_Manager.GetComponent<Equip_Manager>().Equip_Status}");
        str.Append($" (Max: {Equip_Manager.GetComponent<Equip_Manager>().Equip_Max_Status})");

        Equip_Main_Status_Text = str.ToString();
        Equip_Main_Status.GetComponent<Text>().text = Equip_Main_Status_Text;
    }

    private void Equip_Module_Update()
    {
        string Module_str;
        int Module_switch = 0;
        int Module_status = 0;
        int Module_Max = 0;

        for(int i = 1; i <= 3; i++)
        {
            switch (i)
            {
                case 1:
                    {
                        Module_str = Equip_Module1_Text;
                        Module_switch = Equip_Manager.GetComponent<Equip_Manager>().Module1;
                        Module_status = Equip_Manager.GetComponent<Equip_Manager>().Module1_Status;
                        Module_Max = Equip_Manager.GetComponent<Equip_Manager>().Module1_Max_Status;
                        break;
                    }
                case 2:
                    {
                        Module_str = Equip_Module2_Text;
                        Module_switch = Equip_Manager.GetComponent<Equip_Manager>().Module2;
                        Module_status = Equip_Manager.GetComponent<Equip_Manager>().Module2_Status;
                        Module_Max = Equip_Manager.GetComponent<Equip_Manager>().Module2_Max_Status;
                        break;
                    }
                case 3:
                    {
                        Module_str = Equip_Module3_Text;
                        Module_switch = Equip_Manager.GetComponent<Equip_Manager>().Module3;
                        Module_status = Equip_Manager.GetComponent<Equip_Manager>().Module3_Status;
                        Module_Max = Equip_Manager.GetComponent<Equip_Manager>().Module3_Max_Status;
                        break;
                    }
                default: break;
            }

            StringBuilder str = new StringBuilder();

            switch (Module_switch)
            {
                case 1:
                    {
                        str.Append("[방어형] HP");
                        break;
                    }
                case 2:
                    {
                        str.Append("[방어형] HP%");
                        break;
                    }
                case 3:
                    {
                        str.Append("[공격형] ATK");
                        break;
                    }
                case 4:
                    {
                        str.Append("[공격형] ATK%");
                        break;
                    }
                case 5:
                    {
                        str.Append("[방어형] DEF");
                        break;
                    }
                case 6:
                    {
                        str.Append("[방어형] DEF%");
                        break;
                    }
                default: break;
            }
            str.Append($" -  {Module_status} ");
            str.Append($"(Max: {Module_Max})");

            Module_str = str.ToString();

            switch(i)
            {
                case 1:
                    {
                        Equip_Module1.GetComponent<Text>().text = Module_str;
                        break;
                    }
                case 2:
                    {
                        Equip_Module2.GetComponent<Text>().text = Module_str;
                        break;
                    }
                case 3:
                    {
                        Equip_Module3.GetComponent<Text>().text = Module_str;
                        break;
                    }
                default: break;
            }
        }
       
    }

}
