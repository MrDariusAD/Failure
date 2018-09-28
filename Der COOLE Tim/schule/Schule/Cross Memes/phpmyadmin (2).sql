-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 07. Feb 2018 um 00:56
-- Server-Version: 10.1.21-MariaDB
-- PHP-Version: 7.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `phpmyadmin`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `dislikes`
--

DROP TABLE IF EXISTS `dislikes`;
CREATE TABLE `dislikes` (
  `user_idUser` int(11) NOT NULL,
  `upload_idupload` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `dislikes`
--

INSERT INTO `dislikes` (`user_idUser`, `upload_idupload`) VALUES
(1, 1),
(1, 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `kommentare`
--

DROP TABLE IF EXISTS `kommentare`;
CREATE TABLE `kommentare` (
  `idkommentare` int(11) NOT NULL,
  `inhalt` varchar(250) DEFAULT NULL,
  `autor` int(11) NOT NULL,
  `idupload` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
(43, 'Is this from Harry Potter? ðŸ¤¨', 1, 30);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `likes`
--

DROP TABLE IF EXISTS `likes`;
CREATE TABLE `likes` (
  `user_idUser` int(11) NOT NULL,
  `upload_idupload` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `likes`
--

INSERT INTO `likes` (`user_idUser`, `upload_idupload`) VALUES
(1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `upload`
--

DROP TABLE IF EXISTS `upload`;
CREATE TABLE `upload` (
  `idupload` int(11) NOT NULL,
  `uploader` int(11) NOT NULL,
  `datum` date DEFAULT NULL,
  `titel` varchar(45) DEFAULT NULL,
  `ending` varchar(255) DEFAULT NULL,
  `online` tinyint(1) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `upload`
--

INSERT INTO `upload` (`idupload`, `uploader`, `datum`, `titel`, `ending`, `online`) VALUES
(1, 1, '2018-01-24', 'Assassins Creed', 'jpg', 1),
(2, 1, '2018-01-24', 'If you ever feel bad...', 'jpg', 1),
(3, 1, '2018-01-24', 'Tool Expectations', 'jpg', 1),
(4, 1, '2018-01-24', 'Epic', 'jpg', 1),
(5, 1, '2018-01-24', 'LONDON', 'jpg', 1),
(6, 1, '2018-01-24', 'What a scenery', 'jpg', 1),
(7, 1, '2018-02-02', 'Ich an Valentinstag ', 'png', 1),
(8, 1, '2018-02-02', 'Kamele', 'png', 1),
(9, 1, '2018-02-02', 'Der Huhu', 'png', 1),
(10, 1, '2018-02-02', 'Meine Freundin', 'png', 1),
(11, 1, '2018-02-02', 'Walls', 'png', 1),
(12, 1, '2018-02-02', 'Loser', 'png', 1),
(13, 1, '2018-02-02', 'Speed', 'png', 1),
(14, 1, '2018-02-02', 'True', 'jpeg', 1),
(15, 1, '2018-02-02', 'Waffenlieferung', 'png', 1),
(16, 1, '2018-02-02', 'All the same ', 'png', 1),
(17, 1, '2018-02-02', 'Mother', 'png', 1),
(18, 1, '2018-02-02', 'Awesome ', 'jpeg', 1),
(19, 1, '2018-02-02', 'Schlucken ', 'png', 1),
(20, 1, '2018-02-02', 'Zoom', 'png', 1),
(21, 1, '2018-02-02', 'Kinder', 'png', 1),
(22, 1, '2018-02-02', 'Drillinge', 'png', 1),
(23, 1, '2018-02-02', 'The Witcher', 'jpeg', 1),
(24, 1, '2018-02-02', 'Die 1. Millionen', 'png', 1),
(25, 1, '2018-02-02', 'Die 1. Millionen', 'png', 0),
(26, 1, '2018-02-02', 'VLC', 'png', 1),
(27, 1, '2018-02-02', 'Silvester', 'png', 1),
(28, 1, '2018-02-02', 'My Cancer is cured', 'gif', 1),
(29, 1, '2018-02-02', 'dfg', 'JPG', 1),
(30, 4, '2018-02-02', 'LONDON BUS', 'JPG', 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `idUser` int(11) NOT NULL,
  `vorname` varchar(45) NOT NULL,
  `nachname` varchar(45) NOT NULL,
  `username` varchar(45) NOT NULL,
  `age` int(11) NOT NULL,
  `password` varchar(100) NOT NULL,
  `permission` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `user`
--

INSERT INTO `user` (`idUser`, `vorname`, `nachname`, `username`, `age`, `password`, `permission`) VALUES
(1, 'Tim', 'Suellner', 'TimBang', 18, 'geheim', 10),
(2, 'Jan', 'Fischer', 'k3v1n', 19, '1234', 1),
(3, 'Tim', 'Tim', 'Tim', 111, 'Tim', 1),
(4, 'Bay', 'Max', 'BayMax', 22, '1234', 1),
(5, 'Jens', 'SÃ¼llner', 'Jens09', 49, 'geheim', 1),
(6, 'Marius', 'Krüger', 'Emkay', 18, 'marius1', 1);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `dislikes`
--
ALTER TABLE `dislikes`
  ADD PRIMARY KEY (`user_idUser`,`upload_idupload`),
  ADD KEY `fk_user_has_upload_upload2_idx` (`upload_idupload`),
  ADD KEY `fk_user_has_upload_user2_idx` (`user_idUser`);

--
-- Indizes für die Tabelle `kommentare`
--
ALTER TABLE `kommentare`
  ADD PRIMARY KEY (`idkommentare`),
  ADD KEY `fk_kommentare_user1_idx` (`autor`);

--
-- Indizes für die Tabelle `likes`
--
ALTER TABLE `likes`
  ADD PRIMARY KEY (`user_idUser`,`upload_idupload`),
  ADD KEY `fk_user_has_upload_upload1_idx` (`upload_idupload`),
  ADD KEY `fk_user_has_upload_user1_idx` (`user_idUser`);

--
-- Indizes für die Tabelle `upload`
--
ALTER TABLE `upload`
  ADD PRIMARY KEY (`idupload`),
  ADD KEY `fk_upload_user_idx` (`uploader`);

--
-- Indizes für die Tabelle `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`idUser`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `kommentare`
--
ALTER TABLE `kommentare`
  MODIFY `idkommentare` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;
--
-- AUTO_INCREMENT für Tabelle `upload`
--
ALTER TABLE `upload`
  MODIFY `idupload` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;
--
-- AUTO_INCREMENT für Tabelle `user`
--
ALTER TABLE `user`
  MODIFY `idUser` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `dislikes`
--
ALTER TABLE `dislikes`
  ADD CONSTRAINT `fk_user_has_upload_upload2` FOREIGN KEY (`upload_idupload`) REFERENCES `upload` (`idupload`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_user_has_upload_user2` FOREIGN KEY (`user_idUser`) REFERENCES `user` (`idUser`) ON DELETE NO ACTION ON UPDATE NO ACTION;

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

--
-- Constraints der Tabelle `upload`
--
ALTER TABLE `upload`
  ADD CONSTRAINT `fk_upload_user` FOREIGN KEY (`uploader`) REFERENCES `user` (`idUser`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
