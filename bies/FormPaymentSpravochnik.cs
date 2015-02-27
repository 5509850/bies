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
    public partial class FormPaymentSpravochnik : Form
    {
        private int type;
        private bool isHaveDropBox;
        private List<ComboBoxItems> comboBoxItems;
        readonly PaymentBaseCatalog pbc = new PaymentBaseCatalog();

        public FormPaymentSpravochnik(int type)
        {
            InitializeComponent();
            this.type = type;
            isHaveDropBox = (this.type.Equals((int) Enums.cataqlogPaymentID.pContract) ||
                             this.type.Equals((int) Enums.cataqlogPaymentID.pSubcategorywork));
            
            LoadData();
        }

        public void LoadData()
        {
            if (!isHaveDropBox) //без комбобокса
            {
                labelControl_work_oblast.Visible =
                    comboBoxEdit_work_oblast.Visible = false;
                pbc.Load(gridControl_base, GetTableName());
            }
            else // сущности с комбобоксом (подкатегорией работ или контракт с областью)
            {
                comboBoxItems = new List<ComboBoxItems>();
                pbc.Load(gridControl_base, GetTableName(), comboBoxItems);
                FillComboBox();
                SetSelectedMainIDItemComboBox();
            }
        }

        private void FillComboBox()
        {
            if (comboBoxItems != null && comboBoxItems.Count != 0 && comboBoxEdit_work_oblast.Properties.Items.Count == 0)
            {

                foreach (ComboBoxItems item in comboBoxItems)
                {
                    comboBoxEdit_work_oblast.Properties.Items.Add(item.Name);
                }
            }
        }

        private void SetSelectedMainIDItemComboBox()
        {
            if (pbc.PCategoryworkID != 0 && comboBoxItems != null && comboBoxItems.Count != 0)
            {
                foreach (ComboBoxItems item in comboBoxItems)
                {
                    if (item.Id.Equals(pbc.PCategoryworkID))
                    {
                        comboBoxEdit_work_oblast.SelectedItem = item.Name;
                        return;
                    }
                }
            }
        }

        private String GetTableName()
        {
            switch (type)
            {
                case  (int) Enums.cataqlogPaymentID.pZaim:
                    {
                        groupBox1.Text =
                            Text = "справочник Займов";
                        return Enums.cataqlogPaymentID.pZaim.ToString();
                    }

                case (int)Enums.cataqlogPaymentID.pObject:
                    {
                        groupBox1.Text =
                            Text = "справочник Объектов";
                        return Enums.cataqlogPaymentID.pObject.ToString();
                    }

                case (int)Enums.cataqlogPaymentID.pContragent:
                    {
                        groupBox1.Text =
                            Text = "справочник контрагентов";
                        return Enums.cataqlogPaymentID.pContragent.ToString();
                    }

                case (int)Enums.cataqlogPaymentID.pContract:
                    {
                        groupBox1.Text =
                            Text = "справочник контрактов";
                        labelControl_work_oblast.Text = "область";
                        return Enums.cataqlogPaymentID.pContract.ToString();
                    }
                case (int)Enums.cataqlogPaymentID.pCategorycontract:
                    {
                        groupBox1.Text =
                            Text = "справочник категорий контрактов";
            
                        return Enums.cataqlogPaymentID.pCategorycontract.ToString();
                    }
                case (int)Enums.cataqlogPaymentID.pCategorywork:
                    {
                        groupBox1.Text =
                            Text = "справочник категорий работ";
                        return Enums.cataqlogPaymentID.pCategorywork.ToString();
                    }

                case (int)Enums.cataqlogPaymentID.pSubcategorywork:
                    {
                        labelControl_work_oblast.Text = "категория работ";
                        groupBox1.Text =
                            Text = "справочник подкатегорий работ";
                     return Enums.cataqlogPaymentID.pSubcategorywork.ToString();
                    }
                case (int)Enums.cataqlogPaymentID.pOblast:
                    {
                        groupBox1.Text =
                            Text = "справочник областей";
                        return Enums.cataqlogPaymentID.pOblast.ToString();
                    }
                    

                    
                    

                    

                    
            }
            return String.Empty;
        }

        private void simpleButton_Add_Click(object sender, EventArgs e)
        {
            if (isHaveDropBox && comboBoxEdit_work_oblast.SelectedIndex < 0)
            {
                if (this.type.Equals((int) Enums.cataqlogPaymentID.pContract))
                {
                    MessageBox.Show("Не выбрана область!");
                }
                else
                {
                    MessageBox.Show("Не выбрана подкатегория работ!");
                }
                
                
                comboBoxEdit_work_oblast.Focus();
                return;
            }

            if (String.IsNullOrEmpty(textEdit_add.Text))
            {
                MessageBox.Show("Не введены данные!");
                textEdit_add.Focus();
                return;
            }
            
                pbc.IsNew = true;
                pbc.Name = textEdit_add.Text;
                pbc.IsActive = true;
                if (isHaveDropBox)
                {
                    if (comboBoxItems == null || comboBoxItems.Count == 0 || comboBoxEdit_work_oblast.SelectedIndex < 0)
                        return;
                    pbc.PCategoryworkID = pbc.POblastID = comboBoxItems[comboBoxEdit_work_oblast.SelectedIndex].Id;
                    //если контракт - то добавляем область true иначе подкатегория работ - false
                    if (!pbc.CreateOrUpdate(GetTableName(), (this.type.Equals((int) Enums.cataqlogPaymentID.pContract))))
                        MessageBox.Show("Ошибка добавления данных в базу");
                }
                else
                {
                    if (!pbc.CreateOrUpdate(GetTableName()))
                        MessageBox.Show("Ошибка добавления данных в базу");    
                }
                LoadData();
                textEdit_add.Text = String.Empty;
        }

        private void gridView_base_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            pbc.Id = GetSelectedID();
            pbc.PCategoryworkID = pbc.POblastID = GetSelectedMainCatOrOblastID();

            if (pbc.Id == 0)
            {
                simpleButton_edit.Enabled =
                     simpleButton_delete.Enabled = false;
                return;
            }

            textEdit_edit.Text = GetSelectedName();
            simpleButton_edit.Enabled =
                simpleButton_delete.Enabled = true;

            if (isHaveDropBox)
                SetSelectedMainIDItemComboBox();
        }

        private int GetSelectedID()
        {
            int result;
            if (gridView_base.RowCount == 0)
                return 0;
            if (gridView_base.GetSelectedRows().Length == 0)
                return 0;
            var vol = gridView_base.GetRowCellValue(gridView_base.GetSelectedRows()[0], "ID");
            if (vol == null)
                return 0;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   
        }

        private int GetSelectedMainCatOrOblastID()
        {
            int result;
            if (gridView_base.RowCount == 0)
                return 0;
            if (gridView_base.GetSelectedRows().Length == 0)
                return 0;
            Object vol = null;
            if (type.Equals((int)Enums.cataqlogPaymentID.pContract))
            {
                vol = gridView_base.GetRowCellValue(gridView_base.GetSelectedRows()[0], "pOblastID");
            }
            if (type.Equals((int)Enums.cataqlogPaymentID.pSubcategorywork))
            {
                vol = gridView_base.GetRowCellValue(gridView_base.GetSelectedRows()[0], "pCategoryworkID");
            }
            
            if (vol == null)
                return 0;
            Int32.TryParse(vol.ToString(), out result);
            return result; //   
        }

        private String GetSelectedName()
        {
            if (gridView_base.RowCount == 0)
                return String.Empty;
            if (gridView_base.GetSelectedRows().Length == 0)
                return String.Empty;
            var vol = gridView_base.GetRowCellValue(gridView_base.GetSelectedRows()[0], "name");
            if (vol == null)
                return String.Empty;

            return vol.ToString();
        }

        private bool GetSelectedIdActiv()
        {
            bool result;
            if (gridView_base.RowCount == 0)
                return false;
            if (gridView_base.GetSelectedRows().Length == 0)
                return false;
            var vol = gridView_base.GetRowCellValue(gridView_base.GetSelectedRows()[0], "isActive");
            if (vol == null)
                    return false;
            Boolean.TryParse(vol.ToString(), out result);
            return result; //   
        }

        private void simpleButton_edit_Click(object sender, EventArgs e)
        {
            simpleButton_edit.Enabled = simpleButton_delete.Enabled = (pbc.Id != 0);
            //блокирование кнопока редлактирование/удаление, если не выбран объект
            if (String.IsNullOrEmpty(textEdit_edit.Text))
            {
                MessageBox.Show("Не введены данные!");
                textEdit_edit.Focus();
                return;
            }
            if (isHaveDropBox && comboBoxEdit_work_oblast.SelectedIndex < 0)
            {
                if (this.type.Equals((int)Enums.cataqlogPaymentID.pContract))
                {
                    MessageBox.Show("Не выбрана область!");
                }
                else
                {
                    MessageBox.Show("Не выбрана подкатегория работ!");
                }


                comboBoxEdit_work_oblast.Focus();
                return;
            }
            if (isHaveDropBox)
            {
                if (comboBoxItems == null || comboBoxItems.Count == 0 || comboBoxEdit_work_oblast.SelectedIndex < 0)
                return;
                if (pbc.Id != 0)
                {
                    pbc.IsNew = false;
                    pbc.Name = textEdit_edit.Text;
                    pbc.IsActive = GetSelectedIdActiv();
                    pbc.PCategoryworkID = pbc.POblastID = comboBoxItems[comboBoxEdit_work_oblast.SelectedIndex].Id;
                    if (!pbc.CreateOrUpdate(GetTableName(), (type.Equals((int)Enums.cataqlogPaymentID.pContract))))
                        MessageBox.Show("Ошибка добавления данных в базу");
                    LoadData();
                }
                else
                {
                    simpleButton_edit.Enabled = simpleButton_delete.Enabled = false;
                }
            }
            else //without isHaveDropBox
            {
                if (pbc.Id != 0)
                {
                    pbc.IsNew = false;
                    pbc.Name = textEdit_edit.Text;
                    pbc.IsActive = GetSelectedIdActiv();
                    if (!pbc.CreateOrUpdate(GetTableName()))
                        MessageBox.Show("Ошибка добавления данных в базу");
                    LoadData();
                }
                else
                {
                    simpleButton_edit.Enabled = simpleButton_delete.Enabled = false;
                }
            }
        }

        private void simpleButton_delete_Click(object sender, EventArgs e)
        {

            if (GetSelectedIdActiv()) //delete
            {
                switch (MessageBox.Show(String.Format("Удалить {0} ?", GetSelectedName()), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.OK:
                        {
                            pbc.IsNew = false;
                            pbc.Name = textEdit_edit.Text;
                            pbc.IsActive = !GetSelectedIdActiv();
                            if (!pbc.CreateOrUpdate(GetTableName()))
                                MessageBox.Show("Ошибка удаления данных в базе");
                            LoadData();
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            break;
                        }
                }
            }
            else //recovery
            {
                pbc.IsNew = false;
                pbc.Name = textEdit_edit.Text;
                pbc.IsActive = !GetSelectedIdActiv();
                if (!pbc.CreateOrUpdate(GetTableName()))
                    MessageBox.Show("Ошибка удаления данных в базе");
                LoadData();
            }
            
            
        }
    }
}
