using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    private static LoadSceneManager instance;
    public static LoadSceneManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<LoadSceneManager>();

                if (obj == null)
                {
                    instance = UtilClass.CreateObj<LoadSceneManager>("LoadSceneManager");
                }
                else
                {
                    instance = obj;
                }
            }
            return instance;
        }

        set => instance = value;
    }

    [SerializeField]
    private CanvasGroup loadCg;
    [SerializeField]
    private Image progress;
    [SerializeField]
    private TextMeshProUGUI progressText;

    private string loadSceneName = string.Empty;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += EndLoadScene;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }

    private void EndLoadScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name.Equals(loadSceneName))
        {
            StartCoroutine(UtilClass.FadeCanvasGroup(loadCg, 0.8f, false));
            SceneManager.sceneLoaded -= EndLoadScene;
        }
    }

    private IEnumerator Load(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        float timer = 0f;
        int percent = 0;

        progress.fillAmount = 0f;
        UtilClass.SetCanvasGroup(loadCg, true);

        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if(op.progress < 0.9f)
            {
                progress.fillAmount = Mathf.Lerp(progress.fillAmount, op.progress, timer);
                if(progress.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progress.fillAmount = Mathf.Lerp(progress.fillAmount, 1f, timer);

                if (progress.fillAmount.Equals(1f))
                {
                    percent = 100;
                    progressText.text = $"{percent}%";
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
            percent = Mathf.RoundToInt((progress.fillAmount * 100));
            progressText.text = $"{percent}%";
        }
    }
}