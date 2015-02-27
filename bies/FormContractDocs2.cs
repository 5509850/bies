using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using bies.Entity;
using DevExpress.XtraGrid.Views.Base;

namespace bies
{
    public partial class FormContractDocs2 : Form
    {
        private ContractDoc contractdoc;
        private Files files;
        private bool blocked = false;
        private bool isloadedFinish = false;
        private Users curentUser = new Users();
        private int FileDocID;
        private bool afterEditCurCatalog = false;
        private int selectedCurrency;

        private long AmountFirst;

        private Currency cur;

        public FormContractDocs2(ContractDoc _contractdoc, Files files, string TradeName, bool lookReadonly, Users curentUser, int? FileDocID, bool isEdit, string contractName)
        {
            InitializeComponent();
            this.contractdoc = _contractdoc;
            this.curentUser = curentUser;
            this.files = files;
            this.AmountFirst = _contractdoc.AmnountS;
            if (FileDocID != null)
                this.FileDocID = (int)FileDocID;
            else
            {
                //блокируем блок одобрить/не одобрить
                groupBox42.Visible = false;
            }
            textEdit_tradename.Text = TradeName;
            textEdit_contractName.Text = contractName;//files.Title;
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            if (isEdit)
                this.Text = this.contractdoc.IsNew ? "Создание нового документа к контракту" : "Редактирование документа относящегося к контракту";
            else
                this.Text = "Просмотр документа к контракту";
// ReSharper restore DoNotCallOverridableMethodsInConstructor
            FillData();
            isloadedFinish = true;
            RecalculateS1();
            if (lookReadonly)
            {
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

                dateEdit_DateCreate.Properties.ReadOnly =
                    spinEdit_amountS.Properties.ReadOnly =
                    spinEdit_avanceA.Properties.ReadOnly =
                    spinEdit_retentionU.Properties.ReadOnly =
                    checkEdit_signed.Properties.ReadOnly =
                    checkEdit_signed.Properties.ReadOnly =
                    textEdit_title.Properties.ReadOnly =
                    ComboBox_currencyID.Properties.ReadOnly =
                    DataEdit_signedData.Properties.ReadOnly = true;

                simpleButton_save.Visible = false;

                simpleButton_cancel.Text = "OK";
//groupBox4.Enabled 
            }
        }

        private void FillData()
        {
            dateEdit_DateCreate.EditValue = files.Datecreate;//!!!!!!!!!!!!!!!!!! замена!!!!
            DataEdit_signedData.EditValue = files.Datelastupdate; //!!!!!!!!!!!!! замена!!!!
            spinEdit_amountS.EditValue = contractdoc.AmnountS;
            ComboBox_currencyID.SelectedIndex = contractdoc.CurrencyID;
            spinEdit_avanceA.EditValue = contractdoc.AvanceA;
            spinEdit_retentionU.EditValue = contractdoc.RetentionU;
            spinEdit_S1.EditValue = contractdoc.S1;

            avanceA.Text = ((int)(contractdoc.AvanceA*contractdoc.AmnountS/100)).ToString();
            retentionU.Text = ((int)(contractdoc.RetentionU * contractdoc.AmnountS / 100)).ToString();

            textEdit_FileName.Text = files.Filename;
            textEdit_title.Text = files.Title;
            checkEdit_signed.Checked = files.Signed;
            checkEdit_isBaseOrShared.Checked = files.IsFileInBaseOrInShare;
            fillGridApproved(gridControl_contractSigned, FileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contractSigned);
            FillRateByDate();
        }
       
        private void Check()
        {
           if (String.IsNullOrEmpty(textEdit_title.Text))
           {
               MessageBox.Show("Не введено название документа!");
               textEdit_title.Focus();
               blocked = true;
               return;
           }

           if (dateEdit_DateCreate.EditValue == null)
            {
                MessageBox.Show("Не введена дата создания документа!");
                dateEdit_DateCreate.Focus();
                blocked = true;
                return;
            }

            if (spinEdit_amountS.EditValue == null || Convert.ToInt64(spinEdit_amountS.EditValue) == 0)
            {
                MessageBox.Show("Не введена сумма!");
                spinEdit_amountS.Focus();
                blocked = true;
                return;
            }
            if (ComboBox_currencyID.SelectedIndex == 0)
            {
                MessageBox.Show("Не выбрана валюта!");
                ComboBox_currencyID.Focus();
                blocked = true;
                return;
            }
            if (DataEdit_signedData.EditValue == null)
            {
                MessageBox.Show("Не введена дата подписания!");
                DataEdit_signedData.Focus();
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

        private void ChangeContractSumm(long summ)
        {
            int id = contractdoc.ContractID;
            Contract contract = new Contract();
            contract.ContractID = contractdoc.ContractID;
            contract.Amount = summ;
            contract.IsChandgedSumm = true;
            contract.UpdateSumm();

        }

        private void simpleButton_save_Click(object sender, EventArgs e)
        {

            Check();

            files.Datecreate = Convert.ToDateTime(dateEdit_DateCreate.EditValue);//!!!!!!!!!!!!!!!!!
            contractdoc.IsActive = true;
            contractdoc.IsNew = false;
            files.Title = textEdit_title.Text;
            files.Signed = Convert.ToBoolean(checkEdit_signed.Checked);

            if (Convert.ToInt64(spinEdit_amountS.EditValue) != AmountFirst)
            {
                switch (MessageBox.Show(String.Format("Сумма контракта была изменена с {0} на {1} Сохранять изменения в контракте?", AmountFirst, spinEdit_amountS.EditValue), "Изменение суммы контракта", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.OK:
                        {
                            ChangeContractSumm(Convert.ToInt64(spinEdit_amountS.EditValue));
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            break;
                        }
                }
               
            }
            spinEdit_amountS.EditValue = contractdoc.AmnountS;

            contractdoc.CurrencyID = ComboBox_currencyID.SelectedIndex;
            contractdoc.AmnountS = Convert.ToInt64(spinEdit_amountS.EditValue);
            contractdoc.AvanceA = Convert.ToInt32(spinEdit_avanceA.EditValue);
            contractdoc.RetentionU = Convert.ToInt32(spinEdit_retentionU.EditValue);
            contractdoc.S1 = Convert.ToInt64(spinEdit_S1.EditValue);

            
            contractdoc.CurrencyID = ComboBox_currencyID.SelectedIndex;

            files.Datelastupdate = Convert.ToDateTime(DataEdit_signedData.EditValue);//!!!!!!!!!!!

            
            //редактирование contractdoc doc с заменой на новый файл
            if (!contractdoc.IsNew && files.IsNew)
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
       

        private void spinEdit_amountS_EditValueChanged(object sender, EventArgs e)
        {
            RecalculateS1();
            if (radioButton_amountS.Checked)
            {
                SetEquvalentAmount();
            }
                
        }

        private void spinEdit_avanceA_EditValueChanged(object sender, EventArgs e)
        {
            RecalculateS1();
        }

        private void spinEdit_retentionU_EditValueChanged(object sender, EventArgs e)
        {
            RecalculateS1();
        }

        private void RecalculateS1()
        {
            if (isloadedFinish) //блокирование перерасчета до окончания загрузки данных
            {
               long S = contractdoc.AmnountS = Convert.ToInt64(spinEdit_amountS.EditValue);
               int A = contractdoc.AvanceA = Convert.ToInt32(spinEdit_avanceA.EditValue);
               int Y = contractdoc.RetentionU = Convert.ToInt32(spinEdit_retentionU.EditValue);

               if (S < 0)
               {
                   contractdoc.AmnountS = 0;
                   spinEdit_amountS.EditValue = 0;
               }
               if (A < 0)
               {
                   contractdoc.AvanceA = 0; 
                   spinEdit_avanceA.EditValue = 0;
               }
               if (Y < 0)
               {
                   contractdoc.RetentionU = 0; 
                   spinEdit_retentionU.EditValue = 0;
               }

                spinEdit_S1.EditValue = contractdoc.AmnountS - ((contractdoc.AmnountS * contractdoc.AvanceA) / 100) -
                                        ((contractdoc.AmnountS * contractdoc.RetentionU) / 100);

                avanceA.Text = ((int)(contractdoc.AvanceA * contractdoc.AmnountS / 100)).ToString();
                retentionU.Text = ((int)(contractdoc.RetentionU * contractdoc.AmnountS / 100)).ToString();
            }
        }

        private void simpleButton_ApproveContract_Click(object sender, EventArgs e)
        {
            CreateApproveContract();
        }

        private void CreateApproveContract()
        {
            int? selectedFileDocID = FileDocID;
            if (selectedFileDocID == null)
                return;
            Approve approve = new Approve
            {
                ApproveDate = DateTime.Now,
                ApproveUserID = curentUser.UserID,
                Approved = true,
                FiledocID = ((int)selectedFileDocID),
                IsActive = true,
                IsNew = true,
                Remark = String.Empty,
                Title = "Одобрено"
            };

            FormApproveAdd form = new FormApproveAdd(approve);

            switch (form.ShowDialog())
            {
                case DialogResult.Yes:
                    {
                        approve.Approved = true;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 762128 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contractSigned);
                        //gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned
                        memoEdit5.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 62129 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contractSigned);
                        memoEdit5.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void fillGridApproved(DevExpress.XtraGrid.GridControl gcSigned, int filedocId, Control memo, Control button, ColumnView gvSigned)
        {
            //gridControl_tenderDocSigned, (int)selectedFileDocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned

            memo.Text = String.Empty; //
            Approve approve = new Approve();
            approve.FiledocID = filedocId;
            approve.GetListAllApproveByFiledocID(gcSigned);
            //блокрировка повторного вынесения резолюции
            button.Enabled = !isApproveExist(gvSigned);//simpleButton_approveTenderDoc
            //заполнение текстбокса резолюции
            Approve ap = GetSelected_Approve_fromgridViewSigned(gvSigned);
            memo.Text = ap != null ? ap.Remark : String.Empty;
        }

        private Approve GetSelected_Approve_fromgridViewSigned(ColumnView gvSigned)
        {
            if (GetSelectedApproveID(gvSigned) == null)
                return null;

            int? selectedFileDocID = FileDocID;
            if (selectedFileDocID == null)
                return null;

            Approve approve = new Approve();
            approve.FiledocID = (int)selectedFileDocID;
            approve.ApproveID = (int)GetSelectedApproveID(gvSigned);
            approve.Approved = Convert.ToBoolean(gvSigned.GetRowCellValue(gvSigned.GetSelectedRows()[0], "approve"));
            approve.ApproveDate = Convert.ToDateTime(gvSigned.GetRowCellValue(gvSigned.GetSelectedRows()[0], "approveDate"));
            approve.ApproveUserID = Convert.ToInt32(gvSigned.GetRowCellValue(gvSigned.GetSelectedRows()[0], "approveUserID"));
            approve.Remark = (gvSigned.GetRowCellValue(gvSigned.GetSelectedRows()[0], "remark")).ToString();
            approve.Title = (gvSigned.GetRowCellValue(gvSigned.GetSelectedRows()[0], "title")).ToString();
            approve.IsNew = false;
            approve.IsActive = true;

            return approve; //   

        }

        private static int? GetSelectedApproveID(ColumnView gvSigned)
        {
            int result;
            if (gvSigned.RowCount == 0)
                return null;
            if (gvSigned.GetSelectedRows().Length == 0)
                return null;
            var vol = gvSigned.GetRowCellValue(gvSigned.GetSelectedRows()[0], "approveID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   
        }

        private bool isApproveExist(ColumnView gvSigned)
        {
            if (gvSigned.RowCount == 0)
                return false;
            for (int row = 0; row < gvSigned.RowCount; row++)
            {
                if (Convert.ToInt32(gvSigned.GetRowCellValue(row, "approveUserID")) == curentUser.UserID)
                    return true;
            }
            return false;
        }

        private void simpleButton_editContractSign_Click(object sender, EventArgs e)
        {
            EditeContractApprove();
        }

        private void EditeContractApprove()
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_contractSigned);
            if (approve == null)
                return;
            if (curentUser.UserID != approve.ApproveUserID)
                return;
            approve.ApproveDate = DateTime.Now;
            approve.ApproveUserID = curentUser.UserID;

            FormApproveAdd form = new FormApproveAdd(approve);

            switch (form.ShowDialog())
            {
                case DialogResult.Yes:
                    {
                        approve.Approved = true;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 13129 - EditeContractApprove");
                        //gridControl_contractSigned, approve.FiledocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned

                        fillGridApproved(gridControl_contractSigned, approve.FiledocID, memoEdit5, simpleButton_ApproveContract, gridView_contractSigned);
                        memoEdit5.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 13130 - EditeContractApprove()");
                        fillGridApproved(gridControl_contractSigned, approve.FiledocID, memoEdit5, simpleButton_ApproveContract, gridView_contractSigned);
                        memoEdit5.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_DeleteContractSign_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Удалить резолюцию?", "Удаление резолюции", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        int? selectedFileDocID = FileDocID;
                        if (selectedFileDocID == null)
                            return;
                        DeleteApprove(gridView_contractSigned);
                        fillGridApproved(gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contractSigned);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void DeleteApprove(ColumnView gvSigned)
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gvSigned);
            if (approve == null)
                return;
            if (curentUser.UserID != approve.ApproveUserID)
                return;
            approve.IsActive = false;
            if (!approve.SaveOrUpdate())
                MessageBox.Show("Ошибка  удаления 131 - approve.SaveOrUpdate()");
        }

        private bool IsAccessToAproveEdit()
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_contractSigned);
            if (approve == null)
            {
                memoEdit5.Text = String.Empty;
                return false;
            }

            memoEdit5.Text = approve.Remark;
            if (curentUser.UserID != approve.ApproveUserID)
            {
                return false;
            }
            
            return true;
        }

        private void gridView_contractSigned_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            simpleButton_editContractSign.Enabled = simpleButton_DeleteContractSign.Enabled = IsAccessToAproveEdit();
        }

        private void ComboBox_currencyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEquvalentAmount();
        }

        private void SetEquvalentAmount()
        {
            if (Convert.ToDecimal(AmountUSD.EditValue) < 0)
                AmountUSD.EditValue = 0;

            if (selectedCurrency == (int)Enums.currencyID.usd)
            {
                AmountUSD.EditValue = radioButton_amountS.Checked ? spinEdit_amountS.EditValue : spinEdit_S1.EditValue;

                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                    AmountEUR.EditValue = Math.Round((Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToInt32(RateEUR.EditValue))), 2);

                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountUSD.EditValue) * Convert.ToDecimal(RateUSD.EditValue), 2);

                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                    AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountUSD.EditValue) * (Convert.ToDecimal(RateUSD.EditValue) / Convert.ToInt32(RateRUB.EditValue)), 2);

            }

            if (selectedCurrency == (int)Enums.currencyID.eur)
            {
                AmountEUR.EditValue = radioButton_amountS.Checked ? spinEdit_amountS.EditValue : spinEdit_S1.EditValue;
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                    AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);

                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountEUR.EditValue) * Convert.ToDecimal(RateEUR.EditValue), 2);

                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                    AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountEUR.EditValue) * (Convert.ToDecimal(RateEUR.EditValue) / Convert.ToDecimal(RateRUB.EditValue)), 2);

            }

            if (selectedCurrency == (int)Enums.currencyID.rub)
            {
                AmountRUB.EditValue = radioButton_amountS.Checked ? spinEdit_amountS.EditValue : spinEdit_S1.EditValue;
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                    AmountUSD.EditValue = Math.Round((Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateUSD.EditValue))), 2);

                AmountBYR.EditValue = Math.Round(Convert.ToDecimal(AmountRUB.EditValue) * Convert.ToDecimal(RateRUB.EditValue), 2);

                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                    AmountEUR.EditValue = Math.Round(Convert.ToDecimal(AmountRUB.EditValue) * (Convert.ToDecimal(RateRUB.EditValue) / Convert.ToDecimal(RateEUR.EditValue)), 2);

            }

            if (selectedCurrency == (int)Enums.currencyID.byr)
            {
                
                AmountBYR.EditValue = radioButton_amountS.Checked ? spinEdit_amountS.EditValue : spinEdit_S1.EditValue;
                if (Convert.ToInt32(RateUSD.EditValue) != 0)
                    AmountUSD.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateUSD.EditValue), 2);
                if (Convert.ToInt32(RateEUR.EditValue) != 0)
                    AmountEUR.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateEUR.EditValue), 2);
                if (Convert.ToInt32(RateRUB.EditValue) != 0)
                    AmountRUB.EditValue = Math.Round(Convert.ToDecimal(AmountBYR.EditValue) / Convert.ToDecimal(RateRUB.EditValue), 2);


            }



        }
       

        private void FillRateByDate()
        {

            if (!GetCurrencyRateByDataTime())
            {
                if (dateEdit_currency.EditValue == null)
                    return;
                DateTime date = Convert.ToDateTime(dateEdit_currency.EditValue);


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
            if (dateEdit_currency.EditValue == null)
                return false;
            DateTime date = Convert.ToDateTime(dateEdit_currency.EditValue);

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
     
        private void spinEdit_S1_EditValueChanged(object sender, EventArgs e)
        {
            if (radioButton_S1.Checked)
                SetEquvalentAmount();
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




        private void dateEdit_DateCreate_EditValueChanged(object sender, EventArgs e)
        {
            dateEdit_currency.EditValue = dateEdit_DateCreate.EditValue;
        }

        private void simpleButton_currency_catalog_Click(object sender, EventArgs e)
        {
            if (dateEdit_currency.EditValue == null)
                return;
            DateTime date = Convert.ToDateTime(dateEdit_currency.EditValue);
            FormCurencyCatalog form = new FormCurencyCatalog(date.Date);
            form.ShowDialog();
            afterEditCurCatalog = true;
            FillRateByDate();
        }

        private void dateEdit_currency_EditValueChanged(object sender, EventArgs e)
        {
            FillRateByDate();
        }

        private void radioButton_S1_CheckedChanged(object sender, EventArgs e)
        {
            SetEquvalentAmount();
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
