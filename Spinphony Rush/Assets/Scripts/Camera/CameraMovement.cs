using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject[] phonies;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime = .5f;
    public float cameraMovement;
    void LateUpdate()
    {
        phonies = GameObject.FindGameObjectsWithTag("Phony");
        if (phonies.Length == 0)
        {
            return;
        }
        
        Vector3 centerPoint = calculatePhoniesCenter();
        Vector3 newPos = (centerPoint * cameraMovement) + offset;
        newPos[1] = offset[1];
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }

    private Vector3 calculatePhoniesCenter() {
        if(phonies.Length == 1)
        {
            return phonies[0].GetComponentInChildren<Rigidbody>().transform.position;
        }
        Bounds bounds = new Bounds(phonies[0].GetComponentInChildren<Rigidbody>().transform.position, Vector3.zero);
        for(int i = 0; i < phonies.Length; i++)
        {
            bounds.Encapsulate(phonies[i].GetComponentInChildren<Rigidbody>().transform.position);
        }
        return bounds.center;
    }
        
}
