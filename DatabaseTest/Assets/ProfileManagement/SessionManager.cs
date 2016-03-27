using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;

public class SessionManager : MonoBehaviour {

    public Facade f;
    private string[] LoggedUsers = new string[4];
    private int[] PointsEarned = new int[4];

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        for (int i = 0; i < 4; i++)
        {
            LoggedUsers[i] = "";
        }
    }

    // Remember to call this after every match!
    public void SendPointsToDatabase()
    {
        for (int i = 0; i < 4; i++)
        {
            if (LoggedUsers[i].Length != 0 && PointsEarned[i] != 0)
            {
                // Update database with cache points when application is closed
                f.AddPointsToUser(LoggedUsers[i], PointsEarned[i]);
            }
        }
    }

    void Start()
    {
        // Tests
        Login("dummie", "1234");
        AddPointsToUser("dummie", 400);
        Debug.Log("LoggedIn users: ");
        for (int i = 0; i < 4; i++)
        {
            if (LoggedUsers[i].Length != 0)
            {
                Debug.Log(LoggedUsers[i]);
            }
        }
        // UnlockCharacter("dummie", "robotboy");
        if(IsCharacterUnlocked("dummie", "robotboy"))
        {
            Debug.Log("Dummie has RobotBoy!");
        }
    }

    public void Login(string username, string password)
    {
        if(f.CheckPassword(username, password))
        {
            bool loggedIn = false;
            for(int i = 0; i < 4; i++)
            {
                if(LoggedUsers[i].Length == 0)
                {
                    loggedIn = true;
                    LoggedUsers[i] = username;
                    PointsEarned[i] = 0;
                    break;
                }
            }
            if (!loggedIn)
            {
                throw new System.InvalidOperationException("No more than 4 users can be logged in at a time.");
            }
        }
        else
        {
            throw new System.InvalidOperationException("Wrong password.");
        }
    }

    // If username is not logged in, nothing happens
    public void Logout(string username)
    {
        for (int i = 0; i < 4; i++)
        {
            if (LoggedUsers[i].Equals(username))
            {
                LoggedUsers[i] = "";
                // Update database with cache points during logout
                if(PointsEarned[i] != 0)
                {
                    f.AddPointsToUser(username, PointsEarned[i]);
                }
                break;
            }
        }
    }

    public int GetUserPoints(string username)
    {
        int points = f.GetUserPoints(username);
        if(points == -1)
        {
            throw new System.InvalidOperationException("This username does not exist.");
        }
        return points;
    }

    public int GetCharacterCost(string charname)
    {
        int cost = f.GetCharacterCost(charname);
        if(cost == -1)
        {
            throw new System.InvalidOperationException("This character does not exist.");
        }
        return cost;
    }

    // Only works for logged in users
    public void AddPointsToUser(string username, int points)
    {
        for (int i = 0; i < 4; i++)
        {
            if (LoggedUsers[i].Equals(username))
            {
                PointsEarned[i] += points;
                break;
            }
        }
    }

    public void UnlockCharacter(string username, string charname)
    {
        for (int i = 0; i < 4; i++)
        {
            if (LoggedUsers[i].Equals(username))
            {
                f.AddPointsToUser(LoggedUsers[i], PointsEarned[i]);
                PointsEarned[i] = 0;
                int curPoints = f.GetUserPoints(username);
                int charCost = f.GetCharacterCost(charname);
                if(curPoints < charCost)
                {
                    throw new System.InvalidOperationException("Not enough points to unlock character.");
                }
                else
                {
                    try
                    {
                        f.UnlockCharacter(username, charname);
                    }
                    catch(MySqlException e)
                    {
                        switch (e.Number)
                        {
                            case 1062:
                                throw new System.ArgumentException("Character already unlocked.");
                            default:
                                throw new System.InvalidOperationException("An error occurred. Please try again later.");
                        }
                    }
                }
                break;
            }
        }
    }

    public bool IsCharacterUnlocked(string username, string charname)
    {
        return f.isCharacterUnlocked(username, charname);
    }

}
