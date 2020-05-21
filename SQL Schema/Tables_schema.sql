--Drop table Tournament
CREATE TABLE Tournament
(
TournamentID int identity(1,1) not null PRIMARY KEY,
TournamentName nvarchar(100) not null,
EntryFee decimal(10,2) not null,
Active bit not null
);

--Drop table Teams
CREATE TABLE Teams
(
TeamID int identity(1,1) not null PRIMARY KEY,
TeamName nvarchar(255) not null
);


--Drop table Prizes
CREATE TABLE Prizes
(
PrizeID int identity(1,1) not null PRIMARY KEY,
PlaceNumber int not null,
PlaceName nvarchar(50) not null,
PrizeAmount money not null,
PrizePercentage float not null
);

--Drop table TournamentEntries
CREATE TABLE TournamentEntries
(
TournamentEntriesID int identity(1,1) not null PRIMARY KEY,
TournamentID int FOREIGN KEY REFERENCES Tournament(TournamentID),
TeamID int FOREIGN KEY REFERENCES Teams(TeamID)
);


--Drop table TournamentPrizes
CREATE TABLE TournamentPrizes
(
TournamentPrizesID int identity(1,1) not null PRIMARY KEY,
TournamentID int FOREIGN KEY REFERENCES Tournament(TournamentID),
PrizeID int FOREIGN KEY REFERENCES Prizes(PrizeID),
);

--Drop table People
CREATE TABLE People
(
PeopleID int identity(1,1) not null PRIMARY KEY,
FirstName nvarchar(50) not null,
LastName nvarchar(50) not null,
EmailAddress nvarchar(255) not null,
CellPhoneNumber nvarchar(50) not null
);

--Drop table TeamMembers
CREATE TABLE TeamMembers
(
TeamMembersID int identity(1,1) not null PRIMARY KEY,
TeamID int FOREIGN KEY REFERENCES Teams(TeamID),
PeopleID int FOREIGN KEY REFERENCES People(PeopleID)
);

--Drop table Matchups
CREATE TABLE Matchups
(
MatchupID int identity(1,1) not null PRIMARY KEY,
TeamID int FOREIGN KEY REFERENCES Teams(TeamID),
MatchupRound int not null
);

--Drop table MatchupEntries
CREATE TABLE MatchupEntries
(
MatchupEntriesID int identity(1,1) not null PRIMARY KEY,
MatchupID int FOREIGN KEY REFERENCES Matchups(MatchupID) not null,
ParentMatchupID int FOREIGN KEY REFERENCES Matchups(MatchupID) null,
TeamID int FOREIGN KEY REFERENCES Teams(TeamID) null,
Score int null
);