using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float dampTime = 0.2f;
    public float screenEdgeBuffer = 4f;
    public float minSize = 6.5f;
    public Transform[] targets;

    private Camera cam;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPosition;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        findAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
    }

    private void findAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for(int i = 0; i < targets.Length; i++)
        {
            if (targets[i].gameObject.activeSelf)
                continue;

            averagePos += targets[i].position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.y = transform.position.y;

        desiredPosition = averagePos;
    }

    private void Zoom()
    {
        float requiredSize = findRequiredSize();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
    }

    private float findRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);

        float size = 0f;

        for(int i = 0; i < targets.Length; i++)
        {
            if (targets[i].gameObject.activeSelf)
                continue;
            Vector3 targetLocalPos = transform.InverseTransformPoint(targets[i].position);
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / cam.aspect);
        }

        size += screenEdgeBuffer;
        size = Mathf.Max(size, minSize);
        return size;
    }

    public void setStartPositionAndSize()
    {
        findAveragePosition();
        transform.position = desiredPosition;
        cam.orthographicSize = findRequiredSize();
    }

}
