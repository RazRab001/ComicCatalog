using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReviewInterface
{
    public enum ReviewAutors 
    {
        TornadoCom,
        TruReview,
        AAmazing,
    }
    [Serializable]
    public class Review 
    {
        public ReviewAutors Autor { get; set; }
        private decimal value;
        public decimal Value { get; set; }
        public int Issue { get; set; }

        public static readonly List<Review> Reviews = new List<Review>();
        public static void AddReview(Review review)
        {
            foreach(Review rev  in Reviews)
            {
                if(rev.Autor == review.Autor && rev.Issue == review.Issue)
                {
                    rev.Issue = review.Issue;
                    return;
                }
            }
            Reviews.Add(review);
            Save();
        }
        public static void Save()
        {
            try
            {
                string filePath = GetReviewFilePath();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var serializer = new DataContractSerializer(typeof(List<Review>));
                    serializer.WriteObject(stream, Reviews);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while saving the catalog: " + ex.Message);
            }
        }
        public static void Load()
        {
            try
            {
                string filePath = GetReviewFilePath();
                if (File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        var serializer = new DataContractSerializer(typeof(List<Review>));
                        Reviews.Clear();
                        Reviews.AddRange((List<Review>)serializer.ReadObject(stream));
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
