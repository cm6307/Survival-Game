using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class Facade : MonoBehaviour {

    public DatabaseConnection dbConnection;
    private MD5 _md5Hash;

    // Use this for initialization
    void Start () {
        // Just a simple example query
        dbConnection.RunQuery("SELECT id, username FROM User");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
