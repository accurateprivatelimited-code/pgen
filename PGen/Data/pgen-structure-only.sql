/*
SQLyog Ultimate v13.1.1 (64 bit)
MySQL - 10.4.28-MariaDB-log : Database - pgen
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`pgen` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `pgen`;

/*Table structure for table `licenses` */

DROP TABLE IF EXISTS `licenses`;

CREATE TABLE `licenses` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `version` varchar(20) NOT NULL,
  `generated_for` varchar(100) DEFAULT NULL COMMENT 'username, nullable',
  `machine_id` varchar(100) NOT NULL,
  `expires_utc` datetime DEFAULT NULL,
  `created_utc` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `FK_licenses_user` (`generated_for`),
  CONSTRAINT `FK_licenses_user` FOREIGN KEY (`generated_for`) REFERENCES `users` (`username`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Table structure for table `meter_types` */

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

/*Table structure for table `meter_key_rows` */

DROP TABLE IF EXISTS `meter_key_rows`;

CREATE TABLE `meter_key_rows` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `session_id` varchar(36) DEFAULT NULL,
  `msn` varchar(50) NOT NULL,
  `meter_type` varchar(50) NOT NULL,
  `model` varchar(50) NOT NULL,
  `set_index` int(11) NOT NULL,
  `ak8` varchar(16) DEFAULT NULL,
  `ek8` varchar(16) DEFAULT NULL,
  `ak32` varchar(64) DEFAULT NULL,
  `ek32` varchar(64) DEFAULT NULL,
  `created_utc` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  UNIQUE KEY `ux_keys_unique` (`ak8`,`ek8`,`ak32`,`ek32`)
) ENGINE=InnoDB AUTO_INCREMENT=1356 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Table structure for table `roles` */

DROP TABLE IF EXISTS `roles`;

CREATE TABLE `roles` (
  `id` varchar(36) NOT NULL COMMENT 'GUID or fixed id (admin/operator)',
  `name` varchar(100) NOT NULL,
  `rights` bigint(20) NOT NULL DEFAULT 0 COMMENT 'bitmask of UserRight',
  `description` text DEFAULT NULL,
  `created_utc` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  UNIQUE KEY `UX_roles_name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `username` varchar(100) NOT NULL,
  `password_hash` char(64) NOT NULL COMMENT 'SHA‑256 hex string',
  `role_id` varchar(36) NOT NULL,
  `created_utc` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`username`),
  KEY `FK_users_role` (`role_id`),
  CONSTRAINT `FK_users_role` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/* Procedure structure for procedure `sp_AddUser` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_AddUser` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_AddUser`(
    IN p_username VARCHAR(100),
    IN p_password_hash CHAR(64),
    IN p_role_id VARCHAR(36)
)
BEGIN
    IF p_username = '' OR p_password_hash = '' OR p_role_id = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'username/password/role required';
    END IF;
    IF EXISTS(SELECT 1 FROM users WHERE username = p_username) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'User already exists';
    END IF;
    INSERT INTO users(username,password_hash,role_id)
    VALUES(p_username,p_password_hash,p_role_id);
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_AuthenticateUser` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_AuthenticateUser` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_AuthenticateUser`(
    IN p_username VARCHAR(100),
    IN p_password_hash CHAR(64)
)
BEGIN
    SELECT username, role_id
    FROM users
    WHERE username = p_username
      AND password_hash = p_password_hash;
    -- caller checks whether a row was returned
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_CreateLicense` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_CreateLicense` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_CreateLicense`(
    IN p_version VARCHAR(20),
    IN p_generated_for VARCHAR(100),
    IN p_machine_id VARCHAR(100),
    IN p_expires_utc DATETIME
)
BEGIN
    INSERT INTO licenses(version,generated_for,machine_id,expires_utc)
    VALUES(p_version,p_generated_for,p_machine_id,p_expires_utc);
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_CreateRole` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_CreateRole` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_CreateRole`(
    IN p_name VARCHAR(100),
    IN p_rights BIGINT,
    IN p_description TEXT
)
BEGIN
    DECLARE v_exists INT;
    SELECT COUNT(*) INTO v_exists FROM roles WHERE name = p_name;
    IF v_exists > 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Role already exists';
    END IF;
    INSERT INTO roles(id,name,rights,description)
    VALUES(UUID(), p_name, p_rights, p_description);
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_DeleteRole` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_DeleteRole` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_DeleteRole`(
    IN p_id VARCHAR(36)
)
BEGIN
    IF p_id IN ('admin','operator') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cannot delete built-in roles';
    END IF;
    IF EXISTS(SELECT 1 FROM users WHERE role_id = p_id) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Role in use by users';
    END IF;
    DELETE FROM roles WHERE id = p_id;
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_DeleteUser` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_DeleteUser` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_DeleteUser`(
    IN p_username VARCHAR(100)
)
BEGIN
    IF LOWER(p_username) = 'admin' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cannot delete admin';
    END IF;
    DELETE FROM users WHERE username = p_username;
    IF ROW_COUNT() = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'User not found';
    END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_EnsureDefaultRoles` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_EnsureDefaultRoles` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_EnsureDefaultRoles`()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM roles WHERE id='admin') THEN
        INSERT INTO roles(id,name,rights,description)
        VALUES('admin','Administrator', (1<<12)-1, 'Full access to all features');
    END IF;
    IF NOT EXISTS(SELECT 1 FROM roles WHERE id='operator') THEN
        INSERT INTO roles(id,name,rights,description)
        VALUES('operator','Operator',
               (1<<8) + (1<<0) + (1<<4) ,
               'Can generate passwords and view information');
    END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_EnsureDefaultUsers` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_EnsureDefaultUsers` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_EnsureDefaultUsers`()
BEGIN
    CALL sp_EnsureDefaultRoles();
    IF NOT EXISTS(SELECT 1 FROM users WHERE username='admin') THEN
        INSERT INTO users(username,password_hash,role_id)
        VALUES('admin', SHA2('admin123!',256), 'admin');
    END IF;
    IF NOT EXISTS(SELECT 1 FROM users WHERE username='operator') THEN
        INSERT INTO users(username,password_hash,role_id)
        VALUES('operator', SHA2('operator123!',256), 'operator');
    END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_GetLicenseById` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_GetLicenseById` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_GetLicenseById`(
    IN p_id BIGINT
)
BEGIN
    SELECT * FROM licenses WHERE id = p_id;
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_UpdateRole` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_UpdateRole` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_UpdateRole`(
    IN p_id VARCHAR(36),
    IN p_name VARCHAR(100),
    IN p_rights BIGINT,
    IN p_description TEXT
)
BEGIN
    IF p_id = '' OR p_name = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Role id and name required';
    END IF;
    IF p_id IN ('admin','operator') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cannot modify built-in roles';
    END IF;
    UPDATE roles
    SET name = p_name,
        rights = p_rights,
        description = p_description
    WHERE id = p_id;
    IF ROW_COUNT() = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Role not found';
    END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `sp_UpdateUserRole` */

/*!50003 DROP PROCEDURE IF EXISTS  `sp_UpdateUserRole` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`apl`@`%` PROCEDURE `sp_UpdateUserRole`(
    IN p_username VARCHAR(100),
    IN p_new_role VARCHAR(36)
)
BEGIN
    IF p_username = '' OR p_new_role = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'username and role required';
    END IF;
    IF LOWER(p_username) = 'admin' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cannot change admin''s role';
    END IF;
    UPDATE users SET role_id = p_new_role WHERE username = p_username;
    IF ROW_COUNT() = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'User not found';
    END IF;
END */$$
DELIMITER ;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
