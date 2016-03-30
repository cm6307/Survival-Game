using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserBox : MonoBehaviour {

    [SerializeField]
    private Text usernameText, pointsText;
    private Player m_player;

    public void SetUserName(string username)
    {
        usernameText.text = username;
        int points = SessionManager.instance.GetUserPoints(username);
        pointsText.text = "points: " + points;
    }

    public void updatePoints()
    {
        int points = SessionManager.instance.GetUserPoints(usernameText.text);
        pointsText.text = "points: " + points;
    }

    public void SetPlayer(Player p)
    {
        m_player = p;
    }

    public Player GetPlayer()
    {
        return m_player;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
