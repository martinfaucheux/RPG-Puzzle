using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chrono : MonoBehaviour
{

    public static Chrono instance;
    public bool isCounting = false;
    public float timeElapsed {get; private set;} = 0f;

    # region Singleton
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

    # endregion

    // Update is called once per frame
    void Update()
    {
        if (isCounting){
            timeElapsed += Time.deltaTime;
        }
    }
}
