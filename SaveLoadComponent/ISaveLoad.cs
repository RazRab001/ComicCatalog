using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SaveLoadComponent
{
    public interface ISaveLoad
    {
        public static void Save(List<ISaveLoad> saves)
        {
            try
            {
                string filePath = GetReviewFilePath();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var serializer = new DataContractSerializer(typeof(List<ISaveLoad>));
                    serializer.WriteObject(stream, saves);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while saving the catalog: " + ex.Message);
            }
        }
        public static void Load(List<ISaveLoad> saves)
        {
            try
            {
                string filePath = GetReviewFilePath();
                if (File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        var serializer = new DataContractSerializer(typeof(List<ISaveLoad>));
                        saves.Clear();
                        saves.AddRange((List<ISaveLoad>)serializer.ReadObject(stream));
                    }
                }
                else
                {
                    MessageBox.Show("The catalog file does not exist. Starting with an empty catalog.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while loading the catalog: " + ex.Message);
            }
        }
        private static string GetReviewFilePath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(documentsPath, "review_catalog.xml");
        }
    }
}
