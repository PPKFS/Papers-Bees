using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour {
    public string str;
    public GameObject txt;
    public float OldSpeed = 0.04f;
    public float speed = 0.004f;
    public bool enabledOnCreation;
    public IEnumerator Create(string txtStr, float timeToWait){
        yield return StartCoroutine(AnimateText(txtStr, speed));
        Destroy(gameObject, timeToWait);
        //gameObject.SetActive(false);
    }

    void Start()
    {
        if(enabledOnCreation)
            StartCoroutine(Create(str, 10000f));
    }

    IEnumerator AnimateText(string strComplete, float spd){
        int i = 0;
        str = "";
        while( i < strComplete.Length ){
            str += strComplete[i++];
            txt.GetComponent<Text>().text = str;
            yield return new WaitForFixedUpdate();//new WaitForSeconds(spd);
        }
    }
}
