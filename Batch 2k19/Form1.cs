using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing.Imaging;

namespace Batch_2k19
{

    public partial class Form1 : Form
    {
        Main main = new Main();
       
        
        static Regex validate_emailaddress = email_validation();

        public object Tables { get; private set; }

        
        private static Regex email_validation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        int ID = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

       

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.fiverr.com/muhammadabub828?up_rollout=true");
        }

       

        private void clear_form_Click(object sender, EventArgs e)
        {
            txtemail.Text = "";
            txtname.Text = "";
            txtskill.Text = "";
            txtwhatsapp.Text = "";
            textBox5.Text = "";
            txtagno.Text = "";
            txtgroupno.Text = "";
            txtproject.Text ="";
            pictureBox9.Image = null;
        }

        private void txtname_validation(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtname.Text.Trim()))
            {
                student_name.SetError(txtname, "Full Name Is Required.");
            }
            else
            {
                student_name.SetError(txtname, string.Empty);
            }
           
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            mouse_hover.SetToolTip(txtagno, "University Registration Number");
        }

        private void groupno_hover(object sender, EventArgs e)
        {
            mouse_hover.SetToolTip(txtgroupno, "Class Assigned Group no");
        }

        private void serach_hover(object sender, EventArgs e)
        {
            mouse_hover.SetToolTip(textBox5, "Serach By Name");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void email_test(object sender, CancelEventArgs e)
        {
            if (validate_emailaddress.IsMatch(txtemail.Text) != true)
            {
                MessageBox.Show("Invalid Email Address!", "E-mail Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtemail.Focus();
                return;
            }
           
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtname.Text)||string.IsNullOrWhiteSpace(txtproject.Text))
            {
                MessageBox.Show("All The compulsory fields are required. Kindly Fill out these fields.");
                return;
            }
            if (this.lblCode.Text != "-1")
            {
                string query = string.Concat("UPDATE `studentinfo` SET `student_name` = '", txtname.Text, "',`student_email` = '", txtemail.Text, "',`student_whatsapp` = '", txtwhatsapp.Text, "',`student_skill` = '", txtskill.Text, "',`student_agno` = '", txtagno.Text, "',`student_group` = '", txtgroupno.Text, "',`student_projectdetails` = '", txtproject.Text, "' WHERE `student_id` = '", this.lblCode.Text, "';");
                this.main.ExecuteQuery(query);
            }
            else
            {
                byte[] img = main.ConvertImageToBinary(this.pictureBox9.Image);
                string str = string.Concat("INSERT INTO studentinfo(student_name, student_email, student_whatsapp, student_skill, student_agno, student_group, student_projectdetails,s_image) VALUES('", txtname.Text, "', '", txtemail.Text, "', '", txtwhatsapp.Text, "', '", txtskill.Text, "',  '", txtagno.Text, "',  '", txtgroupno.Text, "',  '", txtproject.Text, "','",img,"')");
                this.main.ExecuteQuery(str);
                MessageBox.Show("Data inserted successfully");
            }
        }

        private void get_data_Click(object sender, EventArgs e)
        {

           
            
            gvList.DataSource = this.main.GetDataSet("SELECT * FROM studentinfo").Tables[0];

        }

        private void serach_Click(object sender, EventArgs e)
        {
            gvList.DataSource = this.main.SerachName("select * from studentinfo where student_name like '" + textBox5.Text + "%'").Tables[0];
        }

        private void delete_Click(object sender, EventArgs e)
        {


            if (this.gvList.SelectedRows.Count > 0)
            {
                string str = string.Concat("DELETE FROM `studentinfo` WHERE `student_id` = '", this.lblCode.Text, "';"); ;
                this.main.ExecuteQuery(str);
                MessageBox.Show("Data Deleted successfully");
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {

            if (this.gvList.SelectedRows.Count > 0)
            {
                string str = string.Concat("UPDATE studentinfo SET ");
            this.main.ExecuteQuery(str);
            MessageBox.Show("Data Updated successfully");
            }
            else
            {
                MessageBox.Show("Please Select Record to Update", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void print_pdf_Click(object sender, EventArgs e)
        {
            if (gvList.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "batch2K19Result.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Error Writing Data.","Error I/O" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(gvList.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in gvList.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in gvList.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.lblCode.Text = this.gvList.Rows[e.RowIndex].Cells["code"].Value.ToString();
            this.txtagno.Text = this.gvList.Rows[e.RowIndex].Cells["ag_no"].Value.ToString();
            this.txtemail.Text = this.gvList.Rows[e.RowIndex].Cells["s_email"].Value.ToString();
            this.txtgroupno.Text = this.gvList.Rows[e.RowIndex].Cells["s_group"].Value.ToString();
            this.txtname.Text = this.gvList.Rows[e.RowIndex].Cells["s_name"].Value.ToString();
            this.txtproject.Text = this.gvList.Rows[e.RowIndex].Cells["s_detail"].Value.ToString();
            this.txtskill.Text = this.gvList.Rows[e.RowIndex].Cells["s_skill"].Value.ToString();
            this.txtwhatsapp.Text = this.gvList.Rows[e.RowIndex].Cells["s_whatsapp"].Value.ToString();
            byte[] array = (byte[])(this.gvList.Rows[e.RowIndex].Cells["s_image"].Value);
            pictureBox9.Image = main.ConvertBinaryToImage(array);

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                this.pictureBox9.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.P)
            {
                print_pdf.PerformClick();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contactus nForm = new contactus();
            nForm.Show();
        }
    }
}
