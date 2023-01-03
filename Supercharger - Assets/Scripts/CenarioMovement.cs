using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioMovement : MonoBehaviour
{
    public float speed = -1f;
    public float widthToDestroy;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(speed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < widthToDestroy) {
            Destroy(gameObject);
        }
    }
}
