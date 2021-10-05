using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform cameraTarget;
    public Transform lookTarget;
    public bool isAssigned = false, u, fu, lu, dt;

    public float sSpeed = 10.0f;
    public Vector3 dist;
    float timeDeltaTime;

    void FixedUpdate()
    {
        if (isAssigned && fu) 
        {
            Vector3 dPos = cameraTarget.position + dist;
            Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * timeDeltaTime);
            transform.position = sPos;
            transform.LookAt(lookTarget.position);
        }
    }

    private void LateUpdate()
    {
        if (isAssigned && lu) 
        {
            Vector3 dPos = cameraTarget.position + dist;
            Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * timeDeltaTime);
            transform.position = sPos;
            transform.LookAt(lookTarget.position);
        }
    }
    private void Update()
    {
        if (dt)
        {
            timeDeltaTime = Time.deltaTime;
        }
        else
        {
            timeDeltaTime = 1;
        }
        if (isAssigned && u) 
        {
            Vector3 dPos = cameraTarget.position + dist;
            Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * timeDeltaTime);
            transform.position = sPos;
            transform.LookAt(lookTarget.position);
        }
    }
    public void Assign(bool val)
    {
        isAssigned = val;
    }

    public void SetTargets(Transform _camTarget, Transform _lookTarget)
    {
        cameraTarget = _camTarget;
        lookTarget = _lookTarget;
    }
}
