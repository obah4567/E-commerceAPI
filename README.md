# E-commerceAPI

# A propos 
This is an Ecommerce Application built with .Net 8 web api. It features full CRUD ability on products, order, cart, wishLists, shipments ...
Use of Entity Framework to save data in a Database, and use of the ## Bogus libraries to generate very realistic "fake" data.
Logging implementation to capture output information and for debugging.
Unit Tests to test the Repository (only for ProductRepository)

## Schéma de l'architecture

```mermaid
graph TD;
    A[E-commerceAPI.sln] --> B[E-commerceAPI.Domain]
    A --> C[E-commerceAPI.Application]
    A --> D[E-commerceAPI.Infrastructure]
    A --> E[E-commerceAPI.Tests]

    B --> B1[Models]
    B1 --> B1a[Carts.cs]
    B1 --> B1b[Category.cs]
    B1 --> B1c[Product.cs]
    B1 --> B1d[...]
    
    B --> B2[DTO]
    B2 --> B2a[CartDTO.cs]
    B2 --> B2b[CategoryDTO.cs]
    B2 --> B2c[ProductDTO.cs]
    B2 --> B2d[...]

    B --> B3[GeneratorDataByBogus]
    B3 --> B3a[GeneratorDataByBogus.cs]
    
    B --> B4[Services]
    B4 --> B4a[ICartRepository.cs]
    B4 --> B4b[ICategoryRepository.cs]
    B4 --> B4c[...]

    C --> C1[Controllers]
    C1 --> C1a[CartController.cs]
    C1 --> C1b[CategoryController.cs]
    C1 --> C1c[PaymentController.cs]
    C1 --> C1d[ProductController.cs]
    C1 --> C1e[ShipmentController.cs]
    C1 --> C1f[WishListController.cs]
    C1 --> C1g[Program.cs]

    D --> D1[DbContexts]
    D1 --> D1a[Context.cs]

    D --> D2[Repositories]
    D2 --> D2a[CartRepository.cs]
    D2 --> D2b[CategoryRepository.cs]
    D2 --> D2c[...]

    E --> E1[Infrastructure]
    E1 --> E2[Repository]
    E2 --> E2a[ProductRepositoryTests.cs]
    E2 --> E2b[...]
