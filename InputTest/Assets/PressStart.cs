using UnityEngine;
using System.Collections;
namespace UnityStandardAssets._2D
{
    public class PressStart : MonoBehaviour
    {

        public GameObject player;

        private bool []free;

        // Use this for initialization
        void Start()
        {
            free = new bool[2];
            free[0] = free[1] = true;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetButtonUp("Start1"))
            {
                Vector3 position = new Vector3(5, 0, 0);
                CreatePlayer(1, position);
            }

            if (Input.GetButtonUp("Start2"))
            {
                Vector3 position = new Vector3(-3, 5, 0);
                CreatePlayer(2, position);
            }
        }

        void CreatePlayer(int player_num, Vector3 pos)
        {
            if (free[player_num-1])
            {
                free[player_num-1] = false;
                GameObject p = Instantiate(player, pos, Quaternion.identity) as GameObject;
                PlatformerCharacter2D script = p.GetComponent<PlatformerCharacter2D>();
                script.SetPlayerNumber(player_num);
            }
        }

    }
}