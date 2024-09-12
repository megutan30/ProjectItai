using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BlockClimbInputGetKey : MonoBehaviour
{
    [SerializeField]
    List<KeyCode> rnKeyCodes = new List<KeyCode>{ KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
                              KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
                              KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M,
                              KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9};
    public List<KeyCode> rmdKeyCodes = new List<KeyCode>();
    [SerializeField] private int maxKeys = 4;
    const int kaburiNum = 10;
    public List<KeyCode> inputKeys = new List<KeyCode>();
    public List<bool> isKeyDown = new List<bool>();
    public bool isAllKeyDown = true;

    [SerializeField] private BlockMovementSystem blockSystem;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BlockClimbCount blockCount;
    [SerializeField] private Animator characterAnimator;

    private bool useRightArm = true;
    private int currentKeyIndex = 0;

    void Start()
    {
        if (rmdKeyCodes.Count == 0)
        {
            InitializeRmdKeyCodes();
        }
        InitKeySet();
        UpdateKeyDisplays();
    }

    void InitializeRmdKeyCodes()
    {
        List<KeyCode> tempKeys = new List<KeyCode>(rnKeyCodes);
        for (int i = 0; i < kaburiNum; i++)
        {
            if (tempKeys.Count > 0)
            {
                int index = Random.Range(0, tempKeys.Count);
                rmdKeyCodes.Add(tempKeys[index]);
                tempKeys.RemoveAt(index);
            }
        }
    }

    void Update()
    {
        if (GameDirector.GameOver) return;
        BoolReset();
        isAllKeyDown = true;
        List<int> pressedKeyIndices = new List<int>();

        for (int i = 0; i < inputKeys.Count; i++)
        {
            if (Input.GetKey(inputKeys[i]))
            {
                isKeyDown[i] = true;
                pressedKeyIndices.Add(i);
            }
            else
            {
                isAllKeyDown = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isAllKeyDown = true;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isAllKeyDown = true;
        }
        blockSystem.UpdateKeyColors(pressedKeyIndices);
        if (isAllKeyDown && inputKeys.Count > 0)
        {
            KeyChange();
            UpdateKeyDisplays();
            blockSystem.FlipBlocks();
            if (!GameDirector.hasStarted) GameDirector.hasStarted = true;
            blockCount.AddClimbCount();
            TriggerArmAnimation();
        }
    }

    public void ResetKeys()
    {
        currentKeyIndex = 0;
        InitKeySet();
        UpdateKeyDisplays();
    }

    public void InitKeySet()
    {
        inputKeys.Clear();
        for (int i = 0; i < maxKeys; i++)
        {
            inputKeys.Add(rmdKeyCodes[(currentKeyIndex + i) % rmdKeyCodes.Count]);
        }
        BoolReset();
    }

    public void KeyChange()
    {
        currentKeyIndex = (currentKeyIndex + 1) % rmdKeyCodes.Count;
        inputKeys.RemoveAt(0);
        inputKeys.Add(rmdKeyCodes[(currentKeyIndex + maxKeys - 1) % rmdKeyCodes.Count]);
        BoolReset();
    }

    private void UpdateKeyDisplays()
    {
        for (int i = 0; i < blockSystem.keyDisplays.Count; i++)
        {
            if (i < kaburiNum)
            {
                int keyIndex = (currentKeyIndex + i) % rmdKeyCodes.Count;
                string keyString = rmdKeyCodes[keyIndex].ToString();
                if (keyString.StartsWith("Alpha"))
                {
                    blockSystem.keyDisplays[i].text = keyString.Substring(5);
                }
                else
                {
                    blockSystem.keyDisplays[i].text = keyString;
                }

                // 入力が必要なキーを強調表示（オプション）
                if (i < maxKeys)
                {
                    blockSystem.keyDisplays[i].color = Color.yellow; // または他の目立つ色
                }
                else
                {
                    blockSystem.keyDisplays[i].color = Color.white;
                }
            }
            else
            {
                blockSystem.keyDisplays[i].text = "";
            }
        }
    }

    private void TriggerArmAnimation()
    {
        if (characterAnimator != null)
        {
            if (useRightArm)
            {
                characterAnimator.SetTrigger("RightArm");
                SoundManager.Instance.PlaySFX(SoundManager.SoundType.grasp);
            }
            else
            {
                characterAnimator.SetTrigger("LeftArm");
                SoundManager.Instance.PlaySFX(SoundManager.SoundType.grasp);
            }
            useRightArm = !useRightArm;
        }
    }

    public void BoolReset()
    {
        isKeyDown.Clear();
        for (int i = 0; i < inputKeys.Count; i++)
        {
            isKeyDown.Add(false);
        }
        isAllKeyDown = false;
    }

    public int GetPressedKeyCount()
    {
        return isKeyDown.FindAll(x => x).Count;
    }

    public void SetGameOverState(bool state)
    {
        GameDirector.GameOver = state;
    }

    private void ResetInputKeys()
    {
        inputKeys.Clear();
        for (int i = 0; i < Mathf.Min(maxKeys, rmdKeyCodes.Count); i++)
        {
            inputKeys.Add(rmdKeyCodes[i]);
        }
        BoolReset();
    }
}