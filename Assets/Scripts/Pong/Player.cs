using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isRunning;
    public bool isPerson;
    public float speed;
    public float botSpeed;

    [SerializeField]
    private float heightLimit = 4;

    public Transform ball;


    void FixedUpdate()
    {
        if (isRunning)
        {
            MovePaddle();
        }
    }

    void MovePaddle()
    {
        float y;
        if (isPerson)
            y = Input.GetAxisRaw("Vertical") * speed;
        else
            y = AIGame();
        
        if (transform.position.y > heightLimit)
            transform.position = new Vector2(transform.position.x, heightLimit);
        if (transform.position.y < -heightLimit)
            transform.position = new Vector2(transform.position.x, -heightLimit);

        transform.position = new Vector2(transform.position.x, transform.position.y + y * Time.deltaTime);
    }

    float AIGame()
    {
        float fakeInput;
        if (transform.position.y > ball.position.y)
            fakeInput = -1;
        else
            fakeInput = 1;

        if (transform.position.x > 0)
            if (ball.position.x < -5)
                fakeInput = 0;
        if (transform.position.x < 0)
            if (ball.position.x > 5)
                fakeInput = 0;

        float y = fakeInput * botSpeed;

        return y;
    }
}
