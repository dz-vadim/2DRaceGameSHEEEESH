using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject target;
    void Update()
    {
        Vector3 targetPos = target.transform.position;
        targetPos.z = transform.position.z;
        transform.position = 
            Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
    }
}
