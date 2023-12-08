using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class Status_Reader : MonoBehaviour
{
    //playerstatus.csv
    public int Player_Level;
    public int Player_Hp;
    public int Player_Atk;

    //database.csv
    public int Stage;
    public int Gold;
    public int Token;
    public int Mine_Level;
    public int Mining_Time; // 현재까지 캔 시간

    //enemystatus.csv
    public string Enemy_Name;
    public int Enemy_Hp;
    public int Enemy_Atk;
    public int Enemy_Gold;

    //minestatus.csv
    public int Mine_Upgrade_Cost;
    public int Get_Token;
    public int Mine_Time; // 토큰 얻는 데 걸리는 시간

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

        if (DataBase[0].ContainsKey("Stage")) if (int.TryParse(DataBase[0]["Stage"].ToString(), out Stage)) { }
        if (DataBase[0].ContainsKey("Gold")) if (int.TryParse(DataBase[0]["Gold"].ToString(), out Gold)) { }
        if (DataBase[0].ContainsKey("Token")) if (int.TryParse(DataBase[0]["Token"].ToString(), out Token)) { }
        if (DataBase[0].ContainsKey("Mine_Level")) if (int.TryParse(DataBase[0]["Mine_Level"].ToString(), out Mine_Level)) { }
        if (DataBase[0].ContainsKey("Mining_Time")) if (int.TryParse(DataBase[0]["Mining_Time"].ToString(), out Mining_Time)) { }

        if (Player_Status[Player_Level - 1].ContainsKey("Hp")) if (int.TryParse(Player_Status[Player_Level - 1]["Hp"].ToString(), out Player_Hp)) { }
        if (Player_Status[Player_Level - 1].ContainsKey("Atk")) if (int.TryParse(Player_Status[Player_Level - 1]["Atk"].ToString(), out Player_Atk)) { }

        Enemy_Name = (string)Enemy_Status[Stage - 1]["Name"];

        if (Enemy_Status[Stage - 1].ContainsKey("Hp")) if (int.TryParse(Enemy_Status[Stage - 1]["Hp"].ToString(), out Enemy_Hp)) { }
        if (Enemy_Status[Stage - 1].ContainsKey("Atk")) if (int.TryParse(Enemy_Status[Stage - 1]["Atk"].ToString(), out Enemy_Atk)) { }
        if (Enemy_Status[Stage - 1].ContainsKey("Gold")) if (int.TryParse(Enemy_Status[Stage - 1]["Gold"].ToString(), out Enemy_Gold)) { }

        if (Mine_Status[Mine_Level].ContainsKey("Upgrade")) if (int.TryParse(Mine_Status[Mine_Level]["Upgrade"].ToString(), out Mine_Upgrade_Cost)) { }
        if (Mine_Status[Mine_Level].ContainsKey("Token")) if (int.TryParse(Mine_Status[Mine_Level]["Token"].ToString(), out Get_Token)) { }
        if (Mine_Status[Mine_Level].ContainsKey("Time")) if (int.TryParse(Mine_Status[Mine_Level]["Time"].ToString(), out Mine_Time)) { }

    }
}
