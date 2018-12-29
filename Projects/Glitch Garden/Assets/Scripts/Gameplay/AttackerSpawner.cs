using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour {

    [SerializeField] Attacker [] attackerPrefabs;
    bool continueSpawning = true;
    [Range(0, 5)]
    [SerializeField] float minDelay = 4.0f;
    [Range(1,10)]
    [SerializeField] float maxDelay = 5.0f;


    IEnumerator Start()
    {
        while (continueSpawning)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            SpawnAttacker();
        }
    }


    private void Spawn(Attacker prefab)
    {
        Attacker newAttacker = Instantiate(prefab, transform.position, transform.rotation) as Attacker;
        newAttacker.transform.parent = transform;
    }

    private void SpawnAttacker()
    {
        int x = (int)Random.Range(0, attackerPrefabs.Length);
        Spawn(attackerPrefabs[x]);
    }
}
