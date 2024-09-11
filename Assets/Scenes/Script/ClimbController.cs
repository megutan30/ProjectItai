using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbController : MonoBehaviour
{
    public Animator animator; // キャラクターのAnimator
    public float climbSpeed = 5f; // 登る速度
    private Rigidbody2D rb2D; // キャラクターのRigidbody2D
    private bool isRightArmTurn = true; // 最初は右腕を上げる動作から始める
    private KeyCode currentKey; // 現在のランダムキー
    private List<KeyCode> possibleKeys = new List<KeyCode>() { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F }; // 可能なキー
    private bool isAnimating = false; // アニメーション中かどうかのフラグ

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Rigidbody2Dを取得
        SetRandomKey(); // 最初のランダムキーを設定
    }

    void Update()
    {
        // ランダムに選ばれたキーが押された時、かつアニメーションが再生されていない時のみ動作
        if (Input.GetKeyDown(currentKey) && !isAnimating)
        {
            isAnimating = true; // アニメーション開始
            if (isRightArmTurn)
            {
                // 右腕を上げるアニメーションを再生
                animator.SetTrigger("RightArm");
            }
            else
            {
                // 左腕を上げるアニメーションを再生
                animator.SetTrigger("LeftArm");
            }

            // キャラクターを上に移動
            Climb();

            // アニメーションが終わるのを待つコルーチンを開始
            StartCoroutine(WaitForAnimation());
        }
    }

    // ランダムなキーを設定するメソッド
    void SetRandomKey()
    {
        currentKey = possibleKeys[Random.Range(0, possibleKeys.Count)];
        Debug.Log("次に押すキー: " + currentKey.ToString());
    }

    // キャラクターを上に移動させるメソッド
    void Climb()
    {
        // 上方向に移動
        rb2D.velocity = new Vector2(rb2D.velocity.x, climbSpeed);
    }

    // アニメーションの終了を待つコルーチン
    IEnumerator WaitForAnimation()
    {
        // アニメーションが終了するまで待機
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // アニメーションが終了したら、次のランダムキーを待機する
        isAnimating = false; // アニメーションが終了したのでフラグを解除

        // 交互にアニメーションが切り替わる
        isRightArmTurn = !isRightArmTurn;

        // 次のランダムなキーを設定
        SetRandomKey();
    }
}
