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
    private Interactable _closestInteractable;
    private Interactable closestInteractable {
        get { return _closestInteractable; }
        set {
            if (value == _closestInteractable) {
                return;
            }

            if (_closestInteractable != null) {
                _closestInteractable.SetGlow(false);
                GameManager.instance.interactionDisplay.Clear();
                _closestInteractable = null;
            }

            // Assumes that closest interactable is interactable
            _closestInteractable = value;

            if (_closestInteractable != null) {
                _closestInteractable.SetGlow(true);
                GameManager.instance.interactionDisplay.Set(_closestInteractable.GetInteractionText());
            }
        }
    }

    // Easier property for setting facing direction
    private bool facingRight {
        get { return sr.flipX; }
        set {
            sr.flipX = value;
        }
    }

    protected Rigidbody2D body;
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

    void Awake() {
        playerLayer = gameObject.layer;
        body = GetComponent<Rigidbody2D>();
        gravityScale = body.gravityScale;
        animator = GetComponent<Animator>();
        animatorSpeed = Animator.StringToHash("speed");
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Handle movement and interaction input
    void Update() {
        // Movement
        HandleStandardMovement();

        // Ladders
        float vMovement = Input.GetAxis("Vertical");
        if (isOnLadder && vMovement != 0f) {
            isClimbing = true;
            Vector2 velocity = body.velocity;
            velocity.y = vMovement * Time.deltaTime * moveSpeed;
            body.velocity = velocity;
        } 
            
        animator.SetFloat(animatorSpeed, Mathf.Abs(body.velocity.x));

        // Interaction button
        if (Input.GetButtonDown("Interact")) {
            if (closestInteractable != null && closestInteractable.IsInteractable()) {
                closestInteractable.Interact();
            }
        }
    }

    #region Collisions

    // Set closest interactable once we're in range
    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Ladder")) {
            isOnLadder = true;
            return;
        }

        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable.IsInteractable()) {
            closestInteractable = interactable;
        }
    }

    // Change the closest interactable once we're out of range
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Ladder")) {
            isOnLadder = false;
            isClimbing = false;
            return;
        }

        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable == closestInteractable) {
            closestInteractable = null;
        }
    }

    #endregion

    private void HandleStandardMovement() {

        // Left-Right
        Vector2 velocity = body.velocity;
        float movement = Input.GetAxis("Horizontal");
        velocity.x = movement * Time.deltaTime * moveSpeed;
        body.velocity = velocity;

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