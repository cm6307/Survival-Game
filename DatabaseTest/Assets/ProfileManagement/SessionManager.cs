using UnityEngine;
using System.Collections;

public class SessionManager : MonoBehaviour {

    public Facade f;
    private string[] LoggedUsers = new string[4];
    private int[] PointsEarned = new int[4];

    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            LoggedUsers[i] = null;
        }
        Login("dummie", "1234");
        Debug.Log("LoggedIn users: ");
        for (int i = 0; i < 4; i++)
        {
            if (LoggedUsers[i] != null)
            {
                Debug.Log(LoggedUsers[i]);
            }
        }
    }

    public void Login(string username, string password)
    {
        if(f.CheckPassword(username, password))
        {
            bool loggedIn = false;
            for(int i = 0; i < 4; i++)
            {
                if(LoggedUsers[i] == null)
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

    public void Logout(string username)
    {

    }

    public int GetUserPoints(string username)
    {
        return 0;
    }

    public int GetCharacterCost(string charname)
    {
        return 0;
    }

    public void AddPointsToUser(string username, int points)
    {

    }

    public void UnlockCharacter(string username, string charname)
    {

    }

    public bool IsCharacterUnlocked(string username, string charname)
    {
        return false;
    }

}
