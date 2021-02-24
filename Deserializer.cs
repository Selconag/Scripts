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
    public string Ujson;
    //Connect class data taker
    public string Cjson;
    //PreTest class data taker
    public string Tjson;

    public byte[] xbyte;
    //User class data stored path    
    public string path = @"C:\Unity Projects\UI\Assets\Scripts\UserData.json";//Can change later
    //Connect class data stored path
    public string Cpath = @"C:\Unity Projects\UI\Assets\Scripts\ConnectionData.json";//Can change later

    public User user = new User();
    public Connection con = new Connection();

    //API Connection url addresses for the application
    private const string Register_URL = "http://esvolon.uniqdesignfactory.com/api/users/register";//Can change later between(localhost -> Esvolon)
    private const string Login_URL = "http://esvolon.uniqdesignfactory.com/api/users/login";
    private const string ForgotPassword_URL = "http://esvolon.uniqdesignfactory.com/api/users/forgot-password";
    private const string UserDetail_URL = "http://esvolon.uniqdesignfactory.com/api/users/user-detail";
    //Sender mesage for UnityWebRequest. An old implementation.
    private string sendermessage;

    // Start is called before the first frame update
    void Start()
    {
        //SENDTHENUKES();
    }
    //Used for Json Deserialization

    public void DestroySerialization()
    {
        //trying values
        //user.name = "Control1";
        //user.email = "control@gmail.com";
        //user.password = "Controlx";
        Ujson = JsonUtility.ToJson(user) ?? "";
        System.IO.File.WriteAllText(path, Ujson);
        Debug.Log("Written file is:" + Ujson);
        //return json;
    }

    //Used for Json Serialization
    public void BuildSerialization()
    {
        Cjson = System.IO.File.ReadAllText(path);
        con = JsonUtility.FromJson<Connection>(Cjson);
        Debug.Log("Readed file is:" + Ujson);
        //return user;
    }
    //Used for Json Serialization
    public void BuildSerialization(PreTest test)
    {
        test = JsonUtility.FromJson<PreTest>(Ujson);
        Debug.Log("Readed file is:" + Ujson);
        //return user;
    }
    //User Serializer
    public void BuildSerialization(User user1)
    {
        user1 = JsonUtility.FromJson<User>(Ujson);
        Debug.Log("Readed file is:" + Ujson);
        //return user;
    }
    //Connection Serializer

    public void BuildSerialization(Connection conn1)
    {
        Ujson = System.IO.File.ReadAllText(path);
        conn1 = JsonUtility.FromJson<Connection>(Cjson);
        Debug.Log("Readed file is:" + Cjson);
        //return user;
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
            JsonUtility.FromJsonOverwrite(responseFromServer,user);
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
            JsonUtility.FromJsonOverwrite(responseFromServer, user);
            // Display the content.
            Console.WriteLine(responseFromServer);
        }
        // Close the response.
        response.Close();
        yield return 0;
    }
}