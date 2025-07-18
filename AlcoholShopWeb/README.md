# AlcoholShopWeb – ASP.NET Core MVC Shop for Alcoholic Beverages

## Opis projektu

**AlcoholShopWeb** to aplikacja sklepu internetowego z alkoholami, napisana w technologii **ASP.NET Core MVC**.
System pozwala na przeglądanie produktów, rejestrację użytkowników (z ograniczeniem wiekowym 18+),
zarządzanie koszykiem, składanie zamówień, publikację blogów oraz śledzenie aktywności administratora za pomocą systemu logów.

---

## Funkcje główne

### Sklep:
- Lista dostępnych alkoholi
- Szczegóły produktu (opis, zdjęcie, metoda produkcji, producent, dane techniczne)
- Dodawanie do koszyka i składanie zamówień (dla zalogowanych)

### Użytkownicy:
- Rejestracja z walidacją wieku (18+)
- Logowanie i sesja
- Edycja profilu użytkownika
- Historia zamówień

### Koszyk:
- Dodawanie produktów
- Edycja ilości
- Usuwanie produktów z koszyka
- Składanie zamówień

### Blog:
- Lista wpisów blogowych
- Szczegóły posta
- Przypisanie tagów i kategorii
- Widok ostatnich 3 postów na stronie głównej

### Recenzje:
- Dodawanie ocen i komentarzy do produktów
- Widoczność opinii publiczna

### Panel Admina:
- CRUD dla produktów, kategorii, producentów, postów blogowych
- System logów (edycja, usuwanie, dodawanie)

---

## Struktura projektu

```
AlcoholShopWeb/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
├───Controllers
│       AccountController.cs
│       AdminAgingController.cs
│       AdminBlogController.cs
│       AdminCountriesController.cs
│       AdminLogsController.cs
│       AdminOrdersController.cs
│       AdminProducersController.cs
│       AdminProductionMethodsController.cs
│       AdminProductsController.cs
│       BlogController.cs
│       CartController.cs
│       CategoriesController.cs
│       HomeController.cs
│       OrderItemsController.cs
│       OrdersController.cs
│       ProducersController.cs
│       ProductsController.cs
│       ReviewsController.cs
│       UsersController.cs
│
├───data
│       AlcoholShopContext.cs
│
├───Models
│   │   Aging.cs
│   │   BlogCategory.cs
│   │   BlogPost.cs
│   │   BlogPostTag.cs
│   │   BlogTag.cs
│   │   Cart.cs
│   │   CartItem.cs
│   │   Category.cs
│   │   Country.cs
│   │   DeliveryMethod.cs
│   │   Log.cs
│   │   Order.cs
│   │   OrderItem.cs
│   │   OrderStatus.cs
│   │   PaymentMethod.cs
│   │   Producer.cs
│   │   Product.cs
│   │   ProductionMethod.cs
│   │   Review.cs
│   │   User.cs
│   │
│   └───ViewModels
│           ErrorViewModel.cs
│           HomeViewModel.cs
│           LoginViewModel.cs
│           OrderFormViewModel.cs
│           RegisterViewModel.cs
│
├───Services
│       ShoppingCartService.cs
│
├───Views
│   │   _ViewImports.cshtml
│   │   _ViewStart.cshtml
│   │
│   ├───Account
│   │       Login.cshtml
│   │       Profile.cshtml
│   │       Register.cshtml
│   │
│   ├───AdminAging
│   │       Create.cshtml
│   │       Delete.cshtml
│   │       Edit.cshtml
│   │       Index.cshtml
│   │
│   ├───AdminBlog
│   │       Create.cshtml
│   │       Delete.cshtml
│   │       Edit.cshtml
│   │       Index.cshtml
│   │
│   ├───AdminCountries
│   │       Create.cshtml
│   │       Delete.cshtml
│   │       Edit.cshtml
│   │       Index.cshtml
│   │
│   ├───AdminLogs
│   │       Logs.cshtml
│   │
│   ├───AdminOrders
│   │       Details.cshtml
│   │       Index.cshtml
│   │
│   ├───AdminProducers
│   │       Create.cshtml
│   │       Delete.cshtml
│   │       Edit.cshtml
│   │       Index.cshtml
│   │
│   ├───AdminProductionMethods
│   │       Create.cshtml
│   │       Delete.cshtml
│   │       Edit.cshtml
│   │       Index.cshtml
│   │
│   ├───AdminProducts
│   │       Create.cshtml
│   │       Delete.cshtml
│   │       Details.cshtml
│   │       Edit.cshtml
│   │       Index.cshtml
│   │
│   ├───Blog
│   │       Details.cshtml
│   │       Index.cshtml
│   │
│   ├───Cart
│   │       Index.cshtml
│   │
│   ├───Home
│   │       Index.cshtml
│   │
│   ├───Orders
│   │       Confirm.cshtml
│   │       Create.cshtml
│   │       Orders.cshtml
│   │
│   ├───Products
│   │       Details.cshtml
│   │       Index.cshtml
│   │
│   └───Shared
│           Error.cshtml
│           _Layout.cshtml
│           _Layout.cshtml.css
│           _ValidationScriptsPartial.cshtml
│
├── appsettings.json
└── Program.cs
```

---

## Wymagania

- [.NET 6.0](https://dotnet.microsoft.com/en-us/download)
- [MySQL](https://www.mysql.com/)
- Visual Studio 2022 / VS Code

---

## Konfiguracja

1. Skonfiguruj połączenie z bazą w `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=alcoholshop;User=root;Password=yourpassword;"
}
```

2. Uruchom skrypt SQL ze strukturą bazy (dostarczony w repozytorium).
3. Uruchom projekt w Visual Studio.

---

## Walidacja wieku

Podczas rejestracji użytkownik musi:
- Podać datę urodzenia
- Zostać automatycznie zweryfikowany pod kątem wieku (min. 18 lat)

```csharp
if ((DateTime.Today - model.BirthDate).TotalDays / 365.25 < 18)
{
    ModelState.AddModelError("BirthDate", "Musisz mieć co najmniej 18 lat, aby się zarejestrować.");
    return View(model);
}
```

---

## System logów administratora

Każda edycja, dodanie lub usunięcie produktu/posta przez admina zapisuje się w tabeli `Logs`:

- `LogID`, `UserID`, `Action`, `Description`, `CreatedAt`
- Widok logów w `AdminLogsController` -> `/AdminLogs/Logs`

---

## Widoki i wygląd

- Bootstrap 5
- Widoki zoptymalizowane pod UX e-commerce:
  - Szczegóły produktu (zdjęcie, opis, cena, koszyk, producent, metoda produkcji)
  - Blog jako wsparcie SEO i marketingu

---

## Przykładowe konta

| Email                   | Hasło     | Rola    |
|-------------------------|-----------|---------|
| admin@example.com       | zaq1@WSX  | Admin   |
| klient@example.com      | zaq1@WSX  | Client  |

---

## Autor i licencja

Projekt stworzony do celów edukacyjnych na uczelnię - Politechnika Śląska w Gliwicach na wydział matematyki stosowanej, kierunek informatyka niestacjonarna.

**Licencja:** MIT  
**Data wykonania:** 2025-07-18
