using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
    static float minRadius = 0.5f;
    static float maxRadius = 0.5f;
    [SerializeField] Health _healthComponent;
    [SerializeField] Color _defaultTextColor = Color.white;
    [Tooltip("Show 0 if the health difference is 0.")]
    [SerializeField] bool showNoDamage = true;
    [SerializeField] bool showHealing = false;
    [SerializeField] Color _healColor = Color.white;

    private bool isPlayer { get { return gameObject.tag == "Player"; } }

    void Start()
    {
        GameEvents.instance.onHealthChange += OnHealthChange;
    }

    void OnDestroy()
    {
        GameEvents.instance.onHealthChange -= OnHealthChange;
    }

    private void OnHealthChange(int instanceId, int initValue, int finalValue)
    {
        if (instanceId == _healthComponent.GetInstanceID())
        {
            int healthDiff = finalValue - initValue;
            if (
                (healthDiff == 0 & !showNoDamage)
                | (healthDiff > 0 & !showHealing)

            )
            {
                return;
            }

            Color textColor = _defaultTextColor;
            string damageText = Mathf.Abs(healthDiff).ToString();
            if (showHealing)
            {
                string sign = "-";
                if (healthDiff > 0)
                {
                    sign = "+";
                    textColor = _healColor;
                }
                damageText = sign + damageText;
            }
            Spawn(damageText, GetSpawnPosition(_healthComponent.transform.position), textColor);
        }
    }

    private void Spawn(string damageText, Vector3 spawnPosition, Color textColor)
    {

        GameObject newGo = GameObjectPool.instance.GetOrCreate("DamageNumber");
        DamageNumber component = newGo.GetComponent<DamageNumber>();

        component.ResetState(damageText, textColor);
        newGo.transform.position = spawnPosition;
        newGo.SetActive(true);

        component.Animate();
    }



    private Vector3 GetSpawnPosition(Vector3 entityPosition)
    {
        float angle;
        if (isPlayer)
        {
            angle = Random.Range(0.75f * Mathf.PI, 1.25f * Mathf.PI);
        }
        else
        {
            angle = Random.Range(-0.25f * Mathf.PI, 0.25f * Mathf.PI);
        }

        float radius = Random.Range(minRadius, maxRadius);
        Vector3 offset = radius * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 1f);

        return entityPosition + Constant.camAngle * offset;

    }
}
