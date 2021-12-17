using UnityEngine;

public class MovementData
{
    Vector2[] directionalInputRecorder = new Vector2[Constants.REC_LEN];
    Vector2[] positionRecorder = new Vector2[Constants.REC_LEN];
    Vector2[] velocityRecorder = new Vector2[Constants.REC_LEN];
    
    // Getters of single values
    public Vector2 GetInput(ushort index)
    {
        if(!IsIndexInRange(index)) return new Vector2();
        return directionalInputRecorder[index];
    }

    public Vector2 GetPosition(ushort index){
        if(!IsIndexInRange(index)) return new Vector2();
        return positionRecorder[index];
    }

    public Vector2 GetVelocity(ushort index){
        if(!IsIndexInRange(index)) return new Vector2();
        return velocityRecorder[index];
    }



    // Setters of part
    public void SetInputPart(Vector2[] inputList, ushort startIndex){
        if (IsIndexInRange((ushort) (startIndex + inputList.Length)) && IsIndexInRange(startIndex)) 
            SetPart(inputList, startIndex, directionalInputRecorder);
    }

    public void SetPositionPart(Vector2[] inputList, ushort startIndex){
        if (IsIndexInRange((ushort) (startIndex + inputList.Length)) && IsIndexInRange(startIndex)) 
            SetPart(inputList, startIndex, positionRecorder);
    }

    public void SetVelocityPart(Vector2[] inputList, ushort startIndex){
        if (IsIndexInRange((ushort) (startIndex + inputList.Length)) && IsIndexInRange(startIndex)) 
            SetPart(inputList, startIndex, velocityRecorder);
    }



    // Setters of whole recorder
    private void SetInput(Vector2[] inputList){
        if (IsIndexInRange((ushort) inputList.Length)) 
            SetRecorder(inputList, directionalInputRecorder);
    }

    private void SetPosition(Vector2[] inputList){
        if (IsIndexInRange((ushort) inputList.Length)) 
            SetRecorder(inputList, positionRecorder);
    }

    private void SetVelocity(Vector2[] inputList){
        if (IsIndexInRange((ushort) inputList.Length)) 
            SetRecorder(inputList, velocityRecorder);
    }
    


    // Setter of whole any
    private void SetRecorder(Vector2[] inputList, Vector2[] recorder){
        recorder = inputList;
    }

    // Setter of single any
    private void SetSingle(Vector2 inputVector, ushort index, Vector2[] recorder){
        recorder[index] = inputVector;
    }
    
    // Setter of part any
    private void SetPart(Vector2[] inputList, ushort startIndex, Vector2[] recorder){
        for(int i = 0; i < inputList.Length; i++)
        {
            SetSingle(inputList[i], (ushort) (i + startIndex), recorder);
        }
    }



    // Checkers
    private bool IsIndexInRange(ushort index){
        return !(index >= Constants.REC_LEN || index < 0);
    }
}
