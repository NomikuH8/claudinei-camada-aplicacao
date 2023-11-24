using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float heightLimit;
    [SerializeField]
    private float speed;

    private Vector2 origin;
    private float x;
    private float y;

    void Awake() {
        origin = new Vector2(0, 0);

        x = speed;
        y = speed;
    }

    void Update()
    {
        if (transform.position.y > heightLimit && y == speed)
            y = -y;
        if (transform.position.y < -heightLimit && y == -speed)
            y = -y;
        
        if (transform.position.x > 9.14f ||
            transform.position.x < -9.14f)
            transform.position = origin;

        transform.Translate(new Vector3(x, y, 0) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        x = -x;
    }
}
