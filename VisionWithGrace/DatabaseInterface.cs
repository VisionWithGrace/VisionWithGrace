﻿using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Builders;

using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Wrappers;

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Diagnostics;
using System.Linq;
using DatabaseModule;
using VisionWithGrace;



namespace DatabaseModule
{

    public class DatabaseInterface
    {
        private MongoServer server;
        private MongoDatabase objectsDatabase;
        private const string collectionName = "VObjects";
        private MongoCollection objectsCollection;
        public DatabaseInterface()
        {
            Connect();
        }
        private void Connect()
        {


            const string remoteConnectionString = "mongodb://localhost";
            MongoClient client = new MongoClient(remoteConnectionString);
            server = client.GetServer();
            objectsDatabase = server.GetDatabase("choices_db");
            objectsCollection = objectsDatabase.GetCollection(collectionName);

        }
        private QueryDocument queryFromString(string queryString)
        {
            BsonDocument query = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryString);
            return new QueryDocument(query);
        }
        private static Random random = new Random((int)DateTime.Now.Ticks);
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        private string tagsToString(List<string> tags)
        {
            string outString = "[";
            foreach(string tag in tags)
            {
                outString += "'"+tag + "',";
            }
            outString.Remove(outString.LastIndexOf(','));
            outString += "]";
            return outString;
        }

        //inserts a bson document into specified collection
        //BSON is a key-value dicitonary similar to JSON
        private void Insert(BsonDocument documentToAdd)
        {
            objectsCollection.Insert(documentToAdd);
            
        }
        //inserts a bson document into specified collection
        //documentToAdd is a JSON-formatted key-value dicitonary
        public void Insert(Dictionary<string, object> documentToAdd)
        {
            BsonDocument docToInsert = new BsonDocument(documentToAdd);
            this.Insert(docToInsert);
        }

        public string InsertImage(Image image)
        {
            string filename = RandomString(10) + ".jpg";
            InsertImage(image, filename);
            return filename;
        }

        private void InsertImage(Image image, string filename)
        {

            using (var fs = new System.IO.MemoryStream())
            {
                if (image != null)
                {
                    Bitmap bitmapCopy = new Bitmap(image);
                    bitmapCopy.Save(fs, ImageFormat.Bmp);
                    fs.Position = 0;
                    var gridFsInfo = objectsDatabase.GridFS.Upload(fs, filename);
                    var fileId = gridFsInfo.Id;
                }
               
            }

        }

        public List<Image> GetImages(string key, string value)
        {
            List<Image> toReturn = new List<Image>();
            var cursor = this.Get(key, value);
            foreach (BsonDocument document in cursor)
            {
                List<Image> images = GetImages(document);
                toReturn.AddRange(images);
            }
            return toReturn;

        }
        
        //get images associated with document
        private List<Image> GetImages(BsonDocument document)
        {
            object[] filenameObjects = document.ToDictionary()["filenames"] as object[];
            List<Image> images = new List<Image>();
            foreach(var filenameObject in filenameObjects)
            {
                Image image = GetImage(filenameObject as string);
                if(image!=null)
                {
                    images.Add(image);
                }
            }
            return images;
           
            
        }
        private Image GetImage(string filename)
        {
            var file = objectsDatabase.GridFS.FindOne(Query.EQ("filename", filename));

            if (file == null)
            {
                return null;
            }

            using (var stream = file.OpenRead())
            {
                var image = Image.FromStream(stream, true);
                return image;

            }
        }


        /*
            edit object where identifyingKey= identifyingValue (i.e., “location” = “home”, or “id” = 7).
            Leaning towards some stack overflow-ish tagging system where Grace’s aid is able to add objects to some general tag (home, school, but in addition things like food, toys)
        */
        private void modifyObject(string identifyingKey, string identifyingValue, string fieldName, object changedValue)
        {
            MongoCursor objectsToEdit = Get(Query.EQ(identifyingKey, identifyingValue));
            BsonValue bsonChangedValue = BsonValue.Create(changedValue);
            foreach (BsonDocument doc in objectsToEdit)
            {
                doc.Set(fieldName, bsonChangedValue);
                objectsCollection.Save(doc); //Save document back into the collection
            }
        }

        /*
         * Adds the list of tags to the tags array accessed through the key "tags"
         * Careful. Will currently allow adding the same tag multiple times
         * Reference: http://stackoverflow.com/questions/11553481/updating-elements-inside-of-an-array-within-a-bsondocument
         *            http://stackoverflow.com/questions/6260936/adding-bson-array-to-bsondocument-in-mongodb
         */
        public void addTags(string identifyingKey, string identifyingValue, List<string> tagsToAdd)
        {
            MongoCursor objectsToTag = Get(Query.EQ(identifyingKey, identifyingValue));
            foreach (BsonDocument doc in objectsToTag)
            {

                if (!doc.Contains("tags")) // i.e. it wasn't found as an array yet
                {
                    System.Console.WriteLine("Creating a tag array with an object for the first time.");
                    BsonArray tmp = new BsonArray { };
                    doc.Add("tags", tmp);
                }
                var tagArray = doc["tags"].AsBsonArray;
                foreach (string tagToAdd in tagsToAdd) //Iterates through strings to add, appending them to array
                {
                    tagArray.Add(tagToAdd);
                }
               objectsCollection.Save(doc);
            }
        }

        /*
            Clears tags specified by oldTags.
            Can add functionality to clearAllTags if it's needed
        */
        public void clearTags(string identifyingKey, string identifyingValue, List<string> tagsToRemove)
        {
            MongoCursor docsToClearTags = Get(Query.EQ(identifyingKey, identifyingValue));
            foreach (BsonDocument doc in docsToClearTags)
            {
                if (doc.Contains("tags")) //Tags array does exist
                {
                    BsonArray tagArray = doc["tags"].AsBsonArray;
                    int index;
                    foreach (string tagToRemove in tagsToRemove)
                    {
                        index = tagArray.IndexOf(tagToRemove);
                        if (index >= 0) //IndexOf returns -1 if not found
                        {
                            tagArray.RemoveAt(index);
                        }
                    }
                    objectsDatabase.GetCollection(collectionName).Save(doc);
                }
            }
        }

        /*
         * Returns a list of tags associated with an object that matches the key value pair passed in
         * Currently requiring the key value pair to return one unique element, will return null otherwise
         */
        public List<string> getTags(string identifyingKey, string identifyingValue)
        {
            MongoCursor matchedObjects = Get(Query.EQ(identifyingKey, identifyingValue));
            List<string> foundTags = new List<string>();
            if (matchedObjects.Size() == 1) //Not sure what we want to do about this. Do we want to return a list of tags for multiple objects?
            {
                foreach (BsonDocument doc in matchedObjects) //Cursor only works through iterating it seems. Ben? I may be wrong about this
                {
                    BsonArray tagArray = doc["tags"].AsBsonArray;
                    foreach (string tag in tagArray)
                    {
                        foundTags.Add(tag);
                    }
                }
            }
            else
            {
                return null;

            }
            return foundTags;
        }

        private MongoCursor retrieveRecentSelection()
        {

            return objectsCollection.FindAllAs<BsonDocument>();
        }

        //returns a cursor which points to the set of documents which match query
        private MongoCursor Get(IMongoQuery query)
        {
            return objectsCollection.FindAs<BsonDocument>(query);
        }
        //returns a cursor which points to the set of documents where key=value
        //TODO: process results/return something else?
        //      lt/gt/ne queries
        private MongoCursor Get(string key, string value)
        {
            return this.Get(Query.EQ(key, value));

        }
        
        //queryDict: a Dictionary of key-value pairs
        //returns a cursor which points to the set of elements which match the AND of every key-value pair
        //in queryDict
        private MongoCursor Get(Dictionary<string, string> queryDict)
        {
            var andList = new List<IMongoQuery>();
            foreach (KeyValuePair<string, string> entry in queryDict)
            {
                andList.Add(Query.EQ(entry.Key, entry.Value));
            }
            var query = new QueryBuilder<BsonDocument>();
            return this.Get(query.And(andList));
        }
        private void makeNewDocument(Image image, Dictionary<string, object> info)
        {

                if(!info.ContainsKey("name"))
                {
                    info.Add("name", "");
                }
                info.Add("count", 1);
                info.Add("timestamp", DateTime.Now);

                List<string> filenames = new List<string>();
                if(image!=null)
                {
                    string filename = InsertImage(image);
                    filenames.Add(filename);
                }
               
                info.Add("filenames", filenames);
                this.Insert(new BsonDocument(info));
        }


        /** Public interface functions for GUI/CV use **/
        //Saves object selected by gui
        //image = cropped image of object, info = key/value dict of identifying info about that object
        public void saveSelection(Image image, Dictionary<string, object> info)
        {
            if(!info.ContainsKey("name") || (string)info["name"]=="")
            {
                makeNewDocument(image, info);      
            }
            else{
                MongoCursor priorSelections = Get(Query.EQ("name", (string)info["name"]));
                if (priorSelections.Size() == 0) //First time seeing an object with this name
                {
                    makeNewDocument(image, info);
                }
                else
                {
                    foreach (BsonDocument doc in priorSelections) //Should only be one.
                    {
                        int changedCount = (int)doc["count"] + 1;
                        modifyObject("name", info["name"].ToString(), "count", changedCount);

                        if(info.ContainsKey("tags") && info["tags"]!=null)
                        {

                            List<string> tagObjects = info["tags"] as List<string>;
                            

                            modifyObject("name", info["name"].ToString(), "tags", tagObjects);
                        }
                        modifyObject("name", info["name"].ToString(), "timestamp", DateTime.Now);


                        if (image != null)
                        {
                            string filename = InsertImage(image);
                            object[] filenamesObjects = doc.ToDictionary()["filenames"] as object[];
                            var newFilenames = new BsonArray();
                            foreach (var filenameObject in filenamesObjects)
                            {
                                newFilenames.Add(filenameObject as string);
                            }
                            newFilenames.Add(filename);
                            modifyObject("name", info["name"].ToString(), "filenames", newFilenames);
                        }
                                            
                    }
                }        
            }

            


        }


       


        //Updates a selection with the given id, to have the given key/value pair in its info
        //If the selection already has a value for the given key, it will be replaced by the new
        //value, otherwise the new key will be added to the selection's info
        public void updateSelectionInfo(string id, string key, object value)
        {
            BsonDocument matchedDoc = objectsCollection.FindOneByIdAs<BsonDocument>(new ObjectId(id));
            if (matchedDoc == null)
            {
                return;
            }

            BsonValue bsonValue = BsonValue.Create(value);
            matchedDoc.Set(key, bsonValue);
            objectsCollection.Save(matchedDoc);

        }
        //Updates a selection with the given id to have newInfo as it's info
        //Will completely overwrite old info for the selection
        private void updateSelectionInfo(string id, Dictionary<string, object> newInfo)
        {
            
            BsonDocument document = new BsonDocument(newInfo);
            document.Set("_id", new ObjectId(id));
            objectsCollection.Save(document);
        }
        private int compareDocsTimestamp(Dictionary<string, object> dict1, Dictionary<string, object> dict2)
        {
            int frequencyDiff = (int)(dict1["count"]) - (int)(dict2["count"]);
            double timeDiff = ((DateTime)(dict1["timestamp"]) - (DateTime)(dict2["timestamp"])).TotalDays;

            return (int)Math.Floor(0.5 * frequencyDiff + 0.5 * timeDiff);



        }
        //get a vobject where key=value
        public VObject getOne(string key, string value)
        {
            var dict = objectsCollection.FindOneAs<BsonDocument>(Query.EQ(key, value)).ToDictionary();
            return new VObject(dict);
        }
        
        //return previous selections where key=value
        public List<VObject> getSelections(string key, string value)
        {

            List<VObject> vObjects = new List<VObject>();
            var matchedDocs = objectsCollection.FindAs<BsonDocument>(Query.EQ(key, value));
            foreach (BsonDocument doc in matchedDocs)
            {
                
                List<Image> images = GetImages(doc);
                foreach(var image in images)
                {
                    Dictionary<string, object> dict = doc.ToDictionary();
                    dict.Add("image", image);
                    vObjects.Add(new VObject(dict));
                }
                
            }
            return vObjects;
        }
        //Retrieves all objects with a blank 'name' attribute
        //If no objects found without names, will return an empty list
        public List<VObject> getUnnamedObjects()
        {
            List<VObject> list = new List<VObject>();

            MongoCursor cursor = Get(Query.EQ("name", ""));
            foreach (BsonDocument document in cursor)
            {
                 List<Image> images = GetImages(document);
                 foreach (var image in images)
                 {
                     Dictionary<string, object> listItem = document.ToDictionary();
                     listItem.Add("image", image);
                     list.Add(new VObject(listItem));
                 }
            }
            return list;

        }

        public List<VObject> getAllObjects()
        {
            
            MongoCursor cursor = objectsCollection.FindAllAs<BsonDocument>();

            List<VObject> list = new List<VObject>();
            foreach (BsonDocument document in cursor)
            {               
                List<Image> images = GetImages(document);
                foreach (var image in images)
                {
                    Dictionary<string, object> listItem = document.ToDictionary();
                    listItem.Add("image", image);
                    list.Add(new VObject(listItem));
                }
            }
            return list;

        }

        public List<VObject> getAllUniqueObjects()
        {
            MongoCursor cursor = objectsCollection.FindAllAs<BsonDocument>();

            List<VObject> list = new List<VObject>();
            foreach (BsonDocument document in cursor)
            {
                List<Image> images = GetImages(document);
               
                Dictionary<string, object> listItem = document.ToDictionary();
                listItem.Add("image", images[0]);
                list.Add(new VObject(listItem));
                
            }
            return list;
        }

       
        //given a list of tags describing a current context, return a list of objects that are tagged
        //with these tags
        public List<VObject> getLikelyObjects(List<string> tags, int maxResults = 99)
        {
            List<Dictionary<string, object>> likelyObjects = new List<Dictionary<string, object>>();

            
            var query = queryFromString("{tags:{ $in: " + tagsToString(tags) + "}}");

            MongoCursor docsFound = Get(query);
            foreach (BsonDocument doc in docsFound)
            {
                likelyObjects.Add(doc.ToDictionary());
            }
            likelyObjects.Sort(compareDocsTimestamp);
            likelyObjects.Reverse();
            if (likelyObjects.Count > maxResults)
            {
                likelyObjects.RemoveRange(maxResults, likelyObjects.Count - maxResults);
            }


            List<VObject> vObjects = new List<VObject>();
            foreach(var dict in likelyObjects)
            {
                Dictionary<string, object> vObjDict = dict;
                object[] filenameObjects = dict["filenames"] as object[];
                Image image = null;
                if(filenameObjects.Length>0)
                {
                    string filename = filenameObjects[0] as string;
                    image = GetImage(filename);
                }
                vObjDict.Add("image", image);
                vObjects.Add(new VObject(vObjDict));
            }

            return vObjects;
        }


        public List<string> getAllTags()
        {
            
            MongoCursor cursor = Get(queryFromString("{\"tags\" : {\"$exists\": true}}"));

            HashSet<string> tags = new HashSet<string>();
            foreach (BsonDocument document in cursor)
            {
                Dictionary<string, object> listItem = document.ToDictionary();
                object[] itemTags = listItem["tags"] as object[];
                foreach (object itemTag in itemTags)
                {
                    tags.Add(itemTag as string);
                }
                
            }
            return tags.ToList<string>();
        }
        
        //removes object with given id from collection
        public void deleteObject(string _id)
        {
            BsonDocument docToDelete = objectsCollection.FindOneByIdAs<BsonDocument>(new ObjectId(_id));
            if(docToDelete!=null)
            {
               
                var dict = docToDelete.ToDictionary();
                if(dict.ContainsKey("filename"))
                {
                    objectsDatabase.GridFS.Delete(dict["filename"] as string);
                }
            }
            objectsCollection.Remove(Query.EQ("_id", new ObjectId(_id)));
        }
        

        public void clearCollection()
        {
            objectsCollection.RemoveAll();
            
        }
    }

}
