using UnityEngine;

public class AssignCamera : MonoBehaviour
{
    public Transform[] looAtTarget, cameraTarget;


    private void Start()
    {
        SetCameraTargets();
    }
    void SetCameraTargets()
    {
        switch (GameManager.gameManager.currentPreset)
        {
            case 0:
                {
                    GameManager.gameManager.SetCameraTargets(cameraTarget[0], looAtTarget[0]);
                    break;
                }
            case 1:
                {
                    GameManager.gameManager.SetCameraTargets(cameraTarget[1], looAtTarget[1]);
                    break;
                }
            case 2:
                {
                    GameManager.gameManager.SetCameraTargets(cameraTarget[2], looAtTarget[2]);
                    break;
                }
            default:
                {
                    GameManager.gameManager.SetCameraTargets(cameraTarget[0], looAtTarget[0]);
                    break;
                }
        }

    }
}
