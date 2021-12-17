using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorderHandler
{
    private List<Vector2> inputs = new List<Vector2>();

    // Records the input
    public void RecordInput(Vector2 input) {
        inputs.Add(input);
    }

    // Resets recording of inputs
    public void ResetRecording() {
        inputs = new List<Vector2>();
    }

    // Converts List of input to array of inputs
    // Then it returns this array of inputs
    public Vector2[] GetRecording() {
        Vector2[] tmpInputs = new Vector2[inputs.Count];
        
        for (int i = 0; i < inputs.Count; i++) tmpInputs[i] = inputs[i];
        
        return tmpInputs;
    }
}
