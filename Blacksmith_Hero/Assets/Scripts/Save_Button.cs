using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Button : MonoBehaviour
{
    public GameObject Status_Reader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gold_Update()
    {
        CSVWriter.UpdateDataBase("Gold", Status_Reader.GetComponent<Status_Reader>().Gold.ToString());
    }
}
