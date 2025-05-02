using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailWarehouse.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageUserForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Users_RecipientId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Users_SenderId",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User1");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "User1",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "User1",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "User1",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User1",
                table: "User1",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_User1_RecipientId",
                table: "Packages",
                column: "RecipientId",
                principalTable: "User1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_User1_SenderId",
                table: "Packages",
                column: "SenderId",
                principalTable: "User1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_User1_RecipientId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_User1_SenderId",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User1",
                table: "User1");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User1");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "User1");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "User1");

            migrationBuilder.RenameTable(
                name: "User1",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Users_RecipientId",
                table: "Packages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Users_SenderId",
                table: "Packages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
