# E-commerceAPI

# A propos 
This is an Ecommerce Application built with .Net 8 web api. It features full CRUD ability on products, order, cart, wishLists, shipments ...
Utilisation de Entity Framework pour enregister les données en Base de Données, et utilisation de la libraires #Bogus pour generer des "fakes" données trés réalistes. 
Mise en place de Log (Logging) pour capturer les informations en sortie et pour le debugage.
Tests Unitaires pour tester le Repository (seulement pour ProductRepository)

#Clean Code Architecture
.
E-commerceAPI.sln
│
├── E-commerceAPI.Domain
│   ├── Models
│   │   └── Carts.cs
│   │   └── Category.cs
│   │   └── Product.cs
│   │   └── ...
│   │   
│   ├── DTO
│   │   └── CartDTO.cs
│   │   └── CategoryDTO.cs
│   │   └── ProductDTO.cs
│   │   └── ...
│   │   
│   ├── GeneratorDataByBogus
│   │   └── GeneratorDataByBogus.cs 
│   │   
│   └── Services
│       └── ICartRepository.cs
│       └── ICategoryRepository.cs
│       └── ...
│
├── E-commerceAPI.Application
│   └── Controllers
│       └── CartController.cs
|       └── CategoryController.cs
|       └── PaymentController.cs
|       └── ProductController.cs
|       └── ShipmentController.cs
|       └── WishListController.cs
|     └─ Program.cs
│
├── E-commerceAPI.Infrastructure
│   ├── DbContexts
│   │   └── Context.cs
│   │
│   └── Repositories
│       └── CartRepository.cs
│       └── CategoryRepository.cs
│       └── ...
│
└── E-commerceAPI.Tests
    └── Infrastructure
      └── Repository
        └── ProductRepositoryTests
        └── ...
