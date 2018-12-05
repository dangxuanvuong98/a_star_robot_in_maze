using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestMapDraw : MonoBehaviour {

    int m, n, Sr, Sc, Fr, Fc;
    int[,] map;
    int[,] bonus;
    public GameObject mapManager;
    public GameObject bonusPrefab;
    public GameObject AIPrefab;
    public GameObject startPrefab;
    public GameObject finishPrefab;
    public GameObject[] tileMap;
    public float miniSize = .5f;
    private void Start()
    {
        

    }

    public void setValues(int[,] map, int m, int n, int Sr, int Sc, int Fr, int Fc)
    {
        this.map = map;
        this.m = m;
        this.n = n;
        this.Sr = Sr;
        this.Sc = Sc;
        this.Fr = Fr;
        this.Fc = Fc;
        transform.position += new Vector3((- n - 1) * miniSize, 0, 0);
        Instantiate(startPrefab, new Vector2(Sc + transform.position.x, m - 1 - Sr + transform.position.y), Quaternion.identity, transform);
        Instantiate(finishPrefab, new Vector2(Fc + transform.position.x, m - 1 - Fr + transform.position.y), Quaternion.identity, transform);
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Instantiate(tileMap[map[i, j]], new Vector2(j + transform.position.x, m - 1 - i + transform.position.y), Quaternion.identity,transform);
            }
        }
        Vector3 temp = transform.localScale;
        temp *= miniSize;
        transform.localScale = temp;
    }


    public void setBonus(int[,] t)
    {
        bonus = t;
    }

    public void drawBonus(int[,] bonus, int x, int y)
    {
        destroyAllChild(transform.GetChild(0));
        Vector3 temp = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        temp /= miniSize;
        transform.localScale = temp;
        Instantiate(AIPrefab, new Vector2(y + transform.position.x, m - 1 - x + transform.position.y), Quaternion.identity, transform.GetChild(0));
        this.bonus = bonus;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int bonusPoint = bonus[i, j];
                if (bonusPoint != 0)
                {
                    bonusPrefab.GetComponent<BonusManager>().updatePoint(bonusPoint);
                    Instantiate(bonusPrefab, new Vector2(j + transform.position.x, m - 1 - i + transform.position.y), Quaternion.identity, transform.GetChild(0));
                }
            }
        }
        temp = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        temp *= miniSize;
        transform.localScale = temp;
    }

    void destroyAllChild(Transform t)
    {
        int children = t.childCount;
        for(int i = children - 1; i >=0; i--)
        {
            Destroy(t.GetChild(i).gameObject);
        }
    }

}
