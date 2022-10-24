using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public Joystick joystick;
    private void OnEnable()
    {
    }
    void FixedUpdate()
    {


        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
        if (moveVector != Vector3.zero && joystick != null)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.Self);
        }
    }


}
