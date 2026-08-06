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
