namespace BLL.Plugins
{
    public interface ISerializationPlugin : IPlugin
    {
        string OnAfterSerialize(string text);
        string OnBeforeDeserialize(string text);
        bool Enabled { get; set; }
    }
}