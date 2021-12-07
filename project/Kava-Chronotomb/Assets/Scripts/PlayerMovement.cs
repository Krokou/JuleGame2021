using UnityEngine;

public class PlayerMovement
{   
    //  Function that moves the player on the XZ-plane
    //  using directional input values in a vector 2 format
    public void Move(Vector2 input, Rigidbody playerRB, float speed){
        // Logic for handling input. 
        // TODO: Potentially handle partial 0 inputs differently for smoother turns instead of setting velocity to 0.
        // This could also be done by having velocity SLERP or LERP towards the input-balues instead of being directly set. Math stuff
        playerRB.velocity = new Vector3(input[0] * speed, playerRB.velocity.y, input[1] * speed);
    }
}
