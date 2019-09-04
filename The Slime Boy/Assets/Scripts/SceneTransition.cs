using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition());
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(Transition());
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void Quit()
    {
        StartCoroutine(Transition());
        Application.Quit();
    }

    private IEnumerator Transition()
    {
        animator.SetTrigger("end");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
}
