using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    // �ړ����x�̐ݒ�
    public float speed = 5.0f;
    //�L�����N�^�[�̃I�u�W�F�N�g�����锠���쐬  
    private Rigidbody2D playerRigidbody;

    void Start()
    {
        //�L�����N�^�[�̃I�u�W�F�N�g�������擾
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // A�L�[�������ꂽ�獶�ɁAD�L�[�������ꂽ��E�Ɉړ�
        float moveHorizontal = Input.GetAxis("Horizontal");

        // �L�����N�^�[�Ɉړ�����͂�^����
        playerRigidbody.velocity = new Vector2(moveHorizontal * speed, playerRigidbody.velocity.y);

    }
}