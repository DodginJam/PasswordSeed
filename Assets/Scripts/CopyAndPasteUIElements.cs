using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CopyAndPasteUIElements : MonoBehaviour
{
    public UI_InputManager UI_InputManagerScript
    {  get; private set; }

    public static string ClipBoard
    {
        get
        {
            return GUIUtility.systemCopyBuffer;
        }
        set
        {
            GUIUtility.systemCopyBuffer = value;
        }
    }

    private void Awake()
    {
        if (TryGetComponent<UI_InputManager>(out UI_InputManager ui_InputManagerScript))
        {
            UI_InputManagerScript = ui_InputManagerScript;
        }
        else
        {
            Debug.LogError("Unable to locate the input");
        }
    }

    private void OnEnable()
    {
        if (UI_InputManagerScript != null)
        {
            UI_InputManagerScript.OnClickInput += GrabTextFromClick;
        }
    }

    private void OnDisable()
    {
        if (UI_InputManagerScript != null)
        {
            UI_InputManagerScript.OnClickInput -= GrabTextFromClick;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabTextFromClick()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Unable to locate main camera");
            return;
        }

        GraphicRaycaster graphicsRaycaster = GameObject.FindAnyObjectByType<GraphicRaycaster>();
        PointerEventData pointerEventdata = new PointerEventData(EventSystem.current);
        pointerEventdata.position = UI_InputManagerScript.MousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        graphicsRaycaster.Raycast(pointerEventdata, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Copy"))
            {
                if (result.gameObject.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI textDisplay))
                {
                    ClipBoard = textDisplay.text;
                    Debug.Log(textDisplay.text);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
