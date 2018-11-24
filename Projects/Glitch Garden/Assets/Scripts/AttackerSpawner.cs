using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour {

    [SerializeField] Attacker attackerPrefab;
    bool spawn = true;
    [Range(0, 5)]
    [SerializeField] float minDelay = 0.5f;
    [Range(1,10)]
    [SerializeField] float maxDelay = 3.0f;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        Instantiate(attackerPrefab, transform.position, transform.rotation);
    }
}
