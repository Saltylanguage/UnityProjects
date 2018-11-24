using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    // config params
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int currentWaypointIndex = 0;

    void Start()
    {
        if (waveConfig != null)
        {
            waypoints = waveConfig.GetPathWaypoints();
            transform.position = waypoints[currentWaypointIndex].transform.position;
        }
    }

    void Update()
    {
        FollowPath();
    }

    public void SetWaveConfig(WaveConfig wave)
    {
        waveConfig = wave;
    }

    private void FollowPath()
    {
        if (currentWaypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[currentWaypointIndex].position;
            var distanceToTravel = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, distanceToTravel);
            if (transform.position == targetPosition)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
