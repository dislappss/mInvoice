namespace mInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Clients",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            clientname = c.String(nullable: false),
            //            email = c.String(nullable: false),
            //            owner = c.String(),
            //            street = c.String(),
            //            zip = c.String(),
            //            city = c.String(),
            //            countriesid = c.Int(),
            //            phone = c.String(),
            //            fax = c.String(),
            //            www = c.String(),
            //            ustd_id = c.String(),
            //            finance_office = c.String(),
            //            account_number = c.String(),
            //            bank_name = c.String(),
            //            iban = c.String(),
            //            bic = c.String(),
            //            picture = c.Binary(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Countries", t => t.countriesid)
            //    .Index(t => t.countriesid);
            
            //CreateTable(
            //    "dbo.Countries",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            name = c.String(nullable: false),
            //            code = c.String(nullable: false),
            //            active = c.Boolean(nullable: false),
            //            CreatedAt = c.DateTime(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Customers",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            clientsysid = c.Int(nullable: false),
            //            payment_methodid = c.Int(),
            //            customer_no = c.String(),
            //            customer_name = c.String(nullable: false),
            //            countriesid = c.Int(nullable: false),
            //            email = c.String(nullable: false),
            //            zip = c.String(nullable: false),
            //            city = c.String(nullable: false),
            //            street = c.String(nullable: false),
            //            countriesid_invoice = c.Int(),
            //            email_invoice = c.String(),
            //            zip_invoice = c.String(),
            //            city_invoice = c.String(),
            //            street_invoice = c.String(),
            //            phone = c.String(),
            //            fax = c.String(),
            //            phone_2 = c.String(),
            //            www = c.String(),
            //            ustd_id = c.String(),
            //            finance_office = c.String(),
            //            account_number = c.String(),
            //            bank_name = c.String(),
            //            iban = c.String(),
            //            bic = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedAt = c.DateTime(),
            //            Clients_Id = c.Int(),
            //            Countries1_Id = c.Int(),
            //            Countries_Id = c.Int(),
            //            Countries_Id1 = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Clients", t => t.Clients_Id)
            //    .ForeignKey("dbo.Countries", t => t.countriesid, cascadeDelete: true)
            //    .ForeignKey("dbo.Countries", t => t.Countries1_Id)
            //    .ForeignKey("dbo.Payment_method", t => t.payment_methodid)
            //    .ForeignKey("dbo.Countries", t => t.Countries_Id)
            //    .ForeignKey("dbo.Countries", t => t.Countries_Id1)
            //    .Index(t => t.payment_methodid)
            //    .Index(t => t.countriesid)
            //    .Index(t => t.Clients_Id)
            //    .Index(t => t.Countries1_Id)
            //    .Index(t => t.Countries_Id)
            //    .Index(t => t.Countries_Id1);
            
            //CreateTable(
            //    "dbo.Invoice_header",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            invoice_no = c.String(nullable: false),
            //            order_date = c.DateTime(nullable: false),
            //            delivery_date = c.DateTime(nullable: false),
            //            customers_id = c.Int(nullable: false),
            //            customer_reference = c.String(),
            //            countriesid = c.Int(nullable: false),
            //            zip = c.String(nullable: false),
            //            city = c.String(nullable: false),
            //            street = c.String(nullable: false),
            //            CreatedAt = c.DateTime(),
            //            UpdatedAt = c.DateTime(),
            //            quantity_2_column_name = c.String(),
            //            quantity_3_column_name = c.String(),
            //            BirthdateDay = c.Int(nullable: false),
            //            BirthdateMonth = c.Int(nullable: false),
            //            BirthdateYear = c.Int(nullable: false),
            //            Customers_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Countries", t => t.countriesid, cascadeDelete: true)
            //    .ForeignKey("dbo.Customers", t => t.Customers_Id)
            //    .Index(t => t.countriesid)
            //    .Index(t => t.Customers_Id);
            
            //CreateTable(
            //    "dbo.Invoice_details",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            invoice_header_id = c.Int(nullable: false),
            //            article_id = c.Int(nullable: false),
            //            description = c.String(),
            //            tax_rate_id = c.Int(nullable: false),
            //            quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            quantity_2 = c.Decimal(precision: 18, scale: 2),
            //            quantity_3 = c.Decimal(precision: 18, scale: 2),
            //            price_netto = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            discount = c.Decimal(precision: 18, scale: 2),
            //            CreatedAt = c.DateTime(),
            //            UpdatedAt = c.DateTime(),
            //            Articles_Id = c.Int(),
            //            Tax_rates_Id = c.Int(),
            //            Invoice_header_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Articles", t => t.Articles_Id)
            //    .ForeignKey("dbo.Tax_rates", t => t.Tax_rates_Id)
            //    .ForeignKey("dbo.Invoice_header", t => t.Invoice_header_Id)
            //    .Index(t => t.Articles_Id)
            //    .Index(t => t.Tax_rates_Id)
            //    .Index(t => t.Invoice_header_Id);
            
            //CreateTable(
            //    "dbo.Articles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            article_no = c.String(),
            //            price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            description = c.String(),
            //            tax_rate_id = c.Int(nullable: false),
            //            CreatedAt = c.DateTime(),
            //            UpdatedAt = c.DateTime(),
            //            Tax_rates_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Tax_rates", t => t.Tax_rates_Id)
            //    .Index(t => t.Tax_rates_Id);
            
            //CreateTable(
            //    "dbo.Tax_rates",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            description = c.String(nullable: false),
            //            code = c.String(nullable: false),
            //            value = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            CreatedAt = c.DateTime(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Payment_method",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            name = c.String(nullable: false),
            //            code = c.String(nullable: false),
            //            CreatedAt = c.DateTime(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Customers", "Countries_Id1", "dbo.Countries");
            DropForeignKey("dbo.Customers", "Countries_Id", "dbo.Countries");
            DropForeignKey("dbo.Customers", "payment_methodid", "dbo.Payment_method");
            DropForeignKey("dbo.Invoice_details", "Invoice_header_Id", "dbo.Invoice_header");
            DropForeignKey("dbo.Invoice_details", "Tax_rates_Id", "dbo.Tax_rates");
            DropForeignKey("dbo.Articles", "Tax_rates_Id", "dbo.Tax_rates");
            DropForeignKey("dbo.Invoice_details", "Articles_Id", "dbo.Articles");
            DropForeignKey("dbo.Invoice_header", "Customers_Id", "dbo.Customers");
            DropForeignKey("dbo.Invoice_header", "countriesid", "dbo.Countries");
            DropForeignKey("dbo.Customers", "Countries1_Id", "dbo.Countries");
            DropForeignKey("dbo.Customers", "countriesid", "dbo.Countries");
            DropForeignKey("dbo.Customers", "Clients_Id", "dbo.Clients");
            DropForeignKey("dbo.Clients", "countriesid", "dbo.Countries");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Articles", new[] { "Tax_rates_Id" });
            DropIndex("dbo.Invoice_details", new[] { "Invoice_header_Id" });
            DropIndex("dbo.Invoice_details", new[] { "Tax_rates_Id" });
            DropIndex("dbo.Invoice_details", new[] { "Articles_Id" });
            DropIndex("dbo.Invoice_header", new[] { "Customers_Id" });
            DropIndex("dbo.Invoice_header", new[] { "countriesid" });
            DropIndex("dbo.Customers", new[] { "Countries_Id1" });
            DropIndex("dbo.Customers", new[] { "Countries_Id" });
            DropIndex("dbo.Customers", new[] { "Countries1_Id" });
            DropIndex("dbo.Customers", new[] { "Clients_Id" });
            DropIndex("dbo.Customers", new[] { "countriesid" });
            DropIndex("dbo.Customers", new[] { "payment_methodid" });
            DropIndex("dbo.Clients", new[] { "countriesid" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Payment_method");
            DropTable("dbo.Tax_rates");
            DropTable("dbo.Articles");
            DropTable("dbo.Invoice_details");
            DropTable("dbo.Invoice_header");
            DropTable("dbo.Customers");
            DropTable("dbo.Countries");
            DropTable("dbo.Clients");
        }
    }
}
