using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class alldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterBlogCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBlogCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterContentDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    EntityRelationType = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterContentDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterCountry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCountry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterProgram",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    MasterProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterProgram_MasterProgram_MasterProgramId",
                        column: x => x.MasterProgramId,
                        principalTable: "MasterProgram",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MasterSeoEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterSeoEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterSiteConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstCounterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondCounterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdCounterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstCounterValue = table.Column<int>(type: "int", nullable: false),
                    SecondCounterValue = table.Column<int>(type: "int", nullable: false),
                    ThirdCounterValue = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterSiteConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterSiteSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    PartialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterSiteSection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterSss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterSss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterTeam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linkedn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTeam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MailAddress = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Registered = table.Column<bool>(type: "bit", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialNetworkEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetworkEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SocialRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogCount = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogCategory_MasterBlogCategory_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterBlogCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityRelationType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentDetail_MasterContentDetail_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterContentDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_MasterCountry_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterCity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterCity_MasterCountry_CountryId",
                        column: x => x.CountryId,
                        principalTable: "MasterCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramEntity_MasterProgram_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityRelationType = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramRelation_MasterProgram_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "MasterProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeoEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeoEntity_MasterSeoEntity_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterSeoEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutUs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteConfig_MasterSiteConfig_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterSiteConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteSection_MasterSiteSection_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterSiteSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sss_MasterSss_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterSss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_MasterTeam_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPasswords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HashedPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPasswords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserValidationCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    ValidationCodeType = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserValidationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserValidationCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterBlog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBlog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterBlog_BlogCategory_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterCountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_MasterCity_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterCity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_City_MasterCountry_MasterCountryId",
                        column: x => x.MasterCountryId,
                        principalTable: "MasterCountry",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MasterSchool",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterSchool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterSchool_MasterCity_CityId",
                        column: x => x.CityId,
                        principalTable: "MasterCity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogEntity_BlogCategory_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogEntity_MasterBlog_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterBlog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                    table.ForeignKey(
                        name: "FK_School_MasterSchool_MasterId",
                        column: x => x.MasterId,
                        principalTable: "MasterSchool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MasterSiteConfig",
                columns: new[] { "Id", "CreateTime", "FirstCounterText", "FirstCounterValue", "SecondCounterText", "SecondCounterValue", "ThirdCounterText", "ThirdCounterValue", "UpdateTime" },
                values: new object[] { new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"), new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5261), null, 0, null, 0, null, 0, new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5264) });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreateTime", "DisplayName", "Name", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("bdc23d6c-2e03-4b0d-b34a-7535d74abbc8"), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8794), "Müşteri", "Customer", new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8794) },
                    { new Guid("d7be8d00-4434-4636-a467-c511ea307016"), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8779), "Admin", "Admin", new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8789) },
                    { new Guid("d7be8d00-4434-4636-a467-c511ea307018"), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8791), "Personel", "Staff", new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8792) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDay", "CreateTime", "FirstName", "LastName", "LoginName", "MailAddress", "PhoneNumber", "SocialRef", "UpdateTime" },
                values: new object[] { new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8860), "admin", "admin", "admin", "admin@admin.com", null, null, new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8861) });

            migrationBuilder.InsertData(
                table: "SiteConfig",
                columns: new[] { "Id", "AboutUs", "Address", "CreateTime", "Lang", "MailAddress", "MasterId", "PhoneNumber", "SiteName", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("20f6282d-55f6-4fe8-9929-7b03a6a48fdc"), "Hakkımızda Yazısı", "Adres Bilgisi", new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5302), "tr-TR", "mailadresi@mailadresi.com", new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"), "05515515454", "Klabs Teknoloji", new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5303) },
                    { new Guid("3b35e3d8-5abb-4da0-9ef6-ccf40dbcf0aa"), "Hakkımızda Yazısı", "Adres Bilgisi", new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5288), "en-US", "mailadresi@mailadresi.com", new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"), "05515515454", "Klabs Teknoloji", new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5289) }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreateTime", "OperationClaimId", "UpdateTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06947eb"), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9747), new Guid("d7be8d00-4434-4636-a467-c511ea307018"), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9748), new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb") },
                    { new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9744), new Guid("d7be8d00-4434-4636-a467-c511ea307016"), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9745), new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb") }
                });

            migrationBuilder.InsertData(
                table: "UserPasswords",
                columns: new[] { "Id", "CreateTime", "HashedPassword", "PasswordSalt", "UpdateTime", "UserId" },
                values: new object[] { new Guid("cbe7509a-3e0d-441e-9e61-905742c652d5"), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9724), new byte[] { 33, 156, 91, 219, 2, 252, 18, 248, 1, 212, 106, 193, 46, 29, 22, 164, 26, 65, 189, 190, 20, 122, 133, 47, 73, 10, 108, 20, 156, 119, 108, 145, 40, 62, 15, 90, 122, 107, 70, 249, 171, 232, 247, 207, 224, 31, 232, 72, 135, 153, 226, 202, 90, 8, 243, 124, 20, 61, 127, 60, 73, 13, 118, 6 }, new byte[] { 164, 253, 95, 37, 182, 131, 104, 185, 192, 189, 131, 109, 115, 48, 39, 250, 53, 160, 211, 29, 131, 8, 75, 248, 175, 102, 202, 71, 154, 147, 223, 185, 155, 234, 60, 202, 184, 235, 11, 66, 245, 2, 207, 90, 210, 190, 233, 50, 9, 69, 49, 98, 51, 174, 138, 57, 20, 190, 175, 99, 169, 128, 26, 161, 254, 123, 98, 55, 239, 138, 90, 248, 240, 39, 251, 22, 218, 175, 130, 142, 55, 2, 168, 5, 20, 17, 181, 193, 120, 113, 141, 177, 128, 170, 89, 6, 107, 4, 6, 23, 86, 152, 211, 116, 132, 25, 222, 151, 5, 26, 184, 222, 51, 29, 98, 24, 59, 231, 163, 168, 255, 94, 8, 92, 128, 236, 102, 131 }, new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9725), new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb") });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_MasterId",
                table: "BlogCategory",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogEntity_BlogCategoryId",
                table: "BlogEntity",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogEntity_MasterId",
                table: "BlogEntity",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_City_MasterCountryId",
                table: "City",
                column: "MasterCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_City_MasterId",
                table: "City",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentDetail_MasterId",
                table: "ContentDetail",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_MasterId",
                table: "Country",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBlog_BlogCategoryId",
                table: "MasterBlog",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCity_CountryId",
                table: "MasterCity",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterProgram_MasterProgramId",
                table: "MasterProgram",
                column: "MasterProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterSchool_CityId",
                table: "MasterSchool",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Newsletters_MailAddress",
                table: "Newsletters",
                column: "MailAddress",
                unique: true,
                filter: "[MailAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEntity_MasterId",
                table: "ProgramEntity",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramRelation_ProgramId",
                table: "ProgramRelation",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_School_MasterId",
                table: "School",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_SeoEntity_MasterId",
                table: "SeoEntity",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteConfig_MasterId",
                table: "SiteConfig",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteSection_MasterId",
                table: "SiteSection",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Sss_MasterId",
                table: "Sss",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_MasterId",
                table: "Team",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswords_UserId",
                table: "UserPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserValidationCodes_UserId",
                table: "UserValidationCodes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogEntity");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "ContentDetail");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.DropTable(
                name: "ProgramEntity");

            migrationBuilder.DropTable(
                name: "ProgramRelation");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "SeoEntity");

            migrationBuilder.DropTable(
                name: "SiteConfig");

            migrationBuilder.DropTable(
                name: "SiteSection");

            migrationBuilder.DropTable(
                name: "SocialNetworkEntity");

            migrationBuilder.DropTable(
                name: "Sss");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "UserComment");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "UserPasswords");

            migrationBuilder.DropTable(
                name: "UserValidationCodes");

            migrationBuilder.DropTable(
                name: "MasterBlog");

            migrationBuilder.DropTable(
                name: "MasterContentDetail");

            migrationBuilder.DropTable(
                name: "MasterProgram");

            migrationBuilder.DropTable(
                name: "MasterSchool");

            migrationBuilder.DropTable(
                name: "MasterSeoEntity");

            migrationBuilder.DropTable(
                name: "MasterSiteConfig");

            migrationBuilder.DropTable(
                name: "MasterSiteSection");

            migrationBuilder.DropTable(
                name: "MasterSss");

            migrationBuilder.DropTable(
                name: "MasterTeam");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BlogCategory");

            migrationBuilder.DropTable(
                name: "MasterCity");

            migrationBuilder.DropTable(
                name: "MasterBlogCategory");

            migrationBuilder.DropTable(
                name: "MasterCountry");
        }
    }
}
