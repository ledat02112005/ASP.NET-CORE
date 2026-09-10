-- =====================================================
-- SportsStore Database Setup Script
-- MySQL / MariaDB
-- Chạy file này trên phpMyAdmin hoặc MySQL CLI
-- =====================================================

-- 1. Xóa và tạo lại database
DROP DATABASE IF EXISTS `SportsStore`;

CREATE DATABASE `SportsStore`
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE `SportsStore`;

-- 2. Tạo bảng EF Migrations History (EF Core tự quản lý)
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId`   VARCHAR(150) NOT NULL,
  `ProductVersion` VARCHAR(32) NOT NULL,
  CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

-- Đánh dấu migration đã được áp dụng (khớp với tên file migration)
INSERT INTO `__EFMigrationsHistory` VALUES ('20260806035417_InitialCreate', '8.0.2');

-- 3. Tạo bảng Products (cấu trúc khớp với EF Core migration)
CREATE TABLE `Products` (
  `ProductID`   INT            NOT NULL AUTO_INCREMENT,
  `Name`        LONGTEXT       NOT NULL,
  `Description` LONGTEXT       NOT NULL,
  `Price`       DECIMAL(18,2)  NOT NULL,
  `Category`    LONGTEXT       NOT NULL,
  `ImageUrl`    LONGTEXT       NULL,
  CONSTRAINT `PK_Products` PRIMARY KEY (`ProductID`)
) CHARACTER SET=utf8mb4;

-- 4. Thêm dữ liệu mẫu (12 sản phẩm)
INSERT INTO `Products` (`Name`, `Description`, `Price`, `Category`, `ImageUrl`) VALUES
('Thuyền Kayak',         'Chiếc thuyền nhỏ cho một người chèo.',                  6600000.00,    'Thể thao dưới nước', '/images/kayak.png'),
('Áo phao cứu sinh',     'Bảo hộ an toàn, kiểu dáng thời trang.',                 1175000.00,    'Thể thao dưới nước', '/images/lifejacket.png'),
('Bóng đá tiêu chuẩn',   'Bóng đạt chuẩn kích thước và trọng lượng FIFA.',          470000.00,    'Bóng đá',            '/images/soccer-ball.png'),
('Cờ góc sân',           'Tạo vẻ chuyên nghiệp cho sân bóng của bạn.',              840000.00,    'Bóng đá',            '/images/corner-flag.png'),
('Mô hình sân vận động', 'Sân vận động 35,000 chỗ ngồi (đóng gói phẳng).',      1908000000.00,    'Bóng đá',            '/images/stadium.png'),
('Mũ Tư Duy',            'Cải thiện 75% hiệu suất hoạt động của não.',              385000.00,    'Cờ Vua',             '/images/chess-board.png'),
('Ghế không vững',       'Bí mật gây bất lợi cho đối thủ của bạn.',                 720000.00,    'Cờ Vua',             '/images/unstable-chair.png'),
('Bàn cờ người',         'Trò chơi thú vị cho cả gia đình.',                       1800000.00,    'Cờ Vua',             '/images/chess-board.png'),
('Quân Vua Kim Cương',   'Quân Vua mạ vàng, đính kim cương.',                     28800000.00,    'Cờ Vua',             '/images/chess-king.png'),
('Giày chạy bộ',         'Nhẹ và thoải mái cho quãng đường dài.',                  2400000.00,    'Chạy bộ',            '/images/running-shoes.png'),
('Thảm Yoga cao cấp',    'Bề mặt chống trượt giúp giữ thăng bằng hoàn hảo.',        840000.00,    'Fitness',            '/images/yoga-mat.png'),
('Bình nước giữ nhiệt',  'Giữ lạnh đồ uống trong 24 giờ.',                          380000.00,    'Fitness',            '/images/water-bottle.png');

-- 5. Kiểm tra kết quả
SELECT * FROM `Products`;


-- =====================================================
-- SportsStoreIdentity Database Setup (ASP.NET Core Identity)
-- =====================================================

DROP DATABASE IF EXISTS `SportsStoreIdentity`;

CREATE DATABASE `SportsStoreIdentity`
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE `SportsStoreIdentity`;

-- EF Migrations History cho AppIdentityDbContext
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId`    VARCHAR(150) NOT NULL,
  `ProductVersion` VARCHAR(32)  NOT NULL,
  CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` VALUES ('20260910023852_IdentityInitial', '8.0.2');

-- Bảng Roles
CREATE TABLE `AspNetRoles` (
  `Id`               VARCHAR(255) NOT NULL,
  `Name`             VARCHAR(256) NULL,
  `NormalizedName`   VARCHAR(256) NULL,
  `ConcurrencyStamp` LONGTEXT     NULL,
  CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

-- Bảng Users
CREATE TABLE `AspNetUsers` (
  `Id`                   VARCHAR(255) NOT NULL,
  `UserName`             VARCHAR(256) NULL,
  `NormalizedUserName`   VARCHAR(256) NULL,
  `Email`                VARCHAR(256) NULL,
  `NormalizedEmail`      VARCHAR(256) NULL,
  `EmailConfirmed`       TINYINT(1)   NOT NULL DEFAULT 0,
  `PasswordHash`         LONGTEXT     NULL,
  `SecurityStamp`        LONGTEXT     NULL,
  `ConcurrencyStamp`     LONGTEXT     NULL,
  `PhoneNumber`          LONGTEXT     NULL,
  `PhoneNumberConfirmed` TINYINT(1)   NOT NULL DEFAULT 0,
  `TwoFactorEnabled`     TINYINT(1)   NOT NULL DEFAULT 0,
  `LockoutEnd`           DATETIME(6)  NULL,
  `LockoutEnabled`       TINYINT(1)   NOT NULL DEFAULT 0,
  `AccessFailedCount`    INT          NOT NULL DEFAULT 0,
  CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);
CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

-- Bảng RoleClaims
CREATE TABLE `AspNetRoleClaims` (
  `Id`          INT          NOT NULL AUTO_INCREMENT,
  `RoleId`      VARCHAR(255) NOT NULL,
  `ClaimType`   LONGTEXT     NULL,
  `ClaimValue`  LONGTEXT     NULL,
  CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

-- Bảng UserClaims
CREATE TABLE `AspNetUserClaims` (
  `Id`         INT          NOT NULL AUTO_INCREMENT,
  `UserId`     VARCHAR(255) NOT NULL,
  `ClaimType`  LONGTEXT     NULL,
  `ClaimValue` LONGTEXT     NULL,
  CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

-- Bảng UserLogins
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider`       VARCHAR(128) NOT NULL,
  `ProviderKey`         VARCHAR(128) NOT NULL,
  `ProviderDisplayName` LONGTEXT     NULL,
  `UserId`              VARCHAR(255) NOT NULL,
  CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

-- Bảng UserRoles
CREATE TABLE `AspNetUserRoles` (
  `UserId` VARCHAR(255) NOT NULL,
  `RoleId` VARCHAR(255) NOT NULL,
  CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

-- Bảng UserTokens
CREATE TABLE `AspNetUserTokens` (
  `UserId`        VARCHAR(255) NOT NULL,
  `LoginProvider` VARCHAR(128) NOT NULL,
  `Name`          VARCHAR(128) NOT NULL,
  `Value`         LONGTEXT     NULL,
  CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

-- =====================================================
-- Lưu ý: KHÔNG cần INSERT user Admin vào đây.
-- IdentitySeedData.cs trong app sẽ tự tạo user Admin
-- khi app khởi động lần đầu (nếu chưa tồn tại).
-- =====================================================
