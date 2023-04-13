using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private ThirdPersonController _thirdPersonController;
    [SerializeField]
    private PlayerInteraction _playerInteraction;

    private void Start()
    {
        _playerInteraction = GetComponent<PlayerInteraction>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void FixedUpdate()
    {
        if (_playerInteraction.currentlyPickedUpObject != null)
        {
            _animator.SetBool("isPicked", true);
        }
        else
        {
            _animator.SetBool("isPicked", false);
        }

        if (_thirdPersonController.isGrounded)
        {
            _animator.SetBool("isFloating", false);
        }
        else
        {
            _animator.SetBool("isFloating", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetTrigger("isJumped");
        }

        if (_thirdPersonController.climbing)
        {
            _animator.SetBool("isClimbing", true);
        }
        else
        {
            _animator.SetBool("isClimbing", false);
        }

        if (_characterController.velocity.x != 0 || _characterController.velocity.z != 0)
        {
            _animator.SetBool("isWalking", true);
            if (_thirdPersonController.isSprinting)
            {
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isWalking", false);
            }
            else
            {
                _animator.SetBool("isWalking", true);
                _animator.SetBool("isRunning", false);
            }
        }
        else
        {
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isWalking", false);
        }
    }
}
