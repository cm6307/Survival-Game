using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour {

    [SerializeField]
    private InputField usernameInputField = null;

    [SerializeField]
    private InputField passwordInputField = null;

    [SerializeField]
    private InputField confirmPasswordInputField = null;

    private string username, password, confirmPassword, errorMessage;
    private bool error;

    private void Start()
    {
        var usernameSE = new InputField.SubmitEvent();
        usernameSE.AddListener(UsernameListener);
        usernameInputField.onEndEdit = usernameSE;

        var passwordSE = new InputField.SubmitEvent();
        passwordSE.AddListener(PasswordListener);
        passwordInputField.onEndEdit = passwordSE;

        var confirmPasswordSE = new InputField.SubmitEvent();
        confirmPasswordSE.AddListener(ConfirmPasswordListener);
        confirmPasswordInputField.onEndEdit = confirmPasswordSE;

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

    private void ConfirmPasswordListener(string cp)
    {
        error = false;
        confirmPassword = cp;
    }

    public void RegisterSubmit()
    {
        Debug.Log("Registering " + username + " with password " + password + " and confirmation " + confirmPassword);
        if (!password.Equals(confirmPassword))
        {
            error = true;
            errorMessage = "Password and confirm password must be the same.";
        }
        else
        {
            try
            {
                Registration.instance.CreateAccount(username, password);
                SceneManager.LoadScene(1);
            }
            catch (InvalidOperationException ioe)
            {
                error = true;
                errorMessage = ioe.Message;
            }
            catch (ArgumentException ae)
            {
                error = true;
                errorMessage = ae.Message;
            }
        }
    }

    void OnGUI()
    {
        if (error)
        {
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height / 2 - 150, 200f, 200f), errorMessage);
        }
    }

}
