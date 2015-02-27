using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace bies
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginform = new LoginForm();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (loginform.ShowDialog() == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                //this.txtResult.Text = loginform.TextBox1.Text;
                loginform.Dispose();
                Application.Run(new FormMain());
                
            }
        }
    }
}
