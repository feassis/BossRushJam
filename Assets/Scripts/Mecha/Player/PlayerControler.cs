using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private MechaAssembler assembler;
    [SerializeField] private Rigidbody rigidbody;

    private MechaLegPart leg;
    private MechaArmPart leftArm;
    private MechaArmPart rightArm;
    private MechaBodyPart body;

    private PlayerMechaControls mechaControls;
    private InputAction moveAction;
    private InputAction dashAction;

    private void Awake()
    {
        mechaControls = new PlayerMechaControls();
        mechaControls.Enable();

        moveAction = mechaControls.FindAction("Move");

        moveAction.performed += OnMoveAction;
        moveAction.canceled += OnMoveAction;

        dashAction = mechaControls.FindAction("Dash");

        dashAction.performed += OnDashAction;
    }

    private void OnDashAction(InputAction.CallbackContext context)
    {
        leg.OnDashAction();
    }

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        var moveDirection = context.ReadValue<Vector2>().normalized;
        
        leg.OnMoveAction(new Vector3(moveDirection.x, 0, moveDirection.y));
    }


    private void OnDisable()
    {
        mechaControls.Disable();
    }

    private void OnEnable()
    {
        mechaControls.Enable();
    }


    void Start()
    {
        leg = assembler.LegPart;
        leg.Setup(rigidbody);
        CameraControler.Instance.GoToFollowPlayerMode(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
