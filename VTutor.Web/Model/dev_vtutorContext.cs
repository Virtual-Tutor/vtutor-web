using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VTutor.Model
{
	public class VTutorContext : IdentityDbContext<ApplicationUser>
	{
		public VTutorContext(DbContextOptions<VTutorContext> options)
			: base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DbSet<ScheduleBlock> ScheduleBlocks { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Tutor> Tutors { get; set; }
		public DbSet<TutorSchedule> TutorSchedules { get; set; }
		public DbSet<DatabaseLog> DatabaseLogs { get; set; }
		public DbSet<PromoCode> PromoCodes { get; set; }
		public DbSet<PromoCodeUsage> PromoCodeUsages { get; set; }
		public DbSet<Image> Image { get; set; }
	}

	public class VTutorContextFactory : IDesignTimeDbContextFactory<VTutorContext>
	{
		public VTutorContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<VTutorContext>();
			builder.UseSqlServer("Server=tcp:dev-vtutor.database.windows.net,1433;Initial Catalog=dev_vtutor;Persist Security Info=False;User ID=isaac;Password=testdatabase1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
			return new VTutorContext(builder.Options);
		}
	}
}
