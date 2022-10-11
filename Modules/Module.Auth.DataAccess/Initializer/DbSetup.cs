using System;
using BaseEntities.Abstract;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Module.Auth.Entities;

namespace Module.Auth.DataAccess.Initializer
{
    public class DbSetup : IDbSetup
    {
        public void Setup(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationClaim>().ToTable("OperationClaims");

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<UserPassword>().ToTable("UserPasswords");

            modelBuilder.Entity<UserValidationCode>().ToTable("UserValidationCodes");

            modelBuilder.Entity<UserOperationClaim>().ToTable("UserOperationClaims");

            modelBuilder.Entity<Newsletter>().ToTable("Newsletters");

            modelBuilder.Entity<Newsletter>().HasIndex(w => w.MailAddress).IsUnique();

            //Burdaki Claimlar customer ve staff create metotlarında kullanılıyor. Değişiklik yaparsan oradada yapmalısın.
            modelBuilder.Entity<OperationClaim>().HasData(
                new OperationClaim
                {
                    Id = new Guid("d7be8d00-4434-4636-a467-c511ea307016"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Name = "Admin",
                    DisplayName = "Admin"
                },
                new OperationClaim
                {
                    Id = new Guid("d7be8d00-4434-4636-a467-c511ea307018"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Name = "Staff",
                    DisplayName = "Personel"
                },
                new OperationClaim
                {
                    Id = new Guid("bdc23d6c-2e03-4b0d-b34a-7535d74abbc8"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Name = "Customer",
                    DisplayName = "Müşteri"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    LastName = "admin",
                    FirstName = "admin",
                    MailAddress = "admin@admin.com",
                    LoginName = "admin"
                }
            );

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("admin", out passwordHash, out passwordSalt);

            modelBuilder.Entity<UserPassword>().HasData(
                new UserPassword
                {
                    Id = new Guid("cbe7509a-3e0d-441e-9e61-905742c652d5"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    UserId = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                    HashedPassword = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );

            modelBuilder.Entity<UserOperationClaim>().HasData(
                new UserOperationClaim
                {
                    Id = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    OperationClaimId = new Guid("d7be8d00-4434-4636-a467-c511ea307016"),
                    UserId = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                },
                new UserOperationClaim
                {
                    Id = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06947eb"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    OperationClaimId = new Guid("d7be8d00-4434-4636-a467-c511ea307018"),
                    UserId = new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                }
            );
        }
    }
}