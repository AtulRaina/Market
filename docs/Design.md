# Design Notes & Decisions

This document captures how the project is currently designed, why certain decisions were made, and where future improvements can be applied.

---

## ✅ Core Design Intent

The system is designed to:

- **Scrape multiple vendors** using a shared flow (search for product, parse first result).
- **Avoid hard-coding selectors** in code by moving selectors to an external XML file (`WebControl.xml`).
- **Centralize persistence** by writing results into a local SQLite database.

This makes it possible to add a new vendor by:
1. Adding vendor-specific selectors to `WebControl.xml`.
2. Creating a new vendor class (copy/paste the structure from existing ones).
3. Wiring the new vendor into `PageController` so it can be invoked from the UI.

---

## 🔧 Extending to a New Vendor

To add a new vendor, follow these steps:

1. **Add selectors to `MarketCore/WebControl.xml`**
   - Add a new `<pageControls>` block for the vendor.
   - Provide values for:
     - `vendor` (used for matching)
     - `pageurl`, `searchButton`, `searchclick`, `productname`, `productprice`, `pagelength`, `productmaster`, `productmasterprice`

2. **Create a new vendor scraper class** (e.g., `MyVendor.cs`):
   - Copy the structure from an existing class such as `Amazon.cs`.
   - Use `MarketCoreControlReader` to load selectors.
   - Implement `searchProducts()` and call `MarektPriceUpdater`.

3. **Wire it into `PageController`**
   - Add a new case to the switch statements in `PageController.creatingProduct()` and/or `PageController.cratingMaster()` based on your vendor string.

4. **Update UI (optional)**
   - Add a checkbox to `MainWindow.xaml` and wire it to start the new vendor.

---

## 🧩 How Selectors Are Used

Each vendor class relies on `MarketCoreControlReader` to read a set of selectors from `WebControl.xml`.

The name of the vendor in the XML must match the string used in the code (case-sensitive in `PageController`).

Example (from `Amazon`):

- `searchButton` is mapped by ID and used to find the search input.
- `productname` is a selector used to read the first product title.
- `productprice` is used to read price from the product details page (when `fetchSellerInformation` is enabled).

When a page changes, update the selector strings in `WebControl.xml`.

---

## 🧠 Database Design

The database schema is minimal and used to store the set of products to search and the pricing results.

### Tables

- **`MasterProductTable`**
  - `productid` (INTEGER PRIMARY KEY)
  - `MasterProductName` (string)

- **`PriceTable`**
  - `id` (INTEGER PRIMARY KEY)
  - `vendorid` (string)
  - `productid` (string)
  - `productname` (string)
  - `price` (string)
  - `sellerranking` (string)

- **`VendorTable`** (present but not used)
  - `id` (INTEGER PRIMARY KEY)
  - `VendorName` (string)

### DB Initialization Behavior

- The default constructor for `MarketDatabaseOperations` deletes and recreates `MarketDataBaseFile.sqlite` on each instantiation.
- The alternate constructor `MarketDatabaseOperations(string dummy)` does nothing (used when you do not want to reset the DB).

▶️ **Implication**: If you run the app and the database is not yet created, you should run the master-list creation flow first, or manually insert entries into `MasterProductTable`.

---

## 🧵 Concurrency Model

- The UI uses `System.Threading.Thread` to start each vendor scraper in a separate thread when "Multi Tasking" is enabled.
- There is **no synchronization**, so multiple threads writing to `MarketLog.log` or the SQLite file concurrently may lead to race conditions.

---

## ⚠️ Known Issues / Technical Debt

- **Fragile selector strategy**: Scraping is highly dependent on the site HTML structure and may break frequently.
- **Hard-coded vendor routing**: `PageController` uses `if-else` + `switch` on vendor strings; adding vendors requires changing code in multiple places.
- **SQL is built via string concatenation**: risk of malformed SQL or injection when scraped data contains quotes.
- **ChromeDriver usage**: `new ChromeDriver()` with no explicit driver path assumes `chromedriver.exe` is on PATH.
- **Error handling is minimal**: exceptions are often swallowed (especially in `MarketDatabaseOperations`, `Logger`, and vendor scrapers), which may hide problems.

---

## ✅ Suggested Refactor Ideas

- Introduce an `IVendorScraper` interface and use dependency injection to reduce switch/case routing.
- Add a proper configuration system (JSON/YAML) for selectors and vendor metadata.
- Replace raw SQL concatenation with parameterized SQL (or an ORM).
- Use `Task`/`async` with cancellation tokens instead of raw threads.
- Improve logging (make it thread-safe, add verbosity levels, include exceptions).
- Add unit tests and/or integration tests to capture expected selector behavior.
