using UnityEngine;
using System.Collections;

public class UnlockButton : MonoBehaviour {

    private string charname, username;

	// Use this for initialization
	void Start () {
	
	}
	
    public void SetCharUser(string c, string u)
    {
        charname = c;
        username = u;
    }

    public void Unlock()
    {
        SessionManager.instance.UnlockCharacter(username, charname);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
