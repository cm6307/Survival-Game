using UnityEngine;
using System.Collections;
using System;
using MySql.Data.MySqlClient;

public class Registration : MonoBehaviour {

    public Facade f;

    private static Registration s_Instance = null;

    public static Registration instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(Registration)) as Registration;
            }

            if (s_Instance == null)
            {
                GameObject obj = new GameObject("SessionManager");
                s_Instance = obj.AddComponent(typeof(Registration)) as Registration;
            }

            return s_Instance;
        }
    }


    public void CreateAccount(string username, string password)
    {
        try
        {
            f.CreateNewUser(username, password);
        }
        catch (MySqlException e)
        {
            switch (e.Number)
            {
                case 1062:
                    throw new System.ArgumentException("This username is already in use.");
                default:
                    throw new System.InvalidOperationException("An error occurred. Please try again later.");
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
