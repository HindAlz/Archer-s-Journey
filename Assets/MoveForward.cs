using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        if (transform.position.x > 200)
            Destroy(gameObject);
        if (transform.position.x < -50)
            Destroy(gameObject);
    }
}
