using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPOSGhost
{
    internal class ReadAsampleFormat
    {
        public List<string> AsampleFormats()
        {
            string jsonFile = "A_SampleFormat.json";
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFile);
            List<string> formats = new List<string>();
            using (System.IO.StreamReader file = System.IO.File.OpenText(configFilePath))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject oojectFormat = (JObject)JToken.ReadFrom(reader);
                    foreach (var item in oojectFormat)
                    {
                        formats.Add(item.Key.ToString());
                    }
                }
            }
            return formats;
        }
    }
}
