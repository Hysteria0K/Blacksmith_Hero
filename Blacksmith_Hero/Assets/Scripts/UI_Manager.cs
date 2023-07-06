using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject Status_Reader;
    public GameObject Gold;
    public GameObject Stage;
    public GameObject Token;

    // Start is called before the first frame update
    void Start()
    {
        //UI값 불러오기
        Gold.GetComponent<Text>().text = $"{Status_Reader.GetComponent<Status_Reader>().Gold}";
        Stage.GetComponent<Text>().text = $"Stage {Status_Reader.GetComponent<Status_Reader>().Stage}";
        Token.GetComponent<Text>().text = $"{Status_Reader.GetComponent<Status_Reader>().Token}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
