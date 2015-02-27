using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace bies
{
    class utils
    {
        public static string DateToSqlString(DateTime date)
        {
            return date.ToString("yyyyMMdd 00:00:00");
        }

        public static string DateToSqlStoredProcedureString(DateTime date) //N'20110101'
        {
            return date.ToString("yyyyMMdd");
        }


        

        public static DateTime DateToZeroTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        public static string SetStringDB(string Name)
        {
            return Name.Replace("\'", "''");
        }

        public static int SetBitDB(bool val)
        {
            return val ? 1 : 0;
        }

        public static string SetDoubleDB(double val)
        {
            return val.ToString().Replace(',', '.');
        }

        public static DataSet ImportFromExcelToDataSet(string FileName, bool hasHeaders)
        {
            string headers = hasHeaders ? "Yes" : "No";
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + headers + ";IMEX=1\"";

            DataSet outputDataSet = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in dt.Rows)
                {
                    string sheet = row["TABLE_NAME"].ToString();

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                    cmd.CommandType = CommandType.Text;

                    DataTable outputTable = new DataTable(sheet);
                    outputDataSet.Tables.Add(outputTable);
                    new OleDbDataAdapter(cmd).Fill(outputTable);
                }
            }
            return outputDataSet;
        }
        public static void ExportGV2Xls(string sFileName, DevExpress.XtraGrid.Views.Grid.GridView gvView, bool bTryToOpen, string caption)
        {
            try
            {
                //string sPath = String.Format("{0}\\{1}.xls", Environment.GetEnvironmentVariable("TEMP"), sFileName);
                string sPath = String.Format("{0}\\{1}{2}.xls", Environment.CurrentDirectory, sFileName, DateTime.Now.Second);
                var eoRunning = new XlsExportOptions(false, true);
                eoRunning.UseNativeFormat = true;
                //eoRunning.SheetName = caption;
                gvView.ExportToXls(sPath, eoRunning);
                if (bTryToOpen)
                {
                    try
                    {
                        using (var proc1 = new Process())
                        {
                            proc1.StartInfo.FileName = sPath;
                            proc1.StartInfo.Verb = "Open";
                            proc1.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            proc1.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Неизвестная проблема! Не могу открыть файл в приложении Excel!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Неизвестная проблема! Не могу сохранить таблицу в Excel!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void OpenFileInWordOrExcel(string FilePath, string FileName)
        {
            if (String.IsNullOrEmpty(FileName))
                return;

            string sPath = Path.Combine(FilePath, FileName);
            //string sPath = String.Format("{0}\\{1}.xls", Environment.GetEnvironmentVariable("TEMP"), sFileName);
            //string sPath = String.Format("{0}\\{1}{2}.xls", Environment.CurrentDirectory, sFileName, DateTime.Now.Second);
                try
                    {
                        using (var newProcess = new Process())
                        {
                            newProcess.StartInfo.FileName = sPath;
                            newProcess.StartInfo.Verb = "Open";
                            newProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            newProcess.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Неизвестная проблема! Не могу открыть файл!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
        }

        public static void SaveCompressedFile(string filename, string data) 
        {

            FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Compress);
            StreamWriter writer = new StreamWriter(compressionStream);
            writer.Write(data);
            writer.Close();

        }
        public static string LoadCompressedFile(string filename) 
          {

            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
            StreamReader reader = new StreamReader(compressionStream);
            string data = reader.ReadToEnd();
            reader.Close();
            return data;
          }
        
        /// <summary>
        /// Для сохранения файла из базы на диск!
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public static bool SaveBytesToFile(string path, byte[] data)
        {
            FileStream fout;
            //удаляем файл, если он существует
            if (File.Exists(path))
            {
                try
                {
                    var aFile = new FileInfo(path);
                    //aFile.Attributes = FileAttributes.ReadOnly | FileAttributes.Hidden;
                    aFile.Attributes = aFile.Attributes &~FileAttributes.ReadOnly; // убрать атрибут
                    File.Delete(path);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            
            try
            {
                fout = new FileStream(path, FileMode.Create);
            }
            catch (IOException exc)
            {
                MessageBox.Show(exc.Message + "\nError Opening Output File");
                return false;
            }

            // Write the alphabet to the file.
            try
            {
                foreach (byte b in data)
                {
                    fout.WriteByte(b);
                }
            }
            catch (IOException exc)
            {
                MessageBox.Show(exc.Message + "File Error");
                return false;
            }

            fout.Close();
            return true;
        }

        public static byte[] ReadFromFileToByte(string path)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);

            byte[] MyData = new byte[fs.Length];
            fs.Read(MyData, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return MyData;
        }

        public static bool FilesCopyFromTo(string sourceFile, string targetPath, string targetFileName, bool DeleteAfterCopy)
        {
            //var sourcePath = Environment.CurrentDirectory;  //@"C:\Users\Public\TestFolder";


            // Use Path class to manipulate file and directory paths.
            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!Directory.Exists(targetPath))
            {
                MessageBox.Show(String.Format("{0} - путь не верный", targetPath));
                return false;
            }
            //Copy files
            try
            {
                //string sourceFile = Path.Combine(sourcePath, fileNameFrom);
                string destFile = Path.Combine(targetPath, targetFileName);

                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                File.Copy(sourceFile, destFile, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            //Remove files after complete copied
            if (DeleteAfterCopy)
                try
                {
                    //var sourceFile = Path.Combine(sourcePath, fileNameFrom);
                    var aFile = new FileInfo(sourceFile);
                    //aFile.Attributes = FileAttributes.ReadOnly | FileAttributes.Hidden;
                    aFile.Attributes = aFile.Attributes & ~FileAttributes.ReadOnly; // убрать атрибут
                    File.Delete(sourceFile);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            return true;
        }

        //public static Point Parse(SqlString s)
        //{
        //    if (s.IsNull)
        //        return null;

        //    // Parse input string to separate out points.
        //    Point pt = new Point();
        //    string[] xy = s.Value.Split(",".ToCharArray());
        //    pt.X = Int32.Parse(xy[0]);
        //    pt.Y = Int32.Parse(xy[1]);
        //    return pt;
        //}
}
}
