using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] private Button retryButton;

    void Start()
    {
        retryButton.onClick.AddListener(RetryLevel);
    }

    private void RetryLevel()
    {
        SceneLoader.LoadScene("Arena");
    }
}
