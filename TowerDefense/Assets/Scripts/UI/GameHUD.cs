using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private Text wavesText;
    [SerializeField] private LevelData levelData;

    private int currentWave = -1;

    private void Update()
    {
        if (levelData.CurrentWave != currentWave)
        {
            currentWave = levelData.CurrentWave;
            wavesText.text = $"Wave {currentWave + 1}";
        }
    }
}
