using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempButtons : MonoBehaviour
{
    public GameObject contr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeBees()
    {
        StartCoroutine(contr.GetComponent<Main>().CheckingEvent());
    }

}
