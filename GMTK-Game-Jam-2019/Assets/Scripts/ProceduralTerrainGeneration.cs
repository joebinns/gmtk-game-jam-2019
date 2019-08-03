using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ProceduralTerrainGeneration : MonoBehaviour
{
    private List<int> levelDimensionsInTiles = new List<int>();

    private List<int> tileDimensionsInBlocks = new List<int>();

    private List<int> smallTileDimensionsInBlocks = new List<int>();

    private int denomLargeRoom = 3; // prob =  1 / denomLargeRoom
    private int denomFilledRoom = 4; // prob =  1 / denomLargeRoom

    private bool isTileLarge = false;
    //private bool isTileFilled = false;

    // tiles
    public Tilemap terrainTilemap;


    public RuleTile terrainRuleTile;
    //public Tile terrain00;

    //public Tile terrain01;
    public Tile empty;


    void Start()
    {

        // declare Level Dimensions In Tiles
        levelDimensionsInTiles.Add(3);  //x //3, since large rooms are 2x2 tiles.
        levelDimensionsInTiles.Add(3);  //y

        // declare Tile Dimensions In Blocks
        tileDimensionsInBlocks.Add(21);  //x
        tileDimensionsInBlocks.Add(21);  //y

        // procedurally create the terrain
        GenerateTerrain();
    }


    void GenerateTerrain()
    {
        int entrance;
        int exit;
        entrance = Random.Range(0, 6);
        exit = Random.Range(0, 6);

        for (int xLevel = 0; xLevel < levelDimensionsInTiles[0]; xLevel++)
        {
            for (int yLevel = 0; yLevel < levelDimensionsInTiles[1]; yLevel++)
            {
                isTileLarge = false;
                if (Random.Range(1, denomLargeRoom + 1) == denomLargeRoom)
                {
                    isTileLarge = true;
                }

                // GENERIC FOR ALL TILES
                for (int xTile = 0; xTile < tileDimensionsInBlocks[0]; xTile++)
                {
                    for (int yTile = 0; yTile < tileDimensionsInBlocks[1]; yTile++)
                    {
                        // if end of tile horizontally, then make it a wall
                        if (xTile == (int)(tileDimensionsInBlocks[0]) - 1)
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrainRuleTile);
                        }
                        // if end of tile vertically, then make it a wall
                        if (yTile == (int)(tileDimensionsInBlocks[1]) - 1)
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrainRuleTile);
                        }

                        // if beginning of tile horizontally, then make it a wall
                        if (xTile == 0)
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrainRuleTile);
                        }
                        // if beginning of tile vertically, then make it a wall
                        if (yTile == 0)
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrainRuleTile);
                        }
                    }
                }


                // LARGE/SMALL TILE SPECIFIC
                // if large room, do nothing
                if (isTileLarge == true)
                {
                }

                // if small room, then quarter it
                else
                {
                    for (int xTile = 0; xTile < tileDimensionsInBlocks[0]; xTile++)
                    {
                        for (int yTile = 0; yTile < tileDimensionsInBlocks[1]; yTile++)
                        {
                            // if halfway horizontally through the tile, then make it a wall 
                            if ((xTile == (int)((tileDimensionsInBlocks[0]) / 2)) & ((yTile != 5) & (yTile != 15)))
                            {
                                terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrainRuleTile);
                            }

                            // if halfway vertically through the tile, then make it a wall 
                            if ((yTile == (int)((tileDimensionsInBlocks[1]) / 2)) & ((xTile != 5) & (xTile != 15)))
                            {
                                terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrainRuleTile);
                            }
                        }
                    }

                    /*
                    for (int countX = 0; countX < 2; countX++)
                    {
                        for (int countY = 0; countY < 2; countY++)
                        {

                            isTileFilled = false;
                            if (Random.Range(1, denomFilledRoom + 1) == denomFilledRoom)
                            {
                                isTileFilled = true;
                            }

                            for (int xTile = 0; xTile < (int)(tileDimensionsInBlocks[0] / 2); xTile++)
                            {
                                for (int yTile = 0; yTile < (int)(tileDimensionsInBlocks[1] / 2); yTile++)
                                {
                                    // FILLED/UNFILLED TILE SPECIFIC
                                    // if filled, then make all blocks (leave a 1 gap from the borders empty ?)
                                    if (isTileFilled == true)
                                    {
                                        terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0] / 2), yTile + (yLevel * tileDimensionsInBlocks[1] / 2), 0), terrainRuleTile);
                                    }
                                    // if unfilled, then fill normally (use noise, and leave a 1 gap from the borders empty ?)
                                    else
                                    {

                                    }

                                    // place interior terrain, do this per small room
                                    int numTetris = Random.Range(0, 6); 

                                    for (int i = 0; i < numTetris; i++)
                                    {
                                        int xSpawn = Random.Range(0, 9);
                                        int ySpawn = Random.Range(0, 9);

                                        int tetrisShape = Random.Range(0, 4);   // 0 = square, 1 = L , 2 = dong, 3 = I

                                        // SQUARE
                                        if (tetrisShape == 0)
                                        {
                                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * (int)(tileDimensionsInBlocks[0] / 2) * countX), yTile + (yLevel * (int)(tileDimensionsInBlocks[1] / 2) * countY), 0), terrainRuleTile);
                                        }

                                        // L 
                                        else if (tetrisShape == 1)
                                        {

                                        }

                                        // DONG                                   
                                        else if (tetrisShape == 2)
                                        {

                                        }

                                        // I
                                        else
                                        {

                                        }
                                    }
                                }
                            }

                            for (int xTile = 0; xTile < (tileDimensionsInBlocks[0] / 2) + 1; xTile++)
                            {
                                for (int yTile = 0; yTile < (tileDimensionsInBlocks[1] / 2) + 1; yTile++)
                                {
                                    if ((1 <= xTile && xTile <= ((tileDimensionsInBlocks[0] / 2) - 1)) && (1 == yTile || yTile == ((tileDimensionsInBlocks[1] / 2) - 1)))
                                    {
                                        terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0] / 2), yTile + (yLevel * tileDimensionsInBlocks[1] / 2), 0), empty);
                                    }

                                    else if ((1 == xTile || xTile == ((tileDimensionsInBlocks[0] / 2) - 1)) && (1 <= yTile && yTile <= ((tileDimensionsInBlocks[1] / 2) - 1)))
                                    {
                                        terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0] / 2), yTile + (yLevel * tileDimensionsInBlocks[1] / 2), 0), empty);
                                    }
                                }
                            }
                        }
                    }
                    */
                }
                
                // GENERIC FOR ALL TILES (clear)
                for (int xTile = 0; xTile < tileDimensionsInBlocks[0]; xTile++)
                {
                    for (int yTile = 0; yTile < tileDimensionsInBlocks[1]; yTile++)
                    {   
                        
                        // place interior terrain
                        int numTetris = Random.Range(0, 25);

                        for (int i = 0; i < numTetris; i++)
                        {
                            int xSpawn = Random.Range(2, tileDimensionsInBlocks[0] - 4);
                            int ySpawn = Random.Range(2, tileDimensionsInBlocks[1] - 4);

                            int tetrisShape = Random.Range(0, 4);   // 0 = square, 1 = L , 2 = dong, 3 = I

                            if ((xTile == xSpawn) && (yTile == ySpawn))
                            {
                                // SQUARE
                                if (tetrisShape == 0)
                                {
                                    Vector3Int pos = new Vector3Int(xTile + (xLevel * (int)(tileDimensionsInBlocks[0])), yTile + (yLevel * (int)(tileDimensionsInBlocks[1])), 0);
                                    checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                    checkSetTile(new Vector3Int(pos.x + 1, pos.y, pos.z), terrainRuleTile);
                                    checkSetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), terrainRuleTile);
                                    checkSetTile(new Vector3Int(pos.x + 1, pos.y + 1, pos.z), terrainRuleTile);
                                }

                                // L 
                                else if (tetrisShape == 1)
                                {
                                    Vector3Int pos = new Vector3Int(xTile + (xLevel * (int)(tileDimensionsInBlocks[0])), yTile + (yLevel * (int)(tileDimensionsInBlocks[1])), 0);

                                    int rot = Random.Range(0, 4);
                                    if (rot == 0)
                                    {
                                        // 0
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 1, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y + 2, pos.z), terrainRuleTile);
                                    }
                                    else if (rot == 1)
                                    {
                                        // 90
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 1, pos.y + 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 2, pos.y + 1, pos.z), terrainRuleTile);
                                    }
                                    else if (rot == 2)
                                    {
                                        // 180
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x - 1, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y - 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y - 2, pos.z), terrainRuleTile);
                                    }
                                    else
                                    {
                                        // 270
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y - 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x - 1, pos.y - 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x - 2, pos.y - 1, pos.z), terrainRuleTile);
                                    }
                                }

                                // DONG                                   
                                else if (tetrisShape == 2)
                                {
                                    Vector3Int pos = new Vector3Int(xTile + (xLevel * (int)(tileDimensionsInBlocks[0])), yTile + (yLevel * (int)(tileDimensionsInBlocks[1])), 0);

                                    int rot = Random.Range(0, 4);
                                    if (rot == 0)
                                    {
                                        // 0
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 1, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 2, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 1, pos.y + 1, pos.z), terrainRuleTile);
                                    }
                                    else if (rot == 1)
                                    {
                                        // 90
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 1, pos.y + 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y + 2, pos.z), terrainRuleTile);
                                    }
                                    else if (rot == 2)
                                    {
                                        // 180
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x - 1, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x - 2, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x - 1, pos.y - 1, pos.z), terrainRuleTile);
                                    }
                                    else
                                    {
                                        // 270
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y - 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x - 1, pos.y - 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y - 2, pos.z), terrainRuleTile);
                                    }

                                }

                                // I
                                else
                                {
                                    Vector3Int pos = new Vector3Int(xTile + (xLevel * (int)(tileDimensionsInBlocks[0])), yTile + (yLevel * (int)(tileDimensionsInBlocks[1])), 0);

                                    int rot = Random.Range(0, 2);
                                    if (rot == 0)
                                    {
                                        // 0
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y + 2, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x, pos.y + 3, pos.z), terrainRuleTile);
                                    }
                                    else
                                    {
                                        // 90
                                        checkSetTile(new Vector3Int(pos.x, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 1, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 2, pos.y, pos.z), terrainRuleTile);
                                        checkSetTile(new Vector3Int(pos.x + 3, pos.y, pos.z), terrainRuleTile);
                                    }
                                }
                            }
                        }                      


                        // if ..., then make it a hole (making routes through level)
                        if ((xTile == 5 || xTile == 15) && (yTile == 0 || yTile == 20) || (xTile == 0 || xTile == 20) && (yTile == 5 || yTile == 15))
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), empty);

                        }


                        // if exterior, then make it a wall
                        if ((xTile + (xLevel * tileDimensionsInBlocks[0]) == 0) || (xTile + (xLevel * tileDimensionsInBlocks[0]) == (tileDimensionsInBlocks[0] * levelDimensionsInTiles[0]) - 1))
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrainRuleTile);
                        }
                        else if ((yTile + (yLevel * tileDimensionsInBlocks[1]) == 0) || (yTile + (yLevel * tileDimensionsInBlocks[1]) == (tileDimensionsInBlocks[1] * levelDimensionsInTiles[1]) - 1))
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrainRuleTile);
                        }


                        // clear shite
                        if (((xTile == 1 || xTile == tileDimensionsInBlocks[0] - 2) || (xTile == 9) || (xTile == 11)) && (yTile != 10) && (yTile != 0) && (yTile != tileDimensionsInBlocks[1] - 1))
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), empty);
                        }
                        else if (((yTile == 1 || yTile == tileDimensionsInBlocks[1] - 2) || (yTile == 9) || (yTile == 11)) && (xTile != 10) && (xTile != 0) && (xTile != tileDimensionsInBlocks[0] - 1))
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), empty);
                        }


                        // make entrance / exit
                        // entrance
                        // TODO: PLACE SPAWN HERE
                        if ((yTile + (yLevel * tileDimensionsInBlocks[1]) == (tileDimensionsInBlocks[1] * levelDimensionsInTiles[1]) - 1) & ((int)(entrance * 10.5) + 5 == (xTile + (xLevel * tileDimensionsInBlocks[0]))))
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), empty);
                        }

                        // exit
                        // TODO: PLACE EXIT HERE
                        if ((yTile + (yLevel * tileDimensionsInBlocks[1]) == 0) & ((int)(exit * 10.5) + 5 == (xTile + (xLevel * tileDimensionsInBlocks[0]))))
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), empty);
                        }
                    }
                }
            }
        }
    }

    private void checkSetTile(Vector3Int pos, Tile tile)
    {
        if (terrainTilemap.GetTile(pos) == null)
        {
            terrainTilemap.SetTile(pos, tile);
        }
    }
    private void checkSetTile(Vector3Int pos, RuleTile tile)
    {
        if (terrainTilemap.GetTile(pos) == null)
        {
            terrainTilemap.SetTile(pos, tile);
        }
    }

    void Update()
    {
        
    }
}
