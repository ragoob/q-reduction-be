using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Acl");

            migrationBuilder.EnsureSchema(
                name: "Settings");

            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LabelTextAr = table.Column<string>(nullable: true),
                    LabelValueAr = table.Column<string>(nullable: true),
                    LabelValueEn = table.Column<string>(nullable: true),
                    LabelTextEn = table.Column<string>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                schema: "Acl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsTerms",
                schema: "Acl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Acl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(maxLength: 50, nullable: false),
                    ReadOnly = table.Column<bool>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<int>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                schema: "Acl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 225, nullable: true),
                    NameEn = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemGrids",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGrids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: true),
                    QrCode = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    BranchAddress = table.Column<string>(maxLength: 500, nullable: true),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branchs_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: true),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(maxLength: 50, nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionTextAr = table.Column<string>(maxLength: 250, nullable: false),
                    QuestionTextEn = table.Column<string>(maxLength: 250, nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 505, nullable: true),
                    NoteEN = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PagePermissions",
                schema: "Acl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PermissionTermId = table.Column<int>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    ACLName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagePermissions_Pages_PageId",
                        column: x => x.PageId,
                        principalSchema: "Acl",
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagePermissions_PermissionsTerms_PermissionTermId",
                        column: x => x.PermissionTermId,
                        principalSchema: "Acl",
                        principalTable: "PermissionsTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemGridColumns",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    VisibilityDefault = table.Column<bool>(nullable: false),
                    SystemGridId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGridColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemGridColumns_SystemGrids_SystemGridId",
                        column: x => x.SystemGridId,
                        principalSchema: "Settings",
                        principalTable: "SystemGrids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Acl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserGuid = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 225, nullable: true),
                    UserName = table.Column<string>(maxLength: 255, nullable: true),
                    LastLoginUtcDate = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    ForgotPasswordCode = table.Column<string>(maxLength: 6, nullable: true),
                    ForgotPasswordExpiration = table.Column<DateTime>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowGuid = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UserTypeId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    VerificationCode = table.Column<string>(maxLength: 4, nullable: true),
                    IsVerified = table.Column<bool>(nullable: false, defaultValue: false),
                    UserDeviceId = table.Column<string>(nullable: true),
                    VerificationCodeExpiration = table.Column<DateTime>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: true),
                    BranchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalSchema: "Acl",
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchServices_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePagePermissions",
                schema: "Acl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PagePermissionId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePagePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePagePermissions_PagePermissions_PagePermissionId",
                        column: x => x.PagePermissionId,
                        principalSchema: "Acl",
                        principalTable: "PagePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePagePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Acl",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGridColumns",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsVisible = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    SystemGridColumnId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGridColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGridColumns_SystemGridColumns_SystemGridColumnId",
                        column: x => x.SystemGridColumnId,
                        principalSchema: "Settings",
                        principalTable: "SystemGridColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelpAndSupport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageTitle = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpAndSupport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpAndSupport_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginProviders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProviderType = table.Column<int>(nullable: false),
                    Providertoken = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginProviders_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    StartAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    EndAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    BranchId = table.Column<int>(maxLength: 50, nullable: false),
                    IsEnded = table.Column<bool>(nullable: false),
                    QRCode = table.Column<string>(nullable: true),
                    UserIdSupport = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdateBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_Users_UserIdSupport",
                        column: x => x.UserIdSupport,
                        principalSchema: "Acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Acl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Acl",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftQueues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserTurn = table.Column<int>(nullable: false),
                    ShiftId = table.Column<int>(nullable: false),
                    IsServiceDone = table.Column<bool>(nullable: false),
                    UserIdMobile = table.Column<int>(nullable: false),
                    UserIdBy = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: false),
                    WindowNumber = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    PushId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftQueues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftQueues_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftQueues_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftQueues_Users_UserIdBy",
                        column: x => x.UserIdBy,
                        principalSchema: "Acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftQueues_Users_UserIdMobile",
                        column: x => x.UserIdMobile,
                        principalSchema: "Acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShiftUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ShiftId = table.Column<int>(nullable: false),
                    WindowNumber = table.Column<string>(maxLength: 50, nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftUsers_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftUsers_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftUsers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Acl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(maxLength: 500, nullable: true),
                    ShiftQueueId = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_ShiftQueues_ShiftQueueId",
                        column: x => x.ShiftQueueId,
                        principalTable: "ShiftQueues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    AnswerValue = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationQuestionAnswer_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationQuestionAnswer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branchs_OrganizationId",
                table: "Branchs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchServices_BranchId",
                table: "BranchServices",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchServices_ServiceId",
                table: "BranchServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestionAnswer_EvaluationId",
                table: "EvaluationQuestionAnswer",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestionAnswer_QuestionId",
                table: "EvaluationQuestionAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ShiftQueueId",
                table: "Evaluations",
                column: "ShiftQueueId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpAndSupport_UserId",
                table: "HelpAndSupport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_OrganizationId",
                table: "Instructions",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginProviders_UserId",
                table: "LoginProviders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_OrganizationId",
                table: "Questions",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrganizationId",
                table: "Services",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftQueues_ServiceId",
                table: "ShiftQueues",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftQueues_ShiftId",
                table: "ShiftQueues",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftQueues_UserIdBy",
                table: "ShiftQueues",
                column: "UserIdBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftQueues_UserIdMobile",
                table: "ShiftQueues",
                column: "UserIdMobile");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_BranchId",
                table: "Shifts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UserIdSupport",
                table: "Shifts",
                column: "UserIdSupport");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftUsers_ServiceId",
                table: "ShiftUsers",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftUsers_ShiftId",
                table: "ShiftUsers",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftUsers_UserId",
                table: "ShiftUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PagePermissions_PageId",
                schema: "Acl",
                table: "PagePermissions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PagePermissions_PermissionTermId",
                schema: "Acl",
                table: "PagePermissions",
                column: "PermissionTermId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePagePermissions_PagePermissionId",
                schema: "Acl",
                table: "RolePagePermissions",
                column: "PagePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePagePermissions_RoleId",
                schema: "Acl",
                table: "RolePagePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Acl",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "Acl",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                schema: "Acl",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "Acl",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                schema: "Acl",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                schema: "Acl",
                table: "Users",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                schema: "Acl",
                table: "Users",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemGridColumns_SystemGridId",
                schema: "Settings",
                table: "SystemGridColumns",
                column: "SystemGridId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGridColumns_SystemGridColumnId",
                schema: "Settings",
                table: "UserGridColumns",
                column: "SystemGridColumnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "BranchServices");

            migrationBuilder.DropTable(
                name: "EvaluationQuestionAnswer");

            migrationBuilder.DropTable(
                name: "HelpAndSupport");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "LoginProviders");

            migrationBuilder.DropTable(
                name: "ShiftUsers");

            migrationBuilder.DropTable(
                name: "RolePagePermissions",
                schema: "Acl");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Acl");

            migrationBuilder.DropTable(
                name: "UserGridColumns",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "PagePermissions",
                schema: "Acl");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Acl");

            migrationBuilder.DropTable(
                name: "SystemGridColumns",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "ShiftQueues");

            migrationBuilder.DropTable(
                name: "Pages",
                schema: "Acl");

            migrationBuilder.DropTable(
                name: "PermissionsTerms",
                schema: "Acl");

            migrationBuilder.DropTable(
                name: "SystemGrids",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Acl");

            migrationBuilder.DropTable(
                name: "Branchs");

            migrationBuilder.DropTable(
                name: "UserTypes",
                schema: "Acl");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
