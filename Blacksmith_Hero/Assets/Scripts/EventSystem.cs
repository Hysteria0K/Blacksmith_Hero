using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //화면 설정
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 60;


        // 파일 생성

        string filePath = Path.Combine(Application.persistentDataPath, "DataTable");

        if (!Directory.Exists(filePath))
        {
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
        string sourcePath = Application.streamingAssetsPath + "/DataTable";
        string targetPath = Application.persistentDataPath + "/DataTable";

#if UNITY_ANDROID && !UNITY_EDITOR
    // Android에서는 WWW 클래스를 사용하여 파일에 접근해야 합니다.

    string testPath1 = Path.Combine(Application.persistentDataPath, "test1.csv");
    string testPath2 = Path.Combine(Application.persistentDataPath, "test2.csv");
    string testPath3 = Path.Combine(Application.persistentDataPath, "test3.csv");
    string testPath4 = Path.Combine(Application.persistentDataPath, "test4.csv");
    string testPath5 = Path.Combine(Application.persistentDataPath, "test5.csv");
    string testPath6 = Path.Combine(Application.persistentDataPath, "test6.csv");
    using (UnityWebRequest www = UnityWebRequest.Get(sourcePath))
    {
        File.WriteAllText(testPath1, "test1");
        www.SendWebRequest();

        while (!www.isDone)
        {
            // 대기
            File.WriteAllText(testPath2, "test2");
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllText(testPath3, "test3");
            Directory.CreateDirectory(targetPath);
            string[] files = Directory.GetFiles(sourcePath);

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                string targetFilePath = Path.Combine(targetPath, fileName);

                if (!File.Exists(targetFilePath))
                {
                    File.WriteAllBytes(targetFilePath, www.downloadHandler.data);
                    File.WriteAllText(testPath4, "test4");
                }

                else
                {
                    File.WriteAllText(testPath5, "test5");
                }
            }
        }

        else
        {
            File.WriteAllText(testPath6, "test6");
        }

    }
#else
        // Android 이외의 플랫폼에서는 일반적인 파일 복사 메서드를 사용합니다.
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }

        string[] files = Directory.GetFiles(sourcePath);

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileName(filePath);
            string targetFilePath = Path.Combine(targetPath, fileName);

            if (!File.Exists(targetFilePath))
            {
                File.Copy(filePath, targetFilePath);
            }
        }
#endif
    }
}