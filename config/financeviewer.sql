-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 05. Nov 2018 um 00:45
-- Server-Version: 10.1.35-MariaDB
-- PHP-Version: 7.2.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `financeviewer`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fv_users`
--

CREATE TABLE `fv_users` (
  `u_name` varchar(150) NOT NULL,
  `u_password` varchar(150) NOT NULL,
  `u_isLocked` tinyint(4) NOT NULL,
  `u_firstName` varchar(45) NOT NULL,
  `u_lastName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `fv_users`
--

INSERT INTO `fv_users` (`u_name`, `u_password`, `u_isLocked`, `u_firstName`, `u_lastName`) VALUES
('muster', '$2y$10$N02JsrHy5YH4owzWO2VEv.8dKUCsvJjiALxwpb6QrdfLmy2R6Wy.q', 0, 'Max', 'Muster'),
('root', '$2y$10$kbWAyw.p80N7VixlmPTkC.DbNlIwuXdhWkktmfEEOEgKgHtPhiq3O', 0, 'Root', 'User');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fv_views`
--

CREATE TABLE `fv_views` (
  `v_id` int(11) NOT NULL,
  `v_y_year` varchar(4) NOT NULL,
  `v_u_name` varchar(150) NOT NULL,
  `v_name` varchar(45) NOT NULL,
  `v_description` varchar(80) NOT NULL,
  `v_html_id` varchar(45) NOT NULL,
  `v_notes` varchar(80) NOT NULL,
  `v_profile_url` varchar(180) NOT NULL,
  `v_month_01` varchar(1000) NOT NULL,
  `v_month_02` varchar(1000) NOT NULL,
  `v_month_03` varchar(1000) NOT NULL,
  `v_month_04` varchar(1000) NOT NULL,
  `v_month_05` varchar(1000) NOT NULL,
  `v_month_06` varchar(1000) NOT NULL,
  `v_month_07` varchar(1000) NOT NULL,
  `v_month_08` varchar(1000) NOT NULL,
  `v_month_09` varchar(1000) NOT NULL,
  `v_month_10` varchar(1000) NOT NULL,
  `v_month_11` varchar(1000) NOT NULL,
  `v_month_12` varchar(1000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `fv_views`
--

INSERT INTO `fv_views` (`v_id`, `v_y_year`, `v_u_name`, `v_name`, `v_description`, `v_html_id`, `v_notes`, `v_profile_url`, `v_month_01`, `v_month_02`, `v_month_03`, `v_month_04`, `v_month_05`, `v_month_06`, `v_month_07`, `v_month_08`, `v_month_09`, `v_month_10`, `v_month_11`, `v_month_12`) VALUES
(2, '2018', 'root', 'FVMarket', 'Supermarket', 'fvmarket', 'This is the FVMarket supermarket. I use it to buy groceries.', 'https://cdn.stocksnap.io/img-thumbs/960w/AWJD4WV6W1.jpg', '1+1+41.69+21.49+43.99+14.29', '13.99+52.39+10.79+21.49+23.49', '0.45+1.39+2.79+0.2+1.99+2.49+0.99+1.49+0.89', '43.54-6+16.55+15.42+3.58+6.86+10.17+17.76+-20+2.47+19.15+4.29+18.3+22.02+7.84+6.98', '23.97+2.75+16.39+10.53+18.99+5.34+4.31+5.85+24.34+4.74+12.93', '89.4', '120.3', '153.58', '194.01', '79.99', '112.31', '126.29'),
(3, '2018', 'root', 'Richard', 'Supermarket', 'richard', 'Richard is like the austrian big supermarket. You can buy practically everything', 'https://cdn.stocksnap.io/img-thumbs/960w/R0CTZDRFPY.jpg', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'),
(5, '2018', 'root', 'Amazing', 'Online E-Commerce', 'amazing', 'Amazing is an online eCommerce. I use it to buy stuff and proteins.', 'https://image.shutterstock.com/image-photo/boxes-trolley-on-laptop-keyboard-450w-580499569.jpg', '123.45', '20+35.81+11.04+39.9', '30.2', '39.1+20+1.30+40.91', '88.20', '60.19', '7.91', '88.29', '90.69', '101.29', '119.14', '124.17'),
(7, '2018', 'root', 'Food', 'Food not included in supermarket', 'food', 'Everything I eat (besides from what I buy at the supermarket) is included in thi', 'https://image.shutterstock.com/image-photo/healthy-salad-bowl-quinoa-tomatoes-450w-521741356.jpg', '49.25', '29.49', '90.19', '4+1+5+3+3.50+4.5+1+1+1+2+1', '5+5+6+7+9', '6+4.5', '7+70', '89.13', '99-1', '100.01+20.29', '111', '122'),
(9, '2018', 'root', 'Car', 'Car costs, insurance, gasoline', 'car', 'A car doesn`t come cheap. Everything is spend for this car is included here.', 'https://image.shutterstock.com/image-photo/creative-background-highbeam-headlight-newest-450w-1191643888.jpg', '90+450', '80+550', '90+350.55', '80+154.23', '90+431.49', '80+350', '90+210', '80+482', '90+211', '80+30', '91+40', '82+430'),
(11, '2018', 'root', 'Invoices', 'Monthly costs, rent, etc.', 'invoices', 'All my invoices, like rent, gas, electricity and else is included in this list.', 'https://image.shutterstock.com/image-vector/e-commerce-order-450w-1148589737.jpg', '750+35+35+25+25', '750+35+35+25+30', '750+35+35+25+18.78', '750+35+35+25+69', '750+35+35+25+169', '750+35+35+25+385', '750+35+35+25+342', '750+35+35+25+295', '750+9+35+35+25+284', '750+10+35+35+25+284', '750+112+35+35+25+10', '712+35+35+25+130'),
(13, '2018', 'root', 'Phone', 'Costs for the phone provider', 'phone', 'A phone doesn`t come cheat. The monthly costs for a phone are included here.', 'https://image.shutterstock.com/image-photo/hand-holding-black-phone-isolated-450w-386618683.jpg', '14.2', '14.2', '14.2', '14.2', '14.2+30', '14.2', '14.2+2.60', '8+14.2', '19', '19', '19', '19'),
(15, '2018', 'root', 'Medicine', 'Medicine bought from the pharmacy', 'medicine', 'Descriptions and everything I use medically is included here.', 'https://cdn.stocksnap.io/img-thumbs/960w/LKM1T38B6S.jpg', '12+4', '2+1.3', '3', '6+3.3+10.95+3+4.05+8.3', '12+5.2+2.2', '1+29+4.2', '15.4', '9.20', '9', '26.9+29.9+6', '12+8.99', '13.99+25.28'),
(32, '2018', 'root', 'FVMarket2', 'Supermarket', 'fvmarket2', 'This is my local market', 'https://cdn.stocksnap.io/img-thumbs/960w/AWJD4WV6W1.jpg', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fv_years`
--

CREATE TABLE `fv_years` (
  `y_year` varchar(4) NOT NULL,
  `y_u_name` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `fv_years`
--

INSERT INTO `fv_years` (`y_year`, `y_u_name`) VALUES
('2017', 'root'),
('2018', 'muster'),
('2018', 'root'),
('2019', 'muster');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `fv_users`
--
ALTER TABLE `fv_users`
  ADD PRIMARY KEY (`u_name`);

--
-- Indizes für die Tabelle `fv_views`
--
ALTER TABLE `fv_views`
  ADD PRIMARY KEY (`v_id`) USING BTREE,
  ADD KEY `v_u_name` (`v_u_name`),
  ADD KEY `v_y_year` (`v_y_year`);

--
-- Indizes für die Tabelle `fv_years`
--
ALTER TABLE `fv_years`
  ADD PRIMARY KEY (`y_year`,`y_u_name`) USING BTREE;

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `fv_views`
--
ALTER TABLE `fv_views`
  MODIFY `v_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `fv_views`
--
ALTER TABLE `fv_views`
  ADD CONSTRAINT `v_u_name` FOREIGN KEY (`v_u_name`) REFERENCES `fv_users` (`u_name`),
  ADD CONSTRAINT `v_y_year` FOREIGN KEY (`v_y_year`) REFERENCES `fv_years` (`y_year`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
