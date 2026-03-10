-- Meter types table for PGen meter type dropdown
-- Run this script to create the table and seed default values

USE `pgen`;

DROP TABLE IF EXISTS `meter_types`;

CREATE TABLE `meter_types` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL COMMENT 'Display name for meter type',
  `sort_order` int(11) NOT NULL DEFAULT 0 COMMENT 'Order in dropdown (lower first)',
  `is_active` tinyint(1) NOT NULL DEFAULT 1,
  `created_utc` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  UNIQUE KEY `ux_meter_types_name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Seed default meter types (matching original hardcoded list)
INSERT INTO `meter_types` (`name`, `sort_order`) VALUES
('1P GPRS', 1),
('3P WC GPRS UNI', 2),
('3P WC GPRS BI', 3),
('3P WC NO AMR UNI', 4),
('3P WC NO AMR BI', 5),
('LT/HT GPRS UNI', 6),
('LT/HT GPRS BI', 7),
('LT NON AMR', 8),
('HT NON AMR', 9),
('SMCD', 10),
('P202', 11),
('1P NON AMR', 12);
