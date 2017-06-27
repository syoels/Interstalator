using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class PlayerController : MonoBehaviour {

    #region Variables and properties

    public float moveSpeed = 240f;
    public float jumpForce = 300;

    private float gravityScale;
    private Animator animator = null;
    private int animatorSpeed = 0;
    private SpriteRenderer sr;

    private Interactable _closestInteractable;
    public Interactable closestInteractable {
        get { return _closestInteractable; }
        set {
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
            } else if (GameManager.instance.itemManager.heldItem != null) {
                // If we're holding an item than dropping it should be the available interaction
                _closestInteractable = GameManager.instance.itemManager.heldItem;
                GameManager.instance.interactionDisplay.Set(_closestInteractable.GetInteractionText());
            }
        }
    }

    // Property for setting facing direction
    private bool facingRight {
        get { return sr.flipX; }
        set {
            sr.flipX = value;
        }
    }

    private Rigidbody2D body;
    private int playerLayer;

    private bool isOnLadder = false;
    private bool isClimbing {
        get {
            return body.gravityScale == gravityScale;
        }
        set {
            if (value == true) {
                body.gravityScale = 0;
                // Change layer to avoid collisions with floor while on ladder
                gameObject.layer = LayerMask.NameToLayer("Ladder");
            } else {
                body.gravityScale = gravityScale;
                gameObject.layer = playerLayer;
            }
        }

    }

    #endregion

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

        animator.SetFloat(animatorSpeed, Mathf.Abs(body.velocity.x));

        // Interaction handling
        if (Input.GetButtonDown("Interact")) {
            if (closestInteractable != null && closestInteractable.IsInteractable()) {
                closestInteractable.Interact();
                if (!closestInteractable.IsInteractable()) {
                    closestInteractable = null;
                }
            }
        }
    }

    #region Collisions

    // Set closest interactable once we're in range
    void OnTriggerEnter2D(Collider2D other) {
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

        // Change sprite direction
        if (movement != 0) {
            facingRight = movement > 0;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && velocity.y == 0) {
            body.AddForce(new Vector2(0, jumpForce));
        }

        // Ladders
        float vMovement = Input.GetAxis("Vertical");
        if (isOnLadder && vMovement != 0f) {
            isClimbing = true;
            velocity.y = vMovement * Time.deltaTime * moveSpeed;
            body.velocity = velocity;
        } 
        body.velocity = velocity;
    }
}
}