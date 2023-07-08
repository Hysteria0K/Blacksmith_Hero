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
            string filePath = Path.Combine(Application.persistentDataPath, "DataTable");

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "����Ⱑ �ȵǴ°���");
                // ������ �������� ������ ���� ����
                CopyStreamingAssetsToPersistentDataPath();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CopyStreamingAssetsToPersistentDataPath()
    {
        string streamingAssetsPath = Application.streamingAssetsPath;
        string persistentDataPath = Application.persistentDataPath;

        CopyFolderContents(streamingAssetsPath, persistentDataPath);
    }

    private void CopyFolderContents(string sourceFolder, string destinationFolder)
    {
        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        string[] files = Directory.GetFiles(sourceFolder);
        foreach (string filePath in files)
        {
            string fileName = Path.GetFileName(filePath);
            string destinationPath = Path.Combine(destinationFolder, fileName);
            File.Copy(filePath, destinationPath, true);
        }

        string[] subfolders = Directory.GetDirectories(sourceFolder);
        foreach (string subfolderPath in subfolders)
        {
            string folderName = Path.GetFileName(subfolderPath);
            string destinationSubfolder = Path.Combine(destinationFolder, folderName);
            CopyFolderContents(subfolderPath, destinationSubfolder);
        }
    }
}
