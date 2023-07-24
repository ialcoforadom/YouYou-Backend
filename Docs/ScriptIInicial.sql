Use YouYou;

INSERT into AspNetRoles (Id,Name,NormalizedName,ConcurrencyStamp) VALUES 
('b4029371-be9c-45a1-94a4-ccac2dff88db', 'Admin', 'ADMIN', 'cc7ff09f-6101-4c22-928b-c3040a1f36b5'),
('efcb9598-9ac4-47de-9a9a-583c5192facb', 'Operador', 'OPERADOR', '43868724-7058-4f92-8b60-62d44990706c'),
('091e4c3d-0aa4-48a6-b201-6e69c9577e51', 'Coordenador', 'COORDENADOR', 'ee5f4623-c64d-4fff-a936-28c13d606bd0'),
('b73a8dfc-4c15-474f-a30d-7df6f48179fa', 'Designer', 'DESIGNER', 'ee5f4623-c64d-4fff-a936-28c13d606bd0'),
('a296fc56-ef20-4060-a65a-3e1385ff49de', 'Editor', 'EDITOR', '1acec6d4-88e7-4cfb-b1c3-8ab369b10c6b'),
('ec27cb0e-579c-45f3-94e8-25b92dc4a2d9', 'Cliente','CLIENTE', 'f31cf581-ee6c-4fba-b1a6-51ff65fd0825');

INSERT INTO AspNetUsers
(Id, IsCompany, Disabled, IsDeleted, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, NickName)
VALUES(N'74832FD1-446A-4286-F902-08DB6782C2AC', 0, 0, 0, N'admin@email.com', N'ADMIN@EMAIL.COM', N'admin@email.com', N'ADMIN@EMAIL.COM', 0, N'AQAAAAEAACcQAAAAELw8C9UCcnuE/tFEbSZDMFspZbq9jmLFJ3Aw6WprQdFCQi/JZdm6h6vXuV2F7KTWdA==', N'GP2PC427FELBKT27LR7U2P74B7JRKWD7', N'4830a09b-9b6d-4208-bf01-677e9108599f', NULL, 0, 0, NULL, 1, 0, N'admin@email.com');


INSERT AspNetUserRoles (UserId, RoleId) VALUES
('74832FD1-446A-4286-F902-08DB6782C2AC', 'b4029371-be9c-45a1-94a4-ccac2dff88db');