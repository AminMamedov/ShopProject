using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class ShowInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"create view vw_Invoices
                                    as
                                    select i.id ,i.UserId,u.username,u.DeliveryAddress,i.ProductName ,i.ProductCount ,i.TotalPrice,i.CreateTime from Invoices as i
                                    join Users as u
                                    on
                                    i.UserId = u.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"drop view vw_Invoices");
        }
    }
}
