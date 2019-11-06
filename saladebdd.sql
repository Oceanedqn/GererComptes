-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  mer. 06 nov. 2019 à 11:38
-- Version du serveur :  5.7.24
-- Version de PHP :  7.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `saladebdd`
--

-- --------------------------------------------------------

--
-- Structure de la table `e_city_eci`
--

DROP TABLE IF EXISTS `e_city_eci`;
CREATE TABLE IF NOT EXISTS `e_city_eci` (
  `ECI_id` int(11) NOT NULL AUTO_INCREMENT,
  `ECI_city` varchar(30) NOT NULL,
  `ECI_CP` int(11) NOT NULL,
  PRIMARY KEY (`ECI_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `e_meansofpayement_emp`
--

DROP TABLE IF EXISTS `e_meansofpayement_emp`;
CREATE TABLE IF NOT EXISTS `e_meansofpayement_emp` (
  `EMP_id` int(11) NOT NULL AUTO_INCREMENT,
  `EMP_MeansOfPayement` varchar(30) NOT NULL,
  PRIMARY KEY (`EMP_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `e_meansofpayement_emp`
--

INSERT INTO `e_meansofpayement_emp` (`EMP_id`, `EMP_MeansOfPayement`) VALUES
(8, 'Paypal'),
(9, '****'),
(10, 'Espèces'),
(11, 'Lydia'),
(12, 'Virement automatique');

-- --------------------------------------------------------

--
-- Structure de la table `e_payement_epa`
--

DROP TABLE IF EXISTS `e_payement_epa`;
CREATE TABLE IF NOT EXISTS `e_payement_epa` (
  `EPA_id` int(11) NOT NULL AUTO_INCREMENT,
  `EPA_date` date NOT NULL,
  `EPA_amount` int(11) NOT NULL,
  `EPA_comment` varchar(100) DEFAULT NULL,
  `EUS_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`EPA_id`),
  KEY `E_PAYEMENT_EPA_E_USER_EUS_FK` (`EUS_id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `e_user_eus`
--

DROP TABLE IF EXISTS `e_user_eus`;
CREATE TABLE IF NOT EXISTS `e_user_eus` (
  `EUS_id` int(11) NOT NULL AUTO_INCREMENT,
  `EUS_name` varchar(30) NOT NULL,
  `EUS_firstname` varchar(30) NOT NULL,
  `EUS_dateStart` date NOT NULL,
  `EUS_email` varchar(50) NOT NULL,
  `ECI_id` int(11) DEFAULT NULL,
  `EMP_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`EUS_id`),
  KEY `E_USER_EUS_E_CITY_ECI_FK` (`ECI_id`),
  KEY `E_USER_EUS_E_MeansOfPayement_EMP_FK` (`EMP_id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=latin1;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `e_payement_epa`
--
ALTER TABLE `e_payement_epa`
  ADD CONSTRAINT `E_PAYEMENT_EPA_E_USER_EUS_FK` FOREIGN KEY (`EUS_id`) REFERENCES `e_user_eus` (`EUS_id`);

--
-- Contraintes pour la table `e_user_eus`
--
ALTER TABLE `e_user_eus`
  ADD CONSTRAINT `E_USER_EUS_E_CITY_ECI_FK` FOREIGN KEY (`ECI_id`) REFERENCES `e_city_eci` (`ECI_id`),
  ADD CONSTRAINT `E_USER_EUS_E_MeansOfPayement_EMP_FK` FOREIGN KEY (`EMP_id`) REFERENCES `e_meansofpayement_emp` (`EMP_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
