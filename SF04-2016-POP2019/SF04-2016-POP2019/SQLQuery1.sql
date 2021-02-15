CREATE TABLE [dbo].[Termin]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Time] VARCHAR(15) NOT NULL,
	[DayOfWeek] VARCHAR(15) NOT NULL,
	[Type] VARCHAR(15) NOT NULL,
	[Active] BIT NOT NULL,
	[Classroom_Id] INT NOT NULL,
	[User_Id] INT NOT NULL,
	constraint fk_classroom
		foreign key (Classroom_Id) references dbo.Classroom(Id),
	constraint fk_user
		foreign key (User_Id) references dbo.Users(Id)
)


