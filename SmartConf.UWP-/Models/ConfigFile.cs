using SmartConf.UWP.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConf.UWP.Models
{
    public class ConfigFile
    {
        string _filePath;

        public ConfigFile(string filePath)
        {
            _filePath = filePath;
        }
        public void Save()
        {

        }

        public async List<ConfigurationItem> ReadFile()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("sample.txt");
            string text = await Windows.Storage.FileIO.ReadTextAsync(_filePath);

            string[] lines = File.ReadAllLines(_filePath);

            List<ConfigurationItem> configs = new List<ConfigurationItem>();

            string group = null;

            foreach (string configItem in lines)
            {
                if (configItem.Contains('[') && configItem.Contains(']'))
                {
                    group = configItem;
                }

                else if (configItem.Contains('='))
                {
                    configs.Add(new ConfigurationItem() { Group = group, Name = configItem.Remove(configItem.IndexOf('=')), Value = configItem.Remove(0, configItem.IndexOf('=') + 1) });
                }
            }

            return configs;
        }
    }
}
