# UI Test Report — georgia-antoniou.github.io

**Date:** 2026-07-07
**Target:** https://georgia-antoniou.github.io/ (Blazor WASM portfolio)
**Suite:** `GeorgiaAntoniou.UI.Tests` (NUnit + Microsoft.Playwright, .NET 10)
**Result:** ✅ 27 passed / 0 failed / 0 skipped — ~61s (headless Chromium)

## Summary

Exploratory testing was performed live with the Playwright MCP server across all six
routes (Home, Portfolio, Journey, About, Blog, Contact). Findings were harvested into
the automated C# suite: stale tests were repaired, brittle patterns were hardened, and
new coverage was added for previously untested behavior.

## Regressions found (tests that were failing against the live site)

The recent Generative-AI content additions had drifted the site away from the existing
assertions. These were **stale tests**, not site defects — the site is correct; the tests
were out of date.

| Test | Was asserting | Reality | Fix |
|------|---------------|---------|-----|
| `HomePageTests.Verify_The_Content_In_Home_Page` | "Certified Software Quality Specialist" | "AI-Augmented Software Quality Specialist" + GenAI subheading | Updated assertions |
| `AboutTests.Verify_Cert_Credential` | Cert links by fixed index (`First`, `Nth(5/7)`) | New "Generative AI in Software Testing" cert prepended → all indices shifted | Rewrote to locate by `href` |
| `HomePageTests.Check_Nav_Bar_Links` | Portfolio/About/Blog/Contact | New **Journey** nav link added | Added Journey to the route checks |
| `PortfolioTests.Validate_Project_cards` | `Nth(3)` → socra | Portfolio grew/re-sorted; `Nth(3)` is now a different project | Corrected index to `Nth(4)` |
| `FooterTests.Verify_LinkedIn_Link` | Trailing-slash exact URL | LinkedIn now redirects to an authwall | Assert rendered `href`, not destination |

## Reliability fixes (flaky/incorrect patterns)

- **Blazor async rendering:** immediate `IsVisibleAsync()` checks ran before the SPA
  rendered. Switched to auto-retrying web-first assertions (`Expect(...).ToBeVisibleAsync()`).
- **PDF tabs:** headless Chromium treats PDF links as downloads and never navigates the
  new tab (`about:blank`), so `WaitForLoadStateAsync()` hung. For PDF/download links we now
  assert the rendered `href` instead of the opened tab.
- **Browser leak:** no test class had a `[TearDown]`; every test leaked a Chromium process.
  Centralized lifecycle in a shared `BaseTest` with proper disposal.
- **Machine-specific path:** `HomePageTests` wrote screenshots to a hardcoded path from
  another PC. Replaced with a portable `Screenshots` folder under the test work directory.

## New coverage added

- **Journey page** (`JourneyTests`): heading, timeline cards present, Back-to-Top button.
- **About** (`AboutTests`): heading, new "AI-Augmented QA" skills section, cert links.
- **Contact form** (`ContactTests`): valid submission shows the thank-you confirmation;
  empty submission shows no success message and keeps the form visible.

## Structural changes

- Added `BaseTest` (shared Setup/TearDown, headless by default; set `HEADED=1` to watch).
- All test classes now inherit `BaseTest`, removing duplicated boilerplate.
- Bumped the test project from `net9.0` to `net10.0`.

## How to run

```powershell
# Headless (default)
dotnet test GeorgiaAntoniou.UI.Tests\GeorgiaAntoniou.UI.Tests.csproj

# Watch the browser
$env:HEADED = "1"; dotnet test GeorgiaAntoniou.UI.Tests\GeorgiaAntoniou.UI.Tests.csproj
```

> First run on a new machine requires the Playwright browser build:
> `pwsh GeorgiaAntoniou.UI.Tests\bin\Debug\net10.0\playwright.ps1 install chromium`

## Notes

- A single console error is present on every page — Google Analytics (`gtag`) failing with
  `ERR_ADDRESS_INVALID`. This is a network artifact of the test sandbox, not a site defect.
