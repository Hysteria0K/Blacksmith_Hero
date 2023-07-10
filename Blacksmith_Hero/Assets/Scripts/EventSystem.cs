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

        string filePath = Path.Combine(Application.persistentDataPath, "datatable");

        if (!Directory.Exists(filePath))
        {
            BetterStreamingAssets.Initialize();
            Debug.Log(filePath);
            CopyFilesToPersistentDataPath();
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


    private void CopyFilesToPersistentDataPath()
    {
        string datatableFolderPath = "datatable";  // datatable ���� �̸��� ���
        string targetFolderPath = Application.persistentDataPath + "/datatable";
        string[] csvFiles = BetterStreamingAssets.GetFiles(datatableFolderPath, "*.csv");

        foreach (string csvFilePath in csvFiles)
        {
            string fileName = Path.GetFileName(csvFilePath);
            string targetFilePath = Path.Combine(targetFolderPath, fileName);

            Directory.CreateDirectory(targetFolderPath);

            if (!File.Exists(targetFilePath))
            {
                byte[] fileBytes = BetterStreamingAssets.ReadAllBytes(csvFilePath);
                File.WriteAllBytes(targetFilePath, fileBytes);
            }
        }

        Debug.Log("���� �Ϸ�");
    }


    /*
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
    */
}
