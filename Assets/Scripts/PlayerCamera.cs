using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float panSpeed = 10.0f;
    public Vector3 offset;
    public Transform player;
    public Vector3 minValue, maxValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camPosition = player.TransformPoint(offset);

        Vector3 clampPosition = new Vector3(
            Mathf.Clamp(camPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(camPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(camPosition.z, minValue.z, maxValue.z)
        );

        Vector3 smoothPosition = Vector3.Lerp(transform.position, clampPosition, panSpeed * Time.deltaTime);
        transform.position = smoothPosition;
        transform.LookAt(player);
    }
}
