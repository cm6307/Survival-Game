using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockOrSelect : MonoBehaviour {

    [SerializeField]
    private Text characterText;
    private string charname;
    [SerializeField]
    private Text usernameText;
    private string username;
    private bool unlocked;

	// Use this for initialization
	void Start () {
        charname = characterText.text;
        username = usernameText.text;
        unlocked = SessionManager.instance.IsCharacterUnlocked(username, charname);
        if (!unlocked)
        {
            // create unlock button here
            Debug.Log("Not unlocked!");
        }
        else
        {
            // create select button here
            Debug.Log("Unlocked!");
        }
	}
	
    public void Unlock()
    {
        SessionManager.instance.UnlockCharacter(username, charname);
        // destroy unlock button, create select one
    }

    public void Select()
    {
        // idk, select somehow
    }

	// Update is called once per frame
	void Update () {
	
	}
}
