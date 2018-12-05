using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AiController : MonoBehaviour {
    int count = 0;
    public float moveSpeed;

    MapManager mapManager;
    public string[] movement;
    Vector3 destPosition;

    bool isFinish = false;
    public bool isStart = false;
    bool isPlay = false;
	// Use this for initialization
	void Start () {
        mapManager = FindObjectOfType<MapManager>();
        this.movement = mapManager.movement;
        destPosition = transform.position;
        isFinish = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(isStart)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isPlay)
            {
                this.count = mapManager.count;
                isPlay = true;
            }
                
        }
        if(isPlay)
        {
            if (!isFinish)
            {
                if (count > movement.Length)
                {
                    return;
                }
                
                if (transform.position == destPosition)
                {
                    if (count < movement.Length)
                    {
                        destPosition = dest(movement[count]);
                        mapManager.score -= 1;
                    }
                    count += 1;
                    return;
                }
                transform.position = Vector2.MoveTowards(transform.position, destPosition, moveSpeed * Time.deltaTime);
            }
        }
        
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bonus"))
        {
            int i = collision.GetComponent<BonusManager>().point;
            GameObject.Destroy(collision.gameObject);
            mapManager.scoreChange(i);  
            return;
        }
        if (collision.CompareTag("End"))
        {
            Destroy(collision.gameObject);
            return;
        }
    }

    private Vector3 dest(string move)
    {
        if (move == "W")
            return transform.position + Vector3.up;
        else if (move == "D")
            return transform.position + Vector3.right;
        else if (move == "S")
            return transform.position + Vector3.down; 
        return transform.position + Vector3.left;
    }
}
