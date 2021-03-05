using UnityEngine;
//Classes are stored in here and all of their variables
namespace Library
{
    //Used for storing and sending user info variables
    [System.Serializable]
    public class User
    {
        //Name defined by user
        [SerializeField] public string name;
        //E-mail defined by user / Unique
        [SerializeField] public string email;
        //Password defined by user / Min 8 length chars and vars
        [SerializeField] public string password;
        //Check if user details are entered
        [SerializeField] public string user_detail = "No";
        //User age variable from pre-test
        [SerializeField] public string age;
        //User gender variable from pre-test
        [SerializeField] public string gender;
        //User occupation variable from pre-test
        [SerializeField] public string occupation;
        //User experience variable from pre-test
        [SerializeField] public string prof_exp;
        //User country variable from pre-test 
        [SerializeField] public string country;
        //For now these parts are implemented in Connection Class for modular usage
        //Security number for security clearance
        [SerializeField] public string security;
        //User id which created on the sign-up / Unique
        [SerializeField] public string user_id;
        //Is user finished Pre-Test?
        [SerializeField] public int modul = 0;
    }
    //Auto connection and many other details
    //pre tested for security leaks
    //further tests will be needed
    [System.Serializable]
    public class Connection
    {
        //Security number for security clearance
        [SerializeField] public string security;
        //User id which created on the sign-up / Unique
        [SerializeField] public string user_id;
        //Check if user details are entered
        [SerializeField] public int user_detay = 0;
        //Check if pre tests made
        [SerializeField] public int giris_test = 0;
        //Check if final tests made
        [SerializeField] public int final_test = 0;
        //Check which modul the client is on
        [SerializeField] public int modul = 0;
        //Check if character is selected
        [SerializeField] public int karakter = 0;
    }
    //Class of game inputs
    [System.Serializable]
    public class Game
    {


        //Check how many game tokens player has
        //Gametokens are used in mini games like ammunition in a weapon
        //As in basketball every token equals to a free throw
        [SerializeField] public int tokens = 0;


        //Total score is calculated as a game win condition
        /*
         * If the player reaches enough score, next game will be unlocked
        Basketball: 20 points => Shot Put
        Shot Put: 40 points => Javelin Throw
        Javelin Throw: 60 points => Archery
        Archery: 80 points => Long Jump
        Long Jump: 100 points => Final Test
         */
        //After final test is done the client gets his/hers reward for completing the course
        [SerializeField] public int total_score = 0;
    }

    [System.Serializable]
    public class PreTest
    {
        //First 8 questions, Y/N answers will be taken as string
        [SerializeField] public string bir;
        [SerializeField] public string iki;
        [SerializeField] public string uc;
        [SerializeField] public string dort;
        [SerializeField] public string bes;
        [SerializeField] public string alti;
        [SerializeField] public string yedi;
        [SerializeField] public string sekiz;
        //Rating question, answer will be taken as int
        [SerializeField] public int puan = 0;
        //Security number for security clearance
        [SerializeField] public string security;
        //User id which created on the sign-up / Unique
        [SerializeField] public string user_id;
    }
    [System.Serializable]
    public class LastTest
    {

    }

    //Only used for "forgot my password" event 
    [System.Serializable]
    public class Email
    {
        //E-mail defined by user / Unique
        [SerializeField] public string email;
    }

    //Api Links we use in the app
    public class Links
    {
        public const string Register_URL = "http://esvolon.uniqdesignfactory.com/api/users/register";//Can change later between(localhost -> Esvolon)
        public const string Login_URL = "http://esvolon.uniqdesignfactory.com/api/users/login";
        public const string ForgotPassword_URL = "http://esvolon.uniqdesignfactory.com/api/users/forgot-password";
        public const string UserDetail_URL = "http://esvolon.uniqdesignfactory.com/api/users/user-detail";
        public const string PreTest_URL = "http://esvolon.uniqdesignfactory.com/api/users/giris-test";
        public const string GetUser_URL = "https://esvolon.uniqdesignfactory.com/api/users/get-user";
    }

    [System.Serializable]
    public class Response
    {
        //For boolean state checks
        [SerializeField] public int response;
    }

}
/*
 FURTHER NOTICE FOR IOS, ANDROID AND WINDOWS BUILDS
 1)On Android File.Create(Application.persistentDataPath + "TheFile") statement is crucial for creating save datas
 Application.persistentDataPath => leads to => LocalStorage/Android/data/com."GameName"/files
 so as for (Application.persistentDataPath + "TheFile") statement a PLUS(+) is a must for not making save name something like
 LocalStorage/Android/data/com."GameName"/filesTheFile which is wrong implementation
 2)On İos will be added after tests


 3)For others
 
 */

