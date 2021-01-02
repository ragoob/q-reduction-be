﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QReduction.Infrastructure.DbContexts;

namespace QReduction.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210102173738_UpdateShiftStartAndEndDataTypeToString")]
    partial class UpdateShiftStartAndEndDataTypeToString
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QReduction.Core.Domain.Acl.About", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateAt");

                    b.Property<int?>("CreateBy");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LabelTextAr");

                    b.Property<string>("LabelTextEn");

                    b.Property<string>("LabelValueAr");

                    b.Property<string>("LabelValueEn");

                    b.Property<DateTime?>("UpdateAt");

                    b.Property<int?>("UpdateBy");

                    b.HasKey("Id");

                    b.ToTable("About");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.HelpAndSupport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateAt");

                    b.Property<int?>("CreateBy");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Message");

                    b.Property<string>("MessageTitle");

                    b.Property<DateTime?>("UpdateAt");

                    b.Property<int?>("UpdateBy");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HelpAndSupport");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.LoginProviders", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProviderType");

                    b.Property<string>("Providertoken");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginProviders");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.Page", b =>
                {
                    b.Property<int>("Id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Pages","Acl");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.PagePermission", b =>
                {
                    b.Property<int>("Id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("ACLName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("PageId");

                    b.Property<int>("PermissionTermId");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.HasIndex("PermissionTermId");

                    b.ToTable("PagePermissions","Acl");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.PermissionsTerm", b =>
                {
                    b.Property<int>("Id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PermissionsTerms","Acl");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code");

                    b.Property<DateTime?>("CreateAt");

                    b.Property<int?>("CreateBy");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("ReadOnly");

                    b.Property<DateTime?>("UpdateAt");

                    b.Property<int?>("UpdateBy");

                    b.HasKey("Id");

                    b.ToTable("Roles","Acl");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.RolePagePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PagePermissionId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("PagePermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePagePermissions","Acl");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BranchId");

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<int?>("CreatedBy");

                    b.Property<string>("Email")
                        .HasMaxLength(225);

                    b.Property<string>("FirstName");

                    b.Property<string>("ForgotPasswordCode")
                        .HasMaxLength(6);

                    b.Property<DateTime?>("ForgotPasswordExpiration");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsFirstLogin");

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastLoginUtcDate");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<int?>("OrganizationId");

                    b.Property<byte[]>("Password");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50);

                    b.Property<string>("PhotoPath");

                    b.Property<Guid>("RowGuid");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<int?>("UpdatedBy");

                    b.Property<string>("UserDeviceId");

                    b.Property<Guid>("UserGuid");

                    b.Property<string>("UserName")
                        .HasMaxLength(255);

                    b.Property<int?>("UserTypeId");

                    b.Property<string>("VerificationCode")
                        .HasMaxLength(4);

                    b.Property<DateTime?>("VerificationCodeExpiration");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserName");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users","Acl");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles","Acl");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.UserType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("NameAr")
                        .HasMaxLength(225);

                    b.Property<string>("NameEn")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("UserTypes","Acl");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BranchAddress")
                        .HasMaxLength(500);

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("CreateBy");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note")
                        .HasMaxLength(500);

                    b.Property<int?>("OrganizationId");

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<string>("QrCode");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("UpdateBy");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Branchs");
                });

            modelBuilder.Entity("QReduction.Core.Domain.BranchService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("ServiceId");

                    b.ToTable("BranchServices");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("ShiftQueueId");

                    b.HasKey("Id");

                    b.HasIndex("ShiftQueueId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("QReduction.Core.Domain.EvaluationQuestionAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnswerValue");

                    b.Property<int?>("EvaluationId");

                    b.Property<int?>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationId");

                    b.HasIndex("QuestionId");

                    b.ToTable("EvaluationQuestionAnswer");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Instruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("CreateBy");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("OrganizationId");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("UpdateBy");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(500);

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("CreateBy");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note")
                        .HasMaxLength(500);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("UpdateBy");

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("CreateBy");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("OrganizationId");

                    b.Property<string>("QuestionTextAr")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("QuestionTextEn")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("UpdateBy");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("CreateBy");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("DeletedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note")
                        .HasMaxLength(505);

                    b.Property<string>("NoteEN");

                    b.Property<int>("OrganizationId");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("UpdateBy");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Settings.SystemGrid", b =>
                {
                    b.Property<int>("Id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("SystemGrids","Settings");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Settings.SystemGridColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("SystemGridId");

                    b.Property<bool>("VisibilityDefault");

                    b.HasKey("Id");

                    b.HasIndex("SystemGridId");

                    b.ToTable("SystemGridColumns","Settings");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Settings.UserGridColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsVisible");

                    b.Property<int>("SystemGridColumnId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SystemGridColumnId");

                    b.ToTable("UserGridColumns","Settings");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId")
                        .HasMaxLength(50);

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("CreateBy");

                    b.Property<string>("End");

                    b.Property<DateTime?>("EndAt")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsEnded");

                    b.Property<string>("QRCode");

                    b.Property<string>("Start");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("UpdateBy");

                    b.Property<int?>("UserIdSupport");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("UserIdSupport");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("QReduction.Core.Domain.ShiftQueue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsServiceDone");

                    b.Property<string>("PushId");

                    b.Property<int>("ServiceId");

                    b.Property<int>("ShiftId");

                    b.Property<int?>("UserIdBy");

                    b.Property<int>("UserIdMobile");

                    b.Property<int>("UserTurn");

                    b.Property<string>("WindowNumber")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("ShiftId");

                    b.HasIndex("UserIdBy");

                    b.HasIndex("UserIdMobile");

                    b.ToTable("ShiftQueues");
                });

            modelBuilder.Entity("QReduction.Core.Domain.ShiftUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("ServiceId");

                    b.Property<int>("ShiftId");

                    b.Property<int>("UserId");

                    b.Property<string>("WindowNumber")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("ShiftId");

                    b.HasIndex("UserId");

                    b.ToTable("ShiftUsers");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.HelpAndSupport", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Acl.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.LoginProviders", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Acl.User")
                        .WithMany("LoginProviders")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.PagePermission", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Acl.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QReduction.Core.Domain.Acl.PermissionsTerm", "PermissionsTerm")
                        .WithMany()
                        .HasForeignKey("PermissionTermId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.RolePagePermission", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Acl.PagePermission", "PagePermission")
                        .WithMany()
                        .HasForeignKey("PagePermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QReduction.Core.Domain.Acl.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.User", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId");

                    b.HasOne("QReduction.Core.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QReduction.Core.Domain.Acl.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Acl.UserRole", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Acl.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QReduction.Core.Domain.Acl.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Branch", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("QReduction.Core.Domain.BranchService", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Branch", "Branch")
                        .WithMany("BranchServices")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QReduction.Core.Domain.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Evaluation", b =>
                {
                    b.HasOne("QReduction.Core.Domain.ShiftQueue", "ShiftQueue")
                        .WithMany()
                        .HasForeignKey("ShiftQueueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QReduction.Core.Domain.EvaluationQuestionAnswer", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Evaluation", "Evaluation")
                        .WithMany("EvaluationQuestionAnswers")
                        .HasForeignKey("EvaluationId");

                    b.HasOne("QReduction.Core.Domain.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("QReduction.Core.Domain.Instruction", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Question", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Service", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Settings.SystemGridColumn", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Settings.SystemGrid", "SystemGrid")
                        .WithMany()
                        .HasForeignKey("SystemGridId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Settings.UserGridColumn", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Settings.SystemGridColumn", "SystemGridColumn")
                        .WithMany()
                        .HasForeignKey("SystemGridColumnId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QReduction.Core.Domain.Shift", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("QReduction.Core.Domain.Acl.User", "UserSupport")
                        .WithMany()
                        .HasForeignKey("UserIdSupport")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("QReduction.Core.Domain.ShiftQueue", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("QReduction.Core.Domain.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("QReduction.Core.Domain.Acl.User", "UserBy")
                        .WithMany()
                        .HasForeignKey("UserIdBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("QReduction.Core.Domain.Acl.User", "UserMobile")
                        .WithMany()
                        .HasForeignKey("UserIdMobile")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("QReduction.Core.Domain.ShiftUser", b =>
                {
                    b.HasOne("QReduction.Core.Domain.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");

                    b.HasOne("QReduction.Core.Domain.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QReduction.Core.Domain.Acl.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
