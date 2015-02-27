using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class Currency
    {
        private CurrencyRate usd;
        private CurrencyRate byr;
        private CurrencyRate eur;
        private CurrencyRate rub;

        public CurrencyRate Byr
        {
            get { return byr; }
            set { byr = value; }
        }

        public CurrencyRate Eur
        {
            get { return eur; }
            set { eur = value; }
        }

        public CurrencyRate Rub
        {
            get { return rub; }
            set { rub = value; }
        }

        public CurrencyRate Usd
        {
            get { return usd; }
            set { usd = value; }
        }

        public bool Create()
        {
            DataBase db = new DataBase();
            string request;

            try
            {
                SqlConnection con = db.Connect();
                request =
                String.Format(
                    @"INSERT INTO currencyRate (rate, date, currencyID) VALUES     ('{0}','{1}','{2}');
INSERT INTO currencyRate (rate, date, currencyID) VALUES     ('{3}','{4}','{5}');
INSERT INTO currencyRate (rate, date, currencyID) VALUES     ('{6}','{7}','{8}');
INSERT INTO currencyRate (rate, date, currencyID) VALUES     ('{9}','{10}','{11}');
", utils.SetDoubleDB(Byr.Rate), utils.DateToSqlString(Byr.Date), Byr.CurrencyID,
 utils.SetDoubleDB(Eur.Rate), utils.DateToSqlString(Eur.Date), Eur.CurrencyID,
 utils.SetDoubleDB(Rub.Rate), utils.DateToSqlString(Rub.Date), Rub.CurrencyID,
 utils.SetDoubleDB(Usd.Rate), utils.DateToSqlString(Usd.Date), Usd.CurrencyID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 595");
                    return false;
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка Currency.Create error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            finally
            {
                db.Disconnect();
            }
            return true;
        }

        public bool Update()
        {
            DataBase db = new DataBase();
            string request;

            try
            {
                SqlConnection con = db.Connect();
                request =
                String.Format(
                    @"UPDATE  currencyRate SET rate = '{0}' WHERE     (date = '{1}') AND (currencyID = '{2}');     
UPDATE  currencyRate SET rate = '{3}' WHERE     (date = '{1}') AND (currencyID = '{4}');     
UPDATE  currencyRate SET rate = '{5}' WHERE     (date = '{1}') AND (currencyID = '{6}');     
", utils.SetDoubleDB(Usd.Rate), utils.DateToSqlString(Usd.Date), Usd.CurrencyID,
 utils.SetDoubleDB(Eur.Rate),  Eur.CurrencyID,
 utils.SetDoubleDB(Rub.Rate),  Rub.CurrencyID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 595");
                    return false;
                }
                var _ds = new DataSet();
                myData.Fill(_ds);

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка Currency.Update error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            finally
            {
                db.Disconnect();
            }
            return true;
        }

        public void FillCurremcyGrid(DevExpress.XtraGrid.GridControl gc)
        {
                DataBase db = new DataBase();
            try
            {
                SqlConnection con = db.Connect();
                var request = (@" SELECT     currencyRate.currencyRateID, currencyRate.rate, currencyRate.date, currencyRate.currencyID, currency.shortname, currency.name, currency.code
FROM         currencyRate INNER JOIN currency ON currencyRate.currencyID = currency.currencyID ORDER BY currencyRate.date DESC");
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 1435");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gc.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllContractByTradeID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public void FillTopCurrency(DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit_DateRateCurrency, Currency cur)
        {
            DataBase db = new DataBase();
            try
            {
                SqlConnection con = db.Connect();
                var request = (@"SELECT DISTINCT date
FROM         currencyRate; SELECT     TOP (4) rate, date, currencyID FROM         currencyRate");
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 1435");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                DataTable dt2 = _ds.Tables[1];
                comboBoxEdit_DateRateCurrency.Properties.Items.Clear();
                 foreach (DataRow _dr in dt.Rows)
                {
                    comboBoxEdit_DateRateCurrency.Properties.Items.Add(Convert.ToDateTime(_dr["date"]).Date);
                }
                foreach (DataRow _dr in dt2.Rows)
                {
                    
                    int CurrencyID = Convert.ToInt32(_dr["currencyID"]);
                    switch (CurrencyID)
                    {
                        case (int)Enums.currencyID.byr:
                            {
                                cur.Byr.CurrencyID = CurrencyID;
                                cur.Byr.Date = Convert.ToDateTime(_dr["date"]);
                                cur.Byr.Rate = Convert.ToDouble(_dr["rate"]);
                                break;
                            }
                    case (int)Enums.currencyID.eur:
                            {
                                cur.Eur.CurrencyID = CurrencyID;
                                cur.Eur.Date = Convert.ToDateTime(_dr["date"]);
                                cur.Eur.Rate = Convert.ToDouble(_dr["rate"]);
                                break;
                            }

                              case (int)Enums.currencyID.rub:
                            {
                                cur.Rub.CurrencyID = CurrencyID;
                                cur.Rub.Date = Convert.ToDateTime(_dr["date"]);
                                cur.Rub.Rate = Convert.ToDouble(_dr["rate"]);
                                break;
                            }

                            case (int)Enums.currencyID.usd:
                            {
                                cur.Usd.CurrencyID = CurrencyID;
                                cur.Usd.Date = Convert.ToDateTime(_dr["date"]);
                                cur.Usd.Rate = Convert.ToDouble(_dr["rate"]);
                                break;
                            }
                    }
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка FillTopCurrency error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public bool FillCurrencyRateByDateTime(DateTime date, Currency cur)
        {
            bool result = false;
            DataBase db = new DataBase();
            try
            {
                SqlConnection con = db.Connect();
                var request = (String.Format(@"SELECT     TOP (4) rate, currencyID FROM  currencyRate WHERE     (date = '{0}')", utils.DateToSqlString(date)));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 1435");
                    return false; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    result = true;
                    int CurrencyID = Convert.ToInt32(_dr["currencyID"]);
                    switch (CurrencyID)
                    {
                        case (int)Enums.currencyID.byr:
                            {
                                cur.Byr.CurrencyID = CurrencyID;
                                cur.Byr.Date = date;
                                cur.Byr.Rate = Math.Round(Convert.ToDouble(_dr["rate"]), 2);
                                break;
                            }
                        case (int)Enums.currencyID.eur:
                            {
                                cur.Eur.CurrencyID = CurrencyID;
                                cur.Eur.Date = date;
                                cur.Eur.Rate = Math.Round(Convert.ToDouble(_dr["rate"]), 2);
                                break;
                            }

                        case (int)Enums.currencyID.rub:
                            {
                                cur.Rub.CurrencyID = CurrencyID;
                                cur.Rub.Date = date;
                                cur.Rub.Rate = Math.Round(Convert.ToDouble(_dr["rate"]), 2);
                                break;
                            }

                        case (int)Enums.currencyID.usd:
                            {
                                cur.Usd.CurrencyID = CurrencyID;
                                cur.Usd.Date = date;
                                cur.Usd.Rate = Math.Round(Convert.ToDouble(_dr["rate"]), 2);
                                break;
                            }
                    }
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка FillTopCurrency error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }
    }
}
