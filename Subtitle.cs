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

    DataServices DS;

    public string CurrentLanguage = "TR";

    public float Total_Token;

    //Just a variable
    public int i = 0;
    public void Start()
    {
        DS = GameObject.Find("Something").GetComponent<DataServices>();
        //DS = GameObject.Find("DataServices").GetComponent<DataServices>();

        //NOT USED ANYMORE, SAVED FOR LATER PURPOSES
        //Game_Path = Application.persistentDataPath;
        //path = Game_Path + "/TR_1_1.vtt";
        Player.isLooping = false;
        Player.playOnAwake = false;
        //BELOW PART IS WORKING CORRECTLY HOWEVER NEEDS MORE IMPROVEMENTS
    }

    public void Update()
    {
        //if back button is pressed, cancel anything
        
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
                VideoPointEarning(1, 1);
                Reader(CurrentLanguage, 1, 1);
                ShowTimeDifference();

                break;
            case "billboard_1_2":
                VideoPointEarning(1, 2);

                Reader(CurrentLanguage, 1, 2);
                ShowTimeDifference();

                break;
            case "billboard_2_1":
                VideoPointEarning(2, 1);

                Reader(CurrentLanguage, 2, 1);
                ShowTimeDifference();

                break;
            case "billboard_2_2":
                VideoPointEarning(2, 2);

                Reader(CurrentLanguage, 2, 2);
                ShowTimeDifference();

                break;
            case "billboard_3_1":
                VideoPointEarning(3, 1);

                Reader(CurrentLanguage, 3, 1);
                ShowTimeDifference();

                break;
            case "billboard_3_2":
                VideoPointEarning(3, 2);

                Reader(CurrentLanguage, 3, 2);
                ShowTimeDifference();

                break;
            case "billboard_4_1":
                VideoPointEarning(4, 1);

                Reader(CurrentLanguage, 4, 1);
                ShowTimeDifference();

                break;
            case "billboard_4_2":
                VideoPointEarning(4, 2);

                Reader(CurrentLanguage, 4, 2);
                ShowTimeDifference();

                break;
            case "billboard_5_1":
                VideoPointEarning(5, 1);

                Reader(CurrentLanguage, 5, 1);
                ShowTimeDifference();

                break;
            case "billboard_5_2":
                VideoPointEarning(5, 2);

                Reader(CurrentLanguage, 5, 2);
                ShowTimeDifference();

                break;
                //Test Modules


                //Reading Modules


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

    //NOT COMPLETED
    //WILL WORK LATER
    //Earn points by watching video fully
    void VideoPointEarning(int modul, int video)
    {
        //FIND WHICH VIDEO IS GOING TO PLAYING
        Player.clip = Resources.Load<VideoClip>("Videos/" + modul + '_' + video);
        //FIND ITS LENGTH, NOT USED ANYMORE
        //double x = Resources.Load<VideoClip>("Videos/" + modul + '_' + video).length;
        //Look to Subber()=> Total_Token float variable, holds minutes

        //START CANVAS VIDEO PANEL
        VideoPanel.SetActive(true);
        Subtitles.SetActive(true);
        //WATCH THE VIDEO
        Player.Play();
        //WHEN THE VIDEO ENDS GET POINTS
        Player.loopPointReached += EndReached; //====>>> Call a method which gives points to the user
    }

    //NOT COMPLETED
    //WILL WORK LATER
    //Earn points by completing tests
    void TestPointEarning(int region)
    {
        //START CANVAS TEST PANEL

        //GET QUESTIONS AND ANSWERS

        //DO THE TEST AND GIVE POINTS

        //SEND THE DATA 

        //CALL DataServices.PointGainandSend(10=[as points]);

    }

    //SUBTITLE READER
    //Take the subtitles from source
    //Get time differences of every subtitle
    public void Reader(string lang,int modul,int video)
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
    public void ShowTimeDifference()
    {
        StartCoroutine(Subber());
    }

    public IEnumerator WaitForVideoEnds()
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
    }

}
