using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IDRegion
{
    Board,
    Overworld,
    ViewMore
}

public class Main : MonoBehaviour
{
    public GameObject beeSprite;
    public GameObject speechbubblesprite;
    public float waitTime;
    public float papersSpeed;
    public float oldPapersSpeed = 3.0f;
    public float speechWait;
    public float flySpeed;
    public WaitForSeconds waitYi;
    public static Main main;
    public GameObject idObj;
    public Dictionary<string, GameObject> ids;

    public Rect overworld;
    public Rect boardRect;
    
    // Start is called before the first frame update
    void Start()
    {
        Main.main = this;
        waitYi = new WaitForSeconds(waitTime);
        ids = new Dictionary<string, GameObject>();
        ids["ID"] = idObj;
        StartCoroutine(CheckingEvent());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator CheckingEvent()
    {
        Debug.Log("made a bee");
        GameObject currBee = Instantiate(beeSprite, new Vector2(-12f, 4f), Quaternion.identity);
        yield return StartCoroutine(Main.main.MoveFromTo(currBee.transform, currBee.transform.position,
            new Vector2(-3f, 1f), flySpeed));
        yield return Main.main.waitYi;
        yield return StartCoroutine(Main.main.SpeechBubble("BZZZZZ", speechWait, new Vector2(-3.5f, 3f)));
        yield return waitYi;
        yield return HandPapersOver(currBee);
        //make some sprite appear from the left
        // present papers, potentially
        // pause timer
        // hand control to the player
    }

    public IEnumerator HandPapersOver(GameObject bee)
    {
        //say "papers, plzzzzz".
        StartCoroutine(SpeechBubble("Papers, plzzzzzzz.", speechWait, new Vector2(-6.5f, 4f)));
        //yield return waitYi;
        yield return SpeechBubble("FUCK YOU COPS, YOU'LL NEVER TAKE ME ALIVE.", speechWait, new Vector2(-3.5f, 3f));
        yield return PassPapersOver(bee);

    }

    public IEnumerator PassPapersOver(GameObject bee)
    {
        //get all the ID the bee will hand over
        //make the sprite
        //move it onto the counter
        //let the player pick it up and examine it
        string[] beeID = new string[]{bee.GetComponent<Documents>().GetAll()};
        GameObject id = Instantiate(ids[beeID[0]], new Vector2(-4.3f, 0f), Quaternion.identity);
        yield return MoveFromTo(id.transform, id.transform.position, new Vector2(-5.5f, -3.6f), papersSpeed);
        id.GetComponent<Draggable>().isEnabled = true;
    }

    public IEnumerator SpeechBubble(string txt, float? timeToWait, Vector2 loc)
    {
        //make a speech bubble sprite
        //print the text
        //wait e.g. 5s
        //remove the sprite
        GameObject spr = Instantiate(speechbubblesprite, loc, Quaternion.identity);
        if (timeToWait is float tim)
            yield return spr.GetComponent<AnimatedText>().Create(txt, tim);
        
    }


    public IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed) 
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f) {
                t += step; // Goes from 0 to 1, incrementing by step each time
                objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
                yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
    }

    public IDRegion GetIDRegion(Transform tr)
    {
        return overworld.Contains(tr.position) ? IDRegion.Overworld : boardRect.Contains(tr.position) ?
            IDRegion.Board : IDRegion.ViewMore;
    } 
}
