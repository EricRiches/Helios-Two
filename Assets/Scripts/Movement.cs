using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Transform cameraTransform;
    public float moveSpeed;
    public float sprintSpeed;
    [SerializeField] float cameraSens = 100f; //sens for camera movement.
    [SerializeField] float cameraVerticalClamp = 80;
    private float xRotation = 0f; // used to track current vertical camera rotation.

    Vector2 inputDir;
    Vector2 mouseInputDir;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() {
        // DEBUG REMOVE LATER | press escape to unlock mouse ----------------------------------------------------------------------------------------
        if(Input.GetKeyDown(KeyCode.Escape)) Cursor.lockState = Cursor.lockState == CursorLockMode.None? CursorLockMode.Locked: CursorLockMode.None;


        GetInput(); // input should be gotten every frame.
        RotatePlayer();

    }
    private void FixedUpdate()
    {

        MovePlayer(); // player should move every physics (fixed) update.
        
    }

    /// <summary>
    /// Moves the player based off their current <see cref="inputDir"/> set in <see cref="Update"/>
    /// </summary>
    void MovePlayer()
    {
        // modifies input based off camera direction.---- SetY is a custom function that does exactly what it says. in this case, stops player from being able to walk up into the air.
        Vector3 moveDir = cameraTransform.forward.SetY(0) * inputDir.y + cameraTransform.right.SetY(0) * inputDir.x;

        //Update the GameObject's position with the detected move direction and speed. Check if leftShift held to modify sprinting speed.
        transform.position += moveDir * Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed);
    }

    /// <summary>
    /// rotates the camera and player respective to mouse movements.
    /// </summary>
    void RotatePlayer()
    {
        xRotation -= mouseInputDir.y;
        xRotation = Mathf.Clamp(xRotation, -cameraVerticalClamp, cameraVerticalClamp);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up, mouseInputDir.x);
    }

    /// <summary>
    /// Handles recieving any input that should be consistant throughout the frame, such as movement inputs.
    /// </summary>
    void GetInput()
    {
        inputDir = new( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );
        mouseInputDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * cameraSens * Time.deltaTime; // multiplied by deltatime because it is called in update not fixedupdate.
    }

}
