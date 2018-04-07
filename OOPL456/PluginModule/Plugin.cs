using BLL.Plugins;

namespace PluginModule
{
    public class Plugin : ISerializationPlugin
    {
        public Plugin()
        {
            Enabled = true;
        }

        public string OnAfterSerialize(string text)
        {
            return CryptoHelper.Encrypt(text);
        }

        public string OnBeforeDeserialize(string text)
        {

            return CryptoHelper.Decrypt(text);
        }

        public bool Enabled { get; set; }
    }
}