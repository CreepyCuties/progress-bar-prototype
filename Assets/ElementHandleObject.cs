public struct ElementHandleObject
{
    public int divider;
    public int effectTime;

    public ElementHandleObject(int divider, int effectTime)
    {
        this.divider = divider;
        this.effectTime = effectTime;
    }

    public void Deconstruct(out int divider, out int waitingSecond)
    {
        divider = this.divider;
        waitingSecond = this.effectTime;
    }
}
