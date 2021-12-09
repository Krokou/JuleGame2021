using UnityEngine;

public class PlayerMovement
{   
    //  Function that moves the given rigid body on the XZ-plane with a factor of Z from directional vector ipnut.
    public void Move(Vector2 input, Rigidbody playerRB, float speed){
        // Logic for handling input. 
        if (IsInput(input)) {
            playerRB.velocity = new Vector3(input[0] * speed, playerRB.velocity.y, input[1] * speed);
        }
        // TODO: Potentially handle partial 0 inputs differently for smoother turns instead of setting velocity to 0.
        // This could also be done by having velocity SLERP or LERP towards the input-balues instead of being directly set. Math stuff
    }

    private bool IsInput(Vector2 input){
        return !(input[0] == 0 && input[1] == 0);
    }
}
