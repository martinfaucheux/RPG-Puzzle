using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class ContextMenuController : MonoBehaviour
{

    public static ContextMenuController instance = null;
    private GameObject _attachedGameObject;
    public bool isOpen { get; private set; } = false;
    [SerializeField] float fadeDuration = 0.2f;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] List<UIEntityComponent> entityComponents;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
    }


    public void Show(GameObject attachedGameObject, InspectorData inspectorData)
    {
        this._attachedGameObject = attachedGameObject;
        foreach (UIEntityComponent entityComponent in entityComponents)
        {
            entityComponent.AttachObject(attachedGameObject, inspectorData);
        }

        isOpen = true;
        Fade(1f);
    }

    public void Hide()
    {
        Fade(0f);
        isOpen = false;
    }

    private LTDescr Fade(float targetValue)
    {
        return LeanTween.value(
            gameObject, v => canvasGroup.alpha = v,
            canvasGroup.alpha,
            targetValue,
            fadeDuration
        );
    }
}
