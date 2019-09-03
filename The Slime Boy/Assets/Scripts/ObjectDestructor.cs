using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestructor : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan;


    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }
}
