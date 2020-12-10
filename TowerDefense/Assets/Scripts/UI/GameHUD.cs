using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private Text wavesText;
    [SerializeField] private Text timeLeft;
    [SerializeField] private LevelData levelData;
    [SerializeField] private Button startNowButton;
    [SerializeField] private GameEvent waveStartedEvent;
    [SerializeField] private GameEvent waveEndedEvent;

    private Coroutine timerCoroutine;
    private int currentWave = -1;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(1);

    private void Start()
    {
        startNowButton.onClick.AddListener(waveStartedEvent.Trigger);

        StartCoroutine(Timer(levelData.InitialInterval));

        waveStartedEvent.Listen(OnWaveStarted);
        waveEndedEvent.Listen(OnWaveEnded);
    }

    private void OnDestroy()
    {
        waveStartedEvent.Unlisten(OnWaveStarted);
        waveEndedEvent.Unlisten(OnWaveEnded);
    }

    private void OnWaveStarted()
    {
        startNowButton.gameObject.SetActive(false);
        timeLeft.gameObject.SetActive(false);

        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
    }

    private void OnWaveEnded()
    {
        startNowButton.gameObject.SetActive(true);
        timeLeft.gameObject.SetActive(true);
        timerCoroutine = StartCoroutine(Timer(levelData.IntervalBetweenWaves));
    }

    private IEnumerator Timer(int seconds)
    {
        for (var i = 0; i < seconds; i++)
        {
            timeLeft.text = $"{seconds - i}s left";
            yield return waitOneSecond;
        }
    }

    private void Update()
    {
        if (levelData.CurrentWave != currentWave)
        {
            currentWave = levelData.CurrentWave;
            wavesText.text = $"Wave {currentWave + 1}";
        }
    }
}
