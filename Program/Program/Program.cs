using System.Security.Cryptography.X509Certificates;

namespace CourseRegisteration
{
    public class Student
    {
        public int ID { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        private static int count = 1;

        public Student(string name, string email)
        {
            ID = count++;
            Name = name;
            Email = email;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ID: {ID}, Name: {Name}, Email: {Email}");
        }
    }

    class Courses
    {
        public int CourseID { get; private set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }
        private static int countCourse = 1;

        public Courses(string courseName, int creditHours)
        {
            CourseID = countCourse++;
            CourseName = courseName;
            CreditHours = creditHours;
        }


        public void ShowInfo()
        {
            Console.WriteLine($"Course ID: {CourseID}, Course Name: {CourseName}, Credit Hours: {CreditHours}");
        }
    }

    class College
    {

        private bool[,] registration;
        private static List<Student> Students = new List<Student>();
        private static List<Courses> Courses = new List<Courses>();

        private void ResizeRegistrationArray()
        {
            bool[,] newArray = new bool[Students.Count, Courses.Count];
            if (registration != null)
            {
                for (int i = 0; i < Students.Count && i < registration.GetLength(0); i++)
                {
                    for (int j = 0; j < Courses.Count && j < registration.GetLength(1); j++)
                    {
                        newArray[i, j] = registration[i, j];
                    }
                }
            }

            registration = newArray;
        }

        public void AddStudent()
        {
            Console.WriteLine("Enter student name: ");
            string name = Console.ReadLine();
            Console.WriteLine("enter student email: ");
            string email = Console.ReadLine();

            Student newStudent = new Student(name, email);
            Students.Add(newStudent);

            ResizeRegistrationArray();
            Console.WriteLine("Student added successfully!");
        }

        public void AddCourse()
        {
            Console.WriteLine("Enter course name: ");
            string courseName = Console.ReadLine();
            Console.Write("Enter Credit Hours: ");
            int CreditHours = int.Parse(Console.ReadLine());
            Courses newCourse = new Courses(courseName, CreditHours);
            Courses.Add(newCourse);

            ResizeRegistrationArray();
            Console.WriteLine("Course added successfully!");

        }

        public void RegisterAddStudentToCourse()
        {
            Console.WriteLine("Enter student ID: ");
            int sid = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter course ID: ");
            int cid = int.Parse(Console.ReadLine());
            int sIndex = Students.FindIndex(x => x.ID == sid);
            int Cindex = Courses.FindIndex(x => x.CourseID == cid);
            if (sIndex > 0 && Cindex > 0)
            {
                registration[sIndex, Cindex] = true;
                Console.WriteLine("Registration successfully!");
            }
            else
            {
                Console.WriteLine("Registration failed!");
            }


        }

        public void DisplayAllStudents()
        {
            foreach (var x in Students)
            {
                x.ShowInfo();
            }
        }

        public void DisplayAllCourses()
        {
            foreach (var x in Courses)
            {
                x.ShowInfo();
            }
        }

        public void DisplayAllRegistrations()
        {
            for (int i = 0; i < Students.Count; i++)
            {
                Console.Write($"{Students[i].Name} is registered in: ");

                for (int j = 0; j < Courses.Count; j++)
                {
                    if (registration[i, j])
                    {
                        Console.Write($"{Courses[j].CourseName}, ");
                    }
                }

                Console.WriteLine();
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
    
