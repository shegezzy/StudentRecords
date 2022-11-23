using Microsoft.EntityFrameworkCore;

namespace StudentRecords.Data
{
    public interface IStudentDbContext
    {
        DbSet<Student> Student { get; set; }
        Task<int> SaveChangesAsync();
    }
}
