using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UI_InputAndDisplay : MonoBehaviour
{
    /// <summary>
    /// The seed for the random number generator to control the randomness.
    /// </summary>
    [field: SerializeField]
    public TMP_InputField SeedInput
    {  get; private set; }

    /// <summary>
    /// The text to be used for the base of being randomised input display text field.
    /// </summary>
    [field: SerializeField]
    public TMP_InputField TextInput
    { get; private set; }

    /// <summary>
    /// The output of the randomised generated passcode input display text field.
    /// </summary>
    [field: SerializeField]
    public TextMeshProUGUI TextOutput
    { get; private set; }

    /// <summary>
    /// The number of capitals required in the generated text input display text field.
    /// </summary>
    [field: SerializeField]
    public TMP_InputField CapitalsInput
    { get; private set; }

    /// <summary>
    /// The number of numbers required in the generated input display text field.
    /// </summary>
    [field: SerializeField]
    public TMP_InputField NumbersInput
    { get; private set; }

    /// <summary>
    /// The number of symbols required in the generated text input display text field.
    /// </summary>
    [field: SerializeField]
    public TMP_InputField SymbolsInput
    { get; private set; }

    /// <summary>
    /// The required length of the generated passcode display text field.
    /// </summary>
    [field: SerializeField]
    public TMP_InputField LengthInput
    { get; private set; }

    /// <summary>
    /// The button for generating the passcode.
    /// </summary>
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

    /// <summary>
    /// Set up the listeners for the display elements.
    /// </summary>
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

    /// <summary>
    /// Find reference to other components and script in the scene required for the this.
    /// </summary>
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

    /// <summary>
    /// Validate whether a string can be processed into an integer.
    /// </summary>
    /// <param name="entryText"></param>
    /// <param name="result"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Update the displau for the generated code.
    /// </summary>
    /// <param name="display"></param>
    public void UpdateCodeDisplay(string display)
    {
        TextOutput.text = display;
    }
}
