using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    public GameObject Status_Reader;
    public GameObject Gold;
    public GameObject Stage;
    public GameObject Token;
    // Start is called before the first frame update
    void Start()
    {
        //화면 설정
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 60;

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
