# Reading Diary – ASP.NET Core MVC Learning Project

A web application for managing books, ratings, and personal reading diaries.  
This project was created primarily as a **learning and portfolio project** focused on building a full-featured web application using **ASP.NET Core MVC**.

[🇨🇿 verze níže](#česky)

---

## About

Reading Diary is a web application that allows users to browse a catalog of books, rate them, and keep a personal reading diary.

The main goal of this project is **education**, not production deployment.  
During development, I focused on understanding how a real-world MVC application is structured, how individual layers communicate, and how to design clean, readable, and maintainable code.

Some solutions are intentionally simplified or local (for example, local image storage instead of cloud services) to keep the learning process transparent, understandable, and self-contained.

---

## Project Goals

The project was built to practice and understand:

- ASP.NET Core MVC architecture
- separation of concerns (Domain / Application / Infrastructure / Web)
- Entity Framework Core and relational data modeling
- user authentication and authorization (ASP.NET Identity)
- roles and permissions
- ViewModels, DTOs, and service-based business logic
- server-side pagination, filtering, and sorting
- form handling, validation, and file uploads

---

## Features

- Public book catalog with pagination, filtering, and sorting
- User authentication (Register / Login / Logout)
- Role-based authorization (Reader / Admin)
- Book ratings (1–5 stars, one rating per user)
- Personal reading diary per book
- Reading status tracking:
  - To Read
  - Reading
  - Finished
  - Postponed
- WYSIWYG editor (TinyMCE) for diary notes
- Admin section for managing books and genres
- Local image storage with validation (file type, size)

---

## Technologies

- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- Razor Views
- Bootstrap 5
- TinyMCE
- SQL Server / LocalDB

---

## Architecture Overview

The application is structured into the following layers:

**Domain**  
Entities, enums, core business rules

**Application**  
DTOs, interfaces, services, application logic, exceptions

**Infrastructure**  
Entity Framework Core, repositories, database context, seeders, file storage services

**Web**  
Controllers, Razor views, ViewModels, UI logic

This structure was chosen to keep business logic independent from the UI and to make the application easier to understand, maintain, and extend in the future.

---

## Image Storage

Book cover images are stored **locally on disk**, outside of the project directory.

This approach was chosen intentionally for learning purposes:
- to understand file handling in ASP.NET Core
- to avoid external dependencies (Azure Blob Storage, AWS S3, etc.)
- to keep the project easy to run locally without additional configuration

---

## Seed Data

The project includes seed data for development and testing purposes:

- predefined user roles
- a test admin account
- a sample set of books (Tolkien, Asimov, Clarke, Adams, etc.)
- randomized creation dates to test sorting and pagination

---

## Status

**Version 1.0**

The project is functionally complete as an MVP-level learning application.  
Future improvements may include API endpoints, a SPA frontend, or cloud-based storage.

---

## Author

**Michal Socha**

Learning and portfolio project focused on mastering  
ASP.NET Core MVC and backend web application development.

---

## Česky

Čtenářský deník je webová aplikace pro správu knih, jejich hodnocení  
a vedení osobního čtenářského deníku.

Projekt vznikl primárně jako **učební a portfolio projekt**, jehož cílem bylo
naučit se vytvářet plnohodnotnou aplikaci v **ASP.NET Core MVC** – od databáze,
přes aplikační logiku až po uživatelské rozhraní.

Během vývoje byl kladen důraz na:
- pochopení architektury MVC
- oddělení odpovědností jednotlivých vrstev
- práci s Entity Framework Core
- autentizaci, role a autorizaci uživatelů
- návrh ViewModelů, DTO a aplikačních služeb
- řešení reálných scénářů (hodnocení knih, čtenářský deník, stránkování)

Některá řešení (například ukládání obrázků lokálně) jsou zvolena záměrně
pro vzdělávací účely a snadnější pochopení principů aplikace.
