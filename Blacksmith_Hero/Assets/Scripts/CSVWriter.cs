using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor.Search;
using Unity.VisualScripting.Antlr3.Runtime;
using System;

public class CSVWriter : MonoBehaviour
{
    void Start()
    {
        //UpdateCSVFile("Gold", "200");
    }

    private void UpdateCSVFile(string searchValue, string newValue) // (�����ϰ��� �ϴ� ��, ��)
    {
        // ���� �б⸦ ���� StreamReader�� ����Ͽ� CSV ���� ����
        string filepath = "Assets/DataTable/Resources/Database.csv";

        // �� ��ȣ�� ����ϱ� ���� ����
        int searchLine = -1;
        int searchColumn = -1;


        // CSV ���� �б�
        using (StreamReader reader = new StreamReader(filepath))
        {
            // ������ �� ���� �б�
            string line;
            int currentLine = 0;
            int currentColumn = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string[] columns = line.Split(',');
                for (; currentColumn < columns.Length; currentColumn++)
                {
                    if (columns[currentColumn].Contains(searchValue))
                    {
                        searchColumn = currentColumn;
                        searchLine = currentLine;
                        break;
                    }
                }

                currentLine++;
            }
        }

        if (searchLine >= 0)
        {
            // �˻� ���� �� ��ȣ�� ��ȿ�ϸ� CSV ���� ����
            string[] lines = File.ReadAllLines(filepath);

            if (searchLine + 1 < lines.Length)
            {
                // �˻� ���� �ٷ� �Ʒ� �� ����
                string[] columns = lines[searchLine + 1].Split(',');
                columns[searchColumn] = newValue; // ù ��° �� ����

                // ������ �����ͷ� �� �ٽ� ����
                lines[searchLine + 1] = string.Join(",", columns);

                // ������ �����͸� ���Ͽ� ����
                File.WriteAllLines(filepath, lines);
            }
        }
    }
}