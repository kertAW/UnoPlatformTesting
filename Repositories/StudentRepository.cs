using System.Collections.Generic;
using System.Linq;
using DocsUnoTesting.Models;

namespace DocsUnoTesting.Repositories;

public class StudentRepository
{
    private readonly List<Student> _students = new();

    public StudentRepository()
    {
        Seed();
    }

    public IEnumerable<Student> GetAll() => _students;

    public Student? GetById(Guid id) => _students.FirstOrDefault(s => s.Id == id);

    public void Add(Student student) => _students.Add(student);

    public void Update(Student student)
    {
        var existingStudent = GetById(student.Id);
        if (existingStudent != null)
        {
            // In a real app, you would update properties.
            // As Student is a class with a private setter for FullName, we can't directly update it here.
            // We can only replace the object.
            _students.Remove(existingStudent);
            _students.Add(student);
        }
    }

    public void Delete(Guid id)
    {
        var student = GetById(id);
        if (student != null)
        {
            _students.Remove(student);
        }
    }

    private void Seed()
    {
        _students.AddRange(new List<Student>
        {
            new("John Doe"),
            new("Jane Smith"),
            new("Peter Jones"),
            new("Mary Williams"),
            new("David Brown"),
            new("Susan Davis"),
            new("Michael Miller"),
            new("Linda Wilson"),
            new("Robert Moore"),
            new("Patricia Taylor")
        });
    }
}
