using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float shotTime; // the time player waits before the next shot

    [SerializeField]
    private Projectile projectile; ///TODO: It should be GameObject so it would be possible to add different Projectile classes or a parent projectile class/interface

    [SerializeField]
    [Range(1, 7)]
    private int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }


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
            //Instantiate(projectile, shotPoint.position, transform.rotation).Angle = new Vector3(0, 0, -20);
            //int projectileCount = 2 * level - 1;

            Instantiate(projectile, shotPoint.position, transform.rotation); // forward projectile (middle one)
            for (int i = 0; i < level - 1; i++)
            {
                int zAngle = level > 3 ? (10 - level) * (i + 1) : (13 - level) * (i + 1);
                Instantiate(projectile, shotPoint.position, transform.rotation).transform.Rotate(new Vector3(0, 0, -zAngle)); // projectile below
                Instantiate(projectile, shotPoint.position, transform.rotation).transform.Rotate(new Vector3(0, 0, zAngle)); // mirror projectile above
            }

            shotTime = Time.time + timeBetweenShots;
        }
    }
}
