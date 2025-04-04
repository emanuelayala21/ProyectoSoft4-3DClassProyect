using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour {
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed = 4.5f;
    public float jumpForce = 5.5f;
    public float gravity = 9.8f;
    public float fallVelocity = 0f;
    private Vector3 movePlayer;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Animator animator;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    private float  slideVel = 15f;
    private float slopeForceDown = -10f;

    void Start() {
        player = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        // Obtener la entrada del jugador
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer *= playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();
        PlayerSkills();

        animator.SetFloat("Velocity", player.velocity.magnitude);

        animator.SetBool("IsGrounded", player.isGrounded);

        player.Move(movePlayer * Time.deltaTime);
    }

    void camDirection() {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void PlayerSkills() {
        if(player.isGrounded && Input.GetButtonDown("Jump")) {
            fallVelocity = jumpForce;
            animator.SetTrigger("Jump");
        }
        movePlayer.y = fallVelocity;
    }

    void SetGravity() {
        if(player.isGrounded) {
            fallVelocity = -gravity * Time.deltaTime;
        } else {
            fallVelocity -= gravity * Time.deltaTime;
        }
        movePlayer.y = fallVelocity;
        SlideDown();
    }

    void SlideDown() {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
        if(isOnSlope) {
            // Desliza en los ejes X y Z
            movePlayer.x += ((1 - hitNormal.x) * hitNormal.x) * slideVel;
            movePlayer.z += ((1 - hitNormal.z) * hitNormal.z) * slideVel;
            // Aplica la fuerza de deslizamiento en Y
            movePlayer.y = slopeForceDown;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        hitNormal = hit.normal;
    }
}