using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class paddleMovNeu : MonoBehaviour
{
    public float speed = 11f;
    public Transform floor;
    public Transform paddle;
    public GameObject ball;

    private Vector2 movDir;


    public void Move(InputAction.CallbackContext context) {
        movDir = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        float maxX = floor.localScale.x * 5f * 0.5f * paddle.localScale.x * 0.5f;
        float posX = transform.position.x + movDir.x * speed * Time.deltaTime;  
        float clampedX = Mathf.Clamp(posX, -maxX, maxX);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
