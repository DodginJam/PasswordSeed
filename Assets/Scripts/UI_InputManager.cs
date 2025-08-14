using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_InputManager : MonoBehaviour
{
    public InputSystem_Actions.UIActions UI_InputActions
    { get; private set; }

    private void Awake()
    {
        UI_InputActions = InputManager.Instance.InputSystemActions.UI;

        SetUpInputListeners(UI_InputActions);

        Debug.Log("Actions assigend and listeners init");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        UI_InputActions.Enable();
    }

    private void OnDisable()
    {
        UI_InputActions.Disable();
    }

    public void SetUpInputListeners(InputSystem_Actions.UIActions uiActions)
    {
        uiActions.Click.performed += context =>
        {
            OnClick(context);
        };
    }

    void OnClick(InputAction.CallbackContext context)
    {

    }
}
