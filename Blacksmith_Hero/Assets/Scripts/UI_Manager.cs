using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject Status_Reader;
    public GameObject Gold;
    public GameObject Stage;
    public GameObject Token;

    public Image Panel;

    public GameObject Game_Manager;

    public GameObject Mine_UI;

    public GameObject Mine_Level;

    // Start is called before the first frame update
    void Start()
    {
        Stage_Update();
        UI_Update();
        Mine_Level_Update();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UI_Update()
    {
        Gold.GetComponent<Text>().text = $"{Status_Reader.GetComponent<Status_Reader>().Gold}";
        Token.GetComponent<Text>().text = $"{Status_Reader.GetComponent<Status_Reader>().Token}";
    }

    public void Stage_Update()
    {
        Stage.GetComponent<Text>().text = $"Stage {Status_Reader.GetComponent<Status_Reader>().Stage}";
    }

    public void Mine_Level_Update()
    {
        Mine_Level.GetComponent<Text>().text = $"Depth {Status_Reader.GetComponent<Status_Reader>().Mine_Level}M";
    }


    public void Fade()
    {
        StartCoroutine(FadeCoroutine());

    }
    IEnumerator FadeCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.015f;
            yield return new WaitForSeconds(0.01f);
            Panel.color = new Color(0, 0, 0, fadeCount);
        }

        yield return new WaitForSeconds(1.5f);

        Status_Reader.GetComponent<Status_Reader>().Read_Status();
        Stage_Update();
        Game_Manager.GetComponent<Game_Manager>().ResetStage();

        while(fadeCount > 0.0f)
        {
            fadeCount -= 0.015f;
            yield return new WaitForSeconds(0.01f);
            Panel.color = new Color(0, 0, 0, fadeCount);
        }
    }

    public void Mine_Select()
    {
        Mine_UI.SetActive(true);
    }
}
