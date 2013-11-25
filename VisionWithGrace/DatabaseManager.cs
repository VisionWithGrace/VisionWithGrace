using System;
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
using System.Threading;
using System.Net;
using System.Net.Sockets;



/**
 * TODO:
 * If objects other than strings need to be stored, create necessary methods
 * and serialization map: http://docs.mongodb.org/ecosystem/tutorial/serialize-documents-with-the-csharp-driver/
 * 
 * Failure functions when get/insert fails
 * 
 * Image storing/retrieving
 * Some useful links:
 * http://docs.mongodb.org/manual/core/gridfs/
 * http://stackoverflow.com/questions/4988436/mongodb-gridfs-with-c-how-to-store-files-such-as-images
 * 
**/



/**gui interface:
 * saveSelectedImage(Image image, string identifyingInfo)
 * 
 * editObject(key, value, string newInfo) edit object where key=value
 * 
 * getUnidentifiedObjects()
 * 
 * getRecentObjects() last objects added
 * 
 * getLikelyObjects(key, value) likely objects based on some parameter (time, tag, etc)
 *                              heuristic logic goes here 
 * 
 * 
 * 
 */

namespace DatabaseModule
{
    
    public class DatabaseManager
    {
        
        private string mongodir = @"..\..\..\mongodb\bin";
        private Process mongod;
        // private MongoCollection recognizedObjects;
        public DatabaseManager()
        {
          
          
        }

        ~DatabaseManager()
        {
            killMongoProcess();
        }
        private bool mongoIsRunning()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (process.ProcessName == "mongod")
                {
                    mongod = process;
                    return true;
                }
                
            }
            return false;
        }
        private bool mongoListeningOnPort()
        {
            
    
            string hostname = "localhost";
            int portno = 27017;
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(hostname, portno);
                tcpClient.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
    
        }
        //launch the mongo process on the user's computer
        //assuming the mongo directory has a data\db directory for mongo inside
        //returns true if everything is ok, false if there was an error starting mongo
        public bool startMongoProcess()
        {
            if (mongoIsRunning())
            {
                return true;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.FileName = mongodir + @"\mongod.exe";
            start.WindowStyle = ProcessWindowStyle.Normal;
            var directory = Directory.GetCurrentDirectory();
            var mongoDataDir = mongodir + @"\data\db";
            if (!Directory.Exists(mongoDataDir))
            {
                Directory.CreateDirectory(mongoDataDir);
            }
            start.Arguments = "--dbpath " + mongoDataDir;
            mongod = Process.Start(start);

            Thread.Sleep(500);
            return mongoListeningOnPort();


           
        }
        public void killMongoProcess()
        {
            if (!mongod.HasExited)
            {
                mongod.Kill();
            }
        }
    }
}
