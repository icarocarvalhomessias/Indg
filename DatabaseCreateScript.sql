USE ItemTrader;
GO


IF OBJECT_ID('Person', 'U') IS NULL
BEGIN
	CREATE TABLE [Person](
		[Id]		INT PRIMARY KEY IDENTITY(1,1),
		[Name]		VARCHAR(150) NOT NULL UNIQUE,
		[IsActive]	BIT NOT NULL
	);
END

IF OBJECT_ID('Item', 'U') IS NULL
BEGIN
	CREATE TABLE [Item](
		[Id]		INT PRIMARY KEY IDENTITY(1,1),
		[Name]		VARCHAR(100) NOT NULL UNIQUE,
		[IsActive]	BIT NOT NULL,
		[PersonId]	INT NOT NULL REFERENCES [Person]([Id])
	);
END

IF OBJECT_ID('ItemTransfer', 'U') IS NULL

BEGIN
	CREATE TABLE [ItemTransfer](
		[Id]			INT PRIMARY KEY IDENTITY(1,1),
		[FromPersonId]	INT NOT NULL REFERENCES [Person]([Id]),
		[ToPersonId]	INT NOT NULL REFERENCES [Person]([Id]),
		[ItemId]		INT NOT NULL REFERENCES [Item]([Id])
	);
END
