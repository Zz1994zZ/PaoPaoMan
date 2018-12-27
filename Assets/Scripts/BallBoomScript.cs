using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBoomScript : MonoBehaviour {

    public float disappearDelay;
    // Use this for initialization
    void Start () {
        //这里不知道会不会有问题如果有问题的话
        Invoke("disappear", disappearDelay);
    }
    private void disappear()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
