-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 11. Feb 2018 um 23:18
-- Server-Version: 10.1.21-MariaDB
-- PHP-Version: 7.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `meme`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `kommentare`
--

DROP TABLE IF EXISTS `kommentare`;
CREATE TABLE IF NOT EXISTS `kommentare` (
  `idkommentare` int(11) NOT NULL AUTO_INCREMENT,
  `inhalt` varchar(250) DEFAULT NULL,
  `autor` int(11) NOT NULL,
  `idupload` int(11) NOT NULL,
  PRIMARY KEY (`idkommentare`),
  KEY `fk_kommentare_user1_idx` (`autor`)
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `kommentare`
--

INSERT INTO `kommentare` (`idkommentare`, `inhalt`, `autor`, `idupload`) VALUES
(1, 'Hi', 1, 1),
(2, 'First', 5, 2),
(23, 'Im feeling great', 1, 3),
(29, 'Reliable', 1, 10),
(30, 'Test', 1, 10),
(31, 'Test', 1, 10),
(32, 'Test', 1, 10),
(33, 'Test', 1, 10),
(34, 'Test', 1, 10),
(39, 'So True', 1, 1),
(40, 'One of the best films ever made', 1, 4),
(41, 'So Me ONG LOL ROFELLL', 1, 7),
(42, 'TRIGGED', 1, 14),
(43, 'Is this from Harry Potter? ðŸ¤¨', 1, 30),
(45, 'This is so sadisfiing', 1, 28),
(46, 'IssoðŸ˜‚', 6, 3),
(47, 'ðŸ˜', 6, 32),
(49, 'Why', 1, 33),
(50, 'Bestes BildðŸ˜', 6, 14),
(51, 'So trueðŸ˜Œ', 6, 34),
(52, 'Test', 1, 6),
(53, 'Hahaha', 6, 34),
(54, 'Test', 1, 6),
(55, 'Ich auch mal', 1, 34),
(57, 'Jo', 1, 1),
(58, 'Jo', 3, 1),
(59, 'Ich liebe es', 1, 37);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `likes`
--

DROP TABLE IF EXISTS `likes`;
CREATE TABLE IF NOT EXISTS `likes` (
  `user_idUser` int(11) NOT NULL,
  `upload_idupload` int(11) NOT NULL,
  PRIMARY KEY (`user_idUser`,`upload_idupload`),
  KEY `fk_user_has_upload_upload1_idx` (`upload_idupload`),
  KEY `fk_user_has_upload_user1_idx` (`user_idUser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `likes`
--

INSERT INTO `likes` (`user_idUser`, `upload_idupload`) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 7),
(1, 43),
(1, 44),
(1, 45),
(1, 48),
(4, 47),
(4, 48);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `upload`
--

DROP TABLE IF EXISTS `upload`;
CREATE TABLE IF NOT EXISTS `upload` (
  `idupload` int(11) NOT NULL AUTO_INCREMENT,
  `uploader` int(11) NOT NULL,
  `datum` date DEFAULT NULL,
  `titel` varchar(45) DEFAULT NULL,
  `ending` varchar(255) DEFAULT NULL,
  `online` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`idupload`),
  KEY `fk_upload_user_idx` (`uploader`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `upload`
--

INSERT INTO `upload` (`idupload`, `uploader`, `datum`, `titel`, `ending`, `online`) VALUES
(1, 1, '2018-01-24', 'Assassins Creed', 'jpg', 1),
(2, 1, '2018-01-24', 'If you ever feel bad...', 'jpg', 1),
(3, 1, '2018-01-24', 'Tool Expectations', 'jpg', 1),
(4, 1, '2018-01-24', 'Epic', 'jpg', 1),
(6, 1, '2018-01-24', 'What a scenery', 'jpg', 1),
(7, 1, '2018-02-02', 'Ich an Valentinstag ', 'png', 1),
(9, 1, '2018-02-02', 'Der Huhu', 'png', 1),
(10, 1, '2018-02-02', 'Meine Freundin', 'png', 1),
(11, 1, '2018-02-02', 'Walls', 'png', 1),
(12, 1, '2018-02-02', 'Loser', 'png', 1),
(13, 1, '2018-02-02', 'Speed', 'png', 1),
(14, 1, '2018-02-02', 'True', 'jpeg', 1),
(15, 1, '2018-02-02', 'Waffenlieferung', 'png', 1),
(16, 1, '2018-02-02', 'All the same ', 'png', 1),
(17, 1, '2018-02-02', 'Mother', 'png', 1),
(19, 1, '2018-02-02', 'Schlucken ', 'png', 1),
(20, 1, '2018-02-02', 'Zoom', 'png', 1),
(21, 1, '2018-02-02', 'Kinder', 'png', 1),
(22, 1, '2018-02-02', 'Drillinge', 'png', 1),
(24, 1, '2018-02-02', 'Die 1. Millionen', 'png', 1),
(25, 1, '2018-02-02', 'Die 1. Millionen', 'png', 0),
(26, 1, '2018-02-02', 'VLC', 'png', 1),
(27, 1, '2018-02-02', 'Silvester', 'png', 1),
(28, 1, '2018-02-02', 'My Cancer is cured', 'gif', 1),
(30, 4, '2018-02-02', 'LONDON BUS', 'JPG', 1),
(31, 6, '2018-02-08', 'Essen', 'png', 1),
(32, 6, '2018-02-08', 'Sex', 'png', 1),
(34, 7, '2018-02-08', 'Nico Heroin', 'png', 1),
(35, 6, '2018-02-08', 'Stau', 'jpg', 1),
(37, 6, '2018-02-08', 'Ganz normale klasse ', 'png', 1),
(38, 6, '2018-02-08', 'Kk', 'jpg', 1),
(40, 6, '2018-02-08', 'SchwÃ¶r', 'jpg', 1),
(42, 1, '2018-02-08', 'I made this', 'jpg', 1),
(43, 1, '2018-02-08', 'Rick and Morty', 'mp4', 1),
(44, 1, '2018-02-11', '3D', 'mp4', 1),
(45, 1, '2018-02-11', 'Mondays be like', 'mp4', 1),
(46, 1, '2018-02-11', 'Next level tennis', 'MOV', 1),
(47, 1, '2018-02-11', 'Meme - The movie', 'MOV', 1),
(48, 1, '2018-02-11', 'Harry popper', 'MOV', 1),
(49, 4, '2018-02-11', 'Who is hyped too?', 'jpg', 1),
(50, 4, '2018-02-11', 'sir do you have a moment to talk about our lo', 'JPG', 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `idUser` int(11) NOT NULL AUTO_INCREMENT,
  `vorname` varchar(45) NOT NULL,
  `nachname` varchar(45) NOT NULL,
  `username` varchar(45) NOT NULL,
  `age` int(11) NOT NULL,
  `password` varchar(100) NOT NULL,
  `permission` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idUser`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `user`
--

INSERT INTO `user` (`idUser`, `vorname`, `nachname`, `username`, `age`, `password`, `permission`) VALUES
(1, 'Tim', 'Suellner', 'TimBang', 18, 'geheim', 10),
(2, 'Jan', 'Fischer', 'k3v1n', 19, '1234', 1),
(3, 'Tim', 'Tim', 'Tim', 111, 'Tim', 1),
(4, 'Bay', 'Max', 'BayMax', 22, '1234', 1),
(5, 'Jens', 'SÃ¼llner', 'Jens09', 49, 'geheim', 1),
(6, 'Marius', 'Krüger', 'Emkay', 18, 'marius1', 1),
(7, 'Nico', 'Engelhard ', 'Carx', 18, '123456789', 1);

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `kommentare`
--
ALTER TABLE `kommentare`
  ADD CONSTRAINT `fk_kommentare_user1` FOREIGN KEY (`autor`) REFERENCES `user` (`idUser`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints der Tabelle `likes`
--
ALTER TABLE `likes`
  ADD CONSTRAINT `fk_user_has_upload_upload1` FOREIGN KEY (`upload_idupload`) REFERENCES `upload` (`idupload`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_user_has_upload_user1` FOREIGN KEY (`user_idUser`) REFERENCES `user` (`idUser`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
