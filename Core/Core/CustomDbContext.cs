using System;
using System.Linq;
using System.Reflection;
using BaseEntities.Abstract;
using BaseEntities.Database;
using Core.Core.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Core
{
    public class CustomDbContext : DbContext
    {
        private string[] _myModules;

        public DbSet<MasterBlogCategory> MasterBlogCategory { get; set; }

        public DbSet<BlogCategory> BlogCategory { get; set; }

        public DbSet<MasterBlog> MasterBlog { get; set; }

        public DbSet<BlogEntity> BlogEntity { get; set; }

        public DbSet<MasterCountry> MasterCountry { get; set; }

        public DbSet<CountryEntity> Country { get; set; }

        public DbSet<MasterCity> MasterCity { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<MasterSchool> MasterSchool { get; set; }

        public DbSet<School> School { get; set; }

        public DbSet<ProgramRelation> ProgramRelation { get; set; }

        public DbSet<MasterContentDetail> MasterContentDetail { get; set; }

        public DbSet<ContentDetail> ContentDetail { get; set; }

        public DbSet<MasterSeoEntity> MasterSeoEntity { get; set; }

        public DbSet<SeoEntity> SeoEntity { get; set; }

        public DbSet<MasterSiteConfig> MasterSiteConfig { get; set; }

        public DbSet<SiteConfig> SiteConfig { get; set; }

        public DbSet<MasterSiteSection> MasterSiteSection { get; set; }

        public DbSet<SiteSection> SiteSection { get; set; }

        public DbSet<MasterSss> MasterSss { get; set; }

        public DbSet<Sss> Sss { get; set; }

        public DbSet<Reference> Reference { get; set; }

        public DbSet<UserComment> UserComment { get; set; }

        public DbSet<MasterTeam> MasterTeam { get; set; }

        public DbSet<TeamEntity> Team { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true);

            IConfigurationRoot configuration = config.Build();
            optionsBuilder.UseSqlServer(configuration.GetSection("SqlConnectionString").Get<string>());
            _myModules = configuration.GetSection("DbModules").Get<string[]>();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in _myModules)
                Assembly.Load(item);

            var asd = AppDomain.CurrentDomain.GetAssemblies().Where(w => _myModules.Contains(w.GetName().Name));
            var classes = asd
                .SelectMany(assembly => assembly.GetTypes()).Where(type => typeof(IDbSetup).IsAssignableFrom(type)).ToList();
            foreach (var cx in classes)
                ((IDbSetup)Activator.CreateInstance(cx)).Setup(modelBuilder);

            modelBuilder.ImmutableCreateTimeColumn();



            modelBuilder.Entity<MasterSiteConfig>().HasData(
                new MasterSiteConfig
                {
                    Id = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                }
            );

            modelBuilder.Entity<SiteConfig>().HasData(
                new SiteConfig
                {
                    Id = new Guid("3b35e3d8-5abb-4da0-9ef6-ccf40dbcf0aa"),
                    MasterId = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                    
                    AboutUs = "Hakkımızda Yazısı",
                    Address = "Adres Bilgisi",
                    MailAddress = "mailadresi@mailadresi.com",
                    PhoneNumber = "05515515454",
                    SiteName = "Klabs Teknoloji",
                    Lang = "en-US",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                }
            ) ;

            modelBuilder.Entity<SiteConfig>().HasData(
                new SiteConfig
                {
                    Id = new Guid("20f6282d-55f6-4fe8-9929-7b03a6a48fdc"),
                    MasterId = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),

                    AboutUs = "Hakkımızda Yazısı",
                    Address = "Adres Bilgisi",
                    MailAddress = "mailadresi@mailadresi.com",
                    PhoneNumber = "05515515454",
                    SiteName = "Klabs Teknoloji",
                    Lang = "tr-TR",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                }
            );

        }
    }
}