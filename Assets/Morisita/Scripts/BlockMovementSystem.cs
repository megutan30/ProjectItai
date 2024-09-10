using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockMovementSystem : MonoBehaviour
{
    public GameObject blockPrefab;
    public int columnCount = 2;
    public int rowCount = 5;
    public float blockSpacing = 1f;
    public float columnOffset = 0.5f;

    private List<GameObject> leftBlocks = new List<GameObject>();
    private List<GameObject> rightBlocks = new List<GameObject>();
    public List<TextMeshProUGUI> keyDisplays { get; private set; } = new List<TextMeshProUGUI>();

    private bool isFlipped = false;

    void Start()
    {
        InitializeBlocks();
    }

    void InitializeBlocks()
    {
        float centerX = (columnCount - 1) * blockSpacing * 0.5f;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                Vector3 position = CalculateBlockPosition(i, j);
                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity, transform);

                if (position.x < centerX)
                    leftBlocks.Add(block);
                else
                    rightBlocks.Add(block);

                TextMeshProUGUI textMesh = block.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null)
                {
                    keyDisplays.Add(textMesh);
                }
            }
        }
    }

    Vector3 CalculateBlockPosition(int row, int column)
    {
        float xPos = column * blockSpacing;
        float yPos = row * blockSpacing;

        if (column % 2 == 1)
        {
            yPos += columnOffset;
        }

        return transform.position + new Vector3(xPos, yPos, 0);
    }

    public void FlipBlocks()
    {
        isFlipped = !isFlipped;

        for (int i = 0; i < leftBlocks.Count; i++)
        {
            Vector3 leftPos = leftBlocks[i].transform.position;
            Vector3 rightPos = rightBlocks[i].transform.position;

            leftBlocks[i].transform.position = new Vector3(rightPos.x, leftPos.y, leftPos.z);
            rightBlocks[i].transform.position = new Vector3(leftPos.x, rightPos.y, rightPos.z);
        }
    }

    public void UpdateKeyDisplays(List<KeyCode> keyCodes)
    {
        List<GameObject> allBlocks = new List<GameObject>(leftBlocks);
        allBlocks.AddRange(rightBlocks);

        for (int i = 0; i < allBlocks.Count; i++)
        {
            TextMeshProUGUI textMesh = allBlocks[i].GetComponentInChildren<TextMeshProUGUI>();
            if (i < keyCodes.Count)
            {
                string keyString = keyCodes[i].ToString();
                if (keyString.StartsWith("Alpha"))
                {
                    textMesh.text = keyString.Substring(5);
                }
                else
                {
                    textMesh.text = keyString;
                }
            }
            else
            {
                textMesh.text = "";
            }
        }
    }
}