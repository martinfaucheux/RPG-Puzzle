using UnityEngine;
using UnityEngine.UI;

public class AttackCountUI : MonoBehaviour
{
    [SerializeField] Text attackCountText;
    private Attack _playerAttackComponent;
    private int _playerAttackComponentID;
    void Start(){
        _playerAttackComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        _playerAttackComponentID = _playerAttackComponent.GetInstanceID();

        if (attackCountText == null){
            attackCountText = GetComponent<Text>();
        }

        GameEvents.instance.onAttackChange += UpdateUI;
    }

    private void UpdateUI(int componentID){
        if (componentID == _playerAttackComponentID){
            attackCountText.text = _playerAttackComponent.attackPoints.ToString();
        }
    }
}
