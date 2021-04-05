using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Used for language changing in UIMenu Scene

public class Language_Game : MonoBehaviour
{
    #region Game
    //UI Panel
    public Text Ball_Number;
    public Text General_Score_Text;
    public Text Game_Score_Text;
    public Text Button_Fire;
    public Text Button_Game_Start;
    public Text Button_Game_End;
    public Text Button_Interact;
    //Video Panel
    public Text Video_Button_Cancel;
    //In Game Menu
    public Text Button_Resume;
    public Text Button_Options;
    public Text Button_MainMenu;
    public Text Button_Exit;
    //Test Panel
    public Text Test_Button_Next;
    //Modul Reading Panel
    public Text Reading_Button_Exit;
    //Modul Interactive Panel
    public Text Button_GoToActivity;
    public Text Button_Next;
    public Text Input_Field_Placeholder;
    //
    #endregion
    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    public void Change_Language(int Language)
    {
        switch (Language)
        {
            //ENGLISH
            case 1:
                //GAME TEXTS
                /*Ball_Number.text = "Age";
                General_Score_Text.text = "Age";
                Game_Score_Text.text = "Age";*/
                Button_Fire.text = "Fire";
                Button_Game_Start.text = "Start Mini Game";
                Button_Game_End.text = "End Mini Game";
                Button_Interact.text = "Interact";
                //In Game Menu
                Button_Resume.text = "Resume Game";
                Button_Options.text = "Options";
                Button_MainMenu.text = "Quit to Main Menu";
                Button_Exit.text = "Exit Game";
                //Test Panel
                Test_Button_Next.text = "Next";
                //Modul Reading Panel
                Reading_Button_Exit.text = "Exit";
                //Modul Interactive Panel
                Button_GoToActivity.text = "Go To Activity";
                Button_Next.text = "Next";
                Input_Field_Placeholder.text = "Enter text...";

                break;
            //TURKISH
            case 2:
                //GAMEMENU TEXTS
                /*Ball_Number.text = "Age";
                General_Score_Text.text = "Age";
                Game_Score_Text.text = "Age";*/
                Button_Fire.text = "Ateş";
                Button_Game_Start.text = "Mini Oyuna Başla";
                Button_Game_End.text = "Mini Oyunu Bitir";
                Button_Interact.text = "Etkileşime Geç";
                //In Game Menu
                Button_Resume.text = "Oyuna Dön";
                Button_Options.text = "Ayarlar";
                Button_MainMenu.text = "Ana Menüye Dön";
                Button_Exit.text = "Oyundan Çık";
                //Test Panel
                Test_Button_Next.text = "Sonraki";
                //Modul Reading Panel
                Reading_Button_Exit.text = "Çıkış";
                //Modul Interactive Panel
                Button_GoToActivity.text = "Aktiviteye Git";
                Button_Next.text = "Sonraki";
                Input_Field_Placeholder.text = "Metni Girin...";
                break;
        }
    }
}
