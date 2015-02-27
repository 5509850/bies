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
    public partial class FormTradeEdit : Form
    {
        private Trades trade;
        private bool blocked = false;
        private List <Users> usersList = new List<Users>();
        private bool isBoss;

        public FormTradeEdit(Trades trade, bool isBoss)
        {
            InitializeComponent();
                
            this.trade = trade;
            this.isBoss = isBoss;
            this.Text = this.trade.IsNew ? "Создание новых торгов" : "Редактирование торгов";
            FillProjectNameList();

            if (isBoss)
            {
                //UserResponse.Visible = false; 
                FillComboUsers();
            }
            else
            {
                comboBoxEdit_userResponce.Visible = false;
            }
            FillData();
        }

        private void FillComboUsers()
        {
            Users user = new Users();
            //usersList
            List<Users> users = user.GetListAllUsers();
            usersList.Clear();
            comboBoxEdit_userResponce.Properties.Items.Clear();
            foreach (Users _user in users)
            {
                if (_user.IsActive && _user.GroupID == (int) Enums.group.user)
                {
                    comboBoxEdit_userResponce.Properties.Items.Add(_user.Fio);
                    usersList.Add(_user);
                }
                
            }
        }

        private void SetSelectedUserComboBox(int userIDrespons)
        {
            if (usersList.Count != comboBoxEdit_userResponce.Properties.Items.Count)
                return;

            for (int i = 0; i < usersList.Count; i++)
            {
                if (usersList[i].UserID == userIDrespons)
                {
                    comboBoxEdit_userResponce.SelectedIndex = i;
                    return;
                }
            }
        }

        private void FillData()
        {
            if (this.trade.IsNew)
            {
                UserResponse.Text = trade.UserNameRespons;
                DateBegin.EditValue = DateTime.Now;
            }
            else
            {
                if (isBoss)
                {
                    SetSelectedUserComboBox(trade.UserIDrespons);
                }
                Number.Text = trade.Number;
                TradeName.Text = trade.Name;
                ProgectName.Text = trade.Projectname;
                UserResponse.Text = trade.UserNameRespons;
                if (trade.TradetypeID == (int)Enums.tradetype.withPredqulifications)
                    radioButton_tradetype1.Checked = true;
                else
                    radioButton_tradetype2.Checked = true;
                DateBegin.EditValue = trade.Datebegin;   
            }
        }
       
        private bool Check()
        {
            if (String.IsNullOrEmpty(Number.Text))
            {
                MessageBox.Show("Не введен номер торгов!");
                Number.Focus();
                blocked = true;
                return false;
            }

            if (String.IsNullOrEmpty(TradeName.Text))
            {
                MessageBox.Show("Не введено наименование торгов!");
                TradeName.Focus();
                blocked = true;
                return false;
            }
            if (String.IsNullOrEmpty(ProgectName.Text))
            {
                MessageBox.Show("Не введено наименование проекта!");
                ProgectName.Focus();
                blocked = true;
                return false;
            }

            if (DateBegin.EditValue == null)
            {
                MessageBox.Show("Не введено дата начала торгов!");
                DateBegin.Focus();
                blocked = true;
                return false;
            }
            if (isBoss)
            {
                if (comboBoxEdit_userResponce.SelectedIndex < 0)
                {
                    MessageBox.Show("Не выбран ответсвенный для торгов!");
                    comboBoxEdit_userResponce.Focus();
                    blocked = true;
                    return false;
                }
            }
            blocked = false;
            return true;
        }

        private void FormTradeEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = blocked; //блокирование закрытия формы
        }
      
        private void simpleButton_save_Click(object sender, EventArgs e)
        {
            if (!Check())
                return;
            trade.Number = Number.Text;
            trade.Name = TradeName.Text;
            trade.Projectname = ProgectName.Text;
            trade.UserNameRespons = UserResponse.Text;
            if (radioButton_tradetype1.Checked)
                trade.TradetypeID = (int)Enums.tradetype.withPredqulifications;
            else
                trade.TradetypeID = (int)Enums.tradetype.withoutPredqulifications;
            trade.Datebegin = Convert.ToDateTime(DateBegin.EditValue);
            if (isBoss)
            {
                trade.UserIDrespons = usersList[comboBoxEdit_userResponce.SelectedIndex].UserID;
                trade.UserNameRespons = usersList[comboBoxEdit_userResponce.SelectedIndex].Fio;
            }
        }

        private void simpleButton_cancel_Click(object sender, EventArgs e)
        {
            blocked = false;
        }

        private void FillProjectNameList()
        {
            trade.FillProjectNameList(comboBoxEdit_ProjectName);
            comboBoxEdit_ProjectName.SelectedIndex = GetSelectedIndexForComboBoxEdit();
        }

        private int GetSelectedIndexForComboBoxEdit()
        {
            for (int i = 0; i < comboBoxEdit_ProjectName.Properties.Items.Count; i ++)
            {
               if (comboBoxEdit_ProjectName.Properties.Items[i].ToString().Equals(trade.Projectname))
                   return i;
            }
            return 0;
        }

        private void comboBoxEdit_ProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgectName.Text = comboBoxEdit_ProjectName.SelectedItem.ToString();
        }

        private void simpleButton_catalogProjects_Click(object sender, EventArgs e)
        {
            FormProjectNameCatalog form = new FormProjectNameCatalog();
            form.ShowDialog();
            FillProjectNameList();
        }

        private void comboBoxEdit_userResponce_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserResponse.Text = comboBoxEdit_userResponce.SelectedItem.ToString();
        }
    }
}
