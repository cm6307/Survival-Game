using UnityEngine;
using System.Collections;

public class InputReceiver : MonoBehaviour
{

    private Player m_player;
    private InputManager m_InputManager;
    private bool m_Jump, m_Attack;

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
        if (!m_Attack)
        {
            m_Attack = m_InputManager.GetButtonDown("Attack1");
        }
    }


    private void FixedUpdate()
    {
        float h = m_InputManager.GetAxis("Horizontal");
        m_player.Move(h, m_Jump);
        m_player.Attack(m_Attack);
        m_Jump = m_Attack = false;
    }

}
