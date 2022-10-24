using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class Player3DExample : MonoBehaviour
{

    public float moveSpeed = 8f;
    public float _totalHp;
    public Joystick joystick;
    public static UnityAction JoystickEvent;
    private void OnEnable()
    {

    }
    void FixedUpdate()
    {


        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
        if (moveVector != Vector3.zero && joystick != null && _totalHp > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
    IEnumerator dust()
    {
        yield return new WaitForSeconds(1f);
        //_dustEffect.Play();
    }

}