using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

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

    [field: SerializeField]
    public TMP_InputField TimeToClearPassCodeInput
    { get; private set; }

    public float TimeToClearPassCode
    { get; private set; } = 2.0f;

    /// <summary>
    /// The button for generating the passcode.
    /// </summary>
    [field: SerializeField]
    public Button SubmitButton
    { get; private set; }

    public CodeGenerator CodeGeneratorScript
    { get; private set; }

    public event Action GenerateButtonPress;

    public Coroutine ClearPassCodeRoutine
    { get; private set; }

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
        if (TimeToClearPassCodeInput != null)
        {
            TimeToClearPassCodeInput.text = TimeToClearPassCode.ToString();
        }
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
        SubmitButton.onClick.AddListener(() => GenerateButtonPress?.Invoke());

        SeedInput.onEndEdit.AddListener((value) => {
            int variableTemp = CodeGeneratorScript.SeedEntry;
            if (ValidateStringToNumber(value, out int parsedInt))
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
            if (ValidateStringToNumber(value, out int parsedInt))
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
            if (ValidateStringToNumber(value, out int parsedInt))
            {
                CodeGeneratorScript.UpdateValue<int>(parsedInt, ref variableTemp);
                CodeGeneratorScript.NumbersRequired = variableTemp;
            }
            else
            {
                Debug.LogError("Unable to convert text string to an string");
                NumbersInput.text = string.Empty;
            }
        });

        SymbolsInput.onEndEdit.AddListener((value) => {
            int variableTemp = CodeGeneratorScript.SymbolsRequired;
            if (ValidateStringToNumber(value, out int parsedInt))
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
            if (ValidateStringToNumber(value, out int parsedInt))
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

        TimeToClearPassCodeInput.onEndEdit.AddListener((value) => {
            float variableTemp = TimeToClearPassCode;
            if (ValidateStringToNumber(value, out float parsedInt))
            {
                CodeGeneratorScript.UpdateValue<float>(parsedInt, ref variableTemp);
                TimeToClearPassCode = variableTemp;
            }
            else
            {
                Debug.LogError("Unable to convert text string to an float");
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
    public bool ValidateStringToNumber(string entryText, out int result)
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

    public bool ValidateStringToNumber(string entryText, out float result)
    {
        if (float.TryParse(entryText, out result))
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

        // If a coroutine is currently running, stop it and clear reference to it.
        if (ClearPassCodeRoutine != null)
        {
            StopCoroutine(ClearPassCodeRoutine);
            ClearPassCodeRoutine = null;
        }

        // Run the fresh coroutine for the current output of text to be cleared.
        ClearPassCodeRoutine = StartCoroutine(ClearPasscode(TimeToClearPassCode));
    }

    public IEnumerator ClearPasscode(float timeUntilClear)
    {
        yield return new WaitForSeconds(timeUntilClear);

        TextOutput.text = string.Empty;
    }
}
