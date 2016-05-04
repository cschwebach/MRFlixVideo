  USE master
GO

IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'MrFlixVideoRentals') 
BEGIN
	DROP DATABASE MrFlixVideoRentals
END
GO

CREATE DATABASE MrFlixVideoRentals
GO

USE [MrFlixVideoRentals]
GO

CREATE TABLE [dbo].[Employees](
	[EmployeeID]			[int] IDENTITY(100000, 1)		NOT NULL,
	[UserName]				[varchar](25)					NOT NULL,
	[Password]				[nvarchar](256)					NOT NULL		DEFAULT 'Movies',
	[FirstName]				[varchar](50)					NOT NULL,
	[LastName]				[varchar](75)					NOT NULL,
	[Email]					[varchar](50)					NOT NULL,
	[Active]				[bit] DEFAULT 1					NOT NULL
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_UserName] UNIQUE ([UserName] ASC)
)
GO

CREATE TABLE [dbo].[Customers](
	[CustomerID]			[char](10)						NOT NULL,
	[FirstName]				[varchar](50)					NOT NULL,
	[LastName]				[varchar](75)					NOT NULL,
	[CreatedEmployeeID]		[int]							NOT NULL,
	[Active]				[bit]DEFAULT 1					NOT NULL
	CONSTRAINT [pk_CustomerID] PRIMARY KEY([CustomerID] ASC)
)
GO

CREATE TABLE [dbo].[Genres](
	[GenreType]				[varchar](20)					NOT NULL,
	[GenreDescription]		[varchar](80)					NOT NULL
	CONSTRAINT [pk_GenreType] PRIMARY KEY([GenreType] ASC)
)
GO

CREATE TABLE [dbo].[Movies](
	[MovieTitle]			[varchar](75)					NOT NULL,
	[GenreType]				[varchar](20)					NOT NULL,
	[CreatedEmployeeID]		[int]							NOT NULL,
	[Director]				[varchar](75)					NOT NULL,
	[YearReleased]			[char](4)						NOT NULL,
	[Active]				[bit]DEFAULT 1					NOT NULL
	CONSTRAINT [pk_MovieTitle] PRIMARY KEY([MovieTitle] ASC)
)
GO

CREATE TABLE [dbo].[CheckOutRecords](
	[OrderID]				[int] IDENTITY(100000, 1)		NOT NULL,
	[CheckOutEmployeeID]	[int] 							NOT NULL,
	[MovieTitle]			[varchar](75)					NOT NULL,
	[CustomerID]			[char](10)						NOT NULL,
	[DateCheckedOut]		[datetime]						NOT NULL,
	[CheckInEmployeeID]		[int]DEFAULT NULL				NULL,
	[DateCheckedIn]			[datetime]DEFAULT NULL			NULL,
	[Active]				[bit]DEFAULT 1					NOT NULL
	CONSTRAINT [pk_OrderID] PRIMARY KEY([OrderID] ASC)
)
GO

CREATE TABLE [dbo].[MovieLineItems](
	[MovieTitle]		[varchar](75)					NOT NULL,
	[OrderID]			[int]							NOT NULL,
	[DateCreated]		[datetime]						NOT NULL
	CONSTRAINT [pk_MovieTitleOrderID] PRIMARY KEY([MovieTitle] ASC, [OrderID])
)
GO

CREATE TABLE [dbo].[ReviewRecords](
	[ReviewID]			[int] IDENTITY(100000, 1)		NOT NULL,
	[MovieTitle]		[varchar](75)					NOT NULL,
	[MovieReview]		[varchar](100)					NOT NULL
	CONSTRAINT [pk_ReviewRecords_ReviewID] PRIMARY KEY([ReviewID] ASC)
)
GO

/***** Relationships *****/
-- Foreign keys for [Customers]
ALTER TABLE [dbo].[Customers] WITH NOCHECK
	ADD CONSTRAINT [fk_Customers_CreatedEmployeeID] FOREIGN KEY([CreatedEmployeeID])
		REFERENCES [dbo].[Employees] ([EmployeeID])
		GO

-- Foreign keys for [Movies]
ALTER TABLE [dbo].[Movies] WITH NOCHECK
	ADD CONSTRAINT [fk_Movies_GenreType] FOREIGN KEY([GenreType])
		REFERENCES [dbo].[Genres] ([GenreType])
		GO
ALTER TABLE [dbo].[Movies] WITH NOCHECK
	ADD CONSTRAINT [fk_Movies_CreatedEmployeeID] FOREIGN KEY([CreatedEmployeeID])
		REFERENCES [dbo].[Employees] ([EmployeeID])
		GO

-- Foreign keys for [MovieLineItems]
ALTER TABLE [dbo].[MovieLineItems] WITH NOCHECK
	ADD CONSTRAINT [fk_MovieRentalLogs_MovieTitle] FOREIGN KEY([MovieTitle])
		REFERENCES [dbo].[Movies] ([MovieTitle])
		GO
ALTER TABLE [dbo].[MovieLineItems] WITH NOCHECK
	ADD CONSTRAINT [fk_MovieRentalLogs_OrderID] FOREIGN KEY([OrderID])
		REFERENCES [dbo].[CheckOutRecords] ([OrderID])
		GO

-- Foreign keys for [CheckOutRecords]
ALTER TABLE [dbo].[CheckOutRecords] WITH NOCHECK
	ADD CONSTRAINT [fk_CheckOutRecords_CheckOutEmployeeID] FOREIGN KEY([CheckOutEmployeeID])
		REFERENCES [dbo].[Employees] ([EmployeeID])
		GO
ALTER TABLE [dbo].[CheckOutRecords] WITH NOCHECK
	ADD CONSTRAINT [fk_CheckOutRecords_CheckInEmployeeID] FOREIGN KEY([CheckInEmployeeID])
		REFERENCES [dbo].[Employees] ([EmployeeID])
		GO
ALTER TABLE [dbo].[CheckOutRecords] WITH NOCHECK
	ADD CONSTRAINT [fk_CheckOutRecords_CustomerID] FOREIGN KEY([CustomerID])
		REFERENCES [dbo].[Customers] ([CustomerID])
		GO
ALTER TABLE [dbo].[CheckOutRecords] WITH NOCHECK
	ADD CONSTRAINT [fk_CheckOutRecords_MovieTitle] FOREIGN KEY([MovieTitle])
		REFERENCES [dbo].[Movies] ([MovieTitle])
		GO

-- Foreign keys for [ReviewRecords]
ALTER TABLE [dbo].[ReviewRecords] WITH NOCHECK
	ADD CONSTRAINT [fk_ReviewRecords_MovieTitle] FOREIGN KEY([MovieTitle])
		REFERENCES [dbo].[Movies] ([MovieTitle])
		GO


/***** Indexes *****/
-- Index for [Employees]
CREATE INDEX Employees_LastName
ON Employees(LastName)

-- Index for [Customers]
CREATE INDEX Customers_LastName
ON Customers(LastName)


GO
/***** Sample Data *****/
--Employees Data--
INSERT INTO [dbo].[Employees] ([UserName], [Password], [FirstName], [LastName], [Email], [Active])
VALUES('cschwebach', 'P@ssword1', 'Chris', 'Schwebach', 'schwebach@yahoo.com', 1);

--Customers Data--
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('6416917303', 'Brittney', 'Baccam', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('6752134545', 'Logan', 'Murphy', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('2134327865', 'Sophie', 'Garth', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('8185439809', 'Tye', 'Rutger', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('3334458003', 'Sampson', 'Soyer', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('5156783213', 'Alex', 'Hannam', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('5154345555', 'Brad', 'Barber', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('5153333211', 'Diane', 'Soyer', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('5156784466', 'Beth', 'Wright', 100000, 1);
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [CreatedEmployeeID], [Active])
VALUES('3196783333', 'Dave', 'Knight', 100000, 1);

--Genres Data--
INSERT INTO [dbo].[Genres] ([GenreType], [GenreDescription])
VALUES('Horror', 'Scarey flick with blood and guts, death is upon you');
INSERT INTO [dbo].[Genres] ([GenreType], [GenreDescription])
VALUES('Comedy', 'Will make you happy and laugh');
INSERT INTO [dbo].[Genres] ([GenreType], [GenreDescription])
VALUES('Crime & Gangster', 'Usually about the law, mafia, and cops');
INSERT INTO [dbo].[Genres] ([GenreType], [GenreDescription])
VALUES('Love Story', 'Chick flicks...love and romance scenes');
INSERT INTO [dbo].[Genres] ([GenreType], [GenreDescription])
VALUES('Sci-Fi', 'Science fiction, aliens, space, super hero flicks');
INSERT INTO [dbo].[Genres] ([GenreType], [GenreDescription])
VALUES('Drama', 'Scarey flick with blood and guts, death is upon you');
INSERT INTO [dbo].[Genres] ([GenreType], [GenreDescription])
VALUES('Western', 'Cowboys and Indians era of flicks');
INSERT INTO [dbo].[Genres] ([GenreType], [GenreDescription])
VALUES('Action', 'Up beat, heart pumping, high energy scenes');

--Movies Data--
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES( 'Silence of The Lambs', 'Horror', 100000, 'Jonathan Demme', '1991', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Man of Steel', 'Sci-Fi', 100000,  'Zack Snyder', '2013', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('The Fighter', 'Drama', 100000, 'David O. Russle ', '2010', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Sherlock Holmes', 'Action', 100000, 'Guy Ritchie', '2009', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Jay and Silent Bob Strike Back', 'Comedy', 100000, 'Kevin Smith', '2001', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Menance 2 Society', 'Drama', 100000, 'Huges Brothers', '1993', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Halloween', 'Horror', 100000, 'Rob Zombie', '1978', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('The One', 'Sci-Fi', 100000, 'James Wong', '2001', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Heat', 'Crime & Gangster', 100000, 'Micheal Man', '1995', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Goodfellas', 'Crime & Gangster', 100000, 'Martin Scorsese', '1990', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Mad Max', 'Action', 100000, 'George Miller', '1980', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Notebook', 'Love Story', 100000, 'Nicholas Sparks', '2004', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('TMNT', 'Sci-Fi', 100000, 'Micheal Bay', '2014', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('American Psyco', 'Horror', 100000, 'Mary Harron', '2000', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Batman Begins', 'Action', 100000, 'Christopher Nolan', '2005', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Erin Brockovich', 'Drama', 100000, 'Steven Soderberg', '2000', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Shooter', 'Drama', 100000, 'Antoine Fuqea', '2007', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('50 First Dates', 'Comedy', 100000, 'Peter Segal', '2004', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('The Dark knight', 'Action', 100000, 'Christoopher Nolan', '2008', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Bamboozaled', 'Comedy', 100000, 'Spike Lee', '2000', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('The Godfather', 'Crime & Gangster', 100000, 'Fancis Ford Coppola', '1972', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('The Godfather 2', 'Crime & Gangster', 100000, 'Fancis Ford Coppola', '1974', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Final Destination', 'Sci-Fi', 100000, 'James wong', '2000', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('The Departed', 'Crime & Gangster', 100000, 'Martin Scorsese', '2006', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Despicable Me', 'Comedy', 100000, 'Pierre Colfin', '2010', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Red', 'Action', 100000, 'Robert Schwertke', '2010', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Fast Five', 'Action', 100000, 'Justin Lin', '2011', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('The Ladies Man', 'Comedy', 100000, 'Reginald Hudlin', '2006', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('The Dark Knight Rises', 'Action', 100000, 'Christopher Nolan', '2012', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Django Unchained', 'Western', 100000, 'Quinten Tarantino', '2012', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('End of Watch', 'Crime & Gangster', 100000, 'David Ager', '2012', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Hancock', 'Sci-Fi', 100000, 'Peter Berg', '2008', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Evil Dead 2', 'Sci-Fi', 100000, 'Sam Rimi', '1982', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Star Wars', 'Sci-Fi', 100000, 'George Lucas', '1977', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Star Wars Episode 1', 'Sci-Fi', 100000, 'George Lucas', '1999', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Toy Story', 'Comedy', 100000, 'John Lasseter', '1995', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('300', 'Action', 100000, 'Zach Snyder', '2006', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Avatar', 'Sci-Fi', 100000, 'James Cameron', '2009', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('How To Train A Dragon', 'Comedy', 100000, 'Chris Sanders', '2010', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Thor', 'Sci-Fi', 100000, 'Kenneth Branagh ', '2011', 1);
INSERT INTO [dbo].[Movies] ([MovieTitle], [GenreType], [CreatedEmployeeID], [Director], [YearReleased], [Active])
VALUES('Ted', 'Comedy', 100000, 'Seth MacFarland', '2012', 1);

--CheckOutRecords Data--
INSERT INTO [dbo].[CheckOutRecords] ([CheckOutEmployeeID], [MovieTitle], [CustomerID], [DateCheckedOut], [DateCheckedIn], [CheckInEmployeeID], [Active])
VALUES(100000, 'TMNT', '8185439809', '2015-04-12 16:59:00.000', '2015-04-13 16:00:00.000', 100000,  1);
INSERT INTO [dbo].[CheckOutRecords] ([CheckOutEmployeeID], [MovieTitle], [CustomerID], [DateCheckedOut], [DateCheckedIn], [CheckInEmployeeID], [Active])
VALUES(100000, 'The Fighter', '5156783213', '2015-04-13 08:22:00.000', '2015-04-15 18:11:00.000', 100000, 1);
INSERT INTO [dbo].[CheckOutRecords] ([CheckOutEmployeeID], [MovieTitle], [CustomerID], [DateCheckedOut], [DateCheckedIn], [CheckInEmployeeID], [Active])
VALUES(100000, 'Halloween', '3196783333', '2015-04-13 08:35:00.000', '2015-04-14 09:15:00.000', 100000, 1);
INSERT INTO [dbo].[CheckOutRecords] ([CheckOutEmployeeID], [MovieTitle], [CustomerID], [DateCheckedOut], [DateCheckedIn], [CheckInEmployeeID], [Active])
VALUES(100000, 'Notebook', '5156783213', '2015-04-14 16:22:00.000', '2015-04-16 17:12:00.000', 100000, 1);
INSERT INTO [dbo].[CheckOutRecords] ([CheckOutEmployeeID], [MovieTitle], [CustomerID], [DateCheckedOut], [DateCheckedIn], [CheckInEmployeeID], [Active])
VALUES(100000, 'Jay and Silent Bob Strike Back', '3196783333', '2015-04-14 16:55:00.000', '2015-04-15 10:55:00.000', 100000, 1);
INSERT INTO [dbo].[CheckOutRecords] ([CheckOutEmployeeID], [MovieTitle], [CustomerID], [DateCheckedOut], [DateCheckedIn], [CheckInEmployeeID], [Active])
VALUES(100000, 'Heat', '8185439809', '2015-04-15 10:49:00.000', '2015-04-16 16:22:00.000', 100000, 1);



--ReviewRecords Data--
INSERT INTO [dbo].[ReviewRecords] ([MovieTitle], [MovieReview])
VALUES('Ted', 'Funny comedy! 4/5 stars!! ');
INSERT INTO [dbo].[ReviewRecords] ([MovieTitle], [MovieReview])
VALUES('Silence of the Lambs', 'Scarey flick....dont watch alone!');
INSERT INTO [dbo].[ReviewRecords] ([MovieTitle], [MovieReview])
VALUES('Notebook', 'Great love story and chick flick');
INSERT INTO [dbo].[ReviewRecords] ([MovieTitle], [MovieReview])
VALUES('Heat', 'Classic cops and robbers movie.');
INSERT INTO [dbo].[ReviewRecords] ([MovieTitle], [MovieReview])
VALUES('Avatar', 'Sweet visual effects!');
INSERT INTO [dbo].[ReviewRecords] ([MovieTitle], [MovieReview])
VALUES('Ted', 'Terrible movie! NOT FOR KIDS!');
INSERT INTO [dbo].[ReviewRecords] ([MovieTitle], [MovieReview])
VALUES('Jay and Silent Bob Strike Back', 'Great Movie! Had mew laughing from begining to end!');
INSERT INTO [dbo].[ReviewRecords] ([MovieTitle], [MovieReview])
VALUES('TMNT', 'Action packed! Still a bigger fan of the original after seeing this but overall a good movie! ');


--MovieLineItems Data--
INSERT INTO [dbo].[MovieLineItems] ([OrderID], [MovieTitle], [DateCreated])
VALUES(100000, 'TMNT', '2015-10-02 10:31:00.000' );
INSERT INTO [dbo].[MovieLineItems] ([OrderID], [MovieTitle], [DateCreated])
VALUES(100001, 'The Fighter', '2015-10-02 10:31:00.000' );
INSERT INTO [dbo].[MovieLineItems] ([OrderID], [MovieTitle], [DateCreated])
VALUES(100002, 'Halloween', '2015-10-02 10:31:00.000' );
INSERT INTO [dbo].[MovieLineItems] ([OrderID], [MovieTitle], [DateCreated])
VALUES(100003, 'Notebook', '2015-10-02 10:31:00.000' );
INSERT INTO [dbo].[MovieLineItems] ([OrderID], [MovieTitle], [DateCreated])
VALUES(100004, 'Jay and Silent Bob Strike Back', '2015-10-02 10:31:00.000' );
INSERT INTO [dbo].[MovieLineItems] ([OrderID], [MovieTitle], [DateCreated])
VALUES(100005, 'Heat', '2015-10-02 10:31:00.000' );
GO

create procedure spInsertEmployees (
	@UserName varchar(25),
	@Password varchar(150),
	@FirstName varchar(50),
	@LastName varchar(75),
	@Email varchar(100)
	
	)
AS
BEGIN

IF ((SELECT COUNT(*) FROM Employees AU WHERE LOWER(AU.UserName) = LOWER(@UserName)) > 0)
	BEGIN
		RETURN 2		
	END
ELSE
	BEGIN
		INSERT INTO Employees(
			UserName,
			Password,
			FirstName,
			LastName,
			Email
			
			)
		VALUES(
			@UserName,
			@Password,
			@FirstName,
			@LastName,
			@Email
			
			);

			RETURN @@ROWCOUNT;
	END;
END;
go

CREATE PROCEDURE spSelectEmployeeWithUsernameAndPassword (
    @UserName VARCHAR(25),
    @Password VARCHAR(150)
)
AS
BEGIN
    SELECT COUNT(UserName)
    FROM Employees
    WHERE UserName = @UserName
    AND Password = @Password
    AND Active = 1;
END;
GO

CREATE PROCEDURE spUpdateEmployee (
@EmployeeID int,
@UserName varchar(25),
@PassWord varchar(150),
@FirstName varchar(50),
@LastName varchar(75),
@Email varchar(50),
@Active bit
)
AS
BEGIN
        UPDATE  Employee
		     SET     UserName  = @UserName,
					 PassWord  = @PassWord, 
					 FirstName = @FirstName,
					 LastName  = @LastName, 
				     Email = @Email, 
					 Active    = @Active					 
			WHERE    
					 EmployeeID =  @EmployeeID 

	return @@ROWCOUNT;     
END;
go

CREATE PROCEDURE spSelectEmployeeByUserName (
    @UserName VARCHAR(25)
)
AS
BEGIN
	SELECT EmployeeID, UserName, FirstName, LastName, Email, Active
    FROM Employees
    WHERE UserName = @UserName
END
GO

CREATE PROCEDURE spUpdatePassword (
    @UserName VARCHAR(25),
    @oldPassword VARCHAR(150),
    @newPassword VARCHAR(150)
)
AS
BEGIN
    UPDATE Employees
    SET password = @newPassword
    WHERE UserName = @UserName
    AND password = @oldPassword
    AND active = 1
    RETURN @@rowcount;
END;
GO

/**Stored Procedures*/

print '' print '*** Creating sp_Update_Employee_LocalPhone'
GO
CREATE PROCEDURE sp_Update_Employee_Email
@EmployeeID int,
@Email varchar(50)
AS
Begin
UPDATE Employees
SET Email = @Email
WHERE EmployeeID = @EmployeeID
return @@rowcount
END
GO

print '' print '*** Creating sp_CheckInMovie'
GO
CREATE PROCEDURE sp_CheckInMovie
@MovieTitle varchar(75),
@CheckInEmployeeID int,
@DateCheckedIn [datetime]
AS
Begin
UPDATE CheckOutRecords
SET CheckInEmployeeID = @CheckInEmployeeID,
	DateCheckedIn = @DateCheckedIn
WHERE MovieTitle = @MovieTitle
return @@rowcount
END
GO




print '' print '*** Creating sp_validate_active_username_and_password'
GO
CREATE PROCEDURE sp_Validate_Active_Username_And_Password (
	@UserName varchar(25),
	@Password nvarchar(256)
	)
	AS
	BEGIN
		SELECT COUNT(UserName)
		FROM Employees
		WHERE
			UserName = @UserName
		AND Password = @Password
		AND Active = 1
	END
GO

print '' print '*** Creating sp_update_password_for_username'
GO
CREATE PROCEDURE sp_Update_Password_For_Username (
	@username varchar(20),
	@oldPassword nvarchar(256),
	@newPassword nvarchar(256)
	)
	AS
	BEGIN
		UPDATE Employees
			SET Password = @newPassword
		WHERE
			UserName = @username
		AND Password = @oldPassword
		AND Active = 1
		RETURN @@rowcount
	END
GO

print '' print '*** Creating sp_Select_CheckedOutRecords'
GO
CREATE PROCEDURE sp_Select_CheckedOutRecords 
AS
BEGIN
	SELECT OrderID, MovieTitle, CustomerID, CheckOutEmployeeID, DateCheckedOut
	FROM [dbo].[CheckOutRecords]
	WHERE Active= 1 AND CheckInEmployeeID=null AND DateCheckedIn=null
END
GO

print '' print '*** MVC sp'
go
create procedure spSelectUserNameCount(
	@UserName		varchar(25)
)
as begin
	select count(*) as Match
	from Employees as u
	where u.UserName = @UserName;
end
go

create procedure spSelectUserInformationCount(
	@UserName		varchar(25),
	@Email	varchar(50),
	@PassWord		varchar(150)
)
as begin
	select count(*) as Verification
	from Users as u
	where u.Active = 1 and
		u.UserName = @UserName and 
		u.PassWord = @PassWord and
		u.Email = @Email;
end
go


