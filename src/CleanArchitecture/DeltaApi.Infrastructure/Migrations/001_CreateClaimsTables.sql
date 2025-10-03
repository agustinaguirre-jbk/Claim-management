-- Migration: Create Claims Tables
-- Description: Creates all tables for the Claims domain

-- Create Claims table
CREATE TABLE Claims (
    ClaimId UNIQUEIDENTIFIER PRIMARY KEY,
    CaseId INT NOT NULL,
    ClaimTypeId INT NOT NULL,
    ClaimantId INT NOT NULL,
    ClientId INT NOT NULL,
    PolicyNumber NVARCHAR(100) NOT NULL,
    DeltaFileNumber NVARCHAR(50) NULL,
    ClientFileNumber NVARCHAR(50) NULL,
    DoctorId INT NULL,
    StateOfLossId INT NULL,
    AllegedInjury NVARCHAR(255) NULL,
    InjuryDescription TEXT NULL,
    AttorneyRepresentation BIT NOT NULL DEFAULT 0,
    Liability NVARCHAR(255) NULL,
    WorkersCompensation BIT NOT NULL DEFAULT 0,
    Exposure DECIMAL(18,2) NULL,
    Deleted BIT NOT NULL DEFAULT 0,
    CreatedByUser INT NOT NULL,
    CreatedOn DATETIME NOT NULL,
    ModifiedByUser INT NULL,
    ModifiedOn DATETIME NULL
);

-- Create ClaimDocuments table
CREATE TABLE ClaimDocuments (
    ClaimDocumentId UNIQUEIDENTIFIER PRIMARY KEY,
    ClaimId UNIQUEIDENTIFIER NOT NULL,
    DocumentType NVARCHAR(100) NOT NULL,
    FilePath NVARCHAR(1000) NOT NULL,
    Deleted BIT NOT NULL DEFAULT 0,
    CreatedByUser INT NOT NULL,
    CreatedOn DATETIME NOT NULL,
    ModifiedByUser INT NULL,
    ModifiedOn DATETIME NULL,
    FOREIGN KEY (ClaimId) REFERENCES Claims(ClaimId)
);

-- Create ClaimEvents table
CREATE TABLE ClaimEvents (
    ClaimEventId UNIQUEIDENTIFIER PRIMARY KEY,
    ClaimId UNIQUEIDENTIFIER NOT NULL,
    EventType NVARCHAR(100) NOT NULL,
    EventDate DATE NOT NULL,
    Notes TEXT NULL,
    Deleted BIT NOT NULL DEFAULT 0,
    CreatedByUser INT NOT NULL,
    CreatedOn DATETIME NOT NULL,
    ModifiedByUser INT NULL,
    ModifiedOn DATETIME NULL,
    FOREIGN KEY (ClaimId) REFERENCES Claims(ClaimId)
);

-- Create Doctors table
CREATE TABLE Doctors (
    DoctorId UNIQUEIDENTIFIER PRIMARY KEY,
    DoctorName NVARCHAR(255) NOT NULL,
    DoctorSpecialty NVARCHAR(100) NULL,
    DoctorPhone NVARCHAR(50) NULL,
    DoctorAddress TEXT NULL,
    Deleted BIT NOT NULL DEFAULT 0,
    CreatedByUser INT NOT NULL,
    CreatedOn DATETIME NOT NULL,
    ModifiedByUser INT NULL,
    ModifiedOn DATETIME NULL
);

-- Create ClaimTypes table
CREATE TABLE ClaimTypes (
    ClaimTypeId UNIQUEIDENTIFIER PRIMARY KEY,
    ClaimTypeDescription NVARCHAR(255) NOT NULL,
    ClaimTypeShortCode NVARCHAR(50) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Deleted BIT NOT NULL DEFAULT 0,
    CreatedByUser INT NOT NULL,
    CreatedOn DATETIME NOT NULL,
    ModifiedByUser INT NULL,
    ModifiedOn DATETIME NULL
);

-- Create StatesOfLoss table
CREATE TABLE StatesOfLoss (
    StateId UNIQUEIDENTIFIER PRIMARY KEY,
    StateName NVARCHAR(100) NOT NULL,
    StateCode NVARCHAR(10) NOT NULL,
    Deleted BIT NOT NULL DEFAULT 0,
    CreatedByUser INT NOT NULL,
    CreatedOn DATETIME NOT NULL,
    ModifiedByUser INT NULL,
    ModifiedOn DATETIME NULL
);

-- Create indexes for better performance
CREATE INDEX IX_Claims_CaseId ON Claims(CaseId);
CREATE INDEX IX_Claims_ClaimantId ON Claims(ClaimantId);
CREATE INDEX IX_Claims_ClientId ON Claims(ClientId);
CREATE INDEX IX_Claims_PolicyNumber ON Claims(PolicyNumber);
CREATE INDEX IX_Claims_Deleted ON Claims(Deleted);

CREATE INDEX IX_ClaimDocuments_ClaimId ON ClaimDocuments(ClaimId);
CREATE INDEX IX_ClaimDocuments_Deleted ON ClaimDocuments(Deleted);

CREATE INDEX IX_ClaimEvents_ClaimId ON ClaimEvents(ClaimId);
CREATE INDEX IX_ClaimEvents_Deleted ON ClaimEvents(Deleted);

CREATE INDEX IX_Doctors_Deleted ON Doctors(Deleted);
CREATE INDEX IX_Doctors_DoctorName ON Doctors(DoctorName);

CREATE INDEX IX_ClaimTypes_Deleted ON ClaimTypes(Deleted);
CREATE INDEX IX_ClaimTypes_IsActive ON ClaimTypes(IsActive);

CREATE INDEX IX_StatesOfLoss_Deleted ON StatesOfLoss(Deleted);
CREATE INDEX IX_StatesOfLoss_StateCode ON StatesOfLoss(StateCode);
