﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using bies.Entity;

namespace bies
{
    public partial class FormRatingReportDocAdd : Form
    {
        private RatingReport ratingReport;
        private Files files;
        private bool blocked = false;
        private List<ComboBoxEditTrade> comboBoxEditTradeList;
        private int tradeID;


        public FormRatingReportDocAdd(RatingReport ratingReport, Files files, List<ComboBoxEditTrade> comboBoxEditTradeList)
        {
            InitializeComponent();
            this.ratingReport = ratingReport;
            this.files = files;

            this.comboBoxEditTradeList = comboBoxEditTradeList;
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Text = this.ratingReport.IsNew ? "Создание нового оценочного отчета" : "Редактирование оценочного отчета";
// ReSharper restore DoNotCallOverridableMethodsInConstructor
            foreach (ComboBoxEditTrade _trade in comboBoxEditTradeList)
            {
                comboBoxEdit_trade.Properties.Items.Add(_trade.NameTrade);
            }
            comboBoxEdit_trade.SelectedIndex = GetSelectedIndexForComboBoxEdit();

            FillData();
        }

        public int TradeID
        {
            get { return tradeID; }
            set { tradeID = value; }
        }

        private void FillData()
        {
            checkEdit_sendToBank.Checked = ratingReport.Sendtobank;
            dateEdit_DateCreate.EditValue = ratingReport.DateCreate;
            check_approveRatingCommission.Checked = ratingReport.ApproveRatingCommission;
            dateEdit_approveRatingCommission.EditValue = ratingReport.ApproveRatingDate;
            check_approveTenderCommission.Checked = ratingReport.ApproveTenderCommission;
            DateEdit_approveTenderCommission.EditValue = ratingReport.ApproveTenderDate;
            check_approveBank.Checked = ratingReport.ApproveBank;
            DateEdit_approveBank.EditValue = ratingReport.ApproveBankDate;

            textEdit_FileName.Text = files.Filename;
            textEdit_title.Text = files.Title;
            dateEdit_fileDateCreate.EditValue = files.Datecreate;
            dateEdit_las.EditValue = files.Datelastupdate;
            checkEdit_signed.Checked = files.Signed;
            checkEdit_isBaseOrShared.Checked = files.IsFileInBaseOrInShare;
        }

        private int GetSelectedIndexForComboBoxEdit()
        {
            if (comboBoxEdit_trade.Properties.Items.Count != comboBoxEditTradeList.Count)
                return -1;
            int result = 0;
            foreach (ComboBoxEditTrade _trade in comboBoxEditTradeList)
            {
                if (_trade.TradeID == ratingReport.TradeID)
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

           if (dateEdit_DateCreate.EditValue == null)
            {
                MessageBox.Show("Не введена дата создания документа!");
                dateEdit_DateCreate.Focus();
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

            ratingReport.Sendtobank = Convert.ToBoolean(checkEdit_sendToBank.Checked);
            ratingReport.DateCreate = Convert.ToDateTime(dateEdit_DateCreate.EditValue);
            ratingReport.IsActive = true;
            ratingReport.IsNew = false;
            files.Title = textEdit_title.Text;
            files.Signed = Convert.ToBoolean(checkEdit_signed.Checked);
            ratingReport.TradeID = TradeID;

            ratingReport.ApproveRatingCommission = check_approveRatingCommission.Checked;
            ratingReport.ApproveRatingDate = Convert.ToDateTime(dateEdit_approveRatingCommission.EditValue);
            ratingReport.ApproveTenderCommission = check_approveTenderCommission.Checked;
            ratingReport.ApproveTenderDate = Convert.ToDateTime(DateEdit_approveTenderCommission.EditValue);
            ratingReport.ApproveBank = check_approveBank.Checked;
            ratingReport.ApproveBankDate = Convert.ToDateTime(DateEdit_approveBank.EditValue);

            //редактирование tender doc с заменой на новый файл
            if (!ratingReport.IsNew && files.IsNew)
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
    }
}