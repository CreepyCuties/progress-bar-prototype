using System;

[Serializable]
public struct Element
{
    public float amount;
    public float baseAmount;
    public Elements element;

    public Element(float amount, float baseAmount, Elements element)
    {
        this.amount = amount;
        this.element = element;
        this.baseAmount = baseAmount;
    }
    
    public void Deconstruct(
        out float amount, 
        out float baseAmount,
        out Elements element
    )
    {
        amount = this.amount;
        baseAmount = this.baseAmount;
        element = this.element;
    }

    public Element clone()
    {
        var (amount, baseAmount, element) = this;
        return new Element(amount, baseAmount, element);
    }
}
