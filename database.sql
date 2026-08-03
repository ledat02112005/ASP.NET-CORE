-- =====================================================
-- SportsStore Database Setup Script
-- Chạy file này trên phpMyAdmin hoặc MySQL CLI
-- =====================================================

-- 1. Tạo database (nếu chưa có)
CREATE DATABASE IF NOT EXISTS `SportsStore`
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE `SportsStore`;

-- 2. Tạo bảng Products
CREATE TABLE IF NOT EXISTS `Products` (
  `ProductID`   INT           NOT NULL AUTO_INCREMENT,
  `Name`        VARCHAR(255)  NOT NULL,
  `Description` VARCHAR(500)  NOT NULL,
  `Price`       DECIMAL(18,2) NOT NULL,
  `Category`    VARCHAR(100)  NOT NULL,
  `ImageUrl`    VARCHAR(500)  NULL,
  PRIMARY KEY (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 3. Thêm dữ liệu mẫu
INSERT INTO `Products` (`Name`, `Description`, `Price`, `Category`, `ImageUrl`) VALUES
('Football',      'FIFA-approved size and weight',                25.00,  'Soccer',      '/images/football.png'),
('Surf Board',    'A board for riding the waves',                179.00,  'Surfing',     '/images/surfboard.png'),
('Running Shoes', 'Comfortable and stylish running shoes',        95.00,  'Running',     '/images/runningshoes.png'),
('Kayak',         'A boat for one person',                       275.00,  'Watersports', '/images/kayak.png'),
('Corner Flags',  'Give your playing field a professional touch',  34.95, 'Soccer',      '/images/cornerflags.png');
