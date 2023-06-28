using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status_Reader : MonoBehaviour
{
    public int Player_Level;
    public int Player_Hp;
    public int Player_Atk;
    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Player_Status");

        Player_Hp = (int)data[Player_Level-1]["Hp"];
        Player_Atk = (int)data[Player_Level - 1]["Atk"];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
