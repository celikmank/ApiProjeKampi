# ApiProjeKampi

## Project Overview
ApiProjeKampi is a web application designed to manage restaurant-related data such as categories, chefs, products, reservations, testimonials, and more. The project provides both an API and a web interface for CRUD operations and data presentation.

## Technologies Used
- **.NET 6**: The core framework for building the application.
- **ASP.NET Core Razor Pages**: For building the web UI.
- **Entity Framework Core**: For data access and ORM.
- **AutoMapper**: For mapping between DTOs and entity models.
- **SQL Server**: As the database provider.
- **Dependency Injection**: For managing service lifetimes and dependencies.
- **HttpClient**: For making HTTP requests.
- **Bootstrap & Swiper.js**: For responsive UI and interactive components.

## Features
- CRUD operations for entities like About, Category, Chef, Product, Reservation, Testimonial, etc.
- RESTful API endpoints for data access.
- Razor Pages for web-based management and presentation.
- Admin page routing for backend management.
- Responsive and modern UI components.

## Getting Started
1. Clone the repository:
2. 2. Open the solution in Visual Studio 2022.
3. Update the database connection string in `appsettings.json`.
4. Run database migrations (if needed).
5. Start the application.

## Folder Structure
- `Controllers/`: API controllers for each entity.
- `Dtos/`: Data Transfer Objects for input/output models.
- `Entities/`: Entity models representing database tables.
- `Context/`: Entity Framework DbContext.
- `Views/`: Razor Pages and partial views for UI.
- `wwwroot/`: Static files (CSS, JS, images).

## License
This project is licensed under the MIT License.

---

Feel free to update this README with more details specific to your project!
