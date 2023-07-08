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
        //화면 설정
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 60;


        // 파일 생성
        if (Application.isMobilePlatform)
        {
            string filePath = Path.Combine(Application.persistentDataPath, "database.csv");

            if (!File.Exists(filePath))
            {
                // 파일이 존재하지 않으면 새로 생성
                File.WriteAllText(filePath, "Stage,Gold,Token\n1,0,0");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
