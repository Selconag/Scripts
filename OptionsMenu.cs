using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject InGameMenu;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject InGamePanel;

    DataServices DS;

    void Start()
    {
        DS = GameObject.Find("DataServices").GetComponent<DataServices>();
    }

    //Opens Menu
    public void Open_Menu()
    {
        InGameMenu.SetActive(true);
    }
    //Exits from game
    public void Exit_Game()
    {
        //SAVE ALL DATA BEFORE EXIT
        Save_before_Exit();
        Application.Quit();

    }
    //Returns to the Menu
    public void Return_to_Menu()
    {
        //SAVE ALL DATA BEFORE EXIT
        Save_before_Exit();
        SceneManager.LoadScene("UIMenu");
    }
    //Open the options panel
    public void Options_Panel()
    {
        InGamePanel.SetActive(false);
        OptionsPanel.SetActive(true);
        //OPTİONS MENU => CHANGE LANGUAGE PART WILL BE ADDED!!!!!!!!!!
    }
    //Return to the game
    public void Return_to_Game()
    {
        InGameMenu.SetActive(false);
    }
    //SAVE ALL DATA THAT IS NEEDED TO UPDATE, BEFORE EXIT
    void Save_before_Exit()
    {
        DS.BuildGameSave();
    }
}
