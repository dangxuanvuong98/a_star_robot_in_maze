using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : MonoBehaviour {

    [Header("Main Menu")]
    public Button settingButton;
    public Button startButton;
    public Button mazeGenButton;
    public Button exitButton;
    public Text invalidWarningInMenu;

    [Space]
    [Header("Setting")]
    public InputField directoryInput;
    public InputField movementInput;
    public Button backButton;
    public Text invalidWarning;

    public string inputPath;
    public string movementPath;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        inputPath = PlayerPrefs.GetString("Input Directory");
        movementPath = PlayerPrefs.GetString("Movement Directory");
        PlayerPrefs.SetFloat("AI Speed", 10);
        directoryInput.text = inputPath;
        movementInput.text = movementPath;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setting()
    {
        directoryInput.gameObject.SetActive(true);
        movementInput.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

        invalidWarningInMenu.gameObject.SetActive(false);
        settingButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        mazeGenButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);  
    }

    public void backSetting()
    {
        settingButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        mazeGenButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        directoryInput.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        movementInput.gameObject.SetActive(false);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void start()
    {
        try
        {
            StreamReader reader1 = new StreamReader(inputPath);
            StreamReader reader2 = new StreamReader(movementPath);
        }
        catch
        {
            invalidWarningInMenu.gameObject.SetActive(true);
            Debug.Log("Cant find file");
            return;
        }
        SceneManager.LoadScene(1);
    }

    public void getDirectory()
    {
        inputPath = directoryInput.text;
        inputPath += "\\input.txt";
        try
        {
            StreamReader reader = new StreamReader(inputPath);
        }
        catch 
        {
            invalidWarning.gameObject.SetActive(true);
            backButton.gameObject.SetActive(false);
            Debug.Log("Cant find file");
            return;
        }
        invalidWarning.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        PlayerPrefs.SetString("Input Directory", inputPath);
        directoryInput.text = inputPath;
    }

    public void getInput()
    {
        movementPath = movementInput.text;
        movementPath += "\\movement.txt";
        try
        {
            StreamReader reader = new StreamReader(movementPath);
        }
        catch
        {
            invalidWarning.gameObject.SetActive(true);
            backButton.gameObject.SetActive(false);
            Debug.Log("Cant find file");
            return;
        }
        invalidWarning.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        PlayerPrefs.SetString("Movement Directory", movementPath);
        movementInput.text = movementPath;
    }

    public void createButton()
    {
        SceneManager.LoadScene(2);
    }
}
