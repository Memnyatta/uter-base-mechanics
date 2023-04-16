using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController _controller;
    [SerializeField]
    private Transform cam;

    public float speed;
    public float sprintspeed;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Vector3 velocity;
    public float gravity = -9.81f;
    public bool isGrounded;
    public float distanceToGround;
    public LayerMask Ground;
    public float jumpHeigh;
    public bool canWalk { get; set; }
   [HideInInspector]
    public bool isSprinting;
    public bool canSprint;

    [HideInInspector]
    public bool canMove;

    MyNameIsUter controls = null;

    //Лестницы

    [Header("References Climbing")]
    public LayerMask whatIsLadder;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;

    public bool canClimbing = true;

    private float climbTimer;
    public bool climbing = false;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxLadderLookAngle;

    private float ladderLookAngle;
    private RaycastHit frontLadderHit;
    private bool ladderFront;

    #region OBSTACLEPUSH

    [SerializeField]
    private float _pushForce;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if(rb != null && !rb.isKinematic)
        {
            rb.velocity = hit.moveDirection * _pushForce; 
        }
    }

    #endregion

    #region KEPKAS
    public List<GameObject> kepkas = new List<GameObject>();
    public int CurrentKepka;

    public void ChangeKepkaToRight()
    {
        foreach(GameObject go in kepkas)
        {
            go.SetActive(false);
        }
        if (CurrentKepka != kepkas.Count - 1)
        {
            CurrentKepka++;
        }
        else
        {
            CurrentKepka = 0;
        }
        kepkas[CurrentKepka].SetActive(true);
    }

    public void ChangeKepkaToLeft()
    {
        foreach (GameObject go in kepkas)
        {
            go.SetActive(false);
        }
        if (CurrentKepka != 0)
        {
            CurrentKepka--;
        }
        else
        {
            CurrentKepka = kepkas.Count - 1;
        }
        kepkas[CurrentKepka].SetActive(true);
    }

    #endregion
    private void Awake()
    {
        
        canMove = true;

        controls = new MyNameIsUter();
      
        controls.Player.Enable();
        controls.Player.Sprint.started += Sprint;
        controls.Player.Sprint.canceled += Sprint;

        canWalk = true;
        cam = Camera.main.GetComponent<Transform>();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.Sprint.started -= Sprint;
        controls.Player.Sprint.canceled -= Sprint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, distanceToGround);
    }

    public void SprintEnable()
    {
        speed *= sprintspeed;
        isSprinting = true;
    }

    public void SprintDisable()
    {
        speed /= sprintspeed;
        isSprinting = false;
    }

    #region Climb
    private void StateMachine()
    {
        // State 1 - Climbing
        if (ladderFront && ladderLookAngle < maxLadderLookAngle)
        {
            if (!climbing && climbTimer > 0)
            {
                StartClimbing();
            }

            // timer
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }

        // State 3 - None
        else
        {
            if (climbing) StopClimbing();
        }
    }

    private void LadderCheck()
    {
        //ladderFront = Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out frontLadderHit, detectionLength, whatIsLadder);
        ladderLookAngle = Vector3.Angle(transform.forward, -frontLadderHit.normal);

        if (isGrounded)
        {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimbing()
    {
        

        /// idea - camera fov change
    }

    private void ClimbingMovement()
    {
        if (ladderFront)
        {
            climbing = true;
            velocity = new Vector3(velocity.x, climbSpeed, velocity.z);
        }

        /// idea - sound effect
    }

    private void StopClimbing()
    {
        
        /// idea - particle effect
        /// idea - sound effect
    }
    #endregion

    void Sprint(InputAction.CallbackContext context)
    {
        if (context.started && canSprint)
        {
            SprintEnable();
        }
        if (context.canceled && canSprint && isSprinting)
        {
            SprintDisable();
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0 , vertical).normalized;

        isGrounded = Physics.CheckSphere(transform.position, distanceToGround, Ground);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0f;
        }

        if (controls.Player.Move.IsPressed() && ladderFront)
        {
            LadderCheck();
            StateMachine();
            ClimbingMovement();
        }
        else
        {
            climbing = false;
            StopClimbing();
        }


        if (Mathf.Abs(velocity.y) < 10)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (canMove)
        {
            _controller.Move(velocity * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && isGrounded && canWalk)
        {
            velocity.y = Mathf.Sqrt(jumpHeigh * -2f * gravity);
        }

        if (direction.magnitude >= 0.1f && Time.timeScale != 0 && canWalk)
        {
            ladderFront = Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out frontLadderHit, detectionLength, whatIsLadder);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            if(canMove)
            {
                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                _controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
    }
}
