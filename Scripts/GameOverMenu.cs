using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private LTDescr restartanimation;

    [SerializeField]
    private TMPro.TextMeshProUGUI highScore;
    private void OnEnable()
    {
        highScore.text = $"High Score: { GameManager.Instance.HighScore}";
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,rectTransform.rect.height);
        rectTransform.LeanMoveY(0, 1f).setEaseOutCubic().delay = 0.5f;
        if (restartanimation is null)
        {
            restartanimation = GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.LeanScale(new Vector3(1.1f, 1.1f), 1f).setLoopPingPong();
        }
        restartanimation.resume();

    }

    public void Restart()
    {
        restartanimation.pause();
        gameObject.SetActive(false);
        GameManager.Instance.Enable();
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
