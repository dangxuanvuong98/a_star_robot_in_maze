using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        ReadString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    static string ReadString()
    {
        string path = "F:/testing.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string temp = reader.ReadToEnd().ToString();
        reader.Close();
        return temp;
    }
}
