using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementObsolete : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationSpeed = 100f;

    Rigidbody playerRigidbody;

    float initialMovementSpeed;
    float initialRotationSpeed;

    bool halfSpeed = false;

    void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody>();
        initialMovementSpeed = movementSpeed;
        initialRotationSpeed = rotationSpeed;
    }

    void FixedUpdate()
    {
        MoveForwardBackward();
        MoveLeftRight();
        RotateLeftRight();
    }

    void MoveForwardBackward()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }
    }

    void MoveLeftRight()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }
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