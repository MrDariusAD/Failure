-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 28. Jan 2018 um 20:10
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

CREATE TABLE `dislikes` (
  `user_idUser` int(11) NOT NULL,
  `upload_idupload` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TRUNCATE Tabelle vor dem Einfügen `dislikes`
--

TRUNCATE TABLE `dislikes`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `kommentare`
--

CREATE TABLE `kommentare` (
  `idkommentare` int(11) NOT NULL,
  `inhalt` varchar(250) DEFAULT NULL,
  `autor` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TRUNCATE Tabelle vor dem Einfügen `kommentare`
--

TRUNCATE TABLE `kommentare`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `likes`
--

CREATE TABLE `likes` (
  `user_idUser` int(11) NOT NULL,
  `upload_idupload` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TRUNCATE Tabelle vor dem Einfügen `likes`
--

TRUNCATE TABLE `likes`;
--
-- Daten für Tabelle `likes`
--

INSERT DELAYED INTO `likes` (`user_idUser`, `upload_idupload`) VALUES
(1, 1),
(2, 1),
(3, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `upload`
--

CREATE TABLE `upload` (
  `idupload` int(11) NOT NULL,
  `uploader` int(11) NOT NULL,
  `datum` date DEFAULT NULL,
  `titel` varchar(45) DEFAULT NULL,
  `ending` varchar(255) DEFAULT NULL,
  `online` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TRUNCATE Tabelle vor dem Einfügen `upload`
--

TRUNCATE TABLE `upload`;
--
-- Daten für Tabelle `upload`
--

INSERT DELAYED INTO `upload` (`idupload`, `uploader`, `datum`, `titel`, `ending`, `online`) VALUES
(1, 1, '2018-01-24', 'Assassins Creed', 'jpg', 1),
(2, 1, '2018-01-24', 'If you ever feel bad...', 'jpg', 1),
(3, 1, '2018-01-24', 'Tool Expectations', 'jpg', 1),
(4, 1, '2018-01-24', 'Epic', 'jpg', 0),
(6, 1, '2018-01-24', 'What a scenery', 'jpg', 1),
(7, 1, '2018-01-24', 'My Cancer is cured', 'gif', 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user`
--

CREATE TABLE `user` (
  `idUser` int(11) NOT NULL,
  `vorname` varchar(45) NOT NULL,
  `nachname` varchar(45) NOT NULL,
  `username` varchar(45) NOT NULL,
  `age` int(11) NOT NULL,
  `password` varchar(100) NOT NULL,
  `permission` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TRUNCATE Tabelle vor dem Einfügen `user`
--

TRUNCATE TABLE `user`;
--
-- Daten für Tabelle `user`
--

INSERT DELAYED INTO `user` (`idUser`, `vorname`, `nachname`, `username`, `age`, `password`, `permission`) VALUES
(1, 'Tim', 'Süllner', 'TimBang', 18, '1234', 10),
(2, 'Jan', 'Fischer', 'k3v1n', 19, '1234', 1),
(3, 'Tim', 'Tim', 'Tim', 111, 'Tim', 1),
(4, 'Bay', 'Max', 'BayMax', 22, '1234', 1),
(5, 'Jens', 'SÃ¼llner', 'Jens09', 49, 'geheim', 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `wer_kommentiert`
--

CREATE TABLE `wer_kommentiert` (
  `upload` int(11) NOT NULL,
  `kommentare` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TRUNCATE Tabelle vor dem Einfügen `wer_kommentiert`
--

TRUNCATE TABLE `wer_kommentiert`;
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
-- Indizes für die Tabelle `wer_kommentiert`
--
ALTER TABLE `wer_kommentiert`
  ADD PRIMARY KEY (`upload`,`kommentare`),
  ADD KEY `fk_upload_has_kommentare_kommentare1_idx` (`kommentare`),
  ADD KEY `fk_upload_has_kommentare_upload1_idx` (`upload`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `kommentare`
--
ALTER TABLE `kommentare`
  MODIFY `idkommentare` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `upload`
--
ALTER TABLE `upload`
  MODIFY `idupload` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT für Tabelle `user`
--
ALTER TABLE `user`
  MODIFY `idUser` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
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

--
-- Constraints der Tabelle `wer_kommentiert`
--
ALTER TABLE `wer_kommentiert`
  ADD CONSTRAINT `fk_upload_has_kommentare_kommentare1` FOREIGN KEY (`kommentare`) REFERENCES `kommentare` (`idkommentare`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_upload_has_kommentare_upload1` FOREIGN KEY (`upload`) REFERENCES `upload` (`idupload`) ON DELETE NO ACTION ON UPDATE NO ACTION;


--
-- Metadaten
--
USE `phpmyadmin`;

--
-- Metadaten für Tabelle dislikes
--

--
-- Metadaten für Tabelle kommentare
--

--
-- Metadaten für Tabelle likes
--

--
-- Metadaten für Tabelle upload
--

--
-- Metadaten für Tabelle user
--

--
-- Metadaten für Tabelle wer_kommentiert
--

--
-- Metadaten für Datenbank phpmyadmin
--

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
