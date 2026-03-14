# Advanced Bank Management System with Native SQL Server Auditing and Proactive Fraud Prevention

## Project Overview
A complete **Database Administration & Management (DAM)** Semester project .

This is not a simple banking app. It is a **smart database system** that performs real Database Administration tasks automatically.

## Novelty
Most banking systems handle auditing and fraud checking in the application layer.  
**Our system does everything inside SQL Server itself**:
- Triggers automatically log every change
- Stored procedures detect and block fraud in real-time
- The database becomes intelligent and secure without depending on the app


## Core DAM Tasks Implemented
- Concurrency Control using SERIALIZABLE isolation
- DML & DDL Triggers for automatic auditing
- Stored Procedures for proactive fraud prevention
- Performance Tuning (Partitioning + Columnstore Indexes)
- Role-Based Access Control (RBAC) security
- Monitoring using Extended Events and Profiler

## Features
- Login with different roles (Admin, Teller, Customer)
- Create accounts and perform transactions
- Live Audit Log (see triggers working)
- Fraud Alert system (stops suspicious transactions)
- Dashboard and reports
- Full experimentation with 10,000 synthetic transactions

## Tech Stack
- **Database**: SQL Server 2022
- **Frontend**: C# WinForms (.NET Framework 4.8)
- **Tools**: SSMS, Visual Studio 2022

## Team Members
- Kainat Zahra Alvi Sial 
- Laraib Javed 

## How to Run the Project
1. Restore the `BankManagementSystem.mdf` file in SQL Server
2. Open the solution in Visual Studio 2022
3. Update the connection string in `App.config`
4. Run the project (F5)

## Folder Structure
- `/Database` → .mdf file + all SQL scripts
- `/Application` → Complete Visual Studio WinForms project
- `/Docs` → Proposal, Final Report, Screenshots
- `/Experiments` → Test scripts and result graphs
