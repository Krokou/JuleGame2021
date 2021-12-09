using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CameraMovement mainCamera, secondaryCamera;
    
    private Transform mainTarget, blueTarget, redTarget;

    public GameObject secondaryCameraPrefab;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.gameObject.GetComponent<CameraMovement>();

        mainTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ToggleCameraTimeSplit(){
        if (mainCamera.GetTarget().Equals(mainTarget)){
            CreateSecondaryCamera();
            mainCamera.SetTarget(blueTarget);
            Debug.Log("Starting");
        }
        else{
            mainCamera.SetTarget(mainTarget);
            secondaryCamera.SetTarget(redTarget);
            DestroySecondaryCamera();
            Debug.Log("Ending");
        }
    }

    public void SwitchTimelineCameraView(){
        if (mainCamera.GetTarget().Equals(blueTarget)){
            mainCamera.SetTarget(redTarget);
            secondaryCamera.SetTarget(blueTarget);
        }
        else if (mainCamera.GetTarget().Equals(redTarget)){
            mainCamera.SetTarget(blueTarget);
            secondaryCamera.SetTarget(redTarget);
        }
    }

    public void CreateSecondaryCamera() {
        Debug.Log("Create camera");
        secondaryCamera = Instantiate(secondaryCameraPrefab, new Vector3(), Quaternion.identity).GetComponent<CameraMovement>();
        blueTarget = GameObject.FindGameObjectWithTag("Blue").transform;
        redTarget = GameObject.FindGameObjectWithTag("Red").transform;
        secondaryCamera.SetTarget(redTarget);
    }

    public void DestroySecondaryCamera() {
        blueTarget = null;
        redTarget = null;
        Destroy(secondaryCamera.gameObject);
    }
}
