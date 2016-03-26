using UnityEngine;
using System;
using System.Collections;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Text;

public class Facade : MonoBehaviour {

    public DatabaseConnection dbConnection;
    private MySqlDataReader reader = null;

    // Throws exception on duplicates! Careful!
    public void CreateNewUser(string username, string password)
    {
        string encriptedPassword = CalculateHash(password);
        dbConnection.RunWriteQuery("INSERT INTO USER (username, password) VALUES ('" 
            + username +"', '" + encriptedPassword + "');");
    }

    public bool DoesUserExist(string username)
    {
        bool status = false;
        reader = dbConnection.RunReadQuery("SELECT id FROM User WHERE username='" + username + "'");
        if (reader.Read())
        {
            status = true;
        }
        reader.Close();
        return status;
    }

    public void AddPointsToUser(string username, int points)
    {
        int currentPoints = GetUserPoints(username);
        if (currentPoints == -1)
        {
            // TODO: throw user does not exist exception
            return;
        }
        currentPoints += points;
        // TODO: Check for failure
        dbConnection.RunWriteQuery("UPDATE USER SET points = " + currentPoints.ToString()
            + " WHERE username = '" + username + "';");
    }

    // Returns -1 if "username" is not found in the database
    public int GetUserPoints(string username)
    {
        int points = -1;
        reader = dbConnection.RunReadQuery("SELECT points FROM USER WHERE username='"+ username +"'");
        if (reader.Read())
        {
            points = reader.GetInt32(0);
        }
        reader.Close();
        return points;
    }

    // Returns -1 if "charname" is not found in the database
    public int GetCharacterCost(string charname)
    {
        int cost = -1;
        reader = dbConnection.RunReadQuery("SELECT cost FROM `character` WHERE name='" + charname + "'");
        if (reader.Read())
        {
            cost = reader.GetInt32(0);
        }
        reader.Close();
        return cost;
    }

    // This should be atomic, but it isn't
    public void UnlockCharacter(string username, string charname)
    {
        int userID, charID;
        userID = GetUserID(username);
        charID = GetCharID(charname);
        if(userID == -1 || charID == -1)
        {
            return; // TODO: throw user or character not found
            // Should not happen (users are logged in, characters are on screen)
        }
        int cost = GetCharacterCost(charname);
        AddPointsToUser(username, -cost);
        dbConnection.RunWriteQuery("INSERT INTO `unlock`(userID, characterID) VALUES(" 
            + userID.ToString() + ", " + charID.ToString() + ");");
    }

    public bool isCharacterUnlocked(string username, string charname)
    {
        int userID, charID;
        bool unlocked = false;
        userID = GetUserID(username);
        charID = GetCharID(charname);
        if (userID == -1 || charID == -1)
        {
            return unlocked; // TODO: throw user or character not found
            // Should not happen (users are logged in, characters are on screen)
        }
        reader = dbConnection.RunReadQuery("SELECT* FROM `unlock` WHERE userID = " + userID.ToString()
            + " AND characterID = " + charID.ToString() + ";");
        if (reader.Read())
        {
            unlocked = true;
        }
        reader.Close();
        return unlocked;
    }

    public bool CheckPassword(string username, string password)
    {
        bool correct = false;
        string hashedPassword = CalculateHash(password);
        reader = dbConnection.RunReadQuery("SELECT id FROM USER WHERE username='" 
            + username + "' AND password = '" + hashedPassword + "';");
        if (reader.Read())
        {
            correct = true;
        }
        reader.Close();
        return correct;
    }
    
    // Returns -1 if "username" is not found
    private int GetUserID(string username)
    {
        int id = -1;
        reader = dbConnection.RunReadQuery("SELECT id FROM USER WHERE username='" + username + "'");
        if (reader.Read())
        {
            id = reader.GetInt32(0);
        }
        reader.Close();
        return id;
    }

    // Returns -1 if "charname" is not found
    private int GetCharID(string charname)
    {
        int id = -1;
        reader = dbConnection.RunReadQuery("SELECT id FROM `character` WHERE name='" + charname + "'");
        if (reader.Read())
        {
            id = reader.GetInt32(0);
        }
        reader.Close();
        return id;
    }

    private string CalculateHash(string input)
    {
        MD5 md5 = System.Security.Cryptography.MD5.Create();

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        return sb.ToString();
    }

    void Start()
    {
        // Some tests here
        Debug.Log("Jullytta's points before: "+GetUserPoints("jullytta"));
        Debug.Log("Karteler's points: " + GetUserPoints("karteler"));
        if (DoesUserExist("jullytta"))
        {
            Debug.Log("Jullytta exists.");
        }
        if (!DoesUserExist("karteler"))
        {
            Debug.Log("Karteler doesn't.");
        }
        
        Debug.Log("RobotBoy is worth " + GetCharacterCost("RobotBoy").ToString() + " points.");
        Debug.Log("MagicalGirl is worth " + GetCharacterCost("MagicalGirl").ToString() + " points.");

        // Debug.Log("Jullytta's id is " + GetUserID("jullytta").ToString());
        // Debug.Log("Karteler's id is " + GetUserID("karteler").ToString());

        // Debug.Log("RobotBoy's id is " + GetCharID("RobotBoy").ToString());
        // Debug.Log("MagicalGirl's id is " + GetCharID("MagicalGirl").ToString());

        // Debug.Log("Unlocking RobotBoy for Jullytta...");
        // UnlockCharacter("jullytta", "RobotBoy");
        // Debug.Log("Jullytta's points after: " + GetUserPoints("jullytta"));

        Debug.Log("Is RobotBoy unlocked for Jullytta?");
        if(isCharacterUnlocked("jullytta", "RobotBoy"))
        {
            Debug.Log("Yes!");
        }
        else
        {
            Debug.Log("No...");
        }
        Debug.Log("What about for noob?");
        if (isCharacterUnlocked("noob", "RobotBoy"))
        {
            Debug.Log("Yes!");
        }
        else
        {
            Debug.Log("No...");
        }

        // CreateNewUser("dummie", "1234");

        Debug.Log("Is dummie's password 4567?");
        if (CheckPassword("dummie", "4567"))
        {
            Debug.Log("Yes!");
        }
        else
        {
            Debug.Log("No...");
        }

        Debug.Log("Is it 1234?");
        if (CheckPassword("dummie", "1234"))
        {
            Debug.Log("Yes!");
        }
        else
        {
            Debug.Log("No...");
        }
    }

}
