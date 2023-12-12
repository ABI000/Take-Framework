namespace TakeFramework.IO
{
    public class FileUtilities
    {
        /// <summary>
        /// 路径是否存
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectory(string path)
        {
            return Directory.Exists(path);
        }
        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateDirectory(string path)
        {
            if (IsDirectory(path))
            {
                return false;
            }
            else
            {
                Directory.CreateDirectory(path);
                return true;
            }
        }
        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfo[] GetFiles(string path)
        {
            DirectoryInfo root = new(path);
            return root.GetFiles();
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfo[] GetFiles(string path, string searchPattern)
        {
            DirectoryInfo root = new(path);
            return root.GetFiles(searchPattern);
        }

        public FileSystemInfo[] GetFileSystemInfos(string path, string searchPattern, EnumerationOptions enumerationOptions)
        {
            DirectoryInfo root = new(path);
            return root.GetFiles(searchPattern, enumerationOptions);
        }
        public FileSystemInfo[] GetFileSystemInfos(string path, string searchPattern, SearchOption searchOption)
        {
            DirectoryInfo root = new(path);
            return root.GetFiles(searchPattern, searchOption);
        }
    }
}
