using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour {


    public Text bonusPoint;
    public int point = 1;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void updatePoint(int i)
    {
        point = i;
        bonusPoint.text = point.ToString();
    }
}
