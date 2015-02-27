using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using bies.Entity;

namespace bies
{
    public partial class FormPaymet : Form
    {
        private int pTypePaymentID;
        private Payment payment;
        private bool closingIsBlocked = false;
        private int selectedCurrency;
        private Currency cur;
        private bool isControlLoaded = false;

        public FormPaymet(Payment payment, int pTypePaymentID, bool readOnly)
        {
            InitializeComponent();
            this.pTypePaymentID = pTypePaymentID;
            this.payment = payment;
            if (payment.IsNew)
            {
                datepay.EditValue =
                    datelastupdate.EditValue = DateTime.Now;
            }
            LoadDate();
            if (readOnly)
            {
                simpleButton_cur_catalog.Enabled =
                simpleButton_OK.Enabled = false;
                simpleButton_Cancel.Text = "OK";
                pZaimID.Properties.ReadOnly = 
                    currencyID.Properties.ReadOnly = 
                    pContragentID.Properties.ReadOnly = 
                    pContractID.Properties.ReadOnly = 
                    pCategorycontractID.Properties.ReadOnly = 
                    pCategoryworkID.Properties.ReadOnly = 
                    pSubcategoryworkID.Properties.ReadOnly = 
                    pObjectID.Properties.ReadOnly = 
                    true;
            }

            if (!payment.IsNew)
            {
               SetDataField();
            }

            if (pTypePaymentID == (int)Enums.pTypePaymentID.fact)
                pTypePayment.Text = "фактически";
            if (pTypePaymentID == (int)Enums.pTypePaymentID.plan)
                pTypePayment.Text = "запланировано";
            textEdit_USER.Text = this.payment.User.Fio;
            FillRateByDate();
            isControlLoaded = true;
           
        }

        private void SetDataField()
        {
            checkEdit_recalculate.Checked = false;

            pZaimID.SelectedIndex = GetIndexByID(payment.comboBoxItemsZaim, payment.PZaimID);

            //Курсы будут уточнены после изменения datepay.EditValue из справочника до десятых долей!!!!!!!!!!!!!!
         //   RateUSD.EditValue = payment.RateUSD;
         //   RateRUB.EditValue = payment.RateRUB;
         //   RateEUR.EditValue = payment.RateEUR;

            datepay.EditValue = payment.Datepay;
            typefundingID.SelectedIndex = (payment.TypefundingID - 1);
            datelastupdate.EditValue = payment.Datelastupdate;
            currencyID.SelectedIndex = GetIndexByID(payment.comboBoxCurrency, payment.CurrencyID);
            AmountUSD.EditValue = payment.AmountUSD;
            AmountRUB.EditValue = payment.AmountRUB;
            AmountEUR.EditValue = payment.AmountEUR;
            AmountBYR.EditValue = payment.AmountBYR;
            
            pContragentID.SelectedIndex = GetIndexByID(payment.comboBoxpContragent, payment.PContragentID);
            pContractID.SelectedIndex = GetIndexByID(payment.comboBoxpContract, payment.PContractID);
            pCategorycontractID.SelectedIndex = GetIndexByID(payment.comboBoxpCategorycontract, payment.PCategorycontractID);
            pCategoryworkID.SelectedIndex = GetIndexByID(payment.comboBoxpCategorywork, payment.PCategoryworkID);
            pSubcategoryworkID.SelectedIndex = GetIndexByID(payment.comboBoxpSubcategorywork, payment.PSubcategoryworkID);
            pObjectID.SelectedIndex = GetIndexByID(payment.comboBoxpObject, payment.PObject);
            pDescription.Text = payment.PDescription;
            payment.IsActive = true;
        }

        private static int GetIndexByID(IList<ComboBoxItems> items, int id)
        {
            for (int i = 0; i < items.Count; i++ )
            {
                if (items[i].Id == id)
                    return i;
            }
            return -1;
        }

        private static int GetIndexByID(IList<ComboBoxSubItems> items, int id)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == id)
                    return i;
            }
            return -1;
        }

        private void LoadDate()
        {

            payment.LoadDataForNewPayment(); //загрузка данных для комбобоксов
            typefundingID.EditValue = 1;
            FillComboBox();
        }

        private void FillRateByDate()
        {
            
            if (!GetCurrencyRateByDataTime())
            {
                if (datepay.EditValue == null)
                    return;
                DateTime date = Convert.ToDateTime(datepay.EditValue);


                switch (MessageBox.Show(String.Format("Не введен курс, добавить курсы валют на дату {0} ?", date.ToShortDateString()), "Добавление курса валют", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.OK:
                        {
                            FormCurencyCatalog form = new FormCurencyCatalog(date.Date);
                            form.ShowDialog();
                            if (cur != null && cur.Byr.Date.Date == date.Date)
                            {
                                cur.Byr.Date = date.Date.AddDays(1);
                                //смещение даты для обновления курсов после изменения/добавления
                            }
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
            
            if (datepay.EditValue == null)
                return false;
            DateTime date = Convert.ToDateTime(datepay.EditValue);

            if (cur != null && cur.Byr.Date.Date == date.Date)
                return true;//

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

        private void FillComboBox()
        {
            if (payment.comboBoxItemsZaim != null && payment.comboBoxItemsZaim.Count != 0)
            {

                pZaimID.Properties.Items.Clear();
                currencyID.Properties.Items.Clear();

                pContragentID.Properties.Items.Clear();
                pContractID.Properties.Items.Clear();
                pCategorycontractID.Properties.Items.Clear();
                pCategoryworkID.Properties.Items.Clear();
                pObjectID.Properties.Items.Clear();
                foreach (ComboBoxItems item in payment.comboBoxItemsZaim)
                {
                    pZaimID.Properties.Items.Add(item.Name);
                }
                foreach (ComboBoxItems item in payment.comboBoxCurrency)
                {
                    currencyID.Properties.Items.Add(item.Name);
                }
                foreach (ComboBoxItems item in payment.comboBoxpContragent)
                {
                    pContragentID.Properties.Items.Add(item.Name);
                }
                foreach (ComboBoxItems item in payment.comboBoxpContract)
                {
                    pContractID.Properties.Items.Add(item.Name);
                }
                foreach (ComboBoxItems item in payment.comboBoxpCategorycontract)
                {
                    pCategorycontractID.Properties.Items.Add(item.Name);
                }
                foreach (ComboBoxItems item in payment.comboBoxpCategorywork)
                {
                    pCategoryworkID.Properties.Items.Add(item.Name);
                }
                foreach (ComboBoxItems item in payment.comboBoxpObject)
                {
                    pObjectID.Properties.Items.Add(item.Name);
                }
                
                /*
                 *   = new List<ComboBoxItems>();
                 = new List<ComboBoxItems>();
                 = new List<ComboBoxItems>();
                 = new List<ComboBoxItems>();
                 = new List<ComboBoxItems>();
                 */
                // a
            }
        }


        private void pCategoryworkID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pCategoryworkID.Properties.Items.Count == 0)
                return;
            pSubcategoryworkID.Properties.Items.Clear();
            pSubcategoryworkID.SelectedIndex = -1;
            if (pCategoryworkID.SelectedIndex == -1)
            {
                return;
            }
            
            int mainIndex = payment.comboBoxpCategorywork[pCategoryworkID.SelectedIndex].Id;
            foreach (ComboBoxSubItems item in payment.comboBoxpSubcategorywork)
            {
                if (mainIndex == item.MainId)
                    pSubcategoryworkID.Properties.Items.Add(item.Name);
            }
        }

        private void simpleButton_zaim_Click(object sender, EventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pZaim);
            if (form.ShowDialog() == DialogResult.OK)
                LoadDate();
        }

        private void simpleButton_OK_Click(object sender, EventArgs e)
        {
            closingIsBlocked = !ValidateAndSaveData();
        }

        private bool ValidateAndSaveData()
        {
            if (pZaimID.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран займ!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pZaimID.Focus();
                return false;
            }

            if (currencyID.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбрана валюта!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                currencyID.Focus();
                return false;
            }

            if (pContragentID.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран контрагент!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pContragentID.Focus();
                return false;
            }

            if (pContractID.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран контракт!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pContractID.Focus();
                return false;
            }

            if (pCategorycontractID.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбрана категория контракта!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pCategorycontractID.Focus();
                return false;
            }

            if (pCategoryworkID.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбрана категория работ!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pCategoryworkID.Focus();
                return false;
            }

            if (pSubcategoryworkID.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбрана подкатегория работ!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pSubcategoryworkID.Focus();
                return false;
            }
            if (pObjectID.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран объект!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pObjectID.Focus();
                return false;
            }

            if(datepay.EditValue == null)
            {
                MessageBox.Show("Не выбрана дата!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                datepay.Focus();
                return false;
            }
            SaveData();
            return true;
        }

        private void SaveData()
        {
            if (payment == null)
                return;
            try
            {
                payment.PZaimID = payment.comboBoxItemsZaim[pZaimID.SelectedIndex].Id;
                payment.Datepay = Convert.ToDateTime(datepay.EditValue);
                payment.TypefundingID =
                    Convert.ToInt32(typefundingID.Properties.Items[typefundingID.SelectedIndex].Value);
                payment.CurrencyID = payment.comboBoxCurrency[currencyID.SelectedIndex].Id;
                payment.AmountUSD = Convert.ToDecimal(AmountUSD.EditValue);
                payment.AmountRUB = Convert.ToDecimal(AmountRUB.EditValue);
                payment.AmountEUR = Convert.ToDecimal(AmountEUR.EditValue);
                payment.AmountBYR = Convert.ToDecimal(AmountBYR.EditValue);
                payment.RateUSD = Convert.ToInt32(RateUSD.EditValue);
                payment.RateRUB = Convert.ToInt32(RateRUB.EditValue);
                payment.RateEUR = Convert.ToInt32(RateEUR.EditValue);
                payment.PContragentID = payment.comboBoxpContragent[pContragentID.SelectedIndex].Id;
                payment.PContractID = payment.comboBoxpContract[pContractID.SelectedIndex].Id;
                payment.PCategorycontractID = payment.comboBoxpCategorycontract[pCategorycontractID.SelectedIndex].Id;
                payment.PCategoryworkID = payment.comboBoxpCategorywork[pCategoryworkID.SelectedIndex].Id;
                payment.PSubcategoryworkID = payment.comboBoxpSubcategorywork[pSubcategoryworkID.SelectedIndex].Id;
                payment.PObject = payment.comboBoxpObject[pObjectID.SelectedIndex].Id;
                if (pDescription.Text.Length > 499)
                    pDescription.Text = pDescription.Text.Remove(495);
                payment.PDescription = pDescription.Text;
                payment.PTypePayment = pTypePaymentID;
                payment.IsActive = true;
                payment.Datelastupdate = DateTime.Now;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка - " + ex.Message);
                return;
            }

            if (!payment.Save())
                MessageBox.Show("Ошибка сохранения платежа в базу!");
        }

        private void FormPaymet_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = closingIsBlocked;
        }

        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            closingIsBlocked = false;
        }

        #region Button Catalogs

        private void simpleButton_contragent_Add_Click(object sender, EventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pContragent);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            LoadDate();
        }

        private void simpleButton_contract_Add_Click(object sender, EventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pContract);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            LoadDate();
        }

        private void simpleButton_CatContract_Add_Click(object sender, EventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pCategorycontract);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            LoadDate();
        }

        private void simpleButton_catWork_Add_Click(object sender, EventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pCategorywork);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            LoadDate();
        }

        private void simpleButton_SubCatWork_Add_Click(object sender, EventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pSubcategorywork);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            LoadDate();
        }

        private void simpleButton_Object_Add_Click(object sender, EventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pObject);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            LoadDate();
        }

        #endregion Button Catalogs


        #region Curensy Amount

        /*
         * payment.CurrencyID = payment.comboBoxCurrency[currencyID.SelectedIndex].Id;
                payment.AmountUSD = Convert.ToDecimal(AmountUSD.EditValue);
                payment.AmountRUB = Convert.ToDecimal(AmountRUB.EditValue);
                payment.AmountEUR = Convert.ToDecimal(AmountEUR.EditValue);
                payment.AmountBYR = Convert.ToDecimal(AmountBYR.EditValue);
                payment.RateUSD = Convert.ToInt32(RateUSD.EditValue);
                payment.RateRUB = Convert.ToInt32(RateRUB.EditValue);
                payment.RateEUR = Convert.ToInt32(RateEUR.EditValue);
         */

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
            if (currencyID.SelectedIndex < 0)
            {
                selectedCurrency = 0;
                return;
            }

            selectedCurrency = payment.comboBoxCurrency[currencyID.SelectedIndex].Id;
            switch (selectedCurrency)
            {
                case (int) Enums.currencyID.byr:
                    {
                        AmountBYR.BackColor = Color.Red;
                        AmountBYR.Focus();
                        break;
                    }
                case (int)Enums.currencyID.eur:
                    {
                        AmountEUR.BackColor = Color.Red;
                        AmountEUR.Focus();
                        break;
                    }
                case (int)Enums.currencyID.rub:
                    {
                        AmountRUB.BackColor = Color.Red;
                        AmountRUB.Focus();
                        break;
                    }
                case (int)Enums.currencyID.usd:
                    {
                        AmountUSD.BackColor = Color.Red;
                        AmountUSD.Focus();
                        break;
                    }
            }
            
        }

        

        private void currencyID_EditValueChanged(object sender, EventArgs e)
        {
            if (currencyID.SelectedIndex < 0)
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
            setFontCurrency();
        }

        private void AmountUSD_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(AmountUSD.EditValue) < 0)
                AmountUSD.EditValue = 0;

            if (selectedCurrency != (int)Enums.currencyID.usd || !checkEdit_recalculate.Checked)
            {
                return;
            }
            if (Convert.ToInt32(RateEUR.EditValue) != 0)
                AmountEUR.EditValue = Math.Round((Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToInt32(RateEUR.EditValue))), 2);

            AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountUSD.EditValue) * Convert.ToDecimal(RateUSD.EditValue), 2);

            if (Convert.ToInt32(RateRUB.EditValue) != 0)
                AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToInt32(RateRUB.EditValue)), 2);
         
      
        }

        private void AmountBYR_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(AmountBYR.EditValue) < 0)
                AmountBYR.EditValue = 0;

            if (selectedCurrency != (int)Enums.currencyID.byr || !checkEdit_recalculate.Checked)
            {
                return;
            }
            if (Convert.ToInt32(RateUSD.EditValue) != 0)
                AmountUSD.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateUSD.EditValue), 2);
            if (Convert.ToInt32(RateEUR.EditValue) != 0)
                AmountEUR.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateEUR.EditValue), 2);
            if (Convert.ToInt32(RateRUB.EditValue) != 0)
                AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateRUB.EditValue), 2);
           
        }

        private void AmountEUR_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(AmountEUR.EditValue) < 0)
                AmountEUR.EditValue = 0;

            if (selectedCurrency != (int)Enums.currencyID.eur || !checkEdit_recalculate.Checked)
            {
                return;
            }

            if (Convert.ToInt32(RateUSD.EditValue) != 0)
                AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);

            AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountEUR.EditValue) * Convert.ToDecimal(RateEUR.EditValue), 2);
            
            if (Convert.ToInt32(RateRUB.EditValue) != 0)
                AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateRUB.EditValue)), 2);
         
        }

        private void AmountRUB_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(AmountRUB.EditValue) < 0)
                AmountRUB.EditValue = 0;
            if (selectedCurrency != (int)Enums.currencyID.rub || !checkEdit_recalculate.Checked)
            {
                return;
            }

            if (Convert.ToInt32(RateUSD.EditValue) != 0)
                AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);

            AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountRUB.EditValue) * Convert.ToDecimal(RateRUB.EditValue), 2);

            if (Convert.ToInt32(RateEUR.EditValue) != 0)
                AmountEUR.EditValue = Math.Round(Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateEUR.EditValue)), 2);
      
        }

        private void RateUSD_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(RateUSD.EditValue) < 0)
                RateUSD.EditValue = 0;
            
            if (!checkEdit_recalculate.Checked)
                return;

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

            if (!checkEdit_recalculate.Checked)
                return;

            if (selectedCurrency == (int)Enums.currencyID.byr)
            {
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                {
                    AmountEUR.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateEUR.EditValue), 2);
                }
            }
            if (selectedCurrency == (int)Enums.currencyID.usd)
            {
                 if(Convert.ToInt32(RateEUR.EditValue) != 0)
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

            if (!checkEdit_recalculate.Checked)
                return;

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
        #endregion Curensy Amount

        private void datepay_EditValueChanged(object sender, EventArgs e)
        {
            if (isControlLoaded)
                FillRateByDate();
        }

        private void simpleButton_cur_catalog_Click(object sender, EventArgs e)
        {
            FormCurencyCatalog form = new FormCurencyCatalog();
            form.ShowDialog();
            FillRateByDate(); //перечитывание курсов на дату
        }

    }
}
