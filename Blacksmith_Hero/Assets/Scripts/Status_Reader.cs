using System.Collections;
using System.Collections.Generic;
using System.Text;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class Status_Reader : MonoBehaviour
{       
    public struct Equip
    {
        public int Equip_Type;
        public int Equip_Status_Type;
        public int Equip_Status;

        public int Module1;
        public int Module2;
        public int Module3;

        public int Module1_Type;
        public int Module2_Type;
        public int Module3_Type;

        public int Module1_Status;
        public int Module2_Status;
        public int Module3_Status;

        public bool Equiped;
    };

    //playerstatus.csv
    public int Player_Level;
    public int Player_Hp;
    public int Player_Atk;
    public int Player_Def = 0;

    //database.csv
    public int Stage;
    public int Gold;
    public int Token;
    public int Mine_Level;
    public int Mining_Time; // 현재까지 캔 시간

    public string Helmet_Code;
    public string Chest_Code;
    public string Pants_Code;
    public string Gloves_Code;
    public string Boots_Code;

    //enemystatus.csv
    public string Enemy_Name;
    public int Enemy_Hp;
    public int Enemy_Atk;
    public int Enemy_Gold;

    //minestatus.csv
    public int Mine_Upgrade_Cost;
    public int Get_Token;
    public int Mine_Time; // 토큰 얻는 데 걸리는 시간

    //Equip_Status
    public int Atk_Module;
    public int Def_Module;
    public int Uti_Module;

    Equip Helmet;
    Equip Chest;
    Equip Pants;
    Equip Gloves;
    Equip Boots;

    public int Hp_p;
    public int Atk_p;
    public int Def_p;

    // Start is called before the first frame update
    void Start()
    {
        Read_Status();
    }          
    

    // Update is called once per frame
    void Update()
    {

    }

    public void Read_Status()
    {
        List<Dictionary<string, object>> Player_Status = CSVReader.Read("playerstatus.csv");
        List<Dictionary<string, object>> Enemy_Status = CSVReader.Read("enemystatus.csv");
        List<Dictionary<string, object>> DataBase = CSVReader.Read("database.csv");
        List<Dictionary<string, object>> Mine_Status = CSVReader.Read("minestatus.csv");

        if (DataBase[0].ContainsKey("Stage")) int.TryParse(DataBase[0]["Stage"].ToString(), out Stage);
        if (DataBase[0].ContainsKey("Gold")) int.TryParse(DataBase[0]["Gold"].ToString(), out Gold);
        if (DataBase[0].ContainsKey("Token")) int.TryParse(DataBase[0]["Token"].ToString(), out Token);
        if (DataBase[0].ContainsKey("Mine_Level")) int.TryParse(DataBase[0]["Mine_Level"].ToString(), out Mine_Level);
        if (DataBase[0].ContainsKey("Mining_Time")) int.TryParse(DataBase[0]["Mining_Time"].ToString(), out Mining_Time);

        if (Player_Status[Player_Level - 1].ContainsKey("Hp")) int.TryParse(Player_Status[Player_Level - 1]["Hp"].ToString(), out Player_Hp);
        if (Player_Status[Player_Level - 1].ContainsKey("Atk")) int.TryParse(Player_Status[Player_Level - 1]["Atk"].ToString(), out Player_Atk);

        Enemy_Name = (string)Enemy_Status[Stage - 1]["Name"];

        if (Enemy_Status[Stage - 1].ContainsKey("Hp")) int.TryParse(Enemy_Status[Stage - 1]["Hp"].ToString(), out Enemy_Hp);
        if (Enemy_Status[Stage - 1].ContainsKey("Atk")) int.TryParse(Enemy_Status[Stage - 1]["Atk"].ToString(), out Enemy_Atk);
        if (Enemy_Status[Stage - 1].ContainsKey("Gold")) int.TryParse(Enemy_Status[Stage - 1]["Gold"].ToString(), out Enemy_Gold);

        if (Mine_Status[Mine_Level].ContainsKey("Upgrade")) int.TryParse(Mine_Status[Mine_Level]["Upgrade"].ToString(), out Mine_Upgrade_Cost);
        if (Mine_Status[Mine_Level].ContainsKey("Token")) int.TryParse(Mine_Status[Mine_Level]["Token"].ToString(), out Get_Token);
        if (Mine_Status[Mine_Level].ContainsKey("Time")) int.TryParse(Mine_Status[Mine_Level]["Time"].ToString(), out Mine_Time);

        Atk_Module = 0;
        Def_Module= 0;
        Uti_Module= 0;

        Hp_p = 100;
        Atk_p = 100;
        Def_p = 100;

        Helmet.Equiped = false;
        Chest.Equiped = false;
        Pants.Equiped = false;
        Gloves.Equiped = false;
        Boots.Equiped = false;

        Read_Equip();
    }

    public void Read_Equip()
    {
        List<Dictionary<string, object>> DataBase = CSVReader.Read("database.csv");

        if (DataBase[0].ContainsKey("Helmet")) Helmet_Code = DataBase[0]["Helmet"].ToString();
        if (DataBase[0].ContainsKey("Chest")) Chest_Code = DataBase[0]["Chest"].ToString();
        if (DataBase[0].ContainsKey("Pants")) Pants_Code = DataBase[0]["Pants"].ToString();
        if (DataBase[0].ContainsKey("Gloves")) Gloves_Code = DataBase[0]["Gloves"].ToString(); ;
        if (DataBase[0].ContainsKey("Boots")) Boots_Code = DataBase[0]["Boots"].ToString();

        Equip_Stat_Apply();
    }

    public void Equip_Stat_Apply()
    {
        // 분석
        Analyze_Equip_Stat(Helmet_Code);
        Analyze_Equip_Stat(Chest_Code);
        Analyze_Equip_Stat(Pants_Code);
        Analyze_Equip_Stat(Gloves_Code);
        Analyze_Equip_Stat(Boots_Code);

        // 적용
        if (Helmet.Equiped) Apply_Status(Helmet);
        if (Chest.Equiped) Apply_Status(Chest);
        if (Pants.Equiped) Apply_Status(Pants);
        if (Gloves.Equiped) Apply_Status(Gloves);
        if (Boots.Equiped) Apply_Status(Boots);

        Player_Hp = Player_Hp * Hp_p / 100;
        Player_Atk = Player_Atk * Atk_p / 100;
        Player_Def = Player_Def * Def_p / 100;
    }

    public void Analyze_Equip_Stat(string Equip_Code)
    {
        if (Equip_Code[0] == '0')
        {
            Debug.Log("작동안함");
            return;
        }

        List<Dictionary<string, object>> Module_Data = CSVReader.Read("module_datatable.csv");

        Equip Temp;

        Temp.Module1_Type = 0;
        Temp.Module2_Type = 0;
        Temp.Module3_Type = 0;
        
        //주요 스탯 타입
        StringBuilder str1 = new StringBuilder();

        str1.Append(Equip_Code[1]);
        str1.Append(Equip_Code[2]);

        Temp.Equip_Status_Type = int.Parse(str1.ToString());

        //스탯 수치
        StringBuilder str2 = new StringBuilder();

        str2.Append(Equip_Code[3]);
        str2.Append(Equip_Code[4]);
        str2.Append(Equip_Code[5]);
        str2.Append(Equip_Code[6]);
        str2.Append(Equip_Code[7]);

        Temp.Equip_Status = int.Parse(str2.ToString());

        //모듈1
        StringBuilder str3 = new StringBuilder();

        str3.Append(Equip_Code[8]);
        str3.Append(Equip_Code[9]);

        Temp.Module1= int.Parse(str3.ToString());
        if (Module_Data[Temp.Module1].ContainsKey("Type")) int.TryParse(Module_Data[Temp.Module1]["Type"].ToString(), out Temp.Module1_Type);

        //모듈1 수치
        StringBuilder str4 = new StringBuilder();

        str4.Append(Equip_Code[10]);
        str4.Append(Equip_Code[11]);
        str4.Append(Equip_Code[12]);
        str4.Append(Equip_Code[13]);

        Temp.Module1_Status = int.Parse(str4.ToString());

        //모듈2
        StringBuilder str5 = new StringBuilder();

        str5.Append(Equip_Code[14]);
        str5.Append(Equip_Code[15]);

        Temp.Module2 = int.Parse(str5.ToString());
        if (Module_Data[Temp.Module2].ContainsKey("Type")) int.TryParse(Module_Data[Temp.Module2]["Type"].ToString(), out Temp.Module2_Type);

        //모듈2 수치
        StringBuilder str6 = new StringBuilder();

        str6.Append(Equip_Code[16]);
        str6.Append(Equip_Code[17]);
        str6.Append(Equip_Code[18]);
        str6.Append(Equip_Code[19]);

        Temp.Module2_Status = int.Parse(str6.ToString());

        //모듈3
        StringBuilder str7 = new StringBuilder();

        str7.Append(Equip_Code[20]);
        str7.Append(Equip_Code[21]);

        Temp.Module3 = int.Parse(str7.ToString());
        if (Module_Data[Temp.Module3].ContainsKey("Type")) int.TryParse(Module_Data[Temp.Module3]["Type"].ToString(), out Temp.Module3_Type);

        //모듈3 수치
        StringBuilder str8 = new StringBuilder();

        str8.Append(Equip_Code[22]);
        str8.Append(Equip_Code[23]);
        str8.Append(Equip_Code[24]);
        str8.Append(Equip_Code[25]);

        Temp.Module3_Status = int.Parse(str8.ToString());

        switch(Equip_Code[0])
        {
            case '1':
                {
                    Helmet.Equip_Type = Equip_Code[0];
                    Helmet.Equip_Status_Type = Temp.Equip_Status_Type;
                    Helmet.Equip_Status = Temp.Equip_Status;

                    Helmet.Module1 = Temp.Module1;
                    Helmet.Module1_Type = Temp.Module1_Type;
                    Helmet.Module1_Status = Temp.Module1_Status;

                    Helmet.Module2 = Temp.Module2;
                    Helmet.Module2_Type= Temp.Module2_Type;
                    Helmet.Module2_Status= Temp.Module2_Status;

                    Helmet.Module3 = Temp.Module3;
                    Helmet.Module3_Type = Temp.Module3_Type;
                    Helmet.Module3_Status = Temp.Module3_Status;

                    Helmet.Equiped = true;
                    break;
                }
            case '2':
                {
                    Chest.Equip_Type = Equip_Code[0];
                    Chest.Equip_Status_Type = Temp.Equip_Status_Type;
                    Chest.Equip_Status = Temp.Equip_Status;

                    Chest.Module1 = Temp.Module1;
                    Chest.Module1_Type = Temp.Module1_Type;
                    Chest.Module1_Status = Temp.Module1_Status;

                    Chest.Module2 = Temp.Module2;
                    Chest.Module2_Type = Temp.Module2_Type;
                    Chest.Module2_Status = Temp.Module2_Status;

                    Chest.Module3 = Temp.Module3;
                    Chest.Module3_Type = Temp.Module3_Type;
                    Chest.Module3_Status = Temp.Module3_Status;

                    Chest.Equiped = true;
                    break;
                }
            case '3':
                {
                    Pants.Equip_Type = Equip_Code[0];
                    Pants.Equip_Status_Type = Temp.Equip_Status_Type;
                    Pants.Equip_Status = Temp.Equip_Status;

                    Pants.Module1 = Temp.Module1;
                    Pants.Module1_Type = Temp.Module1_Type;
                    Pants.Module1_Status = Temp.Module1_Status;

                    Pants.Module2 = Temp.Module2;
                    Pants.Module2_Type = Temp.Module2_Type;
                    Pants.Module2_Status = Temp.Module2_Status;

                    Pants.Module3 = Temp.Module3;
                    Pants.Module3_Type = Temp.Module3_Type;
                    Pants.Module3_Status = Temp.Module3_Status;

                    Pants.Equiped = true;
                    break;
                }
            case '4':
                {
                    Gloves.Equip_Type = Equip_Code[0];
                    Gloves.Equip_Status_Type = Temp.Equip_Status_Type;
                    Gloves.Equip_Status = Temp.Equip_Status;

                    Gloves.Module1 = Temp.Module1;
                    Gloves.Module1_Type = Temp.Module1_Type;
                    Gloves.Module1_Status = Temp.Module1_Status;

                    Gloves.Module2 = Temp.Module2;
                    Gloves.Module2_Type = Temp.Module2_Type;
                    Gloves.Module2_Status = Temp.Module2_Status;

                    Gloves.Module3 = Temp.Module3;
                    Gloves.Module3_Type = Temp.Module3_Type;
                    Gloves.Module3_Status = Temp.Module3_Status;

                    Gloves.Equiped = true;
                    break;
                }
            case '5':
                {
                    Boots.Equip_Type = Equip_Code[0];
                    Boots.Equip_Status_Type = Temp.Equip_Status_Type;
                    Boots.Equip_Status = Temp.Equip_Status;

                    Boots.Module1 = Temp.Module1;
                    Boots.Module1_Type = Temp.Module1_Type;
                    Boots.Module1_Status = Temp.Module1_Status;

                    Boots.Module2 = Temp.Module2;
                    Boots.Module2_Type = Temp.Module2_Type;
                    Boots.Module2_Status = Temp.Module2_Status;

                    Boots.Module3 = Temp.Module3;
                    Boots.Module3_Type = Temp.Module3_Type;
                    Boots.Module3_Status = Temp.Module3_Status;

                    Boots.Equiped = true;
                    break;
                }
        }


    }

    public void Apply_Status(Equip parts)
    {
        switch (parts.Equip_Status_Type)
        {
            case 1:
                {
                    Player_Hp += parts.Equip_Status;
                    break;
                }
            case 2:
                {
                    Hp_p += parts.Equip_Status;
                    break;
                }
            case 3:
                {
                    Player_Atk += parts.Equip_Status;
                    break;
                }
            case 4:
                {
                    Atk_p += parts.Equip_Status;
                    break;
                }
            case 5:
                {
                    Player_Def += parts.Equip_Status;
                    break;
                }
            case 6:
                {
                    Def_p += parts.Equip_Status;
                    break;
                }
            default: break;
        }

        switch(parts.Module1)
        {
            case 1:
                {
                    Player_Hp += parts.Module1_Status;
                    break;
                }
            case 2:
                {
                    Hp_p += parts.Module1_Status;
                    break;
                }
            case 3:
                {
                    Player_Atk += parts.Module1_Status;
                    break;
                }
            case 4:
                {
                    Atk_p += parts.Module1_Status;
                    break;
                }
            case 5:
                {
                    Player_Def += parts.Module1_Status;
                    break;
                }
            case 6:
                {
                    Def_p += parts.Module1_Status;
                    break;
                }
            default: break;
        }

        switch(parts.Module1_Type)
        {
            case 1:
                {
                    Atk_Module += 1;
                    break;
                }
            case 2:
                {
                    Def_Module += 1;
                    break;
                }
            case 3:
                {
                    Uti_Module += 1;
                    break;
                }
            default: break;
        }


        switch (parts.Module2)
        {
            case 1:
                {
                    Player_Hp += parts.Module2_Status;
                    break;
                }
            case 2:
                {
                    Hp_p += parts.Module2_Status;
                    break;
                }
            case 3:
                {
                    Player_Atk += parts.Module2_Status;
                    break;
                }
            case 4:
                {
                    Atk_p += parts.Module2_Status;
                    break;
                }
            case 5:
                {
                    Player_Def += parts.Module2_Status;
                    break;
                }
            case 6:
                {
                    Def_p += parts.Module2_Status;
                    break;
                }
            default: break;
        }

        switch (parts.Module2_Type)
        {
            case 1:
                {
                    Atk_Module += 1;
                    break;
                }
            case 2:
                {
                    Def_Module += 1;
                    break;
                }
            case 3:
                {
                    Uti_Module += 1;
                    break;
                }
            default: break;
        }


        switch (parts.Module3)
        {
            case 1:
                {
                    Player_Hp += parts.Module3_Status;
                    break;
                }
            case 2:
                {
                    Hp_p += parts.Module3_Status;
                    break;
                }
            case 3:
                {
                    Player_Atk += parts.Module3_Status;
                    break;
                }
            case 4:
                {
                    Atk_p += parts.Module3_Status;
                    break;
                }
            case 5:
                {
                    Player_Def += parts.Module3_Status;
                    break;
                }
            case 6:
                {
                    Def_p += parts.Module3_Status;
                    break;
                }
            default: break;
        }

        switch (parts.Module3_Type)
        {
            case 1:
                {
                    Atk_Module += 1;
                    break;
                }
            case 2:
                {
                    Def_Module += 1;
                    break;
                }
            case 3:
                {
                    Uti_Module += 1;
                    break;
                }
            default: break;
        }


    }
}
