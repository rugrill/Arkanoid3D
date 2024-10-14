using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class powerUp : MonoBehaviour
{

    private Renderer rend;
    public Vector3 velocity;
    public AudioSource powerUpSound;
    private int powerUpType = 0;
    private bool isInCoroutine = false;

    public GameObject ball;
    public GameObject paddle;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        powerUpType = (byte)Random.Range(1, 4);
        if (powerUpType == 1)
        {
            rend.material.color = Color.blue;
        }
        else if (powerUpType == 2)
        {
            rend.material.color = Color.black;
        }
        else if (powerUpType == 3)
        {
            rend.material.color = Color.green;
        }
        velocity = new Vector3(0, 0, -1.5f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;   
    }

    void SlowMotion()
    {
        Debug.Log("Slow motion activated");
        isInCoroutine = true;
        StartCoroutine(SlowMotionCoroutine(0.5f, 2f));
    }

        IEnumerator SlowMotionCoroutine(float slowVelocityMultiplier, float normalVelocityMultiplier)
    {
        Debug.Log("Slow motion activated in Coroutine");
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        // Slow down all balls
        foreach (GameObject ball in balls)
        {
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballScript != null)
            {
                ballScript.velocity *= slowVelocityMultiplier;
            }
        }
        Debug.Log("Waiting for 5 seconds");
        // Wait for 10 seconds
        yield return new WaitForSeconds(5);

        // Reset the velocity of all balls
        Debug.Log("Corutine finished: Resetting ball velocity");
        foreach (GameObject ball in balls)
        {
            if (ball != null)
            {
                Ball ballScript = ball.GetComponent<Ball>();
                if (ballScript != null)
                {
                    ballScript.velocity *= normalVelocityMultiplier;

                }
            }
        }
        Debug.Log("Slow motion corutine finished, power up destroyed");
        Destroy(gameObject);
    }

    IEnumerator PaddleSizeCoroutine(Vector3 newSize, Vector3 oldSize)
    {
        Debug.Log("Paddle Size started in Coroutine");
        GameObject paddle = GameObject.FindWithTag("Paddle");
        paddleMovNeu paddlescript = paddle.GetComponent<paddleMovNeu>();
        Vector3 oldChildSize = GameObject.FindWithTag("Ball").transform.localScale;
        
        paddlescript.transform.localScale = newSize;

        Debug.Log("Waiting for 10 seconds");

        yield return new WaitForSeconds(10);

        // Reset the velocity of all balls
        Debug.Log("Corutine finished: Resetting size of paddle");
        //When paddle has a child (inital state when the ball sticks to the paddle and the player has to press q to launch the ball)
        // we need to reset the size of the child (ball) after scaling down the paddle
        if (paddle.transform.childCount > 0)
        {
            
            Debug.Log("Paddle has child: Old child size 2: " + oldChildSize);
            paddlescript.transform.localScale = oldSize;
            
            Transform child = paddle.transform.GetChild(0);
            paddle.transform.GetChild(0).transform.SetParent(null);
            child.localScale = oldChildSize;
            child.transform.SetParent(paddle.transform);
        }
        else
        {
            Debug.Log("Paddle has no child");
            paddlescript.transform.localScale = oldSize;
        }

        Destroy(gameObject);
    }

    void ExtraLife() {
         GameManager.Instance.AddLife();
         Destroy(gameObject);
    }

    void MoreBalls() {
        Debug.Log("More balls activated");

        //Instantiate new balls
        for (int i = 0; i <= 2; i++)
        {
            Vector3 position = new Vector3(0, 0.5f, -6.682f);
            Instantiate(ball, position, Quaternion.identity);
            Ball tmp_ball = ball.GetComponent<Ball>();
            tmp_ball.SetPowerUpBall(true);
            tmp_ball.SetShouldMove(true);
        }
    }

    void PaddleSize() {
        Debug.Log("Paddle size activated");
        isInCoroutine = true;
        StartCoroutine(PaddleSizeCoroutine(new Vector3(3, 1, 0.5f), new Vector3(2, 1, 0.5f)));

    }

    private void OnPaddleContact() {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //Activate PowerUp power
        if (powerUpType == 1) {
            SlowMotion();
        } else if (powerUpType == 2) {
            ExtraLife();
        } else if(powerUpType == 3) {
            PaddleSize();
        }
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Collided with: " + other.gameObject.name);
        if(other.CompareTag("Paddle")) {
            Debug.Log("-------------------- Collided with: PADDLE -----------------------");
            powerUpSound.Play();
            OnPaddleContact();
        }
        if(other.CompareTag("BottomWall") && !isInCoroutine) {
            Debug.Log("PowerUp hit bottom wall and was not in coroutine");
            Destroy(gameObject);
        }
    }

    public bool GetIsInCoroutine() {
        return isInCoroutine;
    }
}
