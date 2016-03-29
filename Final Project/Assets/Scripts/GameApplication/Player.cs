using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject currentCharacter;
    private Character characterScript;
    public string username;
    public int points, number;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        characterScript = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Character GetCharacterScript()
    {
        return characterScript;
    }

    public void SetCharacterScript(GameObject character)
    {
        characterScript = character.GetComponent<Character>();
    }

    public void Attack(bool attack)
    {
        if (characterScript != null)
        {
            characterScript.Attack(attack);
        }
    }

    public void Move(float x, bool jump)
    {
        if(characterScript != null)
        {
            characterScript.Move(x, jump);
        }
    }
}
