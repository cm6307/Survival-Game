using UnityEngine;
using System.Collections;

public class HealthBarFollow : MonoBehaviour {

    private GameObject target;
 
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Character");
        if(target != null)
        {
            Vector3 wantedPos = Camera.main.WorldToViewportPoint(target.transform.position);
            transform.position = wantedPos;
        }
    }
}
