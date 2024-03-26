using System.Collections.Generic;
using System.Data.SQLite;
using Ukol_DatabaseWPF;

public class DatabaseManager
{
    private string connectionString = "Data Source=mydatabase2.db;Version=3;";

    public void CreateStudentTable()
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "CREATE TABLE IF NOT EXISTS Students (Id INTEGER PRIMARY KEY, FirstName TEXT, LastName TEXT, Age INTEGER, Class TEXT)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }

    public List<Student> GetStudents()
    {
        List<Student> students = new List<Student>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT Id, FirstName, LastName, Age, Class FROM Students";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Class = reader["Class"].ToString()
                        });
                    }
                }
            }
        }

        return students;
    }

    public static void Initialize()
    {
        DatabaseManager dbManager = new DatabaseManager();
        dbManager.CreateStudentTable();
        dbManager.CreateSubjectTable();
        dbManager.CreateAssignmentTable();
    }

    public void AddStudent(string firstName, string lastName, int age, string className)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Students (FirstName, LastName, Age, Class) VALUES (@FirstName, @LastName, @Age, @Class)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Class", className);
                command.ExecuteNonQuery();
            }
        }
    }

    public void CreateSubjectTable()
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "CREATE TABLE IF NOT EXISTS Subjects (Id INTEGER PRIMARY KEY, Name TEXT, Class TEXT)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }

    public void AddSubject(string name, string className)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Subjects (Name, Class) VALUES (@Name, @Class)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Class", className);
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Subject> GetSubjects()
    {
        List<Subject> subjects = new List<Subject>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT Id, Name, Class FROM Subjects";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjects.Add(new Subject
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Class = reader["Class"].ToString()
                        });
                    }
                }
            }
        }

        return subjects;
    }

    public void CreateAssignmentTable()
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "CREATE TABLE IF NOT EXISTS Assignments (StudentId INTEGER, SubjectId INTEGER, FOREIGN KEY(StudentId) REFERENCES Students(Id), FOREIGN KEY(SubjectId) REFERENCES Subjects(Id))";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }

    public List<Student> GetStudentsForSubject(int subjectId)
    {
        List<Student> students = new List<Student>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT s.Id, s.FirstName, s.LastName, s.Age, s.Class FROM Students s " +
                           "INNER JOIN Assignments a ON s.Id = a.StudentId " +
                           "WHERE a.SubjectId = @SubjectId";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SubjectId", subjectId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Class = reader["Class"].ToString()
                        });
                    }
                }
            }
        }

        return students;
    }
    public bool CheckAssignmentExists(int studentId, int subjectId)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM Assignments WHERE StudentId = @StudentId AND SubjectId = @SubjectId";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@SubjectId", subjectId);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }


    public void AssignSubjectToStudent(int studentId, int subjectId)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Assignments (StudentId, SubjectId) VALUES (@StudentId, @SubjectId)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@SubjectId", subjectId);
                command.ExecuteNonQuery();
            }
        }
    }
    public void UpdateSubject(Subject subject)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE Subjects SET Name = @Name, Class = @Class WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", subject.Name);
                command.Parameters.AddWithValue("@Class", subject.Class);
                command.Parameters.AddWithValue("@Id", subject.Id);
                command.ExecuteNonQuery();
            }
        }
    }
    public void UpdateStudent(Student student)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Class = @Class WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Age", student.Age);
                command.Parameters.AddWithValue("@Class", student.Class);
                command.Parameters.AddWithValue("@Id", student.Id);
                command.ExecuteNonQuery();
            }
        }
    }
    public void RemoveStudentFromSubject(int studentId, int subjectId)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Assignments WHERE StudentId = @StudentId AND SubjectId = @SubjectId";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@SubjectId", subjectId);
                command.ExecuteNonQuery();
            }
        }
    }
}
