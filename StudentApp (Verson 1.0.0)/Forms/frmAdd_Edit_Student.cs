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
    public partial class frmAdd_Edit_Student : Form
    {

        private clsStudent _Student; 
        private int _StudentID = -1; 

        public enum enMode {AddNew = 0 , Update = 1 };
        private enMode _Mode = enMode.AddNew;



        public frmAdd_Edit_Student(int studentID)
        {
            InitializeComponent();
            _StudentID = studentID;
            _Mode = enMode.Update;
            lblTitle.Text = "Update Student";
        }

        public frmAdd_Edit_Student()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            lblTitle.Text = "Add New Student";

        }


        private void _ResetDefault()
        {
            txtGPA.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            cbDepartmint.SelectedIndex = 0;
            dtpBithDate.Value = DateTime.Now;

        }


        private void frmAdd_Edit_Student_Load(object sender, EventArgs e)
        {
            _ResetDefault();

            if(_Mode == enMode.Update)
                _LoadData();  
        }


        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (!this.ValidateChildren(ValidationConstraints.Enabled))
        //    {
        //        MessageBox.Show("Please correct the errors before continuing.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // إنشاء كائن الطالب من البيانات المدخلة
        //    clsStudent student = new clsStudent
        //    {
        //        StudentID = Convert.ToInt32(txtStudentID.Text),
        //        FullName = txtName.Text,
        //        BirthDate = dtpBithDate.Value,
        //        Phone = txtPhone.Text,
        //        Department = cbDepartmint.Text,
        //        GPA = Convert.ToSingle(txtGPA.Text)
        //    };


        //    if (_Mode == enMode.AddNew)
        //    {
        //        clsStudentList.Students.Add(student);
        //        MessageBox.Show("Data Added Successfully :-)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        _Student = student;          // احتفظ بالمرجع الحالي
        //        _Mode = enMode.Update;       // بعد الإضافة يصبح الوضع تحديث
        //        lblTitle.Text = "Update Student";
        //    }
        //    else if (_Mode == enMode.Update)
        //    {
        //        if (clsStudentList.UpdateStudent(_StudentID, student))
        //        {
        //            MessageBox.Show("Data Updated Successfully :-)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            _Student = student;      // تحديث المرجع الحالي
        //        }
        //        else
        //        {
        //            MessageBox.Show("Update failed: Student not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren()) return;

            clsStudent student = new clsStudent
            {
                StudentID = int.Parse(txtStudentID.Text),
                FullName = txtName.Text,
                BirthDate = dtpBithDate.Value,
                Phone = txtPhone.Text,
                Department = cbDepartmint.Text,
                GPA = float.Parse(txtGPA.Text)
            };

            if (_Mode == enMode.AddNew)
            {
                clsStudentList.Students.Add(student);
                _Mode = enMode.Update;
                lblTitle.Text = "Update Student";
                MessageBox.Show("Added successfully!", "Message" , MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (_Mode == enMode.Update && clsStudentList.UpdateStudent(_StudentID, student))
            {
                MessageBox.Show("Updated successfully!" ,"Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            _Student = student;
        }



        private bool _LoadData()
        {
            _Student = clsStudentList.Find(_StudentID);

            //var Find = clsStudentList.Students.Find(s => s.StudentID == _StudentID); course 22 DataStructure

            if (_Student == null)
            {
                _ResetDefault();
                MessageBox.Show("Student not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
       
                return false; 
            }
           
            _LoadStudentInfo();
            return true;
        }


        private void _LoadStudentInfo()
        {
            lblTitle.Text = "Update Student";
            txtStudentID.Text = _StudentID.ToString();
            txtGPA.Text = _Student.GPA.ToString();
            txtName.Text = _Student.FullName;
            txtPhone.Text = _Student.Phone;
            cbDepartmint.Text = _Student.Department.ToString();
            dtpBithDate.Value = _Student.BirthDate;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtStudentID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                errorProvider1.SetError(txtStudentID, "Please enter the Student ID");
                e.Cancel = true;
            }


            //else if (clsStudentList.Students.Exists(s => s.StudentID == Convert.ToInt32(txtStudentID.Text)))

            if (clsStudentList.Students.Any(s => s.StudentID == Convert.ToInt32(txtStudentID.Text) && (_Mode == enMode.AddNew || s != _Student)))
            {
                errorProvider1.SetError(txtStudentID, "This Student ID is already in use");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtStudentID, "");

        }

        private void txtStudentID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // اسمح فقط بالأرقام وبزر Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // يمنع الحرف من الظهور في TextBox
            }

        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Please enter the Student Name");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtName, "");

        }

        private void txtGPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح بالأرقام ومفتاح التحكم
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // يمنع أي حرف آخر
            }

            // السماح بنقطة عشرية واحدة فقط
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true; // تمنع النقطة الثانية
            }

        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // يمنع الحرف من الظهور في TextBox
            }

        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                errorProvider1.SetError(txtPhone, "Please enter the Phone Number");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtPhone, "");
        }

        private void txtGPA_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGPA.Text))
            {
                errorProvider1.SetError(txtGPA, "Please enter the GPA");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtGPA, "");
        }
    }
}
