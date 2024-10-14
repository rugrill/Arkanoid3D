using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public byte life = 1;
    public bool hasPowerUp = false;
    private int scoreValue = 0;

    public GameObject powerUp;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ball")) {
            //Debug.Log("--------------------Obstacle hit-------------------");
            life--;
            if(life == 0) {
                GameManager.Instance.CheckGameWin();
                //Debug.Log("Obstacle destroyed");
                GameManager.Instance.AddToScore(scoreValue);
                DropPowerUp();
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set lives
        life = (byte)Random.Range(1, 4); //1-3 lives
        //powerup chance
        hasPowerUp = Random.Range(0, 4) == 0 ? true : false; // 1 in 5 chance

        //set color
        setColor();

    }

    void DropPowerUp() {
        if(hasPowerUp) {
            Quaternion rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(powerUp, transform.position, rotation);
        }
    }



    // Update is called once per frame
    void Update()
    {
        setColor();
    }

    private void setColor() {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null) {
            if (life == 1) {
                renderer.material.color = Color.green;
                scoreValue = 10;
            } else if (life == 2) {
                renderer.material.color = Color.yellow;
                scoreValue = 50;
            } else if (life == 3) {
                renderer.material.color = Color.red;
                scoreValue = 100;
            }
        }
    }
}
