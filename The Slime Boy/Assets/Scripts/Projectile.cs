using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float lifeSpan;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private int damage;

    //public Vector3 Angle { get; set; }


    // Start is called before the first frame update
    private void Start()
    {
        Invoke("DestroyProjectile", lifeSpan);
        //transform.Rotate(Angle);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) ///TODO: this could lead to execution of the TakeDamage method more than once if more than 1 projectile hit an enemy at the same time
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().DealDamage(damage);
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().DealDamage(damage);
        }


        DestroyProjectile();

    }

    private void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
