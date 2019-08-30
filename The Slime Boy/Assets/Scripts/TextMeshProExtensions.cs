using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TextMeshProExtensions
{
    public static IEnumerator ChangeText(this TextMeshProUGUI textbox, string text)
    {
        var animator = textbox.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Idle");
        textbox.text = text;
        Debug.Log(textbox.text);
    }
}
