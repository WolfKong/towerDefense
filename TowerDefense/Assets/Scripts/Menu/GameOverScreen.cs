using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text wavesLeftText;
    [SerializeField] private FloatVariable waves;
    [SerializeField] private Button retryButton;

    void Start()
    {
        wavesLeftText.text = $"{waves.Value} waves left";
        retryButton.onClick.AddListener(RetryLevel);
    }

    private void RetryLevel()
    {
        SceneLoader.LoadScene("Arena");
    }
}
