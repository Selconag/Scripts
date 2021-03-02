
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
public class Deserializer : MonoBehaviour
{
    //User class data taker
    private string Ujson;
    //Connect class data taker
    private string Cjson;
    //PreTest class data taker
    private string Tjson;

    private byte[] xbyte;
    //User class data stored path    
    private string path = @"C:\Unity Projects\UI\Assets\Scripts\UserData.json";//Can change later
    //Connect class data stored path
    private string Cpath = @"C:\Unity Projects\UI\Assets\Scripts\ConnectionData.json";//Can change later

    public User user = new User();
    public Connection con = new Connection();
    public PreTest pre = new PreTest();

    //API Connection url addresses are in the Library.cs

    private string sendermessage;

    void Start()
    {
       
    }
    public void BuildNewSerialization(User user1)
    {
        Ujson = JsonUtility.ToJson(user1) ?? "";
        StartCoroutine(Upload(Ujson));
        BuildSerialization(con);
        System.IO.File.WriteAllText(Cpath, Cjson);
        //return json;
    }
    //Used For Manual Login
    public void LoginManuel(User user1)
    {
        Ujson = JsonUtility.ToJson(user1) ?? "";
        Modular_Data_Sender(Ujson, 1);
        Cjson = JsonUtility.ToJson(con) ?? "";
        //Below may wrong implemented, will look detailed later
        //con = JsonUtility.FromJson<Connection>(Cjson);
        System.IO.File.WriteAllText(Cpath, Cjson);
        //return Ujson;
    }

    //COMPLETED!!
    //get Local Connection Data
    //if data exists return data as string
    //else return "No" as string
    public string GetLocalUserData()
    {
        //check if file is available
        //then post security and u_id to server
        if (File.Exists(Cpath))
        {
            Cjson = System.IO.File.ReadAllText(Cpath);
            con = JsonUtility.FromJson<Connection>(Cjson);
            //post data here and then get the user data from server
            return Cjson;
        }
        else
        {
            Cjson = System.IO.File.ReadAllText(Cpath);
            Debug.Log("No files exist! Please Log-in first");
            return "No";
        }

    }
    
    //Used for Json Serialization
    public void SendTestData(PreTest test)
    {
        Cjson = System.IO.File.ReadAllText(Cpath);
        Tjson = JsonUtility.ToJson(test) ?? "";
        Sender(Cjson+Tjson);

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

    
    //Starts Data Connection
    public void Data_Connection()
    {
        StartCoroutine(Upload());
    }
    
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
    
    IEnumerator Upload(string data)
    {
        WebRequest request = WebRequest.Create(Links.Register_URL);//Burası değişecek(localhost -> Esvolon)
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
            JsonUtility.FromJsonOverwrite(responseFromServer, con);
            Console.WriteLine(responseFromServer);
        }
        response.Close();
        yield return 0;
    }

    //Bunun bool değer döndürenini de oluştur ve auto-login için kullan
    //Daha sonra URL'yi methodu çağırırken yolla ki modüler tasarım olsun
    public string Sender(string data) {
        WebRequest request = WebRequest.Create(Links.Login_URL);//Burası değişecek(localhost -> Esvolon)
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
            JsonUtility.FromJsonOverwrite(responseFromServer, con);
            Console.WriteLine(responseFromServer);
            
        }
        response.Close();
        return "";

    }

    //MODULAR TESTED DATA SENDER and GETTER
    public void Modular_Data_Sender(string data,int state)
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
            JsonUtility.FromJsonOverwrite(responseFromServer, con);
            Console.WriteLine(responseFromServer);
        }
        response.Close();

    }
}