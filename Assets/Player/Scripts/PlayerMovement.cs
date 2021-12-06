using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationSpeed = 100f;

    void Update()
    {
        MoveForwardBackward();
        MoveLeftRight();
        RotateLeftRight();
    }

    private void MoveForwardBackward()
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

    private void MoveLeftRight()
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

    private void RotateLeftRight()
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
}
