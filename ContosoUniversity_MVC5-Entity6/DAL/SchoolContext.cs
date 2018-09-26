using ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolContext") {
            //Database.SetInitializer<SchoolContext>(new DropCreateDatabaseAlways<SchoolContext>());
            Database.SetInitializer(new SchoolInitializer());
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departmentes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));

            // Table-Per-Type: descriminator column 
            modelBuilder.Entity<Person>()
                    .Map<Student>(m => m.Requires("Type").HasValue("Student"))
                    .Map<Instructor>(m => m.Requires("Type").HasValue("Instructor"));


            // Table-Per-Concrete-Type: maps properties from base to deriving class
            //modelBuilder.Entity<Student>().Map(m =>
            //    {
            //        m.MapInheritedProperties();
            //        m.ToTable("Student");
            //    }).HasKey(s => s.ID).HasMany(s => s.Enrollments);

            //modelBuilder.Entity<Instructor>()
            //    .Map(m =>
            //    {
            //        m.MapInheritedProperties();
            //        m.ToTable("Instructor");
            //    });

            modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}