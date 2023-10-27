public interface ISlashable
{
    public bool OnSlashed(Pattern c);
    public Pattern GetPattern();
    public float GetYPos();
}