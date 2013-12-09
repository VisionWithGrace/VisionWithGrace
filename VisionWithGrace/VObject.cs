using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

/**
 * Represents an object which will be written to the database
 * */
namespace VisionWithGrace
{
    public class VObject
    {
        private string id = null;
        public string name = "";
        public List<string> tags = new List<string>();
        public List<Bitmap> images = new List<Bitmap>();

        public VObject()
        {
        }

        public ~VObject()
        {
            foreach (Bitmap image in images)
            {
                image.Dispose();
            }
        }

        public VObject(Dictionary<string, object> fromDictionary)
        {
            id = fromDictionary["_id"].ToString();

            if (fromDictionary.Keys.Contains("name"))
                name = fromDictionary["name"] as string;

            if (fromDictionary.Keys.Contains("tags"))
            {
                object[] tagObjects = fromDictionary["tags"] as object[];
                for (int i = 0; i < tagObjects.Length; i++)
                {
                    tags.Add(tagObjects[i] as string);
                }
            }

            if (fromDictionary.Keys.Contains("image"))
                images[0] = fromDictionary["image"] as Bitmap;
        }

        public void save()
        {
            // get a database interface
            DatabaseModule.DatabaseInterface db = new DatabaseModule.DatabaseInterface();

            // structure data for database
            Dictionary<string, object> data = new Dictionary<string,object>();

            data["name"] = name;
            data["tags"] = tags;

            db.saveSelection(images[0], data);
        }

        public void delete()
        {
            DatabaseModule.DatabaseInterface db = new DatabaseModule.DatabaseInterface();
            db.deleteObject(this.id);
           
        }
    }
}
