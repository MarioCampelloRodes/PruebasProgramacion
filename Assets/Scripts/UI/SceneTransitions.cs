using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private CanvasGroup fadeScreen;

    public void FadeIn()
    {
        fadeScreen.alpha = 0f;
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.LeanAlpha(1f, 1f).setDelay(0.5f);
        LeanTween.delayedCall(2f, FadeOut);
    }

    public void FadeOut()
    {
        fadeScreen.alpha = 1f;
        fadeScreen.LeanAlpha(0f, 1f).setOnComplete(() => fadeScreen.gameObject.SetActive(false));
    }
}
