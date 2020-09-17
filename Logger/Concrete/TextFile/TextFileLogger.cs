using Log.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Log.Concrete.TextFile
{
    public class TextFileLogger : ILogger
    {
        private string _directoryPath = $"{Environment.CurrentDirectory}/Logs";
        private string _filePath = $"{Environment.CurrentDirectory}/Logs/{DateTime.Now.ToString("yyyy.MM.dd/HH.mm")}.txt";

        #region IO Methods
        private bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
        private void CreateFile(string filePath)
        {
            File.Create(filePath);
        }
        private bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        private void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }
        private void MakeFileControl()
        {
            if (IsExistDirectory(_directoryPath) == false)
                CreateDirectory(_directoryPath);

            if (IsExistFile(_filePath) == false)
                CreateFile(_filePath);
        }
        private void Write(string message, Exception exception = null)
        {
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.WriteLine(message, (exception != null) ? exception.Message : null);
            }
        }
        #endregion

        public void Log(string Message)
        {
            MakeFileControl();
            Write(Message);
        }

        public void Log(string Message, Exception exception)
        {
            MakeFileControl();
            Write(Message, exception);
        }
    }
}
