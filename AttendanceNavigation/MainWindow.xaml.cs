using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AttendanceNavigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var db = new AppDbContext())
            {
                //AddNameStudent();
                //AddSubject();
                //AddVisit();

                GroupTabels();
            }
        }

        public async void AddNameStudent()
        {
            using (var db = new AppDbContext())
            {
                await db.Students.AddAsync(new Student() { Id = Guid.NewGuid(), Name = "Brad Pit" });
                await db.SaveChangesAsync();
            }
        }

        public async void AddSubject()
        {
            using (var db = new AppDbContext())
            {
                await db.Subjects.AddAsync(new Subject() { Id = Guid.NewGuid(), Title ="Math"});
                await db.Subjects.AddAsync(new Subject() { Id = Guid.NewGuid(), Title = "Physics" });
                await db.SaveChangesAsync();
            }
        }

        public async void AddVisit()
        {
            using (var db = new AppDbContext())
            {
                await db.Visits.AddAsync(new Visits()
                {
                    Id = Guid.NewGuid(),
                    Date = new DateOnly(2022, 05, 12),
                    Student = new Student { Id = Guid.Empty, Name = "Angelina Joli" },
                    Subject = new Subject { Id = Guid.Empty, Title = "Physics" }
                });
                await db.SaveChangesAsync();
            }
        }

        public async void GroupTabels()
        {
            using (var db = new AppDbContext())
            {
                var result = from visit in db.Visits
                             join student in db.Students on visit.StudentId equals student.Id
                             join subject in db.Subjects on visit.SubjectId equals subject.Id
                             select new
                             {
                                 Date = visit.Date,
                                 Name = visit.Student.Name,
                                 Subject = visit.Subject.Title

                             };
                foreach(var r in result)
                {
                    Attendance.Text += $"{r.Date} | {r.Name} | {r.Subject} \n";
                }
            }
        }

    }

    public class AppDbContext : DbContext
    {
        private const string ConnectionString = "Data Source = C:\\Users\\kirsa\\OneDrive\\Рабочий стол\\Академия Шаг\\ADO.net and Entity Frimework\\class work\\06.09.22\\AttendanceNavigation\\AttendanceNavigation\\AttendanceNavigationProperties.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Visits> Visits { get; set; }
    }

   
}


