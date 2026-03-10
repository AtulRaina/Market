# Getting Started

This document explains how to build and run the MarketAnalyzer solution on a developer machine.

---

## ✅ Prerequisites

1. **Windows** (project targets .NET Framework 4.0)
2. **Visual Studio** (2010/2012/2013/2015 should work) or a compatible MSBuild environment.
3. **Chrome Browser** (must match the version of `chromedriver` you use).
4. **ChromeDriver**
   - Place `chromedriver.exe` on your system `PATH` or in the same folder as the built executable.
   - The project expects `new ChromeDriver()` to work without passing an explicit driver path.
5. **NuGet packages** (included under `packages/`):
   - Selenium WebDriver (`WebDriver.dll`, `WebDriver.Support.dll`)
   - System.Data.SQLite
   - EntityFramework (not actively used but referenced in `Marketdb`)

---

## 🏗 Build

1. Open `MarketAnalyzer.sln` in Visual Studio.
2. Ensure the solution builds cleanly.
3. If packages are missing, run `Restore NuGet Packages` from Visual Studio or run `nuget restore MarketAnalyzer.sln`.

---

## ▶️ Run

1. Run the `MarketAnalyzer` project (it is a WPF application).
2. In the UI:
   - Enter a URL in the text box to run a single vendor scrape.
   - Or select one or more vendor checkboxes and click the button.
   - Optionally enable `Multi Tasking` to run each selected vendor on a separate thread.

---

## 🔎 Inspecting Results

- **Log output**: `MarketLog.log` (written to the current working directory).
- **Database output**: `MarketDataBaseFile.sqlite` (created in the current working directory).
  - You can use a SQLite browser (e.g., DB Browser for SQLite) to inspect tables:
    - `MasterProductTable` (master products)
    - `PriceTable` (scraped prices)

---

## 🛠 Updating Scraping Logic

To update the scraping selectors for a vendor, edit `MarketCore/WebControl.xml` and adjust the relevant CSS/XPath selectors.

To add a new vendor:
1. Add a new `<pageControls>` entry to `WebControl.xml`.
2. Add a new vendor scraper class to `MarketCore/` (copy an existing vendor class).
3. Wire the vendor into `PageController` and optionally the UI.
