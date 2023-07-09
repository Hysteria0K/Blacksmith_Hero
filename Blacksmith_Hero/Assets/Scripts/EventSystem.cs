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

        string filePath = Path.Combine(Application.persistentDataPath, "/DataTable");

        if (!File.Exists(filePath))
        {
            Debug.Log(filePath);
            YourMethodName();
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

    private void YourMethodName()
    {
        CopyFilesToPersistentDataPath();
    }

    private void CopyFilesToPersistentDataPath()
    {
        string sourcePath = Application.streamingAssetsPath + "/DataTable";
        string targetPath = Application.persistentDataPath + "/DataTable";

#if UNITY_ANDROID && !UNITY_EDITOR
    // Android에서는 WWW 클래스를 사용하여 파일에 접근해야 합니다.
    using (UnityWebRequest www = UnityWebRequest.Get(sourcePath))
    {
        www.SendWebRequest();

        while (!www.isDone)
        {
            // 대기
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            Directory.CreateDirectory(targetPath);
            string[] files = Directory.GetFiles(sourcePath);

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                string targetFilePath = Path.Combine(targetPath, fileName);

                if (!File.Exists(targetFilePath))
                {
                    File.WriteAllBytes(targetFilePath, www.downloadHandler.data);
                }
            }
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