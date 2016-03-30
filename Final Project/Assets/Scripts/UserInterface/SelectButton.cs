using UnityEngine;
using System.Collections;

public class SelectButton : MonoBehaviour {

    [SerializeField]
    private GameObject m_char;
    private Player m_player;

    public void SetChar(GameObject g)
    {
        m_char = g;
    }

	public void SetPlayer(Player p)
    {
        m_player = p;
    }

    public void Select()
    {
        m_player.SetCurrentCharacter(m_char);
    }
}
