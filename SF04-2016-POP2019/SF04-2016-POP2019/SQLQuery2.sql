/*insert into dbo.Faculty (Name,Address,Active)
values('Elektronski fakultet','Aleksandra Medvedeva 14, Nis', 1);
*/
--insert into dbo.Classroom (Name,Seats,TypeOfClassroom,Active,Faculty_Id)
--values('TC12', 12, 'IT', 1, 3);
--insert into dbo.Classroom (Name,Seats,TypeOfClassroom,Active,Faculty_Id)
--values('LLL', 10, 'LABORATORY', 1, 3);
--insert into dbo.Classroom (Name,Seats,TypeOfClassroom,Active,Faculty_Id)
--values('A2', 125, 'AMPHITHEATER', 1, 3);

--alter table Classroom
--alter column Seats int not null;

--CREATE TABLE [dbo].[Zaposlen]
--(
--	[User_Id] INT NOT NULL primary key,
--	[Faculty_Id] INT NOT NULL,
--	constraint usersId
--		foreign key (User_Id) references dbo.Users(Id),
--	constraint facultyId
--		foreign key (Faculty_Id) references dbo.Faculty(Id)
--)

--insert into dbo.Zaposlen (User_Id, Faculty_Id)
--values(1,2);

--insert into dbo.Zaposlen (User_Id, Faculty_Id)
--values(5,2);

--insert into dbo.Zaposlen (User_Id, Faculty_Id)
--values(2,1);

--insert into dbo.Zaposlen (User_Id, Faculty_Id)
--values(6,1);

insert into dbo.Termin(Od, Do, Dan, TipNastave, Active, Classroom_Id, User_Id)
values ('12:00', '14:00', 'PONEDELJAK', 'VEZBE', 1, 4, 1);
insert into dbo.Termin(Od, Do, Dan, TipNastave, Active, Classroom_Id, User_Id)
values ('15:00', '18:30', 'SREDA', 'PREDAVANJA', 1, 6, 5);
insert into dbo.Termin(Od, Do, Dan, TipNastave, Active, Classroom_Id, User_Id)
values ('08:00', '10:00', 'PETAK', 'VEZBE', 1, 1, 2);
insert into dbo.Termin(Od, Do, Dan, TipNastave, Active, Classroom_Id, User_Id)
values ('10:00', '11:30', 'UTORAK', 'LABORATORIJA', 1, 2, 6);