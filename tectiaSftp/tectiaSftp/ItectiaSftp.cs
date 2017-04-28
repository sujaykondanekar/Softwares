using System.Collections.Generic;
namespace TectiaSftp
{
    /// <summary>
    /// 
    /// </summary>
    public interface ItectiaSftp
    {       
        //define the properties to be used for sftp
        /// <summary>
        /// Gets or sets the host IP address. This property has to be initialized for file transfer to work properly.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        string HOST { get; set; }
        /// <summary>
        /// Gets or sets the password used to connect to the SFTP server.This property has to be initialized for file transfer to work properly.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string PASSWORD { get; set; }
        /// <summary>
        /// Gets or sets the username used to connect to the SFTP server.This property has to be initialized for file transfer to work properly.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        string USERNAME { get; set; }
        /// <summary>
        /// Gets or sets the PORT used to connect to the SFTP server.Default if not assigned is 22
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        string PORT { get; set; }
        /// <summary>
        /// Creates the remote directory.This property has to be initialized for file transfer to work properly.
        /// </summary>
        /// <param name="dirctoryName">Name of the directory to be created.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to create a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void createRemoteDirectory(string dirctoryName, string parentDirectoryPath);
        /// <summary>
        /// Removes the remote directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory to be created.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to remove a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void removeRemoteDirectory(string directoryName, string parentDirectoryPath);
        /// <summary>
        /// Creates the local directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory to be create.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to create a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void createLocalDirectory(string directoryName, string parentDirectoryPath);
        /// <summary>
        /// Removes the local directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory to be removed.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to remove a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void removeLocalDirectory(string directoryName, string parentDirectoryPath);
        /// <summary>
        /// Removes the local directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory to be removed.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to remove a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <param name="Force">Force to remove the files in the directory</param>
        /// <returns></returns>
        void removeRemoteDirectory(string directoryName, string parentDirectoryPath,bool Force);
        /// <summary>
        /// Transfers the file.
        /// </summary>
        /// <param name="fileName">Name of the file to be transfered.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to transfer to a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void transferFile(string fullFileName, string fullDestinationfileName);
        /// <summary>
        /// Transfers the files.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to transfer to a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void transferFiles(List<string> fullFileNames, List<string> fullDestinationfileName);
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="fileName">Name of the file to be fetched.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to get file from a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void getFile(string fullFileName, string fullDestinationfileName);
        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="fileNames">The file names to be fetched.</param>
        /// <param name="parentDirectoryPath">The parent directory path. for example, if you need to get files from a directory "test" under the location "D:\ProgramFiles\", value for parentDirectoryPath="D:\ProgramFiles\" </param>
        /// <returns></returns>
        void getFiles(List<string> fullFileNames, List<string> fullDestinationfileNames);
        /// <summary>
        /// rename the server file.
        /// </summary>
        /// <param name="fullFileName">The full path file name to be renamed.</param>
        /// <param name="newFilename">The new file name</param>
        /// <returns></returns>
        void renameFile(string fullFileName, string newFilename);
        /// <summary>
        /// delete the server file.
        /// </summary>
        /// <param name="fileName">The full path file name to be deleted.</param>     
        /// <returns></returns>
        void deleteFile(string fullFileName);
    }
}
