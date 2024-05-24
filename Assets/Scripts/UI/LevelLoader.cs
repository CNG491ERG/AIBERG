using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionAnimator;
    public float transitionTime = 1f;
    public GameObject loadingScreen;
    public static LevelLoader Instance;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadScene(int buildIndex)
    {
        StartCoroutine(Transition(buildIndex));
    }

    IEnumerator Transition(int buildIndex)
    {
        if(!transitionAnimator.gameObject.activeInHierarchy){
            transitionAnimator.gameObject.SetActive(true);
        }
        
        // Start playing the transition animation
        transitionAnimator.SetTrigger("Start");

        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(buildIndex);
        asyncOperation.allowSceneActivation = false;

        // Wait for the transition animation to finish
        yield return new WaitForSeconds(transitionTime);

        // Ensure the animation has finished before proceeding
        while (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // Wait until the asynchronous scene loading is almost complete
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        // Allow the scene to activate
        asyncOperation.allowSceneActivation = true;

        // Hide the loading screen
        transitionAnimator.SetTrigger("Stop");
        yield return new WaitForSeconds(transitionTime/4);
    }
}