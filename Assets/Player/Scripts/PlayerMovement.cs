using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationSpeed = 100f;

    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }

    Rigidbody playerRigidbody;

    Vector3 playerDirection;
    public Vector3 PlayerDirection { get { return playerDirection; } }
    Vector3 playerRotation;

    float initialMovementSpeed;
    float initialRotationSpeed;

    bool halfSpeed = false;
    public bool HalfSpeed { get { return halfSpeed; } }

    void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody>();
        initialMovementSpeed = movementSpeed;
        initialRotationSpeed = rotationSpeed;
    }

    void Update() 
    {
        GetTranslateInput();
    }

    void GetTranslateInput()
    {
        playerDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.Z))
        {
            playerDirection += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerDirection += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerDirection += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            playerDirection += Vector3.right;
        }
        //playerDirection = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        MovePlayer(playerDirection);
        RotateLeftRight();
    }

    public void MovePlayer(Vector3 direction)
    {
        Vector3 localDirection = transform.TransformDirection(direction);
        playerRigidbody.MovePosition(transform.position + localDirection * movementSpeed * Time.fixedDeltaTime);
    }

    void RotateLeftRight()
    {
        Vector3 rotationVector = Vector3.zero;
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector = -Vector3.up * Time.deltaTime * rotationSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationVector = Vector3.up * Time.deltaTime * rotationSpeed;
        }
        transform.Rotate(rotationVector);
    }

    public IEnumerator SetPlayerSpeedToHalf()
    {
        if(!halfSpeed)
        {
            while(movementSpeed > initialMovementSpeed / 2)
            {
                movementSpeed -= 0.05f;
                yield return new WaitForSeconds(0.1f);
            }
            halfSpeed = true;
        }
    }

    public IEnumerator SetPlayerSpeedToMax()
    {
        if(halfSpeed)
        {
            while(movementSpeed < initialMovementSpeed)
            {
                movementSpeed += 0.05f;
                yield return new WaitForSeconds(0.1f);
            }
            movementSpeed = initialMovementSpeed;
            rotationSpeed = initialRotationSpeed;
            halfSpeed = false;
        }
    }

}