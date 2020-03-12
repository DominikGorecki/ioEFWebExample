CREATE DATABASE QuizesExample;

USE QuizesExample;

CREATE TABLE Quizes
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Title VARCHAR(100) NOT NULL
);

CREATE TABLE Questions
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	QuestionOrder INT NOT NULL,
	Question VARCHAR(100) NOT NULL,

	-- One to many relationship with Quizes
	QuizId INT FOREIGN KEY REFERENCES Quizes(Id) NOT NULL
);

CREATE TABLE Answers
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Answer VARCHAR(255) NOT NULL,
	IsCorrect BIT NOT NULL,

	-- One to many relationship with Questions
	QuestionId INT FOREIGN KEY REFERENCES Questions(Id) NOT NULL
);

CREATE TABLE Tag
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100) NOT NULL
);

-- Many to many relationship with Quizes
CREATE TABLE Quizes_Tags
(
	QuizId INT NOT NULL FOREIGN KEY REFERENCES Quizes(Id),
	TagId INT NOT NULL FOREIGN KEY REFERENCES Tags(Id),
	PRIMARY KEY (QuizId, TagId)
);