using UnityEngine;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DBConnection : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string cs = @"server=localhost;userid=root;
            password=25698914;database=survival_game";

        MySqlConnection conn = new MySqlConnection(cs);

        try
        {
            conn.Open();
            Debug.Log("MySQL version : " + conn.ServerVersion);
        }
        catch (MySqlException ex)
        {
            Debug.Log("Error: " + ex.ToString());

        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
    
}
