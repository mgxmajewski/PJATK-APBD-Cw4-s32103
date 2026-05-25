# REST API - Komputery i Komponenty

Aplikacja REST Web API w ASP.NET Core z Entity Framework Core (Code First) do zarządzania komputerami (PC) i ich komponentami. Obsługuje pełny CRUD na komputerach oraz pobieranie komponentów z zagnieżdżonymi danymi producenta i typu.

## Uruchomienie

Wymagane: .NET 10 SDK, SQL Server.

```bash
# Uruchomienie SQL Server w Docker
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=yourStrong(!)Password' \
  -e 'MSSQL_PID=Evaluation' -p 1433:1433 --name sql2025 --hostname sql2025 \
  -d mcr.microsoft.com/mssql/server:2025-latest

# Migracja i start
dotnet ef database update
dotnet run
```

Domyślny adres: `http://localhost:5113`. Baza danych jest seedowana automatycznie przy migracji (3 komputery, 3 komponenty, 3 producenci, 3 typy).

## Endpointy

| Metoda   | Trasa                      | Opis                                      | Sukces  | Błąd  |
|----------|----------------------------|--------------------------------------------|---------|-------|
| `GET`    | `/api/pcs`                 | Lista wszystkich komputerów                | `200`   | -     |
| `GET`    | `/api/pcs/{id}/components` | Komputer z zagnieżdżonymi komponentami     | `200`   | `404` |
| `POST`   | `/api/pcs`                 | Utworzenie nowego komputera                 | `201`   | -     |
| `PUT`    | `/api/pcs/{id}`            | Aktualizacja danych komputera              | `204`   | `404` |
| `DELETE` | `/api/pcs/{id}`            | Usunięcie komputera (kaskadowo)            | `204`   | `404` |

## Struktura projektu

```
Controllers/
  PCsController.cs                          - endpointy REST

Services/
  IPCService.cs                             - interfejs warstwy biznesowej
  PCService.cs                              - implementacja (async)

DAL/
  AppDbContext.cs                           - kontekst EF Core
  Configurations/
    PCConfiguration.cs                      - Fluent API + seed data
    ComponentConfiguration.cs
    ComponentTypeConfiguration.cs
    ComponentManufacturerConfiguration.cs
    PCComponentConfiguration.cs

Entities/
  PC.cs, Component.cs, ComponentType.cs     - czyste POCO
  ComponentManufacturer.cs, PCComponent.cs

DTOs/
  PCGetAllResponse.cs                       - lista PC
  PCGetByIdResponse.cs                      - PC + zagnieżdżone komponenty
  PCCreateRequest.cs, PCCreateResponse.cs   - tworzenie
  PCUpdateRequest.cs                        - aktualizacja

Migrations/                                 - wygenerowana migracja EF Core
```

## Model bazy danych

| Tabela                  | PK                        | Opis                                |
|-------------------------|---------------------------|--------------------------------------|
| `PCs`                   | `Id` (int, identity)      | Komputery                           |
| `ComponentTypes`        | `Id` (int, identity)      | Typy komponentów (CPU, GPU, RAM)    |
| `ComponentManufacturers`| `Id` (int, identity)      | Producenci (AMD, NVIDIA, Corsair)   |
| `Components`            | `Code` (char(10))         | Komponenty z FK do typu i producenta|
| `PCComponents`          | `PCId` + `ComponentCode`  | Tabela łącząca (many-to-many)       |

Usunięcie PC kaskadowo usuwa powiązania w `PCComponents`.

## Testowanie

Endpointy są testowane za pomocą wbudowanego HTTP Client w JetBrains Rider (`PJATK-APBD-Cw4-s32103.http`). Plik zawiera gotowe żądania dla wszystkich endpointów z przykładowymi ciałami JSON zgodnymi ze specyfikacją zadania. Po uruchomieniu aplikacji wystarczy kliknąć zielony przycisk ▶ przy wybranym żądaniu, aby zweryfikować poprawność odpowiedzi.

## Decyzje projektowe

**Architektura warstwowa** - Controller → Service → DAL. Kontroler nie zna `AppDbContext`, serwis nie formatuje odpowiedzi HTTP.

**DTO zamiast encji** - żadna encja bazodanowa nie jest zwracana ani przyjmowana w kontrolerze. Cała komunikacja HTTP opiera się na modelach z `DTOs/`.

**Fluent API (bez Data Annotations)** - wszystkie mapowania, relacje, klucze i ograniczenia zdefiniowane w osobnych klasach `IEntityTypeConfiguration<T>`. Encje pozostają czystymi POCO.

**Asynchroniczność** - wszystkie operacje na bazie używają `async/await` (`ToListAsync`, `FirstOrDefaultAsync`, `SaveChangesAsync`).

**Seedowanie w konfiguracjach** - `HasData()` w każdej klasie konfiguracyjnej. Dane startowe odpowiadają przykładom z treści zadania.
kkkkkk