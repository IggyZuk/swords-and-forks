public struct Pos
{
    public int x;
    public int y;

    public Pos(int x = 0, int y = 0)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + ", " + y;
    }
}
