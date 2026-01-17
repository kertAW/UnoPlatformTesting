namespace DocsUnoTesting.Models;

public class Student(string fullname) : IHasId
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid Id => _id;

    public string FullName { get; private set; } = fullname;

    public void ChangeFullname(string newFullName)
    {
        const int minFullNameLength = 5;

        if (newFullName.Length > minFullNameLength)
        {
            FullName = newFullName;
        }
        else
        {
            throw new ArgumentException(
                $"В имени должно быть минимум {minFullNameLength} символов"
            );
        }
    }
}
