using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class MenuController : MonoBehaviour
{

    public static MenuController instance = null;

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
    public RectTransform rectTransform;

    // if true, the menu will expand with an animation
    public bool expand = true;
    
    public bool isOpen = false;

    private MonoBehaviour[] _hidableComponents;
    private Animator _animator;

    private void Start()
    {
        _hidableComponents = GetComponentsInChildren<Image>();
        _hidableComponents = _hidableComponents.Concat(GetComponentsInChildren<Text>()).ToArray();
        
        _animator = GetComponent<Animator>();

        // enable animator only in the case where expand is true
        _animator.enabled = expand;

        InstantHideMenu();
    }

    public void AttachObject(GameObject attachedGameObject)
    {
        this.attachedGameObject = attachedGameObject;
        foreach(UIEntityComponent entityComponent in GetComponentsInChildren<UIEntityComponent>())
        {
            entityComponent.AttachObject(attachedGameObject);
        }
    }

    private Vector3 GetMenuPosition()
    {
        Vector3 attachedObjectPos = attachedGameObject.transform.position + Vector3.right * 0.5f;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(attachedObjectPos);
        Vector3 menuOffset = GetMenuWidth() * 1.01f * Vector3.right / 2;
        return screenPos + menuOffset;
    }

    private float GetMenuWidth()
    {
        return rectTransform.sizeDelta.x * transform.localScale.x;
    }

    public void Trigger()
    {
        if (CanToggleMenu()){
            if (isOpen)
            {
                if (expand)
                {
                    SlowHideMenu();
                }
                else
                {
                    InstantHideMenu();
                }
            }
            else
            {
                if (expand)
                {
                    SlowShowMenu();
                }
                else
                {
                    InstantShowMenu();
                }
            }
        }
    }

    private bool CanToggleMenu(){
        GameManager gm = GameManager.instance;
        return (
            !(
                gm.isGamePaused)
                || gm.isEndOfLevelScreen
                || gm.isGameOverScreen
                || gm.isGameOverScreen
            );
    }

    public void SlowShowMenu()
    {
        isOpen = true;
        transform.position = GetMenuPosition();
        
        SetImagesEnabled(true);

        _animator.SetBool("isShowing", true);
    }

    public void InstantShowMenu()
    {
        isOpen = true;
        transform.position = GetMenuPosition();

        SetImagesEnabled(true);
    }

    public void SlowHideMenu()
    {
        isOpen = false;
        _animator.SetBool("isShowing", false);
        StartCoroutine(DisableWhenAfterHide());
    }

    public void InstantHideMenu()
    {        
        SetImagesEnabled(false);

        isOpen = false;
    }

    private IEnumerator DisableWhenAfterHide()
    {
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f)
        {
            yield return null;
        }
        InstantHideMenu();
    }

    private void SetImagesEnabled(bool enabled)
    {
        foreach(MonoBehaviour component in _hidableComponents)
        {
            component.enabled = enabled;
        }
    }
}
