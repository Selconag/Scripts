
//NOTES:Sender mesage for UnityWebRequest. An old implementation. Do not use UnityWebRequest class and methods. Instead of Unity's WebRequest please use
//.NET Core Web Request class and its methods

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

    //User class data stored path    
    private string path = Game_Path + "/UserData.json";//Can change later
    //Connect class data stored path
    string Cpath;//Can change later

    private byte[] xbyte;

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
    }

    


    public User user = new User();
    public Connection con = new Connection();
    public PreTest pre = new PreTest();
    public Email mail = new Email();
    public Response res = new Response();

    //API Connection url addresses are in the Library.cs

    private string sendermessage;

    //Helper method of ForgotPasswordButton method from Menus
    public void ForgotPassword(Email email)
    {
        Modular_Data_Sender(JsonUtility.ToJson(email) ?? "", 2);
    }

    public void BuildNewSerialization(User user1)
    {
        Ujson = JsonUtility.ToJson(user1) ?? "";
        Modular_Data_Sender(Ujson, 3);
        BuildSerialization(con);
        if (File.Exists(Cpath))
            System.IO.File.WriteAllText(Cpath, Cjson);
        else
        {
            File.Create(Cpath);
            System.IO.File.WriteAllText(Cpath, Cjson);
        }
    }
    //Used For Manual Login
    public int LoginManuel(User user1)
    {
        int returner;
        Ujson = JsonUtility.ToJson(user1) ?? "";
        returner = Int32.Parse(Modular_Data_Sender(Ujson, 1));
        Cjson = JsonUtility.ToJson(con) ?? "";
        //Below may wrong implemented, will look detailed later
        //con = JsonUtility.FromJson<Connection>(Cjson);
        if(File.Exists(Cpath))
        System.IO.File.WriteAllText(Cpath, Cjson);
        else
        {
            File.Create(Cpath);
            System.IO.File.WriteAllText(Cpath, Cjson);
        }
        
        return returner;
    }

    //COMPLETED!!
    //get Local Connection Data
    //if data exists return data as string
    //else return "No" as string
    //connectıon data is used for AUTO-LOGIN process
    public string GetLocalUserData()
    {
        //check if file is available
        //then post security and u_id to server
        if (File.Exists(Cpath))
        {
            Cjson = System.IO.File.ReadAllText(Cpath);
            con = JsonUtility.FromJson<Connection>(Cjson);
            //post data here and then get the user data from server
            Modular_Data_Sender(Cjson,5);
            return Cjson;
        }
        else
        {
            Cjson = System.IO.File.ReadAllText(Cpath);
            Debug.Log("No files exist! Please Log-in first");
            return "No";
        }

    }
    
    //Used Only for Sending Pre-Test Questions Data
    public void SendTestData(PreTest test)
    {
        test.user_id = con.user_id;
        test.security = con.security;
        Tjson = JsonUtility.ToJson(test) ?? "";
        //Send Pre-test Data
        Modular_Data_Sender(Tjson,4);

    }
    //User Serializer
    public void BuildSerialization(User user1)
    {
        user1 = JsonUtility.FromJson<User>(Ujson);
    }
    //Connection Serializer
    public void BuildSerialization(Connection conn1)
    {
        conn1 = JsonUtility.FromJson<Connection>(Cjson);
        ///return conn1;
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
    public string Modular_Data_Sender(string data,int state)
    {
        string URL;
        switch (state)
        {
            case 0:
                URL = Links.Register_URL;
                break;
            case 1:
                URL = Links.Login_URL;
                break;
            case 2:
                URL = Links.ForgotPassword_URL;
                break;
            case 3:
                URL = Links.UserDetail_URL;
                break;
            case 4:
                URL = Links.PreTest_URL;
                break;
            case 5:
                URL = Links.GetUser_URL;
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
}