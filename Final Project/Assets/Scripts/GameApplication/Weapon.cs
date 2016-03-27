using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // Projectile prefab for shooting
    public Transform shotPrefab;

    // Cooldown in seconds between two shots
    public float shootingRate = 0.25f;

    // Cooldown
    private float shootCooldown;

    private Animator animator;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }


    // Shooting from another script

    // Create a new projectile if possible
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            Shooting shot = shotTransform.gameObject.GetComponent<Shooting>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            Move move = shotTransform.gameObject.GetComponent<Move>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }
        }
    }


    // Is the weapon ready to create a new projectile?
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
