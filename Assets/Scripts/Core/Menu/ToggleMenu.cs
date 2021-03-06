using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleMenu : MonoBehaviour
{
    [SerializeField] private bool _isShowing;
    public bool isShowing{
        get{return _isShowing;}
        set{
            _isShowing = value;
            if(value){
                Show();
            } else {
                Hide();
            }
        }
    }

    public KeyCode toggleKeyCode;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toggleKeyCode != KeyCode.None){
            if(Input.GetKeyDown(toggleKeyCode)){
                Toggle();
            }
        }
    }

    public void Toggle(){
        isShowing = !(isShowing);
    }

    private void Show(){
        foreach(Transform childTransform in transform){
            childTransform.gameObject.SetActive(true);
        }
    }

    private void Hide(){
        foreach(Transform childTransform in transform){
            childTransform.gameObject.SetActive(false);
        }
    }
}
