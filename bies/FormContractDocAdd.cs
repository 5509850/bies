using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using bies.Entity;

namespace bies
{
    public partial class FormContractDocAdd : Form
    {
        private Contract contract;
        private Files files;
        private bool blocked = false;
        private List<ComboBoxEditTrade> comboBoxEditTradeList;
        private int tradeID;
        private Currency cur;
        private bool afterEditCurCatalog = false;
        private int selectedCurrency;

        public FormContractDocAdd(Contract contract, Files files, List<ComboBoxEditTrade> comboBoxEditTradeList, bool lookReadonly)
        {
            InitializeComponent();
            this.contract = contract;
            this.files = files;

            this.comboBoxEditTradeList = comboBoxEditTradeList;
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Text = this.contract.IsNew ? "Создание нового контракта" : "Редактирование контракта";
            
// ReSharper restore DoNotCallOverridableMethodsInConstructor
            foreach (ComboBoxEditTrade _trade in comboBoxEditTradeList)
            {
                comboBoxEdit_trade.Properties.Items.Add(_trade.NameTrade);
            }
            comboBoxEdit_trade.SelectedIndex = GetSelectedIndexForComboBoxEdit();

            FillData();
            if (lookReadonly)
            {
                this.Text = "Просмотр контракта";
                //dateEdit_DateCreate.Enabled =
                //    spinEdit_amountS.Enabled =
                //    spinEdit_avanceA.Enabled =
                //    spinEdit_retentionU.Enabled =
                //    checkEdit_signed.Enabled =
                //    checkEdit_signed.Enabled =
                //    textEdit_title.Enabled =
                //    simpleButton_save.Enabled =
                //    ComboBox_currencyID.Enabled =
                //    DataEdit_signedData.Enabled = false;
                comboBoxEdit_trade.Properties.ReadOnly =
                check_bankguaranteeBackAvans.Properties.ReadOnly =
                check_bankguaranteeRunContract.Properties.ReadOnly =
                ComboBox_typefundingID.Properties.ReadOnly =
                checkEdit_signed.Properties.ReadOnly =
                comboBox_actstartID.Properties.ReadOnly =
                dateEdit_contractData.Properties.ReadOnly =
                spinEdit_amount.Properties.ReadOnly =
                    checkEdit_signed.Properties.ReadOnly =
                    checkEdit_signed.Properties.ReadOnly =
                    textEdit_title.Properties.ReadOnly =
                    ComboBox_currencyID.Properties.ReadOnly =
                    DataEdit_signedData.Properties.ReadOnly = true;

                simpleButton_save.Visible = false;

                simpleButton_cancel.Text = "OK";
                //groupBox4.Enabled 


                LoadChange();
            }
        }

        public int TradeID
        {
            get { return tradeID; }
            set { tradeID = value; }
        }

        private void FillData()
        {
            spinEdit_amount.EditValue = contract.Amount;
            ComboBox_currencyID.SelectedIndex = contract.CurrencyID;
            dateEdit_contractData.EditValue = contract.ContractData;
            ComboBox_typefundingID.SelectedIndex = contract.TypefundingID;
            check_bankguaranteeRunContract.Checked = contract.BankguaranteeRunContract;
            check_bankguaranteeBackAvans.Checked = contract.BankguaranteeBackAvans;
            comboBox_actstartID.SelectedIndex = contract.ActstartID;
            DataEdit_signedData.EditValue = contract.SignedData;

            textEdit_FileName.Text = files.Filename;
            textEdit_title.Text = files.Title;
            dateEdit_fileDateCreate.EditValue = files.Datecreate;
            dateEdit_las.EditValue = files.Datelastupdate;
            checkEdit_signed.Checked = files.Signed;
            checkEdit_isBaseOrShared.Checked = files.IsFileInBaseOrInShare;
            FillRateByDate();
        }

        //загрузка изменений суммы по контракту
        private void LoadChange()
        {
            contract.GetListContractChangeAmountByContractID(gridControl_change);
        }

        private int GetSelectedIndexForComboBoxEdit()
        {
            if (comboBoxEdit_trade.Properties.Items.Count != comboBoxEditTradeList.Count)
                return -1;
            int result = 0;
            foreach (ComboBoxEditTrade _trade in comboBoxEditTradeList)
            {
                if (_trade.TradeID == contract.TradeID)
                    return result;
                result++;
            }
            return -1;
        }
       
        private void Check()
        {

           
            if (TradeID < 1)
           {
               MessageBox.Show("Не выбраны торги!");
               comboBoxEdit_trade.Focus();
               blocked = true;
               return;
           }
          
           if (String.IsNullOrEmpty(textEdit_title.Text))
           {
               MessageBox.Show("Не введено название документа!");
               textEdit_title.Focus();
               blocked = true;
               return;
           }

            if (spinEdit_amount.EditValue == null || Convert.ToInt64(spinEdit_amount.EditValue) == 0)
            {
                MessageBox.Show("Не введена стоимость контракта!");
                spinEdit_amount.Focus();
                blocked = true;
                return;
            }
            if (ComboBox_currencyID.SelectedIndex == 0)
            {
                MessageBox.Show("Не выбрана валюта контракта!");
                ComboBox_currencyID.Focus();
                blocked = true;
                return;
            }
            if (dateEdit_contractData.EditValue == null)
            {
                MessageBox.Show("Не введена дата контракта!");
                dateEdit_contractData.Focus();
                blocked = true;
                return;
            }
            if (DataEdit_signedData.EditValue == null)
            {
                MessageBox.Show("Не введена дата подписания контракта!");
                DataEdit_signedData.Focus();
                blocked = true;
                return;
            }
            
            if (comboBox_actstartID.SelectedIndex == 0)
            {
                MessageBox.Show("Не выбран акт ввода!");
                comboBox_actstartID.Focus();
                blocked = true;
                return;
            }
            if (ComboBox_typefundingID.SelectedIndex == 0)
            {
                MessageBox.Show("Не выбран тип финансирования!");
                ComboBox_typefundingID.Focus();
                blocked = true;
                return;
            }
            blocked = false;
        }

        private void FormTenderDocAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = blocked; //блокирование закрытия формы
        }

        private void simpleButton_cancel_Click(object sender, EventArgs e)
        {
            blocked = false;
        }

        private void simpleButton_save_Click(object sender, EventArgs e)
        {

            Check();

            contract.DateCreate = Convert.ToDateTime(dateEdit_contractData.EditValue);
            contract.IsActive = true;
            contract.IsNew = false;
            files.Title = textEdit_title.Text;
            files.Signed = Convert.ToBoolean(checkEdit_signed.Checked);
            contract.TradeID = TradeID;

            contract.Amount = Convert.ToInt64(spinEdit_amount.EditValue);
            contract.CurrencyID = ComboBox_currencyID.SelectedIndex;
            contract.TypefundingID = ComboBox_typefundingID.SelectedIndex;
            contract.ActstartID = comboBox_actstartID.SelectedIndex;

            contract.SignedData = Convert.ToDateTime(DataEdit_signedData.EditValue);
            contract.ContractData = Convert.ToDateTime(dateEdit_contractData.EditValue);

            contract.BankguaranteeRunContract = check_bankguaranteeRunContract.Checked;
            contract.BankguaranteeBackAvans = check_bankguaranteeBackAvans.Checked;
            //редактирование contract doc с заменой на новый файл
            if (!contract.IsNew && files.IsNew)
            {
                if (files.IsFileInBaseOrInShare)
                {
                    files.IsNew = false; //update file
                    if (!files.SaveOrUpdate())//1 сохраняем файл в БД
                    {
                        MessageBox.Show("Ошибка file.SaveOrUpdate()");
                    }    
                }
                //string targetPath = ;
                //2 Сохраняем копию файла на удаленную шару!!!!
                utils.SaveBytesToFile(Path.Combine(Properties.Settings.Default.SavePathShare, files.FileID.ToString()), files.Body);
            }
        }

        private void simpleButton_doc_Click(object sender, EventArgs e)
        {
            //Просмотр документа
            if (files.IsFileInBaseOrInShare)
            {
                if (files.Body != null)
                {
                    utils.SaveBytesToFile(Path.Combine(Path.GetTempPath(), files.Filename), files.Body);
                }
                else
                {
                    utils.SaveBytesToFile(Path.Combine(Path.GetTempPath(), files.Filename), files.GetFileBodyByFileID());
                }
                
            }
            else
            {
                utils.FilesCopyFromTo(Path.Combine(Properties.Settings.Default.SavePathShare, files.FileID.ToString()), Path.GetTempPath(), files.Filename, false);
            }

            utils.OpenFileInWordOrExcel(Path.GetTempPath(), files.Filename);
        }

        private void comboBoxEdit_trade_SelectedIndexChanged(object sender, EventArgs e)
        {
            TradeID = comboBoxEdit_trade.SelectedIndex >= 0
                          ? comboBoxEditTradeList[comboBoxEdit_trade.SelectedIndex].TradeID
                          : -1;
        }

        private void FillRateByDate()
        {

            if (!GetCurrencyRateByDataTime())
            {
                if (dateEdit_contractData.EditValue == null)
                    return;
                DateTime date = Convert.ToDateTime(dateEdit_contractData.EditValue);


                switch (MessageBox.Show(String.Format("Не введен курс, добавить курсы валют на дату {0} ?", date.ToShortDateString()), "Добавление курса валют", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.OK:
                        {
                            FormCurencyCatalog form = new FormCurencyCatalog(date.Date);
                            form.ShowDialog();
                            afterEditCurCatalog = true;
                            GetCurrencyRateByDataTime(); //перечитывание курсов на дату
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            break;
                        }
                }
            }

            if (cur != null)
            {
                RateUSD.EditValue = cur.Usd.Rate;
                RateRUB.EditValue = cur.Rub.Rate;
                RateEUR.EditValue = cur.Eur.Rate;
            }
        }
        
        private bool GetCurrencyRateByDataTime()
        {
            if (dateEdit_contractData.EditValue == null)
                return false;
            DateTime date = Convert.ToDateTime(dateEdit_contractData.EditValue);

            //после обновления справочника нужно обновить курсы
            if (cur != null && cur.Byr.Date.Date == date.Date && !afterEditCurCatalog)
                return true;//
            afterEditCurCatalog = false;

            cur = new Currency
            {
                Byr = new CurrencyRate
                {
                    CurrencyID = ((int)Enums.currencyID.byr),
                    Rate = 1,
                    Date = DateTime.Now.Date
                },
                Eur = new CurrencyRate
                {
                    CurrencyID = ((int)Enums.currencyID.eur),
                    Rate = 7000,
                    Date = DateTime.Now.Date
                },
                Rub = new CurrencyRate
                {
                    CurrencyID = ((int)Enums.currencyID.rub),
                    Rate = 170,
                    Date = DateTime.Now.Date
                },
                Usd = new CurrencyRate
                {
                    CurrencyID = ((int)Enums.currencyID.usd),
                    Rate = 5000,
                    Date = DateTime.Now.Date
                }
            };
            return cur.FillCurrencyRateByDateTime(date.Date, cur);
        }
       
        private void ComboBox_currencyID_EditValueChanged(object sender, EventArgs e)
        {
            setFontCurrency();
            if (ComboBox_currencyID.SelectedIndex < 0)
            {

                AmountUSD.Enabled =
                    AmountRUB.Enabled =
                    AmountEUR.Enabled =
                    AmountBYR.Enabled =
                    RateUSD.Enabled =
                    RateRUB.Enabled =
                    RateEUR.Enabled = false;
            }
            else
            {
                AmountUSD.Enabled =
                    AmountRUB.Enabled =
                    AmountEUR.Enabled =
                    AmountBYR.Enabled =
                    RateUSD.Enabled =
                    RateRUB.Enabled =
                    RateEUR.Enabled = true;
            }
        }

        private void AmountColorClear()
        {
            AmountUSD.BackColor =
                AmountRUB.BackColor =
                AmountEUR.BackColor =
                AmountBYR.BackColor = Color.White;
        }


        private void setFontCurrency()
        {
            AmountColorClear();
            if (ComboBox_currencyID.SelectedIndex < 0)
            {
                selectedCurrency = 0;
                return;
            }

            selectedCurrency = ComboBox_currencyID.SelectedIndex;
            switch (selectedCurrency)
            {
                case (int)Enums.currencyID.byr:
                    {
                        AmountBYR.BackColor = Color.Red;
                        break;
                    }
                case (int)Enums.currencyID.eur:
                    {
                        AmountEUR.BackColor = Color.Red;
                        break;
                    }
                case (int)Enums.currencyID.rub:
                    {
                        AmountRUB.BackColor = Color.Red;
                        break;
                    }
                case (int)Enums.currencyID.usd:
                    {
                        AmountUSD.BackColor = Color.Red;
                        break;
                    }
            }

        }


        private void spinEdit_amount_EditValueChanged(object sender, EventArgs e)
        {
            SetEquvalentAmount();
        }

        
        private void ComboBox_currencyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEquvalentAmount();
        }

        private void dateEdit_contractData_EditValueChanged(object sender, EventArgs e)
        {
            DataEdit_signedData.EditValue = dateEdit_contractData.EditValue;
            FillRateByDate();
        }

        private void simpleButton_currency_catalog_Click(object sender, EventArgs e)
        {
            if (dateEdit_contractData.EditValue == null)
                return;
            DateTime date = Convert.ToDateTime(dateEdit_contractData.EditValue);
            FormCurencyCatalog form = new FormCurencyCatalog(date.Date);
            form.ShowDialog();
            afterEditCurCatalog = true;
            FillRateByDate();
        }

        private void SetEquvalentAmount()
        {
            if (Convert.ToDecimal(AmountUSD.EditValue) < 0)
                AmountUSD.EditValue = 0;

            if (selectedCurrency == (int)Enums.currencyID.usd)
            {
                AmountUSD.EditValue = spinEdit_amount.EditValue;
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                    AmountEUR.EditValue = Math.Round((Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToInt32(RateEUR.EditValue))), 2);

                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountUSD.EditValue) * Convert.ToDecimal(RateUSD.EditValue), 2);

                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                    AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToInt32(RateRUB.EditValue)), 2);
                
            }

            if (selectedCurrency == (int)Enums.currencyID.eur)
            {
                AmountEUR.EditValue = spinEdit_amount.EditValue;
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                    AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);

                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountEUR.EditValue) * Convert.ToDecimal(RateEUR.EditValue), 2);

                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                    AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateRUB.EditValue)), 2);

            }

            if (selectedCurrency == (int)Enums.currencyID.rub)
            {
                AmountRUB.EditValue = spinEdit_amount.EditValue;
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                    AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);

                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountRUB.EditValue) * Convert.ToDecimal(RateRUB.EditValue), 2);

                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                    AmountEUR.EditValue = Math.Round(Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateEUR.EditValue)), 2);

            }

            if (selectedCurrency == (int)Enums.currencyID.byr)
            {
                AmountBYR.EditValue = spinEdit_amount.EditValue;
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                    AmountUSD.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateUSD.EditValue), 2);
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                    AmountEUR.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateEUR.EditValue), 2);
                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                    AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateRUB.EditValue), 2);
           
      
            }
            
      
       
        }

        private void RateUSD_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(RateUSD.EditValue) < 0)
                RateUSD.EditValue = 0;

            if (selectedCurrency == (int)Enums.currencyID.byr)
            {
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                {
                    AmountUSD.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateUSD.EditValue), 2);
                }
            }
            if (selectedCurrency == (int)Enums.currencyID.eur)
            {
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                {
                    AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);
                }
            }

            if (selectedCurrency == (int)Enums.currencyID.rub)
            {
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                {
                    AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);
                }
            }

            if (selectedCurrency == (int)Enums.currencyID.usd)
            {
                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountUSD.EditValue) * Convert.ToDecimal(RateUSD.EditValue), 2);
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                    AmountEUR.EditValue = Math.Round((Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToDecimal(RateEUR.EditValue))), 2);
                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                    AmountRUB.EditValue = Math.Round((Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToDecimal(RateRUB.EditValue))), 2);
            }
        }

        private void RateEUR_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(RateEUR.EditValue) < 0)
                RateEUR.EditValue = 0;

            if (selectedCurrency == (int)Enums.currencyID.byr)
            {
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                {
                    AmountEUR.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateEUR.EditValue), 2);
                }
            }
            if (selectedCurrency == (int)Enums.currencyID.usd)
            {
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                {
                    AmountEUR.EditValue = Math.Round((Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToDecimal(RateEUR.EditValue))), 2);
                }
            }

            if (selectedCurrency == (int)Enums.currencyID.rub)
            {
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                {
                    AmountEUR.EditValue = Math.Round((Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateEUR.EditValue))), 2);
                }
            }

            if (selectedCurrency == (int)Enums.currencyID.eur)
            {
                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountEUR.EditValue) * Convert.ToDecimal(RateEUR.EditValue), 2);
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                {
                    AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);
                }
                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                {
                    AmountRUB.EditValue = Math.Round((Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateRUB.EditValue))), 2);
                }
            }
        }

        private void RateRUB_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(RateRUB.EditValue) < 0)
                RateRUB.EditValue = 0;
            
            if (selectedCurrency == (int)Enums.currencyID.byr)
            {
                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                {
                    AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateRUB.EditValue), 2);
                }
            }
            if (selectedCurrency == (int)Enums.currencyID.usd)
            {
                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                {
                    AmountRUB.EditValue = Math.Round((Convert.ToDecimal(AmountUSD.EditValue) * ((decimal)Convert.ToDecimal(RateUSD.EditValue) / Convert.ToDecimal(RateRUB.EditValue))), 2);
                }
            }

            if (selectedCurrency == (int)Enums.currencyID.eur)
            {
                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                {
                    AmountRUB.EditValue = Math.Round((Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateRUB.EditValue))), 2);
                }
            }

            if (selectedCurrency == (int)Enums.currencyID.rub)
            {
                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountRUB.EditValue) * Convert.ToDecimal(RateRUB.EditValue), 2);
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                {
                    AmountEUR.EditValue = Math.Round((Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateEUR.EditValue))), 2);
                }
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                {
                    AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);
                }

            }
        }
    }
}
