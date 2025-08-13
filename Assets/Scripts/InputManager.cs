using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-500)]
[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance
    { get; private set; }

    public InputSystem_Actions InputSystemActions
    { get; private set; }

    public PlayerInput PlayerInputComponent
    { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InputSystemActions = new InputSystem_Actions();
            InputSystemActions.Enable();
        }
        else
        {
            Destroy(this);
        }

        if (TryGetComponent<PlayerInput>(out PlayerInput playerInputComponent))
        {
            PlayerInputComponent = playerInputComponent;
        }
        else
        {
            Debug.LogError("Unable to locate a player input component");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
