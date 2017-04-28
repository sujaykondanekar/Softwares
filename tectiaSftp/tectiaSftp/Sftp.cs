using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
/// <summary>
/// 
/// </summary>
[assembly: CLSCompliant(true)]
namespace TectiaSftp
{
    public sealed class Sftp : ItectiaSftp
    {
        string host, username, password,port;
       
        //define the properties to be used for sftp
        /// <summary>
        /// Gets or sets the host IP address. This property has to be initialized for file transfer to work properly.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        string ItectiaSftp.HOST
        {
            get
            {
                return host;
            }
            set
            {
                host = value;
            }
        }
        /// <summary>
        /// Gets or sets the password used to connect to the SFTP server.This property has to be initialized for file transfer to work properly.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string ItectiaSftp.PASSWORD
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        /// <summary>
        /// Gets or sets the username used to connect to the SFTP server.This property has to be initialized for file transfer to work properly.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        string ItectiaSftp.USERNAME
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        /// <summary>
        /// Gets or sets the PORT used to connect to the SFTP server.Default if not assigned is 22
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        string ItectiaSftp.PORT
        {
            get
            {
                return port;
            }
            set
            {
               if(null!=value)
                {
                    if(!String.IsNullOrEmpty(value))
                    {
                        port = value;
                    }
                    else
                    {
                        port = "22";
                    }
                }
               else
                {
                    port = "22";
                }
            }
        }      
        /// <summary>
        /// Creates the local directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory to be create.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to create a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void ItectiaSftp.createLocalDirectory(string directoryName, string parentDirectoryPath)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            string[] directories = parentDirectoryPath.TrimEnd(split).Split(split);
                            foreach (string item in directories)
                            {
                                writer.WriteLine("lcd " + item);
                            }
                            writer.WriteLine("lmkdir " + directoryName);
                            writer.WriteLine("lchmod 700 " + directoryName);
                            writer.WriteLine("quit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port);
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// Creates the remote directory.This property has to be initialized for file transfer to work properly.
        /// </summary>
        /// <param name="dirctoryName">Name of the directory to be created.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to create a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void ItectiaSftp.createRemoteDirectory(string directoryName, string parentDirectoryPath)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            string[] directories = parentDirectoryPath.TrimEnd(split).Split(split);
                            foreach (string item in directories)
                            {
                                writer.WriteLine("cd " + item);
                            }
                            writer.WriteLine("mkdir " + directoryName);
                            writer.WriteLine("chmod 700 " + directoryName);
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="fileName">Name of the file to be fetched.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to get file from a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void ItectiaSftp.getFile(string fullFileName, string fullDestinationfileName)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            int startindex = fullDestinationfileName.LastIndexOf(@"\") + 1;
                            int length = fullDestinationfileName.Length - startindex;
                            string fileName = fullDestinationfileName.Substring(startindex, length);
                            //cd into remote folders
                            string[] directories = fullDestinationfileName.Replace(fileName, "").TrimEnd(split).Split(split);
                            foreach (string item in directories)
                            {
                                writer.WriteLine("lmkdir \"" + item + "\"");
                                writer.WriteLine("lcd " + item);
                            }
                            //cd into local folders
                            startindex = fullFileName.LastIndexOf(@"\") + 1;
                            length = fullFileName.Length - startindex;
                            string srcFileName = fullFileName.Substring(startindex, length);
                            string[] srcDirectories = fullFileName.Replace(fileName, "").TrimEnd(split).Split(split);
                            foreach (string item in srcDirectories)
                            {
                                writer.WriteLine("cd \"" + item + "\"");
                            }
                            writer.WriteLine("get " + srcFileName);
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="fileNames">The file names to be fetched.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to get files from a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void ItectiaSftp.getFiles(List<string> fullFileNames, List<string> fullDestinationfileNames)
        {
            try
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                var stringChars = new char[8];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringChars);
                if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                {
                    File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                }
                using (Process process = new Process())
                {
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            string[] fileName = new string[fullDestinationfileNames.Count];
                            string[] srcFileName = new string[fullFileNames.Count];
                            string[] directories = null; string[] srcDirectories = null;
                            for (int i = 0; i < fullDestinationfileNames.Count; i++)
                            {
                                int startindex = fullDestinationfileNames[i].LastIndexOf(@"\") + 1;
                                int length = fullDestinationfileNames[i].Length - startindex;
                                fileName[i] = fullDestinationfileNames[i].Substring(startindex, length);

                                startindex = fullFileNames[i].LastIndexOf(@"\") + 1;
                                length = fullFileNames[i].Length - startindex;
                                srcFileName[i] = fullFileNames[i].Substring(startindex, length);
                                //cd into remote folders                              
                            }
                            for (int i = 0; i < fullDestinationfileNames.Count; i++)
                            {
                                directories = fullDestinationfileNames[i].Replace(fileName[i], "").TrimEnd(split).Split(split);
                                srcDirectories = fullFileNames[i].Replace(srcFileName[i], "").TrimEnd(split).Split(split);
                                foreach (string item in directories)
                                {
                                    writer.WriteLine("lmkdir \"" + item + "\"");
                                    writer.WriteLine("lcd \"" + item + "\"");
                                }
                                //cd into local folders                                                           
                                foreach (string item in srcDirectories)
                                {
                                    writer.WriteLine("cd \"" + item + "\"");
                                }
                                writer.WriteLine("get " + srcFileName[i]);
                            }
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// Removes the local directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory to be removed.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to remove a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void ItectiaSftp.removeLocalDirectory(string directoryName, string parentDirectoryPath)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            string[] directories = parentDirectoryPath.TrimEnd(split).Split(split);
                            foreach (string item in directories)
                            {
                                writer.WriteLine("lcd " + item);
                            }
                            writer.WriteLine("lrmdir " + directoryName);
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
        /// <summary>
        /// Removes the remote directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory to be created.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to remove a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void ItectiaSftp.removeRemoteDirectory(string directoryName, string parentDirectoryPath)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            string[] directories = parentDirectoryPath.TrimEnd(split).Split(split);
                            foreach (string item in directories)
                            {
                                writer.WriteLine("cd " + item);
                            }
                            writer.WriteLine("rm " + directoryName);
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }
        /// <summary>
        /// Removes the remote directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory to be removed.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to remove a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <param name="Force">Force to remove the files in the directory</param>
        /// <returns></returns>
        void ItectiaSftp.removeRemoteDirectory(string directoryName, string parentDirectoryPath, bool Force)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            string[] directories = parentDirectoryPath.TrimEnd(split).Split(split);
                            foreach (string item in directories)
                            {
                                writer.WriteLine("cd " + item);
                            }
                            writer.WriteLine("rm -r " + directoryName);
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Transfers the file.
        /// </summary>
        /// <param name="fileName">Name of the file to be transfered.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to transfer to a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void ItectiaSftp.transferFile(string fullFileName, string fullDestinationfileName)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;                            
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            int startindex = fullDestinationfileName.LastIndexOf(@"\") + 1;
                            int length = fullDestinationfileName.Length - startindex;
                            string fileName = fullDestinationfileName.Substring(startindex, length);
                            //cd into remote folders
                            string[] directories = fullDestinationfileName.Replace(fileName, "").TrimEnd(split).Split(split);
                            foreach (string item in directories)
                            {
                                writer.WriteLine("mkdir \"" + item + "\"");
                                writer.WriteLine("cd " + item);
                            }
                            //cd into local folders
                            startindex = fullFileName.LastIndexOf(@"\") + 1;
                            length = fullFileName.Length - startindex;
                            string srcFileName = fullFileName.Substring(startindex, length);
                            string[] srcDirectories = fullFileName.Replace(fileName, "").TrimEnd(split).Split(split);
                            foreach (string item in srcDirectories)
                            {
                                writer.WriteLine("lcd \"" + item + "\"");
                            }
                            writer.WriteLine("put " + srcFileName);
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        /// <summary>
        /// Transfers the files.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to transfer to a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void ItectiaSftp.transferFiles(List<string> fullFileNames, List<string> fullDestinationfileNames)
        {
            try
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                var stringChars = new char[8];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringChars);
                if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                {
                    File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                }
                using (Process process = new Process())
                {
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            string[] fileName = new string[fullDestinationfileNames.Count];
                            string[] srcFileName = new string[fullFileNames.Count];
                            string[] directories = null; string[] srcDirectories = null;
                            for (int i = 0; i < fullDestinationfileNames.Count; i++)
                            {
                                int startindex = fullDestinationfileNames[i].LastIndexOf(@"\") + 1;
                                int length = fullDestinationfileNames[i].Length - startindex;
                                fileName[i] = fullDestinationfileNames[i].Substring(startindex, length);

                                startindex = fullFileNames[i].LastIndexOf(@"\") + 1;
                                length = fullFileNames[i].Length - startindex;
                                srcFileName[i] = fullFileNames[i].Substring(startindex, length);
                                //cd into remote folders                              
                            }
                            for (int i = 0; i < fullDestinationfileNames.Count; i++)
                            {
                                directories = fullDestinationfileNames[i].Replace(fileName[i], "").TrimEnd(split).Split(split);
                                srcDirectories = fullFileNames[i].Replace(srcFileName[i], "").TrimEnd(split).Split(split);
                                foreach (string item in directories)
                                {
                                    writer.WriteLine("mkdir \"" + item + "\"");
                                    writer.WriteLine("cd \"" + item + "\"");
                                }
                                //cd into local folders                                                           
                                foreach (string item in srcDirectories)
                                {
                                    writer.WriteLine("lcd \"" + item + "\"");
                                }
                                writer.WriteLine("put " + srcFileName[i]);                                
                        }
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
        /// <summary>
        /// rename the server file.
        /// </summary>
        /// <param name="fullFileName">The file name to be renamed with full path.</param>
        /// <param name="newFilename">Destination file name.
        /// <returns></returns>
        void ItectiaSftp.renameFile(string fullFileName, string newFilename)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();                           
                            //cd into local folders
                            int startindex = fullFileName.LastIndexOf(@"\") + 1;
                            int length = fullFileName.Length - startindex;
                            string srcFileName = fullFileName.Substring(startindex, length);
                            string[] srcDirectories = fullFileName.Replace(srcFileName, "").TrimEnd(split).Split(split);
                            foreach (string item in srcDirectories)
                            {
                                writer.WriteLine("cd \"" + item + "\"");
                            }
                            writer.WriteLine("rename " + srcFileName+" "+ newFilename);
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// delete the server file.
        /// </summary>
        /// <param name="fileName">The full path file name to be deleted.</param>     
        /// <returns></returns>
        void ItectiaSftp.deleteFile(string fullFileName)
        {
            try
            {
                using (Process process = new Process())
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var finalString = new String(stringChars);
                    if (File.Exists(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat");
                    }
                    using (FileStream fs = new FileStream(Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = false;
                            process.StartInfo.RedirectStandardError = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.WorkingDirectory = Environment.SystemDirectory;
                            process.StartInfo.FileName = "cmd.exe";
                            writer.WriteLine("open " + username + "@" + host);
                            char[] split = Convert.ToString(@"\").ToCharArray();
                            //cd into local folders
                            int startindex = fullFileName.LastIndexOf(@"\") + 1;
                            int length = fullFileName.Length - startindex;
                            string srcFileName = fullFileName.Substring(startindex, length);
                            string[] srcDirectories = fullFileName.Replace(srcFileName, "").TrimEnd(split).Split(split);
                            foreach (string item in srcDirectories)
                            {
                                writer.WriteLine("cd \"" + item + "\"");
                            }
                            writer.WriteLine("rm -r " + srcFileName);
                            writer.WriteLine("quit");
                            writer.WriteLine("exit");
                            writer.Flush();
                            // Redirects the standard input so that commands can be sent to the shell.
                            process.StartInfo.RedirectStandardInput = true;
                            process.Start();
                            process.StandardInput.WriteLine("sftpg3.exe -B " + Environment.GetEnvironmentVariable("temp") + @"\" + finalString + ".bat" + " --password=" + password + " -P "+port );
                            process.StandardInput.Flush();
                            process.WaitForExit(5000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
