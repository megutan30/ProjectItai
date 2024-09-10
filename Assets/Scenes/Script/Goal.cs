using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public Image imageToShow; // ゴール時に表示するイメージ
    public Text finalTimeText; // 最終時間を表示するテキスト
    public Timer timer; // タイマーの参照

    void Start()
    {
        imageToShow.gameObject.SetActive(false); // 初期状態で非表示
        finalTimeText.gameObject.SetActive(false); // タイマーも非表示
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // ゴール時にイメージを表示
            imageToShow.gameObject.SetActive(true);

            // タイマーを停止
            timer.StopTimer();

            // 最終タイムを取得して表示
            string finalTime = timer.GetFinalTime();
            finalTimeText.text = "Time: " + finalTime;
            finalTimeText.gameObject.SetActive(true);

            Debug.Log("GameClear");

            // ゲームの動きを止める
            Time.timeScale = 0f;
        }
    }
}
