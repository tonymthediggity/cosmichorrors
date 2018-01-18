using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSystem : MonoBehaviour {

    [SerializeField]
    private Sprite[] solidBlocks;
    [SerializeField]
    private string[] solidNames;

    [SerializeField]
    private Sprite[] backingBlocks;
    [SerializeField]
    private string[] backingNames;

    public Block[] allBlocks;

    private void Awake()
    {
        allBlocks = new Block[solidBlocks.Length + backingBlocks.Length];

        int newBlockID = 0;
        for (int i = 0; i < solidBlocks.Length; i++)
        {
            allBlocks[newBlockID] = new Block(newBlockID, solidNames[i], solidBlocks[i], true);
            newBlockID++;

        }

        for (int i = 0; i < backingBlocks.Length; i++)
        {
            allBlocks[newBlockID] = new Block(newBlockID, backingNames[i], backingBlocks[i], false);
            newBlockID++;

        }
    }


}

public class Block
{
    public int blockId;
    public string blockname;
    public Sprite blockSprite;
    public bool isSolid;

    public Block(int id, string myName, Sprite mySprite, bool amISolid)
    {
        blockId = id;
        blockname = myName;
        blockSprite = mySprite;
        isSolid = amISolid;
    }
}
