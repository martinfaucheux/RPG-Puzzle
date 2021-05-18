using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

 
public class HideInSkillMenu : MonoBehaviour
{

    private List<MonoBehaviour> _hidableComponents;
    private bool _isShowing = true;

    // Start is called before the first frame update
    void Start()
    {
        _hidableComponents = new List<MonoBehaviour>();
        SetHidableComponents();

        GameEvents.instance.onOpenSkillMenu += Hide;
        GameEvents.instance.onCloseSkillMenu += Show;
    }

    private void Show() => SetShow(true);
    private void Hide() => SetShow(false);

    private void SetShow(bool value){
        if (value != _isShowing){
            foreach(MonoBehaviour component in _hidableComponents){
                component.enabled = value;
            }
            _isShowing = value;
        }
    }

    private void SetHidableComponents(){
        MonoBehaviour[] componentArray = GetComponentsInChildren<Image>(true);
        componentArray = componentArray.Concat(GetComponentsInChildren<Text>(true)).ToArray();
        _hidableComponents = componentArray.ToList();
    }

    void OnDestroy(){
        GameEvents.instance.onOpenSkillMenu -= Hide;
        GameEvents.instance.onCloseSkillMenu -= Show;
    }
}
