using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementHandler : MonoBehaviour
{
    public GameObject progressBar;
    public List<Element> elements;
    // Start is called before the first frame update
    public float handleElement(Element element)
    {
        var elementType = element.element;
        Debug.Log($"{elementType} element effect triggered");
        switch (elementType)
        {
            case Elements.Fire:
                return element.factor / 10;
            default:
                return 0;
        }
    }
    
    public int getElementIndex(Elements elementType)
    {
        return elements.FindIndex(element => element.element == elementType);
    }
}
