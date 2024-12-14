using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ProjectIEEE.Models
{
    public class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = QR Code ; trustservercertificate = true ; integrated security = SSPI");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to One :QR , Lecture
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.QRCode)
                .WithOne(q => q.lecture)
                .HasForeignKey<QRCode>(q => q.lectureId);
            
            //Onr to Many :student , Attendance
            modelBuilder.Entity<Student>()
                .HasMany(s=>s.Attendances)
                .WithOne(a=>a.student)
                .HasForeignKey(a => a.studentID);

            //One to many : teacher , lecture
            modelBuilder.Entity <Teacher>()
                .HasMany(t=>t.Lectures)
                .WithOne(l=>l.teacher)
                .HasForeignKey(l=>l.teacherId);
        }
        public DbSet<Student > Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; } 
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }
    }

    public class Student
    {
        public int studentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Attendance> Attendances {  get; set; }
    }

    public class Teacher
    {
        public int teacherID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection <Lecture >Lectures { get; set; }
    }

    public class Lecture
        {
        public int lectureId { get; set; }
        public string lectureName { get; set; }
        public DateTime date { get; set; }

        public int teacherId {  get; set; }
        public Teacher teacher { get; set; }

        public ICollection <Attendance> Attendances { get; set; }
        public QRCode QRCode { get; set; }
        }

    public class Attendance
        {
        public int attendanceId { get; set; }

        public int lectureId { get; set; }
        public Lecture Lecture { get; set; }

        public int studentID { get; set; }
        public Student student { get; set; }
        }

    public class QRCode
        {
       public int QRCodeID { get; set; }
       public DateTime GeneratedAt { get; set; } = DateTime.Now;

       public int lectureId { get; set; }
       public Lecture lecture { get; set; }
    }
} 

