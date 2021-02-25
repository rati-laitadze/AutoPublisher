using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FirstRealTask
{
    public static class MyMethods
    {
        public static void Archive(string path, string newPath)
        {

            using (ZipFile zip = new ZipFile())
            {

                zip.AddDirectory(path);
                zip.Save(newPath);
            }

        }
        public static void IIS(Indicator indicator)
        {
            ServiceController controller = new ServiceController();
            controller.MachineName = ".";
            controller.ServiceName = "w3svc";
            string status = controller.Status.ToString();


            if (indicator == Indicator.Off)
            {   // Stop the service
                controller.Stop();
            }
            else
            {
                // Start the service
                controller.Start();
            }
        }
        public static void DeleteArchive(string path)
        {
            Directory.Delete(path, true);
        }


        //აღარ გახდა საჭირო
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
        public static void Unurchive(string path, string newPath)
        {
            using (ZipFile zip = ZipFile.Read(path))
            {
                foreach (ZipEntry zipEntry in zip)
                {
                    zipEntry.Extract(newPath);
                }
            }
        }
        public static void AutomaticPublisher(string projectPath, string newPath, string prodactionPath, string backupPath, string deleteProjectName, string fileName = @"\Zipped.zip")
        {
            MyMethods.Archive(projectPath, newPath);
            MyMethods.Archive(prodactionPath, backupPath);
            MyMethods.IIS(Indicator.Off);
            MyMethods.DeleteArchive(prodactionPath + @"\" + deleteProjectName);
            MyMethods.Unurchive(newPath, prodactionPath);
            MyMethods.IIS(Indicator.On);
        }
    }
}
