using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class Files
    {
        private int fileID;
        private string title;
        private string filename;
        private byte[] body;
        private DateTime datecreate;
        private DateTime datelastupdate;
        private bool signed;
        private bool isFileInBaseOrInShare;
        private bool isNew;
        private bool isTemplate;

        public int FileID
        {
            get { return fileID; }
            set { fileID = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public byte[] Body
        {
            get { return body; }
            set { body = value; }
        }

        public DateTime Datecreate
        {
            get { return datecreate; }
            set { datecreate = value; }
        }

        public DateTime Datelastupdate
        {
            get { return datelastupdate; }
            set { datelastupdate = value; }
        }

        public bool Signed
        {
            get { return signed; }
            set { signed = value; }
        }

        public bool IsFileInBaseOrInShare
        {
            get { return isFileInBaseOrInShare; }
            set { isFileInBaseOrInShare = value; }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public bool IsTemplate
        {
            get { return isTemplate; }
            set { isTemplate = value; }
        }

        //http://www.akadia.com/services/dotnet_read_write_blob.html
        public bool SaveOrUpdate()
        {
            DataBase db = new DataBase();
            string request;
            bool result = false;
            try
            {
                SqlConnection con = db.Connect();
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 55");
                    return false;
                }
                SqlCommand cmd;
                cmd = IsNew ? new SqlCommand(@"insert into files(title, filename, body, datecreate, datelastupdate, signed, isFileInBaseOrInShare)
                                    values(@title,@filename,@body,@datecreate,@datelastupdate,@signed, @isFileInBaseOrInShare); SELECT @@IDENTITY AS 'ID';", con)
                             : new SqlCommand(@"UPDATE    files SET  title = @title, filename = @filename, body = @body, datecreate = @datecreate, datelastupdate = @datelastupdate, signed = @signed, isFileInBaseOrInShare = @isFileInBaseOrInShare
                                    WHERE     (fileID = @fileID)", con);

                

                if (IsNew)
                {
                cmd.Parameters.Add("@title", SqlDbType.NVarChar, 100).Value = Title;

                cmd.Parameters.Add("@filename", SqlDbType.NVarChar, 100).Value = Filename;
                cmd.Parameters.Add("@body", SqlDbType.Image, Body.Length).Value = Body;

                cmd.Parameters.Add("@datecreate", SqlDbType.DateTime).Value = Datecreate;
                cmd.Parameters.Add("@datelastupdate", SqlDbType.DateTime).Value = Datelastupdate;
                cmd.Parameters.Add("@signed", SqlDbType.Bit).Value = Signed;
                cmd.Parameters.Add("@isFileInBaseOrInShare", SqlDbType.Bit).Value = IsFileInBaseOrInShare;
                }
                else
                {
                    cmd.Parameters.Add("@fileID", SqlDbType.Int).Value = FileID;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar, 100).Value = Title;

                    cmd.Parameters.Add("@filename", SqlDbType.NVarChar, 100).Value = Filename;
                    cmd.Parameters.Add("@body", SqlDbType.Image, Body.Length).Value = Body;

                    cmd.Parameters.Add("@datecreate", SqlDbType.DateTime).Value = Datecreate;
                    cmd.Parameters.Add("@datelastupdate", SqlDbType.DateTime).Value = Datelastupdate;
                    cmd.Parameters.Add("@signed", SqlDbType.Bit).Value = Signed;
                    cmd.Parameters.Add("@isFileInBaseOrInShare", SqlDbType.Bit).Value = IsFileInBaseOrInShare;
                }



                if (IsNew)
                    FileID = Convert.ToInt32(cmd.ExecuteScalar()); //id созданной записи - SELECT @@IDENTITY AS 'ID';
                else
                    cmd.ExecuteNonQuery();
                
                
                //request = String.Format(@"SELECT top(1) fileID FROM  files  Order by fileID DESC");
                //SqlDataAdapter myData = new SqlDataAdapter(request, con);
                //var _ds = new DataSet();
                //myData.Fill(_ds);
                //DataTable dt = _ds.Tables[0];
                //foreach (DataRow _dr in dt.Rows)
                //{
                //    FileID = Convert.ToInt32(_dr["fileID"]);

                //}
                result = true;
            }
            catch (SqlException e)
            {
                result = false;
                MessageBox.Show(e.ToString(), "ошибка SaveOrUpdate error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            
            return result;
        }

        public bool Update()
        {
            
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE files SET  
title = '{1}', filename = '{2}', datecreate = '{3}', datelastupdate = '{4}', signed = '{5}', isFileInBaseOrInShare  = '{6}'
WHERE     (fileID = '{0}')", FileID, utils.SetStringDB(Title), utils.SetStringDB(Filename), utils.DateToSqlString(Datecreate), utils.DateToSqlString(Datelastupdate), utils.SetBitDB(Signed), utils.SetBitDB(IsFileInBaseOrInShare));
            /*
         * 
         */
            return db.ExecuteSql(request);
        }

        public byte[] GetFileBodyByFileID()
        {
            byte[] result = new byte[] {};
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 44565");
                    return result; // no connection}
                }
                var request = String.Format(@"SELECT body FROM files WHERE (fileID = '{0}')", FileID);
                var cmd = new SqlCommand(request, con);
                result = (byte[])cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllTenderDocsByTradeID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }



        public List<Files> GetListAllUsers()
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();
            List<Files> result = new List<Files>();
            try
            {
                SqlConnection con = db.Connect();
                var request =
                    @"SELECT     fileID, title, filename, body, datecreate, datelastupdate, signed, isFileInBaseOrInShare 
FROM         files WHERE     (isTemplate = '1')";
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 1435");
                    return null; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    Files files = new Files();
                    files.FileID = Convert.ToInt32(_dr["fileID"]);
                    files.Title = (_dr["title"]).ToString();
                    files.Filename = (_dr["filename"]).ToString();
                    files.Datecreate = Convert.ToDateTime(_dr["datecreate"]);
                    result.Add(files);
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllUsers error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }


        public bool SaveTemplate()
        {
            DataBase db = new DataBase();
            bool result = false;
            try
            {
                SqlConnection con = db.Connect();
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 55");
                    return false;
                }
                SqlCommand cmd;
                cmd = new SqlCommand(@"insert into files(title, filename, body, datecreate, datelastupdate, signed, isFileInBaseOrInShare, isTemplate)
                                    values(@title,@filename,@body,@datecreate,@datelastupdate,@signed, @isFileInBaseOrInShare, @isTemplate); SELECT @@IDENTITY AS 'ID';", con);
                
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar, 100).Value = Title;

                    cmd.Parameters.Add("@filename", SqlDbType.NVarChar, 100).Value = Filename;
                    cmd.Parameters.Add("@body", SqlDbType.Image, Body.Length).Value = Body;

                    cmd.Parameters.Add("@datecreate", SqlDbType.DateTime).Value = Datecreate;
                    cmd.Parameters.Add("@datelastupdate", SqlDbType.DateTime).Value = Datelastupdate;
                    cmd.Parameters.Add("@signed", SqlDbType.Bit).Value = Signed;
                    cmd.Parameters.Add("@isFileInBaseOrInShare", SqlDbType.Bit).Value = IsFileInBaseOrInShare;
                    cmd.Parameters.Add("@isTemplate", SqlDbType.Bit).Value = IsTemplate;
                
                    FileID = Convert.ToInt32(cmd.ExecuteScalar()); //id созданной записи - SELECT @@IDENTITY AS 'ID';
                
                result = true;
            }
            catch (SqlException e)
            {
                result = false;
                MessageBox.Show(e.ToString(), "ошибка SaveOrUpdate error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }

            return result;
        }


        public bool DeleteTemplate()
        {

            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE files SET  
isTemplate = '{1}' WHERE     (fileID = '{0}')", FileID, utils.SetBitDB(false));
         
            return db.ExecuteSql(request);
        }
        
    }
}
