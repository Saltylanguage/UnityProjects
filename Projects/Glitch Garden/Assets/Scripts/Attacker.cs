using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    [Range(0, 5)]
    float currentSpeed = 1.0f;

    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
