using StudentApp.Classes;
using StudentApp.Globle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentApp.Forms
{
    public partial class frmStudentsList : Form
    {

        private DataTable _dtAllStudent; 

        public frmStudentsList()
        {
            InitializeComponent();
        }

        private void frmStudentsList_Load(object sender, EventArgs e)
        {
            //dgvStudentList.DataSource = clsStudentList.Students.ToList();



            _dtAllStudent = clsStudentList.dtListStudent(); 
            dgvStudentList.DataSource = _dtAllStudent;
            lblRecordCount.Text = dgvStudentList.Rows.Count.ToString();

            cbFilterBy.SelectedIndex  = 0;

            if (dgvStudentList.Rows.Count > 0)
            {
                dgvStudentList.Columns[0].HeaderText = "Student ID";
                dgvStudentList.Columns[0].Width = 90;

                dgvStudentList.Columns[1].HeaderText = "FullName";
                dgvStudentList.Columns[1].Width = 160;

                dgvStudentList.Columns[2].HeaderText = "Phone";
                dgvStudentList.Columns[2].Width = 120;

                dgvStudentList.Columns[3].HeaderText = "Birth Date";
                dgvStudentList.Columns[3].Width = 130;

                dgvStudentList.Columns[4].HeaderText = "Department";
                dgvStudentList.Columns[4].Width = 160;

                dgvStudentList.Columns[5].HeaderText = "GPA";
                dgvStudentList.Columns[5].Width = 100;
            }


        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_Student frm = new frmAdd_Edit_Student();
            frm.ShowDialog();

            frmStudentsList_Load(null , null); 
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(dgvStudentList.CurrentRow.Cells[0].Value);

            frmAdd_Edit_Student frm = new frmAdd_Edit_Student(studentId);
            frm.ShowDialog();

            frmStudentsList_Load(null, null);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(dgvStudentList.CurrentRow.Cells[0].Value);

            if (MessageBox.Show("Are you sure deleted this student?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                clsStudentList.DeleteStudent(studentId);
            }
            else
            {
                MessageBox.Show("Action canceled.");     
            }

            frmStudentsList_Load(null, null);
        }

        private void addNewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_Student frm = new frmAdd_Edit_Student();
            frm.ShowDialog();

            frmStudentsList_Load(null, null);
        }


        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "ID":
                    FilterColumn = "StudentID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;


                case "GPA":
                    FilterColumn = "GPA";
                    break;


                case "Phone":
                    FilterColumn = "Phone";
                    break;


                case "Department":
                    FilterColumn = "Department";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }



            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllStudent.DefaultView.RowFilter = "";
                lblRecordCount.Text = dgvStudentList.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "StudentID" || FilterColumn == "GPA")         
                _dtAllStudent.DefaultView.RowFilter = string.Format("[{0}] = {1}" , FilterColumn  , txtFilterValue.Text.Trim());
            else
                _dtAllStudent.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());



            lblRecordCount.Text = dgvStudentList.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (cbFilterBy.Text == "None")
                txtFilterValue.Enabled = false;
            else
            {
                txtFilterValue.Enabled = true;

                txtFilterValue.Text ="";
                txtFilterValue.Focus();
            }
        }
    }
}
