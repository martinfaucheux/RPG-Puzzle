using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIEntityComponent : UIComponent
{
    public GameObject attachedGameObject;

    public virtual void AttachObject(GameObject gameObject) {
        attachedGameObject = gameObject;
    }

}
