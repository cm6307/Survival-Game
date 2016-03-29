using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserBox : MonoBehaviour {

    [SerializeField]
    private Text usernameText, pointsText;

    public void SetUserName(string username)
    {
        usernameText.text = username;
        int points = SessionManager.instance.GetUserPoints(username);
        pointsText.text = "points: " + points;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
