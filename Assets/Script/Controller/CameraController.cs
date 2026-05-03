using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 nextPosition = new Vector3(
            target.position.x+3,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            nextPosition,
            followSpeed * Time.deltaTime
        );
    }
}