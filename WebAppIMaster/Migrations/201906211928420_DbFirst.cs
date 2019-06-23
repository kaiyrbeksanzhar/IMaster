namespace WebAppIMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddExecutorToOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        OrderId = c.Int(nullable: false),
                        ExecutorId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        OrderNumber = c.String(),
                        Description = c.String(),
                        Photo1Url = c.String(),
                        Photo2Url = c.String(),
                        Photo3Url = c.String(),
                        Photo4Url = c.String(),
                        StartDateType = c.Int(nullable: false),
                        StartedDate = c.DateTime(nullable: false),
                        CostType = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderState = c.Int(nullable: false),
                        NewNotifications = c.Int(nullable: false),
                        OrderType = c.Int(nullable: false),
                        InCityId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        ReceiveOnlyResponses = c.Boolean(nullable: false),
                        PayWithBounce = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                        ExecutorId = c.String(maxLength: 128),
                        EndedDateTime = c.DateTime(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        Bonus = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ViewCount = c.Int(nullable: false),
                        CustomerReview = c.String(),
                        CustomerReviewedDateTime = c.DateTime(nullable: false),
                        ExecutorComment = c.String(),
                        ExecutorCommentedDateTime = c.DateTime(nullable: false),
                        EvaluationPerformerWork = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.InCityId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Executors", t => t.ExecutorId)
                .Index(t => t.InCityId)
                .Index(t => t.AddressId)
                .Index(t => t.CategoryId)
                .Index(t => t.CustomerId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Priority = c.Int(nullable: false),
                        UrlPhoto = c.String(),
                        Executor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Executors", t => t.Executor_Id)
                .Index(t => t.Executor_Id);
            
            CreateTable(
                "dbo.ExecutorSpecializations",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        SpecializationId = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.SpecializationId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.ExecutorId)
                .Index(t => t.SpecializationId)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Executors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BirthDay = c.DateTime(precision: 7, storeType: "datetime2"),
                        Gender = c.Int(),
                        ExecutorCheck = c.Boolean(),
                        PhoneNumber = c.String(),
                        ExecutorType = c.Int(),
                        ExecutorStatus = c.Int(),
                        Rating = c.Int(),
                        AvatarUrl = c.String(),
                        RegistrationDateTime = c.DateTime(),
                        YouTubeVideoUrl = c.String(),
                        Banned = c.Boolean(),
                        BannedDateTime = c.DateTime(),
                        OrganizationName = c.String(),
                        CityId = c.Int(),
                        ExecotorOnline = c.Boolean(),
                        ExecutorClosedOrdersCount = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.String(),
                        Longitudey = c.String(),
                        Preority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        FirstName = c.String(),
                        FatherName = c.String(),
                        Bonus = c.Decimal(precision: 18, scale: 2),
                        PhoneNumber = c.String(),
                        AvatarUrl = c.String(),
                        Status = c.Int(),
                        Gender = c.String(),
                        HowDidYouAboutUsId = c.Int(),
                        InCityId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.HowDidYouAboutUs", t => t.HowDidYouAboutUsId)
                .ForeignKey("dbo.Cities", t => t.InCityId)
                .Index(t => t.Id)
                .Index(t => t.HowDidYouAboutUsId)
                .Index(t => t.InCityId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        FirstName = c.String(),
                        FatherName = c.String(),
                        GenderId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
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
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                "dbo.UserDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Url = c.String(),
                        Verified = c.Boolean(nullable: false),
                        AddedDateTime = c.DateTime(nullable: false),
                        VerifiedDateTime = c.DateTime(),
                        OwnerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
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
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Lastname = c.String(),
                        Firstname = c.String(),
                        Fathername = c.String(),
                        AvatarUrl = c.String(),
                        AccomodationCityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.AccomodationCityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.AccomodationCityId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.HowDidYouAboutUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Order = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HowDidYouAboutUsLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceName = c.String(),
                        Langcode = c.String(),
                        HowDidYouAboutUsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HowDidYouAboutUs", t => t.HowDidYouAboutUsId, cascadeDelete: true)
                .Index(t => t.HowDidYouAboutUsId);
            
            CreateTable(
                "dbo.CityLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Langcode = c.String(),
                        Name = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogotypeUrl = c.String(),
                        Costs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YouTubeVideoUrl = c.String(),
                        PhoneNumber = c.String(),
                        ExtraPhoneNumber = c.String(),
                        SiteUrl = c.String(),
                        Email = c.String(),
                        Longitude = c.String(),
                        latitude = c.String(),
                        MarketCategory = c.String(),
                        Address = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.IPOrganizationPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Type = c.Int(nullable: false),
                        FromPrice = c.String(),
                        ToPrice = c.String(),
                        TarifType = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.IPOrganizationSalesAndDiscounts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PhotoUrl = c.String(),
                        Priroty = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.IPPhotosFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoUrl = c.String(),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.OrganizationLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Langcode = c.String(),
                        Name = c.String(),
                        ShortDescription = c.String(),
                        BannerUrl = c.String(),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.OrganizationPromotionAndDiscounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BannerUrl = c.String(),
                        ViewCount = c.Int(nullable: false),
                        ClickCount = c.Int(nullable: false),
                        CallCount = c.Int(nullable: false),
                        OrganizationCardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationCardId, cascadeDelete: true)
                .Index(t => t.OrganizationCardId);
            
            CreateTable(
                "dbo.ExecutorPassportFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        ExecutorId = c.Int(nullable: false),
                        Executor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Executors", t => t.Executor_Id)
                .Index(t => t.Executor_Id);
            
            CreateTable(
                "dbo.ExecutorPhotoFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoFileUrl = c.String(),
                        Status = c.Int(nullable: false),
                        ExecutorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Executors", t => t.ExecutorId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.ExecutorServices",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        ServiceId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        CostType = c.Int(nullable: false),
                        Name = c.String(),
                        FromCost = c.Int(nullable: false),
                        ToCost = c.Int(nullable: false),
                        FixedCost = c.Int(nullable: false),
                        UnitMeasurementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.ServiceId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.UnitMeasurements", t => t.UnitMeasurementId, cascadeDelete: true)
                .Index(t => t.ExecutorId)
                .Index(t => t.ServiceId)
                .Index(t => t.UnitMeasurementId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Langcode = c.String(),
                        Name = c.String(),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.UnitMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitMeasurementLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Langcode = c.String(),
                        Name = c.String(),
                        UnitMeasurementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UnitMeasurements", t => t.UnitMeasurementId, cascadeDelete: true)
                .Index(t => t.UnitMeasurementId);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Priority = c.Int(nullable: false),
                        PhotoUrl = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Executor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Executors", t => t.Executor_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.Executor_Id);
            
            CreateTable(
                "dbo.SpecializationLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Langcode = c.String(),
                        SpecializationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .Index(t => t.SpecializationId);
            
            CreateTable(
                "dbo.CategoryLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Langcode = c.String(),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AddressLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Langcode = c.String(),
                        Name = c.String(),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        BookMarkUserId = c.String(nullable: false, maxLength: 128),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookMarkUserId, t.OrderId });
            
            CreateTable(
                "dbo.CallToClients",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        OrderExecutorId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                        OrderExecutor_OrderId = c.Int(),
                        OrderExecutor_CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.OrderExecutorId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.OrderExecutors", t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId })
                .Index(t => t.ExecutorId)
                .Index(t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId });
            
            CreateTable(
                "dbo.OrderExecutors",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        CreateAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.CustomerId });
            
            CreateTable(
                "dbo.CancelExecutorResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        OrderId = c.Int(nullable: false),
                        ExecutorId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CancelOrders",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        OrderExecutorId = c.Int(nullable: false),
                        cancelType = c.Int(nullable: false),
                        CanceledDateTime = c.DateTime(nullable: false),
                        OrderExecutor_OrderId = c.Int(),
                        OrderExecutor_CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.OrderExecutorId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.OrderExecutors", t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId })
                .Index(t => t.ExecutorId)
                .Index(t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId });
            
            CreateTable(
                "dbo.PriceCategoryAndSpecializationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceCategoryAndSpecializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceCategoryAndSpecializationTypeId = c.Int(nullable: false),
                        CategoryId = c.Int(),
                        SpecializationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceCategoryAndSpecializationTypes", t => t.PriceCategoryAndSpecializationTypeId, cascadeDelete: true)
                .Index(t => t.PriceCategoryAndSpecializationTypeId);
            
            CreateTable(
                "dbo.ClientPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        MarketId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExecutorPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecutorId = c.String(),
                        MarketId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ViewsNumber = c.Int(nullable: false),
                        PublishedDateTime = c.DateTime(nullable: false),
                        PhotoUrl1 = c.String(),
                        PhotoUrl2Kazakh = c.String(),
                        TypeRoles = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Langcode = c.String(),
                        Title = c.String(),
                        ShortText = c.String(),
                        Description = c.String(),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperationName = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneCheckingCodes",
                c => new
                    {
                        PhoneNumber = c.String(nullable: false, maxLength: 128),
                        CheckingCode = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PhoneNumber);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ExecutorId = c.String(nullable: false, maxLength: 128),
                        OrderExecutorId = c.Int(nullable: false),
                        CreatedAt_Date = c.DateTime(nullable: false),
                        responseComment = c.String(),
                        OrderExecutor_OrderId = c.Int(),
                        OrderExecutor_CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.OrderExecutorId })
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.OrderExecutors", t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId })
                .Index(t => t.ExecutorId)
                .Index(t => new { t.OrderExecutor_OrderId, t.OrderExecutor_CustomerId });
            
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
                "dbo.SendBonus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bonus = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        CustomerId = c.Int(),
                        ExecutorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ExecutorProfileId = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Relevance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExecutorProfileId, t.StartDateTime, t.EndDateTime });
            
            CreateTable(
                "dbo.TransitionExecutorCosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransitionCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Responses", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors");
            DropForeignKey("dbo.Responses", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.NewsLangs", "NewsId", "dbo.News");
            DropForeignKey("dbo.PriceCategoryAndSpecializations", "PriceCategoryAndSpecializationTypeId", "dbo.PriceCategoryAndSpecializationTypes");
            DropForeignKey("dbo.CancelOrders", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors");
            DropForeignKey("dbo.CancelOrders", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.CallToClients", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" }, "dbo.OrderExecutors");
            DropForeignKey("dbo.CallToClients", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.AddressLangs", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.CategoryLangs", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ExecutorSpecializations", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ExecutorSpecializations", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Executors", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Specializations", "Executor_Id", "dbo.Executors");
            DropForeignKey("dbo.SpecializationLangs", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Specializations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.UnitMeasurementLangs", "UnitMeasurementId", "dbo.UnitMeasurements");
            DropForeignKey("dbo.ExecutorServices", "UnitMeasurementId", "dbo.UnitMeasurements");
            DropForeignKey("dbo.ServiceLangs", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ExecutorServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ExecutorServices", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.CustomerOrders", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.ExecutorSpecializations", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.ExecutorPhotoFiles", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.ExecutorPassportFiles", "Executor_Id", "dbo.Executors");
            DropForeignKey("dbo.OrganizationPromotionAndDiscounts", "OrganizationCardId", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationLangs", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.IPPhotosFiles", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.IPOrganizationSalesAndDiscounts", "Id", "dbo.Organizations");
            DropForeignKey("dbo.IPOrganizationPrices", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "CityId", "dbo.Cities");
            DropForeignKey("dbo.CityLangs", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Executors", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Customers", "InCityId", "dbo.Cities");
            DropForeignKey("dbo.HowDidYouAboutUsLangs", "HowDidYouAboutUsId", "dbo.HowDidYouAboutUs");
            DropForeignKey("dbo.Customers", "HowDidYouAboutUsId", "dbo.HowDidYouAboutUs");
            DropForeignKey("dbo.CustomerOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserProfiles", "AccomodationCityId", "dbo.Cities");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserDocuments", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerOrders", "InCityId", "dbo.Cities");
            DropForeignKey("dbo.Categories", "Executor_Id", "dbo.Executors");
            DropForeignKey("dbo.CustomerOrders", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CustomerOrders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Responses", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            DropIndex("dbo.Responses", new[] { "ExecutorId" });
            DropIndex("dbo.NewsLangs", new[] { "NewsId" });
            DropIndex("dbo.PriceCategoryAndSpecializations", new[] { "PriceCategoryAndSpecializationTypeId" });
            DropIndex("dbo.CancelOrders", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            DropIndex("dbo.CancelOrders", new[] { "ExecutorId" });
            DropIndex("dbo.CallToClients", new[] { "OrderExecutor_OrderId", "OrderExecutor_CustomerId" });
            DropIndex("dbo.CallToClients", new[] { "ExecutorId" });
            DropIndex("dbo.AddressLangs", new[] { "AddressId" });
            DropIndex("dbo.CategoryLangs", new[] { "CategoryId" });
            DropIndex("dbo.SpecializationLangs", new[] { "SpecializationId" });
            DropIndex("dbo.Specializations", new[] { "Executor_Id" });
            DropIndex("dbo.Specializations", new[] { "CategoryId" });
            DropIndex("dbo.UnitMeasurementLangs", new[] { "UnitMeasurementId" });
            DropIndex("dbo.ServiceLangs", new[] { "ServiceId" });
            DropIndex("dbo.ExecutorServices", new[] { "UnitMeasurementId" });
            DropIndex("dbo.ExecutorServices", new[] { "ServiceId" });
            DropIndex("dbo.ExecutorServices", new[] { "ExecutorId" });
            DropIndex("dbo.ExecutorPhotoFiles", new[] { "ExecutorId" });
            DropIndex("dbo.ExecutorPassportFiles", new[] { "Executor_Id" });
            DropIndex("dbo.OrganizationPromotionAndDiscounts", new[] { "OrganizationCardId" });
            DropIndex("dbo.OrganizationLangs", new[] { "OrganizationId" });
            DropIndex("dbo.IPPhotosFiles", new[] { "OrganizationId" });
            DropIndex("dbo.IPOrganizationSalesAndDiscounts", new[] { "Id" });
            DropIndex("dbo.IPOrganizationPrices", new[] { "OrganizationId" });
            DropIndex("dbo.Organizations", new[] { "CityId" });
            DropIndex("dbo.CityLangs", new[] { "CityId" });
            DropIndex("dbo.HowDidYouAboutUsLangs", new[] { "HowDidYouAboutUsId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.UserProfiles", new[] { "AccomodationCityId" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.UserDocuments", new[] { "OwnerId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Customers", new[] { "InCityId" });
            DropIndex("dbo.Customers", new[] { "HowDidYouAboutUsId" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropIndex("dbo.Executors", new[] { "CityId" });
            DropIndex("dbo.Executors", new[] { "Id" });
            DropIndex("dbo.ExecutorSpecializations", new[] { "Category_Id" });
            DropIndex("dbo.ExecutorSpecializations", new[] { "SpecializationId" });
            DropIndex("dbo.ExecutorSpecializations", new[] { "ExecutorId" });
            DropIndex("dbo.Categories", new[] { "Executor_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "ExecutorId" });
            DropIndex("dbo.CustomerOrders", new[] { "CustomerId" });
            DropIndex("dbo.CustomerOrders", new[] { "CategoryId" });
            DropIndex("dbo.CustomerOrders", new[] { "AddressId" });
            DropIndex("dbo.CustomerOrders", new[] { "InCityId" });
            DropTable("dbo.TransitionExecutorCosts");
            DropTable("dbo.Shifts");
            DropTable("dbo.SendBonus");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Responses");
            DropTable("dbo.PhoneCheckingCodes");
            DropTable("dbo.Payments");
            DropTable("dbo.NewsLangs");
            DropTable("dbo.News");
            DropTable("dbo.ExecutorPhones");
            DropTable("dbo.ClientPhones");
            DropTable("dbo.PriceCategoryAndSpecializations");
            DropTable("dbo.PriceCategoryAndSpecializationTypes");
            DropTable("dbo.CancelOrders");
            DropTable("dbo.CancelExecutorResponses");
            DropTable("dbo.OrderExecutors");
            DropTable("dbo.CallToClients");
            DropTable("dbo.Bookmarks");
            DropTable("dbo.AddressLangs");
            DropTable("dbo.CategoryLangs");
            DropTable("dbo.SpecializationLangs");
            DropTable("dbo.Specializations");
            DropTable("dbo.UnitMeasurementLangs");
            DropTable("dbo.UnitMeasurements");
            DropTable("dbo.ServiceLangs");
            DropTable("dbo.Services");
            DropTable("dbo.ExecutorServices");
            DropTable("dbo.ExecutorPhotoFiles");
            DropTable("dbo.ExecutorPassportFiles");
            DropTable("dbo.OrganizationPromotionAndDiscounts");
            DropTable("dbo.OrganizationLangs");
            DropTable("dbo.IPPhotosFiles");
            DropTable("dbo.IPOrganizationSalesAndDiscounts");
            DropTable("dbo.IPOrganizationPrices");
            DropTable("dbo.Organizations");
            DropTable("dbo.CityLangs");
            DropTable("dbo.HowDidYouAboutUsLangs");
            DropTable("dbo.HowDidYouAboutUs");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.UserDocuments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Customers");
            DropTable("dbo.Cities");
            DropTable("dbo.Executors");
            DropTable("dbo.ExecutorSpecializations");
            DropTable("dbo.Categories");
            DropTable("dbo.CustomerOrders");
            DropTable("dbo.Addresses");
            DropTable("dbo.AddExecutorToOrders");
        }
    }
}
