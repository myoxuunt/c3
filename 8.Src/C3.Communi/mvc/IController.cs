namespace C3.Communi
{
    public interface IController
    {
        IModel Model { get; set; }
        IViewer Viewer { get; set; }

        void UpdateModel();
        void UpdateViewer();
        bool Verify();
    }

}
