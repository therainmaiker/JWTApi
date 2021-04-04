using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtapitoken.Migrations
{
    public partial class seedrolestotableAspNetRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(

                table:"AspNetRoles" ,
                columns: new []{"Id","Name","NormalizedName","ConcurrencyStamp"},
                values:new object[] {Guid.NewGuid().ToString() ,"Student","Student".ToUpper(),Guid.NewGuid().ToString()}

            );


            migrationBuilder.InsertData(

               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() }

           );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
