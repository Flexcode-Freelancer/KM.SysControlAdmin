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
	INSERT INTO [Role] VALUES('Administrador');
	INSERT INTO [Role] VALUES('Instructor/Docente');
    INSERT INTO [Role] VALUES('Alumno');
GO
    INSERT INTO [User] (IdRole, [Name], LastName, Email, [Password], [Status], DateCreated, DateModification) 
    VALUES (1, 'Flexcode', 'Freelancer', 'DesAdmin@kerigmamusic.com', 'c8aa131427a72781b156ac723ddb917f', 1, SYSDATETIME(), SYSDATETIME());
GO
CREATE TABLE Trainer(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Code VARCHAR(20) NOT NULL,
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
  CREATE TABLE Schedule(
  Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  StartTime TIME NOT NULL,
  EndTime TIME NOT NULL
  );
GO
  CREATE TABLE Course (
  Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  Code VARCHAR(15) NOT NULL,
  [Name] VARCHAR(100) NOT NULL,
  Price MONEY NOT NULL,
  StartTime DATE NOT NULL,
  EndTime DATE NOT NULL,
  MaxStudent INT NULL,
  [Status] TINYINT NOT NULL,
  DateCreated DATETIME NOT NULL,
  DateModification DATETIME NOT NULL,
  IdSchedule INT NOT NULL FOREIGN KEY REFERENCES Schedule(Id),
  IdTrainer INT NOT NULL FOREIGN KEY REFERENCES Trainer(Id)
  );
GO
  CREATE TABLE Student (
  Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  StudentCode VARCHAR(8) NOT NULL,
  ProjectCode VARCHAR(6) NULL,
  ParticipantCode VARCHAR(11) NULL,
  [Name] VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  DateOfBirth DATE NOT NULL,
  Age VARCHAR(3) NOT NULL,
  Church VARCHAR(60) NULL,
  [Status] TINYINT NOT NULL,
  ImageData VARBINARY(MAX) NOT NULL,
  DateCreated DATETIME NOT NULL,
  DateModification DATETIME NOT NULL,
  );
GO
  CREATE TABLE CourseAssignment(
  Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  IdStudent INT NOT NULL FOREIGN KEY REFERENCES Student(Id),
  IdCourse INT NOT NULL FOREIGN KEY REFERENCES Course(Id),
  DateCreated DATETIME NOT NULL,
  DateModification DATETIME NOT NULL,
);
GO