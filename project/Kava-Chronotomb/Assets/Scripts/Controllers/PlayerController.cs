using UnityEngine;

/// 
public class PlayerController : MonoBehaviour
{
    private PlayerMovementHandler playerMovementHandler = new PlayerMovementHandler();
    private Rigidbody playerRB, controllingRB;
    private CameraController cameraController;
    private GameObject currentPlayer, mainPlayer, bluePlayer, redPlayer, childBody;
    private CloneController redClone, blueClone, currentClone;

    // Clone prefab references
    public GameObject redPrefab, bluePrefab;

    public bool isTimesplitting = false;

    public float speed = 10;
    private float diagonalVelocity = Mathf.Sqrt(0.5f);

    private ushort timelineIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        childBody = GameObject.Find("PlayerBody");
        controllingRB = GetComponentInChildren<Rigidbody>();
        cameraController = Camera.main.GetComponent<CameraController>();
        
        currentPlayer = gameObject;
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        
        /*
        bluePlayer = GameObject.FindGameObjectWithTag("Blue");
        redPlayer = GameObject.FindGameObjectWithTag("Red");

        redClone = redPlayer.GetComponent<CloneController>();
        blueClone = bluePlayer.GetComponent<CloneController>();
        */
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            ToggleTimeSplit();
        }
        if (Input.GetKeyDown(KeyCode.V) && isTimesplitting) {
            SwitchTimeline();
        }
    }

    private void FixedUpdate() {
        UpdateMovement();
    }


    // Toggles the state of being in a time split, and calls all necessary game logic
    public void ToggleTimeSplit() {
        if (currentPlayer.Equals(mainPlayer)) {
            playerRB.isKinematic = true;
            childBody.SetActive(false);
            // Create clones and set references to them
            CreateClones();
            currentPlayer = bluePlayer;
            isTimesplitting = true;
            currentClone = currentPlayer.GetComponent<CloneController>();
        }
        else {
            currentPlayer = mainPlayer;
            isTimesplitting = false;
            redClone.EndCloneMovement();
            blueClone.EndCloneMovement();
            currentClone = null;
            DestroyClones();
            childBody.SetActive(true);
            playerRB.isKinematic = false;
            timelineIndex = 0;
        }
        SetActivePlayer(currentPlayer);
        cameraController.ToggleCameraTimeSplit();
    }

    // Toggles between playing as the different time-split players
    private void SwitchTimeline(ushort timeIndex = 0) {
        timelineIndex = timeIndex;
        if (currentPlayer.Equals(bluePlayer)) {
            currentPlayer = redPlayer;
            blueClone.StartCloneMovement(timeIndex);
            redClone.EndCloneMovement();
        }
        else {
            currentPlayer = bluePlayer;
            redClone.StartCloneMovement(timeIndex);
            blueClone.EndCloneMovement();
        }
        SetActivePlayer(currentPlayer);
        cameraController.SwitchTimelineCameraView();

        currentClone.SaveRecordings(currentClone.recorder.GetRecording(), timeIndex);
        currentClone = currentPlayer.GetComponent<CloneController>();
        currentClone.recorder.ResetRecording();

    }

    // Sets the active player to the given player game object
    private void SetActivePlayer(GameObject player) {
        SetPlayerRigidBody(player.GetComponentInChildren<Rigidbody>());
        // TODO: Other things might happen here
    }

    private void UpdateMovement(){
        //  Data holder for vector inputs of the player in planar directions X and Z
        Vector2 directionalInputs = new Vector2(0, 0);

        // Keyboard input logic
        // TODO: EDIT SO EITHER KEYBOARD INPUT OR CONTROLLER INPUT IS PRIORITIZED OVER THE OTHER

        // Keyboard input data holder
        string playerKeyboardInput = "";
        
        // Get the actual inputs and store them
        if (Input.GetKey(KeyCode.W)) playerKeyboardInput += "w";
        if (Input.GetKey(KeyCode.S)) playerKeyboardInput += "s";
        if (Input.GetKey(KeyCode.A)) playerKeyboardInput += "a";
        if (Input.GetKey(KeyCode.D)) playerKeyboardInput += "d";

        // Clean up the string input to exclude oposites
        playerKeyboardInput = StripKeyboardInput(playerKeyboardInput);

        // Convert keyboard inputs to direction vector
        directionalInputs = KeyboardInputsToVector(playerKeyboardInput, directionalInputs);
        
        // End of keyboard input logic

        // TODO: Controller input logic here
        // End of controller input logic
        
        // Check
        playerMovementHandler.Move(directionalInputs, controllingRB, speed);
        
        // Record inputs
        if (currentClone != null) {
            currentClone.recorder.RecordInput(directionalInputs);
            timelineIndex++;
            if (timelineIndex >= Constants.REC_LEN) ToggleTimeSplit();
        }
    }

    // Removes oposite inputs from the total input string
    private string StripKeyboardInput(string input){
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

    // Translates from char inputs to directional vector
    private Vector2 KeyboardInputsToVector(string inputs, Vector2 directionalInputs) {
        switch (inputs)
        {
            case "wa":
                directionalInputs[0] = -diagonalVelocity;
                directionalInputs[1] = diagonalVelocity;     
                break;
            case "wd":
                directionalInputs[0] = diagonalVelocity;
                directionalInputs[1] = diagonalVelocity;
                break;
            case "sa":
                directionalInputs[0] = -diagonalVelocity;
                directionalInputs[1] = -diagonalVelocity;
                break;
            case "sd":
                directionalInputs[0] = diagonalVelocity;
                directionalInputs[1] = -diagonalVelocity;
                break;
            case "w":
                directionalInputs[0] = 0;
                directionalInputs[1] = 1;
                break;
            case "a":
                directionalInputs[0] = -1;
                directionalInputs[1] = 0;
                break;
            case "s":
                directionalInputs[0] = 0;
                directionalInputs[1] = -1;
                break;
            case "d":
                directionalInputs[0] = 1;
                directionalInputs[1] = 0;
                break;
            default:
                break;
        }
        return directionalInputs;
    }
    
    // Changes current target rigid body between main, red and blue
    public void SetPlayerRigidBody(Rigidbody rb){
        controllingRB = rb;
    }

    private void CreateClones() {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        bluePlayer = Instantiate(bluePrefab, position, rotation);
        redPlayer = Instantiate(redPrefab, position, rotation);

        redClone = redPlayer.GetComponent<CloneController>();
        blueClone = bluePlayer.GetComponent<CloneController>();

        Physics.IgnoreCollision(bluePlayer.GetComponentInChildren<Collider>(), redPlayer.GetComponentInChildren<Collider>(), true);
    }

    private void DestroyClones(){
        redClone = null;
        blueClone = null;

        Destroy(bluePlayer);
        Destroy(redPlayer);
    }
}
