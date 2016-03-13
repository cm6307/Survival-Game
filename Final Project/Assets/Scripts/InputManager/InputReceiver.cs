using UnityEngine;
using System.Collections;

public class InputReceiver : MonoBehaviour
{

    private Character m_Character;
    private InputManager m_InputManager;
    private bool m_Jump;

    private void Awake()
    {
        m_Character = GetComponent<Character>();
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
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = m_InputManager.GetAxis("Horizontal");
        m_Character.Move(h, crouch, m_Jump);
        m_Jump = false;
    }

}
