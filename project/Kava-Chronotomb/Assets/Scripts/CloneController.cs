using UnityEngine;

public class CloneController : MonoBehaviour
{
    // Has an attatched data class that it gets data from and does the movement for the gameobject this script is attatched to.
    // Needs an own instance of PlayerMovement to control.
    // This class will need to be able to play the given timeline from index and towards Constants.REC_LEN.
    // When done pls stop without errors.

    // EVERY CLONE HAS THEIR OWN CLONE CONTROLLER
    private PlayerController playerController;

    private Rigidbody cloneRB;
    private ushort startFrame = 0;
    public float speed = 10f;
    private PlayerMovement playerMovement = new PlayerMovement();
    private bool controllerBoolean = false;

    private MovementData movementData = new MovementData();
    public InputRecorder recorder = new InputRecorder();
    
    private void Start(){
        cloneRB = GetComponentInChildren<Rigidbody>();
        playerController = FindObjectOfType<PlayerController>();
    }
    
    private void FixedUpdate() {
        if(controllerBoolean){
            MoveClone();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Red") {
            playerController.ToggleTimeSplit();
        }
    }

    private void OnTriggerExit(Collider other) {
        Physics.IgnoreCollision(other.gameObject.GetComponentInChildren<Collider>(), GetComponentInChildren<Collider>(), false);
    }

    public void StartCloneMovement(ushort frame){
        controllerBoolean = true;
        startFrame = frame;
    }

    public void EndCloneMovement(){
        controllerBoolean = false;
    }

    private void MoveClone(){
        playerMovement.Move(movementData.GetInput(startFrame), cloneRB, speed);
        startFrame++;
        if(startFrame >= Constants.REC_LEN){
            EndCloneMovement();
        }
    }

    public void SaveRecordings(Vector2[] movementRecord, ushort startIndex) {
        movementData.SetInputPart(movementRecord, startIndex);
    }
}
