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
    private bool isTileFilled = false;

    // tiles
    public Tilemap terrainTilemap;
    public Tile terrain00;


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

        // delete terrain that potentially blocks the path
        ClearPathOfTerrain();
    }


    void GenerateTerrain()
    {
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
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrain00);
                        }
                        // if end of tile vertically, then make it a wall
                        if (yTile == (int)(tileDimensionsInBlocks[1]) - 1)
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrain00);
                        }

                        // if beginning of tile horizontally, then make it a wall
                        if (xTile == 0)
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrain00);
                        }
                        // if beginning of tile vertically, then make it a wall
                        if (yTile == 0)
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrain00);
                        }

                        /*
                        // if ..., then make it a hole
                        if (xTile == (int)(tileDimensionsInBlocks[0] / 3) || yTile == (int)(tileDimensionsInBlocks[1] / 3))
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), null);
                        }
                        */
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
                            if (xTile == (int)((tileDimensionsInBlocks[0]) / 2))
                            {
                                terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrain00);
                            }

                            // if halfway vertically through the tile, then make it a wall 
                            if (yTile == (int)((tileDimensionsInBlocks[1]) / 2))
                            {
                                terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), terrain00);
                            }
                        }
                    }

                    for (int countX = 0; countX < 2; countX++)
                    {
                        for (int countY = 0; countY < 2; countY++)
                        {
                            isTileFilled = false;
                            if (Random.Range(1, denomFilledRoom + 1) == denomFilledRoom)
                            {
                                isTileFilled = true;
                            }

                            for (int xTile = 0; xTile < (tileDimensionsInBlocks[0] / 2) + 1; xTile++)
                            {
                                for (int yTile = 0; yTile < (tileDimensionsInBlocks[1] / 2) + 1; yTile++)
                                {
                                    // FILLED/UNFILLED TILE SPECIFIC
                                    // if filled, then make all blocks (leave a 1 gap from the borders empty ?)
                                    if (isTileFilled == true)
                                    {
                                        terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0] / 2), yTile + (yLevel * tileDimensionsInBlocks[1] / 2), 0), terrain00);
                                    }
                                    // if unfilled, then fill normally (use noise, and leave a 1 gap from the borders empty ?)
                                    else
                                    {

                                    }
                                }
                            }

                            for (int xTile = 0; xTile < (tileDimensionsInBlocks[0] / 2) + 1; xTile++)
                            {
                                for (int yTile = 0; yTile < (tileDimensionsInBlocks[1] / 2) + 1; yTile++)
                                {
                                    if ((1 <= xTile && xTile <= ((tileDimensionsInBlocks[0] / 2) - 1)) && (1 == yTile || yTile == ((tileDimensionsInBlocks[1] / 2) - 1)))
                                    {
                                        terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0] / 2), yTile + (yLevel * tileDimensionsInBlocks[1] / 2), 0), null);
                                    }

                                    else if ((1 == xTile || xTile == ((tileDimensionsInBlocks[0] / 2) - 1)) && (1 <= yTile && yTile <= ((tileDimensionsInBlocks[1] / 2) - 1)))
                                    {
                                        terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0] / 2), yTile + (yLevel * tileDimensionsInBlocks[1] / 2), 0), null);
                                    }


                                    /*
                                    // if ..., then make it a hole
                                    if (xTile == (int)(tileDimensionsInBlocks[0] / 2) || yTile == (int)(tileDimensionsInBlocks[1] / 2))
                                    {
                                        terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]), yTile + (yLevel * tileDimensionsInBlocks[1]), 0), null);
                                    }
                                    */
                                }
                            }
                        }
                    }
                }

                // GENERIC FOR ALL TILES (clear)
                for (int xTile = 0; xTile < tileDimensionsInBlocks[0]; xTile++)
                {
                    for (int yTile = 0; yTile < tileDimensionsInBlocks[1]; yTile++)
                    {         
                        // if ..., then make it a hole
                        if ((xTile % (int)(tileDimensionsInBlocks[0] / 3)) == 0 || (yTile % (int)(tileDimensionsInBlocks[1] / 3)) == 0)
                        {
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]) + (int)(tileDimensionsInBlocks[0] / 3), yTile + (yLevel * tileDimensionsInBlocks[1]) + (int)(tileDimensionsInBlocks[1] / 3), 0), null);
                            terrainTilemap.SetTile(new Vector3Int(xTile + (xLevel * tileDimensionsInBlocks[0]) + (int)(tileDimensionsInBlocks[0] / 3), yTile + (yLevel * tileDimensionsInBlocks[1]) + (int)(tileDimensionsInBlocks[1] / 3), 0), null);

                        }

                    }
                }
            }
        }
    }

    void ClearPathOfTerrain()
    {

    }

    void Update()
    {
        
    }
}
