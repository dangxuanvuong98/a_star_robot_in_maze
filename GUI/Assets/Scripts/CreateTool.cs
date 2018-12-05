using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CreateTool : MonoBehaviour {


    CreateMapManager mapManager;
    public GameObject mapObject;   
    public Camera cam;
    public GameObject[] tileList;
    public GameObject bonusPrefab;
    public GameObject startPrefab;
    public GameObject finishPrefab;
    RaycastHit2D[] hit;
    public int selectionMode;
    public int bonusMode;

    public int startFinishMode;
    public bool haveStart = false;
    public bool haveFinish = false;

    public InputField inputBonusScore;
    int bonusScore;

    // Use this for initialization
    void Start () {
        mapManager = GetComponent<CreateMapManager>();
        selectionMode = 0;
        haveStart = false;
        haveFinish = false;
    }
	

	// Update is called once per frame
	void Update () {
        
        if(!mapManager.isStart)
        {
            return;
        }
        if(mapManager.isDrawMap)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.RaycastAll(ray.origin, ray.direction * 50);
                if (hit.Length > 0)
                {
                    if (hit[0].transform.CompareTag("Map"))
                    {
                        if (selectionMode == 0)
                        {
                            for (int i = 1; i < 9; i++)
                            {
                                if (hit[0].transform.GetComponent<MapDetail>().mark[i] == 1)
                                {
                                    Vector3 temp = findNextTile(i);
                                    Ray temp2 = new Ray(hit[0].transform.position + temp, new Vector3(0, 0, -10));
                                    RaycastHit2D[] hit2 = Physics2D.RaycastAll(temp2.origin, temp2.direction * 50);
                                    if (hit2[0].transform.CompareTag("Map"))
                                    {
                                        if (hit2[0].transform.GetComponent<MapDetail>().mark[nextTileSelection(i)] == 1)
                                        {
                                            int j = Mathf.FloorToInt(hit2[0].transform.position.x);
                                            int k = Mathf.FloorToInt(hit2[0].transform.position.y);
                                            int l = hit2[0].transform.GetComponent<MapDetail>().tileNumber - nextTileSelection(i);
                                            mapManager.map[mapManager.m - k - 1, j] = l;
                                            Instantiate(tileList[l], hit2[0].transform.position, Quaternion.identity, mapObject.transform);
                                            Destroy(hit2[0].transform.gameObject);
                                        }
                                    }
                                }
                            }
                            int x = Mathf.FloorToInt(hit[0].transform.position.x);
                            int y = Mathf.FloorToInt(hit[0].transform.position.y);
                            mapManager.map[mapManager.m - y - 1, x] = 0;
                            Instantiate(tileList[0], hit[0].transform.position, Quaternion.identity, mapObject.transform);
                            Destroy(hit[0].transform.gameObject);
                            return;
                        }
                        if (hit[0].transform.GetComponent<MapDetail>().mark[selectionMode] == 0)
                        {
                            Vector3 temp = findNextTile(selectionMode);
                            Ray temp2 = new Ray(hit[0].transform.position + temp, new Vector3(0, 0, -10));
                            RaycastHit2D[] hit2 = Physics2D.RaycastAll(temp2.origin, temp2.direction * 50);
                            if (hit2[0].transform.CompareTag("Map"))
                            {
                                if (hit2[0].transform.GetComponent<MapDetail>().mark[nextTileSelection(selectionMode)] == 0)
                                {
                                    int j = Mathf.FloorToInt(hit2[0].transform.position.x);
                                    int k = Mathf.FloorToInt(hit2[0].transform.position.y);
                                    int l = hit2[0].transform.GetComponent<MapDetail>().tileNumber + nextTileSelection(selectionMode);
                                    mapManager.map[mapManager.m - k - 1, j] = l;
                                    Instantiate(tileList[l], hit2[0].transform.position, Quaternion.identity, mapObject.transform);
                                    //Debug.Log("Next tile: " + (mapManager.map[mapManager.m - k - 1, j]) + "(" + (mapManager.m - k - 1) + "," + j);
                                    Destroy(hit2[0].transform.gameObject);
                                }
                            }

                            int x = Mathf.FloorToInt(hit[0].transform.position.x);
                            int y = Mathf.FloorToInt(hit[0].transform.position.y);
                            int n = hit[0].transform.GetComponent<MapDetail>().tileNumber + selectionMode;
                            mapManager.map[mapManager.m - y - 1, x] = n;
                            Instantiate(tileList[n], hit[0].transform.position, Quaternion.identity, mapObject.transform);
                            //Debug.Log("Current tile: " + (mapManager.map[mapManager.m - y - 1, x]) + "(" + (mapManager.m - y - 1) + "," + x);
                            Destroy(hit[0].transform.gameObject);
                        }
                    }
                }
            }
            return;
        }
        if(mapManager.isBonus)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.RaycastAll(ray.origin, ray.direction * 50);
                if(bonusMode == 0)
                {
                    if (hit.Length == 2)
                    { 
                        if (hit.Length > 1)
                        {
                            if (hit[1].transform.CompareTag("Bonus"))
                            {
                                int x = Mathf.FloorToInt(hit[0].transform.position.x);
                                int y = Mathf.FloorToInt(hit[0].transform.position.y);
                                mapManager.bonus[mapManager.m - 1 - y, x] = 0;
                                Destroy(hit[1].transform.gameObject);
                            }
                        }
                    }
                    return;
                }
                if (hit.Length > 0)
                {
                    if(hit.Length == 1)
                    {
                        if (hit[0].transform.CompareTag("Map"))
                        {
                            bonusPrefab.GetComponent<BonusManager>().updatePoint(bonusScore);
                            int x = Mathf.FloorToInt(hit[0].transform.position.x);
                            int y = Mathf.FloorToInt(hit[0].transform.position.y);
                            Instantiate(bonusPrefab, new Vector3(x, y, 1), Quaternion.identity, mapManager.bonusClone.transform);
                            mapManager.bonus[mapManager.m - 1 - y, x] = bonusScore;
                        }
                    }
                    if (hit.Length == 2)
                    {
                        if (hit.Length > 1)
                        {
                            if(hit[1].transform.CompareTag("Bonus"))
                            {
                                bonusPrefab.GetComponent<BonusManager>().updatePoint(bonusScore);
                                int x = Mathf.FloorToInt(hit[1].transform.position.x);
                                int y = Mathf.FloorToInt(hit[1].transform.position.y);
                                Destroy(hit[1].transform.gameObject);
                                Instantiate(bonusPrefab, new Vector3(x, y, 1), Quaternion.identity, mapManager.bonusClone.transform);
                                mapManager.bonus[mapManager.m - 1 - y, x] = bonusScore;
                            }           
                        }
                    }
                }
            }
            return;
        }
        if(mapManager.isStartFinish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.RaycastAll(ray.origin, ray.direction * 50);
                if (hit.Length == 1 && startFinishMode != 0) 
                {
                    if (hit[0].transform.CompareTag("Map"))
                    {

                        if (startFinishMode == 1 && !haveStart)
                        {
                            int x = Mathf.FloorToInt(hit[0].transform.position.x);
                            int y = Mathf.FloorToInt(hit[0].transform.position.y);
                            Instantiate(startPrefab, new Vector3(x, y, 2), Quaternion.identity, mapManager.bonusClone.transform);
                            mapManager.Sr = mapManager.m - y - 1;
                            mapManager.Sc = x;
                            haveStart = true;
                            return;
                        }
                        else if (startFinishMode == 2 && !haveFinish)
                        {
                            int x = Mathf.FloorToInt(hit[0].transform.position.x);
                            int y = Mathf.FloorToInt(hit[0].transform.position.y);
                            Instantiate(finishPrefab, new Vector3(x, y, 2), Quaternion.identity, mapManager.bonusClone.transform);
                            mapManager.Fr = mapManager.m - y - 1;
                            mapManager.Fc = x;
                            haveFinish = true;
                            return;
                        }  
                    }
                }
                if (hit.Length == 2 && startFinishMode == 0)
                {
                    if (hit[1].transform.CompareTag("Start"))
                    {
                        //int x = Mathf.FloorToInt(hit[0].transform.position.x);
                        //int y = Mathf.FloorToInt(hit[0].transform.position.y);
                        mapManager.Sr = 0;
                        mapManager.Sc = 0;
                        Destroy(hit[1].transform.gameObject);
                        haveStart = false;
                        return;
                    }
                    if (hit[1].transform.CompareTag("End"))
                    {
                        //int x = Mathf.FloorToInt(hit[0].transform.position.x);
                        //int y = Mathf.FloorToInt(hit[0].transform.position.y);
                        mapManager.Fr = 0;
                        mapManager.Fc = 0;
                        Destroy(hit[1].transform.gameObject);
                        haveFinish = false;
                        return;
                    }
                }
                return;
            }
        }
    }


    public void button0()
    {
        selectionMode = 0;
    }
    public void button1()
    {
        selectionMode = 1;
    }
    public void button2()
    {
        selectionMode = 2;
    }
    public void button4()
    {
        selectionMode = 4;
    }
    public void button8()
    {
        selectionMode = 8;
    }


    public void bonusButton()
    {
        bonusMode = 1;
    }
    public void deleteButton()
    {
        bonusMode  = 0;
    }



    Vector3 findNextTile(int i)
    {
        if (i == 1)
            return Vector3.up;
        if (i == 2)
            return Vector3.right;
        if (i == 4)
            return Vector3.down;
        return Vector3.left;
    }

    int nextTileSelection(int i)
    {
        if (i == 1) return 4;
        if (i == 2) return 8;
        if (i == 4) return 1;
        return 2;
    }

    public void bonusChange()
    {
        bonusScore = Int32.Parse(inputBonusScore.text.ToString());
    }

    public void startButton()
    {
        startFinishMode = 1;
    }

    public void finishButton()
    {
        startFinishMode = 2;
    }

    public void deleteSFButton()
    {
        startFinishMode = 0;
    }
}
