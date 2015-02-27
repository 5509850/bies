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
    public partial class FormCurencyCatalog : Form
    {
        private DateTime date_cur = DateTime.Now.Date;
        public FormCurencyCatalog()
        {
            InitializeComponent();
            FillGridControl();
        }

        public FormCurencyCatalog(DateTime date)
        {
            InitializeComponent();
            this.date_cur = date;
            FillGridControl();
        }

        private void simpleButton_Edit_Click(object sender, EventArgs e)
        {
            Currency cur = new Currency
                               {
                                   Byr = new CurrencyRate
                                             {
                                                 CurrencyID = ((int) Enums.currencyID.byr),
                                                 Rate = 1,
                                                 Date = date_cur
                                             },
                                   Eur = new CurrencyRate
                                             {
                                                 CurrencyID = ((int) Enums.currencyID.eur),
                                                 Rate = 4500,
                                                 Date = date_cur
                                             },
                                   Rub = new CurrencyRate
                                             {
                                                 CurrencyID = ((int) Enums.currencyID.rub),
                                                 Rate = 110,
                                                 Date = date_cur
                                             },
                                   Usd = new CurrencyRate
                                             {
                                                 CurrencyID = ((int) Enums.currencyID.usd),
                                                 Rate = 3100,
                                                 Date = date_cur
                                             }
                               };
            if (!FillDataForSeletedDateTime(cur))
                return;
            FormAddCurrency form = new FormAddCurrency(cur, false, gridView_curency);
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!cur.Update())
                            MessageBox.Show("Ошибка редактирования 5476 - Currency.Update()");
                        FillGridControl();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private bool FillDataForSeletedDateTime(Currency cur)
        {
            if (gridView_curency.RowCount == 0)
                return false;
            if (gridView_curency.GetSelectedRows().Length == 0)
                return false;
            bool result = false;
            var vol = gridView_curency.GetRowCellValue(gridView_curency.GetSelectedRows()[0], "date");
            if (vol == null)
                return false;
            DateTime date;
            DateTime.TryParse(vol.ToString(), out date);

            for (int row = 0; row < gridView_curency.RowCount; row++)
            {
                if (Convert.ToDateTime(gridView_curency.GetRowCellValue(row, "date")).Date == date.Date)
                {
                    try
                    {
                        if (Convert.ToInt32(gridView_curency.GetRowCellValue(row, "currencyID")) == (int)Enums.currencyID.byr)
                        {
                            cur.Byr.Rate = Math.Round(Convert.ToDouble(gridView_curency.GetRowCellValue(row, "rate")),2);
                            cur.Byr.Date = date.Date;
                        }
                        if (Convert.ToInt32(gridView_curency.GetRowCellValue(row, "currencyID")) == (int)Enums.currencyID.eur)
                        {
                            cur.Eur.Rate = Math.Round(Convert.ToDouble(gridView_curency.GetRowCellValue(row, "rate")),2);
                            cur.Eur.Date = date.Date;
                        }
                        if (Convert.ToInt32(gridView_curency.GetRowCellValue(row, "currencyID")) == (int)Enums.currencyID.rub)
                        {
                            cur.Rub.Rate = Math.Round(Convert.ToDouble(gridView_curency.GetRowCellValue(row, "rate")),2);
                            cur.Rub.Date = date.Date;
                        }

                        if (Convert.ToInt32(gridView_curency.GetRowCellValue(row, "currencyID")) == (int)Enums.currencyID.usd)
                        {
                            cur.Usd.Rate = Math.Round(Convert.ToDouble(gridView_curency.GetRowCellValue(row, "rate")), 2);
                            cur.Usd.Date = date.Date;
                        }
                        result = true;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                    
                }
            }
            return result;
        
        }

        private void simpleButton_Add_Click(object sender, EventArgs e)
        {
            Currency cur = new Currency
                               {
                                   Byr = new CurrencyRate
                                             {
                                                 CurrencyID = ((int) Enums.currencyID.byr),
                                                 Rate = 1,
                                                 Date = date_cur
                                             },
                                   Eur = new CurrencyRate
                                             {
                                                 CurrencyID = ((int) Enums.currencyID.eur),
                                                 Rate = 4500,
                                                 Date = date_cur
                                             },
                                   Rub = new CurrencyRate
                                             {
                                                 CurrencyID = ((int) Enums.currencyID.rub),
                                                 Rate = 110,
                                                 Date = date_cur
                                             },
                                   Usd = new CurrencyRate
                                             {
                                                 CurrencyID = ((int) Enums.currencyID.usd),
                                                 Rate = 3100,
                                                 Date = date_cur
                                             }
                               };

            FormAddCurrency form = new FormAddCurrency(cur, true, gridView_curency);
            switch (form.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        if (!cur.Create())
                            MessageBox.Show("Ошибка создания 5476 - Currency.Create()");
                        FillGridControl();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void FillGridControl()
        {
            Currency cur = new Currency();
            cur.FillCurremcyGrid(gridControl_curency);
        }
    }
}
