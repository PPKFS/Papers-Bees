using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Draggable : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public Sprite regular;
    public Sprite overworld;
    public Sprite viewmore;
    public Sprite selected;

    public bool isEnabled = false;

    void OnMouseDown()
    {
        offset = gameObject.transform.position - 
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        gameObject.GetComponent<SpriteOutline>().enabled = true;
        isEnabled = true;
    }

    void OnMouseUp()
    {
        gameObject.GetComponent<SpriteOutline>().enabled = false;
        isEnabled = false;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void Update()
    {
        if(!isEnabled)
        return;
        IDRegion reg = Main.main.GetIDRegion(transform);
        switch(reg)
        {
            case IDRegion.Board:
                gameObject.GetComponent<SpriteOutline>().enabled = false;
            break;
            case IDRegion.Overworld:
            gameObject.GetComponent<SpriteOutline>().enabled = true;
                gameObject.GetComponent<SpriteOutline>().color = Color.yellow;
            break;
            case IDRegion.ViewMore:
                gameObject.GetComponent<SpriteOutline>().enabled = true;
                gameObject.GetComponent<SpriteOutline>().color = Color.blue;
            break;
        }
    }
}