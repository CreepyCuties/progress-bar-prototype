using System;
using UnityEngine.Serialization;

[Serializable]
public struct Element
{
    [FormerlySerializedAs("baseAmount")] public float factor;
    public Elements element;

    public Element(float factor, Elements element)
    {
        this.element = element;
        this.factor = factor;
    }
    
    public void Deconstruct(
        out float factor,
        out Elements element
    )
    {
        factor = this.factor;
        element = this.element;
    }

    public Element clone()
    {
        var (baseAmount, element) = this;
        return new Element(baseAmount, element);
    }
}
