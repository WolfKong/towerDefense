using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer currentSprite;
    [SerializeField] private Transform currentTransform;

    private int maxHealth;
    private int currentHealth;

    private Dictionary<float, Color> colorForPercentage = new Dictionary<float, Color>
    {
        {0.5f, Color.green},
        {0.25f, Color.yellow},
        {0.0f, Color.red}
    };

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;
        UpdateDisplay();
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        var percentage = (float)currentHealth / maxHealth;

        currentTransform.localScale = new Vector3(percentage, 1, 1);

        foreach (var keyValuePair in colorForPercentage)
        {
            if (percentage >= keyValuePair.Key)
            {
                currentSprite.color = keyValuePair.Value;
                break;
            }
        }
    }
}
