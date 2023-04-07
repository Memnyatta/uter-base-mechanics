using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UterDamage : MonoBehaviour
{
    public CharacterController _controller;
    public Rigidbody _rb;
    public float DamageForce = 5;
    public ThirdPersonController _th;

    private void Awake()
    {
        MyNameIsUter _controls = new MyNameIsUter();
        _controls.Player.Enable();
        _controls.Player.Interact2.started += input;
    }

    void input(InputAction.CallbackContext context)
    {
        TakeDamage();
    }

    private void Start()
    {
        _th= GetComponent<ThirdPersonController>();
        _rb = GetComponent<Rigidbody>();
        _controller = GetComponent<CharacterController>();
    }
    void TakeDamage()
    {
        _th.canMove= false;
        _controller.enabled = false;
        _rb.isKinematic = false;
        _rb.useGravity = true;
        _rb.AddForce(-transform.forward * DamageForce, ForceMode.Impulse);
        Invoke("GetAlive", 2f);
    }

    private void GetAlive()
    {
        _th.canMove = true;
        _controller.enabled = true;
        _rb.isKinematic = true;
        _rb.useGravity = false;
    }
}
