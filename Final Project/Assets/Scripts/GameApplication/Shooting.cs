using UnityEngine;
using System.Collections;

//Projectile behaviour

public class Shooting : MonoBehaviour
{
    // Damage inflicted
    public int damage = 1;    

    // Projectile damage player or enemies?
    public bool isEnemyShot = false;
    private Animator animator;

    void Start()
    {
        // Limited time to live to avoid any leak
        Destroy(gameObject, 20); // 20sec
    }


}
