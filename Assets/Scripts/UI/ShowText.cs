using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText: MonoBehaviour
{
    //public enum EventType
    //{
    //    LevelUp,
    //    EndOfLevel
    //}
    //public EventType eventType;

    public string levelUpText = "Level Up";
    public string endOfLevelText = "Level Complete";
    public string gameOverText = "Game Over";

    public bool animateEnterText = true;
    public float heightScreenRatio = 0.75f;
 
    public Text textComponent;
    public bool isShowing = false;

    public Color lightColor;
    public Color darkColor;

    private RectTransform _rectTransform;


    protected virtual void Start() {

        _rectTransform = GetComponent<RectTransform>();
        SetPosition();

        // listen to the event
        GameEvents.instance.onEnterLevelUp += ShowLevelUp;
        GameEvents.instance.onExitLevelUp += Hide;
        GameEvents.instance.onEndOfLevel += ShowEndOfLevel;
        GameEvents.instance.onGameOver += ShowGameOver;

        textComponent = GetComponent<Text>();
        if (textComponent == null)
            Debug.LogError(gameObject.ToString() + ": No Text component found");
    }

    public void ShowLevelUp()
    {
        Show(levelUpText);
    }

    public void ShowEndOfLevel()
    {
        Show(endOfLevelText);
    }

    public void ShowGameOver()
    {
        Show(darkColor, gameOverText, 3f);
    }

    public void Show(Color color, string text = "", float speed=0.5f)
    {
        textComponent.enabled = true;
        textComponent.text = text;
        textComponent.color = color;
        isShowing = true;

        if (animateEnterText)
        {
            Vector3 enterPos = _rectTransform.anchoredPosition + new Vector2(0f, 200f);
            StartCoroutine(EnterTextAnimation(enterPos, speed));
        }
    }

    public void Show(string text = "", float speed = 0.5f)
    {
        Show(lightColor, text, speed);
    }

    public void Hide()
    {
        textComponent.enabled = false;
        isShowing = false;
    }

    private void SetPosition(){
        // set the position at 75% of the screen
        float screenHeight = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height;

        float y = screenHeight * (heightScreenRatio - 0.5f);

        _rectTransform.anchoredPosition = new Vector3(0f, y, 0f);
    }   

    private IEnumerator EnterTextAnimation(Vector3 enterPos, float animationDuration)
    {
        Vector3 targetPos = _rectTransform.anchoredPosition;

        float timeSinceStart = 0f;
        while (timeSinceStart < animationDuration)
        {
            Vector3 newPos = enterPos + (timeSinceStart / animationDuration) * (targetPos - enterPos);
            _rectTransform.anchoredPosition = newPos;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }
        _rectTransform.anchoredPosition = targetPos;
    }
}
