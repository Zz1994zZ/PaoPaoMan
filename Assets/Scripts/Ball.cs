using System;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, Boomable
{

    public int force;
    public float boomDelay;
    public GameObject[] boomSprite;
    //所在的格子
    private int row, column;
    // Use this for initialization
    void Start () {
        //这里不知道会不会有问题如果有问题的话
        GameManager.instance.map[row, column] = 0;
        Invoke("onBoom", boomDelay);
	}
    public void setPosition(int row, int column) {
        this.row = row;
        this.column = column;
        GameManager.instance.boomableObjectMap[row, column] = this;
    }



    public bool onBoom()
    {
        Boomable[,] boomableObjectMap = GameManager.instance.boomableObjectMap;
        boomableObjectMap[row, column] = null;
        int[,] map = GameManager.instance.map;

        GameManager.instance.map[row, column] =0;
        Instantiate(boomSprite[4], new Vector3(column, row,  0f), Quaternion.identity);
        for (int i = 1; i < force; i++) {
            int r = row;
            int c = column + i;
            if (c >= map.GetLength(1))
                break;
            if (map[r, c] == 1) {
                break;
            } else if (boomableObjectMap[r, c] != null) {
                //TODO 引爆
                if (!boomableObjectMap[r, c].onBoom()) {
                    Instantiate(boomSprite[3], new Vector3(c, r, 0f), Quaternion.identity);
                    break;
                }
            }
            Instantiate(boomSprite[3], new Vector3(c, r, 0f), Quaternion.identity);
        }
        for (int i = 1; i < force; i++)
        {
            int r = row;
            int c = column - i;
            if (c < 0)
                break;
            if (map[r, c] == 1)
            {
                break;
            }
            else if (boomableObjectMap[r, c] != null)
            {
                //TODO 引爆
                if (!boomableObjectMap[r, c].onBoom())
                {
                    Instantiate(boomSprite[2], new Vector3(c, r, 0f), Quaternion.identity);
                    break;
                }
            }
            Instantiate(boomSprite[2], new Vector3(c, r, 0f), Quaternion.identity);
        }
        for (int i = 1; i < force; i++)
        {
            int r = row + i;
            int c = column;
            if (r >= map.GetLength(0))
                break;
            if (map[r, c] == 1)
            {
                break;
            }
            else if (boomableObjectMap[r, c] != null)
            {
                //TODO 引爆
                if (!boomableObjectMap[r, c].onBoom())
                {
                    Instantiate(boomSprite[0], new Vector3(c, r, 0f), Quaternion.identity);
                    break;
                }
            }
            Instantiate(boomSprite[0], new Vector3(c, r, 0f), Quaternion.identity);
        }
        for (int i = 1; i < force; i++)
        {
            int r = row - i;
            int c = column;
            if (r <0)
                break;
            if (map[r, c] == 1)
            {
                break;
            }
            else if (boomableObjectMap[r, c] != null)
            {
                //TODO 引爆
                if (!boomableObjectMap[r, c].onBoom())
                {
                    Instantiate(boomSprite[1], new Vector3(c, r, 0f), Quaternion.identity);
                    break;
                }
            }
            Instantiate(boomSprite[1], new Vector3(c, r, 0f), Quaternion.identity);
        }

        //TODO 爆炸逻辑
        Destroy(gameObject);
        //爆炸波可以通过
        return true;
    }

    // Update is called once per frame
    void Update () {
		
	}

}
