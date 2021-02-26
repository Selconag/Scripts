using UnityEngine;
//Classes are stored in here and all of their variables
namespace Library
{
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
    [System.Serializable]
    public class Connection
    {
        //Security number for security clearance
        [SerializeField] public string security;
        //User id which created on the sign-up / Unique
        [SerializeField] public string user_id;

    }
    [System.Serializable]
    public class Game
    {

    }

    [System.Serializable]
    public class PreTest
    {
        //Fİrst 8 questions, Y/N answers will be taken as string
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
    }
    [System.Serializable]
    public class LastTest
    {

    }

    //Links we use in the app
    public class Links
    {
        public const string Register_URL = "http://esvolon.uniqdesignfactory.com/api/users/register";//Can change later between(localhost -> Esvolon)
        public const string Login_URL = "http://esvolon.uniqdesignfactory.com/api/users/login";
        public const string ForgotPassword_URL = "http://esvolon.uniqdesignfactory.com/api/users/forgot-password";
        public const string UserDetail_URL = "http://esvolon.uniqdesignfactory.com/api/users/user-detail";
        public const string PreTest = "http://esvolon.uniqdesignfactory.com/api/users/giris-test";
    }
}
