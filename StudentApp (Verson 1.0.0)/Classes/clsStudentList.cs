using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Classes
{
    public class clsStudentList
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;


        public static List<clsStudent> Students = new List<clsStudent>();
        


        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public float GPA { get; set; }

        public DateTime BirthDate { get; set; }


        public int TotalStudents = 0; 


        public clsStudentList()
        {
            TotalStudents = 0; 
        }

        public void AddNewStudent()
        {
         //  _Mode = enMode.AddNew;

            TotalStudents++; 
            clsStudent student = new clsStudent(StudentID, FullName , Phone , BirthDate, Department ,GPA);

            Students.Add(student);
        }

        public static bool UpdateStudent(int StudentID ,  clsStudent student)
        {
    //        _Mode = enMode.Update;

            foreach (var  s in Students)
            {
                if(s.StudentID == StudentID)
                {
                    s.StudentID = student.StudentID;
                    s.FullName = student.FullName;
                    s.Phone = student.Phone;
                    s.Department = student.Department;
                    s. GPA = student. GPA;
                    return true; 
                }
            }

            return false;
        }

        public static bool DeleteStudent(int StudentID)
        {
            foreach (var s in Students)
            {
                if (s.StudentID == StudentID)
                {
                    Students.Remove(s);
                    return true;
                }
            }

            return false;
        }


        private static void CreateStudentTableColumns(DataTable dt)
        {
            dt.Columns.Add("StudentID", typeof(int));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("BirthDate", typeof(string));
            dt.Columns.Add("Department", typeof(string));
            dt.Columns.Add("GPA", typeof(float));
        }

        static public DataTable dtListStudent()
        {
            DataTable dt = new DataTable();

            CreateStudentTableColumns(dt);

            foreach (var s in Students)
            {
                DataRow dataRow = dt.NewRow();

                dataRow["StudentID"] = s.StudentID; 
                dataRow["FullName"] = s.FullName;
                dataRow["Phone"] = s.Phone;
                dataRow["BirthDate"] = s.BirthDate;
                dataRow["Department"] = s.Department;
                dataRow["GPA"] = s.GPA;

                dt.Rows.Add(dataRow);
            }

            return dt; 
        }

        public static clsStudent Find(int studentID)
        {
            return Students.Find(s => s.StudentID == studentID);

            //foreach (var s in Students)
            //{
            //    if (s.StudentID == studentID)
            //    {
            //        return s;
            //    }
            //}

            //return null;
        }






    }


}
