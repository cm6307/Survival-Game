using UnityEngine;
using System.Collections;
using System;
using MySql.Data.MySqlClient;

public class Registration : MonoBehaviour {

    public Facade f;

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

}
