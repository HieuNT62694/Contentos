using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuthenticationService.Entities
{
    public partial class ContentoContext : DbContext
    {
        public ContentoContext()
        {
        }

        public ContentoContext(DbContextOptions<ContentoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Activations> Activations { get; set; }
        public virtual DbSet<Behaviours> Behaviours { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<CampaignTags> CampaignTags { get; set; }
        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Contents> Contents { get; set; }
        public virtual DbSet<FavoritesContents> FavoritesContents { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Occupations> Occupations { get; set; }
        public virtual DbSet<Persionalizations> Persionalizations { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<StatusCampaign> StatusCampaign { get; set; }
        public virtual DbSet<StatusTasks> StatusTasks { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TasksChannels> TasksChannels { get; set; }
        public virtual DbSet<TasksTags> TasksTags { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=34.87.31.23;Database=Contento;User ID=sa;Password=Hieunguyen1@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
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
                    .HasMaxLength(50);

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_accounts_roles");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_accounts_users");
            });

            modelBuilder.Entity<Activations>(entity =>
            {
                entity.ToTable("activations");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdAccount).HasColumnName("id_account");

                entity.Property(e => e.IdBehaviour).HasColumnName("id_behaviour");

                entity.Property(e => e.IdContent)
                    .HasColumnName("id_content")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.Activations)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_activations_accounts");

                entity.HasOne(d => d.IdBehaviourNavigation)
                    .WithMany(p => p.Activations)
                    .HasForeignKey(d => d.IdBehaviour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_activations_behaviours");

                entity.HasOne(d => d.IdContentNavigation)
                    .WithMany(p => p.Activations)
                    .HasForeignKey(d => d.IdContent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_activations_contents");
            });

            modelBuilder.Entity<Behaviours>(entity =>
            {
                entity.ToTable("behaviours");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("campaign");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

                entity.Property(e => e.IdEditor).HasColumnName("id_editor");

                entity.Property(e => e.IdMarketer).HasColumnName("id_marketer");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.StartedDate)
                    .HasColumnName("started_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_StatusCampaign");
            });

            modelBuilder.Entity<CampaignTags>(entity =>
            {
                entity.HasKey(e => new { e.IdCampaign, e.IdTags })
                    .HasName("PK_campaign_tags_1");

                entity.ToTable("campaign_tags");

                entity.Property(e => e.IdCampaign).HasColumnName("id_campaign");

                entity.Property(e => e.IdTags).HasColumnName("id_tags");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.CampaignTags)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_campaign_tags_campaign");

                entity.HasOne(d => d.IdTagsNavigation)
                    .WithMany(p => p.CampaignTags)
                    .HasForeignKey(d => d.IdTags)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_campaign_tags_tags");
            });

            modelBuilder.Entity<Channels>(entity =>
            {
                entity.ToTable("channels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.ToTable("comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Contents>(entity =>
            {
                entity.ToTable("contents");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdComment).HasColumnName("id_comment");

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

                entity.HasOne(d => d.IdCommentNavigation)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.IdComment)
                    .HasConstraintName("FK_CommentContent");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contents_tasks");
            });

            modelBuilder.Entity<FavoritesContents>(entity =>
            {
                entity.HasKey(e => new { e.IdContent, e.IdUser })
                    .HasName("PK_favorites _contents_1");

                entity.ToTable("favorites _contents");

                entity.Property(e => e.IdContent).HasColumnName("id_content");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdContentNavigation)
                    .WithMany(p => p.FavoritesContents)
                    .HasForeignKey(d => d.IdContent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favorites _contents_contents");

                entity.HasOne(d => d.IdContent1)
                    .WithMany(p => p.FavoritesContents)
                    .HasForeignKey(d => d.IdContent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favorites _contents_users");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.ToTable("locations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Location).HasColumnName("location");
            });

            modelBuilder.Entity<Occupations>(entity =>
            {
                entity.ToTable("occupations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Persionalizations>(entity =>
            {
                entity.ToTable("persionalizations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTag).HasColumnName("id_tag");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdTagNavigation)
                    .WithMany(p => p.Persionalizations)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_persionalizations_tags");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Persionalizations)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_persionalizations_users");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StatusCampaign>(entity =>
            {
                entity.ToTable("status_campaign");

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

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Deadline)
                    .HasColumnName("deadline")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IdCampaign).HasColumnName("id_campaign");

                entity.Property(e => e.IdWriter).HasColumnName("id_writer");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PublishTime)
                    .HasColumnName("publish_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StartedDate)
                    .HasColumnName("started_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskCampaign");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_StatusTask");
            });

            modelBuilder.Entity<TasksChannels>(entity =>
            {
                entity.ToTable("tasks_channels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdChannel).HasColumnName("id_channel");

                entity.Property(e => e.IdTask).HasColumnName("id_task");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdChannelNavigation)
                    .WithMany(p => p.TasksChannels)
                    .HasForeignKey(d => d.IdChannel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tasks_channels_channels");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.TasksChannels)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tasks_channels_tasks");
            });

            modelBuilder.Entity<TasksTags>(entity =>
            {
                entity.HasKey(e => new { e.IdTask, e.IdTag })
                    .HasName("PK_tasks_tags_1");

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
                    .HasConstraintName("FK_tasks_tags_tags");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.TasksTags)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tasks_tags_tasks");
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

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasColumnType("text");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__tokens__id_user__02084FDA");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdManager).HasColumnName("id_manager");

                entity.Property(e => e.IdOccupation).HasColumnName("id_occupation");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Quote).HasColumnName("quote");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_locations");

                entity.HasOne(d => d.IdOccupationNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdOccupation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_occupations");
            });
        }
    }
}
