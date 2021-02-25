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

    //API Connection url addresses for the application
    private const string Register_URL = "http://esvolon.uniqdesignfactory.com/api/users/register";//Can change later between(localhost -> Esvolon)
    private const string Login_URL = "http://esvolon.uniqdesignfactory.com/api/users/login";
    private const string ForgotPassword_URL = "http://esvolon.uniqdesignfactory.com/api/users/forgot-password";
    private const string UserDetail_URL = "http://esvolon.uniqdesignfactory.com/api/users/user-detail";
    //Sender mesage for UnityWebRequest. An old implementation.
    private string sendermessage;

    void Start()
    {
       
    }
    //Used for Json Deserialization

    //public void DestroySerialization()
    //{

    //    Ujson = JsonUtility.ToJson(user) ?? "";
    //    System.IO.File.WriteAllText(path, Ujson);
    //    Debug.Log("Written file is:" + Ujson);
    //    //return json;
    //}
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
        Sender(Ujson);
        con = JsonUtility.FromJson<Connection>(Cjson);
        System.IO.File.WriteAllText(Cpath, Cjson);
        //return json;
    }

    //Used for AutoLogin

    public bool AutoLogin()
    {
        Cjson = System.IO.File.ReadAllText(Cpath);
        con = JsonUtility.FromJson<Connection>(Cjson);
        return true;
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

    //COMPLETED!!
    //get Local Connection Data
    //if data exists return data as string
    //else return "No" as string
    public string GetLocalUserData()
    {
        if (File.Exists(Cpath))
        {
            Cjson = System.IO.File.ReadAllText(Cpath);
            con = JsonUtility.FromJson<Connection>(Cjson);
            return Cjson;
        }
        else
        {
            Cjson = System.IO.File.ReadAllText(Cpath);
            Debug.Log("No files exist! Please Log-in first");
            return "No";
        }
        
    }
    //Starts Data Connection
    public void Data_Connection()
    {
        StartCoroutine(Upload());
    }
    
    IEnumerator Upload()
    {
        WebRequest request = WebRequest.Create(Register_URL);//Burası değişecek(localhost -> Esvolon)
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
        WebRequest request = WebRequest.Create(Register_URL);//Burası değişecek(localhost -> Esvolon)
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

    //Daha sonra URL'yi methodu çağırırken yolla ki modüler tasarım olsun
    public bool Sender(string data) {
        WebRequest request = WebRequest.Create(Login_URL);//Burası değişecek(localhost -> Esvolon)
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
        return true;
    }
}