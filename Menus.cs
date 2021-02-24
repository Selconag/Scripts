using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    //Other Classes
    Deserializer D;
    //UI Panels of Menu
    public GameObject Landing;
    public GameObject Connect;
    public GameObject Signin;
    public GameObject Login;
    public GameObject StartTest;
    public GameObject PreTest;
    public GameObject GroupX;
    public GameObject GroupY;
    //Special Texts
    public Text QuestionTextPlace;
    //Variables
    private int i = 0;
    //Special Texts
    public GameObject PreQuestions;
    //Buttons
    public Button yesButton;
    public Button noButton;
    //Images
    public Image Rainbow;
    public Image Rainbow2;
    public GameObject NextButton;
    //TextBoxes
    public Text NameBox;
    public Text EmailBox;
    public Text PasswordBox;


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
    //Questions answers will be stored in here came from Pre-test
    private string[] Answers = new string[9];
    void Start()
    {
        Landing.SetActive(true);
        StartCoroutine(Landing_Fade());
    }
    
    //UNCOMPLETED
    //Play button behaivour region from Connect Panel
    public void Play_Button()
    {
        Button_Waiter();
        Connect.SetActive(false);
        Signin.SetActive(true);
    }

    //UNCOMPLETED
    //Connect button behaivour region from Connect Panel
    public void Connect_Button()
    {
        //Serialization
        string Local_User = D.GetLocalUserData();
        Button_Waiter();
        if (!(Local_User ==  "No"))
        {
            Connect.SetActive(false);

            /*
             If user made pre-test before -> Go to Game
             Else hop on the PreTests Page(Even if half of questions are answered, we still reset the situation)
             */
            if (Local_User.Contains("Done"))//WİLL CHANGE
            {
                //GamePage.SetActive(true);
                Connect.SetActive(false); //FOR NOW!!
            }
            else
            {
                StartTest.SetActive(true);
            }
            
        }
        else
        {
            //There is no Local save please first log in
        }
        
    }

    //UNCOMPLETED
    //Signin button behaivour region from Signin Panel
    public void Signin_Button()
    {
        //If user inputs are fully given
        if (NameBox.text.Length > 0 && EmailBox.text.Length > 0 && PasswordBox.text.Length > 0)
        {
            //If email contains both "."(dots) and "@"(at) chars, accept email 
            if (EmailBox.text.Contains("@") && EmailBox.text.Contains("."))
            {
                string name = NameBox.text.ToString();
                string email = EmailBox.text.ToString();
                string password = PasswordBox.text.ToString();
                //send data to server
            }
            else
            {
                //Error: Email must be filled correct!
                //As an error message open a new panel with text on it
                //Disappear it after 0.5 minutes?
            }

        }
        else
        {
            //Error: All boxes must be filled!
            //As an error message open a new panel with text on it
            //Disappear it after 0.5 minutes?
            //Texts are not disappeared
        }
    //
    Signin.SetActive(false);
        Connect.SetActive(true);
    }

    //UNCOMPLETED
    //Login button behaivour region from Signin Panel
    public void Login_Button()
    {
        if (Signin.activeSelf)
        {
            Button_Waiter();
            Signin.SetActive(false);
            Login.SetActive(true);
        }
        else
        {
            //SISTEME GIRIS YAPACAK
            //...


            //
            Login.SetActive(false);
            StartTest.SetActive(true);
        }
    }

    //UNCOMPLETED
    //StartTest button behaivour region from StartPreTest Panel
    public void StartTest_Button()
    {
        Button_Waiter();
        StartTest.SetActive(false);
        PreTest.SetActive(true); 
      //  Tests();

    }

    //UNCOMPLETED
    //??? button behaivour region from PreTest Panel
    public void Tests()
    {
        if (GroupX.activeSelf)//NEXT Butonuna tıklanınca
        {
            //Cevapları al ve gönder veya kaydet
            Button_Waiter();
            GroupX.SetActive(false);
            GroupY.SetActive(true);
        }
        else
        {
            
            for (i = 0; i < 9; i++)
            {
                //Sonraki soruyu al
                QuestionTextPlace.text = Questions[i];
                Dialog();
                
            }
            //Cevabı al ve yolla

            //sonraki soruya geçmek için sayacı arttır
            i++;
        }
        
        
    }

    //UNCOMPLETED
    //Used for copy_paste operations
    public void Nuller()
    {
        Debug.Log("Hello");
    }

    //Next button behaivour region from all Pretest Panels
    public void Next_Activator()
    {
        NextButton.SetActive(true);
    }




    IEnumerator Dialog()
    {
        // ...
        var waitForButtonYesNo = new WaitForUIButtons(yesButton, noButton).ReplaceCallback(b => Debug.Log("Button with name " + b.name + " got pressed"));
        yield return waitForButtonYesNo.Reset();
        if (waitForButtonYesNo.PressedButton == yesButton)
        {
            // yes was pressed
            Next_Activator();

        }
        else
        {
            // no was pressed
            NextButton.SetActive(true);
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
}
