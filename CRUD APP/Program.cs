using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_APP
{
    internal class Program
    {
         static string connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=TinoeProjects;Integrated Security=True;";

        static int GetValidInt(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
        static void Main(string[] args)
        {
           

            while (true)
            {
                Console.Clear();

                Console.WriteLine("=================================================");
                Console.WriteLine("            STUDENT MANAGEMENT SYSTEM            ");
                Console.WriteLine("=================================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3.Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.Write("Choose Option: ");
                int choice = int.Parse(Console.ReadLine());
                Console.WriteLine("================================================");
                

                switch (choice)
                {
                    case 1: AddStudent(); break;
                    case 2: ViewStudents(); break;
                    case 3: UpdateStudent(); break;
                    case 4: DeleteStudent(); break;
                    case 5: return;
                    default: Console.WriteLine("Invalid Option"); break;
                }
            }

            Console.WriteLine("\nPress any key to go back to MAIN MENU.....");
            Console.ReadLine();
        }

        static void AddStudent()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();


                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Age: ");
                    int age = int.Parse(Console.ReadLine());

                    Console.Write("Enter Course: ");
                    string couse = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(couse))
                    {
                        Console.WriteLine("Name and Course cannot be empty");
                    }

                    string query = "INSERT INTO Students (Name, Age, Course) VALUES (@Name, @Age, @Course)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@Course", couse);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            Console.WriteLine("================================================");
                            Console.Write("Student Added Successfully");
                        }
                        else
                        {
                            Console.WriteLine("================================================");
                            Console.Write("Student Add Failed!!");
                        }
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
            
        
        
    static void ViewStudents()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string query = "SELECT * FROM Students;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("====USERS LIST====");
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["Id"]}, Name : {reader["Name"]}, Age: {reader["Age"]}, Course: {reader["Course"]}");
                            }
                        }
                    }
                    conn.Close();
                    Console.ReadLine();
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void UpdateStudent()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    Console.Write("Enter User ID: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Enter New Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Age: ");
                    int age = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter New Course: ");
                    string course = Console.ReadLine();

                    string query = "UPDATE Students SET Name=@Name, Age=@Age, Course=@Course WHERE Id=@id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@Course", course);


                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            Console.Write("Student Updated Success");
                        else
                            Console.Write("Student Update Failed!!");

                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                    }
        }

        static void DeleteStudent()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    Console.Write("Enter User ID: ");
                    int id = int.Parse(Console.ReadLine());

                    string query = "DELETE FROM Students WHERE Id=@Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            Console.Write("Student Deelet Success");
                        else
                            Console.Write("Student Delete Failed!!");
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                    }
        }

                
            }
        }
