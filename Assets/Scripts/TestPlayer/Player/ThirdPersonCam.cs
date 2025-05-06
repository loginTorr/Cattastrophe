using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour {
    [Header("References")]
    public Transform Orientation;
    public Transform Player;
    public Transform PlayerBody;
    public Rigidbody rb;

    public float RotationSpeed = 7;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        Vector3 ViewDirection = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        Orientation.forward = ViewDirection.normalized;

        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 InputDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;

        if (InputDirection != Vector3.zero) {
            PlayerBody.forward = Vector3.Slerp(PlayerBody.forward, InputDirection.normalized, Time.deltaTime * RotationSpeed);
        }
     }
}
