using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Classes
{
    public class clsStudentDataSimulation
    {

        public static readonly List<clsStudent> students = new List<clsStudent>
        {
            new clsStudent(1 , "Keroles Mezare" , "01029541543" , DateTime.Now , "IT" , 3.2f),
            new clsStudent(2 , "Michael Thompson" , "01012457896" , DateTime.Now , "Computer Science" , 3.8f),
            new clsStudent(3 , "Sophia Carter" , "01096587412" , DateTime.Now , "Business" , 3.4f),
            new clsStudent(4 , "Ethan Rodriguez" , "01035487965" , DateTime.Now , "Engineering" , 2.9f),
            new clsStudent(5 , "Olivia Johnson" , "01078546932" , DateTime.Now , "Medicine" , 3.7f),
            new clsStudent(6 , "Liam Anderson" , "01069874521" , DateTime.Now , "Law" , 3.1f),
            new clsStudent(7 , "Emma Wilson" , "01085741236" , DateTime.Now , "Design" , 3.5f),
            new clsStudent(8 , "Noah Brown" , "01047896523" , DateTime.Now , "Finance" , 3.0f),
            new clsStudent(9 , "Ava Martinez" , "01023658974" , DateTime.Now , "Psychology" , 3.6f),
            new clsStudent(10 , "James Walker" , "01058963214" , DateTime.Now , "Architecture" , 3.3f),

        };

    }
}
