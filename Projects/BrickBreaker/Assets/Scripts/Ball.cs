using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Paddle paddle1;
    [SerializeField] Vector2 ballLaunchVelocity;
    [SerializeField] AudioClip[] ballSFX;
    [SerializeField] float randomFactor;

    Vector2 distanceFromPaddle;
    bool isLaunched = false;

    // cached references
    Rigidbody2D rigidBody;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        distanceFromPaddle = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + distanceFromPaddle;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLaunched = true;
            rigidBody.velocity = ballLaunchVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityAdjust = new Vector2(Random.Range(0.0f, randomFactor), Random.Range(0.0f, randomFactor));
        if (isLaunched)
        {
            AudioClip randomClip = ballSFX[Random.Range(0, ballSFX.Length)];
            audioSource.PlayOneShot(randomClip);
            rigidBody.velocity += velocityAdjust;
        }
    }

}
