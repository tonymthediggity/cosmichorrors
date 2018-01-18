using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour {

    private BlockSystem blockSys;

    private int currentBlockID = 0;
    private Block currentBlock;

    private GameObject blockTemplate;
    private SpriteRenderer currentRend;
    public PhysicsMaterial2D newPhysicsMat;


    private bool buildModeOn = false;
    private bool buildBlocked = false;

    [SerializeField]
    private float blockSizeMod;

    [SerializeField]
    private LayerMask solidNoBuildLayer;
    [SerializeField]
    private LayerMask backingNoBuildLayer;
    [SerializeField]
    private LayerMask allBlocksLayer;

    private GameObject playerObject;

    [SerializeField]
    private float maxBuildDistance;

    private void Awake()
    {
        blockSys = GetComponent<BlockSystem>();

        playerObject = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            buildModeOn = !buildModeOn;

            if (blockTemplate != null)
            {
                Destroy(blockTemplate);
            }

            if (currentBlock == null)
            {
                if (blockSys.allBlocks[currentBlockID] != null)
                {
                    currentBlock = blockSys.allBlocks[currentBlockID];
                }
            }

            if (buildModeOn)
            {
                blockTemplate = new GameObject("CurrentBlockTemplate");
                currentRend = blockTemplate.AddComponent<SpriteRenderer>();
                currentRend.sprite = currentBlock.blockSprite;
            }
        }

        if (buildModeOn && blockTemplate != null)
        {
            float newPosX = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / blockSizeMod) * blockSizeMod;
            float newPosY = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / blockSizeMod) * blockSizeMod;
            blockTemplate.transform.position = new Vector2(newPosX, newPosY);

            RaycastHit2D rayHit;

            if (currentBlock.isSolid == true)
            {
                rayHit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, solidNoBuildLayer);
            }
            else
            {
                rayHit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, backingNoBuildLayer);
            }

            if (rayHit.collider != null)
            {
                buildBlocked = true;
            }
            else
            {
                buildBlocked = false;
            }

            if (Vector2.Distance(playerObject.transform.position, blockTemplate.transform.position) > maxBuildDistance)
            {
                buildBlocked = true;
            }

            if (buildBlocked)
            {
                currentRend.color = new Color(1f, 0f, 0f, 1f);
            }
            else
            {
                currentRend.color = new Color(1f, 1f, 1f, 1f);
            }

            if (Input.GetMouseButton(1) && buildBlocked == false)
            {
                GameObject newBlock = new GameObject(currentBlock.blockname);
                newBlock.transform.position = blockTemplate.transform.position;
                SpriteRenderer newRend = newBlock.AddComponent<SpriteRenderer>();
                newRend.sprite = currentBlock.blockSprite;
                
                
                
                
    
                



                if (currentBlock.isSolid == true)
                {
                    newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 10;
                    newRend.sortingOrder = -10;
                  
                    

                }
                else
                {
                    newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 11;
                    newRend.sortingOrder = -15;
                }
            }

            if (Input.GetKey("left shift") && blockTemplate != null && Input.GetMouseButton(1))
            {
                RaycastHit2D destroyHit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, allBlocksLayer);

                if (destroyHit.collider != null)
                {
                    Destroy(destroyHit.collider.gameObject);
                }
            }
        }
    }
}
