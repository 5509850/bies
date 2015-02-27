using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;

namespace bies.Entity
{
    class PaymentBaseCatalog
    {
        private string name;
        private int id;
        private bool isActive;
        private bool isNew;
        private int pOblastID;
        private int pCategoryworkID;


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public int POblastID
        {
            get { return pOblastID; }
            set { pOblastID = value; }
        }

        public int PCategoryworkID
        {
            get { return pCategoryworkID; }
            set { pCategoryworkID = value; }
        }

        public bool CreateOrUpdate(string table_name)
        {
            DataBase db = new DataBase();
            string request;
            
            request = IsNew
                          ?
                              String.Format(@"INSERT INTO {0} (name, isActive)
VALUES     ('{1}','{2}')", table_name, utils.SetStringDB(Name), utils.SetBitDB(IsActive))
                            :
                            String.Format(@"UPDATE {0} SET name = '{1}', isActive = '{2}'
WHERE     (ID = '{3}')", table_name, utils.SetStringDB(Name), utils.SetBitDB(IsActive), Id);

            return db.ExecuteSql(request);
        }

        public bool CreateOrUpdate(string table_name, bool isOblastOrWork)
        {
            DataBase db = new DataBase();
            string request = "";
            if (isOblastOrWork) //Oblast
            {
                request = IsNew
                          ?
                              String.Format(@"INSERT INTO {0} (name, isActive, pOblastID)
VALUES     ('{1}','{2}', '{3}')", table_name, utils.SetStringDB(Name), utils.SetBitDB(IsActive), POblastID)
                            :
                            String.Format(@"UPDATE {0} SET name = '{1}', isActive = '{2}', pOblastID = '{3}'
WHERE     (ID = '{4}')", table_name, utils.SetStringDB(Name), utils.SetBitDB(IsActive), POblastID, Id);
                
                
            }
            else //CatWork
            {
                request = IsNew
                         ?
                             String.Format(@"INSERT INTO {0} (name, isActive, pCategoryworkID)
VALUES     ('{1}','{2}', '{3}')", table_name, utils.SetStringDB(Name), utils.SetBitDB(IsActive), PCategoryworkID)
                           :
                           String.Format(@"UPDATE {0} SET name = '{1}', isActive = '{2}', pCategoryworkID = '{3}'
WHERE     (ID = '{4}')", table_name, utils.SetStringDB(Name), utils.SetBitDB(IsActive), PCategoryworkID, Id);
            }


            return !request.Equals(String.Empty) && db.ExecuteSql(request);
        }

        /*
         
*/

        public void Load(GridControl gc, string table_name)
        {
            DataBase db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT  * FROM  {0}", table_name);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 3322");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString(), "ошибка Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public void Load(GridControl gc, string table_name, List<ComboBoxItems> comboBoxItems)
        {
            DataBase db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                String table_name_combobox = table_name;
                if (table_name.Equals("pContract"))
                {
                    table_name_combobox = "pOblast";
                }

                if (table_name.Equals("pSubcategorywork"))
                {
                    table_name_combobox = "pCategorywork";
                }

                var request = String.Format(@"SELECT  * FROM  {0}; SELECT  * FROM  {1}", table_name, table_name_combobox);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 3322");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
                foreach (DataRow row in _ds.Tables[1].Rows)
                {
                    var ComboBoxItem = new ComboBoxItems
                                                     {
                                                         Id = Convert.ToInt32(row["ID"]),
                                                         Name = row["name"].ToString()
                                                     };
                    comboBoxItems.Add(ComboBoxItem);
                }
            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString(), "ошибка Load2 error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }
        
    }
}
