using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    Rigidbody2D rigidBody;

    Vector2 targetVelocity;
    Vector2 mVelocity = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            targetVelocity = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A was pressed");
            targetVelocity = Vector2.left * movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D was pressed");
            targetVelocity = Vector2.right * movementSpeed;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space was pressed");
            rigidBody.AddForce(new Vector2(0.0f, jumpForce));
            //rigidBody.AddRelativeForce(new Vector2(0.0f, jumpForce));
        }
        //rigidBody.velocity = targetVelocity;
        rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, targetVelocity, ref mVelocity, 0.5f);
    }
}
