
//NOTES:Sender mesage for UnityWebRequest. An old implementation. Do not use UnityWebRequest class and methods. Instead of Unity's WebRequest please use
//.NET Core Web Request class and its methods
//14.03.2021
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using UnityEngine.SceneManagement;
//Library is our resource class for object classes
using Library;
public class DataServices : MonoBehaviour
{
    //User class data taker
    private string Ujson;
    //Connect class data taker
    private string Cjson;
    //PreTest class data taker
    private string Tjson;
    //Game class data taker
    private string Gjson;

    //User class data stored path    
    private string path = Game_Path + "/UserData.json";//Can change later

    private string Gpath = Game_Path + "/Game.json";
    //Connect class data stored path
    string Cpath;//Can change later

    private byte[] xbyte;

    //Returns the responses from methods
    int returner;

    //Application data path
    /*
     NOTE: The paths of the various platforms can change due to ios security reasons
    Unity Editor: <path to project folder>/Assets

    Mac player: <path to player app bundle>/Contents

    iOS player: <path to player app bundle>/<AppName.app>/Data (this folder is read only, use Application.persistentDataPath to save data).**

    Win/Linux player: <path to executablename_Data folder> (note that most Linux installations will be case-sensitive!)

    WebGL: The absolute url to the player data file folder (without the actual data file name)

    Android: Normally it points directly to the APK. If you are running a split binary build, it points to the OBB instead.

    Windows Store Apps: The absolute path to the player data folder (this folder is read only, use Application.persistentDataPath to save data)

    Use Application.persistentDataPath for ios devices this can fix the problems as stated above too
    */
    [SerializeField] public static string Game_Path;
    //On application's awakening, the application's path is getted

    public void Start()
    {
        Game_Path = Application.persistentDataPath;
        Cpath = Game_Path + "/Connection.json";
        Gpath = Game_Path + "/Game.json";
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    //Will be used for changing menu language texts
    //Will be used for changing game language texts
    public Language_Settings LanguageMenu;
    public Language_Game LanguageGame;

    public User user = new User();
    public Connection con = new Connection();
    public PreTest pre = new PreTest();
    public Email mail = new Email();
    public Response res = new Response();
    public Game game = new Game();

    //API Connection url addresses are in the Library.cs

    private string sendermessage;

    //Helper method of ForgotPasswordButton method from Menus
    public void ForgotPassword(Email email)
    {
        Modular_Data_Sender(JsonUtility.ToJson(email) ?? "", 2);
    }

    //User Serializer
    public void BuildSerialization(User user1)
    {
        user1 = JsonUtility.FromJson<User>(Ujson);
    }
    //Connection Serializer
    //CONTINUE ON LATER
    public void BuildSerialization(Connection conn1)
    {
        Cjson = File.ReadAllText(Cpath);
        conn1 = JsonUtility.FromJson<Connection>(Cjson);
        ///return conn1;
    }

    //Serializer method for multipurpose
    //For sign-in call with 1
    //For user detail call with 2
    public int BuildNewSerialization(User user1,int operation)
    {
        switch (operation)
        {
            case 1:
                Ujson = JsonUtility.ToJson(user1) ?? "";
                //Send register data to modular data sender
                returner = Int32.Parse(Modular_Data_Sender(Ujson, 0));
                break;
            case 2:
                Ujson = JsonUtility.ToJson(user1) ?? "";
                //Send user detail data to modular data sender
                returner = Int32.Parse(Modular_Data_Sender(Ujson, 3));
                break;
        }
        if (File.Exists(Cpath))
            System.IO.File.WriteAllText(Cpath, Cjson);
        else
        {
            File.Create(Cpath);
            System.IO.File.WriteAllText(Cpath, Cjson);
            //NEW IMPLEMENTATION, WILL BE TESTED
            File.Create(Gpath);
            Gjson = JsonUtility.ToJson(game) ?? "";
            System.IO.File.WriteAllText(Gpath, Gjson);
        }
        return returner;
    }
    //Used For Manual Login
    //Added game.json file creator
    public int LoginManuel(User user)
    {
        
        Ujson = JsonUtility.ToJson(user) ?? "";
        returner = Int32.Parse(Modular_Data_Sender(Ujson, 1));
        Cjson = JsonUtility.ToJson(con) ?? "";
        //Below may wrong implemented, will look detailed later
        //con = JsonUtility.FromJson<Connection>(Cjson);
        //Connection data creator
        if (File.Exists(Cpath))
        {
            //Update Connection.json variables
            System.IO.File.WriteAllText(Cpath, Cjson);

        }
        else
        {
            File.Create(Cpath);
            System.IO.File.WriteAllText(Cpath, Cjson);
        }
        //if there is a game data, ???
        //A checker will be written later !!!
        if (File.Exists(Gpath))
        {
            Gjson = System.IO.File.ReadAllText(Gpath);
            game = JsonUtility.FromJson<Game>(Gjson);
            //WILL USED LATER
            //System.IO.File.WriteAllText(Gpath, Gjson);

        }
        //If there is no local save data, create one
        else
        {
            File.Create(Gpath);
            Gjson = JsonUtility.ToJson(game) ?? "";
            System.IO.File.WriteAllText(Gpath, Gjson);
        }
        return returner;
    }

    //COMPLETED!!
    //get Local Connection Data
    //if data exists return data as string
    //else return "No" as string
    //connectıon data is used for AUTO-LOGIN process
    //ALSO GET GAMEDATA.JSON FILE!!!!
    public int GetLocalUserData()
    {
        //check if file is available
        //then post security and u_id to server
        if (File.Exists(Cpath))
        {
            //Connection datas
            Cjson = System.IO.File.ReadAllText(Cpath);
            con = JsonUtility.FromJson<Connection>(Cjson);
            //Game datas NOT TESTED
            Gjson = System.IO.File.ReadAllText(Gpath);
            game = JsonUtility.FromJson<Game>(Gjson);
            //Get the system Language
            UpdateLanguage(game.Language);

            //post data here and then get the user data from server
            returner =Int32.Parse(Modular_Data_Sender(Cjson,5));
            //NOT COMPLETED
            //Update UserData from Connection variables that came from Server response
            user.karakter = con.karakter;
            user.modul = con.modul;
            return returner;
        }
        //There is no such file so abort the mission
        else
        {

            Debug.Log("No files exist! Please Log-in first");
            return 0;
        }
        
    }
    //COMPLETED!!
    //Used Only for Sending Pre-Test Questions Data
    public int SendTestData(PreTest test)
    {
        test.user_id = con.user_id;
        test.security = con.security; 
        Tjson = JsonUtility.ToJson(test) ?? "";
        //Send Pre-test Data
        returner = Int32.Parse(Modular_Data_Sender(Tjson, 4));
        return returner;
    }

    //UNCOMPLETED NOT USED FOR NOW
    //Used for CharSelection and Char data sending
    //D.CharSelection(x); => Set char and send to the system
    public int CharSelection(int character)
    {
        user.user_id = con.user_id;
        user.security = con.security;
        user.karakter = character;
        con.karakter = character;
        Ujson = JsonUtility.ToJson(user) ?? "";
        returner = Int32.Parse(Modular_Data_Sender(Ujson, 6));
        return returner;
    }

    //UNCOMPLETED NOT USED FOR NOW
    //Used Only for Sending Modul Data
    //Basketbol (20 puana ulaşırsa) => SetModul(1) olarak çağır
    //Archery (20 puana ulaşırsa) => SetModul(2) olarak çağır
    //Long Jump (20 puana ulaşırsa) => SetModul(3) olarak çağır
    //SotPut (20 puana ulaşırsa) => SetModul(4) olarak çağır
    //Javelin (20 puana ulaşırsa) => SetModul(5) olarak çağır
    //DataServices.SetModul(x); => Biten oyunun verisini sisteme gönderir
    public int SetModul(int FinishedGame)
    {
        user.user_id = con.user_id;
        user.security = con.security;
        user.modul = FinishedGame;
        Ujson = JsonUtility.ToJson(user) ?? "";
        returner = Int32.Parse(Modular_Data_Sender(Ujson, 7));
        return returner;
    }

    //NON USED PROTOTYPE FOR FURTHER USAGE
    //OTHER PROTOTYPES ARE UPGRADED VERSION OF THIS CODE BLOCK
    IEnumerator Upload()
    {
        WebRequest request = WebRequest.Create(Links.Register_URL);//Burası değişecek(localhost -> Esvolon)
        request.Method = "POST";

        // Create POST data and convert it to a byte array.
        xbyte = System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(path));

        // Set the ContentType property of the WebRequest.
        request.ContentType = "application/json";
        // Set the ContentLength property of the WebRequest.
        request.ContentLength = xbyte.Length;

        // Get the request stream.
        Stream dataStream = request.GetRequestStream();
        // Write the data to the request stream.
        dataStream.Write(xbyte, 0, xbyte.Length);
        // Close the Stream object.
        dataStream.Close();

        // Get the response.
        WebResponse response = request.GetResponse();
        // Display the status.
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        
        // Get the stream containing content returned by the server.
        // The using block ensures the stream is automatically closed.
        using (dataStream = response.GetResponseStream())
        {
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            //Overwrite the registered data its newly added data
            JsonUtility.FromJsonOverwrite(responseFromServer,con);
            // Display the content.
            Console.WriteLine(responseFromServer);
        }
        // Close the response.
        response.Close();
        yield return 0;
    }

    //MODULAR TESTED DATA SENDER and GETTER
    //Used for web connection and data sending/getting from server

    //GET STRING OF DATA RETURNED FROM SERVER
    public string Modular_Data_Sender(string data,int state)
    {
        string URL;
        switch (state)
        {
            //Register operation
            case 0:
                //!!!!
                URL = Links.Register_URL;
                break;
            //Login operation
            case 1:
                URL = Links.Login_URL;
                break;
            //Forgot password send operation
            case 2:
                URL = Links.ForgotPassword_URL;
                break;
            //User Detail send operation
            case 3:
                URL = Links.UserDetail_URL;
                break;
            //PreTest send operation
            case 4:
                URL = Links.PreTest_URL;
                break;
            //Get User operation
            case 5:
                URL = Links.GetUser_URL;
                break;
                //Send selected character value
            case 6:
                URL = Links.CharSelect_URL;
                break;
             //Send game score to server
            case 7:
                URL = Links.SetModul_URL;
                break;
            default:
                URL = Links.GetUser_URL;
                break;
        }
        WebRequest request = WebRequest.Create(URL);
        request.Method = "POST";
        xbyte = System.Text.Encoding.UTF8.GetBytes(data);
        request.ContentType = "application/json";
        request.ContentLength = xbyte.Length;
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(xbyte, 0, xbyte.Length);
        dataStream.Close();

        WebResponse response = request.GetResponse();
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);

        using (dataStream = response.GetResponseStream())
        {
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            //Take the situation response from server
            JsonUtility.FromJsonOverwrite(responseFromServer, res);
            //Get the connection info from server if needed
            JsonUtility.FromJsonOverwrite(responseFromServer, con);
            Console.WriteLine(responseFromServer);
        }
        response.Close();
        return res.response.ToString();
    }


    //Earn points from different things
    //Subtitle.cs => DataServices.PointGainandSend
    //USED FOR:Ball throw earning => Library.token
    //YAĞIZA SOR TOPLAM TOP ATMA HAKKI SİSTEME GÖNDERİLECEK Mİ DİYE?
    //Gain tokens and save it local
    public void PointGainandSend(int point)
    {
        game.tokens += point; 
    }
    //Used only for mini-game Saving
    public void BuildGameSave()
    {
        Gjson = JsonUtility.ToJson(game);
        File.WriteAllText(Gpath, Gjson);
    }


    //HALF COMPLETED ! 
    //Will be called in start from Subtitle and Menus scripts
    public void UpdateLanguage(int Language)
    {
        game.Language = Language;
        if("UIMenu" == SceneManager.GetActiveScene().name)
        {
            LanguageMenu = GameObject.Find("LanguageSettings").GetComponent<Language_Settings>();
            LanguageMenu.Change_Language(Language);
        }
            
        else if ("Game_Map" == SceneManager.GetActiveScene().name)
        {
            LanguageGame = GameObject.Find("LanguageGame").GetComponent<Language_Game>();
            LanguageGame.Change_Language(Language);

        }


    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;
        UpdateLanguage(game.Language);
    }

    //
    /*
    void EndReached(UnityEngine.SceneManagement)
    { 
        //Earn 2 points for every minutes = (Totalminutes + 1) * 2 => Earned points
        Total_Token = Total_Token * 2f;
        //SEND THE DATA to GAME as tokens
        //CALL DataServices.PointGainandSend(10=[as points]);
        DS.PointGainandSend((int)Total_Token);
        Total_Token = 0;
    }
    */
}