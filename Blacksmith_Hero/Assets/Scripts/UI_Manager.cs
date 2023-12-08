using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject Mine_Upgrade_Cost;
    public GameObject Mine_Get_Token;
    public GameObject Mine_Upgrade_Button;

    private bool Mine_Selected;

    // Start is called before the first frame update
    void Start()
    {
        Stage_Update();
        UI_Update();

        Mine_UI.SetActive(false);
        Mine_Selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        Mine_Upgrade_SetActive();
    }

    public void UI_Update()
    {
        //Map
        Gold.GetComponent<Text>().text = $"{Status_Reader.GetComponent<Status_Reader>().Gold}";
        Token.GetComponent<Text>().text = $"{Status_Reader.GetComponent<Status_Reader>().Token}";

        //Mine
        Mine_Level.GetComponent<Text>().text = $"Depth {Status_Reader.GetComponent<Status_Reader>().Mine_Level}M";
        Mine_Upgrade_Cost.GetComponent<Text>().text = $"{Status_Reader.GetComponent<Status_Reader>().Mine_Upgrade_Cost} Gold";
        Mine_Get_Token.GetComponent<Text>().text = $"{Status_Reader.GetComponent<Status_Reader>().Get_Token}";
    }

    public void Stage_Update()
    {
        Stage.GetComponent<Text>().text = $"Stage {Status_Reader.GetComponent<Status_Reader>().Stage}";
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
        if (Mine_Selected == false)
        {
            Mine_UI.SetActive(true);
            Mine_Selected = true;
        }
        else
        {
            Mine_UI.SetActive(false);
            Mine_Selected = false;
        }
    }

    public void Mine_Upgrade_SetActive()
    {
        if (Mine_Selected == true && Status_Reader.GetComponent<Status_Reader>().Gold >= Status_Reader.GetComponent<Status_Reader>().Mine_Upgrade_Cost)
        {
            Mine_Upgrade_Button.SetActive(true);
        }
        else Mine_Upgrade_Button.SetActive(false);
    }
}
