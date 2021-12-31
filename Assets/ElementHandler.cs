using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementHandler : MonoBehaviour
{
    public GameObject progressBar;
    public List<Element> elements;
    // Start is called before the first frame update
    public Element handleElement(Element element)
    {
        var elementType = element.element;
        Debug.Log($"{elementType} element effect triggered");
        switch (elementType)
        {
            case Elements.Fire:
                element.amount += element.baseAmount / 10;
                return element;
            default:
                return element;
        }
    }
}
