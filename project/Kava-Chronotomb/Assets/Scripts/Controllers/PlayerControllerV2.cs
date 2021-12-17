using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// // The player controller's task is to manage player inputs and control the player accordingly using various controller classes. 
// The class should only manage communication and communication translation between the components, 
// as well as keeping track of what the different actions are allowed to happen at any given state, using the player state machine rules.
// RULE 1: This script should only tell other scripts to do stuff, meaning no actions or gameobject handling in the game.
// RULE 2: This script will track all inputs related to player gameplay, and give commands accordingly to the relevant behaviour/handler scripts.

public class PlayerControllerV2 : MonoBehaviour 
{
    // Controllers
    private CameraController cameraController;
    
    // State machine
    private PlayerStateMachine stateMachine = new PlayerStateMachine();

    // Handlers
    private TimeSplitHandler timeSplitHandler = new TimeSplitHandler();
    private PlayerMovementHandler playerMovementHandler = new PlayerMovementHandler();
    
    // Default state values
    private Constants.States currentState = Constants.States.MAIN;
    private Constants.SubStates currentSubState = Constants.SubStates.IDLE;
    
    // State machine variables
    private bool canDash;
    private bool canJump;
    private bool canMove;
    private bool isInvulnerable;
    // etc

    private void Update() {
        
    }

    private void FixedUpdate() {
        
    }

    
}