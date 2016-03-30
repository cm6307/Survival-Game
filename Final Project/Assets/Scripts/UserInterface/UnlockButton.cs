using UnityEngine;
using System.Collections;

public class UnlockButton : MonoBehaviour {

    private string charname, username;
    private UnlockOrSelect us;

	// Use this for initialization
	void Start () {
	
	}

    public void SetUnlockOrSelect(UnlockOrSelect u)
    {
        us = u;
    }
	
    public void SetCharUser(string c, string u)
    {
        charname = c;
        username = u;
    }

    public void Unlock()
    {
        SessionManager.instance.UnlockCharacter(username, charname);
        us.SetUnlocked();
        Destroy(this.gameObject);
    }

}
