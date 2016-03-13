using UnityEngine;
using System.Collections;
namespace UnityStandardAssets._2D
{
    public class PressStart : MonoBehaviour
    {

        public GameObject player;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetButtonUp("Start1"))
            {
                Vector3 position = new Vector3(5, 0, 0);
                GameObject player1 = Instantiate(player, position, Quaternion.identity) as GameObject;
                // set player number to the correct control
                PlatformerCharacter2D script1 = player1.GetComponent<PlatformerCharacter2D>();
                script1.SetPlayerNumber(1);
            }

            if (Input.GetButtonUp("Start2"))
            {
                Vector3 position = new Vector3(-3, 5, 0);
                GameObject player2 = Instantiate(player, position, Quaternion.identity) as GameObject;
                // set player number to the correct control
                PlatformerCharacter2D script2 = player2.GetComponent<PlatformerCharacter2D>();
                script2.SetPlayerNumber(2);
            }
        }
    }
}