CREATE TABLE [Contact] (
    ContactId int NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    ContactType int NOT NULL,
    EmployeeId nvarchar(50) NULL,
    FirstName nvarchar(150) NULL,
    LastName nvarchar(150) NULL,
    Email nvarchar(150) NULL,
    DateOfHire DateTime NULL,
    CurrentlyEmployed bit NULL,
    DateOfBirth DateTime,
    ParentId int NULL
)


CREATE TABLE [Address] (
    AddressId int NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    ContactId int NOT NULL,
    [Label] nvarchar(150) NOT NULL,
    StreetLine1 nvarchar(150) NOT NULL,
    StreetLine2 nvarchar(150) NULL,
    City nvarchar(75) NOT NULL,
    [State] nvarchar(75) NOT NULL,
    PostalCode nvarchar(20) NOT NULL
)


CREATE TABLE [PhoneNumber] (
    PhoneNumberId int NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    ContactId int NOT NULL,
    [Label] nvarchar(150) NOT NULL,
    [Value] nvarchar(50) NOT NULL
)

ALTER TABLE [Address] 
ADD CONSTRAINT FK_ContactAddress
FOREIGN KEY (ContactId) REFERENCES Contact(ContactId);


ALTER TABLE [PhoneNumber] 
ADD CONSTRAINT FK_ContactPhoneNumber
FOREIGN KEY (ContactId) REFERENCES Contact(ContactId);
