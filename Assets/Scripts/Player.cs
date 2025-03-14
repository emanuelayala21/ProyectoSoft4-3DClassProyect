using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour {
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed = 2.5f;
    public float jumpForce = 5.5f;
    public float gravity = 9.8f;
    public float fallVelocity = 0f;
    private Vector3 movePlayer;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    void Start() {
        player = GetComponent<CharacterController>();
    }

    void Update() {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer *= playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();
        PlayerSkill();

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

    void SetGravity() {
        if(player.isGrounded) {
            if(fallVelocity < 0) // Solo reiniciar si est� cayendo
                fallVelocity = 0f;
        } else {
            fallVelocity -= gravity * Time.deltaTime;
        }

        movePlayer.y = fallVelocity;
    }


    public void PlayerSkill() {
        if(player.isGrounded && Input.GetButtonDown("Jump")) {
            fallVelocity = jumpForce; // Solo modifica fallVelocity
        }
    }
}