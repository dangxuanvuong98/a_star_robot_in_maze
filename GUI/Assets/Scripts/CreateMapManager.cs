using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class CreateMapManager : MonoBehaviour {


    public int[,] map;
    public int[,] bonus;
    public int m, n;
    public int Sr, Sc, Fr, Fc;

    public InputField row;
    public InputField column;
    public Button backToMainMenu;
    public Button Enter;
    public Button backToSelect;
    public GameObject[] tilePrefabs;
    public GameObject mapObject;
    public Camera mainCamera;
    public GameObject selectionModeButton;
    public GameObject next;
    public GameObject backToDrawMapMode;
    public GameObject bonusObject;
    public GameObject bonusClone;
    public GameObject startFinishUI;
    public GameObject startFinishObject;
    public bool isStart = false;
    public bool isDrawMap = false;
    public bool isBonus = false;
    public bool isStartFinish = false;
    
    // Use this for initialization
    void Start () {
        isStart = false;
        isDrawMap = false;
        isBonus = false;
        isStartFinish = false;
}
	
	// Update is called once per frame
	void Update () {
        if (m != 0 && n != 0 && isStart == false)
        {
            Enter.gameObject.SetActive(true);
        }
        else
        {
            Enter.gameObject.SetActive(false);
        }     
    }

    public void backToMainMenuButton()
    {
        
        SceneManager.LoadScene(0);
    }

    public void backToSelectButton()
    {
        backToMainMenu.gameObject.SetActive(true);
        Enter.gameObject.SetActive(true);
        backToSelect.gameObject.SetActive(false);
        row.gameObject.SetActive(true);
        column.gameObject.SetActive(true);
        mapObject.gameObject.SetActive(false);
        isStart = false;
        next.gameObject.SetActive(false);
        isDrawMap = false;
        selectionModeButton.gameObject.SetActive(false);
        destroyAllChild(mapObject);
        mainCamera.transform.position = Vector3.zero;
        mainCamera.GetComponent<CreateCameraController>().setMaxDistance(false);
    }

    public void enterButton()
    {
        backToMainMenu.gameObject.SetActive(false);
        Enter.gameObject.SetActive(false);
        backToSelect.gameObject.SetActive(true);
        row.gameObject.SetActive(false );
        column.gameObject.SetActive(false);
        mapObject.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
        isStart = true;
        isDrawMap = true;
        selectionModeButton.gameObject.SetActive(true);
        map = new int[m, n];
        bonus = new int[m, n];
        int maxSize = max(m, n);
        mainCamera.orthographicSize = cameraSize(maxSize);
        mainCamera.transform.position = new Vector3(divideAndRoundUp(n), divideAndRoundUp(m), -10);
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Instantiate(tilePrefabs[map[i, j]], new Vector3(j, m - 1 - i,0), Quaternion.identity, mapObject.transform);
            }
        }
        mainCamera.GetComponent<CreateCameraController>().setMaxDistance(true);
    }

    public void rowInput()
    {
        m = Int32.Parse(row.text);
    }

    public void columnInput()
    {
        n = Int32.Parse(column.text);
    }

    void destroyAllChild(GameObject parent)
    {
        Transform temp = parent.transform;
        Transform[] children = new Transform[temp.childCount];
        for(int i = 0; i < temp.childCount; i++ )
        {
            children[i] = temp.GetChild(i);
        }

        foreach(Transform i in children)
        {
            Destroy(i.gameObject);
        }
    }

    int divideAndRoundUp(int n)
    {
        if (n % 2 == 0)
            return n / 2;
        return n / 2 + 1;
    }

    public int max(int a, int b)
    {
        if (a > b)
            return a;
        return b;
    }

    
    int cameraSize(int a)
    {
        return (a / 9 + 1) * 9;
    }


    public void nextButton()
    {
        if(isDrawMap)
        {
            isDrawMap = false;
            isBonus = true;
            selectionModeButton.gameObject.SetActive(false);
            backToDrawMapMode.gameObject.SetActive(true);
            bonusObject.gameObject.SetActive(true);
            bonusClone.gameObject.SetActive(true);
            return;
        }
        if(isBonus)
        {
            isBonus = false;
            isStartFinish = true;
            startFinishUI.gameObject.SetActive(true);
            bonusObject.gameObject.SetActive(false);
            startFinishUI.gameObject.SetActive(true);
            startFinishObject.gameObject.SetActive(true);
            return;
        }
        if(isStartFinish)
        {
            string path = PlayerPrefs.GetString("Input Directory");
            StreamWriter writer = new StreamWriter(path, false);
            string data = m + " " + n + " " + Sr + " " + Sc + " " + Fr + " " + Fc + "\n";
            for (int i = 0; i < m; i++) 
            {
                for (int j = 0; j < n; j++) 
                {
                    data += map[i,j].ToString();
                    if (j < n - 1) data += " ";
                }
                data += "\n";
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    data += bonus[i, j].ToString();
                    if (j < n - 1) data += " ";
                }
                if(i < m-1)
                    data += "\n";
            }
            writer.WriteLine(data);
            writer.Close();
            SceneManager.LoadScene(0);
        }
    }

    public void backToDrawMapButton()
    {
        if(isBonus)
        {
            isDrawMap = true;
            isBonus = false;
            selectionModeButton.gameObject.SetActive(true);
            backToDrawMapMode.gameObject.SetActive(false);
            bonusObject.gameObject.SetActive(false);
            bonusClone.gameObject.SetActive(false);
            return;
        }
        if(isStartFinish)
        {
            isStartFinish = false;
            isBonus = true;
            startFinishUI.gameObject.SetActive(false);
            bonusObject.gameObject.SetActive(true);
            startFinishUI.gameObject.SetActive(false);
            startFinishObject.gameObject.SetActive(false);
            return;
        }        
    }

}
