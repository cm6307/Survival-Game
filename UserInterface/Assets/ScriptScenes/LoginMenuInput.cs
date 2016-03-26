using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginMenuInput : MonoBehaviour {

    [SerializeField]
    private InputField nameInputField = null;

    [SerializeField]
    private Button submitButton = null;

    private void Start()
    {
        // Add listener to catch the submit
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit = se;

        // Add validation
       // nameInputField.characterValidation = InputField.CharacterValidation.Alphanumeric;

    }

    private void SubmitName(string name)
    {
        //What to do with the value from input field
        Debug.Log(name);
    }
}
