using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using System.Windows.Forms;
using System.Xml.Serialization;
using bies.Entity;
using DevExpress.XtraGrid.Views.Base;

namespace bies
{
    //sa:p123456789
    public partial class FormMain : Form
    {
        private Users curentUser = new Users();
        private Trades trade = new Trades();
        private List<TypeDoc> typeDocList; //тип документа
        private List<InviteStatus> InviteStatusList = new List<InviteStatus>(); //тип документа
        private int? selectedTradeId; //ID выбранных торгов для использования в других табах
        private List<Trades> tradeList; //список торгов
        private bool isTradeTabLoaded = false;//данные таба загружены - для блокировки обновления по фильтру даты

        private List <ComboBoxEditTrade> comboBoxEditTradeList = new List<ComboBoxEditTrade>();
        //private List<TenderDocs> tenderDocsList;
        private long MaxSizeFileInBase;

        #region fields
        public DateTime TradeFiltrDateFrom
        {
            get { return Convert.ToDateTime(dateEdit_tradeDateFrom.EditValue); }
            set { dateEdit_tradeDateFrom.EditValue = value; }
        }
        #endregion fields


        public FormMain()
        {
            InitializeComponent();
            LoadSetting();
            LoadTypeDocListAndInviteStatus();
            if (curentUser.GroupID != (int)Enums.group.buh)
                TradeTabFillData();//загрузить данные по торгам
            ApproveAccessRight();//применить права доступа к редактированию - зависит от группы
            isTradeTabLoaded = true;
            MaxSizeFileInBase = Convert.ToInt64(Properties.Settings.Default.MaxSizeFileInBase);
        }

        private void ApproveAccessRight()
        {
            //gridView_trade.OptionsBehavior.Editable = false; 
            switch (curentUser.GroupID)
            {
                case (int)Enums.group.admin:
                    {
                        break;
                    }
                case (int)Enums.group.boss:
                    {
                        checkEdit_all.Visible = false;

                        
                        tabControl_forms.TabPages[(int)Enums.Tabs.users6].PageVisible =
                        navBarGroup_spravochniki.Visible =
                        navBarGroup_admin.Visible =
                        groupBox_edit.Visible =
                        simpleButton_AddDocTabTrade.Visible = 
                        groupBox_doc_add.Visible =
                        groupBox_TenderEdit.Visible =
                        groupBox_invite.Visible =
                        groupBox12.Visible =
                        groupBox11.Visible =
                        groupBox26.Visible =
                        groupBox25.Visible =
                        groupBox33.Visible =
                        groupBox_AddContracts.Visible =
                        groupBox40.Visible =
                        //groupBox39.Visible =
                        //groupBox46.Visible = 
                        sb_AddcontractDocs.Visible =
                        SB_EditcontractDocs.Visible =
                        SB_AddcontractDocWorks.Visible =
                        SB_DeletecontractDocs.Visible =
                        SB_EditcontractDocWorks.Visible =
                        SB_DeletecontractDocWorks.Visible =

                         navBarItem_s_zaim.Visible =
                            navBarItem_s_oblast.Visible =
                            navBarItem_s_cat_contract.Visible =
                            navBarItem_s_cat_work.Visible =
                            navBarItem_s_subcat_work.Visible =
                            navBarItem_s_contragents.Visible =
                            navBarItem_s_contract.Visible =
                            navBarItem_s_object.Visible =

                        navBarItem_payment_add.Visible =
                            navBarItem_payment_edit.Visible =
                            navBarItem_payment_delete.Visible = 
                            
                            simpleButton_payment_Add.Visible =
                            simpleButton_payment_plan_add.Visible =
                            simpleButton_payment_delete.Visible =
                            

                            simpleButton_finance_direct_add.Visible =
                            simpleButton_finance_spec_account_Add.Visible =

                            navBarItem_report.Visible =
                            false;
                        break;
                    }
                case (int)Enums.group.user:
                    {
                        tabControl_forms.TabPages[(int)Enums.Tabs.users6].PageVisible =
                            tabControl_forms.TabPages[(int)Enums.Tabs.payment7].PageVisible =
                            tabControl_forms.TabPages[(int)Enums.Tabs.finance8].PageVisible = 
                        navBarGroup_admin.Visible =
                        //groupBox_approve.Visible = 
                         navBarItem_payment_add.Visible =
                            navBarItem_payment_edit.Visible =
                            navBarItem_payment_delete.Visible = 
                            
                            navBarItem_s_zaim.Visible =
                            navBarItem_s_oblast.Visible =
                            navBarItem_s_cat_contract.Visible =
                            navBarItem_s_cat_work.Visible = 
                            navBarItem_s_subcat_work.Visible = 
                            navBarItem_s_contragents.Visible = 
                            navBarItem_s_contract.Visible = 
                            navBarItem_s_object.Visible =

                            navBarItem_payment.Visible = 
                            navBarItem_report.Visible =
                            
                            false;
                        break;
                    }
                case (int)Enums.group.buh:
                    {
                        navBarGroup_trade.Caption = "Платежи";

                        tabControl_forms.TabPages[(int)Enums.Tabs.trades0].PageVisible =
                            tabControl_forms.TabPages[(int)Enums.Tabs.tenderDocs1].PageVisible = 
                            tabControl_forms.TabPages[(int)Enums.Tabs.invite2].PageVisible =
                            tabControl_forms.TabPages[(int)Enums.Tabs.openprotocol3].PageVisible =
                            tabControl_forms.TabPages[(int)Enums.Tabs.ratingprotokol4].PageVisible =
                            tabControl_forms.TabPages[(int)Enums.Tabs.contract5].PageVisible = 
                        tabControl_forms.TabPages[(int)Enums.Tabs.users6].PageVisible =
                        navBarGroup_admin.Visible = false;

                        //navBarItem_currency.Visible =
                            navBarItem_templateDoc.Visible = 
                            navBarItem_projectName.Visible = 
                            navBarItem_payment.Visible =

                        navBarItem_trades.Visible =
                        navBarItem_tendersDocs.Visible =
                        navBarItem_Invites.Visible = 
                        navBarItem_openProtokol.Visible = 
                        navBarItem_RatingReport.Visible =
                        navBarItem_Contracts.Visible = false;

                        break;
                    }
            }
        }
        
        /// <summary>
        /// Загрузка настроек из файла  settings.xml
        /// curentUser и TradeFiltrDateFrom
        /// </summary>
        private void LoadSetting()
        {
            if (!File.Exists("settings.xml"))
                return;

            try
            {
                var deser = new XmlSerializer(typeof(LoginSetting));
                TextReader reader = new StreamReader("settings.xml");
                LoginSetting settings = ((LoginSetting)deser.Deserialize(reader));
                reader.Close();
                curentUser.FillUserFieldsByLogin(settings.L);
                TradeFiltrDateFrom = settings.TradeDateFrom;
                Text += ": " + curentUser.Fio;
            }
            catch
            {
                TradeFiltrDateFrom = new DateTime(2011, 1, 1);
                return;
            }

        }

        private void LoadTypeDocListAndInviteStatus()
        {
            //получаем список типов документа и инвайтстатусов
            TypeDoc typeDoc = new TypeDoc();
            typeDocList = typeDoc.GetListTypeDoc(InviteStatusList);
        }

        private bool SaveSettings()
        {
            try
            {
                LoginSetting settings = new LoginSetting();
                settings.L = curentUser.Login;
                settings.TradeDateFrom = TradeFiltrDateFrom;
                var ser = new XmlSerializer(typeof(LoginSetting));
                TextWriter writer = new StreamWriter("settings.xml");
                ser.Serialize(writer, settings);
                writer.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        #region NavBarItemsClick

        private void navBarItem_trades_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.trades0;
        }

        private void navBarItem_tendersDocs_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.tenderDocs1;
        }

        private void navBarItem_Invites_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.invite2;
        }

        private void navBarItem_openProtokol_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int) Enums.Tabs.openprotocol3;
        }

        private void navBarItem_RatingReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int) Enums.Tabs.ratingprotokol4;
        }

        private void navBarItem_Contracts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int) Enums.Tabs.contract5;
        }

        private void navBarItem_users_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.users6;
        }

        private void navBarItem_payment_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.payment7;
        }

        private void navBarItem_currency_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormCurencyCatalog form = new FormCurencyCatalog();
            form.ShowDialog();
        }

        #endregion NavBarItemsClick----------------------------------------------------------------------------------

        private void tabControl_forms_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (tabControl_forms.SelectedTabPageIndex)
            {
                case (int)Enums.Tabs.trades0:
                    {
                        fillDocsForTrade();
                        break;
                    }

                case (int)Enums.Tabs.tenderDocs1:
                    {
                        FillTenderDocTab();
                        break;
                    }

                case (int)Enums.Tabs.invite2:
                    {
                        FillInviteTab();
                        break;
                    }

                case (int)Enums.Tabs.openprotocol3:
                    {
                        FillOpenProtololTab();
                        break;
                    }

                case (int)Enums.Tabs.ratingprotokol4:
                    {
                        FillRatingReportTab();
                        break;
                    }

                case (int)Enums.Tabs.contract5:
                    {
                        FillContractTab();
                        break;
                    }

                case (int)Enums.Tabs.users6:
                    {
                        UserTabFillData();
                        break;
                    }

                case (int)Enums.Tabs.payment7:
                    {
                        FillPaymentTab();
                        break;
                    }

                case (int)Enums.Tabs.finance8:
                    {
                        FillFinanceTab();
                        break;
                    }

            }
        }

        #region userTab--------------------------------------------------------
        private void simpleButton_save_Click(object sender, EventArgs e)
        {
            //update user
            if (String.IsNullOrEmpty(textEdit_userLogin.Text))
            {
                MessageBox.Show("Не введен логин");
                textEdit_userLogin.Focus();
                return;
            }
            if (String.IsNullOrEmpty(textEdit_userFio.Text))
            {
                MessageBox.Show("Не введено ФИО");
                textEdit_userFio.Focus();
                return;
            }
            if (comboBoxEdit_userGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбрана группа");
                comboBoxEdit_userGroup.Focus();
                return;
            }
           // доделать update user !!!!!


            if (CheckExistUserForUpdate())
            {
                MessageBox.Show(String.Format("{0} - такой логин уже существует, выберите другой", textEdit_userNewLogin.Text));
                textEdit_userLogin.Focus();
            }
            else
            {
                UpdateUser();
            }
        }

        private void simpleButton_userPassChange_Click(object sender, EventArgs e)
        {
            if (GetSelectedUser() == null) return;
            Users updateUser = GetSelectedUser();
            updateUser.NeedChangPassword = !updateUser.NeedChangPassword;
            if (updateUser.UpdateUser())
            {
                gridControl_users.DataSource = curentUser.GetListAllUsers();//refresh gridvew
            }
            else
            {
                MessageBox.Show("Возникла ошибка при обновлении пользователя!");
            }
        }

        private void simpleButton_userClearPassword_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Обнулять пароль?", "Обнуление пароля", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (GetSelectedUser() == null) return;
            Users updateUser = GetSelectedUser();
            updateUser.NeedChangPassword = true;
            if (updateUser.ClearPass())
            {
                gridControl_users.DataSource = curentUser.GetListAllUsers();//refresh gridvew
            }
            else
            {
                MessageBox.Show("Возникла ошибка при обнулении пароля пользователя!");
            }
        }

        private void simpleButton_delUser_Click(object sender, EventArgs e)
        {
            if (GetSelectedUser() == null) return;
            Users updateUser = GetSelectedUser();
            updateUser.IsActive = !updateUser.IsActive;
            if (updateUser.UpdateUser())
            {
                gridControl_users.DataSource = curentUser.GetListAllUsers();//refresh gridvew
            }
            else
            {
                MessageBox.Show("Возникла ошибка при обновлении пользователя!");
            }
        }

        private void simpleButton_newUser_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textEdit_userNewLogin.Text))
            {
                MessageBox.Show("Не введен логин");
                textEdit_userNewLogin.Focus();
                return;
            }
            if (String.IsNullOrEmpty(textEdit_userNewFio.Text))
            {
                MessageBox.Show("Не введено ФИО");
                textEdit_userNewFio.Focus();
                return;
            }
            if (Edit_userNewGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбрана группа");
                Edit_userNewGroup.Focus();
                return;
            }

            if (CheckExistLogin(textEdit_userNewLogin.Text))
            {
                MessageBox.Show(String.Format("{0} - такой логин уже существует, выберите другой", textEdit_userNewLogin.Text));
            }
            else
            {
                CreateNewUser();
            }
        }

        private void gridView_users_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (GetSelectedUser() == null)
            {
                textEdit_userLogin.Text = textEdit_userFio.Text = String.Empty;
                simpleButton_save.Enabled =
                    simpleButton_delUser.Enabled =
                    simpleButton_userPassChange.Enabled =
                    simpleButton_userClearPassword.Enabled = false;
                return;
            }

            simpleButton_save.Enabled =
                simpleButton_delUser.Enabled =
                simpleButton_userPassChange.Enabled =
                simpleButton_userClearPassword.Enabled = true;
            textEdit_userLogin.Text = GetSelectedUser().Login;
            textEdit_userFio.Text = GetSelectedUser().Fio;
            comboBoxEdit_userGroup.SelectedIndex = GetSelectedUser().GroupID - 1;
        }

        private bool CheckExistUserForUpdate()
        {
            if (GetSelectedUser() == null) return false;

            if (textEdit_userLogin.Text == GetSelectedUser().Login)
                return false; //new login not changed for update
            return CheckExistLogin(textEdit_userLogin.Text);
        }

        private void UpdateUser()
        {
            Users updateUser = GetSelectedUser();
            updateUser.Login = textEdit_userLogin.Text;
            updateUser.Fio = textEdit_userFio.Text;
            updateUser.GroupID = comboBoxEdit_userGroup.SelectedIndex + 1;
            if (updateUser.UpdateUser())
            {
                gridControl_users.DataSource = curentUser.GetListAllUsers();//refresh gridvew
            }
            else
            {
                MessageBox.Show("Возникла ошибка при обновлении пользователя!");
            }
        }
        
        private void CreateNewUser()
        {
            Users newUser = new Users();
            newUser.Login = textEdit_userNewLogin.Text;
            newUser.Fio = textEdit_userNewFio.Text;
            newUser.GroupID = Edit_userNewGroup.SelectedIndex + 1;
            newUser.IsActive = true;
            newUser.Password = utils.GetHashString(String.Empty); //новый пользователь без пароля
            newUser.NeedChangPassword = true;
            if (newUser.CreateNewUser())
            {
                gridControl_users.DataSource = curentUser.GetListAllUsers();//refresh gridvew
                textEdit_userNewLogin.Text = textEdit_userNewFio.Text = String.Empty;
            }
            else
            {
                MessageBox.Show("Возникла ошибка при создании нового пользователя!");
            }
        }

        private Users GetSelectedUser()
        {
            return (Users)gridView_users.GetRow(gridView_users.GetSelectedRows()[0]);
        }

        private void UserTabFillData()
        {
            gridControl_users.DataSource = curentUser.GetListAllUsers();
            Edit_userNewGroup.SelectedIndex = (int)Enums.group.user - 1;//default new user grop is USER
        }

        private Users GetUserByRow(int row)
        {
            return (Users)gridView_users.GetRow(row);
        }

        private bool CheckExistLogin(string login)
        {
            for (int row = 0; row < gridView_users.RowCount; row++)
            {
                if (GetUserByRow(row).Login.Equals(login))
                    return true;
            }
            return false;
        }
        #endregion userTab

        /// <summary>
        /// применить права доступа к редактированию - зависит от группы
        /// </summary>
        
        

        #region tradeTab-------------------------------------------------------------------------

        //показать торги по всем работникам 
        private void checkEdit_all_CheckedChanged(object sender, EventArgs e)
        {
            TradeTabFillData();
            //при просмотре всех запрещено редактирование/удаление
            ApproveAccessEditAndDeleteAll();
        }

        private void ApproveAccessEditAndDeleteAll()
        {
                    simpleButton_tradeEdit.Visible =
                    groupBox_edit.Visible = 
                    groupBox_doc_add.Visible =
                    groupBox_TenderEdit.Visible = 
                    groupBox_invite.Visible = 
                    groupBox12.Visible = 
                    groupBox11.Visible = 
                    groupBox26.Visible = 
                    groupBox25.Visible = 
                    groupBox33.Visible = 
                    groupBox_AddContracts.Visible = 
                    groupBox40.Visible = 
                    groupBox39.Visible =
                    groupBox46.Visible = !checkEdit_all.Checked;
            
        }


        private void TradeTabFillData()
        {
            if (trade == null)
                trade = new Trades();
            switch (curentUser.GroupID)
            {
                case (int)Enums.group.admin:
                    {
                        tradeList = trade.GetListAllTrades(gridControl_trade, TradeFiltrDateFrom);
                        break;
                    }
                    case (int)Enums.group.boss:
                    {
                        tradeList = trade.GetListAllTrades(gridControl_trade, TradeFiltrDateFrom);
                        break;
                    }
                    case (int)Enums.group.user:
                    {
                        tradeList = checkEdit_all.Checked ? trade.GetListAllTrades(gridControl_trade, TradeFiltrDateFrom) : trade.GetListAllTradesByUserID(gridControl_trade, TradeFiltrDateFrom, curentUser.UserID);

                        break;
                    }
                case (int)Enums.group.buh:
                    {
                        ///:TODO Add for buh here!!!!!!!!!!!!!!!!!!!!!
                        break;
                    }
            }

            selectedTradeId = GetSelectedTradeID();
            
            if (GetSelectedTradeID() != null)
            {
                fillDocsForTrade();//показать какие документы есть по торгам и подписаны
            }
        }

        //показать какие документы есть по торгам
        private void fillDocsForTrade()
        {
            trade = GetSelectedTrade();
            if (trade != null)
                trade.fillDocsForTradeTab(gridControl_trade_doc);
            if (gridView_trade_doc.RowCount != 0)
                FillGridSigned();//заполнить грил с подписями по документам
            else
                gridControl_doc_signed.DataSource = null;
        }
        

        private void dateEdit_tradeDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            if (isTradeTabLoaded)
            {
                TradeTabFillData();
                SaveSettings();
            }
        }
        

        private void simpleButton_tradeNew_Click(object sender, EventArgs e)
        {
            CreateTrade();
        }
        

        private void simpleButton_tradeEdit_Click(object sender, EventArgs e)
        {
            EditTrade();
        }

        private void simpleButton_tradeDelete_Click(object sender, EventArgs e)
        {
            DeleteTrade();
        }
        
        private void DeleteTrade()
        {
            trade = GetSelectedTrade();
            if (trade == null)
                return;

            switch (MessageBox.Show("Удалить торги?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        trade.IsActive = false;
                        if (!trade.CreateOrUpdate())
                            MessageBox.Show("Ошибка удаления!");
                        TradeTabFillData();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void gridView_trade_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedTradeId = GetSelectedTradeID();
            if (selectedTradeId == null)
            {
               simpleButton_tradeEdit.Enabled =
                    simpleButton_tradeDelete.Enabled =
                    simpleButton_AddDocTabTrade.Enabled = false;

                gridControl_trade_doc.DataSource = null;
                return;
            }

            fillDocsForTrade();//показать какие документы есть по торгам и подписаны
            simpleButton_tradeEdit.Enabled =
                simpleButton_tradeDelete.Enabled =
                simpleButton_AddDocTabTrade.Enabled = true;
        }

        private int? GetSelectedTradeID()
        {
            int result;
            if (gridView_trade.RowCount == 0)
                return null;
            if (gridView_trade.GetSelectedRows().Length == 0)
                return null;
            var vol = gridView_trade.GetRowCellValue(gridView_trade.GetSelectedRows()[0], "tradeID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   
        }

        private Trades GetSelectedTrade()
        {
            if (GetSelectedTradeID() == null || tradeList == null)
                return null;

            foreach (var trades in tradeList)
            {
                if (trades.TradeID == GetSelectedTradeID())
                    return trades;
            }
            return null;
        }

        private void gridView_trade_DoubleClick(object sender, EventArgs e)
        {
            return;//double click edit DISABLE!!!!
            if (groupBox_edit.Visible && simpleButton_tradeEdit.Enabled)
                EditTrade();
        }

        private void EditTrade()
        {
            trade = GetSelectedTrade();
            if (trade == null)
                return;
            FormTradeEdit form = new FormTradeEdit(trade, (curentUser.GroupID == (int)Enums.group.boss));
            switch (form.ShowDialog())
            {
                case DialogResult.OK :
                    {
                       if (!trade.CreateOrUpdate())
                            MessageBox.Show("Ошибка обновления!");
                        TradeTabFillData();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void CreateTrade()
        {
            trade = new Trades {UserIDrespons = curentUser.UserID, UserNameRespons = curentUser.Fio};
            FormTradeEdit form = new FormTradeEdit(trade, (curentUser.GroupID == (int)Enums.group.boss));
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!trade.CreateOrUpdate())
                            MessageBox.Show("Ошибка создания!");
                        TradeTabFillData();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private int GetIndexByTypeDocID()
        {
            if (GetSelectedtypedocID() == null || typeDocList == null)
                return -1;
            if (typeDocList.Count == 0)
                return -1;
            for(int index = 0; index < typeDocList.Count; index ++)
            {
                if (typeDocList[index].TypedocID == GetSelectedtypedocID())
                    return index;
            }
            return -1;
        }

        private int GetTabBySelectedTypeDocID()
        {
            if (GetSelectedtypedocID() == null || typeDocList == null)
                return 0;
            if (typeDocList.Count == 0)
                return 0;
            switch (GetIndexByTypeDocID())
            {
                case (int)Enums.typedoc.tenderDocs:
                    {
                        return (int) Enums.Tabs.tenderDocs1;
                    }

                case (int)Enums.typedoc.invite:
                    {
                        return (int)Enums.Tabs.invite2;
                    }

                case (int)Enums.typedoc.openprotocol:
                    {
                        return (int) Enums.Tabs.openprotocol3;
                        
                    }

                case (int)Enums.typedoc.ratingreport:
                    {
                        return (int) Enums.Tabs.ratingprotokol4;
                        
                    }

                case (int)Enums.typedoc.contract:
                    {
                        return (int) Enums.Tabs.contract5;
                    }

                case (int)Enums.typedoc.contractdoc:
                    {
                        return (int)Enums.Tabs.contract5;
                    }
                case (int)Enums.typedoc.contractworksdoc:
                    {
                        return (int)Enums.Tabs.contract5;
                    }

            }
            return 0;
        }


        private void GoToTabDocsByTradeId()//переход на таб по двойному нажатию с документами
        {
            if (GetSelectedtypedocID() == null)
                return;
            tabControl_forms.SelectedTabPageIndex = GetTabBySelectedTypeDocID();
               
        }

        private void gridView_trade_doc_DoubleClick(object sender, EventArgs e)
        {
            GoToTabDocsByTradeId();
        }

        private void simpleButton_trade_refresh_Click(object sender, EventArgs e)
        {
            TradeTabFillData();
        }

        private void simpleButton_AddDocTabTrade_Click(object sender, EventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.tenderDocs1;
            //если файл не выбран - вернутся на главный таб.
            if (!AddNewTenderDoc())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.trades0;
        }

        private void simpleButton_AddDocInviteTab_Click(object sender, EventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.invite2;
            //если файл не выбран - вернутся на главный таб.
            if (!AddNewInvite())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.trades0;
        }

        private void simpleButton_AddDocOpenProtokolTab_Click(object sender, EventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int) Enums.Tabs.openprotocol3;
            //если файл не выбран - вернутся на главный таб.
            if (!AddNewOpenProtokol())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.trades0;
        }

        private void simpleButton_AddRatingReportDoc_Click(object sender, EventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.ratingprotokol4;
            //если файл не выбран - вернутся на главный таб.
            if (!AddNewRatingReport())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.trades0;
        }

        private void simpleButton_AddDocContractTab_Click(object sender, EventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.contract5;
            //если файл не выбран - вернутся на главный таб.
            if (!AddNewContract())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.trades0;
        }


        private void gridView_trade_doc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FillGridSigned();
        }

        private void FillGridSigned()
        {
            
            //заполняем грид с подписями
            if (gridView_trade_doc.RowCount == 0) return;
            if (GetSelectedFiledocID() == null) return;

            Filedoc filedoc = new Filedoc();
            filedoc.FiledocID = (int)GetSelectedFiledocID();
            filedoc.GetListAllSignedByID(gridControl_doc_signed);
        }

        private int? GetSelectedFiledocID()
        {
            int result;
            if (gridView_trade_doc.RowCount == 0)
                return null;
            if (gridView_trade_doc.GetSelectedRows().Length == 0)
                return null;
            var vol = gridView_trade_doc.GetRowCellValue(gridView_trade_doc.GetSelectedRows()[0], "filedocID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   
        }

        //получить выбранный typedocID из грида 'документы' таба 'торги'
        private int? GetSelectedtypedocID()
        {
            int result;
            if (gridView_trade_doc.RowCount == 0)
                return null;
            var vol = gridView_trade_doc.GetRowCellValue(gridView_trade_doc.GetSelectedRows()[0], "typedocID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //  
        }

        private void gridView_doc_signed_DoubleClick(object sender, EventArgs e)
        {
            GoToTabDocsByTradeId();
        }

        private void simpleButton_export2Excel_Click(object sender, EventArgs e)
        {
            if (gridView_trade.RowCount != 0)
                utils.ExportGV2Xls("список торгов" + DateTime.Now.Day + "_" + DateTime.Now.Month, gridView_trade, true, "Список данных");
            else
                MessageBox.Show("нет данных");

        }

        #endregion tradeTab----------------------------------------------------------------------

        

        #region tenderDocsTab-------------------------------------------------------------------------

        private void FillTenderDocTab()
        {
            FillcomboBoxEditTradeList();
            if (selectedTradeId != null && selectedTradeId > 0) //выбраны торги
            {
                comboBoxEdit_tradeList.SelectedIndex = GetSelectedIndexForComboBoxEdit();
                FillGridControl_tenderDoc();
            }
            else
            {
                comboBoxEdit_tradeList.Properties.Items.Clear();
                comboBoxEditTradeList.Clear();
                comboBoxEdit_tradeList.EditValue = String.Empty;
                gridControl_tenderDoc.DataSource =
                    gridControl_tenderDocSigned.DataSource = null;
            }
        }
        
        private void FillcomboBoxEditTradeList()
        {
            
            comboBoxEdit_tradeList.Properties.Items.Clear();
            comboBoxEdit_tradeList2.Properties.Items.Clear();
            comboBoxEdit_tradeList3.Properties.Items.Clear();
            comboBoxEdit_tradeList4.Properties.Items.Clear();
            comboBoxEdit_tradeList5.Properties.Items.Clear();
            comboBoxEditTradeList.Clear();
            if (tradeList == null || tradeList.Count == 0) return;
            foreach (Trades _trade in tradeList)
            {
                comboBoxEdit_tradeList.Properties.Items.Add(_trade.Name);
                comboBoxEdit_tradeList2.Properties.Items.Add(_trade.Name);
                comboBoxEdit_tradeList3.Properties.Items.Add(_trade.Name);
                comboBoxEdit_tradeList4.Properties.Items.Add(_trade.Name);
                comboBoxEdit_tradeList5.Properties.Items.Add(_trade.Name);
                comboBoxEditTradeList.Add(new ComboBoxEditTrade
              {
                  NameTrade = _trade.Name,
                  TradeID = _trade.TradeID
              });
            }
        }

        private int GetSelectedIndexForComboBoxEdit()
        {
            if (comboBoxEdit_tradeList.Properties.Items.Count != tradeList.Count)
                return -1;
            int result = 0;
             foreach (Trades _trade in tradeList)
            {
                if (_trade.TradeID == selectedTradeId)
                    return result;
                result++;
            }
            return -1;
        }

        //получаем выбранный TradeID в зависимости от таба
        private int GetSelectedTradeIDfromComboBox()
        {

            switch (tabControl_forms.SelectedTabPageIndex)
            {
                
                case (int) Enums.Tabs.tenderDocs1:
                    {
                        if (comboBoxEdit_tradeList.SelectedIndex >= 0)
                            return comboBoxEditTradeList[comboBoxEdit_tradeList.SelectedIndex].TradeID;
          
                        break;
                    }

                case (int) Enums.Tabs.invite2:
                    {
                        if (comboBoxEdit_tradeList2.SelectedIndex >= 0)
                            return comboBoxEditTradeList[comboBoxEdit_tradeList2.SelectedIndex].TradeID;
                        break;
                    }

                case (int) Enums.Tabs.openprotocol3:
                    {
                        if (comboBoxEdit_tradeList3.SelectedIndex >= 0)
                            return comboBoxEditTradeList[comboBoxEdit_tradeList3.SelectedIndex].TradeID;
                        break;
                    }

                case (int) Enums.Tabs.ratingprotokol4:
                    {
                        if (comboBoxEdit_tradeList4.SelectedIndex >= 0)
                            return comboBoxEditTradeList[comboBoxEdit_tradeList4.SelectedIndex].TradeID;
                        break;
                    }

                case (int) Enums.Tabs.contract5:
                    {
                        if (comboBoxEdit_tradeList5.SelectedIndex >= 0)
                            return comboBoxEditTradeList[comboBoxEdit_tradeList5.SelectedIndex].TradeID;
                        break;
                    }
            }
            return -1;
        }
        
        private void comboBoxEdit_tradeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit_tradeList.Properties.Items.Count != tradeList.Count)
                return;

            FillGridControl_tenderDoc();
            //MessageBox.Show(GetSelectedTradeIDfromComboBox().ToString());
            
        }

        private void FillGridControl_tenderDoc()
        {
            var tenderdoc = new TenderDocs();
            tenderdoc.TypedocID = typeDocList[(int)Enums.typedoc.tenderDocs].TypedocID;
            tenderdoc.TradeID = GetSelectedTradeIDfromComboBox();
            tenderdoc.GetListAllTenderDocsByTradeID(gridControl_tenderDoc);
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_tenderDoc);
            if (selectedFileDocID != null)
                fillGridApproved(gridControl_tenderDocSigned, (int)selectedFileDocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned);
            if (gridView_tenderDoc.RowCount == 0)
                 BlockButton_Tender(true);
        }

        private void simpleButton_AddDocTabTenderDoc_Click(object sender, EventArgs e)
        {
            AddNewTenderDoc();
        }

        private bool AddNewTenderDoc()
        {
            if (typeDocList.Count == 0)
                return false;
            TenderDocs tenderdoc = new TenderDocs();
            tenderdoc.IsActive = false;
            tenderdoc.TypedocID = typeDocList[(int) Enums.typedoc.tenderDocs].TypedocID;
            tenderdoc.Sendtobank = false;
            tenderdoc.DateCreate = DateTime.Now;
            tenderdoc.TradeID = GetSelectedTradeIDfromComboBox();
            if (tenderdoc.TradeID == -1)
                return false;
            tenderdoc.CreateTenderDocGetID();

            Files files = UploadFileDoc("Выберите документ с конкурсной документацией");
            if (files == null) return false;

            tenderdoc.IsNew = true;
            CreateTenderDoc(tenderdoc, files);


            //SaveBytesToFile(String.Format(@"{0}\{1}", Path.GetTempPath(), files.Filename), files.Body);

            return true;

        }

        private void CreateTenderDoc(TenderDocs tenderdoc, Files files)
        {
            FormTenderDocAdd form = new FormTenderDocAdd(tenderdoc, files, comboBoxEditTradeList);
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!tenderdoc.Update() || !files.Update())
                            MessageBox.Show("Ошибка создания 5423 - tenderdoc.Update() || files.Update()");
                        else
                        {
                            Filedoc filedoc = new Filedoc();
                            //tenderdoc files
                            filedoc.DocID = tenderdoc.TenderDocID;
                            filedoc.FileID = files.FileID;
                            filedoc.IsActive = true;
                            filedoc.TradeID = tenderdoc.TradeID;
                            filedoc.TypedocID = tenderdoc.TypedocID;
                            filedoc.Create(); 
                        }
                        FillGridControl_tenderDoc();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        
        //открытие резервной копии из шары в зависимости от активного таба и чекбокса на нем
        private bool GetCheckBox_ShareOrBasePathToLookFileByTab()
        {
            switch (tabControl_forms.SelectedTabPageIndex)
            {

                case (int)Enums.Tabs.tenderDocs1:
                    {
                        return checkEdit_fileshare.Checked;
                    }

                case (int)Enums.Tabs.invite2:
                    {
                        return checkEdit_fileshare2.Checked;
                    }

                case (int)Enums.Tabs.openprotocol3:
                    {
                        return checkEdit_fileshare3.Checked;
                    }

                case (int)Enums.Tabs.ratingprotokol4:
                    {
                        return checkEdit_fileshare4.Checked;
                    }

                case (int)Enums.Tabs.contract5:
                    {
                        return checkEdit_fileshare5.Checked;
                    }
            }
            return false;
        }


        //название типа документа на основании активного таба
        private String GetTypeDocByTab()
        {
            switch (tabControl_forms.SelectedTabPageIndex)
            {

                case (int)Enums.Tabs.tenderDocs1:
                    {
                        if (typeDocList.Count > 0)
                            return typeDocList[(int)Enums.typedoc.tenderDocs].Name;
                        break;
                    }

                case (int)Enums.Tabs.invite2:
                    {
                        if (typeDocList.Count > 0)
                            return typeDocList[(int)Enums.typedoc.invite].Name;
                        break;
                    }

                case (int)Enums.Tabs.openprotocol3:
                    {
                        if (typeDocList.Count > 0)
                            return typeDocList[(int)Enums.typedoc.openprotocol].Name;
                        break;
                    }

                case (int)Enums.Tabs.ratingprotokol4:
                    {
                        if (typeDocList.Count > 0)
                            return typeDocList[(int)Enums.typedoc.ratingreport].Name;
                        break;
                    }

                case (int)Enums.Tabs.contract5:
                    {
                        if (typeDocList.Count > 0)
                            return typeDocList[(int)Enums.typedoc.contract].Name;
                        break;
                    }
            }
            return String.Empty;
        }

        private Files UploadFileDoc(string title_dialog)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
                                                {
                                                    Filter = "Word doc|*.doc|Word docx|*.docx|Все файлы (*.*)|*.*",
                                                    Title = title_dialog,
                                                    FilterIndex = 3,
                                                    RestoreDirectory = true
                                                };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Files file = new Files();
                    file.Datecreate = file.Datelastupdate = DateTime.Now;

                    FileInfo fi = new FileInfo(openFileDialog.FileName);

                    if (Path.GetFileName(openFileDialog.FileName).Length < 90)//сокращение имени файла - ограничение 100 символов
                        file.Filename = Path.GetFileName(openFileDialog.FileName);
                    else
                        file.Filename = Path.GetFileName(openFileDialog.FileName).Remove(80) + fi.Extension;

                    file.Title = GetTypeDocByTab();


                    if (fi.Length > MaxSizeFileInBase)
                    {
                        file.Body = new byte[0];//check this correct !!!!
                        file.IsFileInBaseOrInShare = false;
                    }
                    else
                    {
                        file.Body = utils.ReadFromFileToByte(openFileDialog.FileName);//-----------------
                        file.IsFileInBaseOrInShare = true;   
                    }

                    file.IsNew = true;
                    if (!file.SaveOrUpdate())//1 сохраняем файл в БД
                    {
                        MessageBox.Show("Ошибка file.SaveOrUpdate()");
                        return null;
                    }

                    //string targetPath = Properties.Settings.Default.SavePathShare;
                    //2 Сохраняем копию файла на удаленную шару!!!!
                    if (!utils.FilesCopyFromTo(openFileDialog.FileName, Properties.Settings.Default.SavePathShare, file.FileID.ToString(), false))
                        {
                            MessageBox.Show(String.Format("Ошибка сохранения файла из {0} в {1}", openFileDialog.FileName,
                                            Properties.Settings.Default.SavePathShare));
                            return null;
                        }
                    
                    return file;
                }
            openFileDialog.Dispose();
            return null;
        }

        private Files GetFileDocBody(string title_dialog)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Word doc|*.doc|Word docx|*.docx|Все файлы (*.*)|*.*",
                Title = title_dialog,
                FilterIndex = 3,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Files file = new Files();
                file.Datecreate = file.Datelastupdate = DateTime.Now;
                file.Filename = openFileDialog.FileName.Length < 100 ? Path.GetFileName(openFileDialog.FileName) : Path.GetFileName(openFileDialog.FileName).Remove(98);
                

                FileInfo fi = new FileInfo(openFileDialog.FileName);
                file.Body = utils.ReadFromFileToByte(openFileDialog.FileName);
                file.IsFileInBaseOrInShare = fi.Length <= MaxSizeFileInBase;
                return file;
            }
            openFileDialog.Dispose();
            return null;
        }

        //http://kbyte.ru/ru/Programming/Sources.aspx?id=917&mode=show -сжатие файла


        #region GetFrom gridView_tenderDoc *******************************************************************

        /// <summary>
        /// Получить FilID из выбранного документа грида gridView_tenderDoc
        /// </summary>
        /// <returns></returns>
        private static int? GetSelected_FileID_fromGridView(ColumnView gv)
        {
            if (gv.RowCount == 0)
                return null;
            int result;
            if (gv.GetSelectedRows().Length == 0)
                return null;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "fileID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }

        /// <summary>
        /// Получить tenderDocID из выбранного документа грида gridView_tenderDoc
        /// </summary>
        /// <returns></returns>
        private int? GetSelected_TenderDocID_fromGridTenderDoc()
        {
            if (gridView_tenderDoc.RowCount == 0)
                return null;
            if (gridView_tenderDoc.GetSelectedRows().Length == 0)
                return null;
            int result;
            var vol = gridView_tenderDoc.GetRowCellValue(gridView_tenderDoc.GetSelectedRows()[0], "tenderDocID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }

        /// <summary>
        /// Получить filedocID из выбранного документа грида gridView_tenderDoc
        /// </summary>
        /// <returns></returns>
        private static int? GetSelected_FiledocID_fromGridView(ColumnView gv)
        {
            if (gv.RowCount == 0)
                return null;
            int result;
            if (gv.GetSelectedRows().Length == 0)
                return null;

            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "filedocID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }

        /// <summary>
        /// Получить filename из выбранного документа грида gridView_tenderDoc
        /// </summary>
        /// <returns></returns>
        private static String GetSelected_Filename_fromGridView(ColumnView gv)
        {
            if (gv.RowCount == 0)
                return String.Empty;
            if (gv.GetSelectedRows().Length == 0)
                return null;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "filename");
            return vol == null ? String.Empty : vol.ToString();
        }

        /// <summary>
        /// Получить isFileInBaseOrInShare из выбранного документа грида gridView_tenderDoc
        /// </summary>
        /// <returns></returns>
        private static bool GetSelectedis_FileInBaseOrInShare_fromGridView(ColumnView gv)
        {
            if (gv.RowCount == 0)
                return false;
            if (gv.GetSelectedRows().Length == 0)
                return false;
            bool result;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "isFileInBaseOrInShare");
            if (vol == null)
                return false;
            Boolean.TryParse(vol.ToString(), out result);
            return result; 
        }

        private bool GetSelected_SendToBank_fromGridVew(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            if (gv.RowCount == 0)
                return false;
            if (gv.GetSelectedRows().Length == 0)
                return false;
            bool result;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "sendtobank");
            if (vol == null)
                return false;
            Boolean.TryParse(vol.ToString(), out result);
            return result;
        }

        private static DateTime GetSelected_DateCreate_fromGridView(ColumnView gv)
        {
            if (gv.RowCount == 0)
                return DateTime.Now;
            if (gv.GetSelectedRows().Length == 0)
                return DateTime.Now;
            DateTime result;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "dateCreate");
            if (vol == null)
                return DateTime.Now;
            DateTime.TryParse(vol.ToString(), out result);
            return result;
        }

        private static String GetSelected_FileTitle_fromGridView(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            if (gv.RowCount == 0)
                return String.Empty;
            if (gv.GetSelectedRows().Length == 0)
                return String.Empty;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "title");
            return vol == null ? String.Empty : vol.ToString();
        }

        private bool GetSelected_Signed_fromGridView(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            if (gv.RowCount == 0)
                return false;
            if (gv.GetSelectedRows().Length == 0)
                return false;
            bool result;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "signed");
            if (vol == null)
                return false;
            Boolean.TryParse(vol.ToString(), out result);
            return result;
        }

        #endregion GetFrom gridView_tenderDoc ------------------------------------------------------------

        private void simpleButton_editTenderDoc_Click(object sender, EventArgs e)
        {
            EditTenderDoc();
        }

        //
        private void gridView_tenderDoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_tenderDoc);
            if (selectedFileDocID == null)
            {
                BlockButton_Tender(true);
                gridControl_tenderDocSigned.DataSource = null;
                return;
            }

            BlockButton_Tender(false);
            fillGridApproved(gridControl_tenderDocSigned, (int)selectedFileDocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned);//показать какие документы одобрены
        }

        private void BlockButton_Tender(bool block)
        {
            if (block)
            {
                simpleButton_lookTenderDoc.Enabled =
                     simpleButton_editTenderDoc.Enabled =
                     simpleButton_deleteTenderDoc.Enabled =
                     simpleButton_approveTenderDoc.Enabled =
                     simpleButton_InviteAddDoc.Enabled =
                     simpleButton_EditTenderDocSign.Enabled =
                     simpleButton_DeleteTenderDocSign.Enabled = false;
            }
            else
            {
                simpleButton_lookTenderDoc.Enabled =
                     simpleButton_editTenderDoc.Enabled =
                     simpleButton_deleteTenderDoc.Enabled =
                     simpleButton_approveTenderDoc.Enabled =
                     simpleButton_InviteAddDoc.Enabled = true;
            if (isApproveExist(gridView_tenderDocSigned))
                simpleButton_approveTenderDoc.Enabled = false;
            }
        }

        private void gridView_tenderDoc_DoubleClick(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_tenderDoc) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_tenderDoc), gridView_tenderDoc);
        }

        private void EditTenderDoc()
        {
         
            if (GetSelected_TenderDocID_fromGridTenderDoc() == null)
                return;
            if (gridView_tenderDoc.GetSelectedRows().Length == 0)
                return;
            int selectedIndex_gridView_tenderDoc = gridView_tenderDoc.GetSelectedRows()[0];
            TenderDocs tenderdoc = new TenderDocs();

            tenderdoc.TenderDocID = (int)GetSelected_TenderDocID_fromGridTenderDoc();
            tenderdoc.IsActive = true;
            tenderdoc.TypedocID = typeDocList[(int) Enums.typedoc.tenderDocs].TypedocID;
            tenderdoc.Sendtobank = GetSelected_SendToBank_fromGridVew(gridView_tenderDoc);
            tenderdoc.DateCreate = GetSelected_DateCreate_fromGridView(gridView_tenderDoc);
            tenderdoc.TradeID = GetSelectedTradeIDfromComboBox();
            tenderdoc.IsNew = false;

            var id = tenderdoc.TenderDocID;
            Files files = new Files();
            switch (MessageBox.Show("Редактировать параметры конкурсной документации?", "Редактирование параметров конкурсной документаци.", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                    //замена файла
                //case DialogResult.Yes:
                //    {
                //        files = GetFileDocBody("Выберите новый документ с конкурсной документацией для замены старого");
                //            if (files == null) return;
                //        files.FileID = (int)GetSelected_FileID_fromGridView(gridView_tenderDoc);
                //        files.Title = GetSelected_FileTitle_fromGridView(gridView_tenderDoc);
                //        files.Signed = GetSelected_Signed_fromGridView(gridView_tenderDoc);
                //        files.IsNew = true;
                //        files.Datecreate = GetSelected_DateCreate_fromGridView(gridView_tenderDoc);
                //        files.Datelastupdate = DateTime.Now;
                //        break;
                //    }
                    //без замены файла
               case DialogResult.Yes:
                    {
                        files.FileID = (int)GetSelected_FileID_fromGridView(gridView_tenderDoc);
                        files.Title = GetSelected_FileTitle_fromGridView(gridView_tenderDoc);
                        files.Signed = GetSelected_Signed_fromGridView(gridView_tenderDoc);
                        files.IsNew = false;
                        files.IsFileInBaseOrInShare = GetSelectedis_FileInBaseOrInShare_fromGridView(gridView_tenderDoc);
                        if (files.IsFileInBaseOrInShare)
                        {
                            files.Body = files.GetFileBodyByFileID();
                        }
                        files.Datelastupdate =
                            files.Datecreate = GetSelected_DateCreate_fromGridView(gridView_tenderDoc);
                        files.Filename = GetSelected_Filename_fromGridView(gridView_tenderDoc);
                        break;
                    }

                case DialogResult.No:
                    {
                        return;
                    }
            }


            FormTenderDocAdd form = new FormTenderDocAdd(tenderdoc, files, comboBoxEditTradeList);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!tenderdoc.Update())
                                MessageBox.Show("Ошибка  редактирования 71 - tenderdoc.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка  редактирования 72 - files.Update() ");
                        //изменены торги для документа
                        if (tenderdoc.TradeID != GetSelectedTradeIDfromComboBox())
                        {
                            
                            Filedoc filedoc = new Filedoc
                                                  {
                                                      DocID = tenderdoc.TenderDocID,
                                                      FileID = files.FileID,
                                                      TradeID = tenderdoc.TradeID,
                                                  };
                            filedoc.Update(); 
                        }
                        
                        //перечитываем грид
                        FillGridControl_tenderDoc();
                        //возвращаем фокус на редактируемую строку
                        gridView_tenderDoc.SelectRow(selectedIndex_gridView_tenderDoc);  
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
            //MessageBox.Show(GetSelected_FileID_fromGridView().ToString());
        }
        
        /// <summary>
        /// Нажатие на просмотр конкурсной документации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton_lookTenderDoc_Click(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_tenderDoc) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_tenderDoc), gridView_tenderDoc);
        }

        private void OpenFileByFileID(int _fileID, DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            //нет имени файла
            if (GetSelected_Filename_fromGridView(gv).Equals(String.Empty))
                return;
            //файл хранится в базе и не нажат чекбокс резервной копии
            if (GetSelectedis_FileInBaseOrInShare_fromGridView(gv) && !GetCheckBox_ShareOrBasePathToLookFileByTab())
            {
                Files file = new Files();
                file.FileID = _fileID;
                byte[] bodyfile = file.GetFileBodyByFileID();
                if (bodyfile != null && bodyfile.Length != 0)
                {
                    if (utils.SaveBytesToFile(String.Format(@"{0}\{1}", Path.GetTempPath(), GetSelected_Filename_fromGridView(gv)), bodyfile))
                        //открываем файл в ворде
                        utils.OpenFileInWordOrExcel(Path.GetTempPath(), GetSelected_Filename_fromGridView(gv));
                    else
                        MessageBox.Show("файл недоступен из базы");
                }
                
            }
            else //файл хранится только на шаре или нажат чекбокс резервной копии
            {
                if (utils.FilesCopyFromTo(
                    Path.Combine(Properties.Settings.Default.SavePathShare,
                                 GetSelected_FileID_fromGridView(gv).ToString()), Path.GetTempPath(),
                    GetSelected_Filename_fromGridView(gv), false))
                    //открываем файл в ворде
                    utils.OpenFileInWordOrExcel(Path.GetTempPath(), GetSelected_Filename_fromGridView(gv));
                else
                    MessageBox.Show("файл недоступен из share");
                
            }
        }

        private void simpleButton_deleteTenderDoc_Click(object sender, EventArgs e)
        {
            if (gridView_tenderDoc.RowCount == 0 || GetSelected_TenderDocID_fromGridTenderDoc() == null)
                return;

            DeleteTenderDoc();
        }

        private void DeleteTenderDoc()
        {
            switch (MessageBox.Show(String.Format("Удалить конкурсную документацию - '{0}'?", GetSelected_FileTitle_fromGridView(gridView_tenderDoc)), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        TenderDocs tender = new TenderDocs();
                        tender.TypedocID = typeDocList[(int)Enums.typedoc.tenderDocs].TypedocID;
                        int? tenderdocid = GetSelected_TenderDocID_fromGridTenderDoc();
                        if (tenderdocid != null)
                        {
                            tender.TenderDocID = (int)GetSelected_TenderDocID_fromGridTenderDoc();
                            if (!tender.Delete())
                                MessageBox.Show("Ошибка при удалении конкурсной документации");
                            else
                                FillGridControl_tenderDoc();

                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
                
        }

        //показать какие документы конкурсной документации одобрены
        private void fillGridApproved(DevExpress.XtraGrid.GridControl gcSigned, int filedocId, Control memo, Control button, ColumnView gvFileDoc, ColumnView gvSigned)
        {
            //gridControl_tenderDocSigned, (int)selectedFileDocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned

            memo.Text = String.Empty; //
            Approve approve = new Approve();
            approve.FiledocID = filedocId;
            approve.GetListAllApproveByFiledocID(gcSigned);
            //блокрировка повторного вынесения резолюции
            button.Enabled = !isApproveExist(gvSigned);//simpleButton_approveTenderDoc
            //заполнение текстбокса резолюции
            Approve ap = GetSelected_Approve_fromgridViewSigned(gvFileDoc, gvSigned);
            memo.Text = ap != null ? ap.Remark : String.Empty;
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

        private void simpleButton_approveTenderDoc_Click(object sender, EventArgs e)
        {
            CreateApproveTenderDoc();
            
        }

        private void CreateApproveTenderDoc()
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_tenderDoc);
            if (selectedFileDocID == null)
                return;
            Approve approve = new Approve
                                  {
                                      ApproveDate = DateTime.Now,
                                      ApproveUserID = curentUser.UserID,
                                      Approved = true,
                                      FiledocID = ((int) selectedFileDocID),
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
                            MessageBox.Show("Ошибка  сохранения 127 - tenderdoc.Update()");
                        fillGridApproved(gridControl_tenderDocSigned, (int)selectedFileDocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned);
                        memoEdit_remark.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 128 - tenderdoc.Update()");
                        fillGridApproved(gridControl_tenderDocSigned, (int)selectedFileDocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned);
                        memoEdit_remark.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_EditTenderDocSign_Click(object sender, EventArgs e)
        {
            EditeTenderApprove();
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


        private static Approve GetSelected_Approve_fromgridViewSigned(ColumnView gvFileDoc, ColumnView gvSigned)
        {
            if (GetSelectedApproveID(gvSigned) == null)
                return null;

            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gvFileDoc);
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

        private void EditeTenderApprove()
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_tenderDoc, gridView_tenderDocSigned);
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
                            MessageBox.Show("Ошибка  сохранения 129 - tenderdoc.Update()");
                        fillGridApproved(gridControl_tenderDocSigned, approve.FiledocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned);
                        memoEdit_remark.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 130 - tenderdoc.Update()");
                        fillGridApproved(gridControl_tenderDocSigned, approve.FiledocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned);
                        memoEdit_remark.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }
        private void DeleteApprove(ColumnView gvFileDoc, ColumnView gvSigned)
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gvFileDoc, gvSigned);
            if (approve == null)
                return;
            if (curentUser.UserID != approve.ApproveUserID)
                return;
            approve.IsActive = false;
            if (!approve.SaveOrUpdate())
                MessageBox.Show("Ошибка  удаления 131 - approve.SaveOrUpdate()");
        }

        private void gridView_tenderDocSigned_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_tenderDoc, gridView_tenderDocSigned);
            if (approve == null)
            {
                memoEdit_remark.Text = String.Empty;
                    simpleButton_EditTenderDocSign.Enabled = simpleButton_DeleteTenderDocSign.Enabled = false;
            }
            else
            {
                //разрешить редактировать/удалять резолюцию только тому, кто ее вносил
                if (approve.ApproveUserID != curentUser.UserID)
                    simpleButton_EditTenderDocSign.Enabled = simpleButton_DeleteTenderDocSign.Enabled = false;
                else
                {
                    simpleButton_EditTenderDocSign.Enabled = simpleButton_DeleteTenderDocSign.Enabled = true;
                }
                memoEdit_remark.Text = approve.Remark;
            }
            if (isApproveExist(gridView_tenderDocSigned))
                simpleButton_approveTenderDoc.Enabled = false;
        }

        private void gridView_tenderDocSigned_DoubleClick(object sender, EventArgs e)
        {
            return;
            if (GetSelected_FiledocID_fromGridView(gridView_tenderDoc) == null || !simpleButton_EditTenderDocSign.Enabled)
                return;
            EditeTenderApprove();
        }

        private void simpleButton_DeleteTenderDocSign_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Удалить резолюцию?", "Удаление резолюции", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_tenderDoc);
                        if (selectedFileDocID == null)
                            return;
                        DeleteApprove(gridView_tenderDoc, gridView_tenderDocSigned);
                        fillGridApproved(gridControl_tenderDocSigned, (int)selectedFileDocID, memoEdit_remark, simpleButton_approveTenderDoc, gridView_tenderDoc, gridView_tenderDocSigned);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_tender_refresh_Click(object sender, EventArgs e)
        {
            //selectedTradeId = ;
            FillGridControl_tenderDoc();
        }

        private void simpleButton_InviteAddDoc_Click(object sender, EventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.invite2;
            //если файл не выбран - вернутся на предыдущий таб таб.
            if (!AddNewInvite())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.tenderDocs1;
            
        }
        #endregion tenderDocsTab-------------------------------------------------------------------------


        #region InviteTab************************************************************************
        private void FillInviteTab()
        {
            FillcomboBoxEditTradeList();
            if (selectedTradeId != null && selectedTradeId > 0) //выбраны торги
            {
                comboBoxEdit_tradeList2.SelectedIndex = GetSelectedIndexForComboBoxEdit();
                FillGridControl_invite();
            }
            
            else
            {
                comboBoxEdit_tradeList2.Properties.Items.Clear();
                comboBoxEditTradeList.Clear();
                comboBoxEdit_tradeList2.EditValue = String.Empty;
                gridControl_invite.DataSource =
                    gridControl_inviteSigned.DataSource = null;
            }
        }
        private void simpleButton_AddInvite_Click(object sender, EventArgs e)
                {
                    AddNewInvite();
                }
        private bool AddNewInvite()
        {
            if (typeDocList.Count == 0)
                return false;
            Invites invites = new Invites();
            invites.IsActive = false;
            invites.TypedocID = typeDocList[(int)Enums.typedoc.invite].TypedocID;
            invites.TradeID = GetSelectedTradeIDfromComboBox();
            invites.DateCreate = 
                invites.InviteStatusDate = DateTime.Now;
            invites.InviteStatusID = InviteStatusList[(int) Enums.invitestatus.site].InviteStatusID;


            if (invites.TradeID == -1)
                return false;
            invites.CreateInviteGetID();

            Files files = UploadFileDoc("Выберите документ с приглашением к участию в торгах");
            if (files == null) return false;

            invites.IsNew = true;
            CreateInvite(invites, files);

            return true;

        }
        private void CreateInvite(Invites invites, Files files)
        {
            if (InviteStatusList.Count == 0)
                return;
            FormInviteDoccAdd form = new FormInviteDoccAdd(invites, files, comboBoxEditTradeList, InviteStatusList);
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!invites.Update() || !files.Update())
                            MessageBox.Show("Ошибка создания 5423 - tenderdoc.Update() || files.Update()");
                        else
                        {
                            Filedoc filedoc = new Filedoc();
                            //tenderdoc files
                            filedoc.DocID = invites.InviteID;
                            filedoc.FileID = files.FileID;
                            filedoc.IsActive = true;
                            filedoc.TradeID = invites.TradeID;
                            filedoc.TypedocID = invites.TypedocID;
                            filedoc.Create();
                        }
                        FillGridControl_invite();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }
        private void comboBoxEdit_tradeList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit_tradeList2.Properties.Items.Count != tradeList.Count)
                return;
            FillGridControl_invite();
        }
        private void FillGridControl_invite()
        {
            var invite = new Invites();
            invite.TypedocID = typeDocList[(int)Enums.typedoc.invite].TypedocID;
            invite.TradeID = GetSelectedTradeIDfromComboBox();
            invite.GetListAllInviteByTradeID(gridControl_invite);
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_invite);
            if (selectedFileDocID != null)
                fillGridApproved(gridControl_inviteSigned, (int)selectedFileDocID, memoEdit_remark2, simpleButton_approveInvite, gridView_invite, gridView_inviteSigned);

            if (gridView_invite.RowCount == 0)
                BlockButton_Invite(true);
            //gridControl_inviteSigned, (int)selectedFileDocID, memoEdit_remark2, simpleButton_approveInvite, gridView_invite, gridView_inviteSigned
        }
        private void simpleButton_EditInvite_Click(object sender, EventArgs e)
        {
            EditInvite();
        }
        private int? GetSelected_inviteID_fromGridView()
        {
            if (gridView_invite.RowCount == 0)
                return null;
            if (gridView_invite.GetSelectedRows().Length == 0)
                return null;
            int result;
            var vol = gridView_invite.GetRowCellValue(gridView_invite.GetSelectedRows()[0], "inviteID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }
        private static int? GetSelected_inviteStatusID_fromGridView(ColumnView gv)
        {
            if (gv.RowCount == 0)
                return null;
            int result;
            if (gv.GetSelectedRows().Length == 0)
                return null;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "inviteStatusID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }
        private static DateTime GetSelected_inviteStatusDate_fromGridView(ColumnView gv)
        {
            if (gv.RowCount == 0)
                return DateTime.Now;
            if (gv.GetSelectedRows().Length == 0)
                return DateTime.Now;
            DateTime result;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "inviteStatusDate");
            if (vol == null)
                return DateTime.Now;
            DateTime.TryParse(vol.ToString(), out result);
            return result;
        }
        private void EditInvite()
        { //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (GetSelected_inviteID_fromGridView() == null)
                return;
            int selectedIndex_gridView_invite = gridView_invite.GetSelectedRows()[0];
            Invites invites = new Invites();

            if (GetSelected_inviteID_fromGridView() != null)
                invites.InviteID = (int)GetSelected_inviteID_fromGridView();
            invites.IsActive = true;
            invites.TypedocID = typeDocList[(int)Enums.typedoc.invite].TypedocID;
            invites.Sendtobank = GetSelected_SendToBank_fromGridVew(gridView_invite);
            invites.DateCreate = GetSelected_DateCreate_fromGridView(gridView_invite);
            invites.TradeID = GetSelectedTradeIDfromComboBox();
            invites.IsNew = false;
            if (GetSelected_inviteStatusID_fromGridView(gridView_invite)!= null)
                invites.InviteStatusID = (int)GetSelected_inviteStatusID_fromGridView(gridView_invite);
            invites.InviteStatusDate = GetSelected_inviteStatusDate_fromGridView(gridView_invite);

            //var id = invites.InviteID;
            Files files = new Files();
            switch (MessageBox.Show("Редактировать приглашение к участию в торгах?", "Редактирование приглашения к участию в торгах?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                ////замена файла
                //case DialogResult.Yes:
                //    {
                //        files = GetFileDocBody("Выберите новый документ с приглашением к участию в торгах для замены старого");
                //        if (files == null) return;
                //        files.FileID = (int)GetSelected_FileID_fromGridView(gridView_invite); //GetSelected_FileID_fromGridInvite();
                //        files.Title = GetSelected_FileTitle_fromGridView(gridView_invite);
                //        files.Signed = GetSelected_Signed_fromGridView(gridView_invite);
                //        files.IsNew = true;
                //        files.Datecreate = GetSelected_DateCreate_fromGridView(gridView_invite);
                //        files.Datelastupdate = DateTime.Now;
                //        break;
                //    }
                ////без замены файла
                case DialogResult.Yes:
                    {
                        files.FileID = (int)GetSelected_FileID_fromGridView(gridView_invite);
                        files.Title = GetSelected_FileTitle_fromGridView(gridView_invite);
                        files.Signed = GetSelected_Signed_fromGridView(gridView_invite);
                        files.IsNew = false;
                        files.IsFileInBaseOrInShare = GetSelectedis_FileInBaseOrInShare_fromGridView(gridView_invite);
                        if (files.IsFileInBaseOrInShare)
                        {
                            files.Body = files.GetFileBodyByFileID();
                        }
                        files.Datelastupdate =
                            files.Datecreate = GetSelected_DateCreate_fromGridView(gridView_invite);
                        files.Filename = GetSelected_Filename_fromGridView(gridView_invite);
                        break;
                    }

                case DialogResult.No:
                    {
                        return;
                    }
            }


            FormInviteDoccAdd form = new FormInviteDoccAdd(invites, files, comboBoxEditTradeList, InviteStatusList);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!invites.Update())
                            MessageBox.Show("Ошибка  редактирования 73 - invites.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка  редактирования 74 - files.Update() ");
                        //изменены торги для документа
                        if (invites.TradeID != GetSelectedTradeIDfromComboBox())
                        {

                            Filedoc filedoc = new Filedoc
                                                  {
                                DocID = invites.InviteID,
                                FileID = files.FileID,
                                TradeID = invites.TradeID,
                            };
                            filedoc.Update();
                        }

                        //перечитываем грид
                        FillGridControl_invite();
                        //возвращаем фокус на редактируемую строку
                        gridView_invite.SelectRow(selectedIndex_gridView_invite);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            } 
        }
        private void gridView_invite_DoubleClick(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_invite) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_invite), gridView_invite);
        }
        private void simpleButton_deleteInvite_Click(object sender, EventArgs e)
        {

            if (gridView_invite.RowCount == 0 || GetSelected_inviteID_fromGridView() == null)
                return;

            DeleteInvite();
        }
        private void DeleteInvite()
        {
            switch (MessageBox.Show(String.Format("Удалить приглашение к участию в торгах - '{0}'?", GetSelected_FileTitle_fromGridView(gridView_invite)), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        Invites invites = new Invites();
                        invites.TypedocID = typeDocList[(int)Enums.typedoc.invite].TypedocID;
                        int? invitesid = GetSelected_inviteID_fromGridView();
                        if (invitesid != null)
                        {
                            invites.InviteID = (int)GetSelected_inviteID_fromGridView();
                            if (!invites.Delete())
                                MessageBox.Show("Ошибка при удалении приглашения к участию в торгах");
                            else
                                FillGridControl_invite();

                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }

        }
        private void simpleButton_lookInvitedoc_Click(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_invite) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_invite), gridView_invite);
        }
        private void simpleButton_approveInvite_Click(object sender, EventArgs e)
        {
            CreateApproveInvite();
        }
        private void CreateApproveInvite()
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_invite);
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
                            MessageBox.Show("Ошибка  сохранения 128 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_inviteSigned, (int)selectedFileDocID, memoEdit_remark2, simpleButton_approveInvite, gridView_invite, gridView_inviteSigned);
                        memoEdit_remark2.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 129 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_inviteSigned, (int)selectedFileDocID, memoEdit_remark2, simpleButton_approveInvite, gridView_invite, gridView_inviteSigned);
                        memoEdit_remark2.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }
        private void EditeInviteApprove()
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_invite, gridView_inviteSigned);
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
                            MessageBox.Show("Ошибка  сохранения 129 - tenderdoc.Update()");
                        fillGridApproved(gridControl_inviteSigned, approve.FiledocID, memoEdit_remark2, simpleButton_approveInvite, gridView_invite, gridView_inviteSigned);
                        memoEdit_remark2.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 130 - tenderdoc.Update()");
                        fillGridApproved(gridControl_inviteSigned, approve.FiledocID, memoEdit_remark2, simpleButton_approveInvite, gridView_invite, gridView_inviteSigned);
                        memoEdit_remark2.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }
        private void simpleButton_EditInviteSign_Click(object sender, EventArgs e)
        {
            EditeInviteApprove();
        }
        private void simpleButton_DeleteInviteSign_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Удалить резолюцию?", "Удаление резолюции", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_invite);
                        if (selectedFileDocID == null)
                            return;
                        DeleteApprove(gridView_invite, gridView_inviteSigned);
                        fillGridApproved(gridControl_inviteSigned, (int)selectedFileDocID, memoEdit_remark2, simpleButton_approveInvite, gridView_invite, gridView_inviteSigned);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }
        private void gridView_inviteSigned_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_invite, gridView_inviteSigned);
            if (approve == null)
            {
                memoEdit_remark2.Text = String.Empty;
                simpleButton_EditInviteSign.Enabled = simpleButton_DeleteInviteSign.Enabled = false;
            }
            else
            {
                //разрешить редактировать/удалять резолюцию только тому, кто ее вносил
                if (approve.ApproveUserID != curentUser.UserID)
                    simpleButton_EditInviteSign.Enabled = simpleButton_DeleteInviteSign.Enabled = false;
                else
                {
                    simpleButton_EditInviteSign.Enabled = simpleButton_DeleteInviteSign.Enabled = true;
                }
                memoEdit_remark2.Text = approve.Remark;
            }
            if (isApproveExist(gridView_inviteSigned))
                simpleButton_approveInvite.Enabled = false;
        }
        private void gridView_invite_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_invite);
            if (selectedFileDocID == null)
            {
                
                BlockButton_Invite(true);
                gridControl_inviteSigned.DataSource = null;
                return;
            }
            BlockButton_Invite(false);
            fillGridApproved(gridControl_inviteSigned, (int)selectedFileDocID, memoEdit_remark2, simpleButton_approveInvite, gridView_invite, gridView_inviteSigned);//показать какие документы одобрены
        }

        private void BlockButton_Invite(bool block)
        {
            if (block)
            {
                simpleButton_lookInvitedoc.Enabled =
                     simpleButton_EditInvite.Enabled =
                     simpleButton_deleteInvite.Enabled =
                     simpleButton_approveInvite.Enabled =
                     simpleButton_AddOpenProtocol.Enabled =
                     simpleButton_EditInviteSign.Enabled =
                     simpleButton_DeleteInviteSign.Enabled = false;
            }
            else
            {
                simpleButton_lookInvitedoc.Enabled =
                     simpleButton_EditInvite.Enabled =
                     simpleButton_deleteInvite.Enabled =
                     simpleButton_approveInvite.Enabled =
                     simpleButton_AddOpenProtocol.Enabled = true;
                if (isApproveExist(gridView_inviteSigned))
                    simpleButton_approveInvite.Enabled = false;    
            }
        }

        private void gridView_inviteSigned_DoubleClick(object sender, EventArgs e)
                {
                    return;        
                if (GetSelected_FiledocID_fromGridView(gridView_invite) == null || !simpleButton_EditInviteSign.Enabled)
                        return;
                    EditeInviteApprove();
                }
        private void simpleButton_refreshInvite_Click(object sender, EventArgs e)
        {
            FillGridControl_invite();
        }
        private void simpleButton_AddOpenProtocol_Click(object sender, EventArgs e)
        {

            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.openprotocol3;
            //если файл не выбран - вернутся на предыдущий таб таб.
            if (!AddNewOpenProtokol())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.invite2;
            
        }

        #endregion InviteTab-------------------------------------------------------------------------
        

        #region OpenProtolol============================================================================

        private void FillOpenProtololTab()
        {
            FillcomboBoxEditTradeList();
            if (selectedTradeId != null && selectedTradeId > 0) //выбраны торги
            {
                comboBoxEdit_tradeList3.SelectedIndex = GetSelectedIndexForComboBoxEdit();
                FillGridControl_openProtokol();
            }
            
            else
            {
                comboBoxEdit_tradeList3.Properties.Items.Clear();
                comboBoxEditTradeList.Clear();
                comboBoxEdit_tradeList3.EditValue = String.Empty;
                gridControl_openProtokol.DataSource =
                    gridControl_openSigned.DataSource = null;
            }
        }

        private void simpleButton_AddOpenProtokol_Click(object sender, EventArgs e)
        {
            AddNewOpenProtokol();
        }

        private bool AddNewOpenProtokol()
        {
            if (typeDocList.Count == 0)
                return false;
            OpenProtolol openProtolol = new OpenProtolol();
            openProtolol.IsActive = false;
            openProtolol.TypedocID = typeDocList[(int)Enums.typedoc.openprotocol].TypedocID;
            openProtolol.TradeID = GetSelectedTradeIDfromComboBox();
            openProtolol.DateCreate = DateTime.Now;

            if (openProtolol.TradeID == -1)
                return false;
            if (!openProtolol.CreateopenProtololGetID())
                return false;

            Files files = UploadFileDoc("Выберите документ с протоколом вскрытия конкурсных предложений");
            if (files == null) return false;

            openProtolol.IsNew = true;
            CreateOpenProtokol(openProtolol, files);
            return true;

        }

        private void CreateOpenProtokol(OpenProtolol openProtolol, Files files)
        {
            //if (InviteStatusList.Count == 0)
            //    return;
            FormProtokolDocAdd form = new FormProtokolDocAdd(openProtolol, files, comboBoxEditTradeList);
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!openProtolol.Update() || !files.Update())
                            MessageBox.Show("Ошибка создания 5423 - openProtolol.Update() || files.Update()");
                        else
                        {
                            Filedoc filedoc = new Filedoc();
                            //tenderdoc files
                            filedoc.DocID = openProtolol.OpenProtocolID;
                            filedoc.FileID = files.FileID;
                            filedoc.IsActive = true;
                            filedoc.TradeID = openProtolol.TradeID;
                            filedoc.TypedocID = openProtolol.TypedocID;
                            filedoc.Create();
                        }
                        FillGridControl_openProtokol();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }
       
        private void FillGridControl_openProtokol()
        {
            var openProtolol = new OpenProtolol();
            openProtolol.TypedocID = typeDocList[(int)Enums.typedoc.openprotocol].TypedocID;
            openProtolol.TradeID = GetSelectedTradeIDfromComboBox();
            openProtolol.GetListAllOpenProtokolByTradeID(gridControl_openProtokol);
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_openProtokol);
            if (selectedFileDocID != null)
                fillGridApproved(gridControl_openSigned, (int)selectedFileDocID, memoEdit_remark3, simpleButton_approveOpenProtokol, gridView_openProtokol, gridView_openSigned);

            if (gridView_openProtokol.RowCount == 0)
            {
                BlockButton_OpenProtokol(true);
            }
            //gridControl_openSigned, (int)selectedFileDocID, memoEdit_remark3, simpleButton_approveOpenProtokol, gridView_openProtokol, gridView_openSigned
        }

        private void comboBoxEdit_tradeList3_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (comboBoxEdit_tradeList3.Properties.Items.Count != tradeList.Count)
                return;
             FillGridControl_openProtokol();
        }

        private void simpleButton_editOpenProtokol_Click(object sender, EventArgs e)
        {
            EditOpenProtokol();
        }

        private int? GetSelected_openProtocolID_fromGridOpenProtokol()
        {
            if (gridView_openProtokol.RowCount == 0)
                return null;
            if (gridView_openProtokol.GetSelectedRows().Length == 0)
                return null;
            int result;
            var vol = gridView_openProtokol.GetRowCellValue(gridView_openProtokol.GetSelectedRows()[0], "openProtocolID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }

        private void EditOpenProtokol()
        { //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (GetSelected_openProtocolID_fromGridOpenProtokol() == null)
                return;
            int selectedIndex_gridView_openProtokol = gridView_openProtokol.GetSelectedRows()[0];
            OpenProtolol openProtolol = new OpenProtolol();

            if (GetSelected_openProtocolID_fromGridOpenProtokol() != null)
                openProtolol.OpenProtocolID = (int)GetSelected_openProtocolID_fromGridOpenProtokol();
            openProtolol.IsActive = true;
            openProtolol.TypedocID = typeDocList[(int)Enums.typedoc.openprotocol].TypedocID;
            openProtolol.Sendtobank = GetSelected_SendToBank_fromGridVew(gridView_openProtokol);
            openProtolol.DateCreate = GetSelected_DateCreate_fromGridView(gridView_openProtokol);
            openProtolol.TradeID = GetSelectedTradeIDfromComboBox();
            openProtolol.IsNew = false;

            //var id = invites.InviteID;
            Files files = new Files();
            switch (MessageBox.Show("Редактировать параметры протокола вскрытия конкурсных предложений?", "Редактирование параметров протокола вскрытия конкурсных предложений.", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //замена файла
                //case DialogResult.Yes:
                //    {
                //        files = GetFileDocBody("Выберите новый документ с протоколом вскрытия конкурсных предложений для замены старого");
                //        if (files == null) return;
                //        files.FileID = (int)GetSelected_FileID_fromGridView(gridView_openProtokol); //GetSelected_FileID_fromGridInvite();
                //        files.Title = GetSelected_FileTitle_fromGridView(gridView_openProtokol);
                //        files.Signed = GetSelected_Signed_fromGridView(gridView_openProtokol);
                //        files.IsNew = true;
                //        files.Datecreate = GetSelected_DateCreate_fromGridView(gridView_openProtokol);
                //        files.Datelastupdate = DateTime.Now;
                //        break;
                //    }
                //без замены файла
                case DialogResult.Yes:
                    {
                        files.FileID = (int)GetSelected_FileID_fromGridView(gridView_openProtokol);
                        files.Title = GetSelected_FileTitle_fromGridView(gridView_openProtokol);
                        files.Signed = GetSelected_Signed_fromGridView(gridView_openProtokol);
                        files.IsNew = false;
                        files.IsFileInBaseOrInShare = GetSelectedis_FileInBaseOrInShare_fromGridView(gridView_openProtokol);
                        if (files.IsFileInBaseOrInShare)
                        {
                            files.Body = files.GetFileBodyByFileID();
                        }
                        files.Datelastupdate =
                            files.Datecreate = GetSelected_DateCreate_fromGridView(gridView_openProtokol);
                        files.Filename = GetSelected_Filename_fromGridView(gridView_openProtokol);
                        break;
                    }

                case DialogResult.No:
                    {
                        return;
                    }
            }


            FormProtokolDocAdd form = new FormProtokolDocAdd(openProtolol, files, comboBoxEditTradeList);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!openProtolol.Update())
                            MessageBox.Show("Ошибка  редактирования 173 - openProtolol.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка  редактирования 174 - files.Update() ");
                        //изменены торги для документа
                        if (openProtolol.TradeID != GetSelectedTradeIDfromComboBox())
                        {

                            Filedoc filedoc = new Filedoc
                            {
                                DocID = openProtolol.OpenProtocolID,
                                FileID = files.FileID,
                                TradeID = openProtolol.TradeID,
                            };
                            filedoc.Update();
                        }

                        //перечитываем грид
                        FillGridControl_openProtokol();
                        //возвращаем фокус на редактируемую строку
                        gridView_openProtokol.SelectRow(selectedIndex_gridView_openProtokol);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void gridView_openProtokol_DoubleClick(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_openProtokol) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_openProtokol), gridView_openProtokol);
        }

        private void simpleButton_deleteOpenProtokol_Click(object sender, EventArgs e)
        {
            if (gridView_openProtokol.RowCount == 0 || GetSelected_openProtocolID_fromGridOpenProtokol() == null)
                        return;

            DeleteOpenProtokol();
        }

        private void DeleteOpenProtokol()
        {
            switch (MessageBox.Show(String.Format("Удалить протокол вскрытия конкурсных предложений - '{0}'?", GetSelected_FileTitle_fromGridView(gridView_openProtokol)), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        OpenProtolol openProtolol = new OpenProtolol();
                        openProtolol.TypedocID = typeDocList[(int)Enums.typedoc.openprotocol].TypedocID;
                        int? openProtololID = GetSelected_openProtocolID_fromGridOpenProtokol();
                        if (openProtololID != null)
                        {
                            openProtolol.OpenProtocolID = (int)GetSelected_openProtocolID_fromGridOpenProtokol();
                            if (!openProtolol.Delete())
                                MessageBox.Show("Ошибка при удалении протокола вскрытия конкурсных предложений");
                            else
                                FillGridControl_openProtokol();
                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_lookOpenProtokoldoc_Click(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_openProtokol) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_openProtokol), gridView_openProtokol);
        }

        private void simpleButton_approveOpenProtokol_Click(object sender, EventArgs e)
        {
            CreateApproveOpenProtokol();
        }


        private void CreateApproveOpenProtokol()
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_openProtokol);
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
                            MessageBox.Show("Ошибка  сохранения 2128 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_openSigned, (int)selectedFileDocID, memoEdit_remark3, simpleButton_approveOpenProtokol, gridView_openProtokol, gridView_openSigned);
                        memoEdit_remark3.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 2129 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_openSigned, (int)selectedFileDocID, memoEdit_remark3, simpleButton_approveOpenProtokol, gridView_openProtokol, gridView_openSigned);
                        memoEdit_remark3.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_EditOpenProtokolSigned_Click(object sender, EventArgs e)
        {
            EditeOpenProtokolApprove();
        }


        private void EditeOpenProtokolApprove()
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_openProtokol, gridView_openSigned);
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
                            MessageBox.Show("Ошибка  сохранения 3129 - EditeOpenProtokolApprove");
                        fillGridApproved(gridControl_openSigned, approve.FiledocID, memoEdit_remark3, simpleButton_approveOpenProtokol, gridView_openProtokol, gridView_openSigned);
                        memoEdit_remark3.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 3130 - EditeOpenProtokolApprove()");
                        fillGridApproved(gridControl_openSigned, approve.FiledocID, memoEdit_remark3, simpleButton_approveOpenProtokol, gridView_openProtokol, gridView_openSigned);
                        memoEdit_remark3.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void somplButton_DeleteOpenProtokolSign_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Удалить резолюцию?", "Удаление резолюции", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_openProtokol);
                        if (selectedFileDocID == null)
                            return;
                        DeleteApprove(gridView_openProtokol, gridView_openSigned);
                        fillGridApproved(gridControl_openSigned, (int)selectedFileDocID, memoEdit_remark3, simpleButton_approveOpenProtokol, gridView_openProtokol, gridView_openSigned);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void gridView_openSigned_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_openProtokol, gridView_openSigned);
            if (approve == null)
            {
                memoEdit_remark3.Text = String.Empty;
                simpleButton_EditOpenProtokolSigned.Enabled = somplButton_DeleteOpenProtokolSign.Enabled = false;
            }
            else
            {
                //разрешить редактировать/удалять резолюцию только тому, кто ее вносил
                if (approve.ApproveUserID != curentUser.UserID)
                    simpleButton_EditOpenProtokolSigned.Enabled = somplButton_DeleteOpenProtokolSign.Enabled = false;
                else
                {
                    simpleButton_EditOpenProtokolSigned.Enabled = somplButton_DeleteOpenProtokolSign.Enabled = true;
                }
                memoEdit_remark3.Text = approve.Remark;
            }
            if (isApproveExist(gridView_openSigned))
                simpleButton_approveOpenProtokol.Enabled = false;
        }

        private void gridView_openProtokol_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_openProtokol);
            if (selectedFileDocID == null)
            {
                BlockButton_OpenProtokol(true);
                gridControl_openSigned.DataSource = null;
                return;
            }
            BlockButton_OpenProtokol(false);
            fillGridApproved(gridControl_openSigned, (int)selectedFileDocID, memoEdit_remark3, simpleButton_approveOpenProtokol, gridView_openProtokol, gridView_openSigned);//показать какие документы одобрены
        }

        private void BlockButton_OpenProtokol(bool block)
        {
            if (block)
            {
                simpleButton_lookOpenProtokoldoc.Enabled =
                     simpleButton_editOpenProtokol.Enabled =
                     simpleButton_deleteOpenProtokol.Enabled =
                     simpleButton_approveOpenProtokol.Enabled =
                     simpleButton_addRatingReport.Enabled =
                     simpleButton_EditOpenProtokolSigned.Enabled =
                     somplButton_DeleteOpenProtokolSign.Enabled = false;
            }
            else
            {
                simpleButton_lookOpenProtokoldoc.Enabled =
                   simpleButton_editOpenProtokol.Enabled =
                   simpleButton_deleteOpenProtokol.Enabled =
                   simpleButton_addRatingReport.Enabled =
                   simpleButton_EditOpenProtokolSigned.Enabled =
                   somplButton_DeleteOpenProtokolSign.Enabled = true;
                if (isApproveExist(gridView_openSigned))
                    simpleButton_approveOpenProtokol.Enabled = false;
            }
        }

        private void gridView_openSigned_DoubleClick(object sender, EventArgs e)
        {
            return;
            if (GetSelected_FiledocID_fromGridView(gridView_openProtokol) == null || !simpleButton_EditOpenProtokolSigned.Enabled)
                return;
            EditOpenProtokol();
        }

        private void simpleButton_refreshOpenProtokol_Click(object sender, EventArgs e)
        {
            FillGridControl_openProtokol();
        }
        
        private void simpleButton_addRatingReport_Click(object sender, EventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.ratingprotokol4;
            //если файл не выбран - вернутся на предыдущий таб таб.
            if (!AddNewRatingReport())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.openprotocol3;
        }

        #endregion OpenProtolol-------------------------------------------------------------------------


        #region RatingReport=====================================================================================

        private void FillRatingReportTab()
        {
            FillcomboBoxEditTradeList();
            if (selectedTradeId != null && selectedTradeId > 0) //выбраны торги
            {
                comboBoxEdit_tradeList4.SelectedIndex = GetSelectedIndexForComboBoxEdit();
                FillGridControl_ratingReport();
            }
            
            else
            {
                comboBoxEdit_tradeList4.Properties.Items.Clear();
                comboBoxEditTradeList.Clear();
                comboBoxEdit_tradeList4.EditValue = String.Empty;
                gridControl_RatingReport.DataSource =
                    gridControl_RatingReportSigned.DataSource = null;
            }
        }

        private void simpleButton_AddRatingReports_Click(object sender, EventArgs e)
        {
            AddNewRatingReport();
        }

        private bool AddNewRatingReport()
                {
                    if (typeDocList.Count == 0)
                        return false;
                    RatingReport ratingReport = new RatingReport();
                    ratingReport.IsActive = false;
                    ratingReport.TypedocID = typeDocList[(int)Enums.typedoc.ratingreport].TypedocID;
                    ratingReport.TradeID = GetSelectedTradeIDfromComboBox();
                    ratingReport.DateCreate =
                        ratingReport.ApproveRatingDate =
                        ratingReport.ApproveBankDate =
                        ratingReport.ApproveTenderDate =
                        DateTime.Now;

                    if (ratingReport.TradeID == -1)
                        return false;
                    if (!ratingReport.CreateRatingReportGetID())
                        return false;

                    Files files = UploadFileDoc("Выберите документ с оценочным отчетом");
                    if (files == null) 
                        return false;

                    ratingReport.IsNew = true;
                    CreateRatingReport(ratingReport, files);
                    return true;

                }    
        
        private void CreateRatingReport(RatingReport ratingReport, Files files)
        {
            //if (InviteStatusList.Count == 0)
            //    return;
            FormRatingReportDocAdd form = new FormRatingReportDocAdd(ratingReport, files, comboBoxEditTradeList);
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!ratingReport.Update() || !files.Update())
                            MessageBox.Show("Ошибка создания 6423 - ratingReport.Update() || files.Update()");
                        else
                        {
                            Filedoc filedoc = new Filedoc();
                            //tenderdoc files
                            filedoc.DocID = ratingReport.RatingReportID;
                            filedoc.FileID = files.FileID;
                            filedoc.IsActive = true;
                            filedoc.TradeID = ratingReport.TradeID;
                            filedoc.TypedocID = ratingReport.TypedocID;
                            filedoc.Create();
                        }
                        FillGridControl_ratingReport();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void FillGridControl_ratingReport()
                {
                    var ratingReport = new RatingReport
                                           {
                                               TypedocID = typeDocList[(int) Enums.typedoc.ratingreport].TypedocID,
                                               TradeID = GetSelectedTradeIDfromComboBox()
                                           };
                    ratingReport.GetListAllRatingReportByTradeID(gridControl_RatingReport);
                    int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_RatingReport);
                    if (selectedFileDocID != null)
                        fillGridApproved(gridControl_RatingReportSigned, (int)selectedFileDocID, memoEdit4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned);

                    if (gridView_RatingReport.RowCount == 0)
                    {
                        BlockButton_RatingReport(true);
                    }
                    //gridControl_RatingReportSigned, (int)selectedFileDocID, memoEdit_remark4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned
                }

        private void BlockButton_RatingReport(bool block)
        {
            if (block)
            {
                simpleButton_lookRatingReportDoc.Enabled =
                     simpleButton_editRatingReport.Enabled =
                     simpleButton_DeleteRatingReport.Enabled =
                     simpleButton_ApproveRatingReport.Enabled =
                     simpleButton_ContractTab.Enabled =
                     simpleButton_editRatingReportSign.Enabled =
                     simpleButton_DeleteRatingReportSign.Enabled = false;
            }
            else
            {
                simpleButton_lookRatingReportDoc.Enabled =
                   simpleButton_editRatingReport.Enabled =
                   simpleButton_DeleteRatingReport.Enabled =
                   simpleButton_ContractTab.Enabled =
                   simpleButton_editRatingReportSign.Enabled =
                   simpleButton_DeleteRatingReportSign.Enabled = true;
                if (isApproveExist(gridView_RatingReportSigned))
                    simpleButton_ApproveRatingReport.Enabled = false;
            }
        }

        private void comboBoxEdit_tradeList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit_tradeList4.Properties.Items.Count != tradeList.Count)
                return;
            FillGridControl_ratingReport();
        }

        private void simpleButton_editRatingReport_Click(object sender, EventArgs e)
        {
            EditRatingReport();
        }

        private int? GetSelected_ratingReportID_fromGridRatingReport()
                {
                    if (gridView_RatingReport.RowCount == 0)
                        return null;
                    if (gridView_RatingReport.GetSelectedRows().Length == 0)
                        return null;
                    int result;
                    var vol = gridView_RatingReport.GetRowCellValue(gridView_RatingReport.GetSelectedRows()[0], "ratingReportID");
                    if (vol == null)
                        return null;
                    Int32.TryParse(vol.ToString(), out result);
                    return result; //   

                }

        private static void FillRatingReportFiellds_fromGridVew(ColumnView gv, RatingReport ratingReport)
        {
            if (gv.RowCount == 0)
                return;
            if (gv.GetSelectedRows().Length == 0)
                return;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "sendtobank");
            if (vol != null)
            {
                bool sendtobank;
                Boolean.TryParse(vol.ToString(), out sendtobank);
                ratingReport.Sendtobank = sendtobank;    
            }
            
            var vol2 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "dateCreate");
            if (vol2 != null)
            {
                DateTime dateCreate;
                DateTime.TryParse(vol2.ToString(), out dateCreate);
                ratingReport.DateCreate = dateCreate;
            }
            

            var vol3 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "approveRatingCommission");
            if (vol3 != null)
            {
                bool approveRatingCommission;
                Boolean.TryParse(vol3.ToString(), out approveRatingCommission);
                ratingReport.ApproveRatingCommission = approveRatingCommission;    
            }
            


            var vol4 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "approveTenderCommission");
            if (vol4 != null)
            {
                bool approveTenderCommission;
                Boolean.TryParse(vol4.ToString(), out approveTenderCommission);
                ratingReport.ApproveTenderCommission = approveTenderCommission;    
            }
            


            var vol5 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "approveBank");
            if (vol5 != null)
            {
                bool approveBank;
                Boolean.TryParse(vol5.ToString(), out approveBank);
                ratingReport.ApproveBank = approveBank;    
            }


            var vol6 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "approveRatingDate");
            if (vol6 != null)
            {
                DateTime approveRatingDate;
                DateTime.TryParse(vol6.ToString(), out approveRatingDate);
                ratingReport.ApproveRatingDate = approveRatingDate;
            }

            var vol7 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "approveTenderDate");
            if (vol7 != null)
            {
                DateTime approveTenderDate;
                DateTime.TryParse(vol7.ToString(), out approveTenderDate);
                ratingReport.ApproveTenderDate = approveTenderDate;
            }

            var vol8 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "approveBankDate");
            if (vol8 != null)
            {
                DateTime approveBankDate;
                DateTime.TryParse(vol8.ToString(), out approveBankDate);
                ratingReport.ApproveBankDate = approveBankDate;
            }
        }

        private static void FillFileFiellds_fromGridVew(ColumnView gv, Files files)
        {
            if (gv.RowCount == 0)
                return;
            if (gv.GetSelectedRows().Length == 0)
                return;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "isFileInBaseOrInShare");
            if (vol != null)
            {
                bool isFileInBaseOrInShare;
                Boolean.TryParse(vol.ToString(), out isFileInBaseOrInShare);
                files.IsFileInBaseOrInShare = isFileInBaseOrInShare;
            }

            var vol2 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "dateCreate");
            if (vol2 != null)
            {
                DateTime dateCreate;
                DateTime.TryParse(vol2.ToString(), out dateCreate);
                files.Datecreate = dateCreate;
            }

            var vol3 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "signed");
            if (vol3 != null)
            {
                bool signed;
                Boolean.TryParse(vol3.ToString(), out signed);
                files.Signed = signed;
            }

            var vol4 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "fileID");
            if (vol4 != null)
            {
                int fileID;
                Int32.TryParse(vol4.ToString(), out fileID);
                files.FileID = fileID;
            }

            var vol5 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "filename");
            if (vol5 != null)
            {
                files.Filename = vol5.ToString();
            }

            var vol6 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "title");
            if (vol6 != null)
            {
                files.Title = vol6.ToString();
            }

            var vol7 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "datelastupdate");
            if (vol7 != null)
            {
                DateTime datelastupdate;
                DateTime.TryParse(vol7.ToString(), out datelastupdate);
                files.Datelastupdate = datelastupdate;
            }
            
        }



        private void EditRatingReport()
        { //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (GetSelected_ratingReportID_fromGridRatingReport() == null)
                return;
            int selectedIndex_gridView_ratingReport = gridView_RatingReport.GetSelectedRows()[0];
            RatingReport ratingReport = new RatingReport();

            if (GetSelected_ratingReportID_fromGridRatingReport() != null)
                ratingReport.RatingReportID = (int)GetSelected_ratingReportID_fromGridRatingReport();
            ratingReport.IsActive = true;
            ratingReport.TypedocID = typeDocList[(int)Enums.typedoc.ratingreport].TypedocID;
            ratingReport.TradeID = GetSelectedTradeIDfromComboBox();

            FillRatingReportFiellds_fromGridVew(gridView_RatingReport, ratingReport);
            ratingReport.IsNew = false;

            //var id = invites.InviteID;
            Files files = new Files();
            switch (MessageBox.Show("Редактировать параметры оценочного отчета?", "Редактирование оценочного отчета.", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //замена файла
                //case DialogResult.Yes:
                //    {
                //        files = GetFileDocBody("Выберите новый документ с оценочным отчетом для замены старого");
                //        if (files == null) return;
                //        FillFileFiellds_fromGridVew(gridView_RatingReport, files);
                //        files.IsNew = true;
                //        files.Datelastupdate = DateTime.Now;
                //        break;
                //    }
                //без замены файла
                case DialogResult.Yes:
                    {
                        FillFileFiellds_fromGridVew(gridView_RatingReport, files);
                        files.IsNew = false;
                        if (files.IsFileInBaseOrInShare)
                        {
                            files.Body = files.GetFileBodyByFileID();
                        }
                        break;
                    }

                case DialogResult.No:
                    {
                        return;
                    }
            }


            FormRatingReportDocAdd form = new FormRatingReportDocAdd(ratingReport, files, comboBoxEditTradeList);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!ratingReport.Update())
                            MessageBox.Show("Ошибка  редактирования 173 - openProtolol.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка  редактирования 174 - files.Update() ");
                        //изменены торги для документа
                        if (ratingReport.TradeID != GetSelectedTradeIDfromComboBox())
                        {

                            Filedoc filedoc = new Filedoc
                            {
                                DocID = ratingReport.RatingReportID,
                                FileID = files.FileID,
                                TradeID = ratingReport.TradeID,
                            };
                            filedoc.Update();
                        }

                        //перечитываем грид
                        FillGridControl_ratingReport();
                        //возвращаем фокус на редактируемую строку
                        gridView_RatingReport.SelectRow(selectedIndex_gridView_ratingReport);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void gridView_RatingReport_DoubleClick(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_RatingReport) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_RatingReport), gridView_RatingReport);
       }

        private void simpleButton_DeleteRatingReport_Click(object sender, EventArgs e)
        {
            if (gridView_RatingReport.RowCount == 0 || GetSelected_ratingReportID_fromGridRatingReport() == null)
                return;

            DeleteRatingReport();
        }

        private void DeleteRatingReport()
        {
            switch (MessageBox.Show(String.Format("Удалить оценочный отчет - '{0}'?",  GetSelected_FileTitle_fromGridView(gridView_RatingReport)), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        RatingReport ratingReport = new RatingReport();
                        ratingReport.TypedocID = typeDocList[(int)Enums.typedoc.ratingreport].TypedocID;
                        int? ratingReportID = GetSelected_ratingReportID_fromGridRatingReport();
                        if (ratingReportID != null)
                        {
                            ratingReport.RatingReportID = (int)GetSelected_ratingReportID_fromGridRatingReport();
                            if (!ratingReport.Delete())
                                MessageBox.Show("Ошибка при удалении оценочного отчета");
                            else
                                FillGridControl_ratingReport();
                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_lookRatingReportDoc_Click(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_RatingReport) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_RatingReport), gridView_RatingReport);
        }

        private void simpleButton_ApproveRatingReport_Click(object sender, EventArgs e)
        {
            CreateApproveRatingReport();
        }

        private void CreateApproveRatingReport()
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_RatingReport);
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
                            MessageBox.Show("Ошибка  сохранения 62128 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_RatingReportSigned, (int)selectedFileDocID, memoEdit4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned);
                        //gridControl_RatingReportSigned, (int)selectedFileDocID, memoEdit4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned
                        memoEdit4.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 62129 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_RatingReportSigned, (int)selectedFileDocID, memoEdit4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned);
                        memoEdit4.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_editRatingReportSign_Click(object sender, EventArgs e)
        {
            EditeRatingReportApprove();
        }
        
        private void EditeRatingReportApprove()
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_RatingReport, gridView_RatingReportSigned);
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
                            MessageBox.Show("Ошибка  сохранения 13129 - EditeRatingReportApprove");
                        fillGridApproved(gridControl_RatingReportSigned, approve.FiledocID, memoEdit4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned);
                        memoEdit4.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 13130 - EditeRatingReportApprove()");
                        fillGridApproved(gridControl_RatingReportSigned, approve.FiledocID, memoEdit4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned);
                        memoEdit4.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_DeleteRatingReportSign_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Удалить резолюцию?", "Удаление резолюции", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_RatingReport);
                        if (selectedFileDocID == null)
                            return;
                        DeleteApprove(gridView_RatingReport, gridView_RatingReportSigned);
                        fillGridApproved(gridControl_RatingReportSigned, (int)selectedFileDocID, memoEdit4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void gridView_RatingReportSigned_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_RatingReport, gridView_RatingReportSigned);
            if (approve == null)
            {
                memoEdit4.Text = String.Empty;
                simpleButton_editRatingReportSign.Enabled = simpleButton_DeleteRatingReportSign.Enabled = false;
            }
            else
            {
                //разрешить редактировать/удалять резолюцию только тому, кто ее вносил
                if (approve.ApproveUserID != curentUser.UserID)
                    simpleButton_editRatingReportSign.Enabled = simpleButton_DeleteRatingReportSign.Enabled = false;
                else
                {
                    simpleButton_editRatingReportSign.Enabled = simpleButton_DeleteRatingReportSign.Enabled = true;
                }
                memoEdit4.Text = approve.Remark;
            }
            if (isApproveExist(gridView_RatingReportSigned))
                simpleButton_ApproveRatingReport.Enabled = false;
        }

        private void gridView_RatingReport_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_RatingReport);
            if (selectedFileDocID == null)
            {
                BlockButton_RatingReport(true);
                gridControl_RatingReportSigned.DataSource = null;
                return;
            }
            BlockButton_RatingReport(false);
            fillGridApproved(gridControl_RatingReportSigned, (int)selectedFileDocID, memoEdit4, simpleButton_ApproveRatingReport, gridView_RatingReport, gridView_RatingReportSigned);//показать какие документы одобрены
        }

        private void gridView_RatingReportSigned_DoubleClick(object sender, EventArgs e)
        {
            return;
            if (GetSelected_FiledocID_fromGridView(gridView_RatingReport) == null || !simpleButton_editRatingReportSign.Enabled)
                return;
            EditRatingReport();
        }

        private void simpleButton_refreshRatingReport_Click(object sender, EventArgs e)
        {
            FillGridControl_ratingReport();
        }

        private void simpleButton_ContractTab_Click(object sender, EventArgs e)
        {
            tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.contract5;
            //если файл не выбран - вернутся на предыдущий таб таб.
            if (!AddNewContract())
                tabControl_forms.SelectedTabPageIndex = (int)Enums.Tabs.ratingprotokol4;
        }


        #endregion RatingReport=====================================================================================

       
        #region Contract ===--------------------------------------------------------------------------------------------
 
        private void simpleButton_AddContract_Click(object sender, EventArgs e)
        {
            AddNewContract();
        }

        private void FillContractTab()
        {
            FillcomboBoxEditTradeList();
            if (selectedTradeId != null && selectedTradeId > 0) //выбраны торги
            {
                comboBoxEdit_tradeList5.SelectedIndex = GetSelectedIndexForComboBoxEdit();
                FillGridControl_Contract();
            }
            else
            {
                comboBoxEdit_tradeList5.Properties.Items.Clear();
                comboBoxEditTradeList.Clear();
                comboBoxEdit_tradeList5.EditValue = String.Empty;
                gridControl_contract.DataSource =
                    gridControl_contractSigned.DataSource = 
                    gridControl_contractDocs.DataSource =
                    gridControl_contracDocWorks.DataSource = null;
            }


        }

        private bool AddNewContract()
        {
            if (typeDocList.Count == 0)
                return false;
            Contract contract = new Contract();
            contract.IsActive = false;
            contract.TypedocID = typeDocList[(int)Enums.typedoc.contract].TypedocID;
            contract.TradeID = GetSelectedTradeIDfromComboBox();
            contract.DateCreate =
                contract.ContractData =
                contract.SignedData = DateTime.Now;
            contract.Amount = 0;
            contract.BankguaranteeRunContract =
                contract.BankguaranteeBackAvans = false;

            contract.CurrencyID = (int) Enums.currencyID.usd;
            contract.TypefundingID = (int) Enums.typefundingID.sredstva_vbrr;
            contract.ActstartID = (int) Enums.actstartID.no;
            contract.IsChandgedSumm = false; //сумма контракта не менялась

            if (contract.TradeID == -1)
                return false;
            if (!contract.CreateContractGetID())
                return false;

            Files files = UploadFileDoc("Выберите документ с контрактом");
            if (files == null)
                return false;

            contract.IsNew = true;
            CreateContract(contract, files);
            return true;

        }


        private void CreateContract(Contract contract, Files files)
        {
            //if (InviteStatusList.Count == 0)
            //    return;
            FormContractDocAdd form = new FormContractDocAdd(contract, files, comboBoxEditTradeList, false);
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!contract.Update())
                            MessageBox.Show("Ошибка создания 26423 - contract.Update()");
                        if (!files.Update())
                             MessageBox.Show("Ошибка создания 26423 - files.Update()");
                        else
                        {
                            Filedoc filedoc = new Filedoc
                                                  {
                                                      DocID = contract.ContractID,
                                                      FileID = files.FileID,
                                                      IsActive = true,
                                                      TradeID = contract.TradeID,
                                                      TypedocID = contract.TypedocID
                                                  };
                            filedoc.Create();
                        }
                        FillGridControl_Contract();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void FillGridControl_Contract()
        {
            var contract = new Contract
            {
                TypedocID = typeDocList[(int)Enums.typedoc.contract].TypedocID,
                TradeID = GetSelectedTradeIDfromComboBox()
            };
            contract.GetListAllContractByTradeID(gridControl_contract);
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_contract);
            if (selectedFileDocID != null)
                fillGridApproved(gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned);
            if (gridView_contract.RowCount == 0) 
            {
                BlockButton_Contract(true);
                gridControl_contractDocs.DataSource = gridControl_contracDocWorks.DataSource = null;
                groupBox39.Visible = groupBox46.Visible = false;
                CheckContrackDocsButtons();
            }
            else //if exist rows with contract then fill grids with docs Contractdocs and Contractdocworks
            {
                FillGridView_contractDocs_contractDocWorks();
                if (!checkEdit_all.Checked)
                {
                    groupBox39.Visible = groupBox46.Visible = true;
                }
            }
            //gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned
        }


        private void BlockButton_Contract(bool block)
        {
            if (block)
            {
                simpleButton_lookContractDoc.Enabled =
                    simpleButton_EditContract.Enabled =
                    simpleButton_DeleteContract.Enabled =
                    simpleButton_ApproveContract.Enabled =
                    simpleButton_editContractSign.Enabled =
                    simpleButton_DeleteContractSign.Enabled = false;
            }
            else
            {
                simpleButton_lookContractDoc.Enabled =
                    simpleButton_EditContract.Enabled =
                    simpleButton_DeleteContract.Enabled =
                    simpleButton_editContractSign.Enabled =
                    simpleButton_DeleteContractSign.Enabled = true;
                if (isApproveExist(gridView_contractSigned))
                    simpleButton_ApproveContract.Enabled = false;
            }
        }

        private void comboBoxEdit_tradeList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit_tradeList5.Properties.Items.Count != tradeList.Count)
                return;
            FillGridControl_Contract();
        }

        private void simpleButton_EditContract_Click(object sender, EventArgs e)
        {
            EditContract();
        }

        private int? GetSelected_contractID_fromGridContract()
        {
            if (gridView_contract.RowCount == 0)
                return null;
            if (gridView_contract.GetSelectedRows().Length == 0)
                return null;
            int result;
            var vol = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "contractID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }

        private static void FillContractFiellds_fromGridView(ColumnView gv, Contract contract)
        {
            if (gv.RowCount == 0)
                return;
            if (gv.GetSelectedRows().Length == 0)
                return;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "bankguaranteeRunContract");
            if (vol != null)
            {
                bool bankguaranteeRunContract;
                Boolean.TryParse(vol.ToString(), out bankguaranteeRunContract);
                contract.BankguaranteeRunContract = bankguaranteeRunContract;    
            }
            
            var vol2 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "dateCreate");
            if (vol2 != null)
            {
                DateTime dateCreate;
                DateTime.TryParse(vol2.ToString(), out dateCreate);
                contract.DateCreate = dateCreate;
            }


            var vol3 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "bankguaranteeBackAvans");
            if (vol3 != null)
            {
                bool bankguaranteeBackAvans;
                Boolean.TryParse(vol3.ToString(), out bankguaranteeBackAvans);
                contract.BankguaranteeBackAvans = bankguaranteeBackAvans;    
            }

            var vol4 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "signedData");
            if (vol4 != null)
            {
                DateTime signedData;
                DateTime.TryParse(vol4.ToString(), out signedData);
                contract.SignedData = signedData;
            }

            var vol44 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "contractData");
            if (vol44 != null)
            {
                DateTime contractData;
                DateTime.TryParse(vol44.ToString(), out contractData);
                contract.ContractData = contractData;
            }

            var vol5 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "contractID");
            if (vol5 != null)
            {
                int contractID;
                Int32.TryParse(vol5.ToString(), out contractID);
                contract.ContractID = contractID;    
            }

            var vol6 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "currencyID");
            if (vol6 != null)
            {
                int currencyID;
                Int32.TryParse(vol6.ToString(), out currencyID);
                contract.CurrencyID = currencyID;
            }
            var vol7 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "typefundingID");
            if (vol7 != null)
            {
                int typefundingID;
                Int32.TryParse(vol7.ToString(), out typefundingID);
                contract.TypefundingID = typefundingID;
            }
            var vol8 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "actstartID");
            if (vol8 != null)
            {
                int actstartID;
                Int32.TryParse(vol8.ToString(), out actstartID);
                contract.ActstartID = actstartID;
            }

            var vol9 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "amount");
            if (vol9 != null)
            {
                Int64 amount;
                Int64.TryParse(vol9.ToString(), out amount);
                contract.Amount = amount;
            }
        }

        private void LookContract()
        {
            if (GetSelected_contractID_fromGridContract() == null)
                return;
            int selectedIndex_gridView_contract = gridView_contract.GetSelectedRows()[0];
            Contract contract = new Contract();

            if (GetSelected_contractID_fromGridContract() != null)
                contract.ContractID = (int)GetSelected_contractID_fromGridContract();
            contract.IsActive = true;
            contract.TypedocID = typeDocList[(int)Enums.typedoc.contract].TypedocID;
            contract.TradeID = GetSelectedTradeIDfromComboBox();

            FillContractFiellds_fromGridView(gridView_contract, contract);
            contract.IsNew = false;

            //var id = invites.InviteID;
            Files files = new Files();
            FillFileFiellds_fromGridVew(gridView_contract, files);
            files.IsNew = false;
            if (files.IsFileInBaseOrInShare)
            {
                files.Body = files.GetFileBodyByFileID();
            }
            FormContractDocAdd form = new FormContractDocAdd(contract, files, comboBoxEditTradeList, true);
            form.ShowDialog();
            
        }
        
        private void EditContract()
        {
            if (GetSelected_contractID_fromGridContract() == null)
                return;
            int selectedIndex_gridView_contract = gridView_contract.GetSelectedRows()[0];
            Contract contract = new Contract();

            if (GetSelected_contractID_fromGridContract() != null)
                contract.ContractID = (int)GetSelected_contractID_fromGridContract();
            contract.IsActive = true;
            contract.TypedocID = typeDocList[(int)Enums.typedoc.contract].TypedocID;
            contract.TradeID = GetSelectedTradeIDfromComboBox();

            FillContractFiellds_fromGridView(gridView_contract, contract);
            contract.IsNew = false;

            //var id = invites.InviteID;
            Files files = new Files();
            switch (MessageBox.Show("Редактировать параметры контракта?", "Редактирование.", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                ////замена файла
                //case DialogResult.Yes:
                //    {
                //        files = GetFileDocBody("Выберите новый документ с контрактом для замены старого");
                //        if (files == null) return;
                //        FillFileFiellds_fromGridVew(gridView_contract, files);
                //        files.IsNew = true;
                //        files.Datelastupdate = DateTime.Now;
                //        break;
                //    }
                //без замены файла
                case DialogResult.Yes:
                    {
                        FillFileFiellds_fromGridVew(gridView_contract, files);
                        files.IsNew = false;
                        if (files.IsFileInBaseOrInShare)
                        {
                            files.Body = files.GetFileBodyByFileID();
                        }
                        break;
                    }

                case DialogResult.No:
                    {
                        return;
                    }
            }


            FormContractDocAdd form = new FormContractDocAdd(contract, files, comboBoxEditTradeList, false);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!contract.Update())
                            MessageBox.Show("Ошибка  редактирования 173 - contract.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка  редактирования 174 - files.Update() ");
                        //изменены торги для документа
                        if (contract.TradeID != GetSelectedTradeIDfromComboBox())
                        {

                            Filedoc filedoc = new Filedoc
                            {
                                DocID = contract.ContractID,
                                FileID = files.FileID,
                                TradeID = contract.TradeID,
                            };
                            filedoc.Update();
                        }

                        //перечитываем грид
                        FillGridControl_Contract();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void gridView_contract_DoubleClick(object sender, EventArgs e)
        {
            //if (GetSelected_FileID_fromGridView(gridView_contract) != null)
            //    OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_contract), gridView_contract);
            LookContract();
        }

        private void simpleButton_DeleteContract_Click(object sender, EventArgs e)
        {
            if (gridView_contract.RowCount == 0 || GetSelected_contractID_fromGridContract() == null)
                return;

            DeleteContract();
        }

        private void DeleteContract()
        {
            if (gridView_contractDocs.RowCount != 0)
            {
                MessageBox.Show("Перед удалением контракта нужно удалить документы, относящиеся к контракту!");
                return;
            }
            if (gridView_contractDocWorks.RowCount != 0)
            {
                MessageBox.Show("Перед удалением контракта нужно удалить документы по выполнению работ по контракту!");
                return;
            }

            switch (MessageBox.Show(String.Format("Удалить контракт - '{0}'?",  GetSelected_FileTitle_fromGridView(gridView_contract)), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        Contract contract = new Contract();
                        contract.TypedocID = typeDocList[(int)Enums.typedoc.contract].TypedocID;
                        int? contractID = GetSelected_contractID_fromGridContract();
                        if (contractID != null)
                        {
                            contract.ContractID = (int)GetSelected_contractID_fromGridContract();
                            if (!contract.Delete())
                                MessageBox.Show("Ошибка при удалении контракта");
                            else
                                FillGridControl_Contract();
                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_lookContractDoc_Click(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_contract) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_contract), gridView_contract);
    
        }

        private void simpleButton_ApproveContract_Click(object sender, EventArgs e)
        {
            CreateApproveContract();
        }

        private void CreateApproveContract()
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_contract);
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
                        fillGridApproved(gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned);
                        //gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned
                        memoEdit5.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 62129 - approve.SaveOrUpdate()");
                        fillGridApproved(gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned);
                        memoEdit5.Text = approve.Remark;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void simpleButton_editContractSign_Click(object sender, EventArgs e)
        {
            EditeContractApprove();
        }

        private void EditeContractApprove()
        {
            Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_contract, gridView_contractSigned);
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

                        fillGridApproved(gridControl_contractSigned, approve.FiledocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned);
                        memoEdit5.Text = approve.Remark;
                        break;
                    }
                case DialogResult.No:
                    {
                        approve.Approved = false;
                        if (!approve.SaveOrUpdate())
                            MessageBox.Show("Ошибка  сохранения 13130 - EditeContractApprove()");
                        fillGridApproved(gridControl_contractSigned, approve.FiledocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned);
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
                        int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_contract);
                        if (selectedFileDocID == null)
                            return;
                        DeleteApprove(gridView_contract, gridView_contractSigned);
                        fillGridApproved(gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void gridView_contractSigned_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
           Approve approve = GetSelected_Approve_fromgridViewSigned(gridView_contract, gridView_contractSigned);
                    if (approve == null)
                    {
                        memoEdit5.Text = String.Empty;
                        simpleButton_editContractSign.Enabled = simpleButton_DeleteContractSign.Enabled = false;
                    }
                    else
                    {
                        //разрешить редактировать/удалять резолюцию только тому, кто ее вносил
                        if (approve.ApproveUserID != curentUser.UserID)
                            simpleButton_editContractSign.Enabled = simpleButton_DeleteContractSign.Enabled = false;
                        else
                        {
                            simpleButton_editContractSign.Enabled = simpleButton_DeleteContractSign.Enabled = true;
                        }
                        memoEdit5.Text = approve.Remark;
                    }
                    if (isApproveExist(gridView_contractSigned))
                        simpleButton_ApproveContract.Enabled = false;
        }

        private void gridView_contract_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int? selectedFileDocID = GetSelected_FiledocID_fromGridView(gridView_contract);
            if (selectedFileDocID == null)
            {
                BlockButton_Contract(true);
                gridControl_contractSigned.DataSource = null;
                return;
            }
            BlockButton_Contract(false);
            fillGridApproved(gridControl_contractSigned, (int)selectedFileDocID, memoEdit5, simpleButton_ApproveContract, gridView_contract, gridView_contractSigned);//показать какие документы одобрены
            if (gridView_contract.RowCount != 0)
                FillGridView_contractDocs_contractDocWorks();//показать документы относящиеся к контракту и документы о выполнении работ по контракту
       }

        private void simpleButton_refreshContract_Click(object sender, EventArgs e)
        {
            FillGridControl_Contract();
        }

        //------------------------------------------------------------------ other docs for contract !------------------------
        private void FillGridView_contractDocs_contractDocWorks() //заполнение двух гридов с документацией по контракту
        {
            if (GetSelected_contractID_fromGridContract() == null)
                return;
            Contract contract = new Contract
                                    {
                                        ContractID = ((int) GetSelected_contractID_fromGridContract())
                                    };
            contract.GetListAllContractDocsByContractID(gridControl_contractDocs, gridControl_contracDocWorks, typeDocList[(int)Enums.typedoc.contractdoc].TypedocID, typeDocList[(int)Enums.typedoc.contractworksdoc].TypedocID);
            CheckContrackDocsButtons();
        }


        private void CheckContrackDocsButtons()
        {
            if (gridView_contractDocs.RowCount == 0)
            {
                SB_LookContractDocs.Enabled = SB_EditcontractDocs.Enabled = SB_DeletecontractDocs.Enabled = false;
            }
            else
            {
                SB_LookContractDocs.Enabled = SB_EditcontractDocs.Enabled = SB_DeletecontractDocs.Enabled = true;
            }

            if (gridView_contractDocWorks.RowCount == 0)
            {

               SB_LookcontractDocWorks.Enabled = SB_EditcontractDocWorks.Enabled = SB_DeletecontractDocWorks.Enabled = false;
            }
            else
            {
                SB_LookcontractDocWorks.Enabled = SB_EditcontractDocWorks.Enabled = SB_DeletecontractDocWorks.Enabled = true;
            }

            
        }



#endregion Contract=====================================================================================

        #region ContractDocs***********************************************************************************
        private void sb_AddcontractDocs_Click(object sender, EventArgs e)
        {
            if (GetSelected_contractID_fromGridContract() == null)
                return;
            AddNewContractDocs();
        }


        private bool AddNewContractDocs()
        {
            /**
             
        
        private int ;
        private int ;
        private Int64 ;
        
        
        private bool isNew;
             */

            if (typeDocList.Count == 0)
                return false;
            if (GetSelected_contractID_fromGridContract() == null)
                return false;
            ContractDoc contractDoc = new ContractDoc();
            contractDoc.IsActive = false;
            contractDoc.TypedocID = typeDocList[(int)Enums.typedoc.contractdoc].TypedocID;
            contractDoc.ContractID = (int) GetSelected_contractID_fromGridContract();

            var vol = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "amount");
            if (vol != null)
            {
                Int64 amount;
                Int64.TryParse(vol.ToString(), out amount);
                contractDoc.AmnountS = amount;
            }

            contractDoc.AvanceA = 20;
                contractDoc.RetentionU = 5;
                contractDoc.S1 = (long)0;

                var vol2 = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "currencyID");
                if (vol2 != null)
                {
                    Int32 curencyid;
                    Int32.TryParse(vol2.ToString(), out curencyid);
                    contractDoc.CurrencyID = curencyid;
                }

            

            if (!contractDoc.CreateContractDocGetID())
                return false;

            string titleContract = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "title").ToString();
            if (titleContract == null)
            {
                titleContract = String.Empty;
            }

            Files files = UploadFileDoc("Выберите документ с относящимся к контракту " + titleContract);
            if (files == null)
                return false;

            contractDoc.IsNew = true;
            CreateContractDoc(contractDoc, files, titleContract);
            return true;

        }

        private void CreateContractDoc(ContractDoc contractDoc, Files files, string titleContract)
        {
            string tradeName = String.Empty;
            if (comboBoxEdit_tradeList5.SelectedIndex >= 0)
                tradeName = comboBoxEditTradeList[comboBoxEdit_tradeList5.SelectedIndex].NameTrade;
            files.Title = String.Format("Изменения №{0}",gridView_contractDocs.RowCount + 1);
            //test
            //end test
            FormContractDocs2 form = new FormContractDocs2(contractDoc, files, tradeName, false, curentUser, null, true, titleContract);
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!contractDoc.Update())
                            MessageBox.Show("Ошибка создания 26423 - contractDoc.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка создания 26423 - files.Update()");
                        else
                        {
                            Filedoc filedoc = new Filedoc
                            {
                                DocID = contractDoc.ContractDocsID,
                                FileID = files.FileID,
                                IsActive = true,
                                TradeID = GetSelectedTradeIDfromComboBox(),
                                TypedocID = contractDoc.TypedocID
                            };
                            filedoc.Create();
                        }
                        FillGridView_contractDocs_contractDocWorks();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
            FillGridControl_Contract();
        }

        private void SB_EditcontractDocs_Click(object sender, EventArgs e)
        {
            EditContractDoc();
        }

        private int? GetSelected_contractDocsID_fromGridContractDoc()
        {
            if (gridView_contractDocs.RowCount == 0)
                return null;
            if (gridView_contractDocs.GetSelectedRows().Length == 0)
                return null;
            int result;
            var vol = gridView_contractDocs.GetRowCellValue(gridView_contractDocs.GetSelectedRows()[0], "contractDocsID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }

        private static string FillContractDocFiellds_fromGridView(ColumnView gv, ContractDoc contractDoc)
        {

            if (gv.RowCount == 0)
                return String.Empty;
            if (gv.GetSelectedRows().Length == 0)
                return String.Empty;

            var vol5 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "contractID");
            if (vol5 != null)
            {
                int contractID;
                Int32.TryParse(vol5.ToString(), out contractID);
                contractDoc.ContractID = contractID;
            }

            var vol9 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "amnountS");
            if (vol9 != null)
            {
                Int64 amount;
                Int64.TryParse(vol9.ToString(), out amount);
                contractDoc.AmnountS = amount;
            }

            var vol6 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "currencyID");
            if (vol6 != null)
            {
                int currencyID;
                Int32.TryParse(vol6.ToString(), out currencyID);
                contractDoc.CurrencyID = currencyID;
            }

            var vol8 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "s1");
             if (vol8 != null)
            {
                Int64 s1;
                Int64.TryParse(vol8.ToString(), out s1);
                contractDoc.S1 = s1;
            }

             var vol7 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "avanceA");
             if (vol7 != null)
             {
                 int avanceA;
                 Int32.TryParse(vol7.ToString(), out avanceA);
                 contractDoc.AvanceA = avanceA;
             }

             var vol4 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "retentionU");
             if (vol4 != null)
             {
                 int retentionU;
                 Int32.TryParse(vol4.ToString(), out retentionU);
                 contractDoc.RetentionU = retentionU;
             }

            var title = String.Empty;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "title");
             if (vol != null)
             {
                 title = vol.ToString();
             }
            return title;
            
        }

        private void EditContractDoc()
        {
            if (GetSelected_contractDocsID_fromGridContractDoc() == null)
                return;


           ContractDoc contractDoc = new ContractDoc();

           if (GetSelected_contractDocsID_fromGridContractDoc() != null)
               contractDoc.ContractDocsID = (int)GetSelected_contractDocsID_fromGridContractDoc();
            contractDoc.IsActive = true;
            contractDoc.TypedocID = typeDocList[(int)Enums.typedoc.contractdoc].TypedocID;

            string titleContractDoc = FillContractDocFiellds_fromGridView(gridView_contractDocs, contractDoc);
            contractDoc.IsNew = false;

            //var id = invites.InviteID;
            Files files = new Files();
            switch (MessageBox.Show(String.Format("Редактировать параметры документа {0}?", titleContractDoc), "Редактирование", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                //без замены файла
                case DialogResult.OK:
                    {
                        FillFileFiellds_fromGridVew(gridView_contractDocs, files);
                        files.IsNew = false;
                        if (files.IsFileInBaseOrInShare)
                        {
                            files.Body = files.GetFileBodyByFileID();
                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        return;
                    }
            }

            string tradeName = String.Empty;
            if (comboBoxEdit_tradeList5.SelectedIndex >= 0)
                tradeName = comboBoxEditTradeList[comboBoxEdit_tradeList5.SelectedIndex].NameTrade;
            //files.Signed = false;
            //files.Datelastupdate = DateTime.Now;
            var vol2 = gridView_contractDocs.GetRowCellValue(gridView_contractDocs.GetSelectedRows()[0], "datecreate");
            if (vol2 != null)
            {
                DateTime datecreate;
                DateTime.TryParse(vol2.ToString(), out datecreate);
                files.Datecreate = datecreate;
            }
            //test
            var dd = GetSelected_FiledocID_fromGridView(gridView_contractDocs);
            var ss = dd;
            //endtest

            FormContractDocs2 form = new FormContractDocs2(contractDoc, files, tradeName, false, curentUser, (int)GetSelected_FiledocID_fromGridView(gridView_contractDocs), true, titleContractDoc);

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!contractDoc.Update())
                            MessageBox.Show("Ошибка  редактирования 173 - ContractDoc.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка  редактирования 174 - files.Update() ");
                       
                        //перечитываем грид
                        FillGridView_contractDocs_contractDocWorks();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
            FillGridControl_Contract();
        }

        private void SB_LookContractDocs_Click(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_contractDocs) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_contractDocs), gridView_contractDocs);
        }

        private void SB_DeletecontractDocs_Click(object sender, EventArgs e)
        {
            if (gridView_contractDocs.RowCount == 0 || GetSelected_contractDocsID_fromGridContractDoc() == null)
                return;

            DeleteContractDoc();
        }

        private void DeleteContractDoc()
        {
            switch (MessageBox.Show(String.Format("Удалить документ '{0}'?", GetSelected_FileTitle_fromGridView(gridView_contractDocs)), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        ContractDoc contractDoc = new ContractDoc();
                        contractDoc.TypedocID = typeDocList[(int)Enums.typedoc.contractdoc].TypedocID;
                        int? contractDocsID = GetSelected_contractDocsID_fromGridContractDoc();
                        if (contractDocsID != null)
                        {
                            contractDoc.ContractDocsID = (int)GetSelected_contractDocsID_fromGridContractDoc();
                            if (!contractDoc.Delete())
                                MessageBox.Show("Ошибка при удалении документа относящегося к контракту");
                            else
                                FillGridView_contractDocs_contractDocWorks();
                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }
   
        private void gridView_contractDocs_DoubleClick(object sender, EventArgs e)
        {
            LookContractDoc();
        }

        private void LookContractDoc()
        {
            if (GetSelected_contractDocsID_fromGridContractDoc() == null)
                return;


            ContractDoc contractDoc = new ContractDoc();

            if (GetSelected_contractDocsID_fromGridContractDoc() != null)
                contractDoc.ContractDocsID = (int)GetSelected_contractDocsID_fromGridContractDoc();
            contractDoc.IsActive = true;
            contractDoc.TypedocID = typeDocList[(int)Enums.typedoc.contractdoc].TypedocID;

            string titleContractDoc = FillContractDocFiellds_fromGridView(gridView_contractDocs, contractDoc);
            contractDoc.IsNew = false;

            //var id = invites.InviteID;
            Files files = new Files();
            FillFileFiellds_fromGridVew(gridView_contractDocs, files);
            files.IsNew = false;
            if (files.IsFileInBaseOrInShare)
              {
                          files.Body = files.GetFileBodyByFileID();
              }
            string tradeName = String.Empty;
            if (comboBoxEdit_tradeList5.SelectedIndex >= 0)
                tradeName = comboBoxEditTradeList[comboBoxEdit_tradeList5.SelectedIndex].NameTrade;
            //files.Signed = false;
            //files.Datelastupdate = DateTime.Now;
            var vol2 = gridView_contractDocs.GetRowCellValue(gridView_contractDocs.GetSelectedRows()[0], "datecreate");
            if (vol2 != null)
            {
                DateTime datecreate;
                DateTime.TryParse(vol2.ToString(), out datecreate);
                files.Datecreate = datecreate;
            }
            FormContractDocs2 form = new FormContractDocs2(contractDoc, files, tradeName, true, curentUser, (int)GetSelected_FiledocID_fromGridView(gridView_contractDocs), false, titleContractDoc);
            form.ShowDialog();
        }

    #endregion ContractDocs=====================================================================================
        
        #region ContractDocsWork***********************************************************************************
        
        private void SB_AddcontractDocWorks_Click(object sender, EventArgs e)
                {
                    if (GetSelected_contractID_fromGridContract() == null)
                        return;
                    AddNewContractDocsWorks();
                }

        private bool AddNewContractDocsWorks()
        {
          
            if (typeDocList.Count == 0)
                return false;
            if (GetSelected_contractID_fromGridContract() == null)
                return false;
            ContractDocWork docWork = new ContractDocWork();
            docWork.IsActive = false;
            docWork.TypedocID = typeDocList[(int)Enums.typedoc.contractworksdoc].TypedocID;
            docWork.ContractID = (int)GetSelected_contractID_fromGridContract();

            var vol = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "amount");
            if (vol != null)
            {
                Int64 amount;
                Int64.TryParse(vol.ToString(), out amount);
                docWork.Amnount = amount;
            }

            var vol2 = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "typefundingID");
            if (vol2 != null)
            {
                int typefundingID;
                Int32.TryParse(vol2.ToString(), out typefundingID);
                docWork.TypefundingID = typefundingID;
            }
            docWork.TypeWork = "Вид выполненных работ";
            /*
             *  private int contractDocWorksID;
             */

            docWork.AvanceA = 20;
            docWork.RetentionU = 5;
            docWork.S1 = (long)0;

            var vol3 = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "currencyID");
            if (vol3 != null)
            {
                Int32 curencyid;
                Int32.TryParse(vol3.ToString(), out curencyid);
                docWork.CurrencyID = curencyid;
            }
            

            if (!docWork.CreateContractDocWorkGetID())
                return false;

            var titleContract = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "title");
            if (titleContract == null)
            {
                titleContract = String.Empty;
            }

            Files files = UploadFileDoc("Выберите документ о выполнении работ по контракту " + titleContract);
            if (files == null)
                return false;

            docWork.IsNew = true;
            CreateContractDocWork(docWork, files);
            return true;

        }

        private void CreateContractDocWork(ContractDocWork contractDocWork, Files files)
        {
            string tradeName = String.Empty;
            if (comboBoxEdit_tradeList5.SelectedIndex >= 0)
                tradeName = comboBoxEditTradeList[comboBoxEdit_tradeList5.SelectedIndex].NameTrade;
            
            var titleContract = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "title") ??
                                String.Empty;

            files.Title = String.Format("акт вып. работ №{0}", gridView_contractDocWorks.RowCount + 1);
            //test
            //end test
            FormContractDocs3 form = new FormContractDocs3(contractDocWork, files, tradeName, false, curentUser, null, true, titleContract.ToString());
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!contractDocWork.Update())
                            MessageBox.Show("Ошибка создания 26423 - ContractDocWork.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка создания 26423 - files.Update()");
                        else
                        {
                            Filedoc filedoc = new Filedoc
                            {
                                DocID = contractDocWork.ContractDocWorksID,
                                FileID = files.FileID,
                                IsActive = true,
                                TradeID = GetSelectedTradeIDfromComboBox(),
                                TypedocID = contractDocWork.TypedocID
                            };
                            filedoc.Create();
                        }
                        FillGridView_contractDocs_contractDocWorks();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void SB_EditcontractDocWorks_Click(object sender, EventArgs e)
        {
            EditContractDocWork();
        }

        //

        private int? GetSelected_contractDocWorksID_fromGridContractDocWorks()
        {
            if (gridView_contractDocWorks.RowCount == 0)
                return null;
            if (gridView_contractDocWorks.GetSelectedRows().Length == 0)
                return null;
            int result;
            var vol = gridView_contractDocWorks.GetRowCellValue(gridView_contractDocWorks.GetSelectedRows()[0], "contractDocWorksID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }

        private static string FillContractDocWorkFiellds_fromGridView(ColumnView gv, ContractDocWork contractDocWork)
        {
            if (gv.RowCount == 0)
                return String.Empty;
            if (gv.GetSelectedRows().Length == 0)
                return String.Empty;

            var vol5 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "contractID");
            if (vol5 != null)
            {
                int contractID;
                Int32.TryParse(vol5.ToString(), out contractID);
                contractDocWork.ContractID = contractID;
            }

            var vol9 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "amount");
            if (vol9 != null)
            {
                Int64 amount;
                Int64.TryParse(vol9.ToString(), out amount);
                contractDocWork.Amnount = amount;
            }

            var vol6 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "currencyID");
            if (vol6 != null)
            {
                int currencyID;
                Int32.TryParse(vol6.ToString(), out currencyID);
                contractDocWork.CurrencyID = currencyID;
            }

            var vol8 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "s1");
            if (vol8 != null)
            {
                Int64 s1;
                Int64.TryParse(vol8.ToString(), out s1);
                contractDocWork.S1 = s1;
            }

            var vol7 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "avanceA");
            if (vol7 != null)
            {
                int avanceA;
                Int32.TryParse(vol7.ToString(), out avanceA);
                contractDocWork.AvanceA = avanceA;
            }

            var vol4 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "retentionU");
            if (vol4 != null)
            {
                int retentionU;
                Int32.TryParse(vol4.ToString(), out retentionU);
                contractDocWork.RetentionU = retentionU;
            }

            var title = String.Empty;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "title");
            if (vol != null)
            {
                title = vol.ToString();
            }

            
            var vol0 = gv.GetRowCellValue(gv.GetSelectedRows()[0], "typeWork");
            if (vol0 != null)
            {
               contractDocWork.TypeWork = vol0.ToString();
            }
            
            return title;

        }

        /*/*contractDocWorksID
shortname
filedocID
fileID
filename
isFileInBaseOrInShare
signed
datelastupdate
             */




        private void EditContractDocWork()
        {
            if (GetSelected_contractDocWorksID_fromGridContractDocWorks() == null)
                return;


            ContractDocWork contractDocWork = new ContractDocWork();

            if (GetSelected_contractDocWorksID_fromGridContractDocWorks() != null)
                contractDocWork.ContractDocWorksID = (int)GetSelected_contractDocWorksID_fromGridContractDocWorks();
            contractDocWork.IsActive = true;
            contractDocWork.TypedocID = typeDocList[(int)Enums.typedoc.contractworksdoc].TypedocID;

           // string titleContractDocWork = 
            FillContractDocWorkFiellds_fromGridView(gridView_contractDocWorks, contractDocWork);
            
            var titleContract = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "title") ??
                               String.Empty;
            contractDocWork.IsNew = false;

            //var id = invites.InviteID;
            Files files = new Files();
            switch (MessageBox.Show(String.Format("Редактировать параметры документа {0}?", titleContract), "Редактирование", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                //без замены файла
                case DialogResult.OK:
                    {
                        FillFileFiellds_fromGridVew(gridView_contractDocWorks, files);
                        files.IsNew = false;
                        if (files.IsFileInBaseOrInShare)
                        {
                            files.Body = files.GetFileBodyByFileID();
                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        return;
                    }
            }

            string tradeName = String.Empty;
            if (comboBoxEdit_tradeList5.SelectedIndex >= 0)
                tradeName = comboBoxEditTradeList[comboBoxEdit_tradeList5.SelectedIndex].NameTrade;
            //files.Signed = false;
            //files.Datelastupdate = DateTime.Now;
            var vol2 = gridView_contractDocWorks.GetRowCellValue(gridView_contractDocWorks.GetSelectedRows()[0], "datecreate");
            if (vol2 != null)
            {
                DateTime datecreate;
                DateTime.TryParse(vol2.ToString(), out datecreate);
                files.Datecreate = datecreate;
            }

            var vol3 = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "typefundingID");
            if (vol3 != null)
            {
                int typefundingID;
                Int32.TryParse(vol3.ToString(), out typefundingID);
                contractDocWork.TypefundingID = typefundingID;
            }

            FormContractDocs3 form = new FormContractDocs3(contractDocWork, files, tradeName, false, curentUser, (int)GetSelected_FiledocID_fromGridView(gridView_contractDocWorks), true, titleContract.ToString());

            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!contractDocWork.Update())
                            MessageBox.Show("Ошибка  редактирования 173 - contractDocWork.Update()");
                        if (!files.Update())
                            MessageBox.Show("Ошибка  редактирования 174 - files.Update() ");

                        //перечитываем грид
                        FillGridView_contractDocs_contractDocWorks();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void SB_LookcontractDocWorks_Click(object sender, EventArgs e)
        {
            if (GetSelected_FileID_fromGridView(gridView_contractDocWorks) != null)
                OpenFileByFileID((int)GetSelected_FileID_fromGridView(gridView_contractDocWorks), gridView_contractDocWorks);


        }

        private void SB_DeletecontractDocWorks_Click(object sender, EventArgs e)
        {
            if (gridView_contractDocWorks.RowCount == 0 || GetSelected_contractDocWorksID_fromGridContractDocWorks() == null)
                    return;

            DeleteContractDocWork();
        }

        private void DeleteContractDocWork()
        {
            switch (MessageBox.Show(String.Format("Удалить документ '{0}'?", GetSelected_FileTitle_fromGridView(gridView_contractDocWorks)), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        ContractDocWork docWork = new ContractDocWork();
                        docWork.TypedocID = typeDocList[(int)Enums.typedoc.contractworksdoc].TypedocID;
                        int? contractDocsworkID = GetSelected_contractDocWorksID_fromGridContractDocWorks();
                        if (contractDocsworkID != null)
                        {
                            docWork.ContractDocWorksID = (int)GetSelected_contractDocWorksID_fromGridContractDocWorks();
                            if (!docWork.Delete())
                                MessageBox.Show("Ошибка при удалении документа относящегося к контракту");
                            else
                                FillGridView_contractDocs_contractDocWorks();
                        }
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void gridView_contractDocWorks_DoubleClick(object sender, EventArgs e)
        {
            LookContractDocWork();
        }

        private void LookContractDocWork()
        {
            if (GetSelected_contractDocWorksID_fromGridContractDocWorks() == null)
                return;


            ContractDocWork docWork = new ContractDocWork();

            if (GetSelected_contractDocWorksID_fromGridContractDocWorks() != null)
                docWork.ContractDocWorksID = (int)GetSelected_contractDocWorksID_fromGridContractDocWorks();
            docWork.IsActive = true;
            docWork.TypedocID = typeDocList[(int)Enums.typedoc.contractdoc].TypedocID;

            FillContractDocWorkFiellds_fromGridView(gridView_contractDocWorks, docWork);
            docWork.IsNew = false;

            //var id = invites.InviteID;
            Files files = new Files();
            FillFileFiellds_fromGridVew(gridView_contractDocWorks, files);
            files.IsNew = false;
            if (files.IsFileInBaseOrInShare)
            {
                files.Body = files.GetFileBodyByFileID();
            }
            string tradeName = String.Empty;
            if (comboBoxEdit_tradeList5.SelectedIndex >= 0)
                tradeName = comboBoxEditTradeList[comboBoxEdit_tradeList5.SelectedIndex].NameTrade;
            //files.Signed = false;
            //files.Datelastupdate = DateTime.Now;
            var vol2 = gridView_contractDocWorks.GetRowCellValue(gridView_contractDocWorks.GetSelectedRows()[0], "datecreate");
            if (vol2 != null)
            {
                DateTime datecreate;
                DateTime.TryParse(vol2.ToString(), out datecreate);
                files.Datecreate = datecreate;
            }
            var vol3 = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "typefundingID");
            if (vol3 != null)
            {
                int typefundingID;
                Int32.TryParse(vol3.ToString(), out typefundingID);
                docWork.TypefundingID = typefundingID;
            }
            var titleContract = gridView_contract.GetRowCellValue(gridView_contract.GetSelectedRows()[0], "title") ??
                               String.Empty;
            FormContractDocs3 form = new FormContractDocs3(docWork, files, tradeName, true, curentUser, (int)GetSelected_FiledocID_fromGridView(gridView_contractDocWorks), false, titleContract.ToString());
            form.ShowDialog();
        }

        #endregion ContractDocsWork=====================================================================================

        #region SimpleButton Click Events
        private void navBarItem_projectName_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormProjectNameCatalog form = new FormProjectNameCatalog();
            form.ShowDialog();
        }

        private void navBarItem_templateDoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormTemplateDoc form = new FormTemplateDoc();
            form.ShowDialog();
        }

        private void simpleButton_templateDoc_Click(object sender, EventArgs e)
        {

            FormTemplateDoc form = new FormTemplateDoc();
            form.ShowDialog();
        }
        #endregion SimpleButton Click Events

        #region Payment ==================================


        #region NavBarClick
        private void navBarItem_s_zaim_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pZaim);
            form.ShowDialog();
        }

        private void navBarItem_s_oblast_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pOblast);
            form.ShowDialog();
        }

        private void navBarItem_s_contragents_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pContragent);
            form.ShowDialog();
            
        }

        private void navBarItem_s_contract_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pContract);
            form.ShowDialog();
            
        }

        private void navBarItem_s_cat_contract_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pCategorycontract);
            form.ShowDialog();
        }

        private void navBarItem_s_cat_work_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pCategorywork);
            form.ShowDialog();
            
        }

        private void navBarItem_s_subcat_work_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pSubcategorywork);
            form.ShowDialog();
        }

        private void navBarItem_s_object_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FormPaymentSpravochnik form = new FormPaymentSpravochnik((int)Enums.cataqlogPaymentID.pObject);
            form.ShowDialog();
        }

        #endregion NavBarClick

        private void simpleButton_payment_2excel_Click(object sender, EventArgs e)
        {
            if (gridView_payment.RowCount != 0)
                utils.ExportGV2Xls("список платежей" + DateTime.Now.Day + "_" + DateTime.Now.Month, gridView_payment, true, "Список данных");
            else
                MessageBox.Show("нет данных");
        }

        private void simpleButton_payment_Add_Click(object sender, EventArgs e)
        {
            AddPayment((int)Enums.pTypePaymentID.fact);
        }

        private void simpleButton_payment_plan_add_Click(object sender, EventArgs e)
                {
                    AddPayment((int)Enums.pTypePaymentID.plan);
                }

        private void AddPayment(int type)
        {
            Payment payment = new Payment();
            payment.IsNew = true;
            payment.User = curentUser;
            FormPaymet form = new FormPaymet(payment, type, false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadGridPayment();    
            }
            
        }

        private Payment GetSelectedPayment()
        {
            if (GetSelected_paymentID_fromGridView(gridView_payment) == null)
                return null;

            Payment payment = new Payment();
          
            var vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "paymentID");
            int result = 0;
            if (vol != null)
            {
                Int32.TryParse(vol.ToString(), out result);
                payment.PaymentID = result;
            }
            var vol2 = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pZaimID");
            if (vol2 != null)
            {
                Int32.TryParse(vol2.ToString(), out result);
                payment.PZaimID = result;
            }
            var vol3 = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "typefundingID");
            if (vol3 != null)
            {
                Int32.TryParse(vol3.ToString(), out result);
                payment.TypefundingID = result;
            }
            var vol4 = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pContragentID");
            if (vol4 != null)
            {
                Int32.TryParse(vol4.ToString(), out result);
                payment.PContragentID = result;
            }

            var vol5 = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pContractID");
            if (vol5 != null)
            {
                Int32.TryParse(vol5.ToString(), out result);
                payment.PContractID = result;
            }

            var vol6 = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "currencyID");
            if (vol6 != null)
            {
                Int32.TryParse(vol6.ToString(), out result);
                payment.CurrencyID = result;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pCategorycontractID");
            if (vol != null)
            {
                Int32.TryParse(vol.ToString(), out result);
                payment.PCategorycontractID = result;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pCategoryworkID");
            if (vol != null)
            {
                Int32.TryParse(vol.ToString(), out result);
                payment.PCategoryworkID = result;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pSubcategoryworkID");
            if (vol != null)
            {
                Int32.TryParse(vol.ToString(), out result);
                payment.PSubcategoryworkID = result;
            }
            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pObjectID");
            if (vol != null)
            {
                Int32.TryParse(vol.ToString(), out result);
                payment.PObject = result;
            }
            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pDescription");
            if (vol != null)
            {
                payment.PDescription = vol.ToString();
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "datelastupdate");
            if (vol != null)
            {
                DateTime res;
                DateTime.TryParse(vol.ToString(), out res);
                payment.Datelastupdate = res;
            }
            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "datepay");
            if (vol != null)
            {
                DateTime res;
                DateTime.TryParse(vol.ToString(), out res);
                payment.Datepay = res;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "AmountUSD");
            if (vol != null)
            {
                decimal res;
                decimal.TryParse(vol.ToString(), out res);
                payment.AmountUSD = res;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "AmountBYR");
            if (vol != null)
            {
                decimal res;
                decimal.TryParse(vol.ToString(), out res);
                payment.AmountBYR = res;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "AmountEUR");
            if (vol != null)
            {
                decimal res;
                decimal.TryParse(vol.ToString(), out res);
                payment.AmountEUR = res;
            }
            
            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "AmountRUB");
            if (vol != null)
            {
                decimal res;
                decimal.TryParse(vol.ToString(), out res);
                payment.AmountRUB = res;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "RateUSD");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                payment.RateUSD = result;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "RateEUR");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                payment.RateEUR = result;
            }
            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "RateRUB");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                payment.RateRUB = result;
            }

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "pTypePaymentID");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                payment.PTypePayment = result;
            }

            

            vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "userID");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                Users user = new Users();
                user.FillUserFieldsByUserID(result);
                payment.User = user;
            }

            

            return payment;
        }

        private void LookEditPayment()
        {
            Payment payment = GetSelectedPayment();
           

            if (payment == null)
                return;

            payment.IsNew = false;
            FormPaymet form;
            //для боса редактирование ЗАПРЕЩЕНО!!!
            form = curentUser.GroupID == (int)Enums.group.boss ? new FormPaymet(payment, payment.PTypePayment, true) : new FormPaymet(payment, payment.PTypePayment, false);

              if (form.ShowDialog() == DialogResult.OK)
            {
                LoadGridPayment();    
            }
        }


        private void FillPaymentTab()
        {
            
            dateEdit_payment_from.EditValue = new DateTime(2011, 01, 01);
            dateEdit_payment_to.EditValue = DateTime.Now.Date;
            int type; 
            if (radioButton_fact.Checked)
                type = (int) Enums.pTypePaymentID.fact;
            else
                type = (int) Enums.pTypePaymentID.plan;

            Payment payment = new Payment();
            payment.GetListAllPayment(gridControl_payment, Convert.ToDateTime(dateEdit_payment_from.EditValue), Convert.ToDateTime(dateEdit_payment_to.EditValue), type);
        }

      

        private void simpleButton_payment_filtr_apply_Click(object sender, EventArgs e)
        {
            LoadGridPayment();    
        }

        private void LoadGridPayment()
        {
            int type;
            if (radioButton_fact.Checked)
                type = (int)Enums.pTypePaymentID.fact;
            else
                type = (int)Enums.pTypePaymentID.plan;

            Payment payment = new Payment();
            payment.GetListAllPayment(gridControl_payment, Convert.ToDateTime(dateEdit_payment_from.EditValue), Convert.ToDateTime(dateEdit_payment_to.EditValue), type);
        
        }


        private void gridView_payment_DoubleClick(object sender, EventArgs e)
        {
            if (GetSelected_paymentID_fromGridView(gridView_payment) != null)
                LookEditPayment();
    
        }

        private static int? GetSelected_paymentID_fromGridView(ColumnView gv)
        {
            if (gv.RowCount == 0)
                return null;
            int result;
            if (gv.GetSelectedRows().Length == 0)
                return null;
            var vol = gv.GetRowCellValue(gv.GetSelectedRows()[0], "paymentID");
            if (vol == null)
                return null;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   

        }
       

        private String GetSelectedPaymentName()
        {
            var vol = gridView_payment.GetRowCellValue(gridView_payment.GetSelectedRows()[0], "datepay");
            if (vol != null)
            {
                DateTime res;
                DateTime.TryParse(vol.ToString(), out res);
                return res.ToLongDateString();
            }
            return String.Empty;
        }

        private void simpleButton_payment_delete_Click(object sender, EventArgs e)
        {
            Delete_Payment();
        }

        private void Delete_Payment()
        {
            if (GetSelected_paymentID_fromGridView(gridView_payment) == null)
                return;


            switch (MessageBox.Show(String.Format("Удалить платежку за {0} ?", GetSelectedPaymentName()), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        Payment payment = new Payment();
                        payment.PaymentID = (int)GetSelected_paymentID_fromGridView(gridView_payment);
                        payment.IsActive = false;
                        payment.Delete();
                        LoadGridPayment();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void navBarItem_payment_add_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddPayment((int)Enums.pTypePaymentID.fact);
        }

        private void navBarItem_payment_edit_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
          if (GetSelected_paymentID_fromGridView(gridView_payment) != null)
                        LookEditPayment();
        }

        private void navBarItem_payment_delete_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Delete_Payment();
        }

        #endregion Payment ==================================================================

     
 


        #region Finance ==================================================================


        private void FillFinanceTab()
        {
            dateEdit_finance_from.EditValue = new DateTime(2010, 01, 01);
            dateEdit_finance_to.EditValue = DateTime.Now.Date.AddDays(30);
            Payment payment = new Payment();
            payment.GetListAllFinance(gridControl_finance, Convert.ToDateTime(dateEdit_finance_from.EditValue), Convert.ToDateTime(dateEdit_finance_to.EditValue));
    
        }

        private void LoadGridFinance()
        {
            Payment payment = new Payment();
            if (radioButton_all.Checked)
            {
                payment.GetListAllFinance(gridControl_finance, Convert.ToDateTime(dateEdit_finance_from.EditValue), Convert.ToDateTime(dateEdit_finance_to.EditValue));
            }
            else

            {
                if (radioButton_directpay.Checked)
                {
                    payment.GetListAllFinance(gridControl_finance, Convert.ToDateTime(dateEdit_finance_from.EditValue), Convert.ToDateTime(dateEdit_finance_to.EditValue), (int)Enums.pTypePaymentID.finance_direct);
                }
                else
                {
                    payment.GetListAllFinance(gridControl_finance, Convert.ToDateTime(dateEdit_finance_from.EditValue), Convert.ToDateTime(dateEdit_finance_to.EditValue), (int)Enums.pTypePaymentID.finance_specaccount);
                }
            }


        }

        private void simpleButton_finance_filtr_Click(object sender, EventArgs e)
        {
            LoadGridFinance();
        }

        private void simpleButton_finance_add_Click(object sender, EventArgs e)
        {
            AddFinance((int)Enums.pTypePaymentID.finance_direct);
        } 
        private void simpleButton_finance_spec_account_Add_Click(object sender, EventArgs e)
        {
            AddFinance((int)Enums.pTypePaymentID.finance_specaccount);
        }

        private void AddFinance(int type)
        {
            Payment payment = new Payment();
            payment.IsNew = true;
            payment.User = curentUser;
            FormFinance form = new FormFinance(payment, type, false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadGridFinance();
            }

        }

        private void gridView_finance_DoubleClick(object sender, EventArgs e)
        {
            if (GetSelected_paymentID_fromGridView(gridView_finance) != null)
                LookEditFinance();
        }

        private void LookEditFinance()
        {
            Payment payment = GetSelectedFinance();


            if (payment == null)
                return;

            payment.IsNew = false;
            FormFinance form;
            //для боса редактирование ЗАПРЕЩЕНО!!!
            form = curentUser.GroupID == (int)Enums.group.boss ? new FormFinance(payment, payment.PTypePayment, true) : new FormFinance(payment, payment.PTypePayment, false);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadGridFinance();
            }
        }

        private Payment GetSelectedFinance()
        {
            if (GetSelected_paymentID_fromGridView(gridView_finance) == null)
                return null;

            Payment payment = new Payment();

            var vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "paymentID");
            int result = 0;
            if (vol != null)
            {
                Int32.TryParse(vol.ToString(), out result);
                payment.PaymentID = result;
            }
            var vol2 = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "pZaimID");
            if (vol2 != null)
            {
                Int32.TryParse(vol2.ToString(), out result);
                payment.PZaimID = result;
            }
            var vol3 = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "typefundingID");
            if (vol3 != null)
            {
                Int32.TryParse(vol3.ToString(), out result);
                payment.TypefundingID = result;
            }
            

            

            var vol6 = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "currencyID");
            if (vol6 != null)
            {
                Int32.TryParse(vol6.ToString(), out result);
                payment.CurrencyID = result;
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "pDescription");
            if (vol != null)
            {
                payment.PDescription = vol.ToString();
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "datelastupdate");
            if (vol != null)
            {
                DateTime res;
                DateTime.TryParse(vol.ToString(), out res);
                payment.Datelastupdate = res;
            }
            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "datepay");
            if (vol != null)
            {
                DateTime res;
                DateTime.TryParse(vol.ToString(), out res);
                payment.Datepay = res;
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "AmountUSD");
            if (vol != null)
            {
                decimal res;
                decimal.TryParse(vol.ToString(), out res);
                payment.AmountUSD = res;
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "AmountBYR");
            if (vol != null)
            {
                decimal res;
                decimal.TryParse(vol.ToString(), out res);
                payment.AmountBYR = res;
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "AmountEUR");
            if (vol != null)
            {
                decimal res;
                decimal.TryParse(vol.ToString(), out res);
                payment.AmountEUR = res;
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "AmountRUB");
            if (vol != null)
            {
                decimal res;
                decimal.TryParse(vol.ToString(), out res);
                payment.AmountRUB = res;
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "RateUSD");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                payment.RateUSD = result;
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "RateEUR");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                payment.RateEUR = result;
            }
            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "RateRUB");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                payment.RateRUB = result;
            }

            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "pTypePaymentID");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                payment.PTypePayment = result;
            }



            vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "userID");
            if (vol != null)
            {

                Int32.TryParse(vol.ToString(), out result);
                Users user = new Users();
                user.FillUserFieldsByUserID(result);
                payment.User = user;
            }



            return payment;
        }

      private void simpleButton_finance_2excel_Click(object sender, EventArgs e)
        {
            if (gridView_finance.RowCount != 0)
                utils.ExportGV2Xls("список денежных поступлений" + DateTime.Now.Day + "_" + DateTime.Now.Month, gridView_finance, true, "Список данных");
            else
                MessageBox.Show("нет данных");
        }


       

        private void simpleButton_Delete_Finance_Click(object sender, EventArgs e)
        {
             if (GetSelected_paymentID_fromGridView(gridView_finance) == null)
                return;


            switch (MessageBox.Show(String.Format("Удалить фин поступление за {0} ?", GetSelectedFinanceName()), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        Payment payment = new Payment
                                              {
                                                  PaymentID =
                                                      ((int) GetSelected_paymentID_fromGridView(gridView_finance)),
                                                  IsActive = false
                                              };
                        payment.Delete();
                        LoadGridFinance();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private String GetSelectedFinanceName()
        {
            var vol = gridView_finance.GetRowCellValue(gridView_finance.GetSelectedRows()[0], "datepay");
            if (vol != null)
            {
                DateTime res;
                DateTime.TryParse(vol.ToString(), out res);
                return res.ToLongDateString();
            }
            return String.Empty;
        }


        #endregion Finance ==================================================================


        #region Reports-----------------------------------
        private void simpleButton_report1a_Click(object sender, EventArgs e)
        {
           /* var openFile1 = new OpenFileDialog();

            // Initialize the OpenFileDialog to look for text files.
            openFile1.Filter = "Excel Files|*.xls";
            openFile1.Title = "Выберите файл Excel для закачки в минскую базу из Радиальной";
            string filename;
            // Check if the user selected a file from the OpenFileDialog.
            if (openFile1.ShowDialog() == DialogResult.OK)
                filename = openFile1.FileName;
            else return;
            */

            /*
            string sPath = String.Format("{0}\\Template\\{1}", Environment.CurrentDirectory, "report1a.xls");

            DataSet ds;

            try
            {
                ds = utils.ImportFromExcelToDataSet(sPath, false);
            }

            catch (Exception err)
            {
                MessageBox.Show("ошибко импорта файла " + filename + err.Message);
                return;
            }
 * */
            ImportExcelForm form = new ImportExcelForm();
            form.ShowDialog();
            
        }



        private void navBarItem_report_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ImportExcelForm form = new ImportExcelForm();
            form.ShowDialog();
        }


        private void simpleButton_report_Click(object sender, EventArgs e)
        {
            ImportExcelForm form = new ImportExcelForm();
            form.ShowDialog();
        }
        #endregion Reports-----------------------------------

        

        





    }
}


    /*
     * 
        #region export to excel
        //if (gridView_report.RowCount != 0)
        //        utils.ExportGV2Xls(nameReport + DateTime.Now.Day + "_" + DateTime.Now.Month, gridView_report, barCheckItem_open.Checked, shortnameReport); 
        //    else
        //        MessageBox.Show("нет данных");
        #endregion

#region ZIP FILE
        private void ZipUnZip()
        {
            try
            {
                string filename = "compressedFile.txt";
                string sourceString = "Source String";
                utils.SaveCompressedFile(filename, sourceString);
                FileInfo compressedFileData = new FileInfo(filename);
                string recoveredString = utils.LoadCompressedFile(filename);
            }

            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion ZIP FILE
        
        #region test
        private void simpleButton_s_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word doc|*.doc|Word docx|*.docx|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Open File";
            openFileDialog.FilterIndex = 3;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!utils.FilesCopyFromTo(openFileDialog.FileName, Properties.Settings.Default.SavePathShare, Path.GetFileName(openFileDialog.FileName), false))
                {
                    MessageBox.Show(String.Format("Ошибка сохранения файла из {0} в {1}", openFileDialog.FileName,
                                    Properties.Settings.Default.SavePathShare));
                    return;
                }
                utils.OpenFileInWordOrExcel(Properties.Settings.Default.SavePathShare,
                                        Path.GetFileName(openFileDialog.FileName));
            }
           openFileDialog.Dispose();
        }

        private void simpleButton_l_Click(object sender, EventArgs e)
        {
           // SaveBytesToFile()  
        }
        #endregion test
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * http://www.gotdotnet.ru/forums/1/75391/
     * 
     * 
     * 
     
SaveFileDialog saveFileDlg = new SaveFileDialog();

if (saveFileDlg.ShowDialog() == DialogResult.OK)

{

SqlCommandBuilder builder = new SqlCommandBuilder(myAdapter);

DataSet ds = new DataSet();

myAdapter.Fill(ds);

FileStream stream = new FileStream(saveFileDlg.FileName, FileMode.OpenOrCreate);

BinaryWriter writer = new BinaryWriter(stream);

byte[] b1 = (byte[])ds.Tables[0].Rows[listBox1.SelectedIndex]["Content"];

writer.Write(b1); writer.Close(); stream.Close();

}
     */


