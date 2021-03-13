using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
public class Subtitle : MonoBehaviour
{  
    //IMPORTANT NOTE: A Subtitle class will be created soon
    //Subtitle as Sub => Sub.start & Sub.end & Sub.text will be used
    private string path;//Can change later
    [SerializeField] public static string Game_Path;
    public string lines;
    public string lines2 = "";
    public string[] times;
    public string times2 = "";
    public string[] real_times;
    public string[] Sub_Start;
    public string[] Sub_End;
    public string[] Sub_Texts;

    public Text Subt;

    int i = 0;
    public void Start()
    {
        
        Game_Path = Application.persistentDataPath;
        path = Game_Path + "/Sub1.vtt";
       
    }
    //Take the subtitles from source
    public void Reader()
    {
        //Subtitle reader
        /*
         * Lines[i*3] = Time stamps
         * Lines[i*3+1] = The Texts that will be shown as subtitles
         * Lines[i*3+2] = Nothing
         */
        string file = System.IO.File.ReadAllText(path);
        string[] lines = file.Split('\n');
        for (i = 0; i < 10; i++) lines2 = lines2 + lines[i * 3] + "\n";
        for (i = 0; i < 10; i++) Sub_Texts[i] = lines[i * 3 + 1] + "\n";
        string[] times = Regex.Split(lines2, " --> ");
        for (i = 0; i < 11; i++) times2 = times2 + times[i] + "\n";
        string[] real_times = Regex.Split(times2, "\n");
        //Subtitle Taker
        for (i = 0; i < real_times.Length; i++)
        {
            //if i = 1 it means start
            //else i = 2 it means end
            switch (i%2)
            {
                case 0:
                    string Sub_Start = real_times[i];
                    break;

                case 1:
                    string Sub_End = real_times[i];
                    break;
            }

        }
    }
    //Start the Subtitle Sequence
    public IEnumerator Subber()
    {

        yield return 0;
    }

}
