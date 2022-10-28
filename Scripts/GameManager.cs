using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hazardPrefab;
    public int maxHarzardsToSpawn = 3;
    public GameObject mainVCam;
    public GameObject zoomVCam;

    public GameObject player;
    private Coroutine hazardsCoroutine;
    public TMPro.TextMeshProUGUI scoreText;
    public Image backgroundMenu;
    public GameObject GameOverMenu;
    private int score;
    private float timer;
    private int highScore;
    private bool gameOver;
    private static GameManager instance;
    private const string HighScorePreferenceKey = "HighScore";
    public static GameManager Instance => instance;
    public int HighScore => highScore;


    void Start()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt(HighScorePreferenceKey);
    }

    private void OnEnable()
    {
        player.SetActive(true);
        zoomVCam.SetActive(false);
        mainVCam.SetActive(true);
        gameOver = false;
        score = 0;
        timer = 0;
        scoreText.text = "0";
        hazardsCoroutine = StartCoroutine(SpawnHazards());
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                StartCoroutine(ScaleTime(0, 1, 0.75f));
                backgroundMenu.gameObject.SetActive(false);
            }
            if(Time.timeScale == 1)
            {
                StartCoroutine(ScaleTime(1, 0, 0.75f));
                backgroundMenu.gameObject.SetActive(true);
            }
        }

        if (gameOver)
            return;

        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            score++;
            scoreText.text = score.ToString();
            timer = 0;

        }
    }

    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastime = Time.realtimeSinceStartup;
        float timer = 0.0f;
        while(timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            timer += (Time.realtimeSinceStartup - lastime);
            lastime = Time.realtimeSinceStartup;
            yield return null;
        }
        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    // Update is called once per frame
    private IEnumerator SpawnHazards()
    {
        var hazardToSpawn = Random.Range(1, maxHarzardsToSpawn);

        for (int i = 0; i < hazardToSpawn; i++)
        {
            var x = Random.Range(-7, 7);
            var hazard = Instantiate(hazardPrefab, new Vector3(x, 13, 0), Quaternion.identity);
            var drag = Random.Range(0f, 2f);
            hazard.GetComponent<Rigidbody>().drag = drag;
        }

        yield return new WaitForSeconds(1f);
        yield return SpawnHazards();

    }


    public void GameOver()
    {


        StopCoroutine(hazardsCoroutine);
        gameOver = true;
        mainVCam.SetActive(false);
        zoomVCam.SetActive(true);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighScorePreferenceKey, highScore);
        }

        gameObject.SetActive(false);
        GameOverMenu.SetActive(true);


    }

    public void Enable()
    {

        gameObject.SetActive(true);
    }
}
