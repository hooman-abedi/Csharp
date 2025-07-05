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

        public void RegisterStudentToCourse()
        {
            Console.WriteLine("Enter student ID: ");
            int sid = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter course ID: ");
            int cid = int.Parse(Console.ReadLine());
            int sIndex = Students.FindIndex(x => x.ID == sid);
            int Cindex = Courses.FindIndex(x => x.CourseID == cid);
            if (sIndex >= 0 && Cindex >= 0)
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

        public void DisplayRegistrations()
        {
            for (int i = 0; i < Students.Count; i++) // lowercase 'students'
            {
                Console.Write($"{Students[i].Name} is registered in: ");

                bool registeredInAnyCourse = false;

                for (int j = 0; j < Courses.Count; j++) // lowercase 'courses'
                {
                    if (registration[i, j])
                    {
                        Console.Write($"{Courses[j].CourseName}, ");
                        registeredInAnyCourse = true;
                    }
                }

                if (!registeredInAnyCourse)
                {
                    Console.Write("No courses");
                }

                Console.WriteLine();
            }
        }
        

        public void SaveData()
        {
            File.WriteAllLines("Students.txt", Students.ConvertAll(s=> $"{s.ID}, {s.Name}, {s.Email}"));
            File.WriteAllLines("Courses.txt", Courses.ConvertAll(c => $"{c.CourseID}, {c.CourseName}m{c.CreditHours}"));
            List<string> reg = new List<string>();
            for (int i = 0; i < Students.Count; i++)
            {
                for (int j = 0; j < Courses.Count; j++)
                {
                    if (registration[i,j])
                    {
                        reg.Add($"{Students[i].ID}, {Courses[j].CourseID}");
                    }
                }
            }
            File.WriteAllLines("registration.txt", reg);
        }
        public void LoadData()
        {
            try
            {
                if (File.Exists("Students.txt"))
                {
                    foreach (var line in File.ReadAllLines("Students.txt"))
                    {
                        var parts = line.Split(',');
                        Students.Add(new Student(parts[1], parts[2]));
                    }
                }

                if (File.Exists("Courses.txt"))
                {
                    foreach (var line in File.ReadAllLines("Courses.txt"))
                    {
                        var parts = line.Split(',');
                        Courses.Add(new Courses(parts[1], int.Parse(parts[2])));
                    }
                }

                ResizeRegistrationArray();

                if (File.Exists("registrations.txt"))
                {
                    foreach (var line in File.ReadAllLines("registrations.txt"))
                    {
                        var parts = line.Split(',');
                        int sIndex = Students.FindIndex(s => s.ID == int.Parse(parts[0]));
                        int cIndex = Courses.FindIndex(c => c.CourseID == int.Parse(parts[1]));
                        if (sIndex >= 0 && cIndex >= 0)
                            registration[sIndex, cIndex] = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading data: " + e.Message);
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            College college = new College();
            college.LoadData();

            while (true)
            {
                Console.WriteLine("\n--- Course Registration System ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Register Student to Course");
                Console.WriteLine("4. Display All Students");
                Console.WriteLine("5. Display All Courses");
                Console.WriteLine("6. Display Registrations");
                Console.WriteLine("7. Save Data");
                Console.WriteLine("8. Load Data");
                Console.WriteLine("9. Exit");
                Console.Write("Enter choice: ");

                switch (Console.ReadLine())
                {
                    case "1": college.AddStudent(); break;
                    case "2": college.AddCourse(); break;
                    case "3": college.RegisterStudentToCourse(); break;
                    case "4": college.DisplayAllStudents(); break;
                    case "5": college.DisplayAllCourses(); break;
                    case "6": college.DisplayRegistrations(); break;
                    case "7": college.SaveData(); break;
                    case "8": college.LoadData(); break;
                    case "9": college.SaveData(); return;
                }
            }
        }
    }
}
    
