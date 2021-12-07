using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement = new PlayerMovement();
    private Rigidbody playerRB;

    public float speed = 10;
    private float diagVel = Mathf.Sqrt(0.5f);

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        UpdateMovement();
    }

    private void UpdateMovement(){
        //  Inputs of the player in planar directions X and Z
        Vector2 directionalInputs = new Vector2(0, 0);

        // Keyboard input logic
        // TODO: EDIT SO EITHER KEYBOARD INPUT OR CONTROLLER INPUT IS PRIORITIZED OVER THE OTHER
        string playerKeyboardInput = "";
        
        if (Input.GetKey(KeyCode.W)) playerKeyboardInput += "w";
        if (Input.GetKey(KeyCode.S)) playerKeyboardInput += "s";
        if (Input.GetKey(KeyCode.A)) playerKeyboardInput += "a";
        if (Input.GetKey(KeyCode.D)) playerKeyboardInput += "d";

        playerKeyboardInput = StripMovementInput(playerKeyboardInput);
        directionalInputs = TranslateKeyboardInputs(playerKeyboardInput, directionalInputs);
        
        // End of keyboard input logic

        // TODO: Controller input logic here
        // End of controller input logic

        if (IsInput(directionalInputs)) playerMovement.Move(directionalInputs, playerRB, speed);
    }

    private bool IsInput(Vector2 input){
        return !(input[0] == 0 && input[1] == 0);
    }

    private string StripMovementInput(string input){
        if (input.Contains("w") && input.Contains("s")) {
            input = input.Replace("w", "");
            input = input.Replace("s", "");
        }
        if (input.Contains("a") && input.Contains("d")) {
            input = input.Replace("a", "");
            input = input.Replace("d", "");
        }
        return input;
    }

    private Vector2 TranslateKeyboardInputs(string inputs, Vector2 directionalInputs) {
        switch (inputs)
        {
            case "wa":
                directionalInputs[0] = -diagVel;
                directionalInputs[1] = diagVel;
                Debug.Log("move");
                break;
            case "wd":
                directionalInputs[0] = diagVel;
                directionalInputs[1] = diagVel;
                Debug.Log("move");
                break;
            case "sa":
                directionalInputs[0] = -diagVel;
                directionalInputs[1] = -diagVel;
                Debug.Log("move");
                break;
            case "sd":
                directionalInputs[0] = diagVel;
                directionalInputs[1] = -diagVel;
                Debug.Log("move");
                break;
            case "w":
                directionalInputs[0] = 0;
                directionalInputs[1] = 1;
                Debug.Log("move");
                break;
            case "a":
                directionalInputs[0] = -1;
                directionalInputs[1] = 0;
                Debug.Log("move");
                break;
            case "s":
                directionalInputs[0] = 0;
                directionalInputs[1] = -1;
                Debug.Log("move");
                break;
            case "d":
                directionalInputs[0] = 1;
                directionalInputs[1] = 0;
                Debug.Log("move");
                break;
            default:
                break;
        }
        return directionalInputs;
    }

}
