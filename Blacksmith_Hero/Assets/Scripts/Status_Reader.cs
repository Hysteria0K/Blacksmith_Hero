using System.Collections;
using System.Collections.Generic;
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
        List<Dictionary<string, object>> Player_Status = CSVReader.Read("Player_Status");
        List<Dictionary<string, object>> Enemy_Status = CSVReader.Read("Enemy_Status");
        List<Dictionary<string, object>> DataBase= CSVReader.Read("DataBase");

        Stage = (int)DataBase[0]["Stage"];
        Gold = (int)DataBase[0]["Gold"];
        Token = (int)DataBase[0]["Token"];

        Player_Hp = (int) Player_Status[Player_Level - 1]["Hp"];
        Player_Atk = (int) Player_Status[Player_Level - 1]["Atk"];

        Enemy_Name = (string)Enemy_Status[Stage - 1]["Name"];
        Enemy_Hp = (int)Enemy_Status[Stage - 1]["Hp"];
        Enemy_Atk = (int)Enemy_Status[Stage - 1]["Atk"];
        Enemy_Gold = (int)Enemy_Status[Stage - 1]["Gold"];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
