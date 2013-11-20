using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VisionWithGrace
{
    class VObject
    {
        public string name = "Unnamed Object";
        public List<string> tags = new List<string>();
        public List<Bitmap> images = new List<Bitmap>();

        public VObject()
        {
        }

        public void save()
        {
            // get a database interface
            DatabaseModule.DatabaseManager db = new DatabaseModule.DatabaseManager();

            // structure data for database
            Dictionary<string, object> data = new Dictionary<string,object>();
            data["name"] = name;
            data["tags"] = tags;
            
            // save item
            db.saveSelection(images[0], data);
        }
    }
}
