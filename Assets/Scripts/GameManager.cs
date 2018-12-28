using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    [HideInInspector]
    public  BoardManager mapScript;
    public GameObject ballSprite;
    [HideInInspector]
    public  List<Man> players = new List<Man>();
    [HideInInspector]
    public  int[,] map =  {
            {0,0,0,2,2,0,0,0,2,0,0,0,2,2,0,0,0},
            {0,0,1,2,0,2,2,2,0,2,2,2,0,2,1,0,0},
            {1,0,2,0,2,0,2,0,2,0,2,0,2,0,2,2,1},
            {1,0,0,2,0,2,1,1,1,1,1,2,0,2,0,2,1},
            {1,2,0,2,2,2,2,2,2,2,2,2,2,2,0,2,1},
            {1,2,0,2,0,2,2,2,2,2,2,2,0,2,0,2,1},
            {1,2,0,2,2,2,1,1,1,1,2,2,2,2,0,2,1},
            {1,2,2,0,2,0,2,2,2,2,2,0,2,0,2,2,1},
            {1,2,2,2,0,2,2,2,2,2,2,2,0,2,2,2,1},
            {1,2,2,2,2,0,2,0,0,0,2,0,2,2,2,2,1},
            {0,0,2,0,2,2,0,2,0,2,0,2,2,0,2,0,0},
            {0,0,0,0,2,2,2,0,2,0,2,2,2,0,0,0,0}};
    public Boomable [,] boomableObjectMap;
    //Awake is always called before any Start functions
    void Awake()
    {
        boomableObjectMap = new Boomable[map.GetLength(0), map.GetLength(1)];
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        mapScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        mapScript.SetupScene(map);

    }
    public void  setBall(int row,int column,int force) {
        if (boomableObjectMap[row, column] == null) {
            //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
            GameObject instance =
                Instantiate(ballSprite, new Vector3(column, row, 0f), Quaternion.identity) as GameObject;
            //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
            //instance.transform.SetParent(boardHolder);
            Ball ball = instance.GetComponent<Ball>();
            ball.force = force;
            ball.setPosition(row, column);
            boomableObjectMap[row, column] = ball;
            foreach (Man man in players) {
                if (man.row == row && man.column == column) {
                    Physics2D.IgnoreCollision(ball.GetComponent<BoxCollider2D>(), man.GetComponent<CircleCollider2D>(), true);
                }
            }
        }
    }


    //Update is called every frame.
    void Update()
    {

    }
}
