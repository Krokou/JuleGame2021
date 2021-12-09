using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Target transform must be set at some point.

    public Transform targetTransform;
    public float zOffset = -8;
    public float smoothTime = 0.3f;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {   
        targetTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        SetTarget(targetTransform);
    }
    
    private void FixedUpdate() {
        FollowTarget();
    }

    private void FollowTarget(){
        targetPosition = new Vector3(targetTransform.position.x, 6, targetTransform.position.z + zOffset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void SetTarget(Transform target){
        targetTransform = target;
        transform.position = new Vector3(targetTransform.position.x, 6, targetTransform.position.z + zOffset);
        transform.LookAt(targetTransform);
    }

    public Transform GetTarget(){
        return targetTransform;
    }
}
