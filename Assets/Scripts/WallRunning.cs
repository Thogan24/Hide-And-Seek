using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunningForce;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    public float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    public RaycastHit rightWallhit;
    private bool leftWall;
    private bool rightWall;

    [Header("References")]
    public Transform orientation;
    private Player pm;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Player>();
    }

    private void CheckForWall() // Checks for Wall
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround() // Checks if player is minimum height above the ground
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - Wallingrunning (Checks if all conditions are met)
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround)
        {

        }
    }

    private void StartWallRun()
    {

    }

    private void WallRunningMovement()
    {

    }

    private void StopWallRun()
    {

    }

    void Update()
    {
        CheckForWall();
    }
}
