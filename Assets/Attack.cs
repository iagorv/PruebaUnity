using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();


        if (damageable != null)
        {
            bool gotHit = damageable.Hit(attackDamage);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
            }

        }

    }
}
