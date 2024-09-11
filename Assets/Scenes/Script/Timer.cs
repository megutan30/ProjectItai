using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int minute;
    private float seconds;
    //　前のUpdateの時の秒数
    private float oldSeconds;
    //　タイマー表示用テキスト
    [SerializeField]private TextMeshProUGUI timerText;
    // タイマーが動作しているかどうか
    private bool isTimerRunning = true;

    void Start()
    {
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;
    }

    void Update()
    {
        if (isTimerRunning||GameDirector.IsGameStart) // タイマーが動作中なら
        {
            seconds += Time.deltaTime;
            if (seconds >= 60f)
            {
                minute++;
                seconds = seconds - 60;
            }
            //　値が変わった時だけテキストUIを更新
            if ((int)seconds != (int)oldSeconds)
            {
                timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            }
            oldSeconds = seconds;
        }
    }

    // タイマーを停止するメソッド
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // 最終的な時間を取得するメソッド
    public string GetFinalTime()
    {
        return minute.ToString("00") + ":" + ((int)seconds).ToString("00");
    }
}
