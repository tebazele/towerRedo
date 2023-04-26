-- SQLBook: Code

-- Active: 1679678448423@@54.187.169.182@3306@classroom_demos

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

DROP TABLE accounts;

CREATE TABLE
    jaEvents(
        id INT NOT NULL AUTO_INCREMENT primary key,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(10000),
        coverImg VARCHAR(1000) NOT NULL,
        location VARCHAR(255),
        capacity INT,
        startDate DATETIME NOT NULL,
        isCanceled BOOLEAN NOT NULL DEFAULT false,
        type VARCHAR(255),
        Foreign Key (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8;

DROP TABLE jaEvents;

CREATE TABLE
    IF NOT EXISTS jaTickets(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        eventId INT NOT NULL,
        accountId VARCHAR(255) NOT NULL,
        FOREIGN KEY (accountId) REFERENCES accounts (id) ON DELETE CASCADE,
        FOREIGN KEY (eventId) REFERENCES jaEvents (id) ON DELETE CASCADE
    ) default charset utf8;

DROP TABLE jaTickets;

CREATE TABLE
    IF NOT EXISTS jaComments (
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        eventId INT NOT NULL,
        body VARCHAR(999) NOT NULL,
        isAttending BOOLEAN,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
        FOREIGN KEY (eventId) REFERENCES jaEvents (id) ON DELETE CASCADE
    ) default charset utf8;

DROP TABLE jaComments;