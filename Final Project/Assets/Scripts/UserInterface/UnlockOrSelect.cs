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
    private bool unlocked = false;
    [SerializeField]
    private Button unlockButton;
    private Button ubInstance = null;
    private Button seInstance = null;
    private GameObject canvas;

	// Use this for initialization
	void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        charname = characterText.text;
        username = usernameText.text;
        unlocked = SessionManager.instance.IsCharacterUnlocked(username, charname);
        if (!unlocked)
        {
            // create unlock button here
            CreateUnlockButton();
        }
        else
        {
            // create select button here
            Debug.Log("Unlocked!");
        }
	}

    private void CreateUnlockButton()
    {
        Vector3 parentPosition = usernameText.transform.parent.localPosition;
        Button ub = Instantiate(unlockButton) as Button;
        ub.transform.SetParent(canvas.transform, false);
        ub.transform.localPosition = parentPosition + new Vector3(-30,-120,0);
        ub.transform.localRotation = Quaternion.identity;
        ub.GetComponent<UnlockButton>().SetCharUser(charname, username);
        ubInstance = ub;
    }

	// Update is called once per frame
	void Update () {
	    if(ubInstance == null && unlocked == true && seInstance == null)
        {
            // Create select button
        }
	}
}
