using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
public class MapManager : MonoBehaviour
{

    [HideInInspector]
    public int m, n, Sr, Sc, Fr, Fc;
    int[,] map;
    int[,] bonus;

    public int[,] miniBonus;

    public string[] movement;

    public Camera mainCamera;

    public GameObject[] listOfTilePrefabs;
    public GameObject bonusPrefab;
    public GameObject AIPrefab;
    public GameObject startPrefab;
    public GameObject finishPrefab;
    public GameObject samplePrefab;

    public AiController currentAI;
    public Text scoreText;
    public int score;
    public int count = 0;
    public Text instructionText;
    public Transform parent;
    public Transform bonusParent;

    public GameObject miniMap;

    public float AISpeed = 5;
    public Slider AISpeedChange;
    public bool isInteracable = true;
    // Use this for initialization
    void Start()
    {
        isInteracable = true;
        AISpeedChange.value = PlayerPrefs.GetFloat("AI Speed");
        //Map Input
        string textFile = ReadString(PlayerPrefs.GetString("Input Directory"));
        string[] temp = textFile.Split(' ', '\n');
        getData(temp);
        mainCamera.transform.position = new Vector3(divideAndRoundUp(n), divideAndRoundUp(m), -10);
        miniBonus = new int[m, n];
        for(int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                Instantiate(listOfTilePrefabs[map[i, j]], new Vector2(j, m - 1 - i), Quaternion.identity);
            }
        }
        int maxSize = max(m, n);
        mainCamera.orthographicSize = cameraSize(maxSize);

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if(bonus[i,j] != 0)
                {
                    bonusPrefab.GetComponent<BonusManager>().updatePoint(bonus[i, j]);
                    Instantiate(bonusPrefab, new Vector2(j, m - 1 - i), Quaternion.identity, bonusParent);
                }
                    
            }
        }
        Instantiate(AIPrefab, new Vector2(Sc, m - 1 - Sr), Quaternion.identity);
        currentAI = FindObjectOfType<AiController>();
        currentAI.gameObject.SetActive(false);
        Instantiate(startPrefab, new Vector2(Sc, m - 1 - Sr), Quaternion.identity);
        Instantiate(finishPrefab, new Vector2(Fc, m - 1 - Fr), Quaternion.identity);

        // Movement input
        string movementFile = ReadString(PlayerPrefs.GetString("Movement Directory"));
        movement = movementFile.Split(' ', '\n');
        score = 0;
        count = 0;
        miniMap.GetComponent<BestMapDraw>().setValues(map, m, n, Sr, Sc, Fr, Fc);
        miniMap.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
        currentAI.moveSpeed = AISpeedChange.value;
        PlayerPrefs.SetFloat("AI Speed", currentAI.moveSpeed);
        //scoreText.text = score.ToString();
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        if(isInteracable)
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                for(int i = 0; i < movement.Length; i++)
                {
                    if (movement[i] == "B")
                    {
                        count = i;
                        break;
                    }  
                }
                currentAI.GetComponent<AiController>().isStart = true;
                isInteracable = false;
                count += 1;
                currentAI.gameObject.SetActive(true);
                destroyAllChild(bonusParent);
                destroyAllChild(parent);
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (bonus[i, j] != 0)
                        {
                            bonusPrefab.GetComponent<BonusManager>().updatePoint(bonus[i, j]);
                            Instantiate(bonusPrefab, new Vector2(j, m - 1 - i), Quaternion.identity, bonusParent);
                        }
                    }
                }
                scoreText.text = "0";
                score = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (movement[count] == "B")
                {
                    currentAI.GetComponent<AiController>().isStart = true;
                    isInteracable = false;
                    count += 1;
                    currentAI.gameObject.SetActive(true);
                    destroyAllChild(parent);
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (bonus[i, j] != 0)
                            {
                                bonusPrefab.GetComponent<BonusManager>().updatePoint(bonus[i, j]);
                                Instantiate(bonusPrefab, new Vector2(j, m - 1 - i), Quaternion.identity, bonusParent);
                            }
                        }
                    }
                    scoreText.text = "0";
                    score = 0;
                    
                }
                destroyAllChild(parent);
                int miniX = 0;
                int miniY = 0;
                if (movement[count] == "P")
                {
                    
                    destroyAllChild(bonusParent);
                    int x = Int32.Parse(movement[count + 2]);
                    int y = m - 1 - Int32.Parse(movement[count + 1]);
                    Instantiate(AIPrefab, new Vector2(x, y), Quaternion.identity, parent);
                    count += 3;
                    scoreText.text = movement[count];
                    count += 1;
                    for (int i = 0; i < 4; i++)
                    {
                        if (movement[count] != "F")
                        {
                            int temp = Int32.Parse(movement[count]);
                            samplePrefab.GetComponent<BonusManager>().updatePoint(temp);
                            if (i == 0)
                            {
                                Instantiate(samplePrefab, new Vector2(x, y + 1), Quaternion.identity, parent);

                            }
                            else if (i == 1)
                            {
                                Instantiate(samplePrefab, new Vector2(x + 1, y), Quaternion.identity, parent);

                            }
                            else if (i == 2)
                            {
                                Instantiate(samplePrefab, new Vector2(x, y - 1), Quaternion.identity, parent);

                            }
                            else if (i == 3)
                            {
                                Instantiate(samplePrefab, new Vector2(x - 1, y), Quaternion.identity, parent);
                            }
                        }
                        count += 1;
                    }
                    int nextBonusPoint = 0;
                    int temp2 = 0;
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            int bonusPoint = Int32.Parse(movement[count + temp2]);                                
                            if (bonusPoint != 0)
                            {
                                bonusPrefab.GetComponent<BonusManager>().updatePoint(bonusPoint);
                                Instantiate(bonusPrefab, new Vector2(j, m - 1 - i), Quaternion.identity, bonusParent);
                            }
                            
                            if (movement[count + m * n] == "P")
                            {
                                nextBonusPoint = Int32.Parse(movement[count + temp2 + 8 + m * n]);
                                miniBonus[i, j] = nextBonusPoint;
                            }
                            temp2 += 1;
                        }
                    }
                    if (movement[count + m * n] == "P")
                    {
                        miniX = Int32.Parse(movement[count + 1 + m * n]);
                        miniY = Int32.Parse(movement[count + 2 + m * n]);
                    }  
                    count += m * n;
                }
                miniMap.GetComponent<BestMapDraw>().drawBonus(miniBonus, miniX, miniY);  
            }

        }
        else
        {
            scoreText.text = score.ToString();
        }

    }

    static string ReadString(string path)
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string str = reader.ReadToEnd();
        reader.Close();
        return str;
    }

    void getData(string[] str)
    {
        m = Int32.Parse(str[0]);
        n = Int32.Parse(str[1]);
        Sr = Int32.Parse(str[2]);
        Sc = Int32.Parse(str[3]);
        Fr = Int32.Parse(str[4]);
        Fc = Int32.Parse(str[5]);
        map = new int[m, n];
        bonus = new int[m, n];
        int count = 6;
        int offset = m * n;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                map[i, j] = Int32.Parse(str[count]);
                bonus[i, j] = Int32.Parse(str[count + offset]);
                count++;
            }
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
        return ((int)a / 9 + 1) * 9;
    }
    
    /*
    int cameraSize(int a)
    {
        if (a > 9)
        {
            return ((int)a / 9 + 1) * 9;
        }
        return ((int)9 / a) + 1;
    }
    */

    public void scoreChange(int i)
    {
        Debug.Log("PLUS " + i + " POINTS");
        score += i;
    }

    public void instructionButton()
    {
        if(instructionText.IsActive())
        {
            instructionText.gameObject.SetActive(false);
            return;
        }
        instructionText.gameObject.SetActive(true);
    }

    public void backButton()
    {
        SceneManager.LoadScene(0);
    }

    void destroyAllChild(Transform t)
    {
        int childs = t.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(t.GetChild(i).gameObject);
        }
    }

}
