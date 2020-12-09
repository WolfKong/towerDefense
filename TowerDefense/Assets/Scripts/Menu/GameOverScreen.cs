using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text wavesLeftText;
    [SerializeField] private LevelData levelData;
    [SerializeField] private Button retryButton;

    void Start()
    {
        wavesLeftText.text = $"{levelData.TotalWaves - levelData.CurrentWave} waves left";
        retryButton.onClick.AddListener(RetryLevel);
    }

    private void RetryLevel()
    {
        SceneLoader.LoadScene("Arena");
    }
}
