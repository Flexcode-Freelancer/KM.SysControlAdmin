CREATE DATABASE KMSysControlAdminDB
GO
    USE KMSysControlAdminDB
GO
CREATE TABLE [Role](
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name] VARCHAR(30) NOT NULL
    );
GO
CREATE TABLE [User](
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name] VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    [Password] VARCHAR(100) NOT NULL,
    [Status] TINYINT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateModification DATETIME NOT NULL,
	ImageData VARBINARY(MAX) NULL,
	IdRole INT NOT NULL FOREIGN KEY REFERENCES [Role](Id)
    );
GO
	INSERT INTO [Role] VALUES('Desarrollador');
GO
    INSERT INTO [User] (IdRole, [Name], LastName, Email, [Password], [Status], DateCreated, DateModification) 
    VALUES (1, 'Flexcode', 'Freelancer', 'DesAdmin@kerigmamusic.com', 'c8aa131427a72781b156ac723ddb917f', 1, SYSDATETIME(), SYSDATETIME());
GO
CREATE TABLE Trainer(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name] VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Dui VARCHAR(10) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Age VARCHAR(3) NOT NULL,
    Gender VARCHAR(20) NOT NULL,
    CivilStatus VARCHAR(20) NOT NULL,
    Phone VARCHAR(9) NOT NULL,
    [Address] VARCHAR(100) NOT NULL,
	[Status] TINYINT NOT NULL,
    CommentsOrObservations VARCHAR(100) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateModification DATETIME NOT NULL,
    ImageData VARBINARY(MAX) NOT NULL,
    );
GO
    INSERT INTO [Role] VALUES('Administrador', 'Instructor/Docente');
GO