using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using bies.Entity;

namespace bies
{
    
    public partial class ImportExcelForm : Form
    {
        #region var

        /*
         13:12:17 mila_iolovich private void BBI_Invoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
  {
            double summSNds = 0;
   GV1_MasterRowCollapsing(null,null);
   BCI_LookNakl.Checked=true;
   GV1_MasterRowExpanding(null,null);
   //   MessageBox.Show("Не работает пока","однако",MessageBoxButtons.OK,MessageBoxIcon.Hand);
   TrXLRep.TrXLReport MRep = new TrXLRep.TrXLReport();
   TrXLRep.TrXLReport MyReport;
   TrXLRep.trxlrepFilesStruct f = new TrXLRep.trxlrepFilesStruct(); 
   MyReport = new TrXLRep.TrXLReport(); 
   System.Data.SqlClient.SqlCommand Scom =new System.Data.SqlClient.SqlCommand(@"
   SELECT 
    [003 Клиенты(новая)].[грузополучатель] +
   CASE WHEN [003 Клиенты(новая)].[код банка]<>0 THEN ', р/сч: ' + [003 Клиенты(новая)].[№счета] + ' в ' + banks.[Название банка] + ', ' + banks.[адрес] + ', код ' + cast([003 Клиенты(новая)].[код банка] AS nvarchar) ELSE '' END  
   FROM 
    [003 Клиенты(новая)] 
   INNER JOIN  
    [2_расх_шапки] 
   ON [2_расх_шапки].[Клиент] = [003 Клиенты(новая)].[Клиент] 
   LEFT JOIN 
    banks 
   ON banks.IdBank=[003 Клиенты(новая)].IdBank 
   WHERE 
    [2_расх_шапки].[№расх]=" + GV1.GetFocusedRowCellValue("idOrd").ToString() + @" 
   UNION all 
   SELECT 
    [003 Клиенты(новая)].[грузополучатель] +
   CASE WHEN [003 Клиенты(новая)].[код банка]<>0 THEN ', р/сч: ' + [003 Клиенты(новая)].[№счета] + ' в ' + banks.[Название банка] + ', ' + banks.[адрес] + ', код ' + cast([003 Клиенты(новая)].[код банка]AS nvarchar) ELSE '' END  
   FROM [003 Клиенты(новая)] 
   LEFT JOIN 
    banks 
   ON banks.IdBank=[003 Клиенты(новая)].IdBank 
   WHERE 
    [УНВП]=(SELECT TOP 1 t_config_main.my_id  FROM t_config_main)",UtilClass.SCon);
   Scom.Connection.Open();
   System.Data.SqlClient.SqlDataReader dr;
   dr=Scom.ExecuteReader();
   int l=0;
   string rec="";
   string sen="";
   while (dr.Read())
   {
    if (l==0)
    {
     rec=dr[0].ToString();
    } 
    else
    {
     sen=dr[0].ToString();
    }
    l=l+1;
   }
   dr.Close();
   Scom.Connection.Close();
   double AllNds=0;
   double AllSNDS=0;

            //#region TEST


            //int count = 0;

            //MessageBox.Show(ds1.dt_SpecOrdView.Rows[0].ItemArray.Length.ToString());
            //foreach (System.Data.DataRow datr in ds1.dt_SpecOrdView)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    for (int i = 0; i < datr.ItemArray.Length; i++)//37/53
            //    {
            //        sb.Append(i + "=" + datr[i] + ";");
            //        datr[i].ToString();
            //    }
            //    MessageBox.Show(sb.ToString());
            //}



            //StringBuilder s_nds = new StringBuilder();
            //StringBuilder price_w_nds = new StringBuilder();

            //foreach (System.Data.DataRow datr in ds1.dt_SpecOrdView)
            //{
            //    //MessageBox.Show(datr.ItemArray.Length.ToString());
            //    if (datr.ItemArray.Length == 53)
            //    {
            //        s_nds.Append(Math.Round((double)datr[27], 0) + "\n");
            //        price_w_nds.Append(Math.Round((double)datr[28], 0) + "\n");
            //    }
            //    else
            //    {
            //        s_nds.Append(Math.Round((double)datr[8], 0) + "\n");
            //        price_w_nds.Append(Math.Round((double)datr[9], 0) + "\n");
            //    }

            //}
            //MessageBox.Show(s_nds.ToString());
            //MessageBox.Show(price_w_nds.ToString());
            //return;

            //#endregion TEST
            
            foreach (System.Data.DataRow datr in ds1.dt_SpecOrdView)
   {
                //if (datr.ItemArray.Length != 53 && datr.ItemArray.Length != 37)
                //{
                //    MessageBox.Show("вот так: " + datr.ItemArray.Length);
                //    return;
                //}
       if (datr.ItemArray.Length >= 53)
                {
                    AllNds = AllNds + Math.Round((double)datr[27], 0); //datr["s_nds"]
                    AllSNDS = AllSNDS + Math.Round((double)datr[28], 0); //datr["price_w_nds"]
                }
                else
                {
                    AllNds = AllNds + Math.Round((double)datr[11], 0); //datr["s_nds"]
                    AllSNDS = AllSNDS + Math.Round((double)datr[9], 0); //datr["price_w_nds"]
                }

   }
   CurrencyGenerator a = new CurrencyGenerator();
   
   string AllNdsT=a.BRBAsString(Convert.ToInt32(AllNds));
   AllNdsT=
    AllNdsT.Substring(0,1).ToUpper()+
    AllNdsT.Substring(1,AllNdsT.Length-1);
   string AllSNDST =a.BRBAsString(Convert.ToInt32(AllSNDS));
   AllSNDST =
    AllSNDST.Substring(0,1).ToUpper()+
    AllSNDST.Substring(1,AllSNDST.Length-1);

   object[,] myObjArray = new object[6,2];
   myObjArray[0, 1] =GV1.GetFocusedRowCellValue("idOrd").ToString();
   myObjArray[1, 1] = GV1.GetFocusedRowCellValue("dateOrd").ToString(); 
   myObjArray[2, 1] = rec;
   myObjArray[3, 1] = sen;
   myObjArray[4, 1] = AllNdsT;
   myObjArray[5, 1] = AllSNDST;

   myObjArray[0, 0] = "numb";
   myObjArray[1, 0] = "date";
   myObjArray[2, 0] = "reciver";
   myObjArray[3, 0] = "sender";
   myObjArray[4, 0] = "AllNds";
   myObjArray[5, 0] = "AllSNDS";

   object varMyObjArray = myObjArray;
   
   object[,] varData = new object[13, ds1.dt_SpecOrdView.Rows.Count+1];
   varData[0,0] = "Art"; 
   varData[1,0] = "goods"; 
   varData[2,0] = "edizm";
   varData[3,0] = "kvo"; 
   varData[4,0] = "priceed";
   varData[5,0] = "stoimed"; 
   varData[6,0] = "stndspr";
   varData[7,0] = "sumndsrub";
   varData[8,0] = "sumsnds";
   varData[9,0] = "tara";
   varData[10,0] = "kvoup";
   varData[11,0] = "kvoprodvupac";
   varData[12,0] = "barcode";
   for (Int32 i=0;i<=ds1.dt_SpecOrdView.Rows.Count-1;i++)
   {
                if (ds1.dt_SpecOrdView.Rows[0].ItemArray.Length >= 53)//(53)/(37)                                                       
                {
                //varData[0,i+1] = ds1.dt_SpecOrdView.Rows[i]["Art"]; //21/
                //varData[1,i+1] = ds1.dt_SpecOrdView.Rows[i]["Name"];//22/
                //varData[2,i+1] = "шт.";
                //varData[3,i+1] = ds1.dt_SpecOrdView.Rows[i]["kolvo"];//23/
                //varData[4,i+1] = ds1.dt_SpecOrdView.Rows[i]["price_no_nds"]; //25/
                //varData[5,i+1] = ds1.dt_SpecOrdView.Rows[i]["stoim"];//26/
                //varData[6,i+1] = ds1.dt_SpecOrdView.Rows[i]["NDS"];//(20%)
                //varData[7,i+1] = ds1.dt_SpecOrdView.Rows[i]["s_nds"];//27/
                //varData[8,i+1] = ds1.dt_SpecOrdView.Rows[i]["price_w_nds"];//28/
                //varData[9,i+1] = (int)ds1.dt_SpecOrdView.Rows[i]["kor"]<1?"н/у":"кор";//32/
                //varData[10,i+1] = ds1.dt_SpecOrdView.Rows[i]["kor"];//32/
                //varData[11,i+1] = ds1.dt_SpecOrdView.Rows[i]["sht"];//?
                //varData[12,i+1] = ds1.dt_SpecOrdView.Rows[i]["barcode"]; //52/
                    varData[0, i + 1] = ds1.dt_SpecOrdView.Rows[i][21]; //21/
                    varData[1, i + 1] = ds1.dt_SpecOrdView.Rows[i][22];//22/
                    varData[2, i + 1] = "шт.";
                    varData[3, i + 1] = ds1.dt_SpecOrdView.Rows[i][23];//23/
                    varData[4, i + 1] = ds1.dt_SpecOrdView.Rows[i][25]; //25/
                    varData[5, i + 1] = ds1.dt_SpecOrdView.Rows[i][26];//26/
                    varData[6, i + 1] = 20;//ds1.dt_SpecOrdView.Rows[i]["NDS"];//(20%)
                    varData[7, i + 1] = ds1.dt_SpecOrdView.Rows[i][27];//27/
                    varData[8, i + 1] = ds1.dt_SpecOrdView.Rows[i][28];//28/
                    varData[9, i + 1] = (int)ds1.dt_SpecOrdView.Rows[i][32] < 1 ? "н/у" : "кор";//32/
                    varData[10, i + 1] = ds1.dt_SpecOrdView.Rows[i][32];//32/
                    varData[11, i + 1] = 555;// ds1.dt_SpecOrdView.Rows[i][30];//?k-vo
                    varData[12, i + 1] = ds1.dt_SpecOrdView.Rows[i][52]; //52/
                }
                else //37
                {
                    varData[0, i + 1] = ds1.dt_SpecOrdView.Rows[i][1];
                    varData[1, i + 1] = ds1.dt_SpecOrdView.Rows[i][2];
                    varData[2, i + 1] = "шт.";
                    varData[3, i + 1] = ds1.dt_SpecOrdView.Rows[i][6];//23/
                    varData[4, i + 1] = ds1.dt_SpecOrdView.Rows[i][8];//666??????? //25/ 5 ->8 // ******** 11->8
                    varData[5, i + 1] = ds1.dt_SpecOrdView.Rows[i][9];//26/
                    varData[6, i + 1] = 20;// ds1.dt_SpecOrdView.Rows[i]["NDS"];//(20%)
                    varData[7, i + 1] = ds1.dt_SpecOrdView.Rows[i][11];//27/
                    varData[8, i + 1] = ds1.dt_SpecOrdView.Rows[i][12];//28/
                    

                    try
                    {
                        summSNds += Convert.ToDouble(varData[8, i + 1]);                          
                        var sa = (int) ds1.dt_SpecOrdView.Rows[i][6];
                        var sb = (int) ds1.dt_SpecOrdView.Rows[i][3];
                        bool qq = sa/sb < 1;
                        varData[9, i + 1] = qq ? "н/у" : "кор";//32/
                        ////[TEST]
                        //StringBuilder sb = new StringBuilder();
                        //for (int j = 0; j < 22; j++)//37/53
                        //{
                        //    sb.Append(j + "=" + ds1.dt_SpecOrdView.Rows[i][j] + ";");
                        //}
                        //MessageBox.Show(sb.ToString());
                        ////[END TEST] 
                        varData[10, i + 1] = varData[12, i + 1] = (int)sa / sb;//ds1.dt_SpecOrdView.Rows[i][36]; //52/
                        varData[11, i + 1] = qq ? sa : 0;//ds1.dt_SpecOrdView.Rows[i][6];//?
                        varData[12, i + 1] = ds1.dt_SpecOrdView.Rows[i][22]; //barcode
                    }
                    catch (Exception)
                    {
                        //nothing
                    }                                   
                }
       
   }
            AllSNDST = a.BRBAsString(Convert.ToInt32(summSNds));
            myObjArray[5, 1] = AllSNDST;  //added now !!!!!!!!!!!!   
   object varDataArray = varData;
   //   string repFullPath =  @"\\server\pbh\invoice.xls";
   //   Console.WriteLine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString())); 

   string repFullPath = Application.StartupPath + @"\invoice.xls";
   f.TemplateWorkbookFullName = repFullPath;
   f.TemplateSheetName = "inv";
   MyReport.Flags =TrXLRep.trxlrepFlags.trxlrepFlagNamesInFirstColumn;
   //   MyReport.Flags = ;
   MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);

  }
13:12:31 mila_iolovich это подойдет?
13:14:06 mila_iolovich еще тут что-то есть
13:14:07 mila_iolovich private void BBI_printRaspiska_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
  {
  TrXLRep.TrXLReport MRep = new TrXLRep.TrXLReport();
    TrXLRep.TrXLReport MyReport;
    TrXLRep.trxlrepFilesStruct f = new TrXLRep.trxlrepFilesStruct(); 
    MyReport = new TrXLRep.TrXLReport(); 
   DataRow dr =
    dS_HistPereuch1.dt_PereuchHistZagl.Select("IdPereuch=" + GV_ZagPereuch.GetFocusedRowCellValue("IdPereuch").ToString())[0];
   object[,] myObjArray = new object[9,2];
   myObjArray[0, 1] = dr["firm"];
   myObjArray[1, 1] = dr["DatePereuch"]; //DataFormats.Format(DTP_date, "Short Date")
   myObjArray[2, 1] = "Материально-ответственое лицо: " + dr["otvperson"];//& tb_mat_otv
   myObjArray[3, 1] = "На основании приказа №: " + dr["prikaz"] + " от " + dr["dateprikaz"] +
                         " произведено снятие фактических остатков ТМЦ.";
   myObjArray[4, 1] = "По состоянию на " + dr["posost"]; //& DataFormats.Format(DTP_sost, "Short Date")
   myObjArray[5, 1] = "Инвентаризация начата " + dr["dbeg"] + " окончена " + dr["Dend"];
   myObjArray[6, 1] = dr["IdFilial"];//lb_filials.Column(1)
   myObjArray[7, 1] = dr["Nprih"];
   myObjArray[8, 1] = dr["Nrash"];

   myObjArray[0, 0] = "firm_name";
   myObjArray[1, 0] = "date_doc";
   myObjArray[2, 0] = "mat_otv";
   myObjArray[3, 0] = "prikaz";
   myObjArray[4, 0] = "sost";
   myObjArray[5, 0] = "beg_end";
   myObjArray[6, 0] = "filial";
   myObjArray[7, 0] = "num_izl";
   myObjArray[8, 0] = "num_ned";

   object varMyObjArray = myObjArray;
   
    object[,] varData = new object[dS_HistPereuch1.dt_PereuchHistSpec.Columns.Count, dS_HistPereuch1.dt_PereuchHistSpec.Rows.Count+1];
    varData[0,0] = "ar"; 
    varData[1,0] = "name"; 
    varData[2,0] = "b_k";
    varData[3,0] = "b_s"; 
    varData[4,0] = "f_k";
    varData[5,0] = "f_s"; 
    varData[6,0] = "i_k";
    varData[7,0] = "i_s";
    varData[8,0] = "n_k";
    varData[9,0] = "n_s";
   for (Int32 i=0;i<=dS_HistPereuch1.dt_PereuchHistSpec.Rows.Count-1;i++)
   {
    varData[0,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["Art"];
    varData[1,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["Name"];
    varData[2,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["BKol"];
    varData[3,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["SBkol"];
    varData[4,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["FKol"];
    varData[5,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["SFkol"];
    varData[6,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["Ikol"];
    varData[7,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["SIkol"];
    varData[8,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["NKol"];
    varData[9,i+1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["SNkol"];
   }
   
   object varDataArray = varData;
    string repFullPath = StartClass.GlSet.PathDBMDB + "raspiska_per.xls";
    f.TemplateWorkbookFullName = repFullPath;
    f.TemplateSheetName = "rasp";
    MyReport.Flags = TrXLRep.trxlrepFlags.trxlrepFlagNamesInFirstColumn;
    MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
  }
         
         */




        private Payment payment;

       
        #endregion var

        public ImportExcelForm()
        {
            InitializeComponent();
            payment = new Payment();
            LoadDate();
        }


        private void LoadDate()
        {
            dateEditFrom.EditValue = new DateTime(DateTime.Now.Year, 1, 1);
            dateEditTo.EditValue = DateTime.Now;

            payment.LoadDataForNewPayment(); //загрузка данных для комбобоксов
            FillComboBox();
        }

        private void FillComboBox()
        {
            if (payment.comboBoxItemsZaim != null && payment.comboBoxItemsZaim.Count != 0)
            {

                comboBoxEdit_zaim.Properties.Items.Clear();
              
                foreach (ComboBoxItems item in payment.comboBoxItemsZaim)
                {
                    comboBoxEdit_zaim.Properties.Items.Add(item.Name);
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

        private bool ValidateAndSaveData()
        {
            if (comboBoxEdit_zaim.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран займ!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxEdit_zaim.Focus();
                return false;
            }



            if (dateEditFrom.EditValue == null)
            {
                MessageBox.Show("Не выбрана дата!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEditFrom.Focus();
                return false;
            }

            if (dateEditTo.EditValue == null)
            {
                MessageBox.Show("Не выбрана дата!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEditTo.Focus();
                return false;
            }
            
            return true;
        }

      

        private void barButtonItem_1A_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!ValidateAndSaveData())
                return;
            payment.PZaimID = payment.comboBoxItemsZaim[comboBoxEdit_zaim.SelectedIndex].Id;
            List<object> result;
            int countFields = 39;

            result = payment.GetReport_1a(Convert.ToDateTime(dateEditFrom.EditValue), Convert.ToDateTime(dateEditTo.EditValue));

            if (result == null || result.Count == 0)
            {
                MessageBox.Show("Нет данных");
                return;
            }


            TrXLRep.TrXLReport MyReport;
            TrXLRep.trxlrepFilesStruct f = new TrXLRep.trxlrepFilesStruct();
            MyReport = new TrXLRep.TrXLReport();

            #region comments
            //DataRow dr =
            // dS_HistPereuch1.dt_PereuchHistZagl.Select("IdPereuch=" + GV_ZagPereuch.GetFocusedRowCellValue("IdPereuch").ToString())[0];
            //object[,] myObjArray = new object[9, 2];
            //myObjArray[0, 1] = dr["firm"];
            //myObjArray[1, 1] = dr["DatePereuch"]; //DataFormats.Format(DTP_date, "Short Date")
            //myObjArray[2, 1] = "Материально-ответственое лицо: " + dr["otvperson"];//& tb_mat_otv
            //myObjArray[3, 1] = "На основании приказа №: " + dr["prikaz"] + " от " + dr["dateprikaz"] +
            //                      " произведено снятие фактических остатков ТМЦ.";
            //myObjArray[4, 1] = "По состоянию на " + dr["posost"]; //& DataFormats.Format(DTP_sost, "Short Date")
            //myObjArray[5, 1] = "Инвентаризация начата " + dr["dbeg"] + " окончена " + dr["Dend"];
            //myObjArray[6, 1] = dr["IdFilial"];//lb_filials.Column(1)
            //myObjArray[7, 1] = dr["Nprih"];
            //myObjArray[8, 1] = dr["Nrash"];

            //myObjArray[0, 0] = "firm_name";
            //myObjArray[1, 0] = "date_doc";
            //myObjArray[2, 0] = "mat_otv";
            //myObjArray[3, 0] = "prikaz";
            //myObjArray[4, 0] = "sost";
            //myObjArray[5, 0] = "beg_end";
            //myObjArray[6, 0] = "filial";
            //myObjArray[7, 0] = "num_izl";
            //myObjArray[8, 0] = "num_ned";

            //object varMyObjArray = myObjArray;

//            Dim varData(3, 2) As Variant 
//Dim f As trxlrepFilesStruct

//varData(0, 0) = "ID" 
//varData(1, 0) = "First Name"
//varData(2, 0) = "Last Name"

//varData(0, 1) = "1" 
//varData(1, 1) = "John"
//varData(2, 1) = "Smith"
//varData(0, 2) = "2" 
//varData(1, 2) = "Helen"
//varData(2, 2) = "Hunt"
//varData(0, 3) = "3" 
//varData(1, 3) = "Alex"
            //varData(2, 3) = "Johrdan"

            //for (Int32 i = 0; i <= dS_HistPereuch1.dt_PereuchHistSpec.Rows.Count - 1; i++)
            //{
            //    varData[0, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["Art"];
            //    varData[1, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["Name"];
            //    varData[2, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["BKol"];
            //    varData[3, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["SBkol"];
            //    varData[4, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["FKol"];
            //    varData[5, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["SFkol"];
            //    varData[6, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["Ikol"];
            //    varData[7, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["SIkol"];
            //    varData[8, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["NKol"];
            //    varData[9, i + 1] = dS_HistPereuch1.dt_PereuchHistSpec.Rows[i]["SNkol"];
            //}
            #endregion comments

            object[,] varData = new object[countFields, 2];
            varData[0, 0] = "zaim";
            varData[1, 0] = "pba";
            varData[2, 0] = "ibrd";
            varData[3, 0] = "gf";
            varData[4, 0] = "gf2";
            varData[5, 0] = "gf3";
            varData[6, 0] = "dp";
            varData[7, 0] = "dp1";
            varData[8, 0] = "dp2";
            varData[9, 0] = "sa";
            varData[10, 0] = "sa1";
            varData[11, 0] = "sa2";
            varData[12, 0] = "w";
            varData[13, 0] = "w1";
            varData[14, 0] = "w2";
            varData[15, 0] = "g";
            varData[16, 0] = "g1";
            varData[17, 0] = "g2";
            varData[18, 0] = "cst";
            varData[19, 0] = "cst1";
            varData[20, 0] = "cst2";
            varData[21, 0] = "ww";
            varData[22, 0] = "ww1";
            varData[23, 0] = "ww2";
            varData[24, 0] = "gg";
            varData[25, 0] = "gg1";
            varData[26, 0] = "gg2";
            varData[27, 0] = "cc";
            varData[28, 0] = "cc1";
            varData[29, 0] = "cc2";
            varData[30, 0] = "t";
            varData[31, 0] = "t1";
            varData[32, 0] = "t2";
            varData[33, 0] = "e";
            varData[34, 0] = "e1";
            varData[35, 0] = "e2";
            varData[36, 0] = "o";
            varData[37, 0] = "o1";
            varData[38, 0] = "o2";

            if (result.Count != countFields)
                MessageBox.Show("Error не хватает полей для данных!");

            for (int i = 0; i < result.Count; i++)
            {
                varData[i, 1] = result[i];
            }

            #region for test

            //varData[0, 1] = "1";
            //varData[1, 1] = "2";
            //varData[2, 1] = "3";
            //varData[3, 1] = "4";
            //varData[4, 1] = "5";
            //varData[5, 1] = "6";
            //varData[6, 1] = "7";
            //varData[7, 1] = "8";
            //varData[8, 1] = "9";
            //varData[9, 1] = "10";
            //varData[10, 1] = "11";
            //varData[11, 1] = "12";
            //varData[12, 1] = "13";
            //varData[13, 1] = "14";
            //varData[14, 1] = "15";
            //varData[15, 1] = "16";
            //varData[16, 1] = "17";
            //varData[17, 1] = "18";
            //varData[18, 1] = "19";
            //varData[19, 1] = "20";
            //varData[20, 1] = "21";
            //varData[21, 1] = "22";
            //varData[22, 1] = "23";
            //varData[23, 1] = "24";
            //varData[24, 1] = "25";
            //varData[25, 1] = "26";
            //varData[26, 1] = "27";
            //varData[27, 1] = "28";
            //varData[28, 1] = "29";
            //varData[29, 1] = "30";
            //varData[30, 1] = "31";
            //varData[31, 1] = "32";
            //varData[32, 1] = "33";
            //varData[33, 1] = "34";
            //varData[34, 1] = "35";
            //varData[35, 1] = "36";
            //varData[36, 1] = "37";
            //varData[37, 1] = "38";

            #endregion for test

            object varDataArray = varData;
            object varMyObjArray = null;//varData;
            string rep_file_name;
            //русский - ангийский
            rep_file_name = radioButton_en.Checked ? "Report1a" : "Report1ar";
            string repFullPath = String.Format("{0}\\Template\\{1}.xls", Environment.CurrentDirectory, rep_file_name);//Payment// Report1a.xls
            f.TemplateWorkbookFullName = repFullPath;
            f.TemplateSheetName = "Template"; // название листа

            try
            {
                MyReport.Flags = TrXLRep.trxlrepFlags.trxlrepFlagNamesInFirstColumn;
                //MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
                MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private void barButtonItem_report_2a_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!ValidateAndSaveData())
                return;
            payment.PZaimID = payment.comboBoxItemsZaim[comboBoxEdit_zaim.SelectedIndex].Id;
            List<long> result;
            int countFields = 2;

            result = payment.GetReport_2a(Convert.ToDateTime(dateEditFrom.EditValue), Convert.ToDateTime(dateEditTo.EditValue));

            if (result == null || result.Count == 0)
            {
                MessageBox.Show("Нет данных");
                return;
            }




            TrXLRep.TrXLReport MyReport;
            TrXLRep.trxlrepFilesStruct f = new TrXLRep.trxlrepFilesStruct();
            MyReport = new TrXLRep.TrXLReport();

            object[,] varData = new object[countFields, 2];



            //---------------------------------------------
            varData[0, 0] = "pba";
            varData[1, 0] = "ibrd";
            //varData[2, 0] = "gf";
            //varData[3, 0] = "gf2";
            //varData[4, 0] = "gf3";
            //varData[5, 0] = "dp";
            //varData[6, 0] = "dp1";
            //varData[7, 0] = "dp2";
            //varData[8, 0] = "sa";
            //varData[9, 0] = "sa1";
            //varData[10, 0] = "sa2";
            //varData[11, 0] = "w";
            //varData[12, 0] = "w1";
            //varData[13, 0] = "w2";
            //varData[14, 0] = "g";
            //varData[15, 0] = "g1";
            //varData[16, 0] = "g2";
            //varData[17, 0] = "cst";
            //varData[18, 0] = "cst1";
            //varData[19, 0] = "cst2";
            //varData[20, 0] = "ww";
            //varData[21, 0] = "ww1";
            //varData[22, 0] = "ww2";
            //varData[23, 0] = "gg";
            //varData[24, 0] = "gg1";
            //varData[25, 0] = "gg2";
            //varData[26, 0] = "cc";
            //varData[27, 0] = "cc1";
            //varData[28, 0] = "cc2";
            //varData[29, 0] = "t";
            //varData[30, 0] = "t1";
            //varData[31, 0] = "t2";
            //varData[32, 0] = "e";
            //varData[33, 0] = "e1";
            //varData[34, 0] = "e2";
            //varData[35, 0] = "o";
            //varData[36, 0] = "o1";
            //varData[37, 0] = "o2";

            if (result.Count != countFields)
                MessageBox.Show("Error не хватает полей для данных!");

            for (int i = 0; i < result.Count; i++)
            {
                varData[i, 1] = result[i];
            }



            object varDataArray = varData;
            object varMyObjArray = null;//varData;
            string rep_file_name;
            //русский - ангийский
            rep_file_name = radioButton_en.Checked ? "report2a" : "report2ar";
            string repFullPath = String.Format("{0}\\Template\\{1}.xls", Environment.CurrentDirectory, rep_file_name);//Payment// Report1a.xls
            f.TemplateWorkbookFullName = repFullPath;
            f.TemplateSheetName = "Template"; // название листа

            try
            {
                MyReport.Flags = TrXLRep.trxlrepFlags.trxlrepFlagNamesInFirstColumn;
                //MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
                MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }



            //TrXLRep.TrXLReport MyReport;
            //TrXLRep.trxlrepFilesStruct f = new TrXLRep.trxlrepFilesStruct();
            //MyReport = new TrXLRep.TrXLReport();

            //object[,] varData = new object[countFields, 2];
        


            ////---------------------------------------------
            //varData[0, 0] = "pba";
            //varData[1, 0] = "ibrd";
            //varData[2, 0] = "gf";
            //varData[3, 0] = "gf2";
            //varData[4, 0] = "gf3";
            //varData[5, 0] = "dp";
            //varData[6, 0] = "dp1";
            //varData[7, 0] = "dp2";
            //varData[8, 0] = "sa";
            //varData[9, 0] = "sa1";
            //varData[10, 0] = "sa2";
            //varData[11, 0] = "w";
            //varData[12, 0] = "w1";
            //varData[13, 0] = "w2";
            //varData[14, 0] = "g";
            //varData[15, 0] = "g1";
            //varData[16, 0] = "g2";
            //varData[17, 0] = "cst";
            //varData[18, 0] = "cst1";
            //varData[19, 0] = "cst2";
            //varData[20, 0] = "ww";
            //varData[21, 0] = "ww1";
            //varData[22, 0] = "ww2";
            //varData[23, 0] = "gg";
            //varData[24, 0] = "gg1";
            //varData[25, 0] = "gg2";
            //varData[26, 0] = "cc";
            //varData[27, 0] = "cc1";
            //varData[28, 0] = "cc2";
            //varData[29, 0] = "t";
            //varData[30, 0] = "t1";
            //varData[31, 0] = "t2";
            //varData[32, 0] = "e";
            //varData[33, 0] = "e1";
            //varData[34, 0] = "e2";
            //varData[35, 0] = "o";
            //varData[36, 0] = "o1";
            //varData[37, 0] = "o2";

            //if (result.Count != countFields)
            //    MessageBox.Show("Error не хватает полей для данных!");

            //for (int i = 0; i < result.Count; i++)
            //{
            //    varData[i, 1] = result[i];
            //}

       

            //object varDataArray = varData;
            //object varMyObjArray = null;//varData;
            //string rep_file_name;
            ////русский - ангийский
            //rep_file_name = radioButton_en.Checked ? "report2a" : "report2ar";
            //string repFullPath = String.Format("{0}\\Template\\{1}.xls", Environment.CurrentDirectory, rep_file_name);//Payment// Report1a.xls
            //f.TemplateWorkbookFullName = repFullPath;
            //f.TemplateSheetName = "Template"; // название листа

            //try
            //{
            //    MyReport.Flags = TrXLRep.trxlrepFlags.trxlrepFlagNamesInFirstColumn;
            //    //MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
            //    MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);

            //}

        }

        private void barButtonItem_report_1b_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!ValidateAndSaveData())
                return;
            payment.PZaimID = payment.comboBoxItemsZaim[comboBoxEdit_zaim.SelectedIndex].Id;
            List<long> result;
            int countFields = 38;

            result = payment.GetReport_1b(Convert.ToDateTime(dateEditFrom.EditValue), Convert.ToDateTime(dateEditTo.EditValue));

            if (result == null || result.Count == 0)
            {
                MessageBox.Show("Нет данных");
                return;
            }


            TrXLRep.TrXLReport MyReport;
            TrXLRep.trxlrepFilesStruct f = new TrXLRep.trxlrepFilesStruct();
            MyReport = new TrXLRep.TrXLReport();

            object[,] varData = new object[countFields, 2];
            varData[0, 0] = "pba";
            varData[1, 0] = "ibrd";
            varData[2, 0] = "gf";
            varData[3, 0] = "gf2";
            varData[4, 0] = "gf3";
            varData[5, 0] = "dp";
            varData[6, 0] = "dp1";
            varData[7, 0] = "dp2";
            varData[8, 0] = "sa";
            varData[9, 0] = "sa1";
            varData[10, 0] = "sa2";
            varData[11, 0] = "w";
            varData[12, 0] = "w1";
            varData[13, 0] = "w2";
            varData[14, 0] = "g";
            varData[15, 0] = "g1";
            varData[16, 0] = "g2";
            varData[17, 0] = "cst";
            varData[18, 0] = "cst1";
            varData[19, 0] = "cst2";
            varData[20, 0] = "ww";
            varData[21, 0] = "ww1";
            varData[22, 0] = "ww2";
            varData[23, 0] = "gg";
            varData[24, 0] = "gg1";
            varData[25, 0] = "gg2";
            varData[26, 0] = "cc";
            varData[27, 0] = "cc1";
            varData[28, 0] = "cc2";
            varData[29, 0] = "t";
            varData[30, 0] = "t1";
            varData[31, 0] = "t2";
            varData[32, 0] = "e";
            varData[33, 0] = "e1";
            varData[34, 0] = "e2";
            varData[35, 0] = "o";
            varData[36, 0] = "o1";
            varData[37, 0] = "o2";

            if (result.Count != countFields)
                MessageBox.Show("Error не хватает полей для данных!");

            for (int i = 0; i < result.Count; i++)
            {
                varData[i, 1] = result[i];
            }



            object varDataArray = varData;
            object varMyObjArray = null;//varData;
            string rep_file_name;
            //русский - ангийский
            rep_file_name = radioButton_en.Checked ? "Report1b" : "Report1br";
            string repFullPath = String.Format("{0}\\Template\\{1}.xls", Environment.CurrentDirectory, rep_file_name);//Payment// Report1a.xls
            f.TemplateWorkbookFullName = repFullPath;
            f.TemplateSheetName = "Template"; // название листа

            try
            {
                MyReport.Flags = TrXLRep.trxlrepFlags.trxlrepFlagNamesInFirstColumn;
                //MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
                MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void barButtonItem_report_2b_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!ValidateAndSaveData())
                return;
            payment.PZaimID = payment.comboBoxItemsZaim[comboBoxEdit_zaim.SelectedIndex].Id;
            List<long> result;
            int countFields = 38;

            result = payment.GetReport_2b(Convert.ToDateTime(dateEditFrom.EditValue), Convert.ToDateTime(dateEditTo.EditValue));

            if (result == null || result.Count == 0)
            {
                MessageBox.Show("Нет данных");
                return;
            }


            TrXLRep.TrXLReport MyReport;
            TrXLRep.trxlrepFilesStruct f = new TrXLRep.trxlrepFilesStruct();
            MyReport = new TrXLRep.TrXLReport();

            object[,] varData = new object[countFields, 2];
            varData[0, 0] = "pba";
            varData[1, 0] = "ibrd";
            varData[2, 0] = "gf";
            varData[3, 0] = "gf2";
            varData[4, 0] = "gf3";
            varData[5, 0] = "dp";
            varData[6, 0] = "dp1";
            varData[7, 0] = "dp2";
            varData[8, 0] = "sa";
            varData[9, 0] = "sa1";
            varData[10, 0] = "sa2";
            varData[11, 0] = "w";
            varData[12, 0] = "w1";
            varData[13, 0] = "w2";
            varData[14, 0] = "g";
            varData[15, 0] = "g1";
            varData[16, 0] = "g2";
            varData[17, 0] = "cst";
            varData[18, 0] = "cst1";
            varData[19, 0] = "cst2";
            varData[20, 0] = "ww";
            varData[21, 0] = "ww1";
            varData[22, 0] = "ww2";
            varData[23, 0] = "gg";
            varData[24, 0] = "gg1";
            varData[25, 0] = "gg2";
            varData[26, 0] = "cc";
            varData[27, 0] = "cc1";
            varData[28, 0] = "cc2";
            varData[29, 0] = "t";
            varData[30, 0] = "t1";
            varData[31, 0] = "t2";
            varData[32, 0] = "e";
            varData[33, 0] = "e1";
            varData[34, 0] = "e2";
            varData[35, 0] = "o";
            varData[36, 0] = "o1";
            varData[37, 0] = "o2";

            if (result.Count != countFields)
                MessageBox.Show("Error не хватает полей для данных!");

            for (int i = 0; i < result.Count; i++)
            {
                varData[i, 1] = result[i];
            }



            object varDataArray = varData;
            object varMyObjArray = null;//varData;
            string rep_file_name;
            //русский - ангийский
            rep_file_name = radioButton_en.Checked ? "report2b" : "report2br";
            string repFullPath = String.Format("{0}\\Template\\{1}.xls", Environment.CurrentDirectory, rep_file_name);//Payment// Report1a.xls
            f.TemplateWorkbookFullName = repFullPath;
            f.TemplateSheetName = "Template"; // название листа

            try
            {
                MyReport.Flags = TrXLRep.trxlrepFlags.trxlrepFlagNamesInFirstColumn;
                //MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
                MyReport.CreateReport(ref varDataArray, ref f, ref varMyObjArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        /*      ////http://www.sql.ru/forum/actualthread.aspx?tid=620401
          public const string UID = "Excel.Application";
          object oExcel = null;
          object WorkBooks, WorkBook, WorkSheets, WorkSheet, Range, Interior;

          //КОНСТРУКТОР КЛАССА
          public Excel()
          {
              oExcel = Activator.CreateInstance(Type.GetTypeFromProgID(UID));
          }

          //ВИДИМОСТЬ EXCEL - СВОЙСТВО КЛАССА
          public bool Visible
          {
              set
              {
                  if (false == value)
                      oExcel.GetType().InvokeMember("Visible", BindingFlags.SetProperty,
                          null, oExcel, new object[] { false });

                  else
                      oExcel.GetType().InvokeMember("Visible", BindingFlags.SetProperty,
                          null, oExcel, new object[] { true });
              }
          }
        

          //ОТКРЫТЬ ДОКУМЕНТ
          public void OpenDocument(string name)
          {
              WorkBooks = oExcel.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, oExcel, null);
              WorkBook = WorkBooks.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null, WorkBooks, new object[] { name, true });
              WorkSheets = WorkBook.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, WorkBook, null);
              WorkSheet = WorkSheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, WorkSheets, new object[] { 1 });
              // Range = WorkSheet.GetType().InvokeMember("Range",BindingFlags.GetProperty,null,WorkSheet,new object[1] { "A1" });
          }
        
          // НОВЫЙ ДОКУМЕНТ
          public void NewDocument()
          {
              WorkBooks = oExcel.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, oExcel, null);
              WorkBook = WorkBooks.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, WorkBooks, null);
              WorkSheets = WorkBook.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, WorkBook, null);
              WorkSheet = WorkSheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, WorkSheets, new object[] { 1 });
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, WorkSheet, new object[1] { "A1" });
          }
          //ЗАКРЫТЬ ДОКУМЕНТ
          public void CloseDocument()
          {
              WorkBook.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, WorkBook, new object[] { true });
          }
          //СОХРАНИТЬ ДОКУМЕНТ
          public void SaveDocument(string name)
          {
              if (File.Exists(name))
                  WorkBook.GetType().InvokeMember("Save", BindingFlags.InvokeMethod, null,
                      WorkBook, null);
              else
                  WorkBook.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null,
                      WorkBook, new object[] { name });
          }

          // ЗАПИСАТЬ ЗНАЧЕНИЕ В ЯЧЕЙКУ
          public void SetValue(string range, string value)
          {
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                  null, WorkSheet, new object[] { range });
              Range.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, Range, new object[] { value });
          }

          //ОБЪЕДЕНИТЬ ЯЧЕЙКИ 
          // Alignment - ВЫРАВНИВАНИЕ В ОБЪЕДИНЕННЫХ ЯЧЕЙКАХ
          public void SetMerge(string range, int Alignment)
          {
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                  null, WorkSheet, new object[] { range });
              object[] args = new object[] { Alignment };
              Range.GetType().InvokeMember("MergeCells", BindingFlags.SetProperty, null, Range, new object[] { true });
              Range.GetType().InvokeMember("HorizontalAlignment", BindingFlags.SetProperty, null, Range, args);
          }

          //УСТАНОВИТЬ ОРИЕНТАЦИЮ СТРАНИЦЫ 
          //1 - КНИЖНЫЙ
          //2 - АЛЬБОМНЫЙ
          public void SetOrientation(int Orientation)
          {
              //Range.Interior.ColorIndex
              object PageSetup = WorkSheet.GetType().InvokeMember("PageSetup", BindingFlags.GetProperty,
                  null, WorkSheet, null);

              PageSetup.GetType().InvokeMember("Orientation", BindingFlags.SetProperty,
                  null, PageSetup, new object[] { Orientation });
          }

          //УСТАНОВИТЬ ШИРИНУ СТОЛБЦОВ
          public void SetColumnWidth(string range, double Width)
          {
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                  null, WorkSheet, new object[] { range });
              object[] args = new object[] { Width };
              Range.GetType().InvokeMember("ColumnWidth", BindingFlags.SetProperty, null, Range, args);
          }
        
          //УСТАНОВИТЬ ВЫСОТУ СТРОК
          public void SetRowHeight(string range, double Height)
          {
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                  null, WorkSheet, new object[] { range });
              object[] args = new object[] { Height };
              Range.GetType().InvokeMember("RowHeight", BindingFlags.SetProperty, null, Range, args);
          }

          //УСТАНОВИТЬ ВИД РАМКИ ВОКРУГ ЯЧЕЙКИ
          public void SetBorderStyle(string range, int Style)
          {
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                  null, WorkSheet, new object[] { range });
              object[] args = new object[] { 1 };
              object[] args1 = new object[] { 1 };
              object Borders = Range.GetType().InvokeMember("Borders", BindingFlags.GetProperty, null, Range, null);
              Borders = Range.GetType().InvokeMember("LineStyle", BindingFlags.SetProperty, null, Borders, args);
          } 

          //ЧТЕНИЕ ДАННЫХ ИЗ ВЫБРАННОЙ ЯЧЕЙКИ
          public string GetValue(string range)
          {
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                  null, WorkSheet, new object[] { range });
              return Range.GetType().InvokeMember("Value", BindingFlags.GetProperty,
                  null, Range, null).ToString();
          }

          //УСТАНОВИТЬ ВЫРАВНИВАНИЕ В ЯЧЕЙКЕ ПО ВЕРТИКАЛИ
          public void SetVerticalAlignment(string range, int Alignment)
          {
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                  null, WorkSheet, new object[] { range });
              object[] args = new object[] { Alignment };
              Range.GetType().InvokeMember("VerticalAlignment", BindingFlags.SetProperty, null, Range, args);
          }

          //УСТАНОВИТЬ ВЫРАВНИВАНИЕ В ЯЧЕЙКЕ ПО ГОРИЗОНТАЛИ
          public void SetHorisontalAlignment(string range, int Alignment)
          {
              Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty,
                  null, WorkSheet, new object[] { range });
              object[] args = new object[] { Alignment };
              Range.GetType().InvokeMember("HorizontalAlignment", BindingFlags.SetProperty, null, Range, args);
          }
         */



        //http://qaskill.com/c-rabota-s-excel.html
        /*
         * using Microsoft.Office.Interop.Excel;
using System.IO;
class ExcelValuesWriter
{
    public void WorkWithExcel()
    {
        Application xlApp = new ApplicationClass();
        Workbook xlWorkBook;
        Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        string filePath = "c:\\InputValuesExcel.xls";
        //Если не существует файла то создать его
        bool isFileExist;
        FileInfo fInfo = new FileInfo(filePath);
        if (!fInfo.Exists)
        {
            xlWorkBook = xlApp.Workbooks.Add(misValue);//Добавить новый book в файл
            isFileExist = false;
        }
        else
        {
            //Открыть существующий файл
            xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", true, 
                XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            isFileExist = true;
        }
        //Открытие первой вкладки
        xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //Запись значения в самую первую ячейку
        xlWorkSheet.Cells[1, 1] = "Client Name";
 
        //Получить количество используемых столбцов
        int columnsCount = xlWorkSheet.UsedRange.Columns.Count;
        //Получить количество используемых строк
        int usedRowsCount = xlWorkSheet.UsedRange.Rows.Count;
        //Проверить значение последней используемой ячейки в последней исползуеймой строке и 
        //последнем используемом столбце. Если значение этой ячейки равно "Привет", изменить его на "Пока".
        if ((xlWorkSheet.UsedRange.Cells[usedRowsCount, columnsCount] as Range).Value2 != null)
        {
            if ((xlWorkSheet.UsedRange.Cells[usedRowsCount, columnsCount] as Range).Value2.ToString() == "Привет")
                (xlWorkSheet.UsedRange.Cells[usedRowsCount, columnsCount] as Range).Value2 = "Пока";
        }
        //Если файл существовал, просто сохранить его по умолчанию. Иначе сохранить в указанную директорию
        if (isFileExist)
        {
            xlWorkBook.Save();
        }
        else
        {
            xlWorkBook.SaveAs(filePath, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, 
                misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        }
        xlWorkBook.Close(true, misValue, misValue);
        xlApp.Quit();
        //Освобождение ресурсов
        releaseObject(xlWorkSheet);
        releaseObject(xlWorkBook);
        releaseObject(xlApp);
    }
    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            Console.Write("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
        }
    }
}
         */



        #region Хранимые процедуры
        /*
         * ALTER PROCEDURE [dbo].[Radialnaja_GetQntForUpdate]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     [1_расх_спец].id, [1_расх_спец].Арт AS 'Артикул', [1_расх_спец].[кол-во],
CASE WHEN [1_расх_спец].main_quant > 0 THEN 0 ELSE 1 END AS main_quant_zero
FROM         [1_расх_спец] INNER JOIN
                      [2_расх_шапки] ON [1_расх_спец].[№расх] = [2_расх_шапки].[№расх]
WHERE     ([2_расх_шапки].код_отгрузки = 480)
ORDER BY 'Артикул'
END
         * 
         ********************************************************************************* 
         * 
         * 
ALTER PROCEDURE [dbo].[Radialnaja_GetQntForLook]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     [1_расх_спец].Арт AS 'Артикул', [Номенклатура с ценниками].Наименование, SUM([1_расх_спец].[кол-во]) AS [кол-во]
FROM         [1_расх_спец] INNER JOIN
                      [2_расх_шапки] ON [1_расх_спец].[№расх] = [2_расх_шапки].[№расх] INNER JOIN
                      [Номенклатура с ценниками] ON [1_расх_спец].Арт = [Номенклатура с ценниками].Артикул
WHERE     ([2_расх_шапки].код_отгрузки = 480)
GROUP BY [1_расх_спец].Арт, [Номенклатура с ценниками].Наименование
ORDER BY 'Артикул'
END
         * ***************************************************************
ALTER PROCEDURE [dbo].[Radialnaja_CorrectRest]	
AS
BEGIN
	DECLARE 
		@ra0 INT, -- кол-во исправлений в отгрузках на филиалы(в род. записях)
		@ra1 INT, -- кол-во несоответствий(по ключам)
		@ra2 INT, -- кол-во исправлений(по ключам)
		@ra3 INT, -- кол-во записей в сводной таблице по ключам
		@ra4 INT, -- кол-во записей в сводной таблице по артикулам
		@ra5 INT, -- кол-во отрицательных безнальных оперативных остатков
		@ra6 INT, -- кол-во отрицательных нальных оперативных остатков
		@ra7 INT, -- кол-во исправлений в безнальных оперативных остатках
		@ra8 INT; -- кол-во исправлений в нальных оперативных остатках

	SET @ra0 = 0
	SET @ra1 = 0
	SET @ra2 = 0
	SET @ra3 = 0
	SET @ra4 = 0
	SET @ra5 = 0
	SET @ra6 = 0
	SET @ra7 = 0
	SET @ra8 = 0


	SELECT J.A, J.B, J.C, J.D, J.E, J.F
	INTO #REST_FULL_VIP 
	FROM 
		(
			SELECT [Артикул] AS A, -SUM([кол-во]) AS B, 0 AS C, 0 AS D, 0 AS E, 0 AS F
			FROM [расходы_спец] INNER JOIN [расходы_шапки] ON [расходы_шапки].[№] = [расходы_спец].[№]
			inner join (SELECT     [1_расх_спец].Арт
						FROM         [2_расх_шапки] INNER JOIN
						[1_расх_спец] ON [2_расх_шапки].[№расх] = [1_расх_спец].[№расх]
						WHERE     ([2_расх_шапки].код_отгрузки = 480)
						GROUP BY [1_расх_спец].Арт) rad 
			ON [расходы_спец].Артикул = rad.Арт
			WHERE ([расходы_шапки].[Переключатель] = 0) 
			GROUP BY [Артикул]
			
			UNION ALL
			
			SELECT [1_прих_спец].[Арт] AS A, 0 AS B, SUM([кол-во]) AS C, 0 AS D, 0 AS E, 0 AS F
			FROM [1_прих_спец] INNER JOIN [1_прих_шапки] ON [1_прих_шапки].[№прих] = [1_прих_спец].[№прих] 
			inner join (SELECT     [1_расх_спец].Арт
						FROM         [2_расх_шапки] INNER JOIN
						[1_расх_спец] ON [2_расх_шапки].[№расх] = [1_расх_спец].[№расх]
						WHERE     ([2_расх_шапки].код_отгрузки = 480)
						GROUP BY [1_расх_спец].Арт) rad 
			ON [1_прих_спец].Арт = rad.Арт
			WHERE ([Оприходованио] <> 0) 
			GROUP BY [1_прих_спец].[Арт]
			
			UNION ALL

			SELECT [1_расх_спец].[Арт] AS A, 0 AS B, 0 AS C, SUM([кол-во]) AS D, 0 AS E, 0 AS F
			FROM [1_расх_спец] 
			inner join (SELECT     [1_расх_спец].Арт
						FROM         [2_расх_шапки] INNER JOIN
						[1_расх_спец] ON [2_расх_шапки].[№расх] = [1_расх_спец].[№расх]
						WHERE     ([2_расх_шапки].код_отгрузки = 480)
						GROUP BY [1_расх_спец].Арт) rad 
			ON [1_расх_спец].Арт = rad.Арт
			GROUP BY [1_расх_спец].[Арт]
			
			UNION ALL

			SELECT [1_расх_спец].[Арт] AS A, 0 AS B, 0 AS C, 0 AS D, SUM([кол-во]) AS E, 0 AS F
			FROM [1_расх_спец] INNER JOIN [2_расх_шапки] ON [2_расх_шапки].[№расх]=[1_расх_спец].[№расх] 
			inner join (SELECT     [1_расх_спец].Арт
						FROM         [2_расх_шапки] INNER JOIN
						[1_расх_спец] ON [2_расх_шапки].[№расх] = [1_расх_спец].[№расх]
						WHERE     ([2_расх_шапки].код_отгрузки = 480)
						GROUP BY [1_расх_спец].Арт) rad 
			ON [1_расх_спец].Арт = rad.Арт
			WHERE ([2_расх_шапки].поставлено <> 0) 
			GROUP BY [1_расх_спец].[Арт]
			
			UNION ALL
			
			SELECT [1_прих_спец].[Арт] AS A, 0 AS B, 0 AS C, 0 AS D, 0 AS E, SUM(ost) AS F
			FROM [1_прих_спец] INNER JOIN [1_прих_шапки] ON [1_прих_шапки].[№прих] = [1_прих_спец].[№прих] 
			inner join (SELECT     [1_расх_спец].Арт
						FROM         [2_расх_шапки] INNER JOIN
						[1_расх_спец] ON [2_расх_шапки].[№расх] = [1_расх_спец].[№расх]
						WHERE     ([2_расх_шапки].код_отгрузки = 480)
						GROUP BY [1_расх_спец].Арт) rad 
			ON [1_прих_спец].Арт = rad.Арт
			WHERE ([Оприходованио] <> 0) 
			GROUP BY [1_прих_спец].[Арт]
		) J

	SELECT 
		A AS A,											-- артикул
		ISNULL(SUM(B),0) AS B,							-- кол-во снятого с резерва нала
		ISNULL(SUM(C),0) AS C,							-- кол-во оприходованных приходов безнала
		ISNULL(SUM(D),0) AS D,							-- кол-во всех расходов безнала
		ISNULL(SUM(E),0) AS E,							-- кол-во товарных расходов безнала
		ISNULL(SUM(F),0) AS F,							-- кол-во оприходованных остатков безнала
		0 AS VP,
		0 AS MG
	INTO #REST_VIP 
	FROM #REST_FULL_VIP 
	GROUP BY A
	
	SET @ra4 = @@ROWCOUNT
	
	UPDATE #REST_VIP
	SET 
		VP = B - E,										-- остатки нала
		MG = F											-- остатки безнала
		
	--но правильно:
	--UPDATE #REST_VIP
	--SET 
	--	VP = B - E,										-- остатки нала
	--	MG = C - D										-- остатки безнала
		
		
	UPDATE #REST_VIP
	SET MG = 0
	WHERE MG < 0

	SET @ra5 = @@ROWCOUNT

	UPDATE [склад_mag]
	SET Количество = #REST_VIP.MG
	FROM #REST_VIP 
	WHERE (#REST_VIP.A = [склад_mag].[Артикул])AND(Количество <> #REST_VIP.MG)

	SET @ra7 = @@ROWCOUNT

		BEGIN
			UPDATE #REST_VIP
			SET VP = 0
			WHERE VP < 0

			SET @ra6 = @@ROWCOUNT

			UPDATE [склад_vip]
			SET Количество = #REST_VIP.VP
			FROM #REST_VIP
			WHERE (#REST_VIP.A = [склад_vip].[Артикул])AND(Количество <> #REST_VIP.VP)

			SET @ra8 = @@ROWCOUNT
		END	
--	SELECT *
--	FROM #REST_VIP
--	WHERE (#REST_VIP.VP<0) OR (#REST_VIP.MG<0) OR (#REST_VIP.B<0)
--	ORDER BY #REST_VIP.A

	DROP TABLE #REST_VIP
	DROP TABLE #REST_FULL_VIP

	SELECT 
		@ra0 AS ra0, 
		@ra1 AS ra1, 
		@ra2 AS ra2, 
		@ra3 AS ra3, 
		@ra4 AS ra4, 
		@ra5 AS ra5, 
		@ra6 AS ra6, 
		@ra7 AS ra7, 
		@ra8 AS ra8
END
         */
        #endregion

    }
}
