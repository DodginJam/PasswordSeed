using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Display : MonoBehaviour
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
    public Button SumbitButton
    { get; private set; }

    private void Awake()
    {
        SetUpUIListeners();
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

    }
}
