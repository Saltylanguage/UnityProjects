using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour {

    [SerializeField] Attacker attackerPrefab;
    bool spawn = true;
    [Range(0, 5)]
    [SerializeField] float minDelay = 4.0f;
    [Range(1,10)]
    [SerializeField] float maxDelay = 5.0f;

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
        Attacker newAttacker = Instantiate(attackerPrefab, transform.position, transform.rotation) as Attacker;
        newAttacker.transform.parent = transform;
      
    }
}
