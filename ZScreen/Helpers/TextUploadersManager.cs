﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ZSS.Helpers
{
    [Serializable]
    public class TextUploadersManager
    {

        public List<object> TextUploadersSettings = new List<object>();

        public void Write()
        {
            WriteBF(Program.TextUploadersFilePath);
        }

        public void WriteBF(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    bf.Serialize(fs, this);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        private void WriteXML(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                XmlSerializer xs = new XmlSerializer(typeof(TextUploadersManager));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    xs.Serialize(fs, this);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public static TextUploadersManager Read()
        {
            return ReadBF(Program.TextUploadersFilePath);
        }

        public static TextUploadersManager ReadBF(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (File.Exists(filePath))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        return (TextUploadersManager)bf.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
                }
            }

            return new TextUploadersManager();
        }

        private static TextUploadersManager ReadXML(string filePath)
        {

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (File.Exists(filePath))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(TextUploadersManager));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        return (TextUploadersManager)xs.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
                }
            }

            return new TextUploadersManager();
        }

    }
}
