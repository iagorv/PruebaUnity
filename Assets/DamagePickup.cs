using UnityEngine;

public class DamagePickup : MonoBehaviour
{
  public int damage = 20;

    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);


  

   
   private void OnTriggerEnter2D(Collider2D collision) {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable){
          damageable.Hit(damage, Vector2.zero);
          Destroy(gameObject);
        }
        
   }

   private void Update() {
       transform.eulerAngles+=spinRotationSpeed * Time.deltaTime;
   }



}
