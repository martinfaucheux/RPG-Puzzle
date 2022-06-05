using UnityEngine;

public class SingletoneBase<T> : MonoBehaviour
    where T : MonoBehaviour
{
    /// <summary>
    /// SingletoneBase instance back field
    /// </summary>
    public static T instance { get; private set; } = null;

    protected virtual void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = gameObject.GetComponent<T>();

        //If instance already exists and it's not this:
        else if (instance.GetInstanceID() != GetInstanceID())

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);
    }
}