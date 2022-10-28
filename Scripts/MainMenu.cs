using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager; 

    [SerializeField]
    private RectTransform scoreRectTransform;

    private void Start()
    {
        scoreRectTransform.anchoredPosition = new Vector2(scoreRectTransform.anchoredPosition.x, 25);
        GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.LeanScale(new Vector3(1.2f, 1.2f), 0.5f).setLoopPingPong();
    }

    public void Play()
    {
        GetComponentInChildren<CanvasGroup>().gameObject.LeanAlpha(0,0.2f).setOnComplete(OnComplete); 
    }

    public void OnComplete()
    {
        scoreRectTransform.LeanMoveY(-40f, 0.75f).setEaseOutCubic();
        gameManager.Enable();
        Destroy(gameObject);
    }
}
