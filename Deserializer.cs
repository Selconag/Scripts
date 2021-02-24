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
//using System.Net.Http.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Library;
public class Deserializer : MonoBehaviour
{
    public string Ujson;
    public string Cjson;

    public byte[] xbyte;
    public string path = @"C:\Unity Projects\UI\Assets\Scripts\UserData.json";//Can change later
    public string Cpath = @"C:\Unity Projects\UI\Assets\Scripts\ConnectionData.json";//Can change later

    public User user = new User();
    public Connection con = new Connection();
    //API Connection url addresses for the application
    private const string Register_URL = "http://esvolon.uniqdesignfactory.com/api/users/register";//Can change later betweb(localhost -> Esvolon)
    private const string Login_URL = "http://esvolon.uniqdesignfactory.com/api/users/login";
    private const string ForgotPassword_URL = "http://esvolon.uniqdesignfactory.com/api/users/forgot-password";
    private const string UserDetail_URL = "http://esvolon.uniqdesignfactory.com/api/users/user-detail";
    private string sendermessage;
    // Start is called before the first frame update
    void Start()
    {
        //SENDTHENUKES();
    }
    public void DestroySerialization()
    {
        user.name = "Control1";
        user.email = "control@gmail.com";
        user.password = "Controlx";
        Ujson = JsonUtility.ToJson(user) ?? "";
        System.IO.File.WriteAllText(path, Ujson);
        Debug.Log("Written file is:" + Ujson);
        //return json;
    }
    public void BuildSerialization()
    {
        Ujson = System.IO.File.ReadAllText(path);
        user = JsonUtility.FromJson<User>(Ujson);
        Debug.Log("Readed file is:" + Ujson);
        //return user;
    }
    //COMPLETED!!
    //get Local User Data
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
}

//EXAMPLE DESERİALİZATİON
/*public void DestroySerialization()
    {
        string path = @"C:\Unity Projects\UI\Assets\Scripts";
        //string path = "C:/Unity Projects/UI/Assets/Scripts";
        Kisi user = new Kisi();
        user.name = "Ahmet";
        user.email = "ahmet@gmail.com";
        user.password = "Selo1234";
        json = JsonUtility.ToJson(user);
        // json now contains: '{"name":Ahmet,"email":ahmet@gmail.com,"password":"Selo1234"}'
        //System.IO.File.WriteAllText(Application.persistentDataPath + "/UserData.json", json);
        System.IO.File.WriteAllText(path + "/UserData.json", json);
        //Debug.Log(json);

        //var obje = JsonUtility.FromJson<Kisi>(json);
        //Debug.Log(obje);
    }*/