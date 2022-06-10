using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class ContextMenuController : MonoBehaviour
{

    public static ContextMenuController instance = null;

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

    public GameObject attachedGameObject;


    public bool isOpen { get; private set; } = false;

    [SerializeField] List<UIEntityComponent> entityComponents;

    private void Start()
    {
        Hide();
    }


    public void Show(GameObject attachedGameObject)
    {
        this.attachedGameObject = attachedGameObject;
        foreach (UIEntityComponent entityComponent in entityComponents)
        {
            entityComponent.AttachObject(attachedGameObject);
        }

        isOpen = true;
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(true);
        }

    }

    public void Hide()
    {
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(false);
        }
        isOpen = false;
    }

    private bool CanToggleMenu()
    {
        // TODO: use game state instead
        GameManager gm = GameManager.instance;
        return (
            !(
                gm.isGamePaused)
                || gm.isEndOfLevelScreen
                || gm.isGameOverScreen
                || gm.isGameOverScreen
            );
    }



}
