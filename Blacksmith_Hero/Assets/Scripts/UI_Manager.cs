using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.AnimatedValues;
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
    public GameObject Equip_Manager;

    public GameObject Mine_UI;
    public GameObject Equip_UI;
    public GameObject Char_UI;

    public GameObject Mine_Level;
    public GameObject Mine_Upgrade_Cost;
    public GameObject Mine_Get_Token;
    public GameObject Mine_Upgrade_Button;

    public GameObject Equip_Require_Token;
    public GameObject Equip_Create_Button;

    public GameObject Player_Level;
    public GameObject Player_Hp;
    public GameObject Player_Atk;
    public GameObject Player_Def;
    public GameObject Atk_Module;
    public GameObject Def_Module;
    public GameObject Uti_Module;

    private bool Mine_Selected;
    private bool Equip_Selected;
    private bool Char_Selected;

    // Start is called before the first frame update
    void Start()
    {
        Stage_Update();
        UI_Update();

        Mine_UI.SetActive(false);
        Mine_Selected = false;

        Equip_UI.SetActive(false);
        Equip_Selected = false;

        Char_UI.SetActive(false);
        Char_Selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        Mine_Upgrade_SetActive();
        Equip_Create_SetActive();
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

        //Equip_Create
        if ( Equip_Manager.GetComponent<Equip_Manager>().Create_Mode == 1 ) Equip_Require_Token.GetComponent<Text>().text = $"{Equip_Manager.GetComponent<Equip_Manager>().Require_Token} Required";

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

            Equip_UI.SetActive(false);
            Equip_Selected = false;

            Char_UI.SetActive(false);
            Char_Selected = false;
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

    public void Equip_Select()
    {
        if (Equip_Selected == false)
        {
            Equip_UI.SetActive(true);
            Equip_Selected = true;

            Mine_UI.SetActive(false);
            Mine_Selected = false;

            Char_UI.SetActive(false);
            Char_Selected = false;
        }
        else
        {
            Equip_UI.SetActive(false);
            Equip_Selected = false;
        }
    }

    public void Equip_Create_SetActive()
    {
        if (Equip_Selected == true && Status_Reader.GetComponent<Status_Reader>().Token >= Equip_Manager.GetComponent<Equip_Manager>().Require_Token)
        {
            Equip_Create_Button.SetActive(true);
        }
        else Equip_Create_Button.SetActive(false);
    }

    public void Char_Select()
    {
        if (Char_Selected == false)
        {
            Char_UI.SetActive(true);
            Char_Selected = true;

            Equip_UI.SetActive(false);
            Equip_Selected = false;

            Mine_UI.SetActive(false);
            Mine_Selected = false;

            Char_Update();
        }
        else
        {
            Char_UI.SetActive(false);
            Char_Selected = false;
        }
    }

    public void Char_Update() 
    {
        Player_Level.GetComponent<Text>().text = $"Level {Status_Reader.GetComponent<Status_Reader>().Player_Level}";
        Player_Hp.GetComponent<Text>().text = $"Hp : {Status_Reader.GetComponent<Status_Reader>().Player_Hp}";
        Player_Atk.GetComponent<Text>().text = $"Atk : {Status_Reader.GetComponent<Status_Reader>().Player_Atk}";
        Player_Def.GetComponent<Text>().text = $"Def : {Status_Reader.GetComponent<Status_Reader>().Player_Def}";
        Atk_Module.GetComponent<Text>().text = $"Atk M : {Status_Reader.GetComponent<Status_Reader>().Atk_Module}";
        Def_Module.GetComponent<Text>().text = $"Def M : {Status_Reader.GetComponent<Status_Reader>().Def_Module}";
        Uti_Module.GetComponent<Text>().text = $"Uti M : {Status_Reader.GetComponent<Status_Reader>().Uti_Module}";

    }

}
