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

    public void AllowIntoHive()
    {
        //say "yeah you can go on in"
        //move bee into the right
    }

    public void TurnAway()
    {
        //say no
        //bee moves to the left
    }

    public void Query()
    {
        //open the 'query' subdialog
        //select two items to be queried
    }

}
