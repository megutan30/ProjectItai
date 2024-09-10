using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    // 移動速度の設定
    public float speed = 5.0f;
    //キャラクターのオブジェクトを入れる箱を作成  
    private Rigidbody2D playerRigidbody;

    void Start()
    {
        //キャラクターのオブジェクトを情報を取得
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Aキーが押されたら左に、Dキーが押されたら右に移動
        float moveHorizontal = Input.GetAxis("Horizontal");

        // キャラクターに移動する力を与える
        playerRigidbody.velocity = new Vector2(moveHorizontal * speed, playerRigidbody.velocity.y);

    }
}