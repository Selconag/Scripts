using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Used for language changing in UIMenu Scene

public class Language_Settings : MonoBehaviour
{
    #region UIMenu
    //Connect Panel
    public Text Play;
    public Text Connect;
    //Signin Panel
    public Text Signin_Signup;
    public Text Signin_Login;
    public Text Signin_Name_Text;//PlaceHolder
    public Text Signin_Email_Text;//PlaceHolder
    public Text Signin_Password_Text;//PlaceHolder
    //Login Panel
    public Text Login_Login;
    public Text Login_Email_Text;//PlaceHolder
    public Text Login_Password_Text;//PlaceHolder
    public Text Login_Forgot_Password_Text;
    //PreTest Panel
    public Text Age_Text;//PlaceHolder
    public Text Gender_Text;//PlaceHolder
    public Text Occupation_Text;//PlaceHolder
    public Text ProfExp_Text;//PlaceHolder
    public Text Country_Text;//PlaceHolder
    public Text PreTest_Next;
    public Text Start_PreTest;
    public Text Button_Yes;
    public Text Button_No;
    //Char Selection Panel
    public Text Char_Select;
    public Text You_Sure_Text;
    public Text Char_Yes;
    public Text Char_No;

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
                //UIMENU TEXTS
                //Connect Panel
                Play.text = "Play";
                Connect.text = "Connect";
                //Signin Panel
                Signin_Signup.text = "Sign Up";
                Signin_Login.text = "Log In";
                Signin_Name_Text.text = "Name";
                Signin_Email_Text.text = "Email";
                Signin_Password_Text.text = "Password";
                //Login Panel
                Login_Login.text = "Log In";
                Login_Email_Text.text = "Email";
                Login_Password_Text.text = "Password";
                Login_Forgot_Password_Text.text = "Forgot my Password";
                //PreTest Panel
                Age_Text.text = "Age";
                Gender_Text.text = "Gender";
                Occupation_Text.text = "Occupation";
                ProfExp_Text.text = "Professional Experience(Years)";
                Country_Text.text = "Country";
                PreTest_Next.text = "Next";
                Start_PreTest.text = "Start My Pre-Test";
                Button_Yes.text = "Yes";
                Button_No.text = "No";
                //Char Selection Panel
                Char_Select.text = "Select";
                You_Sure_Text.text = "Are you sure you want to pick this character?";
                Char_Yes.text = "Yes";
                Char_No.text = "No";
                break;
            //TURKISH
            case 2:
                //UIMENU TEXTS
                //Connect Panel
                Play.text = "Oyna";
                Connect.text = "Bağlan";
                //Signin Panel
                Signin_Signup.text = "Kayıt Ol";
                Signin_Login.text = "Giriş Yap";
                Signin_Name_Text.text = "İsim";
                Signin_Email_Text.text = "Eposta";
                Signin_Password_Text.text = "Şifre";
                //Login Panel
                Login_Login.text = "Giriş Yap";
                Login_Email_Text.text = "Eposta";
                Login_Password_Text.text = "Şifre";
                Login_Forgot_Password_Text.text = "Şifremi Unuttum";
                //PreTest Panel
                Age_Text.text = "Yaş";
                Gender_Text.text = "Cinsiyet";
                Occupation_Text.text = "Meslek";
                ProfExp_Text.text = "Profesyonel Tecrübe(Yıl)";
                Country_Text.text = "Ülke";
                PreTest_Next.text = "Sonraki";
                Start_PreTest.text = "Ön Teste Başla";
                Button_Yes.text = "Evet";
                Button_No.text = "Hayır";
                //Char Selection Panel
                Char_Select.text = "Seç";
                You_Sure_Text.text = "Bu karakteri seçmek istediğine emin misin?";
                Char_Yes.text = "Evet";
                Char_No.text = "Hayır";
                break;
        }
    }

}
