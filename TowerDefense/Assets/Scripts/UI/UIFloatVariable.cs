using UnityEngine;
using UnityEngine.UI;

public class UIFloatVariable : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private FloatVariable floatVariable;
    [SerializeField] private string prefix;

    private float value;

    private void Update()
    {
        if (value != floatVariable.Value)
        {
            value = floatVariable.Value;
            uiText.text = $"{prefix}{value}";
        }
    }
}
