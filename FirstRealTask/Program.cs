using Ionic.Zip;
using System;
using System.IO;
using System.ServiceProcess;

namespace FirstRealTask
{
    class Program
    {
        static void Main(string[] args)
        {
          
        
            string path =          @"C:\Users\admin\Desktop\TestPublish"; //projectPath 
            

            string newpath =    @"C:\Users\admin\Desktop\testzip\Zipped.zip"; //newPath

            string prodaction = @"C:\Users\admin\Desktop\TestPublishProdaction"; //prodactionPath
            string backup =     @"C:\Users\admin\Desktop\Backup\Zipped.zip";  //backupPath
       
          MyMethods.AutomaticPublisher(path, newpath, prodaction, backup, "Project");
        }
    }
}