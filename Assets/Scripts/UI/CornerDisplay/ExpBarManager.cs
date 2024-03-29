using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBarManager : MonoBehaviour
{
    public Slider barSlider;
    [SerializeField] TextMeshProUGUI levelIntTextComponent;

    public float fillTransitionDuraton = 0.2f;
    private Experience _expComponent;

    void Start()
    {
        _expComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();

        // TODO: register to exp change signal
        GameEvents.instance.onPlayerExperienceChange += UpdateExpFill;
        GameEvents.instance.onLevelUp += UpdateLevelInt;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onPlayerExperienceChange -= UpdateExpFill;
        GameEvents.instance.onLevelUp -= UpdateLevelInt;
    }

    public void UpdateExpFill()
    {
        float fillPercent = (float)_expComponent.currentExpPoints / (float)_expComponent.targetExpPoints;
        StartCoroutine(FilleCoroutine(fillPercent));

    }

    public void UpdateLevelInt()
    {
        levelIntTextComponent.text = _expComponent.currentLevel.ToString();
    }

    private IEnumerator FilleCoroutine(float targetPercent)
    {
        float timeSinceStart = 0f;
        float initPercent = barSlider.value;

        while (timeSinceStart < fillTransitionDuraton)
        {
            float percent = initPercent + (timeSinceStart / fillTransitionDuraton) * (targetPercent - initPercent);
            barSlider.value = percent;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        barSlider.value = targetPercent;
    }

}
