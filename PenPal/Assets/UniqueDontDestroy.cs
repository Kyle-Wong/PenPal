using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueDontDestroy : MonoBehaviour
{

    // Use this for initialization
    public string uniqueTag;
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag(uniqueTag).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
