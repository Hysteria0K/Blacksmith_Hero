using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Reader : MonoBehaviour
{
    public int Player_Level;
    public int Player_Hp;
    public int Player_Atk;

    public int Stage;
    public int Enemy_Hp;
    public int Enemy_Atk;
    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> Player_Status = CSVReader.Read("Player_Status");
        List<Dictionary<string, object>> Enemy_Status = CSVReader.Read("Enemy_Status");

        Player_Hp = (int) Player_Status[Player_Level - 1]["Hp"];
        Player_Atk = (int) Player_Status[Player_Level - 1]["Atk"];

        Enemy_Hp = (int)Enemy_Status[Stage - 1]["Hp"];
        Enemy_Atk = (int)Enemy_Status[Stage - 1]["Atk"];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
