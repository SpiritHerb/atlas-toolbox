namespace AtlasToolbox.Services.ConfigurationServices
{
    public interface IConfigurationService
    {
        bool IsEnabled();
        void Enable();
        void Disable();
    }
}
