using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CampaignService.Entities
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
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Behaviours> Behaviours { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<Contents> Contents { get; set; }
        public virtual DbSet<FavoritesContents> FavoritesContents { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Occupations> Occupations { get; set; }
        public virtual DbSet<Persionalizations> Persionalizations { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<PositionsAccounts> PositionsAccounts { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TasksAccounts> TasksAccounts { get; set; }
        public virtual DbSet<TasksChannels> TasksChannels { get; set; }
        public virtual DbSet<TasksTags> TasksTags { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
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

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");

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

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Behaviours>(entity =>
            {
                entity.ToTable("behaviours");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("campaign");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Channels>(entity =>
            {
                entity.ToTable("channels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Contents>(entity =>
            {
                entity.ToTable("contents");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdStask).HasColumnName("id_stask");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100);

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.IdStaskNavigation)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.IdStask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contents_tasks_channels");
            });

            modelBuilder.Entity<FavoritesContents>(entity =>
            {
                entity.ToTable("favorites _contents");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdContent).HasColumnName("id_content");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

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

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTag).HasColumnName("id_tag");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
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

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.ToTable("positions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<PositionsAccounts>(entity =>
            {
                entity.ToTable("positions_accounts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAccount).HasColumnName("id_account");

                entity.Property(e => e.IdCampaign).HasColumnName("id_campaign");

                entity.Property(e => e.IdPosition).HasColumnName("id_position");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.PositionsAccounts)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_positions_accounts_accounts");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.PositionsAccounts)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_positions_accounts_campaign");

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.PositionsAccounts)
                    .HasForeignKey(d => d.IdPosition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_positions_accounts_positions");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Deadline)
                    .HasColumnName("deadline")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IdCampaign).HasColumnName("id_campaign");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.PublishTime)
                    .HasColumnName("publish_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tasks_campaign");
            });

            modelBuilder.Entity<TasksAccounts>(entity =>
            {
                entity.ToTable("tasks_accounts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdAccount).HasColumnName("id_account");

                entity.Property(e => e.IdTask).HasColumnName("id_task");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.TasksAccounts)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tasks_accounts_accounts");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.TasksAccounts)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tasks_accounts_tasks");
            });

            modelBuilder.Entity<TasksChannels>(entity =>
            {
                entity.ToTable("tasks_channels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdChannel).HasColumnName("id_channel");

                entity.Property(e => e.IdTask).HasColumnName("id_task");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
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
                entity.ToTable("tasks_tags");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTag).HasColumnName("id_tag");

                entity.Property(e => e.IdTask).HasColumnName("id_task");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdTagNavigation)
                    .WithMany(p => p.TasksTags)
                    .HasForeignKey(d => d.IdTag)
                    .HasConstraintName("FK_tasks_tags_tags");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.TasksTags)
                    .HasForeignKey(d => d.IdTask)
                    .HasConstraintName("FK_tasks_tags_tasks");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.ToTable("tokens");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeviceType)
                    .HasColumnName("device_type")
                    .HasMaxLength(50);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Modified)
                    .HasColumnName("modified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

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

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdManager).HasColumnName("id_manager");

                entity.Property(e => e.IdOccupation).HasColumnName("id_occupation");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Quote).HasColumnName("quote");

                entity.Property(e => e.Status).HasColumnName("status");

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
