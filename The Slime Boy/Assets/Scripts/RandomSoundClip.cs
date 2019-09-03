using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundClip : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> sounds;


    // Start is called before the first frame update
    void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        if (sounds.Count == 1)
        {
            audioSource.pitch = Random.value + 1;
        }
        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Count)]);
    }
}
