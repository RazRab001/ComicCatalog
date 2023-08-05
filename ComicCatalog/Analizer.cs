using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Linq;
using System.Xml.Serialization;
using ReviewInterface;
using ComicCatalog;

namespace ComicAnalizer
{
    public static class Analizer
    {
        public static readonly List<Comic> Catalog = new List<Comic>();
        public static void AddComic(string name, int issue) 
        {
            foreach(var comic in Catalog)
            {
                if (comic.Issue == issue) return;
            }
            Catalog.Add(new Comic(name, issue));
        }
        public static bool RemoveComic(string name)
        {
            foreach(var comic in Catalog)
            {
                if (comic.Name == name)
                {
                    Catalog.Remove(comic);
                    return true;
                }
            }
            MessageBox.Show("This comic don't exist. Control your data.");
            return false;
        }
        public static bool RemoveComic(int issue)
        {
            foreach (var comic in Catalog)
            {
                if (comic.Issue == issue)
                {
                    Catalog.Remove(comic);
                    return true;
                }
            }
            MessageBox.Show("This comic don't exist. Control your data.");
            return false;
        }
        public static IEnumerable<string> ShowReview(ReviewAutors autor)
        {
            Review.Load();
            var rezult =
                from comic in Catalog
                join review in Review.Reviews
                on comic.Issue equals review.Issue
                where review.Autor == autor
                select $"{comic} revew: {review.Value :F2}";
            return rezult;
        }

        public static void Load()
        {
            try
            {
                string filePath = GetCatalogFilePath();
                if (File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        var serializer = new DataContractSerializer(typeof(List<Comic>));
                        Catalog.Clear();
                        Catalog.AddRange((List<Comic>)serializer.ReadObject(stream));
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

        public static void Save()
        {
            try
            {
                string filePath = GetCatalogFilePath();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var serializer = new DataContractSerializer(typeof(List<Comic>));
                    serializer.WriteObject(stream, Catalog);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while saving the catalog: " + ex.Message);
            }
        }

        private static string GetCatalogFilePath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(documentsPath, "comic_catalog.xml");
        }

    }
}
