using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CSVReader
{
    public static List<Dictionary<string, object>> Read(string file)
    {
        string filePath = Path.Combine(Application.persistentDataPath + "/datatable", file);

        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

        // 파일 읽기
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            string[] headers = reader.ReadLine().Split(',');

            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');

                Dictionary<string, object> rowData = new Dictionary<string, object>();
                for (int i = 0; i < headers.Length; i++)
                {
                    string header = headers[i];
                    string value = values[i];

                    rowData[header] = value;
                }
                data.Add(rowData);
            }
        }
        return data;
    }
}