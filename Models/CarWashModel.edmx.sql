
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/26/2018 21:38:54
-- Generated from EDMX file: C:\Users\Pavel\documents\visual studio 2015\Projects\CarWash\CarWash\Models\CarWashModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CarWash];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClientCarClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarClientSet] DROP CONSTRAINT [FK_ClientCarClient];
GO
IF OBJECT_ID(N'[dbo].[FK_CarCarClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarClientSet] DROP CONSTRAINT [FK_CarCarClient];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryOfCarCar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarSet] DROP CONSTRAINT [FK_CategoryOfCarCar];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryOfCarServicesByCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServicesByCategorySet] DROP CONSTRAINT [FK_CategoryOfCarServicesByCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ServicesServicesByCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServicesByCategorySet] DROP CONSTRAINT [FK_ServicesServicesByCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_CarWasherWorkSchedule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkScheduleSet] DROP CONSTRAINT [FK_CarWasherWorkSchedule];
GO
IF OBJECT_ID(N'[dbo].[FK_CarWasherRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecordSet] DROP CONSTRAINT [FK_CarWasherRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_BoxesRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecordSet] DROP CONSTRAINT [FK_BoxesRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_CarClientRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecordSet] DROP CONSTRAINT [FK_CarClientRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_RecordOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_RecordOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderServicesByCategory_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderServicesByCategory] DROP CONSTRAINT [FK_OrderServicesByCategory_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderServicesByCategory_ServicesByCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderServicesByCategory] DROP CONSTRAINT [FK_OrderServicesByCategory_ServicesByCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PaymentSet] DROP CONSTRAINT [FK_OrderPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_RecordServicesByCategory_Record]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecordServicesByCategory] DROP CONSTRAINT [FK_RecordServicesByCategory_Record];
GO
IF OBJECT_ID(N'[dbo].[FK_RecordServicesByCategory_ServicesByCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecordServicesByCategory] DROP CONSTRAINT [FK_RecordServicesByCategory_ServicesByCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ClientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientSet];
GO
IF OBJECT_ID(N'[dbo].[CarClientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarClientSet];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[CarSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarSet];
GO
IF OBJECT_ID(N'[dbo].[CategoryOfCarSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryOfCarSet];
GO
IF OBJECT_ID(N'[dbo].[ServicesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServicesSet];
GO
IF OBJECT_ID(N'[dbo].[ServicesByCategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServicesByCategorySet];
GO
IF OBJECT_ID(N'[dbo].[BoxesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BoxesSet];
GO
IF OBJECT_ID(N'[dbo].[WorkScheduleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkScheduleSet];
GO
IF OBJECT_ID(N'[dbo].[CarWasherSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarWasherSet];
GO
IF OBJECT_ID(N'[dbo].[RecordSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecordSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[PaymentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentSet];
GO
IF OBJECT_ID(N'[dbo].[OrderServicesByCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderServicesByCategory];
GO
IF OBJECT_ID(N'[dbo].[RecordServicesByCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecordServicesByCategory];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [ClientId] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NULL,
    [PhoneNumer] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CarClientSet'
CREATE TABLE [dbo].[CarClientSet] (
    [CarNumber] uniqueidentifier  NOT NULL,
    [ClientClientId] uniqueidentifier  NOT NULL,
    [CarCarId] uniqueidentifier  NOT NULL,
    [PriorityBox] int  NULL,
    [PriorityWashman] nvarchar(max)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'CarSet'
CREATE TABLE [dbo].[CarSet] (
    [CarId] uniqueidentifier  NOT NULL,
    [Mark] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [CategoryOfCarCategoryId] int  NOT NULL
);
GO

-- Creating table 'CategoryOfCarSet'
CREATE TABLE [dbo].[CategoryOfCarSet] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ServicesSet'
CREATE TABLE [dbo].[ServicesSet] (
    [CodeService] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ServicesByCategorySet'
CREATE TABLE [dbo].[ServicesByCategorySet] (
    [ServiceId] uniqueidentifier  NOT NULL,
    [Price] int  NOT NULL,
    [Duration] time  NOT NULL,
    [CategoryOfCarCategoryId] int  NOT NULL,
    [ServicesCodeService] int  NOT NULL
);
GO

-- Creating table 'BoxesSet'
CREATE TABLE [dbo].[BoxesSet] (
    [BoxId] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WorkScheduleSet'
CREATE TABLE [dbo].[WorkScheduleSet] (
    [Year] int IDENTITY(1,1) NOT NULL,
    [Week] int IDENTITY(1,1) NOT NULL,
    [NumberOfHours] int  NOT NULL,
    [CarWasherCarWasherId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'CarWasherSet'
CREATE TABLE [dbo].[CarWasherSet] (
    [CarWasherId] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RecordSet'
CREATE TABLE [dbo].[RecordSet] (
    [RecordId] uniqueidentifier  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [CarWasherCarWasherId] uniqueidentifier  NOT NULL,
    [BoxesBoxId] int  NOT NULL,
    [CarClientCarNumber] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [OrderId] uniqueidentifier  NOT NULL,
    [RecordRecordId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'PaymentSet'
CREATE TABLE [dbo].[PaymentSet] (
    [PaymentId] uniqueidentifier  NOT NULL,
    [DataTime] nvarchar(max)  NOT NULL,
    [OrderOrderId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'OrderServicesByCategory'
CREATE TABLE [dbo].[OrderServicesByCategory] (
    [Order_OrderId] uniqueidentifier  NOT NULL,
    [ServicesByCategory_ServiceId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RecordServicesByCategory'
CREATE TABLE [dbo].[RecordServicesByCategory] (
    [Record_RecordId] uniqueidentifier  NOT NULL,
    [ServicesByCategory_ServiceId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClientId] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([ClientId] ASC);
GO

-- Creating primary key on [CarNumber] in table 'CarClientSet'
ALTER TABLE [dbo].[CarClientSet]
ADD CONSTRAINT [PK_CarClientSet]
    PRIMARY KEY CLUSTERED ([CarNumber] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [CarId] in table 'CarSet'
ALTER TABLE [dbo].[CarSet]
ADD CONSTRAINT [PK_CarSet]
    PRIMARY KEY CLUSTERED ([CarId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'CategoryOfCarSet'
ALTER TABLE [dbo].[CategoryOfCarSet]
ADD CONSTRAINT [PK_CategoryOfCarSet]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [CodeService] in table 'ServicesSet'
ALTER TABLE [dbo].[ServicesSet]
ADD CONSTRAINT [PK_ServicesSet]
    PRIMARY KEY CLUSTERED ([CodeService] ASC);
GO

-- Creating primary key on [ServiceId] in table 'ServicesByCategorySet'
ALTER TABLE [dbo].[ServicesByCategorySet]
ADD CONSTRAINT [PK_ServicesByCategorySet]
    PRIMARY KEY CLUSTERED ([ServiceId] ASC);
GO

-- Creating primary key on [BoxId] in table 'BoxesSet'
ALTER TABLE [dbo].[BoxesSet]
ADD CONSTRAINT [PK_BoxesSet]
    PRIMARY KEY CLUSTERED ([BoxId] ASC);
GO

-- Creating primary key on [Year], [Week], [CarWasherCarWasherId] in table 'WorkScheduleSet'
ALTER TABLE [dbo].[WorkScheduleSet]
ADD CONSTRAINT [PK_WorkScheduleSet]
    PRIMARY KEY CLUSTERED ([Year], [Week], [CarWasherCarWasherId] ASC);
GO

-- Creating primary key on [CarWasherId] in table 'CarWasherSet'
ALTER TABLE [dbo].[CarWasherSet]
ADD CONSTRAINT [PK_CarWasherSet]
    PRIMARY KEY CLUSTERED ([CarWasherId] ASC);
GO

-- Creating primary key on [RecordId] in table 'RecordSet'
ALTER TABLE [dbo].[RecordSet]
ADD CONSTRAINT [PK_RecordSet]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [OrderId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [PaymentId] in table 'PaymentSet'
ALTER TABLE [dbo].[PaymentSet]
ADD CONSTRAINT [PK_PaymentSet]
    PRIMARY KEY CLUSTERED ([PaymentId] ASC);
GO

-- Creating primary key on [Order_OrderId], [ServicesByCategory_ServiceId] in table 'OrderServicesByCategory'
ALTER TABLE [dbo].[OrderServicesByCategory]
ADD CONSTRAINT [PK_OrderServicesByCategory]
    PRIMARY KEY CLUSTERED ([Order_OrderId], [ServicesByCategory_ServiceId] ASC);
GO

-- Creating primary key on [Record_RecordId], [ServicesByCategory_ServiceId] in table 'RecordServicesByCategory'
ALTER TABLE [dbo].[RecordServicesByCategory]
ADD CONSTRAINT [PK_RecordServicesByCategory]
    PRIMARY KEY CLUSTERED ([Record_RecordId], [ServicesByCategory_ServiceId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClientClientId] in table 'CarClientSet'
ALTER TABLE [dbo].[CarClientSet]
ADD CONSTRAINT [FK_ClientCarClient]
    FOREIGN KEY ([ClientClientId])
    REFERENCES [dbo].[ClientSet]
        ([ClientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientCarClient'
CREATE INDEX [IX_FK_ClientCarClient]
ON [dbo].[CarClientSet]
    ([ClientClientId]);
GO

-- Creating foreign key on [CarCarId] in table 'CarClientSet'
ALTER TABLE [dbo].[CarClientSet]
ADD CONSTRAINT [FK_CarCarClient]
    FOREIGN KEY ([CarCarId])
    REFERENCES [dbo].[CarSet]
        ([CarId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarCarClient'
CREATE INDEX [IX_FK_CarCarClient]
ON [dbo].[CarClientSet]
    ([CarCarId]);
GO

-- Creating foreign key on [CategoryOfCarCategoryId] in table 'CarSet'
ALTER TABLE [dbo].[CarSet]
ADD CONSTRAINT [FK_CategoryOfCarCar]
    FOREIGN KEY ([CategoryOfCarCategoryId])
    REFERENCES [dbo].[CategoryOfCarSet]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryOfCarCar'
CREATE INDEX [IX_FK_CategoryOfCarCar]
ON [dbo].[CarSet]
    ([CategoryOfCarCategoryId]);
GO

-- Creating foreign key on [CategoryOfCarCategoryId] in table 'ServicesByCategorySet'
ALTER TABLE [dbo].[ServicesByCategorySet]
ADD CONSTRAINT [FK_CategoryOfCarServicesByCategory]
    FOREIGN KEY ([CategoryOfCarCategoryId])
    REFERENCES [dbo].[CategoryOfCarSet]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryOfCarServicesByCategory'
CREATE INDEX [IX_FK_CategoryOfCarServicesByCategory]
ON [dbo].[ServicesByCategorySet]
    ([CategoryOfCarCategoryId]);
GO

-- Creating foreign key on [ServicesCodeService] in table 'ServicesByCategorySet'
ALTER TABLE [dbo].[ServicesByCategorySet]
ADD CONSTRAINT [FK_ServicesServicesByCategory]
    FOREIGN KEY ([ServicesCodeService])
    REFERENCES [dbo].[ServicesSet]
        ([CodeService])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServicesServicesByCategory'
CREATE INDEX [IX_FK_ServicesServicesByCategory]
ON [dbo].[ServicesByCategorySet]
    ([ServicesCodeService]);
GO

-- Creating foreign key on [CarWasherCarWasherId] in table 'WorkScheduleSet'
ALTER TABLE [dbo].[WorkScheduleSet]
ADD CONSTRAINT [FK_CarWasherWorkSchedule]
    FOREIGN KEY ([CarWasherCarWasherId])
    REFERENCES [dbo].[CarWasherSet]
        ([CarWasherId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarWasherWorkSchedule'
CREATE INDEX [IX_FK_CarWasherWorkSchedule]
ON [dbo].[WorkScheduleSet]
    ([CarWasherCarWasherId]);
GO

-- Creating foreign key on [CarWasherCarWasherId] in table 'RecordSet'
ALTER TABLE [dbo].[RecordSet]
ADD CONSTRAINT [FK_CarWasherRecord]
    FOREIGN KEY ([CarWasherCarWasherId])
    REFERENCES [dbo].[CarWasherSet]
        ([CarWasherId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarWasherRecord'
CREATE INDEX [IX_FK_CarWasherRecord]
ON [dbo].[RecordSet]
    ([CarWasherCarWasherId]);
GO

-- Creating foreign key on [BoxesBoxId] in table 'RecordSet'
ALTER TABLE [dbo].[RecordSet]
ADD CONSTRAINT [FK_BoxesRecord]
    FOREIGN KEY ([BoxesBoxId])
    REFERENCES [dbo].[BoxesSet]
        ([BoxId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BoxesRecord'
CREATE INDEX [IX_FK_BoxesRecord]
ON [dbo].[RecordSet]
    ([BoxesBoxId]);
GO

-- Creating foreign key on [CarClientCarNumber] in table 'RecordSet'
ALTER TABLE [dbo].[RecordSet]
ADD CONSTRAINT [FK_CarClientRecord]
    FOREIGN KEY ([CarClientCarNumber])
    REFERENCES [dbo].[CarClientSet]
        ([CarNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarClientRecord'
CREATE INDEX [IX_FK_CarClientRecord]
ON [dbo].[RecordSet]
    ([CarClientCarNumber]);
GO

-- Creating foreign key on [RecordRecordId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_RecordOrder]
    FOREIGN KEY ([RecordRecordId])
    REFERENCES [dbo].[RecordSet]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecordOrder'
CREATE INDEX [IX_FK_RecordOrder]
ON [dbo].[OrderSet]
    ([RecordRecordId]);
GO

-- Creating foreign key on [Order_OrderId] in table 'OrderServicesByCategory'
ALTER TABLE [dbo].[OrderServicesByCategory]
ADD CONSTRAINT [FK_OrderServicesByCategory_Order]
    FOREIGN KEY ([Order_OrderId])
    REFERENCES [dbo].[OrderSet]
        ([OrderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ServicesByCategory_ServiceId] in table 'OrderServicesByCategory'
ALTER TABLE [dbo].[OrderServicesByCategory]
ADD CONSTRAINT [FK_OrderServicesByCategory_ServicesByCategory]
    FOREIGN KEY ([ServicesByCategory_ServiceId])
    REFERENCES [dbo].[ServicesByCategorySet]
        ([ServiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderServicesByCategory_ServicesByCategory'
CREATE INDEX [IX_FK_OrderServicesByCategory_ServicesByCategory]
ON [dbo].[OrderServicesByCategory]
    ([ServicesByCategory_ServiceId]);
GO

-- Creating foreign key on [OrderOrderId] in table 'PaymentSet'
ALTER TABLE [dbo].[PaymentSet]
ADD CONSTRAINT [FK_OrderPayment]
    FOREIGN KEY ([OrderOrderId])
    REFERENCES [dbo].[OrderSet]
        ([OrderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderPayment'
CREATE INDEX [IX_FK_OrderPayment]
ON [dbo].[PaymentSet]
    ([OrderOrderId]);
GO

-- Creating foreign key on [Record_RecordId] in table 'RecordServicesByCategory'
ALTER TABLE [dbo].[RecordServicesByCategory]
ADD CONSTRAINT [FK_RecordServicesByCategory_Record]
    FOREIGN KEY ([Record_RecordId])
    REFERENCES [dbo].[RecordSet]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ServicesByCategory_ServiceId] in table 'RecordServicesByCategory'
ALTER TABLE [dbo].[RecordServicesByCategory]
ADD CONSTRAINT [FK_RecordServicesByCategory_ServicesByCategory]
    FOREIGN KEY ([ServicesByCategory_ServiceId])
    REFERENCES [dbo].[ServicesByCategorySet]
        ([ServiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecordServicesByCategory_ServicesByCategory'
CREATE INDEX [IX_FK_RecordServicesByCategory_ServicesByCategory]
ON [dbo].[RecordServicesByCategory]
    ([ServicesByCategory_ServiceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------