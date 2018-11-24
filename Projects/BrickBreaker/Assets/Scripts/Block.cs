using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockImpactParticles;
    [SerializeField] Sprite[] damageLevelSprites;

    // cached references
    Level currentLevel;

    // state variables
    [SerializeField] int currentDamage = 0; //TODO: remove debug serialize

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        if (tag == "Breakable")
        {
            int maxHealth = damageLevelSprites.Length + 1;
            currentDamage++;

            if (currentDamage >= maxHealth)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextDamageSprite();
            }

        }
    }

    private void ShowNextDamageSprite()
    {
        if (damageLevelSprites[currentDamage - 1])
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = damageLevelSprites[currentDamage - 1];
        }
        else
        {
            Debug.Log("Block is missing a sprite." + "Object Name: " + gameObject.name);
        }
    }

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        currentLevel = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            currentLevel.CountBlocks();
        }
    }

    void DestroyBlock()
    {
        FindObjectOfType<GameSession>().AddToScore();
        Destroy(gameObject);
        currentLevel.BreakBlock();
        SpawnImpactParticles();
    }

    private void SpawnImpactParticles()
    {
        GameObject impactParticleEffect = Instantiate(blockImpactParticles, transform.position, transform.rotation);
        Destroy(impactParticleEffect, 1.0f);
    }
}
