IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [RoleId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
);
GO

CREATE TABLE [Users] (
    [UserId] uniqueidentifier NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Gender] int NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [CitizenId] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [RoleId] uniqueidentifier NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId])
);
GO

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240122041414_InitCreate', N'6.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] DROP CONSTRAINT [FK_Users_Roles_RoleId];
GO

DROP TABLE [Roles];
GO

DROP INDEX [IX_Users_RoleId] ON [Users];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RoleId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] DROP COLUMN [RoleId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240122041842_RemoveRoleTable', N'6.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Password');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [Password] nvarchar(32) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Email');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Users] ALTER COLUMN [Email] nvarchar(50) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'CitizenId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Users] ALTER COLUMN [CitizenId] nchar(12) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240122042942_AddLengthConstraints', N'6.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [FirstName] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Users] ADD [LastName] nvarchar(30) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Users] ADD [Username] nvarchar(30) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240122124055_AddUsernameColumn', N'6.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE UNIQUE INDEX [IX_Users_CitizenId_Email_Username] ON [Users] ([CitizenId], [Email], [Username]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240125033824_EnforceUniquenessConstraints', N'6.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [Discriminator] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Users] ADD [IsVerified] bit NULL;
GO

ALTER TABLE [Users] ADD [PhoneNumber] nvarchar(max) NOT NULL DEFAULT N'';
GO

CREATE TABLE [RealEstateOwners] (
    [RealEstateOwnerId] uniqueidentifier NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    [CitizenId] nchar(10) NOT NULL,
    [ContactInformation] text NOT NULL,
    CONSTRAINT [PK_RealEstateOwners] PRIMARY KEY ([RealEstateOwnerId])
);
GO

CREATE TABLE [RealEstates] (
    [RealEstateId] uniqueidentifier NOT NULL,
    [RealEstateName] nvarchar(max) NOT NULL,
    [Address] nvarchar(100) NOT NULL,
    [Description] text NOT NULL,
    [ImageUrl] text NOT NULL,
    [Status] nvarchar(30) NOT NULL,
    [OwnerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_RealEstates] PRIMARY KEY ([RealEstateId]),
    CONSTRAINT [FK_RealEstates_RealEstateOwners_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [RealEstateOwners] ([RealEstateOwnerId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Auctions] (
    [AuctionId] uniqueidentifier NOT NULL,
    [Title] nvarchar(50) NOT NULL,
    [Description] text NOT NULL,
    [ListingDate] datetime2 NOT NULL,
    [RegistrationPeriodStart] datetime2 NOT NULL,
    [RegistrationPeriodEnd] datetime2 NOT NULL,
    [AuctionPeriodStart] datetime2 NOT NULL,
    [AuctionPeriodEnd] datetime2 NOT NULL,
    [InitialPrice] decimal(18,0) NOT NULL,
    [IncrementalPrice] decimal(18,0) NOT NULL,
    [StartingBid] decimal(18,0) NOT NULL,
    [CurrentBid] decimal(18,0) NOT NULL,
    [WinningBid] decimal(18,0) NOT NULL,
    [Status] nvarchar(30) NOT NULL,
    [StaffId] uniqueidentifier NOT NULL,
    [AdminId] uniqueidentifier NOT NULL,
    [RealEstateId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Auctions] PRIMARY KEY ([AuctionId]),
    CONSTRAINT [FK_Auctions_RealEstates_AuctionId] FOREIGN KEY ([AuctionId]) REFERENCES [RealEstates] ([RealEstateId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Auctions_Users_AuctionId] FOREIGN KEY ([AuctionId]) REFERENCES [Users] ([UserId]),
    CONSTRAINT [FK_Auctions_Users_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [LegalDocuments] (
    [DocumentId] uniqueidentifier NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [DocumentUrl] text NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [DocumentType] nvarchar(30) NOT NULL,
    [RealEstateId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_LegalDocuments] PRIMARY KEY ([DocumentId]),
    CONSTRAINT [FK_LegalDocuments_RealEstates_RealEstateId] FOREIGN KEY ([RealEstateId]) REFERENCES [RealEstates] ([RealEstateId]) ON DELETE CASCADE
);
GO

CREATE TABLE [AuctionRegistrations] (
    [RegistrationId] uniqueidentifier NOT NULL,
    [MemberId] uniqueidentifier NOT NULL,
    [AuctionId] uniqueidentifier NOT NULL,
    [RegistrationDate] datetime2 NOT NULL,
    [RegistrationStatus] nvarchar(30) NOT NULL,
    [DepositAmount] decimal(18,0) NOT NULL,
    CONSTRAINT [PK_AuctionRegistrations] PRIMARY KEY ([RegistrationId]),
    CONSTRAINT [FK_AuctionRegistrations_Auctions_AuctionId] FOREIGN KEY ([AuctionId]) REFERENCES [Auctions] ([AuctionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AuctionRegistrations_Users_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Bids] (
    [BidId] uniqueidentifier NOT NULL,
    [Amount] decimal(18,0) NOT NULL,
    [Date] datetime2 NOT NULL,
    [MemberId] uniqueidentifier NOT NULL,
    [AuctionId] uniqueidentifier NOT NULL,
    [IsWinningBid] bit NOT NULL,
    CONSTRAINT [PK_Bids] PRIMARY KEY ([BidId]),
    CONSTRAINT [FK_Bids_Auctions_AuctionId] FOREIGN KEY ([AuctionId]) REFERENCES [Auctions] ([AuctionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bids_Users_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transactions] (
    [TransactionId] uniqueidentifier NOT NULL,
    [TransactionDate] datetime2 NOT NULL,
    [Amount] decimal(18,0) NOT NULL,
    [Status] nvarchar(30) NOT NULL,
    [PaymentMethod] nvarchar(30) NOT NULL,
    [BidId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([TransactionId]),
    CONSTRAINT [FK_Transactions_Bids_TransactionId] FOREIGN KEY ([TransactionId]) REFERENCES [Bids] ([BidId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AuctionRegistrations_AuctionId] ON [AuctionRegistrations] ([AuctionId]);
GO

CREATE INDEX [IX_AuctionRegistrations_MemberId] ON [AuctionRegistrations] ([MemberId]);
GO

CREATE INDEX [IX_Auctions_StaffId] ON [Auctions] ([StaffId]);
GO

CREATE INDEX [IX_Bids_AuctionId] ON [Bids] ([AuctionId]);
GO

CREATE INDEX [IX_Bids_MemberId] ON [Bids] ([MemberId]);
GO

CREATE INDEX [IX_LegalDocuments_RealEstateId] ON [LegalDocuments] ([RealEstateId]);
GO

CREATE INDEX [IX_RealEstates_OwnerId] ON [RealEstates] ([OwnerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240201032618_AddTonsOfEntities', N'6.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Auctions]') AND [c].[name] = N'CurrentBid');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Auctions] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Auctions] DROP COLUMN [CurrentBid];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Auctions]') AND [c].[name] = N'StartingBid');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Auctions] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Auctions] DROP COLUMN [StartingBid];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Auctions]') AND [c].[name] = N'WinningBid');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Auctions] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Auctions] DROP COLUMN [WinningBid];
GO

DROP INDEX [IX_Users_CitizenId_Email_Username] ON [Users];
DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'CitizenId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Users] ALTER COLUMN [CitizenId] nvarchar(12) NOT NULL;
CREATE UNIQUE INDEX [IX_Users_CitizenId_Email_Username] ON [Users] ([CitizenId], [Email], [Username]);
GO

ALTER TABLE [Auctions] ADD [CurrentBidId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [Auctions] ADD [StartingBidId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [Auctions] ADD [WinningBidId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

CREATE UNIQUE INDEX [IX_Auctions_CurrentBidId] ON [Auctions] ([CurrentBidId]);
GO

CREATE UNIQUE INDEX [IX_Auctions_StartingBidId] ON [Auctions] ([StartingBidId]);
GO

CREATE UNIQUE INDEX [IX_Auctions_WinningBidId] ON [Auctions] ([WinningBidId]);
GO

ALTER TABLE [Auctions] ADD CONSTRAINT [FK_Auctions_Bids_CurrentBidId] FOREIGN KEY ([CurrentBidId]) REFERENCES [Bids] ([BidId]);
GO

ALTER TABLE [Auctions] ADD CONSTRAINT [FK_Auctions_Bids_StartingBidId] FOREIGN KEY ([StartingBidId]) REFERENCES [Bids] ([BidId]);
GO

ALTER TABLE [Auctions] ADD CONSTRAINT [FK_Auctions_Bids_WinningBidId] FOREIGN KEY ([WinningBidId]) REFERENCES [Bids] ([BidId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240201042000_AuctionContainsBidInfo', N'6.0.26');
GO

COMMIT;
GO

