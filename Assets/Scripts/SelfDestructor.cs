using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("To be destroyed" + gameObject.name);
        Destroy(gameObject, 5f); //todo allow customizations
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
