using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;
    public PlayerActions playerActions;

    public float moveSpeed, walkSpeed, runSpeed;

    public Vector2 moveInput;

    public bool isRunning, isJumping;
    private void OnEnable()
    {
        playerActions = new PlayerActions();
        playerActions.ActionMap.Keyboard.performed += OnMove;
        playerActions.ActionMap.Keyboard.canceled += OnMove;
        playerActions.ActionMap.Sprint.performed += Sprint_performed;
        playerActions.ActionMap.Sprint.canceled += Sprint_canceled;
        playerActions.Enable();
    }

    private void Sprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(obj.performed)
        {
            isRunning = true;
        }
    }
    
    private void Sprint_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isRunning = false;
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveInput = obj.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveSpeed = isRunning ? runSpeed : walkSpeed;
        animator.SetFloat("Speed", moveInput.magnitude <= 0 ? 0f : (isRunning ? 0.75f : 0.25f));



        //Move Character
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);

        //Concatenation
        moveDir = transform.TransformDirection(moveDir) * moveSpeed;

        characterController.Move(moveDir * Time.deltaTime);

    }
}
