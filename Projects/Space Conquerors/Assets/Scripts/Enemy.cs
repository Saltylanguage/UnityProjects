using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    [SerializeField] float currentHealth = 100.0f;
    [SerializeField] Vector2 projectileVelocity;
    [SerializeField] float minDelay = 0.2f;
    [SerializeField] float maxDelay = 3.0f;
    [SerializeField] int pointValue = 150;
    float shotDelay;

    [Header("VFX")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explosionDuration = 0.5f;

    [Header("SFX")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0,2)] float explosionVolume = 0.5f;

    // Use this for initialization
    void Start()
    {
        shotDelay = Random.Range(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        FireOnTimer();
    }

    private void FireOnTimer()
    {
        shotDelay -= Time.deltaTime;
        if (shotDelay <= 0)
        {
            Fire();
            shotDelay = Random.Range(minDelay, maxDelay);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, explosionVolume);
        GameObject projectile = Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = projectileVelocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        currentHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        KillEnemy();
    }

    private void KillEnemy()
    {
        if (currentHealth <= 0)
        {
            FindObjectOfType<GameSession>().AddToScore(pointValue);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionVolume);
            Destroy(explosion, explosionDuration);
            Destroy(gameObject);
        }
    }
}
