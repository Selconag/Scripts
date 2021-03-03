using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Library;
public class Menus : MonoBehaviour
{
    //Other Classes
    DataServices D;
    //UI Panels of Menu
    public GameObject Landing;
    public GameObject Connect;
    public GameObject Signin;
    public GameObject Login;
    public GameObject PreTest;
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
    //Buttons
    public Button yesButton;
    public Button noButton;
    //Images
    public Image Rainbow;
    public Image Rainbow2;
    public GameObject NextButton;
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
    //Error Panel
    public GameObject ErrorPanel;
    public Text ErrorText;
    //Game Scene

    //TESTING NEW OUTCOMES
    

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
        //UNCOMPLETED
        //Check IF a local save data is here
        //If:Connect Button appears

        //Else:Connect button fades away
    }

    void Start()
    {
        QuestionTextPlace.text = Questions[i];
        Landing.SetActive(true);
        StartCoroutine(Landing_Fade());
        //Check IF a local save data is here
        D = GetComponent<DataServices>();
        

    }

    //COMPLETED!!
    //Forgot password sends email to the server
    //the rest is up to server
    //Calls a ForgotPassword method from DataService
    public void ForgotPasswordButton()
    {
        D.mail.email = EmailBox_Login.text.ToString();
        D.ForgotPassword(D.mail);
    }

    //UNCOMPLETED
    //Pre_Test Next button behaivour region from PreTest Panel
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
            //Cevapları al ve gönder
            D.user.age = Age.text.ToString();
            D.user.country = Gender.text.ToString();
            D.user.occupation = Occupation.text.ToString();
            D.user.prof_exp = ProfExp.text.ToString();
            D.user.security = D.con.security;
            D.user.user_id = D.con.user_id;
            D.BuildNewSerialization(D.user);
            User_Detail_Panel.SetActive(false);
            TestGroup.SetActive(true);
            QuestionTextPlace.text = Questions[i];
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
                        D.SendTestData(D.pre);
                        break;
                }
                //Increment the counter to get next question
                i++;
                QuestionTextPlace.text = Questions[i];
            }
            buttonstate = null;
            buttonvalue = 0;
        }

    }


    //COMPLETED!!
    //Play button behaivour region from Connect Panel
    public void Play_Button()
    {
        Button_Waiter();
        Connect.SetActive(false);
        Signin.SetActive(true);
    }
    

    //HALF COMPLETED ! NEED TO BE CHECKED
    //Connect button behaivour region from Connect Panel
    //Reads local Connection.json data and gets 2 variables
    //gets user_id and security, sends the data and auto-connects to server
    //then logs-in to app and continues on where the person left
    public void Connect_Button()
    {
        //Get local Data
        string Local_User = D.GetLocalUserData();
        Button_Waiter();
        if (!(Local_User == "No"))
        {
            Connect.SetActive(false);
            /*
             If user not made pre-test before -> Go to PreTest
             Else user made pre-test before -> Go to Game
             */
            if (Local_User.Contains("user_detay:1"))
            {
                if (Local_User.Contains("giris_test:1"))
                {
                    //Go to game scene
                    SceneManager.LoadScene("Basketbol");
                }
                else
                {
                    //Go to pretest
                    PreTest.SetActive(true);
                    TestGroup.SetActive(true);
                }
            }
            else
            {
                //Go to user_detail panel
                PreTest.SetActive(true);
                Start_Test_Panel.SetActive(true);
            }
        }
        else
        {
            //There is no Local save please first log in
            StartCoroutine(ErrorButton_Waiter());
        }

    }

    //HALF COMPLETED ! NEED TO BE CHECKED
    //Signin button behaivour region from Signin Panel
    public void Signin_Button()
    {
        //If: We are on the login page => Go to Signin page
        if (!Signin.activeSelf)
        {
            Signin.SetActive(true);
            Login.SetActive(false);
        }
        //If user inputs are fully given
        if (NameBox.text.Length > 0 && EmailBox.text.Length > 0 && PasswordBox.text.Length > 0)
        {
            //If email contains both "."(dots) and "@"(at) chars, accept email 
            if (((EmailBox.text).ToString()).Contains("@") && ((EmailBox.text).ToString()).Contains("."))
            {
                D.user.name = NameBox.text.ToString();
                D.user.email = EmailBox.text.ToString();
                D.user.password = PasswordBox.text.ToString();
                //send data to server
                D.BuildNewSerialization(D.user);

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

    //HALF COMPLETED ! LAST CHECKS WILL BE MADE SOON
    //Login button behaivour region from Signin Panel
    public void Login_Button()
    {
        //If: We are on the sign-in page => Go to Login page
        if (Signin.activeSelf)
        {
            Signin.SetActive(false);
            Login.SetActive(true);
        }
        //Else: Continue on process
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
                    D.LoginManuel(D.user);
                    //MİLLETİANS AT WORK !!
                    //At here the system will get user info to know which page must direct the client
                    string X = D.GetLocalUserData();
                    //string X = "";
                    if (!(X == "No"))
                    {
                        Login.SetActive(false);
                        /*
                         If user not made pre-test before -> Go to PreTest
                         Else user made pre-test before -> Go to Game
                         */
                        if (X.Contains("user_detay:1"))
                        {
                            //NOTICE: LATER IMPLEMENTATIONS WITH MODULS WILL ADDED LATER
                            //If User is on modul 0
                            if (X.Contains("giris_test:1"))
                            {
                                //Go to game scene
                                SceneManager.LoadScene("Basketbol");
                            }
                            else
                            {
                                //Go to pretest
                                PreTest.SetActive(true);
                                TestGroup.SetActive(true);
                            }
                        }
                        else
                        {
                            //Go to user_detail panel
                            PreTest.SetActive(true);
                            Start_Test_Panel.SetActive(true);
                        }
                    }
                    else
                    {
                        //There is no Local save please first log in
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
        Debug.Log("Connected");

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
                break;
            //Game Screen page
            case 5:
                //Game Screen to char selection screen
                break;
        }
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
