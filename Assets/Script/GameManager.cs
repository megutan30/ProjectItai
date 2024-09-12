using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameOverTime = 3f;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private BlockClimbInputGetKey inputGetKey;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private BlockClimbCount BlockClimbCount;
    [SerializeField] private GameObject clearUI;
    [SerializeField] private TextMeshProUGUI clearTimeText;
    [SerializeField] private BackGroundMovement backGroundMovement;

    private float timer = 0f;
    private float gameTime = 0f;

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (GameDirector.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) Restart();
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartPanel.SetActive(false);
        }

        if (!GameDirector.hasStarted) return;

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
    }

    private void UpdateTimeDisplay()
    {
        if (timeText != null)
        {
            timeText.text = $"Time: {gameTime:F2}";
        }
    }

    private void GameOver()
    {
        GameDirector.GameOver = true;
        if (gameOver != null)
        {
            gameOver.gameObject.SetActive(true);
        }
        inputGetKey.SetGameOverState(true);
        SoundManager.Instance.PlaySFX(SoundManager.SoundType.GameOver);
    }

    private void GameClear()
    {
        GameDirector.GameOver = true;
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
        InitializeGame();
        inputGetKey.ResetKeys();
        backGroundMovement.ResetPosition();
        BlockClimbCount.climbCount = 0;  // クライムカウントをリセット
    }

    private void InitializeGame()
    {
        //StartPanel.SetActive(false);
        GameDirector.GameOver = false;
        GameDirector.hasStarted = false;
        GameDirector.GameClear = false;
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
        timer = 0f;
        UpdateTimeDisplay();
    }

    public bool IsGameOver()
    {
        return GameDirector.GameOver;
    }
}