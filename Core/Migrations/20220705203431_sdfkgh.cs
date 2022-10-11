using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class sdfkgh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteConfig",
                keyColumn: "Id",
                keyValue: new Guid("20f6282d-55f6-4fe8-9929-7b03a6a48fdc"));

            migrationBuilder.DeleteData(
                table: "SiteConfig",
                keyColumn: "Id",
                keyValue: new Guid("3b35e3d8-5abb-4da0-9ef6-ccf40dbcf0aa"));

            migrationBuilder.DeleteData(
                table: "MasterSiteConfig",
                keyColumn: "Id",
                keyValue: new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"));

            migrationBuilder.AddColumn<string>(
                name: "FriendlyName",
                table: "ContentDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MasterSiteConfig",
                columns: new[] { "Id", "CreateTime", "FirstCounterText", "FirstCounterValue", "SecondCounterText", "SecondCounterValue", "ThirdCounterText", "ThirdCounterValue", "UpdateTime" },
                values: new object[] { new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"), new DateTime(2022, 7, 5, 23, 34, 30, 754, DateTimeKind.Local).AddTicks(4870), null, 0, null, 0, null, 0, new DateTime(2022, 7, 5, 23, 34, 30, 754, DateTimeKind.Local).AddTicks(4873) });

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("bdc23d6c-2e03-4b0d-b34a-7535d74abbc8"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(8784), new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(8784) });

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d7be8d00-4434-4636-a467-c511ea307016"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(8769), new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(8777) });

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d7be8d00-4434-4636-a467-c511ea307018"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(8781), new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(8781) });

            migrationBuilder.UpdateData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06947eb"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(9773), new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(9774) });

            migrationBuilder.UpdateData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(9770), new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "UserPasswords",
                keyColumn: "Id",
                keyValue: new Guid("cbe7509a-3e0d-441e-9e61-905742c652d5"),
                columns: new[] { "CreateTime", "HashedPassword", "PasswordSalt", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(9754), new byte[] { 142, 5, 223, 254, 155, 156, 53, 144, 30, 48, 151, 21, 195, 172, 58, 136, 188, 64, 54, 48, 40, 66, 187, 46, 82, 241, 8, 34, 72, 230, 237, 185, 227, 204, 168, 130, 165, 162, 216, 165, 237, 66, 255, 64, 113, 10, 250, 6, 232, 18, 85, 221, 184, 180, 134, 209, 11, 254, 134, 119, 162, 188, 31, 5 }, new byte[] { 252, 90, 187, 43, 18, 185, 46, 244, 76, 37, 211, 112, 144, 66, 33, 50, 202, 130, 139, 0, 7, 219, 101, 170, 210, 167, 1, 72, 79, 49, 22, 96, 212, 26, 102, 41, 86, 73, 50, 104, 189, 71, 45, 87, 55, 246, 186, 222, 155, 153, 134, 127, 123, 233, 83, 50, 73, 224, 139, 141, 66, 142, 78, 234, 21, 13, 233, 131, 89, 239, 203, 111, 81, 189, 46, 106, 101, 131, 100, 174, 245, 77, 232, 112, 47, 94, 96, 244, 243, 136, 111, 228, 176, 150, 54, 30, 183, 142, 172, 35, 13, 63, 27, 66, 132, 169, 17, 114, 214, 47, 255, 51, 85, 137, 70, 88, 111, 59, 255, 22, 116, 204, 72, 71, 100, 193, 64, 171 }, new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(9754) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(8929), new DateTime(2022, 7, 5, 23, 34, 30, 753, DateTimeKind.Local).AddTicks(8930) });

            migrationBuilder.InsertData(
                table: "SiteConfig",
                columns: new[] { "Id", "AboutUs", "Address", "CreateTime", "Lang", "MailAddress", "MasterId", "PhoneNumber", "SiteName", "UpdateTime" },
                values: new object[] { new Guid("20f6282d-55f6-4fe8-9929-7b03a6a48fdc"), "Hakkımızda Yazısı", "Adres Bilgisi", new DateTime(2022, 7, 5, 23, 34, 30, 754, DateTimeKind.Local).AddTicks(4910), "tr-TR", "mailadresi@mailadresi.com", new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"), "05515515454", "Klabs Teknoloji", new DateTime(2022, 7, 5, 23, 34, 30, 754, DateTimeKind.Local).AddTicks(4910) });

            migrationBuilder.InsertData(
                table: "SiteConfig",
                columns: new[] { "Id", "AboutUs", "Address", "CreateTime", "Lang", "MailAddress", "MasterId", "PhoneNumber", "SiteName", "UpdateTime" },
                values: new object[] { new Guid("3b35e3d8-5abb-4da0-9ef6-ccf40dbcf0aa"), "Hakkımızda Yazısı", "Adres Bilgisi", new DateTime(2022, 7, 5, 23, 34, 30, 754, DateTimeKind.Local).AddTicks(4898), "en-US", "mailadresi@mailadresi.com", new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"), "05515515454", "Klabs Teknoloji", new DateTime(2022, 7, 5, 23, 34, 30, 754, DateTimeKind.Local).AddTicks(4898) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendlyName",
                table: "ContentDetail");

            migrationBuilder.UpdateData(
                table: "MasterSiteConfig",
                keyColumn: "Id",
                keyValue: new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5261), new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5264) });

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("bdc23d6c-2e03-4b0d-b34a-7535d74abbc8"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8794), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8794) });

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d7be8d00-4434-4636-a467-c511ea307016"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8779), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8789) });

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d7be8d00-4434-4636-a467-c511ea307018"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8791), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8792) });

            migrationBuilder.UpdateData(
                table: "SiteConfig",
                keyColumn: "Id",
                keyValue: new Guid("20f6282d-55f6-4fe8-9929-7b03a6a48fdc"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5302), new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5303) });

            migrationBuilder.UpdateData(
                table: "SiteConfig",
                keyColumn: "Id",
                keyValue: new Guid("3b35e3d8-5abb-4da0-9ef6-ccf40dbcf0aa"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5288), new DateTime(2022, 7, 3, 15, 11, 31, 191, DateTimeKind.Local).AddTicks(5289) });

            migrationBuilder.UpdateData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06947eb"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9747), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9748) });

            migrationBuilder.UpdateData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9744), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9745) });

            migrationBuilder.UpdateData(
                table: "UserPasswords",
                keyColumn: "Id",
                keyValue: new Guid("cbe7509a-3e0d-441e-9e61-905742c652d5"),
                columns: new[] { "CreateTime", "HashedPassword", "PasswordSalt", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9724), new byte[] { 33, 156, 91, 219, 2, 252, 18, 248, 1, 212, 106, 193, 46, 29, 22, 164, 26, 65, 189, 190, 20, 122, 133, 47, 73, 10, 108, 20, 156, 119, 108, 145, 40, 62, 15, 90, 122, 107, 70, 249, 171, 232, 247, 207, 224, 31, 232, 72, 135, 153, 226, 202, 90, 8, 243, 124, 20, 61, 127, 60, 73, 13, 118, 6 }, new byte[] { 164, 253, 95, 37, 182, 131, 104, 185, 192, 189, 131, 109, 115, 48, 39, 250, 53, 160, 211, 29, 131, 8, 75, 248, 175, 102, 202, 71, 154, 147, 223, 185, 155, 234, 60, 202, 184, 235, 11, 66, 245, 2, 207, 90, 210, 190, 233, 50, 9, 69, 49, 98, 51, 174, 138, 57, 20, 190, 175, 99, 169, 128, 26, 161, 254, 123, 98, 55, 239, 138, 90, 248, 240, 39, 251, 22, 218, 175, 130, 142, 55, 2, 168, 5, 20, 17, 181, 193, 120, 113, 141, 177, 128, 170, 89, 6, 107, 4, 6, 23, 86, 152, 211, 116, 132, 25, 222, 151, 5, 26, 184, 222, 51, 29, 98, 24, 59, 231, 163, 168, 255, 94, 8, 92, 128, 236, 102, 131 }, new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(9725) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5073b7b-4c66-4b12-ae40-e3f9e06949eb"),
                columns: new[] { "CreateTime", "UpdateTime" },
                values: new object[] { new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8860), new DateTime(2022, 7, 3, 15, 11, 31, 190, DateTimeKind.Local).AddTicks(8861) });
        }
    }
}
