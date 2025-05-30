using c_sharp_backend.Models;
using c_sharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_backend.Config;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> users { get; set; }

    public DbSet<Announcement> announcements{ get; set; }
    
    public DbSet<ChatterCraft> chatterCraft { get; set; }
    
    public DbSet<Community> communities { get; set; }
    
    public DbSet<Gossip> gossips { get; set; }
    public DbSet<Lesson> lessons { get; set; }
    public DbSet<LessonPdf> lessonPdfs { get; set; }
    public DbSet<Teacher> teachers { get; set; }
    
    public DbSet<Like> likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Teacher)
            .WithMany(t => t.Lessons)
            .HasForeignKey(l => l.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}