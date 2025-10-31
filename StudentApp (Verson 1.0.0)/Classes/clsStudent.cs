using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Classes
{
    public class clsStudent
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }

        public DateTime  BirthDate { get; set; }
        public float GPA { get; set; }

        public clsStudent()
        {

        }


        public clsStudent(int StudentID ,string FullName, string Phone , DateTime BirthDate ,  string Department ,float GPA)
        {
            this.StudentID = StudentID;
            this.FullName = FullName;
            this.Phone = Phone;
            this.Department = Department;
            this. GPA = GPA;
            this.BirthDate = BirthDate; 
        }

    }

}
