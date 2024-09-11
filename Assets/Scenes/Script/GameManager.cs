using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameOverTime = 3f;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private BlockClimbInputGetKey inputGetKey;
    [SerializeField] private TextMeshProUGUI timeText; // ���ԕ\���p�̃e�L�X�g
    //[SerializeField] private GameObject clearUI; // �N���A���ɕ\������ UI
    //[SerializeField] private TextMeshProUGUI clearTimeText; // �N���A���ԕ\���p�̃e�L�X�g

    private float timer = 0f;
    private float gameTime = 0f; // �Q�[�����Ԃ�ǐ�

    private void Start()
    {
        if (gameOver != null)
        {
            gameOver.gameObject.SetActive(false);
        }
        //if (clearUI != null)
        //{
        //    clearUI.SetActive(false);
        //}
        UpdateTimeDisplay();
    }

    private void Update()
    {
        if (GameDirector.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) Restart();
        }
        if (GameDirector.GameOver || !GameDirector.hasStarted) return;

        // �Q�[�����Ԃ��X�V
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

        // �����ɃQ�[���N���A�̏�����ǉ�
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
        }
        inputGetKey.SetGameOverState(true);
    }

    //private void GameClear()
    //{
    //    GameDirector.GameOver = true; // �Q�[�����~
    //    if (clearUI != null)
    //    {
    //        clearUI.SetActive(true);
    //    }
    //    if (clearTimeText != null)
    //    {
    //        clearTimeText.text = $"Clear Time: {gameTime:F2}";
    //    }
    //}

    private void Restart()
    {
        GameDirector.GameOver = false;
        GameDirector.hasStarted = false;
        gameTime = 0f;
        if (gameOver != null)
        {
            gameOver.gameObject.SetActive(false);
        }
        //if (clearUI != null)
        //{
        //    clearUI.SetActive(false);
        //}
        ResetTimer();
        UpdateTimeDisplay();
    }

    public bool IsGameOver()
    {
        return GameDirector.GameOver;
    }
}