using UnityEngine;
using System.Collections;
using System;

public class PressStart : MonoBehaviour
{

    public GameObject player;
    public GameObject guestLogin;
    public GameObject userBox;
    public GameObject startButton;
    private Vector3[] boxPosition;
    private bool[] free;
    private int nextFree, numOfPlayers;
    private GameObject canvas;

    private static PressStart s_Instance = null;

    public static PressStart instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(PressStart)) as PressStart;
            }

            if (s_Instance == null)
            {
                GameObject obj = new GameObject("PressStart");
                s_Instance = obj.AddComponent(typeof(PressStart)) as PressStart;
            }

            return s_Instance;
        }
    }

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
        boxPosition[0] = farLeft + new Vector3(margin, 100, 0);
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
                free[i] = false;
                if(nextFree == 0)
                {
                    // host
                    nextFree++;
                    CreateUserBox(i, 0, SessionManager.instance.GetUserAt(0));
                    GameObject sb = Instantiate(startButton) as GameObject;
                    sb.transform.SetParent(canvas.transform, false);
                    float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
                    sb.transform.localPosition = new Vector3(0,-canvasHeight/2+20,0);
                    sb.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    CreateGuestLogin(i);
                }
            }
        }
    }

    private void CreateGuestLogin(int player_num)
    {
        GameObject gl = Instantiate(guestLogin) as GameObject;
        gl.GetComponent<GuestLogin>().player_num = player_num;
        gl.GetComponent<GuestLogin>().screen_position = nextFree;
        gl.transform.SetParent(canvas.transform, false);
        gl.transform.localPosition = boxPosition[nextFree];
        gl.transform.localRotation = Quaternion.identity;
        nextFree++;
        // Maybe call a function here from gl to let it know which player they are
        // After login, create player with the correct value player_num in the input manager!
    }
    
    private GameObject CreatePlayer(int player_num)
    {
        GameObject p = Instantiate(player) as GameObject;
        InputManager im = p.GetComponent<InputManager>();
        im.SetID(player_num);
        return p;
    }

    public void CreateUserBox(int player_num, int screen_position, string username)
    {
        GameObject ub = Instantiate(userBox) as GameObject;
        ub.transform.SetParent(canvas.transform, false);
        ub.transform.localPosition = boxPosition[screen_position];
        ub.transform.localRotation = Quaternion.identity;
        ub.GetComponent<UserBox>().SetUserName(username);
        GameObject p = CreatePlayer(player_num+1);
        Player playerScript = p.GetComponent<Player>();
        playerScript.username = username;
        ub.GetComponent<UserBox>().SetPlayer(playerScript);
    }

}