using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class Approve
    {
        private int approveID;
        private bool approved;
        private DateTime approveDate;
        private int approveUserID;
        private string remark;
        private string title;
        private int filedocID;
        private bool isActive;
        private bool isNew;

        #region fields
        public int ApproveID
        {
            get { return approveID; }
            set { approveID = value; }
        }

        public bool Approved
        {
            get { return approved; }
            set { approved = value; }
        }

        public DateTime ApproveDate
        {
            get { return approveDate; }
            set { approveDate = value; }
        }

        public int ApproveUserID
        {
            get { return approveUserID; }
            set { approveUserID = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public int FiledocID
        {
            get { return filedocID; }
            set { filedocID = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        #endregion fields

        public void GetListAllApproveByFiledocID(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT    approve.approveID, approve.approve, approve.approveDate, approve.approveUserID, approve.remark, approve.title, users.fio
FROM         approve INNER JOIN users ON approve.approveUserID = users.userID WHERE     (approve.isActive = '1') AND (approve.filedocID = '{0}')", FiledocID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 7891");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllApproveByFiledocID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

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
                    MessageBox.Show("нет связи с сервером - 5455");
                    return false;
                }
                SqlCommand cmd;
                cmd = IsNew ? new SqlCommand(@"INSERT INTO approve
                      (approve, approveDate, approveUserID, remark, title, isActive, filedocID)
VALUES     (@approve,@approveDate,@approveUserID,@remark,@title,@isActive,@filedocID); SELECT @@IDENTITY AS 'ID';", con)
                             : new SqlCommand(@"UPDATE    approve
SET              approve = @approve, approveDate = @approveDate, approveUserID = @approveUserID, remark = @remark, title = @title, filedocID = @filedocID, isActive = @isActive
WHERE     (approveID = @approveID)", con);

                if (IsNew)
                {
                    cmd.Parameters.Add("@approve", SqlDbType.Bit).Value = Approved;
                    cmd.Parameters.Add("@approveDate", SqlDbType.DateTime).Value = ApproveDate;
                    cmd.Parameters.Add("@approveUserID", SqlDbType.Int).Value = ApproveUserID;
                    cmd.Parameters.Add("@remark", SqlDbType.NVarChar, 200).Value = Remark;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar, 50).Value = Title;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@filedocID", SqlDbType.Int).Value = FiledocID;
                }
                else
                {
                    cmd.Parameters.Add("@approve", SqlDbType.Bit).Value = Approved;
                    cmd.Parameters.Add("@approveDate", SqlDbType.DateTime).Value = ApproveDate;
                    cmd.Parameters.Add("@approveUserID", SqlDbType.Int).Value = ApproveUserID;
                    cmd.Parameters.Add("@remark", SqlDbType.NVarChar, 200).Value = Remark;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar, 50).Value = Title;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@filedocID", SqlDbType.Int).Value = FiledocID;
                    cmd.Parameters.Add("@approveID", SqlDbType.Int).Value = ApproveID;
                }



                if (IsNew)
                    ApproveID = Convert.ToInt32(cmd.ExecuteScalar()); //id созданной записи - SELECT @@IDENTITY AS 'ID';
                else
                    cmd.ExecuteNonQuery();
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
    }
}
