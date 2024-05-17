using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        Vector3 desiredPos = player.TransformPoint(offset);

        Vector3 smoothPos = Vector3.Slerp(
            transform.position,
            desiredPos,
            speed * Time.deltaTime
        );

        transform.position = smoothPos;

        transform.LookAt(player);
    }
}
