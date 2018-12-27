using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public GameObject[] MapTiles;                                  //Array of wall prefabs.
    public GameObject[] foodTiles;                                  //Array of food prefabs.

    private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
    private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.
    [HideInInspector]
    public int[,] mapData;
    private int columns;                                         //Number of columns in our game board.
    private int rows;                                            //Number of rows in our game board.

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        //Clear our list gridPositions.
        gridPositions.Clear();

        //Loop through x axis (columns).
        for (int x = 1; x < columns - 1; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = 1; y < rows - 1; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    //Sets up the outer walls and floor (background) of the game board.
    void MapSetup()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;

        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for (int x = -1; x < columns + 1; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for (int y = -1; y < rows + 1; y++)
            {

                GameObject toInstantiate;
                //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = MapTiles[1];
                else {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    toInstantiate = MapTiles[mapData[y,x]];
                }
                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
                BreakableWall Obj = instance.GetComponent<BreakableWall>();
                if (Obj != null) {
                    Obj.setPosition(y,x);
                }
                //加上地板
                instance =
                    Instantiate(MapTiles[0], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);

            }
        }
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(int[,] map)
    {
        //init map
        this.mapData = map;
        rows = mapData.GetLength(0);
        columns = mapData.GetLength(1);
        //Creates the outer walls and floor.
        MapSetup();

        //Reset our list of gridpositions.
        InitialiseList();

    }
}
