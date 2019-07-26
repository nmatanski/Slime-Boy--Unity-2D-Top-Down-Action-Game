using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float ghostDelaySeconds;

    [SerializeField]
    private float ghostDelay;

    [SerializeField]
    private GameObject ghost;

    [SerializeField]
    private bool isMakingGhost = false;
    public bool IsMakingGhost
    {
        get { return isMakingGhost; }
        set { isMakingGhost = value; }
    }

    public Vector3 EnemyStartPosition { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMakingGhost)
        {
            return;
        }
        if (ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.deltaTime;
        }
        else
        {
            //Generate a ghost
            var currentGhost = Instantiate(ghost, transform.position + new Vector3(-8f, 0, 0), transform.rotation);
            currentGhost.transform.localScale = transform.localScale;
            ghostDelaySeconds = ghostDelay;
            Destroy(currentGhost, 0.5f);
        }
    }
}
