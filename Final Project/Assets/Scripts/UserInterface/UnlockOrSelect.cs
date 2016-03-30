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
    [SerializeField]
    private Button selectButton;
    private Button ubInstance = null;
    private Button seInstance = null;
    private GameObject canvas;
    [SerializeField]
    private GameObject m_char;
    [SerializeField]
    private Vector3 position;

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
            CreateSelectButton();
        }
	}

    private void CreateUnlockButton()
    {
        Vector3 parentPosition = usernameText.transform.parent.localPosition;
        Button ub = Instantiate(unlockButton) as Button;
        ub.transform.SetParent(canvas.transform, false);
        ub.transform.localPosition = parentPosition + position;
        ub.transform.localRotation = Quaternion.identity;
        ub.GetComponent<UnlockButton>().SetCharUser(charname, username);
        ubInstance = ub;
        ub.GetComponent<UnlockButton>().SetUnlockOrSelect(this);
    }

    public void CreateSelectButton()
    {
        Vector3 parentPosition = usernameText.transform.parent.localPosition;
        Button se = Instantiate(selectButton) as Button;
        se.transform.SetParent(canvas.transform, false);
        se.transform.localPosition = parentPosition + position;
        se.transform.localRotation = Quaternion.identity;
        seInstance = se;
        Player p = GetComponentInParent<UserBox>().GetPlayer();
        se.GetComponent<SelectButton>().SetChar(m_char);
        se.GetComponent<SelectButton>().SetPlayer(p);
    }

	// Update is called once per frame
	void Update () {
	    if(ubInstance == null && unlocked == true && seInstance == null)
        {
            CreateSelectButton();
        }
	}

    public void SetUnlocked()
    {
        unlocked = true;
    }
}
