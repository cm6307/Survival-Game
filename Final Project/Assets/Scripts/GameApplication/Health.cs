using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    // Total hitpoints
    public int hp = 100;    
    // Enemy or player?  
    public bool isEnemy = false;

    public Slider healthSlider;
    //public Image damageImage;
    //public float flashSpeed = 5f;
    //public Color flashColor = new Color(1f, 0f, 0f, 0.1f);


    // Inflicts damage and check if the object should be destroyed
    public void Damage(int damageCount)
    {
        hp -= damageCount;
        //healthSlider.value = hp;

        if (hp <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a shot?
        Shooting shot = otherCollider.gameObject.GetComponent<Shooting>();
        if (shot != null)
        {
            // Avoid friendly fire
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);
                // Destroy the shot                           
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }
        }
    }
}
