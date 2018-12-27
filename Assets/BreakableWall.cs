using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour,Boomable {

    //所在的格子
    public int row, column;
    public void setPosition(int row, int column)
    {
        this.row = row;
        this.column = column;
        GameManager.instance.boomableObjectMap[row, column] = this;
    }
    public bool onBoom()
    {
        GameManager.instance.boomableObjectMap[row, column] = null;
        Destroy(gameObject);
        //爆炸波无法通过
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
