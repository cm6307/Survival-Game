using UnityEngine;
using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DatabaseConnection : MonoBehaviour {

    public string host, database, user, password;
    public bool pooling = true;

    private string connectionString;
    private MySqlConnection connection = null;

    void OpenConnection()
    {
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

    public void RunQuery(string query)
    {
        MySqlCommand command = new MySqlCommand(query, connection);
        command.CommandText = query;
        try
        {
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Still need to figure out how to return this data
                Debug.Log(reader.GetString(1));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void CloseConnection()
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

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        OpenConnection();
    }

    void OnApplicationQuit()
    {
        CloseConnection();
    }

}
