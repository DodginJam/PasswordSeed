using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UI_InputAndDisplay : MonoBehaviour
{
    [field: SerializeField]
    public TMP_InputField SeedInput
    {  get; private set; }

    [field: SerializeField]
    public TMP_InputField TextInput
    { get; private set; }

    [field: SerializeField]
    public TextMeshProUGUI TextOutput
    { get; private set; }

    [field: SerializeField]
    public TMP_InputField CapitalsInput
    { get; private set; }

    [field: SerializeField]
    public TMP_InputField NumbersInput
    { get; private set; }

    [field: SerializeField]
    public TMP_InputField SymbolsInput
    { get; private set; }

    [field: SerializeField]
    public TMP_InputField LengthInput
    { get; private set; }

    [field: SerializeField]
    public Button SumbitButton
    { get; private set; }

    public CodeGenerator CodeGeneratorScript
    { get; private set; }

    public event Action GenerateButtonPress;

    private void Awake()
    {
        GrabObjectReferences();

        SetUpUIListeners();
    }

    private void OnEnable()
    {
        if (CodeGeneratorScript != null)
        {
            CodeGeneratorScript.DisplayCodeOutput += UpdateCodeDisplay;
        }
    }

    private void OnDisable()
    {
        if (CodeGeneratorScript != null)
        {
            CodeGeneratorScript.DisplayCodeOutput -= UpdateCodeDisplay;
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

    public void SetUpUIListeners()
    {
        SumbitButton.onClick.AddListener(() => GenerateButtonPress?.Invoke());

        SeedInput.onEndEdit.AddListener((value) => {
            int variableTemp = CodeGeneratorScript.SeedEntry;
            if (ValidateStringToInt(value, out int parsedInt))
            {
                CodeGeneratorScript.UpdateValue<int>(parsedInt, ref variableTemp);
                CodeGeneratorScript.SeedEntry = variableTemp;
            }
            else
            {
                Debug.LogError("Unable to convert text string to an int");
                SeedInput.text = string.Empty;
            }     
        });

        TextInput.onEndEdit.AddListener((value) => {
            string variableTemp = CodeGeneratorScript.TextEntry;
            CodeGeneratorScript.UpdateValue<string>(value, ref variableTemp);
            CodeGeneratorScript.TextEntry = variableTemp;
        });

        CapitalsInput.onEndEdit.AddListener((value) => {
            int variableTemp = CodeGeneratorScript.CapitalsRequired;
            if (ValidateStringToInt(value, out int parsedInt))
            {
                CodeGeneratorScript.UpdateValue<int>(parsedInt, ref variableTemp);
                CodeGeneratorScript.CapitalsRequired = variableTemp;
            }
            else
            {
                Debug.LogError("Unable to convert text string to an int");
                CapitalsInput.text = string.Empty;
            }
        });

        NumbersInput.onEndEdit.AddListener((value) => {
            int variableTemp = CodeGeneratorScript.NumbersRequired;
            if (ValidateStringToInt(value, out int parsedInt))
            {
                CodeGeneratorScript.UpdateValue<int>(parsedInt, ref variableTemp);
                CodeGeneratorScript.NumbersRequired = variableTemp;
            }
            else
            {
                Debug.LogError("Unable to convert text string to an int");
                NumbersInput.text = string.Empty;
            }
        });

        SymbolsInput.onEndEdit.AddListener((value) => {
            int variableTemp = CodeGeneratorScript.SymbolsRequired;
            if (ValidateStringToInt(value, out int parsedInt))
            {
                CodeGeneratorScript.UpdateValue<int>(parsedInt, ref variableTemp);
                CodeGeneratorScript.SymbolsRequired = variableTemp;
            }
            else
            {
                Debug.LogError("Unable to convert text string to an int");
                SymbolsInput.text = string.Empty;
            }
        });

        LengthInput.onEndEdit.AddListener((value) => {
            int variableTemp = CodeGeneratorScript.CodeLength;
            if (ValidateStringToInt(value, out int parsedInt))
            {
                CodeGeneratorScript.UpdateValue<int>(parsedInt, ref variableTemp);
                CodeGeneratorScript.CodeLength = variableTemp;
            }
            else
            {
                Debug.LogError("Unable to convert text string to an int");
                SymbolsInput.text = string.Empty;
            }
        });
    }

    public void GrabObjectReferences()
    {
        if (CodeGeneratorScript == null)
        {
            CodeGenerator codeGenerator = GameObject.FindAnyObjectByType<CodeGenerator>();
            if (codeGenerator != null)
            {
                CodeGeneratorScript = codeGenerator;
            }
            else
            {
                Debug.LogError("Unable to locate Code Generator Script in scene.");
            }
        }
    }

    public bool ValidateStringToInt(string entryText, out int result)
    {
        if (int.TryParse(entryText, out result))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateCodeDisplay(string display)
    {
        TextOutput.text = display;
    }
}
