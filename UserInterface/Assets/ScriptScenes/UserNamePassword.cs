using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserNamePassword : MonoBehaviour {

    [SerializeField]
    private InputField _userName;
    [SerializeField]
    private InputField _passWord;

    string a;
    string b;

    public void UserIn() {
        string userName = _userName.text;
        a = userName;
        output(a);
    }
    public void PassIn() {
        string passWord = _passWord.text;
        b = passWord;
        output(b);
    }
    public void output(string name){
        Debug.Log(name);
    }
    
}
