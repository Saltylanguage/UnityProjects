using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config params
    [Header("Player Config")]
    [SerializeField] float speedFactor;
    [SerializeField] float xBoundsPadding;
    [SerializeField] float yBoundsPadding;
    [SerializeField] int playerHealth = 200;

    [Header("Projectile Config")]
    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed = 10.0f;
    [SerializeField] float laserFireCooldown = 0.1f;
    [SerializeField] AudioClip deathNoise;
    [SerializeField] [Range(0, 2)] float deathNoiseVolume = 0.5f;

    Coroutine firingCoroutine;
    // state variables
    float xMin;
    float xMax;
    float yMin;
    float yMax;


    // Use this for initialization
    void Start()
    {
        SetBounds();
    }

    public int GetHealth()
    {
        return playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speedFactor;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speedFactor;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);

    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    private void SpawnLaser()
    {
        GameObject newLaser = Instantiate
        (laser, transform.position, transform.rotation) as GameObject;
        newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            SpawnLaser();
            yield return new WaitForSeconds(laserFireCooldown);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        KillPlayer();
    }

    void KillPlayer()
    {
        if (playerHealth <= 0)
        {
            FindObjectOfType<LevelLoader>().LoadGameOver();
            AudioSource.PlayClipAtPoint(deathNoise, Camera.main.transform.position, deathNoiseVolume);
            Destroy(gameObject);
        }
    }

    private void SetBounds()
    {
        Camera gameCamera = Camera.main;
        xBoundsPadding = GetComponent<SpriteRenderer>().sprite.rect.width / Screen.width * 2;
        yBoundsPadding = GetComponent<SpriteRenderer>().sprite.rect.height / Screen.height * 2;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xBoundsPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xBoundsPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yBoundsPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yBoundsPadding;
    }



}