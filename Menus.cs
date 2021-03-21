using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Library;
using System.IO;
using UnityEngine.Android;
// using System.Text.RegularExpressions
//Used for comparison of texts like Regex.IsMatch(Age.text, @"^\d+$"))=>
//=> Compares a textbox, if all chars are integer returns true, else returns false
using System.Text.RegularExpressions;
public class Menus : MonoBehaviour
{
    //Other Classes
    public DataServices D;
    //UI Panels of Menu
    public GameObject Landing;
    public GameObject Connect;
    public GameObject Signin;
    public GameObject Login;
    public GameObject PreTest;
    public GameObject CharSelection;
    //Pretest sub panel groups
    public GameObject Start_Test_Panel;
    public GameObject User_Detail_Panel;
    public GameObject TestGroup;
    //Special Texts
    public Text QuestionTextPlace;
    //Variables
    private int i = 0;
    private int buttonvalue = 0;
    private string buttonstate = null;
    //Special Objects
    public GameObject YesNoButtonHolder;//Yes No Buttons
    public GameObject NumberButtonHolder;//For Rating Buttons
    public GameObject NextButton;
    public GameObject ConnectButton;
    public GameObject StartGameButton;//Play Button after pre test
    //Buttons
    public Button yesButton;
    public Button noButton;
    //Images
    public Image Rainbow;
    public Image Rainbow2;
    
    //TextBoxes of Sign-in/Login
    public InputField NameBox;
    public InputField EmailBox;
    public InputField PasswordBox;
    public InputField EmailBox_Login;
    public InputField PasswordBox_Login;
    //TextBoxes of PreTest User Details
    public InputField Age;
    public InputField Gender;
    public InputField Occupation;
    public InputField ProfExp;
    public InputField Country;
    //Error Panel
    public GameObject ErrorPanel;
    public Text ErrorText;
    //Character Images
    public GameObject Char1;
    public GameObject Char2;
    public GameObject Char3;


    //Questions that will be used in Pre-Test
    //1-8 Questions will take Yes/No
    //Last question will take a number ranges from 1 to 10
    private string[] Questions =
    {
        "Have you completed an online training on volunteering before?",
        "Have you ever had a gamification-based online training?",
        "Have you completed an online training before and received a certificate?",
        "Do you have any knowledge about social leadership and volunteering in sport?",
        "Do you have any knowledge about social innovation in/through sport?",
        "Do you have any knowledge about social inclusion in sport?",
        "Do you have any knowledge on social equality in sport?",
        "Do you have any knowledge on good governance, safety and security in sport events?",
        "What do you think about your level of knowledge and skills in social innovation and leadership in sports?"
    };

    private void Update()
    {
        //COMPLETED!
        //Check IF a local save data is here    
        //If:Connect Button appears
        if (Connect.activeSelf) 
        {
            if (File.Exists(Application.persistentDataPath + "/Connection.json"))
            {
                //If a save data comes later or available at start
                ConnectButton.SetActive(true);
            }
            //Else:Connect button fades away
            else
            {
                //Disable Connect Button
                ConnectButton.SetActive(false);
            }
        }
    }

    //YEDEK IS USED FOR MULTIPLATFORM BUILDING CHANGES
    private void yedek()
    {

        //IF APPLICATION IS RUNNING ON AN ANDROID DEVICE
        if (Application.platform == RuntimePlatform.Android)
        {
            /*
            Ask for User File Read and Write permissions for Android
            */
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) &&
                Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
            {
                // The user authorized read and write abilities, so you can start game.

                //Load first question to Questions Array for pretest
                QuestionTextPlace.text = Questions[i];
                //Go to Landing page
                Landing.SetActive(true);
                //Fadeaway from landing to connect page
                StartCoroutine(Landing_Fade());
                //Awake the DataServices
                DontDestroyOnLoad(D.gameObject);
            }
            else
            {
                // We do not have permission to use the microphone.
                // Ask for permission or proceed without the functionality enabled.
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) &&
                Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
                {
                    //Load first question to Questions Array for pretest
                    QuestionTextPlace.text = Questions[i];
                    //Go to Landing page
                    Landing.SetActive(true);
                    //Fadeaway from landing to connect page
                    StartCoroutine(Landing_Fade());
                    //Awake the DataServices
                    DontDestroyOnLoad(D.gameObject);
                }
                else Application.Quit();
            }
        }
        //IF APPLICATION IS RUNNING ON AN IOS DEVICE
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //Load first question to Questions Array for pretest
            QuestionTextPlace.text = Questions[i];
            //Go to Landing page
            Landing.SetActive(true);
            //Fadeaway from landing to connect page
            StartCoroutine(Landing_Fade());
            //Awake the DataServices
            DontDestroyOnLoad(D.gameObject);
        }
        //IF APPLICATION IS RUNNING ON WHATEVER
        else
        {
            //Load first question to Questions Array for pretest
            QuestionTextPlace.text = Questions[i];
            //Go to Landing page
            Landing.SetActive(true);
            //Fadeaway from landing to connect page
            StartCoroutine(Landing_Fade());
            //Awake the DataServices
            DontDestroyOnLoad(D.gameObject);
        }

    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        PreTest.SetActive(false);
        //Load first question to Questions Array for pretest
        QuestionTextPlace.text = Questions[i];
        //Go to Landing page
        Landing.SetActive(true);
        //Fadeaway from landing to connect page
        StartCoroutine(Landing_Fade());
        //Awake the DataServices
        //D = GetComponent<DataServices>();
        //D.BuildSerialization(D.con);
        DontDestroyOnLoad(D.gameObject);
    }
      
    //For now Awake is not used
    private void Awake()
    {
        /*
        Check if there is a local save data;
        IF:if exists then show Connect Button
        ELSE:Disable the Connect button from UI
         */

    }

    //Character Selecting Button
    //Selected char will remain forever

    public void Char_Selection()
    {
        //Ask user if he/she is sure to select that character
        if (true)
        {
            //Create or call a button and make it asks to user


        }

        int resp;
        if (Char1.activeInHierarchy)
        {
            //Go to 2nd character from 1st
            resp = D.CharSelection(1);
            if(resp == 1)
            {
                //Everything is ok, go to game
            }
            else 
            {
                //Some error occured try again

            }
        }
        else if (Char2.activeInHierarchy)
        {
            //Go to 3rd character from 2nd
            resp = D.CharSelection(2);
            if (resp == 1)
            {
                //Everything is ok, go to game
            }
            else
            {
                //Some error occured try again

            }
        }
        else
        {
            //Go to 1st character from 3rd
            resp = D.CharSelection(3);
            if (resp == 1)
            {
                //Everything is ok, go to game
            }
            else
            {
                //Some error occured try again

            }
        }
        
    }
    //Character image changer
    //Selected char will remain forever
    public void Char_Change(bool button)
    {
        //Go right
        if(button)
        {
            if (Char1.activeInHierarchy)
            {
                //Go to 2nd character from 1st
                Char1.SetActive(false);
                Char2.SetActive(true);

            }
            else if (Char2.activeInHierarchy)
            {
                //Go to 3rd character from 2nd
                Char2.SetActive(false);
                Char3.SetActive(true);
            }
            else
            {
                //Go to 1st character from 3rd
                Char3.SetActive(false);
                Char1.SetActive(true);
            }
        }
        //Go Left
        else
        {
            if (Char1.activeInHierarchy)
            {
                //Go to 3rd character from 1st
                Char1.SetActive(false);
                Char3.SetActive(true);
            }
            else if (Char2.activeInHierarchy)
            {
                //Go to 1st character from 2nd
                Char2.SetActive(false);
                Char1.SetActive(true);
            }
            else
            {
                //Go to 2nd character from 3rd
                Char3.SetActive(false);
                Char2.SetActive(true);
            }
        }
    }




    //COMPLETED!!
    //Forgot password sends email to the server
    //the rest is up to server
    //Calls a ForgotPassword method from DataService
    public void ForgotPasswordButton()
    {
        if (EmailBox_Login.text.Length > 0)
        {
            if (((EmailBox_Login.text).ToString()).Contains("@") && ((EmailBox_Login.text).ToString()).Contains("."))
            {
                D.mail.email = EmailBox_Login.text.ToString();
                D.ForgotPassword(D.mail);
            }
            else
            {
                //Error message pops-up as please enter a valid email
                ErrorText.text = "Email area must be filled correctly!";
                StartCoroutine(ErrorButton_Waiter());
            }
        }
        else
        {
            //Error message pops-up as nothing entered on emailbox
            ErrorText.text = "Email area must be filled!";
            StartCoroutine(ErrorButton_Waiter());
        }
    }

    //HALF COMPLETED // LATER TESTS AND IMPROVEMENTS WILL DONE SOON
    //Pre_Test Next button behaivour region from PreTest Panel
    //When last question is answered make Play button appear instead of next button

    //As the response checker the system will send the test data sand tries to get a response
    //if the response is corect then 
    public void Tests_Next_Button()
    {
        if (Start_Test_Panel.activeSelf)//NEXT Butonuna tıklanınca
        {
            Start_Test_Panel.SetActive(false);
            User_Detail_Panel.SetActive(true);
            NextButton.SetActive(true);
        }
        //Sends User Details on first "next" button click
        else if (User_Detail_Panel.activeSelf)//NEXT Butonuna tıklanınca
        {
            if (Gender.text.Length > 0 && Occupation.text.Length > 0 && Age.text.Length > 0 && ProfExp.text.Length > 0 && Country.text.Length > 0)
            {
                //Regex compares the textbox, if all chars are integer then returns true, forr others we check if string is not empty
                if ((Regex.IsMatch(Age.text, @"^\d+$"))  && (Regex.IsMatch(ProfExp.text, @"^\d+$")))
                {
                    //Cevapları al ve gönder
                    D.user.age = Age.text.ToString();
                    D.user.gender = Gender.text.ToString();
                    D.user.occupation = Occupation.text.ToString();
                    D.user.prof_exp = ProfExp.text.ToString();
                    D.user.country = Country.text.ToString();
                    D.user.security = D.con.security;
                    D.user.user_id = D.con.user_id;
                    //YAĞIZA DANIŞ BU KISMI
                    //EĞER VERİ DÜZGÜN BİR ŞEKİLDE GÖNDERİLEBİLİYORSA SİSTEM TAMAMDIR
                    int resp = D.BuildNewSerialization(D.user,2);
                    if(resp == 1)
                    {
                        //Successfully sended data
                        User_Detail_Panel.SetActive(false);
                        TestGroup.SetActive(true);
                        QuestionTextPlace.text = Questions[i];
                    }
                    else
                    {
                        //Couldn't send the data
                        //Maybe wrong type data or server connection problems?
                        ErrorText.text = "An Error Occured!";
                        StartCoroutine(ErrorButton_Waiter());
                    }

                }
            }
        }
        else
        {
            //Soruyu al
            if (((buttonstate != "") && (buttonstate != null && buttonvalue == 0)) || (buttonstate == null && buttonvalue != 0))
            {
                switch (i)
                {
                    //Sends answers based on which question we are on
                    case 0:
                        D.pre.bir = buttonstate;
                        break;
                    case 1:
                        D.pre.iki = buttonstate;
                        break;
                    case 2:
                        D.pre.uc = buttonstate;
                        break;
                    case 3:
                        D.pre.dort = buttonstate;
                        break;
                    case 4:
                        D.pre.bes = buttonstate;
                        break;
                    case 5:
                        D.pre.alti = buttonstate;
                        break;
                    case 6:
                        D.pre.yedi = buttonstate;
                        break;
                    case 7:
                        D.pre.sekiz = buttonstate;
                        YesNoButtonHolder.SetActive(false);
                        NumberButtonHolder.SetActive(true);
                        break;
                    case 8:
                        D.pre.puan = buttonvalue;
                        //Next button => Start Button
                        //YAĞIZA DANIŞ BU KISMI
                        //EĞER VERİ DÜZGÜN BİR ŞEKİLDE GÖNDERİLEBİLİYORSA SİSTEM TAMAMDIR
                        int resp = D.SendTestData(D.pre);
                        if (resp == 1)
                        {
                            //Successfully sended data
                            User_Detail_Panel.SetActive(false);
                            TestGroup.SetActive(true);
                            QuestionTextPlace.text = Questions[i];
                        }
                        else
                        {
                            //Couldn't send the data
                            //Maybe wrong type data or server connection problems?
                            ErrorText.text = "An Error Occured!";
                            StartCoroutine(ErrorButton_Waiter());
                        }
                        NextButton.SetActive(false);
                        //Activate the Start game Button
                        StartGameButton.SetActive(true);
                        //GO TO CHAR SELECTION PANEL
                        PreTest.SetActive(false);
                        CharSelection.SetActive(true);

                        break;
                }
                //Increment the counter to get next question
                i++;
                if(i<9)
                QuestionTextPlace.text = Questions[i];
            }
            buttonstate = null;
            buttonvalue = 0;
        }
        //TEST HERE!!
    }


    //COMPLETED!!
    //Play button behaivour region from Connect Panel
    public void Play_Button()
    {
        Button_Waiter();
        Connect.SetActive(false);
        Signin.SetActive(true);
    }


    //COMPLETED !!
    //Connect button behaivour region from Connect Panel
    //Reads local Connection.json data and gets 2 variables
    //gets user_id and security, sends the data and auto-connects to server
    //then logs-in to app and continues on where the person left
    //IMPORTANT NOTE: Platform based local save path may change due to some security issues
    //IF THIS EVER HAPPENS read Application.dataPath and Script Serialization docs from Unity Docs
    public void Connect_Button()
    {
        //Get local Data for auto login
        int situation = D.GetLocalUserData();
        Button_Waiter();
        if (situation == 1)
        {
            Connect.SetActive(false);
            /*
             If user not made pre-test before -> Go to PreTest
             Else user made pre-test before -> Go to Game
             */
            if ((D.con.user_detay == 1))
            {
                if (D.con.giris_test == 1)
                {
                    //If user not selected character go to char selection panel
                    if(D.con.karakter == 0)
                    {
                        CharSelection.SetActive(true);
                    }
                    //Else start the game
                    else
                    {
                        SceneManager.LoadScene("Game_Map");
                    }
                }
                else
                {
                    //Go to pretest
                    PreTest.SetActive(true);
                    Start_Test_Panel.SetActive(false);
                    NextButton.SetActive(true);
                    User_Detail_Panel.SetActive(false);
                    TestGroup.SetActive(true);
                }   
            }
            else
            {
                //Go to user_detail panel
                PreTest.SetActive(true);
                Start_Test_Panel.SetActive(true);
                NextButton.SetActive(false);
                User_Detail_Panel.SetActive(false);
                TestGroup.SetActive(false);
            }
        }
        else
        {
            //There is no Local save please first log in
            ErrorText.text = "There is no local save data present. Please login first!";
            StartCoroutine(ErrorButton_Waiter());
        }
        situation = 0;

    }

    //COMPLETED !!
    //Signin button behaivour region from Signin Panel
    //NOTICE: CAN'T CHECK IF USER IS AVAILABLE OR NOT
    //OR TRUE CONNECTION IS ESTABLISHED
    //ADD A TRY CATCH TO THE SYSTEM
    //OR A CHECKING SYSTEM
    public void Signin_Button()
    {
        //If user inputs are fully given
        if (NameBox.text.Length > 0 && EmailBox.text.Length > 0 && PasswordBox.text.Length > 0)
        {
            //If email contains both "."(dots) and "@"(at) chars, accept email 
            if (((EmailBox.text).ToString()).Contains("@") && ((EmailBox.text).ToString()).Contains("."))
            {
                D.user.name = NameBox.text.ToString();
                D.user.email = EmailBox.text.ToString();
                D.user.password = PasswordBox.text.ToString();
                //send data to server for user register
                int situation = D.BuildNewSerialization(D.user,1);
                if(situation == 1) ErrorText.text = "Registered to the system successfully";
                else if (situation == 2) ErrorText.text = "You are already registered to the system";
                else ErrorText.text = "An Error Occured";
                StartCoroutine(ErrorButton_Waiter());

                NameBox.text = "";
                EmailBox.text = "";
                PasswordBox.text = "";
            }
            else
            {
                //Error: Email must be filled correct!
                //As an error message open a new panel with text on it
                ErrorText.text = "Email area must be filled correctly!";
                StartCoroutine(ErrorButton_Waiter());
            }

        }
        else
        {
            //Error: All boxes must be filled!
            //As an error message open a new panel with text on it
            ErrorText.text = "All areas must be filled!";
            StartCoroutine(ErrorButton_Waiter());
        }

    }

    //COMPLETED ! WILL BE UPDATED !!!!
    //Login button behaivour region from Signin Panel
    //NOTICE: CAN'T CHECK IF USER IS AVAILABLE OR NOT
    //OR TRUE CONNECTION IS ESTABLISHED
    //ADD A TRY CATCH TO THE SYSTEM
    //OR A CHECKING SYSTEM
    public void Login_Button()
    {
        if (Signin.activeSelf)
        {
            Signin.SetActive(false);
            Login.SetActive(true);
        }
        else
        {      
            //If user inputs are fully given
            if (EmailBox_Login.text.Length > 0 && PasswordBox_Login.text.Length > 0)
            {
                //If email contains both "."(dots) and "@"(at) chars, accept email 
                if (((EmailBox_Login.text).ToString()).Contains("@") && ((EmailBox_Login.text).ToString()).Contains("."))
                {
                    D.user.email = EmailBox_Login.text.ToString();
                    D.user.password = PasswordBox_Login.text.ToString();
                    //Do the login
                    int situation = D.LoginManuel(D.user);
                    
                    //If response from server is returned as OK! then go on
                    if(situation == 1)
                    {
                        ErrorText.text = "Logged in to the system";
                        StartCoroutine(ErrorButton_Waiter());

                        //At here the system will get user info to know which page must direct the client
                        //string redirect = D.GetLocalUserData();
                        //if (!(redirect == "No"))

                        //Upward system is not used for now, a new system is testing

                            Login.SetActive(false);
                        /*
                         If user not made pre-test before -> Go to PreTest
                         Else user made pre-test before -> Go to Game
                         */
                        if (D.con.user_detay == 1)
                        {
                            //NOTICE: LATER IMPLEMENTATIONS WITH MODULS WILL ADDED LATER
                            //If User is on modul 0
                            if (D.con.giris_test == 1)
                            {
                                //Go to game scene
                                if (D.con.karakter == 0)
                                {
                                    //WILL BE UPDATED LATER
                                    CharSelection.SetActive(true);
                                    NextButton.SetActive(false);
                                    User_Detail_Panel.SetActive(false);
                                    TestGroup.SetActive(false);


                                }
                                else
                                {
                                    SceneManager.LoadScene("Game_Map");
                                }
                            }
                            else
                            {
                                //Go to pretest
                                PreTest.SetActive(true);
                                Start_Test_Panel.SetActive(false);
                                NextButton.SetActive(true);
                                User_Detail_Panel.SetActive(false);
                                TestGroup.SetActive(true);
                            }
                        }
                        //Else if for
                        // public string Modular_Data_Sender(string data,int state) BURAYA BAK
                        else
                        {
                            //Go to user_detail panel
                            PreTest.SetActive(true);
                            Start_Test_Panel.SetActive(true);
                            NextButton.SetActive(false);
                            User_Detail_Panel.SetActive(false);
                            TestGroup.SetActive(false);
                        }
                    }
                    //If response is came as 0, then bring up an error message
                    else
                    {
                        ErrorText.text = "Could not logged in to the system!";
                        StartCoroutine(ErrorButton_Waiter());
                    }
                }
                else
                {
                    //Error: Email must be filled correct!
                    //As an error message open a new panel with text on it
                    ErrorText.text = "Email area must be filled correctly!";
                    StartCoroutine(ErrorButton_Waiter());
                }
            }
            else
            {
                //Error: All boxes must be filled!
                //As an error message open a new panel with text on it
                ErrorText.text = "All areas must be filled!";
                StartCoroutine(ErrorButton_Waiter());
            }
            EmailBox_Login.text = "";
            PasswordBox_Login.text = "";
        }
    }

    //COMPLETED!!
    //Waits half minute for smooth transition
    //It will be used in 2 different operations
    //1)Changes on pages
    //2)Error message pop-ups
    IEnumerator Button_Waiter()
    {
        yield return new WaitForSeconds(0.5f);
    }

    //Error Button wait
    IEnumerator ErrorButton_Waiter()
    {
        ErrorPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        ErrorPanel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
    }
    //COMPLETED!!
    //Landing page's waiting effect method
    IEnumerator Landing_Fade()
    {
        yield return new WaitForSeconds(1);
        Landing.SetActive(false);
        Connect.SetActive(true);
        yield return 0;
    }
    //COMPLETED!!
    //Pre-Test rating value taker
    public void RatingButtonValueTaker(int button)
    {
        buttonvalue = button;
    }
    //COMPLETED!!
    //PreTest Yes-No value Taker
    public void YesNoButtonValueTaker(bool button)
    {
        if (button)
        {
            buttonstate = "Yes";
        }
        else
        {
            buttonstate = "No";
        }
        
    }
    //UNCOMPLETED
    //It will take the number of page the app on
    //Sends it to the method and then sets the active page with a
    //switch case operation
    public void Back_Button(int button)
    {
        switch (button)
        {
            //From Signin to Connect Page
            case 1:
                Signin.SetActive(false);
                Connect.SetActive(true);
                break;
            //From Signin to login Page
            case 2:
                Login.SetActive(false);
                Signin.SetActive(true);
                break;
            //From pretest to Login Page
            case 3:
                if (TestGroup.activeSelf)
                {
                    i--;
                    QuestionTextPlace.text = Questions[i];
                }
                else if (TestGroup.activeSelf && i==0)
                {
                    User_Detail_Panel.SetActive(true);
                    PreTest.SetActive(false);
                }
                else
                {
                    Start_Test_Panel.SetActive(true);
                    User_Detail_Panel.SetActive(false);
                }
                //WILL ADD LATER - FOR NOW WRONG IMPLEMENTATION
                //PreTest.SetActive(false);
                //Login.SetActive(true);
                break;
            //Char Selection Page
            case 4:
                //Char Selection to Connect screen
                CharSelection.SetActive(false);
                Connect.SetActive(true);
                break;
            //Game Screen page
            case 5:
                //Game Screen to char selection screen
                //OR just exit from game from in game menu
                Application.Quit();
                break;
        }
    }

    //Start button for scene change from UI to PlayArea
    public void StartGame()
    {
        SceneManager.LoadScene("Game_Map");
    }

    //NOT USED PART, SAVED FOR REFERENCE
    /*
        IEnumerator Dialog()
        {
            // ...
            var waitForButtonYesNo = new WaitForUIButtons(yesButton, noButton).ReplaceCallback(b => Debug.Log("Button with name " + b.name + " got pressed"));
            yield return waitForButtonYesNo.Reset();
            if (waitForButtonYesNo.PressedButton == yesButton)
            {
                // yes was pressed
                Next_Button();

            }
            else
            {
                // no was pressed
                NextButton.SetActive(true);
            }
        }
    */
}
