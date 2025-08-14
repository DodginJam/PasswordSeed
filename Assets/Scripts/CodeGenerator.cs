using System;
using System.Collections.Generic;
using UnityEngine;

public class CodeGenerator : MonoBehaviour
{
    private int seedEntry;
    public int SeedEntry
    {
        get { return seedEntry; }
        set
        {
            seedEntry = value;
        }
    }

    private string textEntry;
    public string TextEntry
    {
        get { return textEntry; }
        set
        {
            textEntry = value;
        }
    }

    private string textOutput;
    public string TextOutput
    {
        get { return textOutput; }
        set
        {
            textOutput = value;
        }
    }

    private int codeLength;
    public int CodeLength
    {
        get { return codeLength; }
        set
        {
            codeLength = value;
        }
    }

    private int capitalsRequired;
    public int CapitalsRequired
    {
        get { return capitalsRequired; }
        set
        {
            capitalsRequired = value;
        }
    }

    private int numbersRequired;
    public int NumbersRequired
    {
        get { return numbersRequired; }
        set
        {
            numbersRequired = value;
        }
    }

    private int symbolsRequired;
    public int SymbolsRequired
    {
        get { return symbolsRequired; }
        set
        {
            symbolsRequired = value;
        }
    }

    public UI_InputAndDisplay InputAndOutputDisplay
    { get; set; }

    public event Action<string> DisplayCodeOutput;

    public static char[] AllowedCharactersAll = new char[]
    {
    // Digits
    '0','1','2','3','4','5','6','7','8','9',

    // Uppercase letters
    'A','B','C','D','E','F','G','H','I','J','K','L','M',
    'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',

    // Lowercase letters
    'a','b','c','d','e','f','g','h','i','j','k','l','m',
    'n','o','p','q','r','s','t','u','v','w','x','y','z',

    // Common symbols (safe across most platforms)
    '!','@','#','$','%','&','*','(',')',
    '-','_','=','+','[',']','{','}',';',':',
    ',','.','<','>','?'
    };

    public static char[] AllowedLetters = new char[]
    {
    // Lowercase letters
    'a','b','c','d','e','f','g','h','i','j','k','l','m',
    'n','o','p','q','r','s','t','u','v','w','x','y','z',
    };

    public static char[] AllowedNumbers = new char[]
    {
    // Digits
    '0','1','2','3','4','5','6','7','8','9',
    };

    public static char[] AllowedSymbols = new char[]
    {
    // Common symbols (safe across most platforms)
    '!','@','#','$','%','&','*','(',')',
    '-','_','=','+','[',']','{','}',';',':',
    ',','.','<','>','?'
    };

    private void Awake()
    {
        GrabObjectReferences();
    }

    private void OnEnable()
    {
        if (InputAndOutputDisplay != null)
        {
            InputAndOutputDisplay.GenerateButtonPress += GeneratePassword;
        }
    }

    private void OnDisable()
    {
        if (InputAndOutputDisplay != null)
        {
            InputAndOutputDisplay.GenerateButtonPress -= GeneratePassword;
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

    public void GeneratePassword()
    {
        TextOutput = GenerateValidPass(SeedEntry, TextEntry, CodeLength, CapitalsRequired, NumbersRequired, SymbolsRequired);

        if (!String.IsNullOrWhiteSpace(TextOutput))
        {
            DisplayCodeOutput?.Invoke(TextOutput);
        }
        else
        {
            DisplayError();
        }
    }

    public void DisplayError()
    {
        Debug.Log($"No valid text generated");
    }

    public string GenerateValidPass(int seed, string textEntry, int codeLength, int capitalsRequired, int numbersRequired, int symbolsRequired)
    {
        if (codeLength < capitalsRequired + numbersRequired + symbolsRequired)
        {
            Debug.LogError("Error - passcode not long enough to be complient with requirements");
            return "Error - passcode not long enough to be complient with requirements.";
        }

        char[] generatedText = new char[codeLength];

        System.Random seededRandomGenerator = new System.Random(seed);

        // Generate plain characters first.
        int textIndex = 0;

        for (int codeIndex = 0; codeIndex < codeLength; codeIndex++)
        {
            int indexOfTextCharacterInAllowed = Array.IndexOf(AllowedLetters, textEntry[textIndex]);

            System.Random seededRandomForIndex = new System.Random(seed + seed.ToString().Length + seededRandomGenerator.Next() + indexOfTextCharacterInAllowed + codeIndex + codeLength + textEntry.Length);

            int newIndex = seededRandomForIndex.Next(0, AllowedLetters.Length);
            char newChar = AllowedLetters[newIndex];

            generatedText[codeIndex] = newChar;

            if (textIndex < textEntry.Length - 1)
            {
                textIndex++;
            }
            else
            {
                textIndex = 0;
            }
        }

        ApplyCapitals(generatedText, capitalsRequired, seededRandomGenerator, out List<int> indexesAllowForChange);
        ApplyNumbers(generatedText, numbersRequired, seededRandomGenerator, indexesAllowForChange);
        ApplySymbols(generatedText, symbolsRequired, seededRandomGenerator, indexesAllowForChange, AllowedSymbols);

        return new string(generatedText);
    }

    public void ApplyCapitals(char[] generatedText, int capitalsRequired, System.Random seededRandomGenerator, out List<int> indexesAllowForChange)
    {
        // Keep track of the avilable to be modified indexes of the generated text to keep track what can be changed.
        indexesAllowForChange = new List<int>();

        for (int i = 0; i < generatedText.Length; i++)
        {
            indexesAllowForChange.Add(i);
        }

        // Ensure that capitals are added to the code and keep track of what has been changed.
        if (capitalsRequired > 0)
        {
            for (int currentCapitalToMake = 0; currentCapitalToMake < capitalsRequired; currentCapitalToMake++)
            {
                int indexFromAllowedIndexList = seededRandomGenerator.Next(0, indexesAllowForChange.Count);
                int chosenIndexFromGeneratedText = indexesAllowForChange[indexFromAllowedIndexList];

                indexesAllowForChange.RemoveAt(indexFromAllowedIndexList);

                generatedText[chosenIndexFromGeneratedText] = Char.ToUpper(generatedText[chosenIndexFromGeneratedText]);
            }
        }
    }

    public void ApplyNumbers(char[] generatedText, int numbersRequired, System.Random seededRandomGenerator, List<int> indexesAllowForChange)
    {
        if (numbersRequired > 0)
        {
            for (int currentNumbersToMake = 0; currentNumbersToMake < numbersRequired; currentNumbersToMake++)
            {
                int indexFromAllowedIndexList = seededRandomGenerator.Next(0, indexesAllowForChange.Count);
                int chosenIndexFromGeneratedText = indexesAllowForChange[indexFromAllowedIndexList];

                indexesAllowForChange.RemoveAt(indexFromAllowedIndexList);

                generatedText[chosenIndexFromGeneratedText] = (char)('0' + seededRandomGenerator.Next(0, 10));
            }
        }
    }

    public void ApplySymbols(char[] generatedText, int symbolsRequired, System.Random seededRandomGenerator, List<int> indexesAllowForChange, char[] symbolsAllowed)
    {
        if (symbolsRequired > 0)
        {
            for (int currentSymbolsToMake = 0; currentSymbolsToMake < symbolsRequired; currentSymbolsToMake++)
            {
                int indexFromAllowedIndexList = seededRandomGenerator.Next(0, indexesAllowForChange.Count);
                int chosenIndexFromGeneratedText = indexesAllowForChange[indexFromAllowedIndexList];

                indexesAllowForChange.RemoveAt(indexFromAllowedIndexList);

                generatedText[chosenIndexFromGeneratedText] = symbolsAllowed[seededRandomGenerator.Next(0, symbolsAllowed.Length)];
            }
        }
    }

    public void GrabObjectReferences()
    {
        if (InputAndOutputDisplay == null)
        {
            UI_InputAndDisplay uiDisplayScript = GameObject.FindAnyObjectByType<UI_InputAndDisplay>();
            if (uiDisplayScript != null)
            {
                InputAndOutputDisplay = uiDisplayScript;
            }
            else
            {
                Debug.LogError("Unable to locate Code Generator Script in scene.");
            }
        }
    }

    public void UpdateValue<T>(T inbound, ref T toUpdate)
    {
        toUpdate = inbound;
    }
}
