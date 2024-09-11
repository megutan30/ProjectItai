using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputGetKey : MonoBehaviour
{
    [SerializeField]
    List<KeyCode> rnKeyCodes = new List<KeyCode>{ KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
                              KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
                              KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M,
                              KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9};
    [SerializeField] List<KeyCode> rmdKeyCodes = new List<KeyCode>();
    [SerializeField] private int maxKeys = 4;
    const int kaburiNum = 10;
    public List<KeyCode> inputKeys = new List<KeyCode>();
    public List<bool> isKeyDown = new List<bool>();
    public bool isAllKeyDown = true;

    [SerializeField] private BlockMovementSystem blockSystem;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        InitKeySet();
        UpdateKeyDisplays();
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
                pressedKeyIndices.Add(rmdKeyCodes.IndexOf(inputKeys[i]));
            }
            else
            {
                isAllKeyDown = false;
            }
        }
        blockSystem.UpdateKeyColors(pressedKeyIndices);
        if (isAllKeyDown && inputKeys.Count > 0)
        {
            KeyChange();
            UpdateKeyDisplays();
            blockSystem.FlipBlocks();
            if(!GameDirector.hasStarted)GameDirector.hasStarted = true;
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

    public void InitKeySet()
    {
        rmdKeyCodes.Clear();
        inputKeys.Clear();
        for (int i = 0; i < kaburiNum; i++)
        {
            int rnd = Random.Range(0, rnKeyCodes.Count);
            KeyCode key = rnKeyCodes[rnd];
            rnKeyCodes.RemoveAt(rnd);
            if (i < maxKeys)
                inputKeys.Add(key);
            rmdKeyCodes.Add(key);
        }
        BoolReset();
    }

    public void KeyChange()
    {
        if (inputKeys.Count > 0)
        {
            inputKeys.RemoveAt(0);
        }
        if (rmdKeyCodes.Count > 0)
        {
            int rnd = Random.Range(0, rnKeyCodes.Count);
            KeyCode key = rnKeyCodes[rnd];
            rnKeyCodes.RemoveAt(rnd);
            rnKeyCodes.Add(rmdKeyCodes[0]);
            rmdKeyCodes.RemoveAt(0);
            rmdKeyCodes.Add(key);
            if (inputKeys.Count < maxKeys)
            {
                inputKeys.Add(rmdKeyCodes[maxKeys-1]);
            }
        }
        BoolReset();
    }

    private void UpdateKeyDisplays()
    {
        for (int i = 0; i < blockSystem.keyDisplays.Count; i++)
        {
            if (i < rmdKeyCodes.Count)
            {
                string keyString = rmdKeyCodes[i].ToString();
                if (keyString.StartsWith("Alpha"))
                {
                    blockSystem.keyDisplays[i].text = keyString.Substring(5);
                }
                else
                {
                    blockSystem.keyDisplays[i].text = keyString;
                }
            }
            else
            {
                blockSystem.keyDisplays[i].text = "";
            }
        }
    }
    public int GetPressedKeyCount()
    {
        return isKeyDown.FindAll(x => x).Count;
    }

    public void SetGameOverState(bool state)
    {
        GameDirector.GameOver = state;
    }
}