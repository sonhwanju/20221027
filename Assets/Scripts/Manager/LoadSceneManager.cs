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

    [Header("LoadScene")]
    [SerializeField]
    private CanvasGroup loadCg;
    [SerializeField]
    private Image progress;
    [SerializeField]
    private TextMeshProUGUI progressText;

    private string loadSceneName = string.Empty;

    [Header("Loading")]
    [SerializeField]
    private CanvasGroup loadingCg;
    [SerializeField]
    private TextMeshProUGUI loadingProgressText;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        UtilClass.SetCanvasGroup(loadingCg, false);
        UtilClass.SetCanvasGroup(loadCg, false);
    }

    public IEnumerator LoadSceneCoroutine(string sceneName, bool isAdditive =false)
    {
        UtilClass.SetCanvasGroup(loadCg, true);

        if (!sceneName.Equals("BetweenScene"))
        {
            yield return StartCoroutine(Load("BetweenScene"));
        }

        LoadScene(sceneName, isAdditive);
        yield return null;
    }

    public void LoadScene(string sceneName, bool isAdditive = false)
    {
        UtilClass.SetCanvasGroup(loadCg, true);

        if(!isAdditive)
            SceneManager.sceneLoaded += EndLoadScene;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName, isAdditive));
    }

    private void EndLoadScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name.Equals(loadSceneName))
        {
            StartCoroutine(UtilClass.FadeCanvasGroup(loadCg, 0.8f, false));
            SoundManager.Instance.SetBgm(scene.buildIndex);
            SceneManager.sceneLoaded -= EndLoadScene;
        }
    }

    private IEnumerator Load(string sceneName, bool isAdditive = false)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        float timer = 0f;
        int percent = 0;

        progress.fillAmount = 0f;
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

    private IEnumerator UnLoad(string sceneName)
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(sceneName);

        op.allowSceneActivation = true;

        while (!op.isDone)
        {
            yield return null;
        }
    }

    #region loading

    public void StartLoading()
    {
        UtilClass.SetCanvasGroup(loadingCg, true);
    }

    public void EndLoading()
    {
        UtilClass.SetCanvasGroup(loadingCg, false);
    }

    public void SetLoadingText(float progress)
    {
        loadingProgressText.text = $"{Mathf.RoundToInt(progress)}%";
    }

    #endregion
}
