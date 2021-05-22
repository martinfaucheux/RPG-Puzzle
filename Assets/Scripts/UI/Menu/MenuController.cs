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

    [SerializeField] RectTransform bubbleRectTransform;
    
    public bool isOpen = false;
    public float offset = 100;
    public float flipScreenRatio = 0.75f;

    // offset added when displaying the menu
    private List<MonoBehaviour> _hidableComponents;
    private Animator _animator;
    private RectTransform _rectTransform;
    private int _lastFrameTrigger;

    // whether the game object is top side of the screen
    private bool isAttachedGameObjectTopSide{
        get{
            return IsEntityTopSide(attachedGameObject.transform.position);
        }
    }

    private void Start()
    {
        _rectTransform = GetComponentInChildren<RectTransform>();
        
        SetHidableComponents(); 
        
        _animator = GetComponent<Animator>();
        InstantHideMenu();
    }

    void Update(){
        if (
            Input.GetMouseButtonUp(0)
            && !GameManager.instance.isGamePaused
            && !SkillMenu.instance.isShowing
            && isOpen
            // make sure menu was not opened during current frame
            && Time.frameCount != _lastFrameTrigger
        ){
            InstantHideMenu();
        }
    }

    public void AttachObject(GameObject attachedGameObject)
    {
        this.attachedGameObject = attachedGameObject;
        foreach(UIEntityComponent entityComponent in GetComponentsInChildren<UIEntityComponent>())
        {
            entityComponent.AttachObject(attachedGameObject);
        }
    }
    public void RegisterHiddableComponent(MonoBehaviour component){
        _hidableComponents.Add(component);
    }

    public void Trigger()
    {
        if (CanToggleMenu()){
            if (isOpen)
            {
                InstantHideMenu();
            }
            else
            {
                InstantShowMenu();
            }
        }
    }

    public void InstantShowMenu()
    {
        _lastFrameTrigger = Time.frameCount;
        isOpen = true;
        transform.position = GetMenuPosition();

        FlipBubbleImage();
        SetChildrenEnabled(true);

        _animator.SetTrigger("pop");
    }

    public void InstantHideMenu()
    {        
        SetChildrenEnabled(false);
        isOpen = false;
    }
    protected void SetHidableComponents(){
        MonoBehaviour[] componentArray = GetComponentsInChildren<Image>(true);
        componentArray = componentArray.Concat(GetComponentsInChildren<Text>(true)).ToArray();
        _hidableComponents = componentArray.ToList();
    }

    private Vector3 GetMenuPosition()
    {
        Vector3 attachedObjectPos = attachedGameObject.transform.position + Vector3.up * 0.5f; // assume the size of entity is 1
        Vector3 screenPos = Camera.main.WorldToScreenPoint(attachedObjectPos);

        float menuOffsetValue = ((GetMenuSize().y * 1.01f) / 2  + offset);
        Vector3 menuOffsetVect = menuOffsetValue * (isAttachedGameObjectTopSide ? Vector3.down : Vector3.up);
        return screenPos + menuOffsetVect;
    }

    private Vector2 GetMenuSize()
    {
        return new Vector2(
            _rectTransform.sizeDelta.x * transform.localScale.x,
            _rectTransform.sizeDelta.y * transform.localScale.y
        );
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

    private void SetChildrenEnabled(bool enabled)
    {
        foreach(MonoBehaviour component in _hidableComponents)
        {
            component.enabled = enabled;
        }
    }

    private bool IsEntityTopSide(Vector3 position){
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        return (screenPos.y / Screen.height) > flipScreenRatio;
    }

    private void FlipBubbleImage(){
        Vector3 bubbleScale = bubbleRectTransform.localScale;
        bubbleScale.y = (isAttachedGameObjectTopSide ? -1 : 1);
        bubbleRectTransform.localScale = bubbleScale;
    }
}
