-- Migration: Create Claims Tables for PostgreSQL
-- Description: Creates all tables for the Claims domain

-- Create schema if it doesn't exist
CREATE SCHEMA IF NOT EXISTS claims;

-- Create Claims table
CREATE TABLE claims.claim (
    claim_id UUID PRIMARY KEY,
    case_id INTEGER NOT NULL,
    claim_type_id INTEGER NOT NULL,
    claimant_id INTEGER NOT NULL,
    client_id INTEGER NOT NULL,
    policy_number VARCHAR(100) NOT NULL,
    delta_file_number VARCHAR(50) NULL,
    client_file_number VARCHAR(50) NULL,
    doctor_id INTEGER NULL,
    state_of_loss_id INTEGER NULL,
    alleged_injury VARCHAR(255) NULL,
    injury_description TEXT NULL,
    attorney_representation BOOLEAN NOT NULL DEFAULT FALSE,
    liability VARCHAR(255) NULL,
    workers_compensation BOOLEAN NOT NULL DEFAULT FALSE,
    exposure DECIMAL(18,2) NULL,
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL
);

-- Create ClaimDocuments table
CREATE TABLE claims.claim_document (
    document_id UUID PRIMARY KEY,
    claim_id UUID NOT NULL,
    document_name VARCHAR(255) NOT NULL,
    document_type VARCHAR(100) NOT NULL,
    file_path VARCHAR(500) NOT NULL,
    file_size BIGINT NOT NULL,
    uploaded_by_user INTEGER NOT NULL,
    uploaded_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL,
    FOREIGN KEY (claim_id) REFERENCES claims.claim(claim_id)
);

-- Create ClaimEvents table
CREATE TABLE claims.claim_event (
    event_id UUID PRIMARY KEY,
    claim_id UUID NOT NULL,
    event_type VARCHAR(100) NOT NULL,
    event_description TEXT NOT NULL,
    event_date TIMESTAMP WITH TIME ZONE NOT NULL,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    FOREIGN KEY (claim_id) REFERENCES claims.claim(claim_id)
);

-- Create Doctors table
CREATE TABLE claims.doctor (
    doctor_id UUID PRIMARY KEY,
    doctor_name VARCHAR(255) NOT NULL,
    doctor_specialty VARCHAR(100) NULL,
    doctor_phone VARCHAR(50) NULL,
    doctor_address TEXT NULL,
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL
);

-- Create ClaimTypes table
CREATE TABLE claims.claim_type (
    claim_type_id UUID PRIMARY KEY,
    type_name VARCHAR(100) NOT NULL,
    description TEXT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL
);

-- Create StatesOfLoss table
CREATE TABLE claims.state_of_loss (
    state_of_loss_id UUID PRIMARY KEY,
    state_name VARCHAR(100) NOT NULL,
    state_code VARCHAR(10) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL
);

-- Create indexes for better performance
CREATE INDEX IX_claim_case_id ON claims.claim(case_id);
CREATE INDEX IX_claim_claimant_id ON claims.claim(claimant_id);
CREATE INDEX IX_claim_client_id ON claims.claim(client_id);
CREATE INDEX IX_claim_claim_type_id ON claims.claim(claim_type_id);
CREATE INDEX IX_claim_doctor_id ON claims.claim(doctor_id);
CREATE INDEX IX_claim_state_of_loss_id ON claims.claim(state_of_loss_id);
CREATE INDEX IX_claim_deleted ON claims.claim(deleted);
CREATE INDEX IX_claim_created_on ON claims.claim(created_on);

CREATE INDEX IX_claim_document_claim_id ON claims.claim_document(claim_id);
CREATE INDEX IX_claim_document_document_type ON claims.claim_document(document_type);
CREATE INDEX IX_claim_document_deleted ON claims.claim_document(deleted);

CREATE INDEX IX_claim_event_claim_id ON claims.claim_event(claim_id);
CREATE INDEX IX_claim_event_event_type ON claims.claim_event(event_type);
CREATE INDEX IX_claim_event_event_date ON claims.claim_event(event_date);
CREATE INDEX IX_claim_event_deleted ON claims.claim_event(deleted);

CREATE INDEX IX_doctor_last_name ON claims.doctor(last_name);
CREATE INDEX IX_doctor_specialization ON claims.doctor(specialization);
CREATE INDEX IX_doctor_deleted ON claims.doctor(deleted);

CREATE INDEX IX_claim_type_type_name ON claims.claim_type(type_name);
CREATE INDEX IX_claim_type_is_active ON claims.claim_type(is_active);
CREATE INDEX IX_claim_type_deleted ON claims.claim_type(deleted);

CREATE INDEX IX_state_of_loss_state_code ON claims.state_of_loss(state_code);
CREATE INDEX IX_state_of_loss_is_active ON claims.state_of_loss(is_active);
CREATE INDEX IX_state_of_loss_deleted ON claims.state_of_loss(deleted);

-- Insert sample data
INSERT INTO claims.claim_type (type_name, description, created_by_user) VALUES
('Personal Injury', 'Claims related to personal injuries', 1),
('Property Damage', 'Claims related to property damage', 1),
('Medical Malpractice', 'Claims related to medical malpractice', 1),
('Workers Compensation', 'Claims related to workplace injuries', 1);

INSERT INTO claims.state_of_loss (state_name, state_code, created_by_user) VALUES
('California', 'CA', 1),
('Texas', 'TX', 1),
('Florida', 'FL', 1),
('New York', 'NY', 1),
('Illinois', 'IL', 1);

INSERT INTO claims.doctor (first_name, last_name, specialization, license_number, created_by_user) VALUES
('Dr. John', 'Smith', 'Orthopedics', 'MD12345', 1),
('Dr. Sarah', 'Johnson', 'Neurology', 'MD67890', 1),
('Dr. Michael', 'Brown', 'Cardiology', 'MD11111', 1);

