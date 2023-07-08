using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ȭ�� ����
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 60;


        // ���� ����
        if (Application.isMobilePlatform)
        {
            string filePath = Path.Combine(Application.persistentDataPath, "database.csv");

            if (!File.Exists(filePath))
            {
                // ������ �������� ������ ���� ����
                File.WriteAllText(filePath, "Stage,Gold,Token\n1,0,0");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
