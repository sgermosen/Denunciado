using Microsoft.EntityFrameworkCore.Migrations;

namespace Denounces.Infraestructure.Migrations
{
    public partial class AddingUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" 
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Lastname], [Rnc], [Phone], [Address]) VALUES (N'45368eda-2f7a-4ed3-aec2-64b7acc219c4', N'sgrysoft@gmail.com', N'SGRYSOFT@GMAIL.COM', N'sgrysoft@gmail.com', N'SGRYSOFT@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGg8QhdOC+psCyFEig3Jn7JaitIGcghMy+l0iNICagMKrq5mpzDDgAlwqgisCihfJQ==', N'ZX4DDC3DF5OKPZQUILKAOZDJWAFU6RPU', N'75de2e98-6665-4210-862e-dafed9f6c4dc', N'849 207 7714', 0, 0, NULL, 1, 0, N'Starling', N'Germosen', NULL, NULL, NULL)
GO
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Lastname], [Rnc], [Phone], [Address]) VALUES (N'a0846894-7abd-41ae-b71a-c1595cd7ecd0', N'supervisor@denunciado.com', N'SUPERVISOR@denunciado.COM', N'supervisor@denunciado.com', N'SUPERVISOR@denunciado.COM', 1, N'AQAAAAEAACcQAAAAEFxACPEqXiUbXHc1ZLwjnQDUY/Qc8dy0uWTYWAc3u/WE2oeBPsfX6uvTm55RLT8D6Q==', N'ACGVTQRU7CNS5HP67WVDAK5OUCGZHWFC', N'ad11a795-a15b-41ce-a78b-a70ebff15661', N'849 207 7714', 0, 0, NULL, 1, 0, N'User', N'Fundacion', NULL, NULL, NULL)
GO
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Lastname], [Rnc], [Phone], [Address]) VALUES (N'c2785a4a-1265-4fcf-9056-b05dde4d3141', N'admin@denunciado.com', N'ADMIN@denunciado.COM', N'admin@denunciado.com', N'ADMIN@denunciado.COM', 1, N'AQAAAAEAACcQAAAAENnEDuH+z/RkHmohKLbA3RNjAJEfVHoubAWOoOy54/Et4NrEzS/W4JX27DmkeYOUAg==', N'MGTAMZQQTWR5O3L2LYPBZ2YS26UL4OGU', N'64603234-828a-4ff7-bbdc-19e37bde8b54', N'849 207 7714', 0, 0, NULL, 1, 0, N'Admin', N'Fundacion', NULL, NULL, NULL)
GO
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Lastname], [Rnc], [Phone], [Address]) VALUES (N'c64b74ac-def7-4195-969d-1b6acc08192b', N'user@denunciado.com', N'USER@denunciado.COM', N'user@denunciado.com', N'USER@denunciado.COM', 1, N'AQAAAAEAACcQAAAAEJWYuSZHlZxHLETXiryBMjqfhBP1nKSv13CrdGIplz5M3JfwDMOl9DNHrHk6GcUdxg==', N'QMMSCVKCJAEQTISC5PZ3RDAQKQPTEBMO', N'4966412b-190c-4b94-9c71-740c56284dcd', N'849 207 7714', 0, 0, NULL, 1, 0, N'User', N'Fundacion', NULL, NULL, NULL)
GO 
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'160e2ca4-d166-4bb4-a7f0-c2e038d6abbf', N'User', N'USER', N'640792c1-abeb-4361-879d-a4a70de326db')
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'47a091fe-9535-45f1-bc20-de8cb66acadc', N'Supervisor', N'SUPERVISOR', N'c9a720c8-f94a-48a0-94fd-f6c8a0ee5c0f')
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ce7b0eb1-57ee-439d-9acf-6934c8b28a4e', N'Admin', N'ADMIN', N'76edb7ca-07d6-416e-bfdf-ac399c9ca465')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'c64b74ac-def7-4195-969d-1b6acc08192b', N'160e2ca4-d166-4bb4-a7f0-c2e038d6abbf')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'a0846894-7abd-41ae-b71a-c1595cd7ecd0', N'47a091fe-9535-45f1-bc20-de8cb66acadc')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'45368eda-2f7a-4ed3-aec2-64b7acc219c4', N'ce7b0eb1-57ee-439d-9acf-6934c8b28a4e')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'c2785a4a-1265-4fcf-9056-b05dde4d3141', N'ce7b0eb1-57ee-439d-9acf-6934c8b28a4e')
GO
SET IDENTITY_INSERT [dbo].[ProposalTypes] ON 
GO
INSERT [dbo].[ProposalTypes] ([Id], [Name], [Deleted], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [Logo], [IsActive], [Order]) VALUES (1, N'Denuncia', 0, CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', NULL, NULL, NULL, 1, 9)
GO
INSERT [dbo].[ProposalTypes] ([Id], [Name], [Deleted], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [Logo], [IsActive], [Order]) VALUES (2, N'Irregularidad', 0, CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', NULL, NULL, NULL, 1, 9)
GO
INSERT [dbo].[ProposalTypes] ([Id], [Name], [Deleted], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [Logo], [IsActive], [Order]) VALUES (3, N'Robo', 0, CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', NULL, NULL, NULL, 1, 9)
GO
INSERT [dbo].[ProposalTypes] ([Id], [Name], [Deleted], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [Logo], [IsActive], [Order]) VALUES (4, N'Zona Peligrosa', 0, CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', NULL, NULL, NULL, 1, 9)
GO
INSERT [dbo].[ProposalTypes] ([Id], [Name], [Deleted], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [Logo], [IsActive], [Order]) VALUES (5, N'Propuesta Legislativa', 0, CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', NULL, NULL, NULL, 1, 9)
GO
INSERT [dbo].[ProposalTypes] ([Id], [Name], [Deleted], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [Logo], [IsActive], [Order]) VALUES (6, N'Propuesta Congresual', 0, CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', NULL, NULL, NULL, 1, 9)
GO
INSERT [dbo].[ProposalTypes] ([Id], [Name], [Deleted], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [Logo], [IsActive], [Order]) VALUES (7, N'Propuesta Gubernamental', 0, CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', NULL, NULL, NULL, 1, 9)
GO
INSERT [dbo].[ProposalTypes] ([Id], [Name], [Deleted], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [Logo], [IsActive], [Order]) VALUES (8, N'Avería', 0, CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', CAST(N'2020-12-12T00:00:00.0000000' AS DateTime2), N'c2785a4a-1265-4fcf-9056-b05dde4d3141', NULL, NULL, NULL, 1, 9)
GO
SET IDENTITY_INSERT [dbo].[ProposalTypes] OFF
GO
");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
