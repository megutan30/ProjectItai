using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameOverTime = 3f;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private BlockClimbInputGetKey inputGetKey;
    [SerializeField] private TextMeshProUGUI timeText; // 時間表示用のテキスト
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private BlockClimbCount BlockClimbCount;
    [SerializeField] private GameObject clearUI; // クリア時に表示する UI
    [SerializeField] private TextMeshProUGUI clearTimeText; // クリア時間表示用のテキスト

    private float timer = 0f;
    private float gameTime = 0f; // ゲーム時間を追跡

    private void Start()
    {
        if (gameOver != null)
        {
            gameOver.gameObject.SetActive(false);
        }
        if (clearUI != null)
        {
            clearUI.SetActive(false);
        }
        UpdateTimeDisplay();
    }

    private void Update()
    {
        if (GameDirector.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) Restart();
        }
        if (GameDirector.GameOver || !GameDirector.hasStarted) return;

        // ゲーム時間を更新
        gameTime += Time.deltaTime;
        UpdateTimeDisplay();

        int pressedKeyCount = inputGetKey.GetPressedKeyCount();

        if (pressedKeyCount <= 2)
        {
            timer += Time.deltaTime;
            if (timer >= gameOverTime)
            {
                GameOver();
            }
        }
        else
        {
            timer = 0f;
        }

        if (BlockClimbCount.GetGoalHeight() <= BlockClimbCount.climbCount)
        {
            GameClear();
        }

        // ここにゲームクリアの条件を追加
        if (GameDirector.GameClear)
        {
            timeText.gameObject.SetActive(true);
            //GameClear();
        }
    }

    private void UpdateTimeDisplay()
    {
        if (timeText != null)
        {
            timeText.text = $"Time: {gameTime:F2}";
        }
    }

    public void ResetTimer()
    {
        timer = 0f;
    }


    private void GameOver()
    {
        GameDirector.GameOver = true;
        if (gameOver != null)
        {
            gameOver.gameObject.SetActive(true);
           // SoundManager.Instance.PlayBGM(SoundManager.SoundType.GameOver);
        }
        inputGetKey.SetGameOverState(true);
        SoundManager.Instance.PlaySFX(SoundManager.SoundType.GameOver);
    }

    private void GameClear()
    {
        GameDirector.GameOver = true; // ゲームを停止
        if (clearUI != null)
        {
            clearUI.SetActive(true);
        }
        if (clearTimeText != null)
        {
            clearTimeText.text = $"Clear Time: {gameTime:F2}";
        }
    }

    private void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartPanel.SetActive(false);
        GameDirector.GameOver = false;
        GameDirector.hasStarted = false;
        gameTime = 0f;
        BlockClimbCount.climbCount = 0;
        if (gameOver != null)
        {
            gameOver.gameObject.SetActive(false);
        }
        if (clearUI != null)
        {
            clearUI.SetActive(false);
        }
        ResetTimer();
        UpdateTimeDisplay();
    }

    public bool IsGameOver()
    {
        return GameDirector.GameOver;
    }
}