using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleMenu : MonoBehaviour
{
    public bool isShowing = false;

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
        if (isShowing)
            Hide();
        else
            Show();
    }

    private void Show(){
        isShowing = true;
        foreach(Transform childTransform in transform){
            childTransform.gameObject.SetActive(true);
        }
    }

    public void Hide(){
        isShowing = false;
        foreach(Transform childTransform in transform){
            childTransform.gameObject.SetActive(false);
        }
    }
}
