using UnityEngine;

public class UI_InputManager : MonoBehaviour
{
    public InputSystem_Actions.UIActions UI_InputActions
    { get; private set; }

    private void Awake()
    {
        UI_InputActions = InputManager.Instance.InputSystemActions.UI;

        SetUpInputListeners(UI_InputActions);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpInputListeners(InputSystem_Actions.UIActions uiActions)
    {
        
    }
}
