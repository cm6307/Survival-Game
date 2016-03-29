using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject currentCharacter;
    private MovingObject characterMovingObject;
    public string username;
    public int points, number;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        characterMovingObject = currentCharacter.GetComponent<MovingObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move(float x, bool jump)
    {
        // Call the move function here
    }
}
