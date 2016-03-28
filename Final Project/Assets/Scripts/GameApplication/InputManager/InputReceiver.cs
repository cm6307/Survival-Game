using UnityEngine;
using System.Collections;

public class InputReceiver : MonoBehaviour
{

    private Player m_player;
    private InputManager m_InputManager;
    private bool m_Jump;

    private void Awake()
    {
        m_player = GetComponent<Player>();
        m_InputManager = GetComponent<InputManager>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = m_InputManager.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {
        float h = m_InputManager.GetAxis("Horizontal");
        m_player.Move(h, m_Jump);
        m_Jump = false;
    }

}
