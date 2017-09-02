using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace DiceRoller.Win10.Services
{
    /// <summary>
    /// File service that reads and writes text files.
    /// </summary>
    public class TextFileService
    {
        /// <summary>
        /// Reads the text file and returns the data as string.
        /// </summary>
        /// <param name="filename">File name to read</param>
        /// <param name="knownFolder">Known folder to use; default to local folder</param>
        /// <returns>Data as a string</returns>
        public async Task<string> ReadFileAsync(string filename, DefaultFolderType knownFolder = DefaultFolderType.Local)
        {
            StorageFolder folder = this.GetStorageFolder(knownFolder);
            return await this.ReadFileAsync(folder, filename);
        }

        /// <summary>
        /// Reads the text file and returns the data as string.
        /// </summary>
        /// <param name="folder">Folder to get file from</param>
        /// <param name="filename">File name to read</param>
        /// <returns>Data as a string</returns>
        public async Task<string> ReadFileAsync(StorageFolder folder, string filename)
        {
            try
            {
                StorageFile file = await folder.GetFileAsync(filename);
                return await FileIO.ReadTextAsync(file);
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine(string.Format("File: {0} does not exist. Trying to read it before it was created.", filename));
                return string.Empty;
            }
        }

        /// <summary>
        /// Writes the text data to the specified file.
        /// </summary>
        /// <param name="filename">File name to write data to</param>
        /// <param name="data">String data to save</param>
        /// <param name="knownFolder">Known folder to use; default to local folder</param>
        /// <returns>Task</returns>
        public async Task WriteFileAsync(string filename, string data, DefaultFolderType knownFolder = DefaultFolderType.Local)
        {
            StorageFolder folder = this.GetStorageFolder(knownFolder);
            await this.WriteFileAsync(folder, filename, data);
        }

        /// <summary>
        /// Writes the text data to the specified file.
        /// </summary>
        /// <param name="folder">Folder to write file to</param>
        /// <param name="filename">File name to write data to</param>
        /// <param name="data">String data to save</param>
        /// <returns>Task</returns>
        public async Task WriteFileAsync(StorageFolder folder, string filename, string data)
        {
            IStorageItem item = await folder.TryGetItemAsync(filename);
            StorageFile file = null;

            if (item != null)
            {
                file = await folder.GetFileAsync(filename);
            }
            else
            {
                file = await folder.CreateFileAsync(filename);
            }

            await FileIO.WriteTextAsync(file, data);
        }

        #region Helper methods

        /// <summary>
        /// Gets the appropriate StorageFolder based on specified folder type.
        /// </summary>
        /// <param name="folderType">Type of folder to get.</param>
        /// <returns>Returns corresponding storage folder, or null if not found.</returns>
        private StorageFolder GetStorageFolder(DefaultFolderType folderType)
        {
            switch (folderType)
            {
                case DefaultFolderType.Local:
                    return Windows.Storage.ApplicationData.Current.LocalFolder;

                case DefaultFolderType.LocalCache:
                    return Windows.Storage.ApplicationData.Current.LocalCacheFolder;

                case DefaultFolderType.SharedLocal:
                    return Windows.Storage.ApplicationData.Current.SharedLocalFolder;

                case DefaultFolderType.Roaming:
                    return Windows.Storage.ApplicationData.Current.RoamingFolder;

                case DefaultFolderType.Temporary:
                    return Windows.Storage.ApplicationData.Current.TemporaryFolder;
            }

            return null;
        }
        #endregion

        #region DefaultFolderType enum

        public enum DefaultFolderType
        {
            Local = 0,
            LocalCache = 1,
            SharedLocal = 2,
            Roaming = 3,
            Temporary = 4
        }
        #endregion
    }
}
