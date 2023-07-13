using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //화면 설정
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 60;

        // 파일 생성

        string filePath = Path.Combine(Application.persistentDataPath, "datatable");

        if (!Directory.Exists(filePath))
        {
            BetterStreamingAssets.Initialize();
            Debug.Log(filePath);
            CopyFilesToPersistentDataPath();
        }

        else
        {
            Debug.Log("복사불필요");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void CopyFilesToPersistentDataPath()
    {
        string targetFolderPath = Application.persistentDataPath + "/datatable";

        string[] csvFiles = BetterStreamingAssets.GetFiles("/", "*.csv", SearchOption.AllDirectories);

        Directory.CreateDirectory(targetFolderPath);

        foreach (string csvFilePath in csvFiles)
        {
            string fileName = Path.GetFileName(csvFilePath);
            string targetFilePath = Path.Combine(targetFolderPath, fileName);

            if (!File.Exists(targetFilePath))
            {
                byte[] fileBytes = BetterStreamingAssets.ReadAllBytes(csvFilePath);
                File.WriteAllBytes(targetFilePath, fileBytes);
            }
        }

        Debug.Log("복사 완료");
    }
}
