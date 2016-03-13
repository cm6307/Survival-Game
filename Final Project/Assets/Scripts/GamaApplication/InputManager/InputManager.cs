using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

    private string inputID;

    public void SetID(int newID)
    {
        inputID = Convert.ToString(newID);
    }

    public bool GetButton(string name)
    {
        return Input.GetButton("P" + inputID  + "_" + name);
    }


    public bool GetButtonDown(string name)
    {
        return Input.GetButtonDown("P" + inputID + "_" + name);
    }


    public bool GetButtonUp(string name)
    {
        return Input.GetButtonUp("P" + inputID + "_" + name);
    }

    public float GetAxis(string name)
    {
        return Input.GetAxis("P" + inputID + "_" + name);
    }

}
