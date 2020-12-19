using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BunifuUI_Test
{
    public partial class Calculator : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Calculator()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //From https://stackoverflow.com/a/6052679/14304544
        /// <summary>
        /// Evaluates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static double Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        string Expression = "";
        public double LastAns { get; private set; }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Expression = "";
            lblExpression.Text = "";
            lblResult.Text = "0";
        }


        private void btnEquals_Click(object sender, EventArgs e)
        {
            LastAns = Evaluate(Expression);
            lblResult.Text = LastAns.ToString();
        }
        private void btnNumeric_Click(object sender, EventArgs e)
        {
            lblResult.Text = 0.ToString();
            lblExpression.Text = (Expression += ((Control)sender).Tag.ToString());
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                lblExpression.Text = Expression = Expression.Substring(0, Expression.Length - 1);

            }
            catch (Exception)
            {

            }
        }



        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
            }
        }

        private void Calculator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

       
    }
}
