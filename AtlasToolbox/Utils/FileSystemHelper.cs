using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace AtlasToolbox.Utils
{
    public class FileSystemHelper
    {
        public static void AppendLastCharacterToFileName(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            string newFilePath = filePath + filePath[^1];
            string newFileName = Path.GetFileName(newFilePath)!;

            FileSystem.RenameFile(filePath, newFileName);
        }

        public static void TrimLastCharacterFromFileName(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            string newFilePath = filePath[..^1];
            string newFileName = Path.GetFileName(newFilePath)!;

            FileSystem.RenameFile(filePath, newFileName);
        }

        public static void AppendLastCharacterToDirectoryName(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            string newDirectoryPath = directoryPath + directoryPath[^1];
            string newDirectoryName = Path.GetDirectoryName(newDirectoryPath)!;

            FileSystem.RenameFile(directoryPath, newDirectoryName);
        }

        public static void TrimLastCharacterFromDirectoryName(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            string newDirectoryPath = directoryPath[..^1];
            string newDirectoryName = Path.GetDirectoryName(newDirectoryPath)!;

            FileSystem.RenameFile(directoryPath, newDirectoryName);
        }
    }
}
