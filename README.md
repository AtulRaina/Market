# Market (MarketAnalyzer)

**Market** is a Windows desktop tool (WPF) for scraping product pricing information from multiple online retailers and storing results in a local SQLite database. It uses Selenium WebDriver (ChromeDriver) to automate browser interaction and CSS/XPath selectors configured via `WebControl.xml`.

> ⚠️ This project is a prototype / research tool. It contains early-stage scraping logic (hard-coded selectors, no robust error handling) and is intended for exploration or further refactoring rather than production use.

---

## 📦 Repository Structure

- `MarketAnalyzer/` — WPF UI project (entry point) that drives scraping tasks and lets you select vendors and options.
- `MarketCore/` — Core scraping logic (vendor-specific scrapers + controller + config reader).
- `Marketdb/` — Local SQLite database management and tables storage.
- `Logger/` — Simple file logger used across projects.
- `packages/` — NuGet packages and third-party binaries (EntityFramework, SQLite, Selenium, etc.)

---

## ✅ Quick Start

### 1) Prerequisites

- Windows (project targets .NET Framework 4.0 / Visual Studio 2010 era)
- Visual Studio (2012/2013/2015 should open the solution, but older VS may be required for compatibility)
- Chrome (a version compatible with the bundled ChromeDriver)
- ChromeDriver on `PATH` or placed next to the executable (the code calls `new ChromeDriver()` with no path)

### 2) Build

1. Open `MarketAnalyzer.sln` in Visual Studio.
2. Restore NuGet packages, or ensure `packages/` directory exists and contains required assemblies.
3. Build the solution.

### 3) Run

- Run the `MarketAnalyzer` project (WPF UI).
- Choose a vendor by checking a checkbox, or provide a direct URL in the input box.
- `Multi Tasking` enables per-vendor scraping in a separate thread.

---

## 🧩 Configuration

- **Selectors and vendor configuration:** `MarketCore/WebControl.xml`
  - Contains vendor-specific CSS/XPath selectors for search boxes, results, prices, etc.
  - When a site changes its HTML structure, update the selector entries to match.

- **Database file:** `MarketDataBaseFile.sqlite`
  - Created/overwritten automatically when `MarketDatabaseOperations()` is constructed without parameters.
  - Contains two tables: `MasterProductTable` and `PriceTable`.

- **Log file:** `MarketLog.log`
  - Written by `MarketLogger.Logger`.

---

## 🧠 High-level Flow

1. The UI (`MarketAnalyzer/MainWindow.xaml.cs`) creates `PageController` instances for each selected vendor.
2. `PageController` reads master products from the SQLite database and calls vendor-specific scraper classes (e.g., `Amazon`, `BestBuy`, etc.).
3. Each vendor scraper uses Selenium to navigate the vendor site, uses selectors from `WebControl.xml`, and writes scraped prices to the database.

---

## 📌 Notes / Known Limitations

- Scraper logic is brittle (relies on hard-coded CSS selectors and XPath). Websites often change structure.
- Concurrency is implemented with raw `Thread` objects and no synchronization.
- Database operations use string concatenation for SQL (risk of SQL injection and errors if data contains quotes).
- The SQLite file is deleted & recreated by default on startup (`MarketDatabaseOperations.CreateDatabase()`), so data is not persisted across runs unless you use the alternate constructor (`new MarketDatabaseOperations("No Operations")`).

---

## 📄 Next Steps / Improvements (Suggested)

- Add a proper configuration layer + a shipping of a structured mapping file (JSON/YAML) for selectors.
- Use a proper ORM or parameterized queries (avoid string concatenation in SQL).
- Replace `Thread` with `Task` / `async` and improve UI responsiveness.
- Add unit tests and vendor extraction contract interfaces.

---

If you want a more detailed architecture/design overview, see `docs/Architecture.md` and `docs/Design.md`.
