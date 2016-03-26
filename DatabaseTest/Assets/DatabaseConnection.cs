using UnityEngine;
using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DatabaseConnection : MonoBehaviour {

    public string host, database, user, password;
    public bool pooling = true;

    private string connectionString;
    private MySqlConnection connection = null;
    private MySqlCommand command = null;
    private MySqlDataReader reader = null;
    private MD5 _md5Hash;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        connectionString = "Server=" + host + ";Database=" + database + ";User=" + user + ";Password=" + password + ";Pooling=";
        if (pooling)
        {
            connectionString += "true;";
        }
        else {
            connectionString += "false;";
        }

        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Debug.Log("Connection to database was successful.");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void OnApplicationQuit()
    {
        if (connection != null)
        {
            if (connection.State.ToString() != "Closed")
            {
                connection.Close();
                Debug.Log("MySql connection closed. Goodbye!");
            }
            connection.Dispose();
        }
    }

}
