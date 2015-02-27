using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace bies.Entity
{
    public class Payment
    {
        private long paymentID;
        private int pZaimID;
        private DateTime datepay;
        private int typefundingID;
        private int pContragentID;
        private int pContractID;
        private int currencyID;
        private decimal amountUSD;
        private decimal amountBYR;
        private decimal amountEUR;
        private decimal amountRUB;
        private int rateUSD;
        private int rateEUR;
        private int rateRUB;
        private int pCategorycontractID;
        private int pCategoryworkID;
        private int pSubcategoryworkID;
        private int pObjectID;
        private string pDescription;
        private bool isActive;
        private int pTypePaymentID;
        private DateTime datelastupdate;
        private Users user;
        
        private bool isNew;

        public long PaymentID
        {
            get { return paymentID; }
            set { paymentID = value; }
        }

        public int PZaimID
        {
            get { return pZaimID; }
            set { pZaimID = value; }
        }

        public DateTime Datepay
        {
            get { return datepay; }
            set { datepay = value; }
        }

        public int TypefundingID
        {
            get { return typefundingID; }
            set { typefundingID = value; }
        }

        public int PContragentID
        {
            get { return pContragentID; }
            set { pContragentID = value; }
        }

        public int PContractID
        {
            get { return pContractID; }
            set { pContractID = value; }
        }

        public int CurrencyID
        {
            get { return currencyID; }
            set { currencyID = value; }
        }

        public decimal AmountUSD
        {
            get { return amountUSD; }
            set { amountUSD = value; }
        }

        public decimal AmountBYR
        {
            get { return amountBYR; }
            set { amountBYR = value; }
        }

        public decimal AmountEUR
        {
            get { return amountEUR; }
            set { amountEUR = value; }
        }

        public decimal AmountRUB
        {
            get { return amountRUB; }
            set { amountRUB = value; }
        }

        public int RateUSD
        {
            get { return rateUSD; }
            set { rateUSD = value; }
        }

        public int RateEUR
        {
            get { return rateEUR; }
            set { rateEUR = value; }
        }

        public int RateRUB
        {
            get { return rateRUB; }
            set { rateRUB = value; }
        }

        public int PCategorycontractID
        {
            get { return pCategorycontractID; }
            set { pCategorycontractID = value; }
        }

        public int PCategoryworkID
        {
            get { return pCategoryworkID; }
            set { pCategoryworkID = value; }
        }

        public int PSubcategoryworkID
        {
            get { return pSubcategoryworkID; }
            set { pSubcategoryworkID = value; }
        }

        public int PObject
        {
            get { return pObjectID; }
            set { pObjectID = value; }
        }

        public string PDescription
        {
            get { return pDescription; }
            set { pDescription = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public int PTypePayment
        {
            get { return pTypePaymentID; }
            set { pTypePaymentID = value; }
        }

        public DateTime Datelastupdate
        {
            get { return datelastupdate; }
            set { datelastupdate = value; }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public Users User
        {
            get { return user; }
            set { user = value; }
        }

        public List<ComboBoxItems> comboBoxItemsZaim;
        public List<ComboBoxItems> comboBoxCurrency;
        public List<ComboBoxItems> comboBoxpContragent;
        public List<ComboBoxItems> comboBoxpContract;
        public List<ComboBoxItems> comboBoxpCategorycontract;
        public List<ComboBoxItems> comboBoxpCategorywork;
        public List<ComboBoxSubItems> comboBoxpSubcategorywork;
        public List<ComboBoxItems> comboBoxpObject;

        public void LoadDataForNewPayment()
        {
            DataBase db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();

                var request = String.Format(@"SELECT  * FROM  {0} WHERE isActive = '1'; 
SELECT     currencyID AS 'ID', name FROM {1}; SELECT  * FROM  {2} WHERE isActive = '1';  SELECT  * FROM  {3} WHERE isActive = '1'; SELECT  * FROM  {4} WHERE isActive = '1'; SELECT  * FROM  {5} WHERE isActive = '1'; SELECT  * FROM  {6} WHERE isActive = '1'; SELECT  * FROM  {7} WHERE isActive = '1';",
"pZaim", "currency", "pContragent", "pContract", "pCategorycontract", "pCategorywork", "pObject", "pSubcategorywork");
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 3322");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                comboBoxItemsZaim = new List<ComboBoxItems>();
                comboBoxCurrency = new List<ComboBoxItems>();
                comboBoxpContragent = new List<ComboBoxItems>();
                comboBoxpContract = new List<ComboBoxItems>();
                comboBoxpCategorycontract = new List<ComboBoxItems>();
                comboBoxpCategorywork = new List<ComboBoxItems>();
                comboBoxpObject = new List<ComboBoxItems>();
                comboBoxpSubcategorywork = new List<ComboBoxSubItems>();

                foreach (DataRow row in _ds.Tables[0].Rows)
                {
                    var ComboBoxItem = new ComboBoxItems
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["name"].ToString()
                    };
                    comboBoxItemsZaim.Add(ComboBoxItem);
                }
                foreach (DataRow row in _ds.Tables[1].Rows)
                {
                    var ComboBoxItem = new ComboBoxItems
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["name"].ToString()
                    };
                    comboBoxCurrency.Add(ComboBoxItem);
                }
                foreach (DataRow row in _ds.Tables[2].Rows)
                {
                    var ComboBoxItem = new ComboBoxItems
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["name"].ToString()
                    };
                    comboBoxpContragent.Add(ComboBoxItem);
                }
                foreach (DataRow row in _ds.Tables[3].Rows)
                {
                    var ComboBoxItem = new ComboBoxItems
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["name"].ToString()
                    };
                    comboBoxpContract.Add(ComboBoxItem);
                }
                foreach (DataRow row in _ds.Tables[4].Rows)
                {
                    var ComboBoxItem = new ComboBoxItems
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["name"].ToString()
                    };
                    comboBoxpCategorycontract.Add(ComboBoxItem);
                }
                foreach (DataRow row in _ds.Tables[5].Rows)
                {
                    var ComboBoxItem = new ComboBoxItems
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["name"].ToString()
                    };
                    comboBoxpCategorywork.Add(ComboBoxItem);
                }
                foreach (DataRow row in _ds.Tables[6].Rows)
                {
                    var ComboBoxItem = new ComboBoxItems
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["name"].ToString()
                    };
                    comboBoxpObject.Add(ComboBoxItem);
                }
                foreach (DataRow row in _ds.Tables[7].Rows)
                {
                    var ComboBoxItem = new ComboBoxSubItems
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["name"].ToString(),
                        MainId = Convert.ToInt32(row["pCategoryworkID"])

                    };
                    comboBoxpSubcategorywork.Add(ComboBoxItem);
                }
                
                
            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString(), "ошибка LoadDataForNewPayment error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public bool Save()
        {
            DataBase db = new DataBase();
            bool result = false;
            try
            {
                SqlConnection con = db.Connect();
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 575");
                    return false;
                }
                SqlCommand cmd = null;
                cmd = IsNew ? new SqlCommand(@"insert into payment (pZaimID, datepay, typefundingID, pContragentID, pContractID, currencyID, AmountUSD, AmountBYR, AmountEUR, AmountRUB, RateUSD, 
                      RateEUR, RateRUB, pCategorycontractID, pCategoryworkID, pSubcategoryworkID, pObjectID, pDescription, isActive, pTypePaymentID, datelastupdate, userID)
                                    values(@pZaimID, @datepay, @typefundingID, @pContragentID, @pContractID, @currencyID, @AmountUSD, @AmountBYR, @AmountEUR, @AmountRUB, @RateUSD, 
                      @RateEUR, @RateRUB, @pCategorycontractID, @pCategoryworkID, @pSubcategoryworkID, @pObjectID, @pDescription, @isActive, @pTypePaymentID, @datelastupdate, @userID); SELECT @@IDENTITY AS 'ID';", con) 
: new SqlCommand(String.Format(@"update payment SET pZaimID = @pZaimID, datepay = @datepay, typefundingID = @typefundingID, pContragentID = @pContragentID, pContractID = @pContractID, currencyID = @currencyID, AmountUSD = @AmountUSD, AmountBYR = @AmountBYR, AmountEUR = @AmountEUR, AmountRUB = @AmountRUB, RateUSD = @RateUSD, 
                      RateEUR = @RateEUR, RateRUB = @RateRUB, pCategorycontractID = @pCategorycontractID, pCategoryworkID = @pCategoryworkID, pSubcategoryworkID = @pSubcategoryworkID, pObjectID = @pObjectID, pDescription = @pDescription, isActive = @isActive, pTypePaymentID = @pTypePaymentID, datelastupdate = @datelastupdate, userID = @userID WHERE     (paymentID = '{0}')", PaymentID), con);


                if (cmd != null)
                {
                    cmd.Parameters.Add("@pZaimID", SqlDbType.Int).Value = PZaimID;
                    cmd.Parameters.Add("@datepay", SqlDbType.DateTime).Value = utils.DateToZeroTime(Datepay);
                    cmd.Parameters.Add("@typefundingID", SqlDbType.Int).Value = TypefundingID;
                    cmd.Parameters.Add("@pContragentID", SqlDbType.Int).Value = PContragentID;
                    cmd.Parameters.Add("@pContractID", SqlDbType.Int).Value = PContractID;
                    cmd.Parameters.Add("@currencyID", SqlDbType.Int).Value = CurrencyID;

                    cmd.Parameters.Add("@AmountUSD", SqlDbType.Money).Value = AmountUSD;
                    cmd.Parameters.Add("@AmountBYR", SqlDbType.Money).Value = AmountBYR;
                    cmd.Parameters.Add("@AmountEUR", SqlDbType.Money).Value = AmountEUR;
                    cmd.Parameters.Add("@AmountRUB", SqlDbType.Money).Value = AmountRUB;

                    cmd.Parameters.Add("@RateUSD", SqlDbType.Int).Value = RateUSD;
                    cmd.Parameters.Add("@RateEUR", SqlDbType.Int).Value = RateEUR;
                    cmd.Parameters.Add("@RateRUB", SqlDbType.Int).Value = RateRUB;

                    cmd.Parameters.Add("@pCategorycontractID", SqlDbType.Int).Value = PCategorycontractID;
                    cmd.Parameters.Add("@pCategoryworkID", SqlDbType.Int).Value = PCategoryworkID;
                    cmd.Parameters.Add("@pSubcategoryworkID", SqlDbType.Int).Value = PSubcategoryworkID;
                    cmd.Parameters.Add("@pObjectID", SqlDbType.Int).Value = PObject;

                    cmd.Parameters.Add("@pDescription", SqlDbType.NVarChar, 500).Value = PDescription;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@pTypePaymentID", SqlDbType.Int).Value = PTypePayment;
                    cmd.Parameters.Add("@datelastupdate", SqlDbType.DateTime).Value = utils.DateToZeroTime(Datelastupdate);
                    cmd.Parameters.Add("@userID", SqlDbType.Int).Value = User.UserID;
                
                    if (IsNew)
                    {
                        PaymentID = Convert.ToInt64(cmd.ExecuteScalar()); //id созданной записи - SELECT @@IDENTITY AS 'ID';
                    }
                    else
                    {
                        cmd.ExecuteScalar();
                    }
                    
                }

                result = true;
            }
            catch (SqlException e)
            {
                result = false;
                MessageBox.Show(e.ToString(), "ошибка Payment.Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }

            return result;
        }

        public bool SaveFinance()
        {
            DataBase db = new DataBase();
            bool result = false;
            try
            {
                SqlConnection con = db.Connect();
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 575");
                    return false;
                }
                SqlCommand cmd = null;
                cmd = IsNew ? new SqlCommand(@"insert into payment (pZaimID, datepay, typefundingID, currencyID, AmountUSD, AmountBYR, AmountEUR, AmountRUB, RateUSD, 
                      RateEUR, RateRUB, pDescription, isActive, pTypePaymentID, datelastupdate, userID)
                                    values(@pZaimID, @datepay, @typefundingID, @currencyID, @AmountUSD, @AmountBYR, @AmountEUR, @AmountRUB, @RateUSD, 
                      @RateEUR, @RateRUB, @pDescription, @isActive, @pTypePaymentID, @datelastupdate, @userID); SELECT @@IDENTITY AS 'ID';", con)
: new SqlCommand(String.Format(@"update payment SET pZaimID = @pZaimID, datepay = @datepay, typefundingID = @typefundingID, currencyID = @currencyID, AmountUSD = @AmountUSD, AmountBYR = @AmountBYR, AmountEUR = @AmountEUR, AmountRUB = @AmountRUB, RateUSD = @RateUSD, 
                      RateEUR = @RateEUR, RateRUB = @RateRUB, pDescription = @pDescription, isActive = @isActive, pTypePaymentID = @pTypePaymentID, datelastupdate = @datelastupdate, userID = @userID WHERE     (paymentID = '{0}')", PaymentID), con);


                if (cmd != null)
                {
                    cmd.Parameters.Add("@pZaimID", SqlDbType.Int).Value = PZaimID;
                    cmd.Parameters.Add("@datepay", SqlDbType.DateTime).Value = utils.DateToZeroTime(Datepay);
                    cmd.Parameters.Add("@typefundingID", SqlDbType.Int).Value = TypefundingID;
                    cmd.Parameters.Add("@currencyID", SqlDbType.Int).Value = CurrencyID;

                    cmd.Parameters.Add("@AmountUSD", SqlDbType.Money).Value = AmountUSD;
                    cmd.Parameters.Add("@AmountBYR", SqlDbType.Money).Value = AmountBYR;
                    cmd.Parameters.Add("@AmountEUR", SqlDbType.Money).Value = AmountEUR;
                    cmd.Parameters.Add("@AmountRUB", SqlDbType.Money).Value = AmountRUB;

                    cmd.Parameters.Add("@RateUSD", SqlDbType.Int).Value = RateUSD;
                    cmd.Parameters.Add("@RateEUR", SqlDbType.Int).Value = RateEUR;
                    cmd.Parameters.Add("@RateRUB", SqlDbType.Int).Value = RateRUB;

                    cmd.Parameters.Add("@pCategorycontractID", SqlDbType.Int).Value = PCategorycontractID;
                    cmd.Parameters.Add("@pCategoryworkID", SqlDbType.Int).Value = PCategoryworkID;
                    cmd.Parameters.Add("@pSubcategoryworkID", SqlDbType.Int).Value = PSubcategoryworkID;
                    cmd.Parameters.Add("@pObjectID", SqlDbType.Int).Value = PObject;

                    cmd.Parameters.Add("@pDescription", SqlDbType.NVarChar, 500).Value = PDescription;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@pTypePaymentID", SqlDbType.Int).Value = PTypePayment;
                    cmd.Parameters.Add("@datelastupdate", SqlDbType.DateTime).Value = utils.DateToZeroTime(Datelastupdate);
                    cmd.Parameters.Add("@userID", SqlDbType.Int).Value = User.UserID;

                    if (IsNew)
                    {
                        PaymentID = Convert.ToInt64(cmd.ExecuteScalar()); //id созданной записи - SELECT @@IDENTITY AS 'ID';
                    }
                    else
                    {
                        cmd.ExecuteScalar();
                    }

                }

                result = true;
            }
            catch (SqlException e)
            {
                result = false;
                MessageBox.Show(e.ToString(), "ошибка Payment.SaveFinance error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }

            return result;
        }

        public bool Delete()
        {
            DataBase db = new DataBase();
            bool result = false;
            try
            {
                SqlConnection con = db.Connect();
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 5715");
                    return false;
                }
                SqlCommand cmd = null;
                cmd =  new SqlCommand(String.Format(@"update payment SET isActive = @isActive WHERE (paymentID = '{0}')", PaymentID), con);
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = IsActive;
                    cmd.ExecuteScalar();
                    result = true;
            }
            catch (SqlException e)
            {
                result = false;
                MessageBox.Show(e.ToString(), "ошибка Payment.Delete error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }

            return result;
        }

        public void GetListAllPayment(DevExpress.XtraGrid.GridControl gc, DateTime from, DateTime to, int type)
        {
            
            var db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"EXEC [dbo].[payment_get] '{0}', '{1}', '{2}'", type, utils.DateToSqlStoredProcedureString(from), utils.DateToSqlStoredProcedureString(to));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 224322");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                gc.DataSource = dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetListAllPayment error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public void GetListAllFinance(DevExpress.XtraGrid.GridControl gc, DateTime from, DateTime to, int type)
        {

            var db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"EXEC [dbo].[finance_get] '{0}', '{1}', '{2}'", type, utils.DateToSqlStoredProcedureString(from), utils.DateToSqlStoredProcedureString(to));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 224322");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                gc.DataSource = dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetListAllFinance error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public void GetListAllFinance(DevExpress.XtraGrid.GridControl gc, DateTime from, DateTime to)
        {

            var db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"EXEC [dbo].[finance_get_all] '{0}', '{1}'", utils.DateToSqlStoredProcedureString(from), utils.DateToSqlStoredProcedureString(to));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 224322");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                gc.DataSource = dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetListAllFinance error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public List<object> GetReport_1a(DateTime from, DateTime to)
        {

            List<object> result = new List<object>();
            var db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();

                var request0 = String.Format(@"EXEC [dbo].[zaim_name] '{0}'", PZaimID);
                var request = String.Format(@"EXEC [dbo].[report_1a] '{0}', '{1}', '{2}'", utils.DateToSqlStoredProcedureString(from), utils.DateToSqlStoredProcedureString(to), PZaimID);
                var myData = new SqlDataAdapter(request, con);
                var myData0 = new SqlDataAdapter(request0, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 22432322");
                    return null; // no connection
                }

                var _ds0 = new DataSet();
                myData0.Fill(_ds0);
                DataTable dt0 = _ds0.Tables[0];
                
                foreach (DataRow _dr in dt0.Rows)
                {
                    result.Add(_dr[0].ToString());
                }


                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                
                foreach (DataRow _dr in dt.Rows)
                {
                    result.Add(Convert.ToInt64(_dr[0]));    
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetReport_1a error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }

            return result;
        }

        public List<long> GetReport_2a(DateTime from, DateTime to)
        {

            List<long> result = new List<long>();
            var db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"EXEC [dbo].[report_2a] '{0}', '{1}', '{2}'", utils.DateToSqlStoredProcedureString(from), utils.DateToSqlStoredProcedureString(to), PZaimID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 22432322");
                    return null; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    //for (int i = 0; i < count; i++ )
                    //{
                    //    result.Add(Convert.ToInt64(_dr[i]));
                    //}
                    result.Add(Convert.ToInt64(_dr[0]));
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetReport_2a error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }

            return result;
        }

        public List<long> GetReport_1b(DateTime from, DateTime to)
        {

            List<long> result = new List<long>();
            var db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"EXEC [dbo].[report_1b] '{0}', '{1}', '{2}'", utils.DateToSqlStoredProcedureString(from), utils.DateToSqlStoredProcedureString(to), PZaimID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 22432322");
                    return null; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    //for (int i = 0; i < count; i++ )
                    //{
                    //    result.Add(Convert.ToInt64(_dr[i]));
                    //}
                    result.Add(Convert.ToInt64(_dr[0]));
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetReport_1b error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }

            return result;
        }

        public List<long> GetReport_2b(DateTime from, DateTime to)
        {

            List<long> result = new List<long>();
            var db = new DataBase();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"EXEC [dbo].[report_2b] '{0}', '{1}', '{2}'", utils.DateToSqlStoredProcedureString(from), utils.DateToSqlStoredProcedureString(to), PZaimID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 22432322");
                    return null; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    //for (int i = 0; i < count; i++ )
                    //{
                    //    result.Add(Convert.ToInt64(_dr[i]));
                    //}
                    result.Add(Convert.ToInt64(_dr[0]));
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetReport_2b error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }

            return result;
        }
    }
}
