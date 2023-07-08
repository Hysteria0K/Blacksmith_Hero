using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class Status_Reader : MonoBehaviour
{
    public int Player_Level;
    public int Player_Hp;
    public int Player_Atk;

    public int Stage;
    public int Gold;
    public int Token;


    public string Enemy_Name;
    public int Enemy_Hp;
    public int Enemy_Atk;
    public int Enemy_Gold;

    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> Player_Status = CSVReader.Read("Player_Status.csv");
        List<Dictionary<string, object>> Enemy_Status = CSVReader.Read("Enemy_Status.csv");
        List<Dictionary<string, object>> DataBase= CSVReader.Read("DataBase.csv");

        if (DataBase[0].ContainsKey("Stage")) if (int.TryParse(DataBase[0]["Stage"].ToString(), out Stage)) { }
        if (DataBase[0].ContainsKey("Gold")) if (int.TryParse(DataBase[0]["Gold"].ToString(), out Gold)) { }
        if (DataBase[0].ContainsKey("Token")) if (int.TryParse(DataBase[0]["Token"].ToString(), out Token)) { }

        if (Player_Status[Player_Level-1].ContainsKey("Hp")) if (int.TryParse(Player_Status[Player_Level-1]["Hp"].ToString(), out Player_Hp)) { }
        if (Player_Status[Player_Level-1].ContainsKey("Atk")) if (int.TryParse(Player_Status[Player_Level-1]["Atk"].ToString(), out Player_Atk)) { }

        Enemy_Name = (string)Enemy_Status[Stage - 1]["Name"];

        if (Enemy_Status[Stage - 1].ContainsKey("Hp")) if (int.TryParse(Enemy_Status[Stage - 1]["Hp"].ToString(), out Enemy_Hp)) { }
        if (Enemy_Status[Stage - 1].ContainsKey("Atk")) if (int.TryParse(Enemy_Status[Stage - 1]["Atk"].ToString(), out Enemy_Atk)) { }
        if (Enemy_Status[Stage - 1].ContainsKey("Gold")) if (int.TryParse(Enemy_Status[Stage - 1]["Gold"].ToString(), out Enemy_Gold)) { }
                                
    }

    // Update is called once per frame
    void Update()
    {

    }
}
