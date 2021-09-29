using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb2D;
    private Animator animator;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    [SerializeField]
    private float moveSpeed = 2f;

    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Direction = Animator.StringToHash("direction");

    private void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb2D.MovePosition(rb2D.position + new Vector2(x, y) * moveSpeed * Time.deltaTime);
        rb2D.velocity = Vector2.zero;
        
        animator.SetBool(Moving, x != 0 || y != 0);

        int direction = animator.GetInteger(Direction);
        
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
        
        animator.SetInteger(Direction, direction);
    }
}