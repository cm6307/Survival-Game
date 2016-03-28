using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GuestLogin : MonoBehaviour {

    [SerializeField]
    private InputField usernameInputField = null;

    [SerializeField]
    private InputField passwordInputField = null;

    private string username, password, errorMessage;
    private bool error;

    private void Start()
    {
        var usernameSE = new InputField.SubmitEvent();
        usernameSE.AddListener(UsernameListener);
        usernameInputField.onEndEdit = usernameSE;

        var passwordSE = new InputField.SubmitEvent();
        passwordSE.AddListener(PasswordListener);
        passwordInputField.onEndEdit = passwordSE;

        error = false;

    }

    private void UsernameListener(string un)
    {
        error = false;
        username = un;
        Debug.Log(un);
    }

    private void PasswordListener(string pw)
    {
        error = false;
        password = pw;
    }

    public void LoginSubmit()
    {
        Debug.Log("Logging in " + username + " with password " + password);
        try
        {
            SessionManager.instance.Login(username, password);
            // create the user box
            // destroy this one
            Destroy(this.gameObject);
        }
        catch (InvalidOperationException ioe)
        {
            error = true;
            errorMessage = ioe.Message;
        }
    }

    void OnGUI()
    {
        if (error)
        {
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height / 2 - 100, 200f, 200f), errorMessage);
        }
    }

}
