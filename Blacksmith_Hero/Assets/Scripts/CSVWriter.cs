using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
//using UnityEditor.Search;
using Unity.VisualScripting.Antlr3.Runtime;
using System;
using UnityEditor;

public class CSVWriter : MonoBehaviour
{
    void Start()
    {

    }

    public static void UpdateDataBase(string searchValue, string newValue) // (수정하고자 하는 값, 값)
    {
        string filepath;
        // 파일 읽기를 위해 StreamReader를 사용하여 CSV 파일 열기
        if (Application.isEditor)
        {
            filepath = "Assets/Database.csv";
        }

        else
        {
            filepath = Path.Combine(Application.persistentDataPath, "Database.csv"); //"Assets/DataTable/Resources/Database.csv";
        }

        // 행 번호를 기억하기 위한 변수
        int searchLine = -1;
        int searchColumn = -1;


        // CSV 파일 읽기
        using (StreamReader reader = new StreamReader(filepath))
        {
            // 파일의 각 줄을 읽기
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
            // 검색 값의 행 번호가 유효하면 CSV 파일 수정
            string[] lines = File.ReadAllLines(filepath);

            if (searchLine + 1 < lines.Length)
            {
                // 검색 값의 바로 아래 열 수정
                string[] columns = lines[searchLine + 1].Split(',');
                columns[searchColumn] = newValue; // 첫 번째 열 수정

                // 수정된 데이터로 줄 다시 구성
                lines[searchLine + 1] = string.Join(",", columns);

                // 수정된 데이터를 파일에 쓰기
                File.WriteAllLines(filepath, lines);
            }
        }
    }
}
