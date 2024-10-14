using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Ball : MonoBehaviour
{
    private bool shouldMove = false;
    private bool powerUpBall = false;
    public GameObject paddle;
    private Vector3 paddleDefaultPosition;
    public Vector3 velocity;
    public GameObject newBall;
    public AudioSource beep;
    // Start is called before the first frame update

    private void Awake()
    {
        // Store the default position of the paddle
        if (paddle != null)
        {
            paddleDefaultPosition = paddle.transform.position;
        }
    }

    void Start()
    { 
        velocity = new Vector3(10, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            shouldMove = true;
        }

        if(shouldMove) {
            if (transform.parent != null) {transform.SetParent(null);}
            transform.position += velocity * Time.deltaTime;
        } else {
            if (transform.parent == null) {transform.SetParent(paddle.transform);}
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Wall")) {
            //Debug.Log(other.gameObject.name + " hit");
            bool isSide = other.gameObject.name == "Left" || other.gameObject.name == "Right";
            //Debug.Log("isSide is: " + isSide);
            if(isSide) {
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            } 
            else {
                velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
            }
            //Debug.Log(velocity.x + ", " + velocity.y + ", " + velocity.z);
        }
        else if(other.CompareTag("Paddle")) {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        else if(other.CompareTag("Obstacle")) {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        else if (other.CompareTag("BottomWall")) {
            Debug.Log("Ball hit bottom wall, Ball was PowerUpBall:" + powerUpBall);
            if (powerUpBall)
            {
                Debug.Log("PowerUpBall hit bottom wall");
                Destroy(gameObject);
            }
            else
            {
                DestroyAllPowerUpsWhenLostLife();
                if (paddle != null)
                {
                    paddle.transform.position = paddleDefaultPosition;
                }
                Vector3 ballPosition = paddle.transform.position + new Vector3(0, 0, 1); //new Vector3(0, 0.5f, -6.682f);
                GameObject instantiatedBall = Instantiate(newBall, ballPosition, Quaternion.identity);
                GameManager.Instance.LostLife();
                Rigidbody rb = instantiatedBall.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Debug.Log("New ball created with velocity 0");
                }
                Destroy(gameObject);
            }
        }
        if (!other.CompareTag("PowerUp")) {
            beep.Play();
        }
        //Debug.Log(other.gameObject.name);
    }

    public void OnMoveBall(InputAction.CallbackContext context) {
        //Debug.Log("????MoveBall called with context: " + shouldMove);
        if(!shouldMove) {
            shouldMove = true;
        }
    }

    public void SetPowerUpBall(bool value) {
        powerUpBall = value;
        Debug.Log("PowerUpBall set to: " + powerUpBall);
    }

    public void SetShouldMove(bool value) {
        shouldMove = value;
        Debug.Log("ShouldMove set to: " + shouldMove);
    }

    private void DestroyAllPowerUpsWhenLostLife()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject powerUp in powerUps)
        {
            if (!powerUp.GetComponent<powerUp>().GetIsInCoroutine())
            {
                Destroy(powerUp);
            }
        }
    }
}
