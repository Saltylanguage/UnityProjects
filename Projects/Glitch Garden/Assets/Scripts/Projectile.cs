using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] int damage = 50;
    [SerializeField] Rigidbody2D mRigidBody;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemyHealth = collision.GetComponent<Health>();
        if(enemyHealth)
        {
            enemyHealth.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    void Update ()
    {
        transform.Rotate(Vector3.forward, -rotationSpeed);
        transform.transform.position += Vector3.right * speed * Time.deltaTime;
	}
}
