using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class TypeDoc
    {
        private int typedocID;
        private string name;

        public int TypedocID
        {
            get { return typedocID; }
            set { typedocID = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public List<TypeDoc> GetListTypeDoc(List<InviteStatus> inviteStatus)
        {
            DataBase db = new DataBase();
            List<TypeDoc> result = new List<TypeDoc>();

            try
            {
                SqlConnection con = db.Connect();
                var request = @"SELECT [typedocID] ,[name] FROM [typedoc] where [isActive] = '1' order by sortNumber; 
SELECT [inviteStatusID], [name] FROM [bies].[dbo].[inviteStatus] where [isActive] = '1'";
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 25");
                    return null; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                DataTable dt2 = _ds.Tables[1];
                foreach (DataRow _dr in dt.Rows)
                {
                    TypeDoc typeDoc = new TypeDoc
                                          {
                                              TypedocID = Convert.ToInt32(_dr["typedocID"]),
                                              Name = (_dr["name"]).ToString()
                                          };
                    result.Add(typeDoc);
                }

                foreach (DataRow _dr in dt2.Rows)
                {
                    InviteStatus status = new InviteStatus
                                              {
                                                  InviteStatusID = Convert.ToInt32(_dr["inviteStatusID"]),
                                                  Name = (_dr["name"]).ToString()
                                              };
                    inviteStatus.Add(status);
                }
            }
            catch (SqlException e)
            {
                result = null;
                MessageBox.Show(e.ToString(), "ошибка GetListTypeDoc error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }

       
    }
}
