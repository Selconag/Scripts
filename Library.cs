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
        /*    //Security number for security clearance
        [SerializeField] public string security;
        //User id which created on the sign-up / Unique
        [SerializeField] public string user_id; */
        //User age variable from pre-test
        [SerializeField] public int age;
        //User gender variable from pre-test
        [SerializeField] public string gender;
        //User occupation variable from pre-test
        [SerializeField] public string occupation;
        //User experience variable from pre-test
        [SerializeField] public int prof_exp;
        //User country variable from pre-test 
        [SerializeField] public string country;

    }
    [System.Serializable]
    public class PreTest
    {
        //Is user finished Pre-Test?
        [SerializeField] public bool pre_test;

    }
    [System.Serializable]
    public class Connection
    {
        //Security number for security clearance
        [SerializeField] public string security;
        //User id which created on the sign-up / Unique
        [SerializeField] public string user_id;
        //Is user finished Pre-Test?
        [SerializeField] public bool pre_test;
    }
    [System.Serializable]
    public class Game
    {
        
    }
}
