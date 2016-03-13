using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static bool GetButton(string name)
    {
        return Input.GetButton(name);
    }


    public static bool GetButtonDown(string name)
    {
        return Input.GetButtonDown(name);
    }


    public static bool GetButtonUp(string name)
    {
        return Input.GetButtonUp(name);
    }

    public static float GetAxis(string name)
    {
        return Input.GetAxis(name);
    }

}
