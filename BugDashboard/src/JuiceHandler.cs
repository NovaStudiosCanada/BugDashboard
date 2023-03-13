using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Windows;
using System.Diagnostics.Contracts;

namespace BugDashboard.src
{
    public class JuiceHandler
    {

        List<JuiceSession> juiceSessions = new List<JuiceSession>();

        public void GetAllSessions(List<string> juices)
        {
            //Get Directories For All Juice Folders\\
            foreach (string juice in juices)
            {
                //Get Juice\\
                DirectoryInfo newDir = GetJuiceDirectory(juice);

                //If Found\\
                if(newDir != null)
                {
                    JuiceSession newSession = new JuiceSession();
                    newSession.directory = newDir;
                    newSession.files = new List<FileInfo>(newDir.GetFiles());
                    juiceSessions.Add(newSession);
                }

            }            
        }


        public DirectoryInfo GetJuiceDirectory(string juice)
        {
            //Get Directory\\
            DirectoryInfo dir = new DirectoryInfo("C:\\Dev\\CSLearn\\BugDashboard\\BugDashboard\\Juice\\" + juice);

            //Check Directory Exists\\
            if (!dir.Exists) { return null; }

            return dir;
        }

        public List<FileInfo> GetJuiceFiles(DirectoryInfo juiceFolder)
        {
            //Check Directory Exists\\
            if (!juiceFolder.Exists) { return null; }

            //Get List\\
            List<FileInfo> files = new List<FileInfo>(juiceFolder.GetFiles());

            return files;

        }


        public string ReadFile(FileInfo file)
        {

            //Open file for Read\Write
            FileStream fs = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);

            //Read File Content\\
            string fileContent = sr.ReadToEnd();

            //Close StreamReaders\\
            sr.Close();
            fs.Close();

            return fileContent;
        }

        public JuiceSession GetJuiceSession(int index)
        {
            if(juiceSessions[index] != null)
            {
                return juiceSessions[index];
            }else
            {
                return null;
            }
            
        }

        public int CurrentSessionCount()
        {
            return juiceSessions.Count;
        }
    }
}
