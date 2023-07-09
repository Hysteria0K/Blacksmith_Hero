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
        //ȭ�� ����
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 60;


        // ���� ����

        string filePath = Path.Combine(Application.persistentDataPath, "/DataTable");

        if (!File.Exists(filePath))
        {
            Debug.Log(filePath);
            YourMethodName();
        }

        else
        {
            Debug.Log("������ʿ�");
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
    // Android������ WWW Ŭ������ ����Ͽ� ���Ͽ� �����ؾ� �մϴ�.
    using (UnityWebRequest www = UnityWebRequest.Get(sourcePath))
    {
        www.SendWebRequest();

        while (!www.isDone)
        {
            // ���
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
        // Android �̿��� �÷��������� �Ϲ����� ���� ���� �޼��带 ����մϴ�.
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