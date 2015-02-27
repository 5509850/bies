using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using bies.Entity;

namespace bies
{
    public partial class LoginForm : Form
    {
        private bool blocked = false;
        private bool deleted = false;
        private bool userNotExist = false;
        private bool NeedChangePass = false;
        private DateTime filtr = new DateTime(2011, 1, 1);
        Users curentUser = new Users();

        public LoginForm()
        {
            InitializeComponent();
            textEdit_login.Focus();
            LoadSetting();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = blocked; //блокирование закрытия формы
        }

        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            blocked = false;
        }

        private void simpleButton_OK_Click(object sender, EventArgs e)
        {
            Check();
        }

        private void ChangeFormForChangePassword()
        {
            textEdit_password.Text =
                textEdit_new.Text = String.Empty;

            labelControl_result.Visible =
                simpleButton_OK.Visible = false;

            simpleButton_save.Visible =
            labelControl_new.Visible =
                textEdit_new.Visible = true;

            labelControl_pass.Text = "новый пароль";
            textEdit_login.Enabled = false;
            textEdit_password.TabIndex = 0;
            textEdit_new.TabIndex = 1;
            simpleButton_save.TabIndex = 2;
            simpleButton_Cancel.TabIndex = 3;

            Text = "Введите новый пароль";
        }

        private void Check()
        {
            if (textEdit_login.Text.Length == 0)
            {
                MessageBox.Show("Не введено имя!");
                blocked = true;
                return;
            }
            //if (textEdit_password.Text.Length == 0)
            //{
            //    MessageBox.Show("Не введен пароль!");
            //    blocked = true;
            //    return;
            //}

            if (IsPassValid(textEdit_login.Text, textEdit_password.Text))
            {
                if (deleted)
                    return;
                if (NeedChangePass)
                {
                    blocked = true;
                    ChangeFormForChangePassword();
                    return;
                }
                blocked = false;
            }
            else
            {
                if (deleted)
                    return;
                blocked = true;
                labelControl_result.Visible = true;
                if (!userNotExist)
                {
                    MessageBox.Show("Не верный пароль!");
                    labelControl_result.Text = "Пароль не верный. Попробуйте еще раз.";
                    textEdit_password.Text = String.Empty;
                }
                textEdit_password.Focus();
            }
        }

        private bool IsPassValid(string login, string pass)
        {
          if (!curentUser.FillUserFieldsByLogin(login))
          {
              //no connect
              MessageBox.Show(String.Format("БД {0} не доступна - сервер выключен?", Properties.Settings.Default.BiesConnectionString.Remove(26)));
              DialogResult = DialogResult.Cancel;
              blocked = false;
              deleted = true;
              Close();
              return false;

          }
            if (curentUser.UserID == 0)
           {
               MessageBox.Show("Такого пользователя не существует");
               labelControl_result.Text = "Такого пользователя не существует";
               userNotExist = true;
               textEdit_login.Focus();
               return false;
           }

           if (!curentUser.IsActive)
           {
               MessageBox.Show(String.Format("Пользователь {0} удален из базы!", curentUser.Login));
               DialogResult = DialogResult.Cancel;
               blocked = false;
               deleted = true;
               Close();
               return false;
           }

           userNotExist = false;
           bool isPassValid = curentUser.Password.Equals(utils.GetHashString(pass));
           if (curentUser.NeedChangPassword && isPassValid)
           {
               NeedChangePass = true;
               return true;
           }
           if (isPassValid)
           {
               if (!SaveSettings(curentUser.Login, filtr))
                   MessageBox.Show("Настройки не сохранены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
            

           return isPassValid;
        }
       

        private void textEdit_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !NeedChangePass)
            {
                Check();
                DialogResult = deleted ? DialogResult.Cancel : DialogResult.OK;
                Close();
            }
                
        }

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
                textEdit_login.Text = settings.L;
                filtr = settings.TradeDateFrom;
                textEdit_password.Focus();
                textEdit_password.TabIndex = 0;
                simpleButton_OK.TabIndex = 1;
                simpleButton_Cancel.TabIndex = 2;
                textEdit_login.TabIndex = 3;
            }
            catch
            {
                return;
            }

            //#define DEBUG
            // ...
            #if DEBUG
            textEdit_password.Text = "1";
            #endif
        }

        private static bool SaveSettings(string login, DateTime filtr)
        {
            try
            {
                LoginSetting settings = new LoginSetting();
                settings.L = login;
                settings.TradeDateFrom = filtr;
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

        private void simpleButton_ChangePassword_Click(object sender, EventArgs e)
        {
            UpdatePassword();
        }

        private void UpdatePassword()
        {
            if (textEdit_password.Text.Length == 0 || textEdit_new.Text.Length == 0)
            {
                MessageBox.Show("пароль не может быть пустым!");
                labelControl_result.Visible = true;
                labelControl_result.Text = "попробуйте еще раз";
                return;
            }
            if (textEdit_password.Text != textEdit_new.Text)
            {
                MessageBox.Show("Пароли не совпадают!");
                labelControl_result.Visible = true;
                labelControl_result.Text = "попробуйте еще раз";
                textEdit_password.Text = textEdit_new.Text = String.Empty;
                return;
            }

            if (curentUser.UpdatePassword(curentUser.UserID, utils.GetHashString(textEdit_password.Text)))
            {
                DialogResult = deleted ? DialogResult.Cancel : DialogResult.OK;
                blocked = false;
                if (!SaveSettings(curentUser.Login, filtr))
                    MessageBox.Show("Настройки не сохранены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
                MessageBox.Show("Произошла ошибка при изменении пароля");
        }

        private void textEdit_new_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && NeedChangePass)
                UpdatePassword();
        }

    }
}
