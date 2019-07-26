using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float minX;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float minY;

    [SerializeField]
    private float maxY;


    // Start is called before the first frame update
    private void Start()
    {
        transform.position = playerTransform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform != null)
        {
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);
            ///
            //var rotationHelper = new GameObject();
            //rotationHelper.transform.position = transform.position;
            //rotationHelper.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotationHelper.transform.rotation, speed);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, 0), speed);
            ///
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}
