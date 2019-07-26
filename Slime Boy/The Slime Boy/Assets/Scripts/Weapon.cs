using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float shotTime; // the time player waits before the next shot

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    public Transform shotPoint;

    [SerializeField]
    float timeBetweenShots; // time between shots in seconds


    // Update is called once per frame
    private void Update()
    {
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0) && Time.time >= shotTime) //LMB
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            shotTime = Time.time + timeBetweenShots;
        }
    }
}
