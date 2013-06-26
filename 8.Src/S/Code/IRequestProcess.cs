namespace S
{
    internal interface IRequestProcess
    {
        bool Process(Client client, byte[] received);
    }
}
