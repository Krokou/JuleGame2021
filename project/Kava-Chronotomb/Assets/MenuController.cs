using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button startButton, exitButton;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("start-button");
        exitButton = root.Q<Button>("exit-button");

        startButton.clicked += StartButtonPress;
        exitButton.clicked += ExitButtonPress;
    }

    void StartButtonPress()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ExitButtonPress()
    {
        Debug.Log("Qitting game");
        Application.Quit();
    }
}
