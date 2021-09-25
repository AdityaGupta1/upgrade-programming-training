using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb2D;
    private Animator animator;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    [SerializeField]
    private float moveSpeed = 2f;

    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb2D.MovePosition(rb2D.position + new Vector2(x, y) * moveSpeed * Time.deltaTime);
        rb2D.velocity = Vector2.zero;
        
        animator.SetBool("moving", x != 0 || y != 0);

        int direction = animator.GetInteger("direction");
        
        if (y > 0) {
            direction = 1;
        } else if (y < 0) {
            direction = 3;
        }

        if (x > 0) {
            direction = 0;
        } else if (x < 0) {
            direction = 2;
        }
        
        animator.SetInteger("direction", direction);
    }
}