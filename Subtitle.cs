using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.Video;
public class Subtitle : MonoBehaviour
{  
    //IMPORTANT NOTE: A Subtitle class will be created soon
    //Subtitle as Sub => Sub.start & Sub.end & Sub.text will be used
    private string path;//Can change later
    [SerializeField] public static string Game_Path;
    //holds everything
    public string lines;

    #region Videos
    //Holds times in string format as combined
    public string lines2 = "";
    //Holds times as seperated but wrong 1st step of seperation
    public string[] times;
    //Holds times as seperated but wrong 2nd step of seperation
    public string times2 = "";
    //Holds times as correctly seperated
    //odd numbers will be starting times
    //even numbers will be ending times
    public string[] real_times;
    //Holds every line's time difference
    public string[] time_times;
    //Holds starting times of subtitles
    public string[] Sub_Start;
    //Holds Ending times of subtitles
    public string[] Sub_End;
    //Holds texts for subtitles
    public string[] Sub_Texts;
    //Holds how many different sub texts are available
    public int Loop;
    //The Subtitle Text on UI Panel
    public Text Subtitle_UI_Element;
    //Readed Textfile for subtitles
    public TextAsset Subtitle_Textfile;
    


    public VideoPlayer Player;
    public GameObject  VideoPanel;
    public GameObject Subtitles;
    #endregion

    #region Quiz
    //Readed Textfile for quizzes
    public TextAsset Test_Textfile;
    //Questions of Quizzes
    public string[] Questions = { "", "", "", "", "" };
    //Answers of Quizzes
    public string[] Answers_A = { "","","","",""};
    public string[] Answers_B = { "", "", "", "", "" };
    public string[] Answers_C = { "", "", "", "", "" };
    public string[] Answers_D = { "", "", "", "", "" };
    //Text Places for Questions and Answers
    public Text Question_Text;
    public Text Answer_A_Text;
    public Text Answer_B_Text;
    public Text Answer_C_Text;
    public Text Answer_D_Text;

    public int Button_Index = 4;
    public int[] User_Answers =
    { 4,4,4,4,4 };
    public int[] Real_Answers =
    { 4,4,4,4,4,
      3,0,1,0,1,
      1,1,3,0,3,
      2,0,3,3,3,
      2,2,1,1,0  };


    public GameObject QuizPanel;

    public int modul;
    #endregion

    #region Reading
    public GameObject ReadingPanel;
    public GameObject ReadingGroup1;
    public GameObject ReadingGroup2;
    public GameObject ReadingGroup3;
    public GameObject ReadingGroup4;
    public GameObject ReadingGroup5;


    public string[] Reading_Links =
    {   "",
        "",
        "",
        "",
        "https://www.gsb.stanford.edu/faculty-research/centers-initiatives/csi/defining-social-innovation",
        "https://www.mitpressjournals.org/doi/pdf/10.1162/itgg.2006.1.2.145",
        "https://www.ukessays.com/essays/cultural-studies/role-of-sport-in-modern-society-cultural-studies-essay.php",
        "https://www.peace-sport.org/opinion/athletes-leading-social-change-through-sports/",
        "https://www.youtube.com/watch?v=vhsYf__KQcE",
        "https://www.youtube.com/watch?v=gc7CAdDoBGU",
        "https://www.researchgate.net/publication/318033203",
        "https://www.un.org/esa/socdev/rwss/2016/executive-summary.pdf",
        "https://www.sportsthinktank.com/uploads/women-on-boards-gender-balance-in-sport-report-july-2014-3.pdf",
        "http://www.ews-online.org",
        "http://www.olympic.org/women-in-sport-commission",
        "https://www.coe.int/t/dg4/epas/resources/texts/INF25%20Gender%20equality%20and%20elite%20sport.pdf",
        "https://sportsplanningguide.com/volunteers-key-ingredient-successful-sports-event/",
        "https://www.tandfonline.com/doi/full/10.1080/19407960903204356",
        "http://www.pseudology.org/terovanesian/Masterman_Strategic_Sports_Event_Management2.pdf",
        "https://www.rand.org/content/dam/rand/pubs/research_reports/RR2800/RR2804/RAND_RR2804.pdf"
    };
    #endregion

    #region Interactive_Tasks
    public GameObject InteractivePanel;

    public Text Interactive_Text;

    public InputField Interactive_Input;

    public string[] Interactive_Questions;
    public string[] Interactive_Answers;
    public int Interactive;
    public int Interactive_End;



    public string[] Interactive_Links =
    {
    "",
    "",

    "https://www.engso.eu/post/sports-clubs-for-social-change-embracing-innovation",
    "http://willmarre.com/nike/",
    "https://www.sportanddev.org/en/article/news/social-innovation-through-sport",

    "https://www.olympic.org/about-ioc-olympic-movement",
    "https://www.fifa.com/what-we-do/fifa-foundation/overview/",

    "https://ec.europa.eu/sport/policy/society/gender_en",
    "https://vimeo.com/359476890",

    "https://www.youtube.com/watch?v=xInABTGavt8",
    "https://ec.europa.eu/citizenship/pdf/council_conclusions_on_volunteering_in_sport_en.pdf"
    };

    #endregion


    //Will be used for Data services
    DataServices DS;
    public GameObject Char1;
    public GameObject Char2;
    public GameObject Char3;

    Language_Game L_G;

    public string CurrentLanguage = "EN";
    

    public float Total_Token;


    private void Awake()
    {
        DS = GameObject.Find("DataServices").GetComponent<DataServices>();
        //DS = GameObject.Find("DataServices").GetComponent<DataServices>();
        L_G = GameObject.Find("LanguageGame").GetComponent<Language_Game>();

        Player.isLooping = false;
        Player.playOnAwake = false;
        //BELOW PART IS WORKING CORRECTLY HOWEVER NEEDS MORE IMPROVEMENTS

        //Set language as saved language to the local
        if (DS.game.Language == 1)
        {
            CurrentLanguage = "EN";
            L_G.Change_Language(1);
        }
        else if (DS.game.Language == 2)
        {
            CurrentLanguage = "TR";
            L_G.Change_Language(2);
        }
        if (DS.con.karakter == 1)
        {
            Char1.SetActive(true);
        }
        else if (DS.con.karakter == 2)
        {
            Char2.SetActive(true);
        }
        else
        {
            Char3.SetActive(true);
        }
    }
    //Just a variable
    public int i = 0;
    public void Start()
    {
       

    }

    public void Update()
    {
        //if language is changed in DataServices
        
    }

    //NOT COMPLETED
    //WILL WORK LATER
    //If videoplaying is cancelled in midway
    //Dont give points to the user
    //Stop the video player seperately
    public void CancelVideo()
    {
        StopAllCoroutines();
        VideoPanel.SetActive(false);
        Subtitles.SetActive(false);
        Player.Stop();
        Screen.orientation = ScreenOrientation.Portrait;
    }
    //NOT COMPLETED
    //WILL WORK LATER
    //String can be send through here so we can just find the video
    public void GetBillboardButton()
    {
        //get player controller object
        Player_Controller P = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        RaycastHit hit = P.hit;
        //DETECT WHICH MODULE IS TRIGGER
        string Module_Name = hit.collider.gameObject.name;
        switch (Module_Name)
        {
            //Video Modules
            case "billboard_1_1":
                //Call video reader as
                //Start datetime chronometer
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module1/en_introductionvideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 1, 1);
                Screen.orientation = ScreenOrientation.Portrait;
                //Stop chronometer when user clicks the button
                //if the time taken since last point is greater than time needed
                //create a button and give the ball amount to the user 
                break;
            case "billboard_1_2":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module1/en_coursevideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 1, 2);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "billboard_2_1":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module2/en_introductionvideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 2, 1);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "billboard_2_2":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module2/en_coursevideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 2, 2);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "billboard_3_1":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module3/en_introductionvideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 3, 1);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "billboard_3_2":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module3/en_coursevideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 3, 2);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "billboard_4_1":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module4/en_introductionvideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 4, 1);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "billboard_4_2":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module4/en_coursevideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 4, 2);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "billboard_5_1":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module5/en_introductionvideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 5, 1);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case "billboard_5_2":
                Screen.orientation = ScreenOrientation.Landscape;
                VideoPointEarning("https://kastanjetextile.com/static/esvolVideos/Module5/en_coursevideo1.mp4");
                Reader_of_Subtitles(CurrentLanguage, 5, 2);
                Screen.orientation = ScreenOrientation.Portrait;
                break;
                
            //Test Modules
            case "clipboard_1_1":
                modul = 1;
                QuizPanel.SetActive(true);
                Reader_of_Quizzes(CurrentLanguage, 1);
                break;
            case "clipboard_2_1":
                modul = 2;
                QuizPanel.SetActive(true);
                Reader_of_Quizzes(CurrentLanguage, 2);
                break;
            case "clipboard_3_1":
                modul = 3;
                QuizPanel.SetActive(true);
                Reader_of_Quizzes(CurrentLanguage, 3);
                break;
            case "clipboard_4_1":
                modul = 4;
                QuizPanel.SetActive(true);
                Reader_of_Quizzes(CurrentLanguage, 4);
                break;
            case "clipboard_5_1":
                modul = 5;
                QuizPanel.SetActive(true);
                Reader_of_Quizzes(CurrentLanguage,5);
                break;

            //Reading Modules
            case "book_1_1":
                ReadingPanel.SetActive(true);
                //Open Reading Panel1
                ReadingGroup1.SetActive(true);
                break;
            case "book_2_1":
                ReadingPanel.SetActive(true);
                //Open Reading Panel2
                ReadingGroup2.SetActive(true);
                break;
            case "book_3_1":
                ReadingPanel.SetActive(true);
                //Open Reading Panel3
                ReadingGroup3.SetActive(true);
                break;
            case "book_4_1":
                ReadingPanel.SetActive(true);
                //Open Reading Panel4
                ReadingGroup4.SetActive(true);
                break;
            case "book_5_1":
                ReadingPanel.SetActive(true);
                //Open Reading Panel5
                ReadingGroup5.SetActive(true);
                break;

            //Interactive Tasks Modules
            case "notepad_1_1":
                InteractivePanel.SetActive(true);
                Interactive_Tasks_Reader(1);
                break;
            case "notepad_2_1":
                InteractivePanel.SetActive(true);
                Interactive_Tasks_Reader(2);
                break;
            case "notepad_3_1":
                InteractivePanel.SetActive(true);
                Interactive_Tasks_Reader(3);
                break;
            case "notepad_4_1":
                InteractivePanel.SetActive(true);
                Interactive_Tasks_Reader(4);
                break;
            case "notepad_5_1":
                InteractivePanel.SetActive(true);
                Interactive_Tasks_Reader(5);
                break;
        }

    }

    //HALF COMPLETED
    //WILL WORK LATER
    //Send points to the user
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //Earn 2 points for every minutes = (Totalminutes + 1) * 2 => Earned points
        Total_Token = Total_Token * 2f;
        //SEND THE DATA to GAME as tokens
        //CALL DataServices.PointGainandSend(10=[as points]);
        DS.PointGainandSend((int)Total_Token);
        Total_Token = 0;
    }
    public IEnumerator GetTime(DateTime x)
    {
        x = Convert.ToDateTime(System.DateTime.Now);
        yield return new WaitForSeconds(0);

    }
    //HALF COMPLETED!
    //NOT TESTED
    void VideoPointEarning(string URL)
    {
        //FIND WHICH VIDEO IS GOING TO PLAYING
        //Player.clip = Resources.Load<VideoClip>("Videos/" + modul + '_' + video);
        Player.url = URL;
        //START CANVAS VIDEO PANEL
        VideoPanel.SetActive(true);
        Subtitles.SetActive(true);
        Player.prepareCompleted += ShowTimeDifference;
        //WATCH THE VIDEO
        Player.Play();

        //WHEN THE VIDEO ENDS GET POINTS
        Player.loopPointReached += EndReached; //====>>> Call a method which gives points to the user
    }
    //NOT COMPLETED
    //WILL WORK LATER
    //Earn points by watching video fully
    void VideoPointEarning(int modul, int video)
    {
        //FIND WHICH VIDEO IS GOING TO PLAYING
        Player.clip = Resources.Load<VideoClip>("Videos/" + modul + '_' + video);
        //START CANVAS VIDEO PANEL
        VideoPanel.SetActive(true);
        Subtitles.SetActive(true);
        //WATCH THE VIDEO
        Player.Play();
        //WHEN THE VIDEO ENDS GET POINTS
        Player.loopPointReached += EndReached; //====>>> Call a method which gives points to the user
    }

    

    //SUBTITLE READER
    //Take the subtitles from source
    //Get time differences of every subtitle
    public void Reader_of_Subtitles(string lang,int modul,int video)
    {
        /*
         * @param lang The language string for video subtitle
         * @param modul The module number for video subtitle
         * @param video The video number for video subtitle
         */
        Subtitle_Textfile = Resources.Load<TextAsset>(lang + '/' + lang + '_' + modul + '_' + video);
        //Subtitle reader
        /*
         * Lines[i*3] = Time stamps
         * Lines[i*3+1] = The Texts that will be shown as subtitles
         * Lines[i*3+2] = Nothing
         */
        string file = Subtitle_Textfile.text.ToString();
        string[] lines = file.Split('\n');
        //Find mathematical solution for loop amounts
        if(lines.Length%3 == 0) Loop = lines.Length / 3;
        else Loop = (lines.Length / 3) + 1;

        for (i = 0; i < Loop; i++)
        {
            lines2 = lines2 + lines[i*3] + "\n";
        }
        //Dynamic Array Size Set
        Sub_Texts = new string[Loop];
        for (i = 0; i < Loop; i++)
        {
            Sub_Texts[i] = lines[i * 3 + 1] + "\n";
        } 
        times = Regex.Split(lines2, " --> ");
        //DİKKAT NOKTASI
        for (i = 0; i < times.Length; i++) times2 = times2 + times[i] + "\n";
        if(real_times == null)
        real_times = Regex.Split(times2, "\n");
        else
        {
            real_times = null;
            real_times = Regex.Split(times2, "\n");
        }

        Sub_Start = new string[Loop];
        Sub_End = new string[Loop];
        int j = 0, k = 0;
        //Subtitle Taker
        i = 0;
        while( real_times[i] != "")
        {
            //if i = 1 it means start
            //else i = 2 it means end
            switch (i%2)
            {
                case 0:
                    Sub_Start[j] = real_times[i];
                    j++;
                    break;

                case 1:
                    Sub_End[k] = real_times[i];
                    k++;
                    break;
            }
            i++;

        }
    }
    //SUBBER
    //Start the Subtitle Sequence
    //For now only waits for the sub end
    public IEnumerator Subber()
    {
        DateTime t_start = Convert.ToDateTime(System.DateTime.Now);
        for (int j = 0; j < Loop; j++)
        {
            TimeSpan ts;
            DateTime T_S = Convert.ToDateTime((Sub_Start[j]));
            DateTime T_E = Convert.ToDateTime((Sub_End[j]));
            ts = T_E - T_S;
            Subtitle_UI_Element.text = Sub_Texts[j];
            float f = (float)(ts.TotalSeconds);
            //Wait for next subtitle appear
            yield return new WaitForSeconds(f);
        }
        DateTime t_end = Convert.ToDateTime(System.DateTime.Now);
        Total_Token = (float)((t_end-t_start).TotalMinutes) + 1f;
        yield return new WaitForSeconds(0.1f);

    }
    //Will hold the time to change and print subtitles on the screen
    public void ShowTimeDifference(UnityEngine.Video.VideoPlayer vp)
    {
        StartCoroutine(Subber());
    }

    /*public IEnumerator WaitForVideoEnds()
    {
            for (int j = 0; j < Loop; j++)
            {
                TimeSpan ts;
                DateTime T_S = Convert.ToDateTime((Sub_Start[j]));
                DateTime T_E = Convert.ToDateTime((Sub_End[j]));
                ts = T_E - T_S;
                Subtitle_UI_Element.text = Sub_Texts[j];
                float f = (float)(ts.TotalSeconds);
                //Wait for next subtitle appear
                yield return new WaitForSeconds(f);
            }
    }*/

    public void Back_Button(int button)
    {
        if (QuizPanel.activeInHierarchy) QuizPanel.SetActive(false);
        else if (VideoPanel.activeInHierarchy) VideoPanel.SetActive(false);
        else if (ReadingPanel.activeInHierarchy)  ReadingPanel.SetActive(false);
        else InteractivePanel.SetActive(false);
        modul = 0;
    }

    //BURADA KALDIM!!!!
    //SUBTITLE READER
    //Take the subtitles from source
    //Get time differences of every subtitle
    public void Reader_of_Quizzes(string lang, int modul)
    {
        /*
         * @param lang The language string for quiz texts
         * @param modul The module number for quizzes
         */
        Test_Textfile = Resources.Load<TextAsset>(lang + '/' + lang + '_' + modul + '_' + 'Q');
        //Text reader
        /*
         * Lines[i*5] = Questions
         * Lines[i*5+1,+2,+3,+4] = The Answers of questions
         */
        string file = Test_Textfile.text.ToString();
        //string file = Resources.Load<TextAsset>(lang + '/' + lang + '_' + modul + '_' + 'Q').ToString();
        //Split them with '!' sign
        string[] lines = file.Split('!');

        //Defines every loop => 5 lines = 1 question and 4 answer lines
        if (lines.Length % 5 == 0) Loop = (lines.Length / 5) + 1;
        else Loop = lines.Length / 5;

        #region Set Questions
        //Define the size of Questions Dynamic Array


        //WARNING ERRORS ARE BELOW!!!
        //Questions = new string[5];
        for (i = 0; i < Loop; i++)
        {
            Questions[i] = lines[i * 5] + "\n";
        }
        #endregion
        #region Set Answers
        //Define the size of Answers Dynamic Arrays
        //Answers_A = new string[Loop];
        //Answers_B = new string[Loop];
        //Answers_C = new string[Loop];
        //Answers_D = new string[Loop];
        for (i = 0; i < Loop; i++)
        {
            Answers_A[i] = lines[i * 5 + 1] + "\n";
            Answers_B[i] = lines[i * 5 + 2] + "\n";
            Answers_C[i] = lines[i * 5 + 3] + "\n";
            Answers_D[i] = lines[i * 5 + 4] + "\n";
        }
        #endregion
        i = 0;
        Question_Text.text = Questions[i].ToString();
        Answer_A_Text.text = Answers_A[i].ToString();
        Answer_B_Text.text = Answers_B[i].ToString();
        Answer_C_Text.text = Answers_C[i].ToString();
        Answer_D_Text.text = Answers_D[i].ToString();
    }

    //HALF COMPLETED!
    //Next button event button
    public void Answer_Collector()
    {
        if(Button_Index != 4)
        {
            switch (Button_Index)
            {
                case 0:
                    User_Answers[i] = Button_Index;
                    break;

                case 1:
                    User_Answers[i] = Button_Index;
                    break;

                case 2:
                    User_Answers[i] = Button_Index;
                    break;

                case 3:
                    User_Answers[i] = Button_Index;
                    break;
            }
            //Get the Answer => Which button is clicked?
            //Get the next question and answers
            i++;
            //When button is clicked get the next question and answers
            if (Questions.Length > i)
            {
                Question_Text.text = Questions[i].ToString();
                Answer_A_Text.text = Answers_A[i].ToString();
                Answer_B_Text.text = Answers_B[i].ToString();
                Answer_C_Text.text = Answers_C[i].ToString();
                Answer_D_Text.text = Answers_D[i].ToString();
            }
            else
            {
                //Abort the system, because we answered all questions
                //Compare the answers user gave with we have got

                //Close panel and continue on game
                QuizPanel.SetActive(false);
            }
        }
        else
        {

        }
        Button_Index = 4;
        if(i == Questions.Length)
        {
            TestPointEarning();
        }
    }

    //HALF COMPLETED!
    //WILL WORK LATER
    //Earn points by completing tests
    //Read by ReadAllText and then seperate it into different types
    void TestPointEarning()
    {
        int j;
        //(modul-1) * 4 = Range of questions
        //For modul 2 => (2-1) * 4 = 4,5,6 and 7 is modul 2's questions
        int point_amount=0;
        //(MODUL - 1) * 5 => i
        i = (modul - 1) * 5;
        for (j = 0; j < Questions.Length; j++)
        {
            if(Real_Answers[i] == User_Answers[j])
            {
                //Earn a point
                point_amount++;
            }
            else
            {
                //Not earn a point
            }
            i++;
        }
        point_amount = point_amount * 2;
        DS.PointGainandSend(point_amount);
    }
    //COMPLETED !!
    public void ButtonIndexer(int button)
    {
        Button_Index = button;
    }
    //COMPLETED !!
    public void Reading_Modules(int button)
    {
        string URL = Reading_Links[button];
        Application.OpenURL(URL);
        ReadingPanel.SetActive(false);
    }

    //NOT COMPLETED
    //First release the question
    //Then forward them to link
    //then get answer and save it
    //After all answers given, send them to server
    //Earn points
    public void Interactive_Tasks_Reader(int modul)
    {
        string Quests = Resources.Load<TextAsset>(CurrentLanguage + '/' + CurrentLanguage + "_InteractiveTasks").text;
        Interactive_Questions = Quests.Split('!');
        switch (modul)
        {
            case 1:
                Interactive = 0;
                Interactive_End = 2;
                break;
            case 2:
                Interactive = 2;
                Interactive_End = 5;
                break;
            case 3:
                Interactive = 5;
                Interactive_End = 7;
                break;
            case 4:
                Interactive = 7;
                Interactive_End = 9;
                break;
            case 5:
                Interactive = 9;
                Interactive_End = 11;
                break;
        }
        Interactive_Text.text = Interactive_Questions[Interactive];
        Interactive_Answers = new string[Interactive_End - Interactive];
    }

    //NOT COMPLETED
    public void Interactive_Answer_Get()
    {
        /* Question system
         * modul1 = 0,1
         * modul2 = 2,3,4
         * modul3 = 5,6
         * modul4 = 7,8
         * modul5 = 9,10
         */
        if (Interactive_Input.text != "")
        {
            //Do your job
            Interactive_Answers[Interactive_Answers.Length - (Interactive_End - Interactive)] = Interactive_Input.text.ToString();
            Interactive++;
            Interactive_Text.text = Interactive_Questions[Interactive];
            Interactive_Input.text = "";
        }
        else
        {
            //Do nothing

        }
        //IF THE LAST QUESTION IS NOT ANSWERED
        if (Interactive == Interactive_End)
        {
            //Do your job and send data to server
            //DATA SENDING PART!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            //GET POINTS PART!!!!!!
            //if user send answers before, give no points and warn them!!!!
            //else give points
            DS.PointGainandSend(Interactive_Answers.Length * 2);
            //At the end close the panel
            InteractivePanel.SetActive(false);
        }
        //IF THE LAST QUESTION IS NOT ANSWERED
        else
        {
            //Do nothing
        }
        
    }
    //COMPLETED !!
    public void Interactive_Link()
    {
        Application.OpenURL(Interactive_Links[Interactive]);
    }

    

}
