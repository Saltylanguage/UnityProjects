using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config File")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetPathWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform t in pathPrefab.transform)
        {
            waveWaypoints.Add(t);
        }
        return waveWaypoints;
    }
    public float GetSpawnDelay() { return spawnDelay; }
    public float GetRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }


}
