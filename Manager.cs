using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    //public string sceneX="SettingsMenu";
    public GameObject PanelMain;
    public GameObject PanelSettings;
    public GameObject PanelLevelSelect;
    public void Start_Button()
    {
        //SceneManager.LoadScene("GameScene");
        PanelMain.SetActive(false);
        PanelLevelSelect.SetActive(true);
    }
    public void Settings_Button()
    {
        //SceneManager.LoadScene("SettingsMenu");
        PanelMain.SetActive(false);
        PanelSettings.SetActive(true);
        //SceneManager.LoadScene(sceneX);
    }
    public void Back_to_Menu()
    {
        if (PanelSettings.activeSelf)
        {
            PanelSettings.SetActive(false);
            PanelMain.SetActive(true);
        }
        else
        {
            PanelLevelSelect.SetActive(false);
            PanelMain.SetActive(true);
        }
    }
    public void Exit_Button()
    {
        Application.Quit();
    }
    public void Level_1()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Level_2()
    {
        //SceneManager.LoadScene("SahilBölgesi");
    }
    public void Level_3()
    {
        //SceneManager.LoadScene("SahilBölgesi");
    }
    public void Level_4()
    {
        //nothing happens
        //SceneManager.LoadScene("SahilBölgesi");
    }
    public void Level_5()
    {
        //nothing happens
        //SceneManager.LoadScene("SahilBölgesi");
    }




}