using UnityEngine;
using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DatabaseConnection : MonoBehaviour {

    [SerializeField]
    private string host, database, user, password;
    [SerializeField]
    private bool pooling = true;

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
        catch (MySqlException e)
        {
            // TO DO: set a timeout and try again!
            Debug.Log(e);
        }
    }

    public MySqlDataReader RunReadQuery(string query)
    {
        MySqlCommand command = new MySqlCommand(query, connection);
        command.CommandText = query;
        try
        {
            return command.ExecuteReader();
        }
        catch (MySqlException e)
        {
            throw e;
        }
    }

    public void RunWriteQuery(string query)
    {
        MySqlCommand command = new MySqlCommand(query, connection);
        command.CommandText = query;
        try
        {
            command.ExecuteNonQuery();
        }
        catch (MySqlException e)
        {
            throw e;
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
