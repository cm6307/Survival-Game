using UnityEngine;
using System.Collections;
using System;

public class PressStart : MonoBehaviour
{

    public GameObject player;
    private Vector3 spawnPoint;
    private bool[] free;

    // Use this for initialization
    void Start()
    {
        free = new bool[3];
        free[0] = free[1] = free[2] = true;
        spawnPoint = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            if (Input.GetButtonUp("P" + Convert.ToString(i+1) + "_Start"))
            {
                CreatePlayer(i+1);
            }
        }
    }

    void CreatePlayer(int player_num)
    {
        if (free[player_num - 1])
        {
            free[player_num - 1] = false;
            GameObject p = Instantiate(player, spawnPoint, Quaternion.identity) as GameObject;
            InputManager im = p.GetComponent<InputManager>();
            im.SetID(player_num);
        }
    }

}