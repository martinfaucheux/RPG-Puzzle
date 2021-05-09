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
    private RectTransform _rectTransform;

    // if true, the menu will expand with an animation
    public bool expand = true;
    
    public bool isOpen = false;

    // offset added when displaying the menu
    public float offset = 100;
    private MonoBehaviour[] _hidableComponents;
    private Animator _animator;

    private void Start()
    {
        _rectTransform = GetComponentInChildren<RectTransform>();
        
        SetHidableComponents(); 
        
        _animator = GetComponent<Animator>();
        if(_animator == null)
            expand = false;
        else{
            // enable animator only in the case where expand is true
            _animator.enabled = expand;
        }
        InstantHideMenu();
    }

    public void AttachObject(GameObject attachedGameObject)
    {
        Debug.Log("Attach " + attachedGameObject.name);

        this.attachedGameObject = attachedGameObject;
        foreach(UIEntityComponent entityComponent in GetComponentsInChildren<UIEntityComponent>())
        {
            entityComponent.AttachObject(attachedGameObject);
        }
    }

    private Vector3 GetMenuPosition()
    {
        Vector3 attachedObjectPos = attachedGameObject.transform.position + Vector3.up * 0.5f; // assume the size of entity is 1
        Vector3 screenPos = Camera.main.WorldToScreenPoint(attachedObjectPos);
        Vector3 menuOffset = ((GetMenuSize().y * 1.01f) / 2  + offset) * Vector3.up;
        return screenPos + menuOffset;
    }

    private Vector3 GetMenuPositionOld()
    {
        Vector3 attachedObjectPos = attachedGameObject.transform.position + Vector3.right * 0.5f;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(attachedObjectPos);
        Vector3 menuOffset = GetMenuSize().x * 1.01f * Vector3.right / 2;
        return screenPos + menuOffset;
    }

    private Vector2 GetMenuSize()
    {
        return new Vector2(
            _rectTransform.sizeDelta.x * transform.localScale.x,
            _rectTransform.sizeDelta.y * transform.localScale.y
        );
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
        
        SetChildrenEnabled(true);

        if(_animator != null)
            _animator.SetBool("isShowing", true);
    }

    public void InstantShowMenu()
    {
        isOpen = true;
        transform.position = GetMenuPosition();

        SetChildrenEnabled(true);
    }

    public void SlowHideMenu()
    {
        isOpen = false;
        
        if(_animator != null){
            _animator.SetBool("isShowing", false);
            StartCoroutine(DisableWhenAfterHide());
        }
    }

    public void InstantHideMenu()
    {        
        SetChildrenEnabled(false);

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

    protected void SetHidableComponents(){
        _hidableComponents = GetComponentsInChildren<Image>(true);
        _hidableComponents = _hidableComponents.Concat(GetComponentsInChildren<Text>(true)).ToArray();
    }

    private void SetChildrenEnabled(bool enabled)
    {
        foreach(MonoBehaviour component in _hidableComponents)
        {
            component.enabled = enabled;
        }

        // foreach(Transform childTransform in transform){
        //     childTransform.gameObject.SetActive(enabled);
        // }
    }
}
