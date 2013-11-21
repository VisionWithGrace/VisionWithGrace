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
    class VObject
    {
        public string name = "Unnamed Object";
        public List<string> tags = new List<string>();
        public Bitmap image;

        public VObject()
        {
        }

        public void save()
        {
            // get a database interface
            DatabaseModule.DatabaseInterface db = new DatabaseModule.DatabaseInterface("VObjects");

            // structure data for database
            Dictionary<string, object> data = new Dictionary<string,object>();
            data["name"] = name;
            data["tags"] = tags;
            
            // save item
            db.saveSelection(image, data);
        }
    }
}
