using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

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

        // TODO: Add validation
        // nameInputField.characterValidation = InputField.CharacterValidation.Alphanumeric;

    }

    private void UsernameListener(string un)
    {
        error = false;
        username = un;
    }

    private void PasswordListener(string pw)
    {
        error = false;
        password = pw;
    }

    public void LoginSubmit()
    {
        try
        {
            SessionManager.instance.Login(username, password);
            SceneManager.LoadScene(5);
        }
        catch(InvalidOperationException ioe)
        {
            error = true;
            errorMessage = ioe.Message;
        }
    }

    void OnGUI()
    {
        if (error)
        {
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height / 2 -100, 200f, 200f), errorMessage);
        }
    }

}
