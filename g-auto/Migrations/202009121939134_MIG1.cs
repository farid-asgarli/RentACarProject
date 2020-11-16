namespace g_auto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIG1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Phone = c.String(nullable: false, maxLength: 14),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 150),
                        FacebookLink = c.String(),
                        LinkedinLink = c.String(),
                        InstagramLink = c.String(),
                        VimeoLink = c.String(),
                        TwitterLink = c.String(),
                        PinterestLink = c.String(),
                        SkypeLink = c.String(),
                        CoverImageFirst = c.String(maxLength: 150),
                        CoverImageFirstTitle = c.String(maxLength: 50),
                        CoverImageSecondTitle = c.String(maxLength: 50),
                        CoverImageThirdTitle = c.String(maxLength: 50),
                        CoverImageFirstContent = c.String(maxLength: 150),
                        CoverImageSecondContent = c.String(maxLength: 150),
                        CoverImageThirdContent = c.String(maxLength: 150),
                        CoverImageSecond = c.String(maxLength: 150),
                        CoverImageThird = c.String(maxLength: 150),
                        CoverImageFourth = c.String(maxLength: 150),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ProfilePicture = c.String(),
                        Phone = c.String(maxLength: 14),
                        Speciality = c.String(),
                        hasPrivelege = c.Boolean(nullable: false),
                        isBlocked = c.Boolean(nullable: false),
                        EntranceCount = c.Long(nullable: false),
                        LastEntranceTime = c.DateTime(),
                        LastIPAddress = c.String(),
                        ConnectionId = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AdminSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminId = c.Int(nullable: false),
                        alwaysActive = c.Boolean(nullable: false),
                        DisplayName = c.String(nullable: false, maxLength: 125),
                        menuGrouping = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.MessageReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1000),
                        MessageId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        AdminId = c.Int(nullable: false),
                        isReadByTheUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Messages", t => t.MessageId, cascadeDelete: false)
                .Index(t => t.MessageId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 150),
                        Phone = c.String(nullable: false, maxLength: 14),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        PostDate = c.DateTime(nullable: false),
                        isRead = c.Boolean(nullable: false),
                        UserId = c.Int(),
                        isReplied = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 150),
                        Address = c.String(maxLength: 500),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        ProfilePicture = c.String(),
                        Phone = c.String(maxLength: 14),
                        IsRegistered = c.Boolean(nullable: false),
                        isBlocked = c.Boolean(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        EntranceCount = c.Long(nullable: false),
                        LastEntranceTime = c.DateTime(),
                        LastIPAddress = c.String(),
                        ConnectionId = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        PostedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        UpvoteCount = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminId = c.Int(nullable: false),
                        AdminModifiedId = c.Int(),
                        PostDate = c.DateTime(nullable: false),
                        BlogCoverImage = c.String(maxLength: 150),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Description = c.String(nullable: false, storeType: "ntext"),
                        Title = c.String(nullable: false, maxLength: 150),
                        isActive = c.Boolean(nullable: false),
                        enableComments = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ViewCount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.BlogToCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        BlogCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: false)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryId, cascadeDelete: false)
                .Index(t => t.BlogId)
                .Index(t => t.BlogCategoryId);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        AdminId = c.Int(nullable: false),
                        AdminModifiedId = c.Int(),
                        PostDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.BlogToTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: false)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: false)
                .Index(t => t.BlogId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        PostedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        UpvoteCount = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: false)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.BlogId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                        isPending = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isFinished = c.Boolean(nullable: false),
                        isCancelled = c.Boolean(nullable: false),
                        DeliveryAddress = c.String(nullable: false, maxLength: 500),
                        StartDate = c.DateTime(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        CancelledAdminId = c.Int(),
                        CancelledDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.CancelledAdminId)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ModelId)
                .Index(t => t.CancelledAdminId);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Color = c.String(nullable: false, maxLength: 150),
                        Condition = c.String(nullable: false, maxLength: 250),
                        Doors = c.Int(nullable: false),
                        DriveTrain = c.String(nullable: false),
                        Engine = c.Decimal(nullable: false, storeType: "money"),
                        EngineLayout = c.String(),
                        FuelType = c.String(nullable: false, maxLength: 250),
                        HorsePower = c.Int(nullable: false),
                        Mass = c.Decimal(nullable: false, storeType: "money"),
                        Mileage = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        PriceDaily = c.Decimal(nullable: false, storeType: "money"),
                        Seats = c.Int(nullable: false),
                        Transmission = c.String(nullable: false, maxLength: 250),
                        Year = c.Int(nullable: false),
                        hasABS = c.Boolean(nullable: false),
                        hasAlloyWheels = c.Boolean(nullable: false),
                        hasESP = c.Boolean(nullable: false),
                        hasPSensors = c.Boolean(nullable: false),
                        hasConditioner = c.Boolean(nullable: false),
                        hasCC = c.Boolean(nullable: false),
                        hasLeatherInterior = c.Boolean(nullable: false),
                        hasXenon = c.Boolean(nullable: false),
                        Description = c.String(nullable: false, storeType: "ntext"),
                        BrandId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                        ViewCount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: false)
                .Index(t => t.BrandId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Logo = c.String(maxLength: 150),
                        OriginCountry = c.String(),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.ModelImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        ModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: false)
                .Index(t => t.ModelId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        PostedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ReservationId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: false)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ReservationId)
                .Index(t => t.ModelId);
            
            CreateTable(
                "dbo.ModelReviewReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                        PostedDate = c.DateTime(nullable: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Reviews", t => t.ReviewId, cascadeDelete: false)
                .Index(t => t.ReviewId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.ReservationToFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureSetId = c.Int(nullable: false),
                        ReservationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FeatureSets", t => t.FeatureSetId, cascadeDelete: false)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: false)
                .Index(t => t.FeatureSetId)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.FeatureSets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 125),
                        Content = c.String(nullable: false, maxLength: 250),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        PostDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminModifiedId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.ReservationServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        isPending = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isFinished = c.Boolean(nullable: false),
                        isCancelled = c.Boolean(nullable: false),
                        AppDate = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Description = c.String(nullable: false, storeType: "ntext"),
                        ServiceImageFirst = c.String(),
                        ServiceImageSecond = c.String(),
                        ServiceIcon = c.String(),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                        ViewCount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.ServiceBenefits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.ServiceToInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        ServiceInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .ForeignKey("dbo.ServiceInfoes", t => t.ServiceInfoId, cascadeDelete: false)
                .Index(t => t.ServiceId)
                .Index(t => t.ServiceInfoId);
            
            CreateTable(
                "dbo.ServiceInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Title = c.String(maxLength: 120),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.ReviewProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        PostedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        Condition = c.String(nullable: false, maxLength: 250),
                        About = c.String(nullable: false, storeType: "ntext"),
                        Desc = c.String(nullable: false, storeType: "ntext"),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isNewlyAdded = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                        ViewCount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductToCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: false)
                .Index(t => t.ProductId)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CancelledAdminId = c.Int(),
                        CancelledDate = c.DateTime(),
                        ProductId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        PostDate = c.DateTime(nullable: false),
                        OrderNote = c.String(maxLength: 500),
                        IsRefunded = c.Boolean(nullable: false),
                        isCancelled = c.Boolean(nullable: false),
                        isPending = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isFinished = c.Boolean(nullable: false),
                        isRefundRequested = c.Boolean(nullable: false),
                        RefundDate = c.DateTime(),
                        RefundRequestDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.CancelledAdminId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CancelledAdminId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ESTDelivery = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        Address = c.String(maxLength: 500),
                        IsReady = c.Boolean(nullable: false),
                        IsDelivered = c.Boolean(nullable: false),
                        IsCanceled = c.Boolean(nullable: false),
                        SaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: false)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.ProductReviewReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewProductId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                        PostedDate = c.DateTime(nullable: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.ReviewProducts", t => t.ReviewProductId, cascadeDelete: false)
                .Index(t => t.ReviewProductId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        PostedDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AboutBenefits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 100),
                        LayoutId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Layouts", t => t.LayoutId, cascadeDelete: false)
                .Index(t => t.LayoutId);
            
            CreateTable(
                "dbo.Layouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        sliderToggle = c.Boolean(nullable: false),
                        modelToggle = c.Boolean(nullable: false),
                        serviceToggle = c.Boolean(nullable: false),
                        testimonialToggle = c.Boolean(nullable: false),
                        blogToggle = c.Boolean(nullable: false),
                        expertToggle = c.Boolean(nullable: false),
                        promoToggle = c.Boolean(nullable: false),
                        aboutToggle = c.Boolean(nullable: false),
                        contactToggle = c.Boolean(nullable: false),
                        sliderCount = c.Int(),
                        blogCount = c.Int(),
                        serviceCount = c.Int(),
                        modelCount = c.Int(),
                        expertCount = c.Int(),
                        testiominalCount = c.Int(),
                        Logo = c.String(),
                        LogoFooter = c.String(),
                        Promo_Image = c.String(maxLength: 150),
                        About_Image = c.String(maxLength: 150),
                        Signature = c.String(maxLength: 150),
                        About_Title = c.String(maxLength: 150),
                        About_Content = c.String(storeType: "ntext"),
                        About_CEO = c.String(maxLength: 150),
                        Contact_Title = c.String(maxLength: 150),
                        Contact_Content = c.String(storeType: "ntext"),
                        Promo_Content = c.String(storeType: "ntext"),
                        Promo_Link = c.String(maxLength: 500),
                        Phone = c.String(nullable: false, maxLength: 14),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Experts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 150),
                        ExperienceYear = c.Int(nullable: false),
                        ProfilePicture = c.String(maxLength: 150),
                        FacebookProfile = c.String(),
                        InstagramProfile = c.String(),
                        LinkedInProfile = c.String(),
                        TwitterProfile = c.String(),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.GalleryImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Picture = c.String(),
                        AdminId = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.NewsLetters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Subtitle = c.String(),
                        Link = c.String(),
                        Image = c.String(maxLength: 150),
                        Order = c.Int(),
                        PostDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        AdminId = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(),
                        AdminModifiedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.TempAccessCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 128),
                        PostDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vacancies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ViewCount = c.Long(nullable: false),
                        AdminId = c.Int(nullable: false),
                        AdminModifiedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        isActive = c.Boolean(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        CoverImage = c.String(maxLength: 150),
                        Title = c.String(maxLength: 250),
                        Salary = c.String(nullable: false, maxLength: 250),
                        Content = c.String(storeType: "ntext"),
                        Description = c.String(maxLength: 250),
                        Deadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.Admins", t => t.AdminModifiedId)
                .Index(t => t.AdminId)
                .Index(t => t.AdminModifiedId);
            
            CreateTable(
                "dbo.VerificationCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostDate = c.DateTime(nullable: false),
                        VerCode = c.String(maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VerificationCodes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Vacancies", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Vacancies", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Sliders", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Sliders", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.GalleryImages", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.GalleryImages", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Experts", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Experts", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.AboutBenefits", "LayoutId", "dbo.Layouts");
            DropForeignKey("dbo.Abouts", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Abouts", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Testimonials", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReviewProducts", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReviewProducts", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.ProductReviewReplies", "ReviewProductId", "dbo.ReviewProducts");
            DropForeignKey("dbo.ProductReviewReplies", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.ReviewProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropForeignKey("dbo.Shipments", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Sales", "CancelledAdminId", "dbo.Admins");
            DropForeignKey("dbo.ProductToCategories", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategories", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.ProductCategories", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.ProductToCategories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Products", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.ReservationServices", "UserId", "dbo.Users");
            DropForeignKey("dbo.ServiceToInfoes", "ServiceInfoId", "dbo.ServiceInfoes");
            DropForeignKey("dbo.ServiceInfoes", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.ServiceInfoes", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.ServiceToInfoes", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ServiceBenefits", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ReservationServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Services", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReservationToFeatures", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.ReservationToFeatures", "FeatureSetId", "dbo.FeatureSets");
            DropForeignKey("dbo.FeatureSets", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.FeatureSets", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.ModelReviewReplies", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.ModelReviewReplies", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Reviews", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Reservations", "ModelId", "dbo.Models");
            DropForeignKey("dbo.ModelImages", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Brands", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Brands", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Models", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Models", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Reservations", "CancelledAdminId", "dbo.Admins");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Replies", "UserId", "dbo.Users");
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Replies", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.BlogToTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Tags", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Tags", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.BlogToTags", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.BlogToCategories", "BlogCategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogCategories", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.BlogCategories", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.BlogToCategories", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Blogs", "AdminModifiedId", "dbo.Admins");
            DropForeignKey("dbo.Blogs", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.MessageReplies", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.MessageReplies", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.AdminSettings", "AdminId", "dbo.Admins");
            DropIndex("dbo.VerificationCodes", new[] { "UserId" });
            DropIndex("dbo.Vacancies", new[] { "AdminModifiedId" });
            DropIndex("dbo.Vacancies", new[] { "AdminId" });
            DropIndex("dbo.Sliders", new[] { "AdminModifiedId" });
            DropIndex("dbo.Sliders", new[] { "AdminId" });
            DropIndex("dbo.GalleryImages", new[] { "AdminModifiedId" });
            DropIndex("dbo.GalleryImages", new[] { "AdminId" });
            DropIndex("dbo.Experts", new[] { "AdminModifiedId" });
            DropIndex("dbo.Experts", new[] { "AdminId" });
            DropIndex("dbo.AboutBenefits", new[] { "LayoutId" });
            DropIndex("dbo.Testimonials", new[] { "UserId" });
            DropIndex("dbo.ProductReviewReplies", new[] { "AdminId" });
            DropIndex("dbo.ProductReviewReplies", new[] { "ReviewProductId" });
            DropIndex("dbo.Shipments", new[] { "SaleId" });
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropIndex("dbo.Sales", new[] { "CancelledAdminId" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropIndex("dbo.ProductCategories", new[] { "AdminModifiedId" });
            DropIndex("dbo.ProductCategories", new[] { "AdminId" });
            DropIndex("dbo.ProductToCategories", new[] { "ProductCategoryId" });
            DropIndex("dbo.ProductToCategories", new[] { "ProductId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "AdminModifiedId" });
            DropIndex("dbo.Products", new[] { "AdminId" });
            DropIndex("dbo.ReviewProducts", new[] { "SaleId" });
            DropIndex("dbo.ReviewProducts", new[] { "ProductId" });
            DropIndex("dbo.ReviewProducts", new[] { "UserId" });
            DropIndex("dbo.ServiceInfoes", new[] { "AdminModifiedId" });
            DropIndex("dbo.ServiceInfoes", new[] { "AdminId" });
            DropIndex("dbo.ServiceToInfoes", new[] { "ServiceInfoId" });
            DropIndex("dbo.ServiceToInfoes", new[] { "ServiceId" });
            DropIndex("dbo.ServiceBenefits", new[] { "ServiceId" });
            DropIndex("dbo.Services", new[] { "AdminModifiedId" });
            DropIndex("dbo.Services", new[] { "AdminId" });
            DropIndex("dbo.ReservationServices", new[] { "ServiceId" });
            DropIndex("dbo.ReservationServices", new[] { "UserId" });
            DropIndex("dbo.FeatureSets", new[] { "AdminId" });
            DropIndex("dbo.FeatureSets", new[] { "AdminModifiedId" });
            DropIndex("dbo.ReservationToFeatures", new[] { "ReservationId" });
            DropIndex("dbo.ReservationToFeatures", new[] { "FeatureSetId" });
            DropIndex("dbo.ModelReviewReplies", new[] { "AdminId" });
            DropIndex("dbo.ModelReviewReplies", new[] { "ReviewId" });
            DropIndex("dbo.Reviews", new[] { "ModelId" });
            DropIndex("dbo.Reviews", new[] { "ReservationId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.ModelImages", new[] { "ModelId" });
            DropIndex("dbo.Brands", new[] { "AdminModifiedId" });
            DropIndex("dbo.Brands", new[] { "AdminId" });
            DropIndex("dbo.Models", new[] { "AdminModifiedId" });
            DropIndex("dbo.Models", new[] { "AdminId" });
            DropIndex("dbo.Models", new[] { "BrandId" });
            DropIndex("dbo.Reservations", new[] { "CancelledAdminId" });
            DropIndex("dbo.Reservations", new[] { "ModelId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.Replies", new[] { "BlogId" });
            DropIndex("dbo.Replies", new[] { "UserId" });
            DropIndex("dbo.Tags", new[] { "AdminModifiedId" });
            DropIndex("dbo.Tags", new[] { "AdminId" });
            DropIndex("dbo.BlogToTags", new[] { "TagId" });
            DropIndex("dbo.BlogToTags", new[] { "BlogId" });
            DropIndex("dbo.BlogCategories", new[] { "AdminModifiedId" });
            DropIndex("dbo.BlogCategories", new[] { "AdminId" });
            DropIndex("dbo.BlogToCategories", new[] { "BlogCategoryId" });
            DropIndex("dbo.BlogToCategories", new[] { "BlogId" });
            DropIndex("dbo.Blogs", new[] { "AdminModifiedId" });
            DropIndex("dbo.Blogs", new[] { "AdminId" });
            DropIndex("dbo.Comments", new[] { "BlogId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.MessageReplies", new[] { "AdminId" });
            DropIndex("dbo.MessageReplies", new[] { "MessageId" });
            DropIndex("dbo.AdminSettings", new[] { "AdminId" });
            DropIndex("dbo.Abouts", new[] { "AdminModifiedId" });
            DropIndex("dbo.Abouts", new[] { "AdminId" });
            DropTable("dbo.VerificationCodes");
            DropTable("dbo.Vacancies");
            DropTable("dbo.TempAccessCodes");
            DropTable("dbo.Sliders");
            DropTable("dbo.NewsLetters");
            DropTable("dbo.GalleryImages");
            DropTable("dbo.Experts");
            DropTable("dbo.Layouts");
            DropTable("dbo.AboutBenefits");
            DropTable("dbo.Testimonials");
            DropTable("dbo.ProductReviewReplies");
            DropTable("dbo.Shipments");
            DropTable("dbo.Sales");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.ProductToCategories");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Products");
            DropTable("dbo.ReviewProducts");
            DropTable("dbo.ServiceInfoes");
            DropTable("dbo.ServiceToInfoes");
            DropTable("dbo.ServiceBenefits");
            DropTable("dbo.Services");
            DropTable("dbo.ReservationServices");
            DropTable("dbo.FeatureSets");
            DropTable("dbo.ReservationToFeatures");
            DropTable("dbo.ModelReviewReplies");
            DropTable("dbo.Reviews");
            DropTable("dbo.ModelImages");
            DropTable("dbo.Brands");
            DropTable("dbo.Models");
            DropTable("dbo.Reservations");
            DropTable("dbo.Replies");
            DropTable("dbo.Tags");
            DropTable("dbo.BlogToTags");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.BlogToCategories");
            DropTable("dbo.Blogs");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.MessageReplies");
            DropTable("dbo.AdminSettings");
            DropTable("dbo.Admins");
            DropTable("dbo.Abouts");
        }
    }
}
