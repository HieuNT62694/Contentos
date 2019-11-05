using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BatchjobService.Entities
{
    public partial class ContentoDbContext : DbContext
    {
        public ContentoDbContext()
        {
        }

        public ContentoDbContext(DbContextOptions<ContentoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Campaigns> Campaigns { get; set; }
        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Contents> Contents { get; set; }
        public virtual DbSet<Fanpages> Fanpages { get; set; }
        public virtual DbSet<FavoritesContents> FavoritesContents { get; set; }
        public virtual DbSet<Notifys> Notifys { get; set; }
        public virtual DbSet<Personalizations> Personalizations { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<StatusCampaigns> StatusCampaigns { get; set; }
        public virtual DbSet<StatusTasks> StatusTasks { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<TagsCampaigns> TagsCampaigns { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TasksAccounts> TasksAccounts { get; set; }
        public virtual DbSet<TasksFanpages> TasksFanpages { get; set; }
        public virtual DbSet<TasksTags> TasksTags { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=34.87.31.23;Database=ContentoDb;User ID=sa;Password=Hieunguyen1@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.ToTable("accounts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_AccountRole");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_AccountUser");
            });

            modelBuilder.Entity<Campaigns>(entity =>
            {
                entity.ToTable("campaigns");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

                entity.Property(e => e.IdEditor).HasColumnName("id_editor");

                entity.Property(e => e.IdMarketer).HasColumnName("id_marketer");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.CampaignsIdCustomerNavigation)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK_CampaignCustomer");

                entity.HasOne(d => d.IdEditorNavigation)
                    .WithMany(p => p.CampaignsIdEditorNavigation)
                    .HasForeignKey(d => d.IdEditor)
                    .HasConstraintName("FK_CampaignEditor");

                entity.HasOne(d => d.IdMarketerNavigation)
                    .WithMany(p => p.CampaignsIdMarketerNavigation)
                    .HasForeignKey(d => d.IdMarketer)
                    .HasConstraintName("FK_CampaignMarketer");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_CampaignStatus");
            });

            modelBuilder.Entity<Channels>(entity =>
            {
                entity.ToTable("channels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.ToTable("comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdContent).HasColumnName("id_content");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.HasOne(d => d.IdContentNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdContent)
                    .HasConstraintName("FK_comment_content");
            });

            modelBuilder.Entity<Contents>(entity =>
            {
                entity.ToTable("contents");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTask).HasColumnName("id_task");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.TheContent).HasColumnName("the_content");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.IdTask)
                    .HasConstraintName("FK_ContentToTask");
            });

            modelBuilder.Entity<Fanpages>(entity =>
            {
                entity.ToTable("fanpages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdChannel).HasColumnName("id_channel");

                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

                entity.Property(e => e.IdMarketer).HasColumnName("id_marketer");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(250);

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Token).HasColumnName("token");

                entity.Property(e => e.Username).HasColumnName("username");

                entity.HasOne(d => d.IdChannelNavigation)
                    .WithMany(p => p.Fanpages)
                    .HasForeignKey(d => d.IdChannel)
                    .HasConstraintName("FK_fanpage_channel");

                entity.HasOne(d => d.IdMarketerNavigation)
                    .WithMany(p => p.Fanpages)
                    .HasForeignKey(d => d.IdMarketer)
                    .HasConstraintName("FK_fanpage_marketer");
            });

            modelBuilder.Entity<FavoritesContents>(entity =>
            {
                entity.HasKey(e => new { e.IdContent, e.IdUser });

                entity.ToTable("favorites _contents");

                entity.Property(e => e.IdContent).HasColumnName("id_content");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdContentNavigation)
                    .WithMany(p => p.FavoritesContents)
                    .HasForeignKey(d => d.IdContent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoritesContentToContent");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.FavoritesContents)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoritesContentToUser");
            });

            modelBuilder.Entity<Notifys>(entity =>
            {
                entity.ToTable("notifys");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdToken).HasColumnName("id_token");

                entity.Property(e => e.Messager)
                    .HasColumnName("messager")
                    .HasMaxLength(250);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdTokenNavigation)
                    .WithMany(p => p.Notifys)
                    .HasForeignKey(d => d.IdToken)
                    .HasConstraintName("FK_token");
            });

            modelBuilder.Entity<Personalizations>(entity =>
            {
                entity.HasKey(e => new { e.IdTag, e.IdUser });

                entity.ToTable("personalizations");

                entity.Property(e => e.IdTag).HasColumnName("id_tag");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.TimeInteraction)
                    .HasColumnName("time_interaction")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdTagNavigation)
                    .WithMany(p => p.Personalizations)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonalizationsToTags");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Personalizations)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonalizationsToUser");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StatusCampaigns>(entity =>
            {
                entity.ToTable("status_campaigns");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<StatusTasks>(entity =>
            {
                entity.ToTable("status_tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TagsCampaigns>(entity =>
            {
                entity.HasKey(e => new { e.IdCampaign, e.IdTag });

                entity.ToTable("tags_campaigns");

                entity.Property(e => e.IdCampaign).HasColumnName("id_campaign");

                entity.Property(e => e.IdTag).HasColumnName("id_tag");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.TagsCampaigns)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagCampaignToCampaign");

                entity.HasOne(d => d.IdTagNavigation)
                    .WithMany(p => p.TagsCampaigns)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagCampaignToTags");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Deadline)
                    .HasColumnName("deadline")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IdCampaign).HasColumnName("id_campaign");

                entity.Property(e => e.IdWritter).HasColumnName("id_writter");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PublishTime)
                    .HasColumnName("publish_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdCampaign)
                    .HasConstraintName("FK_TaskCampaignToCampaign");

                entity.HasOne(d => d.IdWritterNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdWritter)
                    .HasConstraintName("FK_TaskUser");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_TaskStatus");
            });

            modelBuilder.Entity<TasksAccounts>(entity =>
            {
                entity.HasKey(e => new { e.IdAccount, e.IdTask });

                entity.ToTable("tasks_accounts");

                entity.Property(e => e.IdAccount).HasColumnName("id_account");

                entity.Property(e => e.IdTask).HasColumnName("id_task");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.TasksAccounts)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TasksAccountToAccount");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.TasksAccounts)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TasksAccountToTask");
            });

            modelBuilder.Entity<TasksFanpages>(entity =>
            {
                entity.HasKey(e => new { e.IdTask, e.IdFanpage });

                entity.ToTable("tasks_fanpages");

                entity.Property(e => e.IdTask).HasColumnName("id_task");

                entity.Property(e => e.IdFanpage).HasColumnName("id_fanpage");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdJob).HasColumnName("id_job");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdFanpageNavigation)
                    .WithMany(p => p.TasksFanpages)
                    .HasForeignKey(d => d.IdFanpage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskChannelToFanage");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.TasksFanpages)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TasksChannelsToTasks");
            });

            modelBuilder.Entity<TasksTags>(entity =>
            {
                entity.HasKey(e => new { e.IdTask, e.IdTag });

                entity.ToTable("tasks_tags");

                entity.Property(e => e.IdTask).HasColumnName("id_task");

                entity.Property(e => e.IdTag).HasColumnName("id_tag");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdTagNavigation)
                    .WithMany(p => p.TasksTags)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskTagToTags");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.TasksTags)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.ToTable("tokens");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeviceType)
                    .HasColumnName("device_type")
                    .HasMaxLength(50);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasColumnType("text");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_TokenUser");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Company)
                    .HasColumnName("company")
                    .HasMaxLength(200);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdManager).HasColumnName("id_manager");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.InverseIdManagerNavigation)
                    .HasForeignKey(d => d.IdManager)
                    .HasConstraintName("FK_UserManager");
            });
        }
    }
}
