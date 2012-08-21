namespace C3.Communi
{
    public interface IPicker
    {
        string Name { get; set; } 
        PickResult Pick(IDevice device, byte[] bs);
    }
}