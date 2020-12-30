using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int keyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddKey()
    {
        Debug.Log("Player Picked up a new key");
        keyCount += 1;
    }

    public bool UseKey()
    {
        if (keyCount > 0)
        {
            keyCount--;
            return true;
        }
        return false;
    }


}
