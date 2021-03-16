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

    public void Update()
    {
        //if(Player.loopPointReached == true)
        
    }

    void PointGain(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }

    //Just a variable
    public int i = 0;
    public void Start()
    {
        //NOT USED ANYMORE, SAVED FOR LATER PURPOSES
        //Game_Path = Application.persistentDataPath;
        //path = Game_Path + "/TR_1_1.vtt";
        Reader("TR", 1, 1);
        ShowTimeDifference();
    }
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
    //Start the Subtitle Sequence
    //For now only waits for the sub end
    //Working correctly
    public IEnumerator Subber()
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
    //Will hold the time to change and print subtitles on the screen
    public void ShowTimeDifference()
    {
        StartCoroutine(Subber());
    }

}
