using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace College.Database.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AcademicItems",
                table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_AcademicItems", x => x.ItemId); });

            migrationBuilder.CreateTable(
                "Academics",
                table => new
                {
                    AcademicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Academics", x => x.AcademicId); });

            migrationBuilder.CreateTable(
                "AppSettings",
                table => new
                {
                    SettingsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientCode = table.Column<string>(nullable: true),
                    ProjectCode = table.Column<string>(nullable: true),
                    Certificate = table.Column<string>(nullable: true),
                    License = table.Column<string>(nullable: true),
                    OrganizationName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AppSettings", x => x.SettingsId); });

            migrationBuilder.CreateTable(
                "Carousel",
                table => new
                {
                    CarouselId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Carousel", x => x.CarouselId); });

            migrationBuilder.CreateTable(
                "Downloads",
                table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    FileLink = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Downloads", x => x.FileId); });

            migrationBuilder.CreateTable(
                "Email",
                table => new
                {
                    EmailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAvailable = table.Column<bool>(nullable: true, defaultValue: true),
                    IsMasterEmail = table.Column<bool>(nullable: true, defaultValue: true),
                    MailServer = table.Column<string>(nullable: true, defaultValue: "smtp.gmail.com"),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    From = table.Column<string>(nullable: false,
                        defaultValue: "Shahid Bishnu Dhani Memorial Polytechnic Institute"),
                    SmtpPort = table.Column<int>(nullable: false, defaultValue: 465),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Email", x => x.EmailId); });

            migrationBuilder.CreateTable(
                "Footer",
                table => new
                {
                    FooterHeaderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slogan = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false),
                    ContactAddress = table.Column<string>(nullable: false),
                    FacebookLink = table.Column<string>(nullable: true),
                    TweeterLink = table.Column<string>(nullable: true),
                    InstaGramLink = table.Column<string>(nullable: true),
                    GooglePlusLink = table.Column<string>(nullable: true),
                    YoutubeLink = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Footer", x => x.FooterHeaderId); });

            migrationBuilder.CreateTable(
                "Gallery",
                table => new
                {
                    GalleryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    Photographer = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Gallery", x => x.GalleryId); });

            migrationBuilder.CreateTable(
                "ImportantLinks",
                table => new
                {
                    ImportantLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_ImportantLinks", x => x.ImportantLinkId); });

            migrationBuilder.CreateTable(
                "Page",
                table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(nullable: true),
                    BackgroundImage = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Page", x => x.PageId); });

            migrationBuilder.CreateTable(
                "Popup",
                table => new
                {
                    PopupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Popup", x => x.PopupId); });

            migrationBuilder.CreateTable(
                "Resets",
                table => new
                {
                    ResetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    IssuedDateTime = table.Column<DateTime>(nullable: false),
                    ExpirationDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Resets", x => x.ResetId); });

            migrationBuilder.CreateTable(
                "Roles",
                table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false, defaultValue: 1),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Roles", x => x.RoleId); });

            migrationBuilder.CreateTable(
                "SalientFeatures",
                table => new
                {
                    SalientFeatureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feature = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_SalientFeatures", x => x.SalientFeatureId); });

            migrationBuilder.CreateTable(
                "StudentSay",
                table => new
                {
                    StudentSayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slogan = table.Column<string>(nullable: false),
                    BackgroundImage = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_StudentSay", x => x.StudentSayId); });

            migrationBuilder.CreateTable(
                "StudentsSayStudents",
                table => new
                {
                    StudentSayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(nullable: false),
                    StudentDesignation = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_StudentsSayStudents", x => x.StudentSayId); });

            migrationBuilder.CreateTable(
                "Teachers",
                table => new
                {
                    TeacherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Instagram = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true),
                    GooglePlus = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table => { table.PrimaryKey("PK_Teachers", x => x.TeacherId); });

            migrationBuilder.CreateTable(
                "Images",
                table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryId = table.Column<int>(nullable: false),
                    ImageLink = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        "FK_Images_Gallery_GalleryId",
                        x => x.GalleryId,
                        "Gallery",
                        "GalleryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Attachment",
                table => new
                {
                    AttachmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                    table.ForeignKey(
                        "FK_Attachment_Page_PageId",
                        x => x.PageId,
                        "Page",
                        "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Authentications",
                table => new
                {
                    AuthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsEmailVerified = table.Column<bool>(nullable: false, defaultValue: false),
                    DateEmailVerified = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    Allowed = table.Column<int>(nullable: false, defaultValue: 1),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentications", x => x.AuthId);
                    table.ForeignKey(
                        "FK_Authentications_Roles_RoleId",
                        x => x.RoleId,
                        "Roles",
                        "RoleId");
                });

            migrationBuilder.CreateTable(
                "Privileges",
                table => new
                {
                    PrivilegeId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Read = table.Column<bool>(nullable: false),
                    Create = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.PrivilegeId);
                    table.ForeignKey(
                        "FK_Privileges_Roles_PrivilegeId",
                        x => x.PrivilegeId,
                        "Roles",
                        "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "AcademicItems",
                new[] {"ItemId", "CreatedDate", "Description", "Image", "Link", "Title"},
                new object[,]
                {
                    {
                        1, new DateTime(2020, 9, 15, 17, 28, 38, 472, DateTimeKind.Local).AddTicks(746),
                        "&lt;p&gt;&lt;span style=&quot;color: #343a40; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;Forests are the most significant part of natural resources. Specially, the forests are our greatest property as the Nepali saying goes, &quot;Hariyo ban, Nepal ko dhan&quot;.&lt;/span&gt;&lt;/p&gt;",
                        "\\User_Information\\Pages\\Academic Item\\Forestry\\course_img2.jpg", "/Home/Forestry",
                        "Forestry"
                    },
                    {
                        2, new DateTime(2020, 9, 15, 17, 28, 38, 472, DateTimeKind.Local).AddTicks(3914),
                        "&lt;p&gt;&lt;span style=&quot;color: #343a40; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;A degree in agriculture gives the students the knowledge and skills needed to manage agricultural and farm businesses, or to work in areas such as agricultural sales, food production and farming.&lt;/span&gt;&lt;/p&gt;",
                        "\\User_Information\\Pages\\Academic Item\\Agriculture\\course_img.jpg", "/Home/Agriculture",
                        "Agriculture"
                    }
                });

            migrationBuilder.InsertData(
                "Academics",
                new[] {"AcademicId", "CreatedDate", "Description"},
                new object[]
                {
                    1, new DateTime(2020, 9, 15, 17, 28, 38, 476, DateTimeKind.Local).AddTicks(2471),
                    "<p><span style=\"color: #8d9297; font-family: Roboto, sans-serif; font-size: 16px; text-align: center; background-color: #ffffff;\">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore.</span></p>"
                });

            migrationBuilder.InsertData(
                "Email",
                new[] {"EmailId", "Email", "Password"},
                new object[]
                {
                    1, "bishnudhain2020@gmail.com", "4k+S8mpm/2I+CwmEdUp+elHZK3wHudBBfiwPfu3gGPkK4v0FUbAUa8vQ+HLvkWx3"
                });

            migrationBuilder.InsertData(
                "Footer",
                new[]
                {
                    "FooterHeaderId", "ContactAddress", "ContactEmail", "ContactNumber", "FacebookLink",
                    "GooglePlusLink", "InstaGramLink", "Slogan", "TweeterLink", "YoutubeLink"
                },
                new object[]
                {
                    1, "Barbardiya Municipality ward no 6, Jaynagar", "bishnudhani@gmail.com", "084-404100", null, null,
                    null,
                    "If you are going to use a passage of Lorem Ipsum you need to be sure there isn't anything embarrassing hidden in the middle of text",
                    null, null
                });

            migrationBuilder.InsertData(
                "ImportantLinks",
                new[] {"ImportantLinkId", "Link", "Title"},
                new object[,]
                {
                    {1, "http://ctevt.org.np/", "Ctevt"},
                    {2, "http://barbardiyamun.gov.np/", "Barbardiyamun"},
                    {3, "https://daobardiya.moha.gov.np/", "Daobardiya"}
                });

            migrationBuilder.InsertData(
                "Page",
                new[] {"PageId", "BackgroundImage", "CreatedDate", "Description", "PageName"},
                new object[,]
                {
                    {
                        12, null, new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8474), null,
                        "Notice"
                    },
                    {
                        11, null, new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8467), null,
                        "Prospectus"
                    },
                    {
                        9, null, new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8445), null,
                        "Downloads"
                    },
                    {
                        8, "\\User_Information\\Pages\\Mayor\\about_bg.jpg",
                        new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8436),
                        "&lt;h1 class=&quot;MsoListParagraphCxSpFirst&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 0.0001pt; font-weight: 500; line-height: normal; font-size: 2.5rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; vertical-align: baseline; text-align: center;&quot; align=&quot;center&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-size: 18pt;&quot;&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-weight: bolder;&quot;&gt;नगर प्रमुख&lt;/span&gt;&lt;/span&gt;&lt;span style=&quot;box-sizing: border-box; font-weight: bolder;&quot;&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;को कलमबाट&lt;/span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/h1&gt;  &lt;h3 class=&quot;MsoListParagraph&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 6pt; font-weight: 500; line-height: normal; font-size: 1.75rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; vertical-align: baseline;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-size: 12pt;&quot;&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&lt;a style=&quot;box-sizing: border-box; color: #292b2c; text-decoration-line: none; background-color: transparent; transition: all 0.5s ease 0s;&quot; title=&quot;Mayor&quot; href=&quot;https://bishnudhani.edu.np/wwwroot/images/IMG_0174.JPG&quot; target=&quot;_blank&quot; rel=&quot;noopener&quot;&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-size: 10pt; font-family: Kalimati; color: black;&quot;&gt;&lt;img style=&quot;box-sizing: border-box; vertical-align: middle; border-style: none; max-width: 100%; display: block; margin-left: auto; margin-right: auto;&quot; src=&quot;https://bishnudhani.edu.np/wwwroot/images/IMG_0174.JPG&quot; alt=&quot;&quot; width=&quot;262&quot; height=&quot;244&quot; /&gt;&lt;/span&gt;&lt;/a&gt;&lt;/span&gt;&lt;/span&gt;&lt;/h3&gt;  &lt;h3 class=&quot;MsoListParagraph&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 6pt; font-weight: 500; line-height: normal; font-size: 1.75rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; text-align: justify; vertical-align: baseline;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-size: 12pt;&quot;&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;सहिद विष्णु&amp;ndash;धनि स्मृति बहुप्राविधिक शिक्षालय संचालनको यो बिन्दुसम्म आइपुग्दा महान जनयुद्धमा शहादत&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;प्राप्त&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;गर्ने अमर सहिदहरुको सपना साकार हुदै गरेको परिदृश्य देखिदै छ ।&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;यस शिक्षालयले&lt;/span&gt;&amp;nbsp;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;गाउँका गरिब किसानहरुका&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;सन्तितह&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;रुलाई सर्वसुलभ तरिकाले ब्य&lt;/span&gt;&lt;span style=&quot;box-sizing: border-box; font-weight: bolder;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-family: Preeti; color: black;&quot;&gt;f&lt;/span&gt;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;वहारिक&lt;/span&gt;&lt;span style=&quot;box-sizing: border-box; font-weight: bolder;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-family: Preeti; color: black;&quot;&gt;&amp;divide;&lt;/span&gt;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;प्राविधिक शिक्षा&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;प्रदान गरी उनीहरुलाई&lt;/span&gt;&amp;nbsp;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;योग्य नागरिक बन्ने सु&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;-&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;अवसर प्रदान गर्नेछ ।&lt;/span&gt;&amp;nbsp;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;यस शिक्षालयमा अध्ययन गर्ने हरेक&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;विद्यार्थीह&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;रुले राष्ट्र निर्माणमा योगदान गर्नेछन&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;्&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&amp;nbsp;। देशको समृद्धिको यात्रालाई गतिवान बनाउन&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;े&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&amp;nbsp;जनवादी शिक्षाको आत्मा नै आम शिक्षा हो&amp;nbsp;&lt;span style=&quot;box-sizing: border-box; letter-spacing: -0.3pt;&quot;&gt;। शिक्षामा आम जनताको न्यायसंगत पहुँच स्थापित गर्न&lt;/span&gt;&lt;/span&gt;&amp;nbsp;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black; letter-spacing: -0.3pt;&quot;&gt;हामी&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black; letter-spacing: -0.3pt;&quot;&gt;सत्&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black; letter-spacing: -0.3pt;&quot;&gt;प्रयास गर्नेछौ&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black; letter-spacing: -0.3pt;&quot;&gt;ँ&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black; letter-spacing: -0.3pt;&quot;&gt;&amp;nbsp;।&lt;/span&gt;&lt;/span&gt;&lt;/h3&gt;  &lt;h3 class=&quot;MsoListParagraph&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 6pt; font-weight: 500; line-height: normal; font-size: 1.75rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; text-align: justify; vertical-align: baseline;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-size: 12pt;&quot;&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;राजनीतिक व्यवस्थाका हिसाबले जनवादी र समाजवादी व्यवस्थामा भिन्नता छ तर शैक्षिक सार भने निकट छ । साथै नेपाली समाजको चरित्रलाई विश्लेषण गर्दा आजको शिक्षाको दिशा समाजवादी हुनुपर्दछ तर शिक्षाको जनवादीकरणको विकास पनि भइ नसकेको अवस्था रहेकोले यहाँ समाजवादको दिशा आवश्यकतामा जोड दिदै हामीले नेपालमा&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;आधारभूत तहसम्म&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;को शिक्षालाई&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;&amp;nbsp;नि:शुल्क र अनिवार्य एवम् माध्यामिक तहसम्म&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;को शिक्षा&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;&amp;nbsp;निशुल्क घोषण&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;ा&lt;/span&gt;&amp;nbsp;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;गरी&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;कार्यन्वयन&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;&amp;nbsp;गरिरहेका छौं ।&lt;/span&gt;&lt;/span&gt;&lt;/h3&gt;  &lt;h3 class=&quot;MsoListParagraphCxSpMiddle&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 0.0001pt; font-weight: 500; line-height: normal; font-size: 1.75rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; text-align: justify; vertical-align: baseline;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-size: 12pt;&quot;&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;अहिले&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;हाम्रो&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&amp;nbsp;नगरपालिका भित्र वन विज्ञान&lt;/span&gt;&lt;span style=&quot;box-sizing: border-box; font-weight: bolder;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-family: Preeti; color: black;&quot;&gt;,&amp;nbsp;&lt;/span&gt;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;कृषि र सिभिल इन्जिनियरिङ्ग विषयको अध्यापन भइरहेको छ भने निकट भविष्यमै अन्य विषय समेत यस शिक्षालयमा समावेश गरिनेछ । विद्यार्थीरुले आफूले चाहेको विषय रोजेर पढन&lt;/span&gt;&amp;nbsp;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;पाउने&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;गरी&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&amp;nbsp;बहुप्र&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;ा&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;विधिक शिक्षालय स्थापना भएको छ ।&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;श&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;हरको महंगीले प्राविधिक शिक्षा हासिल गर्नबाट बन्चित विद्यार्थीहरु यस बहुप्राविधिक शिक्षालयले दिने अवसरबाट&amp;nbsp;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: red;&quot;&gt;लाभान्वित&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&amp;nbsp;हुनेछन&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;्&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&amp;nbsp;र लगनशील भएर अध्ययन गर्ने छन&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;्&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt; भन्ने पनि विश्वास लिएको छु ।&lt;/span&gt;&lt;/span&gt;&lt;/h3&gt;  &lt;h3 class=&quot;MsoListParagraphCxSpMiddle&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 0.0001pt; font-weight: 500; line-height: normal; font-size: 1.75rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; text-align: justify; vertical-align: baseline;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-size: 12pt;&quot;&gt;&lt;strong&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;मिति : श्रावण&lt;/span&gt;&lt;span style=&quot;box-sizing: border-box;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-family: Preeti; color: black;&quot;&gt;,&lt;/span&gt;&lt;/span&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-family: Kalimati; color: black;&quot;&gt;&amp;nbsp;२०७७&amp;nbsp;&lt;/span&gt;&amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp;&lt;/strong&gt; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &lt;strong&gt;&amp;nbsp; &amp;nbsp; धन्यवाद ।&lt;/strong&gt;&lt;/span&gt;&lt;/h3&gt;  &lt;h3 class=&quot;MsoListParagraphCxSpMiddle&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 0.0001pt; font-weight: 500; line-height: normal; font-size: 1.75rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; vertical-align: baseline; text-align: right;&quot;&gt;&amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &amp;nbsp; &lt;strong&gt;&lt;span style=&quot;box-sizing: border-box; font-size: 12pt;&quot;&gt;&amp;nbsp; &amp;nbsp;दुर्गाबहादुर थारु&amp;nbsp;&lt;span style=&quot;box-sizing: border-box;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-family: &#39;Times New Roman&#39;, serif;&quot;&gt;&#39;&lt;/span&gt;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati;&quot;&gt;कविर&lt;/span&gt;&lt;span style=&quot;box-sizing: border-box;&quot;&gt;&lt;span style=&quot;box-sizing: border-box; font-family: Preeti;&quot;&gt;&amp;rsquo;&lt;/span&gt;&lt;/span&gt;&lt;span lang=&quot;HI&quot; style=&quot;box-sizing: border-box; font-family: Kalimati;&quot;&gt;&amp;nbsp;&lt;/span&gt;&lt;/span&gt;&lt;/strong&gt;&lt;/h3&gt;  &lt;h3 class=&quot;MsoListParagraphCxSpMiddle&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 0.0001pt; font-weight: 500; line-height: normal; font-size: 1.75rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; vertical-align: baseline; padding-left: 160px; text-align: right;&quot;&gt;&lt;strong&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-size: 12pt; font-family: Kalimati;&quot;&gt;नगर प्रमुख&lt;/span&gt;&lt;/strong&gt;&lt;/h3&gt;  &lt;h3 class=&quot;MsoListParagraphCxSpLast&quot; style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 0.0001pt; font-weight: 500; line-height: normal; font-size: 1.75rem; color: #222222; font-family: Poppins, sans-serif; background-color: #ffffff; vertical-align: baseline;&quot; align=&quot;right&quot;&gt;&lt;strong&gt;&lt;span lang=&quot;NE&quot; style=&quot;box-sizing: border-box; font-size: 12pt; font-family: Kalimati;&quot;&gt;बारबर्दिया नगरपालिका&lt;/span&gt;&lt;/strong&gt;&lt;/h3&gt;",
                        "Mayor's Saying"
                    },
                    {
                        7, "\\User_Information\\Pages\\Staff and Faculties\\course_img2.jpg",
                        new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8428),
                        "&lt;p&gt;Staff and Faculties&lt;/p&gt;", "Staff and Faculties"
                    },
                    {
                        10, null, new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8457), null,
                        "Gallery"
                    },
                    {
                        5, "\\User_Information\\Pages\\Agriculture\\cta_bg.jpg",
                        new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8405),
                        "&lt;div class=&quot;course_desc&quot; style=&quot;box-sizing: border-box; color: #8d9297; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;  &lt;div class=&quot;heading_s1&quot; style=&quot;box-sizing: border-box; margin-bottom: 20px;&quot;&gt;  &lt;h4 style=&quot;box-sizing: border-box; margin: 0px; line-height: 1.2; font-size: 1.5rem; color: #222222; font-family: Poppins, sans-serif; text-transform: capitalize;&quot;&gt;Description:&lt;/h4&gt;  &lt;/div&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;Agriculture is the backbone of our country&#39;s economy. A large number of population of Nepal depends on agriculture for their survival. Agriculture has been at the center of human civilization since civilization began, and it remains at the heart of many of the most pressing issues for modern societies. In the context of Nepal, although most of the people are dependent on agriculture, it lacks educated and qualified people working in this field. And this is one of the main causes of poor agricultural results in our country.&lt;/p&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;There is need of skilled manpower with technical knowledge and higher degrees in the agriculture sector. Agriculture intersects with several fields like poverty, famine, development economics, genetic modification, environmental sustainability, disease epidemics, and agricultural scholars are involved in research and development work in all these fields.&lt;/p&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;Careers in agriculture can make up one of the largest industries and sources of long-term employment in Nepal. Scholars perusing agriculture have ample job opportunities in the government as well as private sector. The degree Diploma in Agriculture equips students with the knowledge and skills that will help them to conduct simple research works in agricultural field, conduct field surveys and manage different areas of farming practices.&lt;/p&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;A degree in agriculture gives the students the knowledge and skills needed to manage agricultural and farm businesses, or to work in areas such as agricultural sales, food production and farming. Moreover, an individual with higher degree in this field can work as an agricultural consultant, estates manager, farm manager, plant breeder/geneticist, soil scientist, rural practice surveyor and so on.&lt;/p&gt;  &lt;/div&gt;  &lt;div class=&quot;course_curriculum&quot; style=&quot;box-sizing: border-box; padding-bottom: 10px; color: #8d9297; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;  &lt;div class=&quot;heading_s1&quot; style=&quot;box-sizing: border-box; margin-bottom: 20px;&quot;&gt;  &lt;h4 style=&quot;box-sizing: border-box; margin: 0px; line-height: 1.2; font-size: 1.5rem; color: #222222; font-family: Poppins, sans-serif; text-transform: capitalize;&quot;&gt;Course Curriculum For Agriculture:&lt;/h4&gt;  &lt;/div&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;The course will make students understand the various aspects of agriculture science and enrich skills in using modern equipment, tools and techniques associated with agriculture. Moreover, it will motivate students for involving them in lifelong agricultural practices and further study and research in this field, and linking agriculture with economic development of the country.&lt;/p&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;The institute offers the following courses under Diploma in Agriculture:&lt;/p&gt;  &lt;/div&gt;",
                        "Agriculture"
                    },
                    {
                        4, "\\User_Information\\Pages\\Forestry\\banner9.jpg",
                        new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8396),
                        "&lt;div class=&quot;course_desc&quot; style=&quot;box-sizing: border-box; color: #8d9297; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;  &lt;div class=&quot;heading_s1&quot; style=&quot;box-sizing: border-box; margin-bottom: 20px;&quot;&gt;  &lt;h4 style=&quot;box-sizing: border-box; margin: 0px; line-height: 1.2; font-size: 1.5rem; color: #222222; font-family: Poppins, sans-serif; text-transform: capitalize;&quot;&gt;Description:&lt;/h4&gt;  &lt;/div&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;&lt;span style=&quot;color: #000000;&quot;&gt;Forests are the most significant part of natural resources. Specially, the forests are our greatest property as the Nepali saying goes, &quot;Hariyo ban, Nepal ko dhan&quot;. Forestry is the science of planting, managing, maintaining as well as conversing forests and its related resources. Due to the careless cut down of trees, gradual disappearance of forests and competing demands on the resources, the field of forestry has a good potential for employment in the coming years. They can find jobs at places such as educational institutions, forest service companies, food companies, biotechnology firms, plant health inspection service companies, biological supply houses and so on.&lt;/span&gt;&lt;/p&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;&lt;span style=&quot;color: #000000;&quot;&gt;A scholar specialized in forestry is primarily concerned with the protection of forests which contributes to the environmental conservation too. He/she is involved in many activities such as protection of natural resources, ecological restoration, timber harvesting and so on. The course of forestry enables the individual to develop forest management plans, select sites for growing new trees and plants and supervise forestry projects.&lt;/span&gt;&lt;/p&gt;  &lt;/div&gt;  &lt;div class=&quot;course_curriculum&quot; style=&quot;box-sizing: border-box; padding-bottom: 10px; color: #8d9297; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;  &lt;div class=&quot;heading_s1&quot; style=&quot;box-sizing: border-box; margin-bottom: 20px;&quot;&gt;  &lt;h4 style=&quot;box-sizing: border-box; margin: 0px; line-height: 1.2; font-size: 1.5rem; color: #222222; font-family: Poppins, sans-serif; text-transform: capitalize;&quot;&gt;Course Curriculum For Forestry:&lt;/h4&gt;  &lt;/div&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;&lt;span style=&quot;color: #000000;&quot;&gt;Global warming is the biggest environmental challenge in today&#39;s world and forestry education is one of the best tools to fight this challenge. Forestry course will help the students to understand forest ecology, concepts, and factors affecting them. Moreover, it will enable to be familiar with and identify common and invasive tree pests and diseases and also the associated control methods. They will also understand how forest health and management affect biodiversity, global warming, and forest fragmentation. The course will create awareness among the students about the importance and value of trees in urban and community settings, and why trees and forests are important to human health, recreation, wildlife, and watershed quality.&lt;/span&gt;&lt;/p&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;&lt;span style=&quot;color: #000000;&quot;&gt;The institute offers the following courses under Diploma in Forestry:&lt;/span&gt;&lt;/p&gt;  &lt;/div&gt;",
                        "Forestry"
                    },
                    {
                        3, "\\User_Information\\Pages\\Background\\course_bg.jpg",
                        new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8385),
                        "&lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; color: #8d9297; line-height: 28px; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;The polytechnic institute has been named after the two martyrs of Nepali political movement Bishnu Kumar Pokharel and Dhani Ram Tharu, both of whom belonged to the present Barbardiya Municipality of Bardiya. The establishment of the institute is also a tribute paid to those great personalities who devoted their entire life for bringing long-term political changes in the country through the development of political awareness among people.&lt;/p&gt;  &lt;div class=&quot;row&quot; style=&quot;box-sizing: border-box; display: flex; flex-wrap: wrap; margin-right: -15px; margin-left: -15px; color: #8d9297; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;  &lt;div class=&quot;col-md-6&quot; style=&quot;box-sizing: border-box; position: relative; width: 570px; padding-right: 15px; padding-left: 15px; flex: 0 0 50%; max-width: 50%;&quot;&gt;  &lt;div class=&quot;single_img&quot; style=&quot;box-sizing: border-box; padding-left: 80px;&quot;&gt;&lt;img class=&quot;w-50 mb-3 mb-md-4 5&quot; style=&quot;box-sizing: border-box; vertical-align: middle; border-style: none; max-width: 100%; width: 270px; margin-bottom: 1.5rem !important;&quot; src=&quot;../images/BishnuPokherel.jpg&quot; alt=&quot;Bishnu Pokherel&quot; /&gt;&lt;/div&gt;  &lt;/div&gt;  &lt;div class=&quot;col-md-6&quot; style=&quot;box-sizing: border-box; position: relative; width: 570px; padding-right: 15px; padding-left: 15px; flex: 0 0 50%; max-width: 50%;&quot;&gt;  &lt;div class=&quot;mb-3 mb-md-4&quot; style=&quot;box-sizing: border-box; margin-bottom: 1.5rem !important;&quot;&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;Bishnu Kumar Pokharel, born on 2nd Chaitra, 2007 BS, was interested in literary writing and philosophy from his childhood. He was always ready to help the needy people. Bishnu Kumar Pokharel, who not only started taking part in various progressive political activities but also started providing leadership to several political movements from very early stage of his student life. He also made a significant contribution to the field of education in the local level. Bishnu Kumar Pokharel is also found to have fought against the oppressions made by the landlords to the Kamaiya (bonded labourers). As an active leader of the Communist Party of Nepal (Maoist), he was shot dead by the then government on 28 Jestha, 2055 BS at the place called Ranijaruwa in the local jungle.&lt;/p&gt;  &lt;/div&gt;  &lt;/div&gt;  &lt;/div&gt;  &lt;div class=&quot;row&quot; style=&quot;box-sizing: border-box; display: flex; flex-wrap: wrap; margin-right: -15px; margin-left: -15px; color: #8d9297; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;  &lt;div class=&quot;col-md-6&quot; style=&quot;box-sizing: border-box; position: relative; width: 570px; padding-right: 15px; padding-left: 15px; flex: 0 0 50%; max-width: 50%;&quot;&gt;  &lt;div class=&quot;mb-3 mb-md-4&quot; style=&quot;box-sizing: border-box; margin-bottom: 1.5rem !important;&quot;&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; line-height: 28px;&quot;&gt;Dhani Ram Tharu, born on 8 Shrawan, 2020 BS, in the village Banghushri of present Barbardiya Municipality, Ward No. 5, Bardiya District, was a good student and had a keen interest in sports, social works and politics from his childhood. He is found to have helped the people of his locality to solve and settle several social and economic disputes. Dhani Ram Tharu entered into active politics when he was in class seven by working against the exploitation and oppression made by the local landlords to the poor people and the Kamaiya. He became a political activist when he took part in the popular &#39;Thumni Revolution&#39; held in 2036 BS. Later, Dhani Ram joined the Communist Party of Nepal (Maoist) which had started an underground armed revolution, and was shot dead by the then government on 28 Jestha, 2055 BS at the place called Ranijaruwa in the local jungle.&lt;/p&gt;  &lt;/div&gt;  &lt;/div&gt;  &lt;div class=&quot;col-md-6&quot; style=&quot;box-sizing: border-box; position: relative; width: 570px; padding-right: 15px; padding-left: 15px; flex: 0 0 50%; max-width: 50%;&quot;&gt;  &lt;div class=&quot;single_img&quot; style=&quot;box-sizing: border-box; padding-left: 80px;&quot;&gt;&lt;img class=&quot;w-50 mb-3 mb-md-4&quot; style=&quot;box-sizing: border-box; vertical-align: middle; border-style: none; max-width: 100%; width: 270px; margin-bottom: 1.5rem !important;&quot; src=&quot;../images/DhaniRamTharu.jpg&quot; alt=&quot;Dhani Ram Tharu&quot; /&gt;&lt;/div&gt;  &lt;/div&gt;  &lt;/div&gt;",
                        "Background"
                    },
                    {
                        2, "\\User_Information\\Pages\\About Us\\about_bg.jpg",
                        new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8308),
                        "&lt;h2 style=&quot;box-sizing: border-box; margin: 0px; line-height: 1.2; font-size: 2rem; color: #222222; font-family: Poppins, sans-serif; text-transform: capitalize; background-color: #ffffff; text-align: justify;&quot;&gt;&lt;strong&gt;We&#39;re One Of The Best Places To &lt;/strong&gt;&lt;strong style=&quot;font-size: 2rem;&quot;&gt;Explore And Learn With Fun&lt;/strong&gt;&lt;/h2&gt;  &lt;h2 style=&quot;box-sizing: border-box; margin: 0px; line-height: 1.2; font-size: 2rem; color: #222222; font-family: Poppins, sans-serif; text-transform: capitalize; background-color: #ffffff;&quot;&gt;&amp;nbsp;&lt;/h2&gt;  &lt;h2 style=&quot;box-sizing: border-box; margin: 0px; line-height: 1.2; font-size: 2rem; color: #222222; font-family: Poppins, sans-serif; text-transform: capitalize; background-color: #ffffff;&quot;&gt;&lt;img style=&quot;float: left;&quot; src=&quot;https://bishnudhani.edu.np/wwwroot/images/about_img2.jpg&quot; alt=&quot;&quot; width=&quot;568&quot; height=&quot;473&quot; /&gt;&lt;/h2&gt;  &lt;p style=&quot;padding-left: 600px; text-align: justify;&quot;&gt;&lt;span style=&quot;color: #000000;&quot;&gt;Shahid Bishnu Dhani Memorial Polytechnic Institute is a newly established educational institution that primarily attempts to contribute to the development of the country through the production of skilled and semi-skilled human resources. The maximum and appropriate use of natural resources depends on the availability of human resources. Although with abundant natural means and resources, Nepal is still an under-developed country owing to the paucity of human resources.&lt;/span&gt;&lt;/p&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; color: #8d9297; line-height: 28px; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff; padding-left: 600px; text-align: justify;&quot;&gt;&lt;span style=&quot;color: #000000;&quot;&gt;The main aim of Shahid Bishnu Dhani Memorial Polytechnic Institute is to produce skill human resources that will help develop the country by strengthening the economy of the country through the appropriate use of available means and resources. At the local level, it will also ease the access of the students coming from economically weak backgrounds to quality technical education. At the national level, it will contribute to the overall development of the country by reducing the cost of hiring manpower from abroad. It is also believed to create job opportunities by mobilizing the stagnated idle semi-skilled and unskilled human resources.&lt;/span&gt;&lt;/p&gt;  &lt;p style=&quot;box-sizing: border-box; margin-top: 0px; margin-bottom: 20px; color: #8d9297; line-height: 28px; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff; padding-left: 600px; text-align: justify;&quot;&gt;&lt;span style=&quot;color: #000000;&quot;&gt;Shahid Bishnu Dhani Memorial Polytechnic Institute ensures a disciplined academic environment for all students. The institute is located in the head-quarter of Barbardiya Municipality, Ward no. 6, Bardiya, Province no.5, Nepal. The well-furnished academic building and the administrative building of the institute are under construction.&lt;/span&gt;&lt;/p&gt;",
                        "About Us"
                    },
                    {
                        1, "\\User_Information\\Pages\\About our institute\\about_img.jpg",
                        new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(5135),
                        "&lt;p&gt;&lt;span style=&quot;color: #ecf0f1;&quot;&gt;Shahid Bishnu Dhani Memorial Polytechnic Institute is a newly established educational institution which primarily attempts to contribute to the development of the country through the production of skilled and semi-skilled human resources. The maximum and appropriate use of natural resources depends on the availability of human resources. Although with abundant natural means and resources, Nepal is still an under-developed country owing to the paucity of human resources.&lt;/span&gt;&lt;/p&gt;",
                        "About our institute"
                    },
                    {
                        6, "\\User_Information\\Pages\\Privacy Policy\\blog_bg.jpg",
                        new DateTime(2020, 9, 15, 17, 28, 38, 457, DateTimeKind.Local).AddTicks(8420),
                        "&lt;p&gt;Privacy Policy&lt;/p&gt;", "Privacy Policy"
                    }
                });

            migrationBuilder.InsertData(
                "Popup",
                new[] {"PopupId", "CreatedDate", "Link", "Name"},
                new object[]
                {
                    1, new DateTime(2020, 9, 15, 17, 28, 38, 483, DateTimeKind.Local).AddTicks(6510),
                    "\\User_Information\\Popup\\Popup\\PopUp.jpg", "Popup"
                });

            migrationBuilder.InsertData(
                "Roles",
                new[] {"RoleId", "Description", "RoleName", "State"},
                new object[,]
                {
                    {1, "Role for Administrator", "Administrator", 1},
                    {2, "Role for Developer", "Developer", 1},
                    {3, "Role for Manager", "Manager", 1}
                });

            migrationBuilder.InsertData(
                "SalientFeatures",
                new[] {"SalientFeatureId", "Feature"},
                new object[,]
                {
                    {9, "Publication of periodic general and research journals."},
                    {
                        8,
                        "Inter-college student exchange programme with different forestry and agricultural colleges of Nepal."
                    },
                    {
                        7,
                        "Good coordination with different governmental and non-governmental organizations and agencies."
                    },
                    {6, "Availability of enough land for practical activities."},
                    {1, "Situated in the peaceful area away from the noise of the city."},
                    {4, "Highly motivated teachers and professional faculties."},
                    {3, "E-library and 24/7 internet facilities."},
                    {2, "Well ventilated and echo free classrooms with comfortable seating arrangement."},
                    {
                        10,
                        "Provision of project works, work experience programmes, etc. for integrating the theoretical course with real life experience."
                    },
                    {5, "Well-equipped multidisciplinary laboratory."}
                });

            migrationBuilder.InsertData(
                "StudentSay",
                new[] {"StudentSayId", "BackgroundImage", "CreatedDate", "Slogan"},
                new object[]
                {
                    1, "\\User_Information\\Pages\\Students Say\\Images\\teacher_bg.jpg",
                    new DateTime(2020, 9, 15, 17, 28, 38, 445, DateTimeKind.Local).AddTicks(8507),
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore."
                });

            migrationBuilder.InsertData(
                "Attachment",
                new[] {"AttachmentId", "FileName", "Link", "PageId"},
                new object[,]
                {
                    {3, "Brochure", "\\User_Information\\Pages\\Brochure\\5_6188035137427472707 (1).pdf", 1},
                    {
                        1, "Forestry Curriculum",
                        "\\User_Information\\Pages\\Forestry\\2019-09-29_Diploma-in-Forestry-Revised-2019.pdf", 4
                    },
                    {2, "Agriculture Curriculum", null, 5},
                    {4, "Prospectus", null, 11},
                    {5, "Notice", null, 12}
                });

            migrationBuilder.InsertData(
                "Authentications",
                new[]
                {
                    "AuthId", "Address", "DateEmailVerified", "DateOfBirth", "Email", "FullName", "Gender", "Image",
                    "IsEmailVerified", "Password", "PhoneNumber", "RoleId", "UserName"
                },
                new object[,]
                {
                    {
                        2, "Baneshowr, Kathmendu",
                        new DateTime(2020, 9, 15, 17, 28, 38, 363, DateTimeKind.Local).AddTicks(4786),
                        new DateTime(1994, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "leo.shivapoudel@gmail.com",
                        "Shiva Poudel", 1, null, true,
                        "f804fb935ebf257c3c4c94591aef12692e6413e687e3451e00be2132914e2687", "9801061035", 1, "Shiva"
                    },
                    {
                        1, "Gaushala, Kathmendu",
                        new DateTime(2020, 9, 15, 17, 28, 38, 358, DateTimeKind.Local).AddTicks(1080),
                        new DateTime(1999, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "kingraj530@gmail.com",
                        "Dibesh Raj Subedi", 1, null, true,
                        "37c217bb02332590ff91d81a5bea827ed8f5c5db75491a786f453acb6b970ec8", "9861315234", 2,
                        "Developer Dibesh"
                    }
                });

            migrationBuilder.InsertData(
                "Privileges",
                new[] {"PrivilegeId", "Create", "Delete", "Read", "RoleId", "Update"},
                new object[,]
                {
                    {1, true, true, true, 1, true},
                    {2, true, true, true, 2, true},
                    {3, false, false, true, 3, true}
                });

            migrationBuilder.CreateIndex(
                "IX_AcademicItems_ItemId",
                "AcademicItems",
                "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Academics_AcademicId",
                "Academics",
                "AcademicId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_AppSettings_SettingsId",
                "AppSettings",
                "SettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Attachment_AttachmentId",
                "Attachment",
                "AttachmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Attachment_PageId",
                "Attachment",
                "PageId");

            migrationBuilder.CreateIndex(
                "IX_Authentications_AuthId",
                "Authentications",
                "AuthId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Authentications_Email",
                "Authentications",
                "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Authentications_PhoneNumber",
                "Authentications",
                "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Authentications_RoleId",
                "Authentications",
                "RoleId");

            migrationBuilder.CreateIndex(
                "IX_Authentications_UserName",
                "Authentications",
                "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Carousel_CarouselId",
                "Carousel",
                "CarouselId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Downloads_FileId",
                "Downloads",
                "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Email_EmailId",
                "Email",
                "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Footer_FooterHeaderId",
                "Footer",
                "FooterHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Gallery_GalleryId",
                "Gallery",
                "GalleryId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Images_GalleryId",
                "Images",
                "GalleryId");

            migrationBuilder.CreateIndex(
                "IX_Images_ImageId",
                "Images",
                "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_ImportantLinks_ImportantLinkId",
                "ImportantLinks",
                "ImportantLinkId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Page_PageId",
                "Page",
                "PageId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Popup_PopupId",
                "Popup",
                "PopupId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Privileges_PrivilegeId",
                "Privileges",
                "PrivilegeId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Resets_ResetId",
                "Resets",
                "ResetId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Roles_RoleId",
                "Roles",
                "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Roles_RoleName",
                "Roles",
                "RoleName",
                unique: true,
                filter: "[RoleName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_SalientFeatures_SalientFeatureId",
                "SalientFeatures",
                "SalientFeatureId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_StudentSay_StudentSayId",
                "StudentSay",
                "StudentSayId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_StudentsSayStudents_StudentSayId",
                "StudentsSayStudents",
                "StudentSayId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Teachers_TeacherId",
                "Teachers",
                "TeacherId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AcademicItems");

            migrationBuilder.DropTable(
                "Academics");

            migrationBuilder.DropTable(
                "AppSettings");

            migrationBuilder.DropTable(
                "Attachment");

            migrationBuilder.DropTable(
                "Authentications");

            migrationBuilder.DropTable(
                "Carousel");

            migrationBuilder.DropTable(
                "Downloads");

            migrationBuilder.DropTable(
                "Email");

            migrationBuilder.DropTable(
                "Footer");

            migrationBuilder.DropTable(
                "Images");

            migrationBuilder.DropTable(
                "ImportantLinks");

            migrationBuilder.DropTable(
                "Popup");

            migrationBuilder.DropTable(
                "Privileges");

            migrationBuilder.DropTable(
                "Resets");

            migrationBuilder.DropTable(
                "SalientFeatures");

            migrationBuilder.DropTable(
                "StudentSay");

            migrationBuilder.DropTable(
                "StudentsSayStudents");

            migrationBuilder.DropTable(
                "Teachers");

            migrationBuilder.DropTable(
                "Page");

            migrationBuilder.DropTable(
                "Gallery");

            migrationBuilder.DropTable(
                "Roles");
        }
    }
}