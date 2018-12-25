using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MovingObject : MonoBehaviour {
    public int speed;


    private enum State
    {
        Idle,
        Up,
        Down,
        Left,
        Right
    }
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;
    private State state = State.Idle;
    private State lastState = State.Idle;
    private  Direction direction = Direction.Down;

    private Animator playerAnimator;
    private LinkedList<State> oplist = new LinkedList<State>();
    private Sprite[] idleSprites;
    void Start() {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }


    void FixedUpdate() {
        switch (state) {         
          case State.Up:
              direction = Direction.Up;
              transform.Translate(0, speed * Time.deltaTime, 0, Space.Self);
              break;
          case State.Down:
                direction = Direction.Down;
              transform.Translate(0, -speed * Time.deltaTime, 0, Space.Self);
                break;
          case State.Left:
                direction = Direction.Left;
              transform.Translate(-speed * Time.deltaTime, 0, 0, Space.Self);
                break;
          case State.Right:
                direction = Direction.Right;
              transform.Translate(speed * Time.deltaTime, 0, 0, Space.Self);
                break;
      }
    }

    // Update is called once per frame
    private void Update()
    {
        caculateState();
    }
    private void caculateState() {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (oplist.Last!=null && State.Up == oplist.Last.Value) {
                return;
            }
            if (oplist.Contains(State.Up)) {
                oplist.Remove(State.Up);
            }
            oplist.AddLast(State.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (oplist.Last != null && State.Down == oplist.Last.Value)
            {
                return;
            }
            if (oplist.Contains(State.Down))
            {
                oplist.Remove(State.Down);
            }
            oplist.AddLast(State.Down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (oplist.Last != null && State.Left == oplist.Last.Value)
            {
                return;
            }
            if (oplist.Contains(State.Left))
            {
                oplist.Remove(State.Left);
            }
            oplist.AddLast(State.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (oplist.Last != null && State.Right == oplist.Last.Value)
            {
                return;
            }
            if (oplist.Contains(State.Right))
            {
                oplist.Remove(State.Right);
            }
            oplist.AddLast(State.Right);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            oplist.Remove(State.Up);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            oplist.Remove(State.Down);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            oplist.Remove(State.Left);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            oplist.Remove(State.Right);
        }

        if (oplist.Count == 0)
        {
            state = State.Idle;
        }
        else
        {
            state = oplist.Last.Value;
        }
        if (lastState == state)
            return;
        else
        {
            
            //改变姿态
            playerAnimator.SetBool("turnIdle", false);
            playerAnimator.SetBool("turnBack", false);
            playerAnimator.SetBool("turnAhead", false);
            playerAnimator.SetBool("turnLeft", false);
            playerAnimator.SetBool("turnRight", false);
            switch (state)
            {
                case State.Idle:
                    playerAnimator.SetBool("turnIdle", true);
                    break;
                case State.Up:
                    playerAnimator.SetBool("turnBack", true);
                    break;
                case State.Down:
                    playerAnimator.SetBool("turnAhead", true);
                    break;
                case State.Left:
                    playerAnimator.SetBool("turnLeft", true);
                    break;
                case State.Right:
                    playerAnimator.SetBool("turnRight", true);
                    break;
            }
            print(oplist.Count);
            lastState = state;
        }
      
    }

}
