namespace BLL.Plugins
{
    public interface ISerializationPlugin : IPlugin
    {
        void OnAfterSerialize(string text);
        string[] OnBeforeDeserialize(string[] text);
    }
}