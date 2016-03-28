using UnityEngine;
using System.Collections;
using System;

public class PressStart : MonoBehaviour
{

    public GameObject player;
    public GameObject guestLogin;
    private Vector3[] boxPosition;
    private bool[] free;
    private int nextFree, numOfPlayers;
    private GameObject canvas;

    // Use this for initialization
    void Start()
    {
        numOfPlayers = 4;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float guestWidth = guestLogin.GetComponent<RectTransform>().rect.width;
        float margin = 10;
        float padding = (canvasWidth - margin*2 - guestWidth*numOfPlayers) / numOfPlayers;
        Vector3 farLeft = new Vector3(-canvasWidth/2 + guestWidth*2 + margin, 0, 0);
        boxPosition = new Vector3[numOfPlayers];
        free = new bool[numOfPlayers];
        boxPosition[0] = farLeft + new Vector3(margin, 0, 0);
        for(int i = 1; i < numOfPlayers; i++)
        {
            boxPosition[i] = boxPosition[i - 1] + new Vector3(padding + guestWidth, 0, 0);
        }
        for(int i = 0; i < numOfPlayers; i++)
        {
            free[i] = true;
        }
        nextFree = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numOfPlayers; i++)
        {
            if (Input.GetButtonUp("P" + Convert.ToString(i+1) + "_Start") && free[i])
            {
                // CreatePlayer(i+1);
                CreateGuestLogin(i);
            }
        }
    }

    private void CreateGuestLogin(int player_num)
    {
        free[player_num] = false;
        GameObject gl = Instantiate(guestLogin) as GameObject;
        gl.transform.SetParent(canvas.transform, false);
        gl.transform.localPosition = boxPosition[nextFree];
        gl.transform.localRotation = Quaternion.identity;
        nextFree++;
        // Maybe call a function here from gl to let it know which player they are
        // After login, create player with the correct value player_num in the input manager!
    }

    // Here just as an example, have to change that function
    void CreatePlayer(int player_num)
    {
        GameObject p = Instantiate(player, boxPosition[player_num], Quaternion.identity) as GameObject;
        InputManager im = p.GetComponent<InputManager>();
        im.SetID(player_num);
    }

}