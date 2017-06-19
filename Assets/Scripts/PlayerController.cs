using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class PlayerController : MonoBehaviour {

    public float moveSpeed = 240f;
    public float jumpForce = 300;
    private float gravityScale;
    private Animator animator = null;
    private int animatorSpeed = 0;
    private SpriteRenderer sr;

    // Easier property for setting facing direction
    private bool facingRight {
        get { return sr.flipX; }
        set {
            sr.flipX = value;
        }
    }

    protected Rigidbody2D body;
    private ShipComponent closestInteractable;
    private int playerLayer;

    private bool isOnLadder = false;
    private bool isClimbing {
        get {
            return body.gravityScale == gravityScale;
        }
        set {
            if (value == true) {
                body.gravityScale = 0;
                gameObject.layer = LayerMask.NameToLayer("Ladder");
            } else {
                body.gravityScale = gravityScale;
                gameObject.layer = playerLayer;
            }
        }

    }

    virtual protected void Awake() {
        playerLayer = gameObject.layer;
        body = GetComponent<Rigidbody2D>();
        gravityScale = body.gravityScale;
        animator = GetComponent<Animator>();
        animatorSpeed = Animator.StringToHash("speed");
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Handle movement and interaction input
    virtual protected void Update() {
        // Movement
        HandleStandardMovement();

        float vMovement = Input.GetAxis("Vertical");
        if (isOnLadder && vMovement != 0f) {
            isClimbing = true;
            Vector2 velocity = body.velocity;
            velocity.y = vMovement * Time.deltaTime * moveSpeed;
            body.velocity = velocity;
        }
    }

    // Set closest interactable once we're in range
    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Ladder")) {
            isOnLadder = true;
        }
    }

    // Change the closest interactable once we're out of range
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Ladder")) {
            isOnLadder = false;
            isClimbing = false;
        }
    }

    virtual protected void HandleStandardMovement() {

        // Left-Right
        Vector2 velocity = body.velocity;
        float movement = Input.GetAxis("Horizontal");
        velocity.x = movement * Time.deltaTime * moveSpeed;

        body.velocity = velocity;
        if (animator != null) {
            animator.SetFloat(animatorSpeed, velocity.x);
        }

        // Change sprite direction
        if (movement != 0) {
            facingRight = movement > 0;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && velocity.y == 0) {
            body.AddForce(new Vector2(0, jumpForce));
        }
    }
}
}