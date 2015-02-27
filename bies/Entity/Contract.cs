using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bies.Entity
{
    public class Contract
    {
        private int contractID;
        private int tradeID;
        private DateTime dateCreate;
        private Int64 amount;
        private int currencyID;
        private DateTime contractData;
        private DateTime signedData;
        private int typefundingID;
        private bool bankguaranteeRunContract;
        private bool bankguaranteeBackAvans;
        private int actstartID;
        private int typedocID;
        private bool isActive;
        private bool isNew;
        private bool isChandgedSumm;

        public int ContractID
        {
            get { return contractID; }
            set { contractID = value; }
        }

        public int TradeID
        {
            get { return tradeID; }
            set { tradeID = value; }
        }

        public DateTime DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }

        public Int64 Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public int CurrencyID
        {
            get { return currencyID; }
            set { currencyID = value; }
        }

        public DateTime ContractData
        {
            get { return contractData; }
            set { contractData = value; }
        }

        public DateTime SignedData
        {
            get { return signedData; }
            set { signedData = value; }
        }

        public int TypefundingID
        {
            get { return typefundingID; }
            set { typefundingID = value; }
        }

        public bool BankguaranteeRunContract
        {
            get { return bankguaranteeRunContract; }
            set { bankguaranteeRunContract = value; }
        }

        public bool BankguaranteeBackAvans
        {
            get { return bankguaranteeBackAvans; }
            set { bankguaranteeBackAvans = value; }
        }

        public int ActstartID
        {
            get { return actstartID; }
            set { actstartID = value; }
        }

        public int TypedocID
        {
            get { return typedocID; }
            set { typedocID = value; }
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

        public bool IsChandgedSumm
        {
            get { return isChandgedSumm; }
            set { isChandgedSumm = value; }
        }

        //
        public bool CreateContractGetID()
        {
            DataBase db = new DataBase();
            string request;

            try
            {
                SqlConnection con = db.Connect();
                request =
                String.Format(
                    @"INSERT INTO  contracts (
tradeID, 
dateCreate, 
amount, 
currencyID, 
contractData, 
signedData, 
typefundingID, 
bankguaranteeRunContract, 
bankguaranteeBackAvans, 
actstartID, 
typedocID, 
isActive, 
isChandgedSumm)
VALUES     ('{0}',{1},'{2}','{3}',{1},{1},'{4}','{5}','{6}','{7}','{8}','{9}','{10}');
SELECT @@IDENTITY AS 'ID';", TradeID,
                           "CONVERT(DATETIME, GETDATE(), 102)",
                           Amount,
                           CurrencyID,
                           TypefundingID,
                           utils.SetBitDB(BankguaranteeRunContract),
                           utils.SetBitDB(BankguaranteeBackAvans),
                           ActstartID,
                           TypedocID,
                           utils.SetBitDB(IsActive), utils.SetBitDB(IsChandgedSumm));
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 595");
                    return false;
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    ContractID = Convert.ToInt32(_dr["ID"]);

                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка CreateContractGetID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            /*
             */
            string request = String.Format(
                    @"UPDATE    contracts
SET              tradeID ='{1}', dateCreate = '{2}', amount = '{3}', currencyID = '{4}', contractData = '{5}',  signedData = '{6}', typefundingID =  '{7}', bankguaranteeRunContract =  '{8}', bankguaranteeBackAvans =  '{9}', 
                      actstartID = '{10}', typedocID = '{11}', isActive = '{12}'
WHERE     (contractID = {0})", ContractID, TradeID, utils.DateToSqlString(DateCreate), Amount, CurrencyID, utils.DateToSqlString(ContractData), utils.DateToSqlString(SignedData), TypefundingID,  utils.SetBitDB(BankguaranteeRunContract), utils.SetBitDB(BankguaranteeBackAvans),
                            ActstartID, TypedocID, utils.SetBitDB(IsActive));
            return db.ExecuteSql(request);
        }

        private static int GetCountOfChanges(String request)
        {
            DataBase db = new DataBase();
            int result = 0;
            try
            {
                SqlConnection con = db.Connect();
                
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 543595");
                    return 0;
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                DataTable dt = _ds.Tables[0];
                foreach (DataRow _dr in dt.Rows)
                {
                    result = Convert.ToInt32(_dr[0]);

                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ошибка GetCountOfChanges error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            finally
            {
                db.Disconnect();
            }
            return result;
        }

        public bool UpdateSumm()
        {
            DataBase db = new DataBase();
            /*
             */
            int countChanges = GetCountOfChanges(String.Format("SELECT  count(*) FROM contractChanged   WHERE     (contractID = {0}) ", ContractID));
            if (countChanges == 0)
                countChanges = 1;

            string request = countChanges == 1 ? String.Format(
                                                     @"INSERT INTO contractChanged (contractID, amount, changetext) SELECT     contractID, amount, 'первоначальная сумма' AS Expr1
FROM         contracts WHERE     (contractID = '{0}');
UPDATE    contracts SET   amount = '{1}', isChandgedSumm = '{2}' WHERE     (contractID = {0}); 
INSERT INTO contractChanged (contractID, amount, changetext) SELECT     contractID, amount, 'изменение № {3}' AS Expr1
FROM         contracts
WHERE     (contractID = '{0}')
", ContractID, Amount, IsChandgedSumm, countChanges) : String.Format(@"UPDATE    contracts SET   amount = '{1}', isChandgedSumm = '{2}' WHERE     (contractID = {0}); 
INSERT INTO contractChanged (contractID, amount, changetext) SELECT     contractID, amount, 'изменение № {3}' AS Expr1
FROM         contracts
WHERE     (contractID = '{0}')
", ContractID, Amount, IsChandgedSumm, countChanges);
            
            return db.ExecuteSql(request);
        }

        public void GetListAllContractByTradeID(DevExpress.XtraGrid.GridControl gc)
        {
            DataBase db = new DataBase();
            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT  contracts.contractID, filedoc.filedocID, files.fileID, files.filename, files.isFileInBaseOrInShare, files.title, 
contracts.dateCreate, contracts.amount, currency.shortname, contracts.contractData, contracts.bankguaranteeRunContract,
contracts.bankguaranteeBackAvans, typefunding.name AS 'typefundingname', actstart.name AS 'actstartname', 
files.signed, contracts.signedData, contracts.currencyID, contracts.typefundingID, contracts.actstartID, files.datelastupdate, contracts.isChandgedSumm
                      FROM contracts
                      INNER JOIN currency ON contracts.currencyID = currency.currencyID
                      INNER JOIN typefunding ON contracts.typefundingID = typefunding.typefundingID
                      INNER JOIN actstart ON contracts.actstartID = actstart.actstartID
                      INNER JOIN filedoc ON contracts.contractID = filedoc.docID 
                      INNER JOIN files ON filedoc.fileID = files.fileID                      
WHERE     (contracts.tradeID = '{0}') AND (contracts.typedocID = '{1}') AND (contracts.isActive = '1') AND (filedoc.isActive = '1') AND (filedoc.typedocID = '{1}') AND (filedoc.tradeID = '{0}')",
TradeID, TypedocID);
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

        public bool Delete()
        {
            DataBase db = new DataBase();
            string request = String.Format(@"UPDATE   contracts  SET isActive = '0' WHERE  (contractID = '{0}');
UPDATE    filedoc SET isActive = '0' WHERE     (docID = '{0}') AND (typedocID = '{1}')", ContractID, TypedocID);
            return db.ExecuteSql(request);
        }


        public void GetListAllContractDocsByContractID(DevExpress.XtraGrid.GridControl gcDoc, DevExpress.XtraGrid.GridControl gcDocWork, int typedocIDgcDoc, int typedocIDgcDocWork)
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT     contractDocs.contractDocsID, contractDocs.contractID, contractDocs.amnountS, contractDocs.currencyID, contractDocs.avanceA, contractDocs.retentionU, 
                      contractDocs.s1, currency.shortname, filedoc.filedocID, files.fileID, files.filename, files.isFileInBaseOrInShare, files.title, 
files.signed, files.datelastupdate, files.datecreate
FROM         contractDocs INNER JOIN
                      currency ON contractDocs.currencyID = currency.currencyID INNER JOIN
                      filedoc ON contractDocs.contractDocsID = filedoc.docID INNER JOIN files ON filedoc.fileID = files.fileID                      
WHERE     (contractDocs.contractID = '{0}') AND (contractDocs.isActive = '1') AND (contractDocs.typedocID = '{1}') AND (filedoc.isActive = '1') AND (filedoc.typedocID = '{1}');
SELECT     contractDocWorks.contractDocWorksID, contractDocWorks.contractID, contractDocWorks.typeWork, contractDocWorks.amount, contractDocWorks.currencyID, 
                      contractDocWorks.avanceA, contractDocWorks.retentionU, contractDocWorks.s1, currency.shortname, filedoc.filedocID, files.fileID, files.filename, files.isFileInBaseOrInShare, files.title, files.signed, files.datelastupdate, contracts.typefundingID, files.datecreate
FROM         contractDocWorks INNER JOIN
                      contracts ON contracts.contractID = contractDocWorks.contractID INNER JOIN
                      currency ON contractDocWorks.currencyID = currency.currencyID INNER JOIN
                      filedoc ON contractDocWorks.contractDocWorksID = filedoc.docID INNER JOIN
                      files ON filedoc.fileID = files.fileID
WHERE     (contractDocWorks.contractID = '{0}') AND (contractDocWorks.typedocID = '{2}') AND (contractDocWorks.isActive = '1') AND (filedoc.isActive = '1') AND (filedoc.typedocID = '{2}')",
ContractID, typedocIDgcDoc, typedocIDgcDocWork);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 1435");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gcDoc.DataSource = _ds.Tables[0];
                gcDocWork.DataSource = _ds.Tables[1];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListAllContractDocsByContractID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

        public void GetListContractChangeAmountByContractID(DevExpress.XtraGrid.GridControl gcChanged)
        {
            DataBase db = new DataBase();
            //List<TenderDocs> result = new List<TenderDocs>();

            try
            {
                SqlConnection con = db.Connect();
                var request = String.Format(@"SELECT  changetext, amount FROM  contractChanged 
WHERE  contractID = '{0}'",ContractID);
                var myData = new SqlDataAdapter(request, con);
                if (con == null)
                {
                    MessageBox.Show("нет связи с сервером - 143re5");
                    return; // no connection
                }
                var _ds = new DataSet();
                myData.Fill(_ds);
                gcChanged.DataSource = _ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "ошибка GetListContractChangeAmountByContractID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                db.Disconnect();
            }
        }

    
    }


}
