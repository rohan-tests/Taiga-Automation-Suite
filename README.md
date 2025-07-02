# 🚀 Taiga Test Automation Suite

An end-to-end UI automation framework built using **Playwright (.NET)** and **SpecFlow**, integrated with **ExtentReports**, **API-based test data management**, and **parallel test execution**.  
This project tests real Kanban board features on the Taiga Agile Project Management platform — structured for production-level scalability and showcase readiness.

---

## 🧰 Tech Stack

- ✅ Playwright (C#)
- ✅ SpecFlow (BDD)
- ✅ NUnit
- ✅ ExtentReports (with screenshot on failure)
- ✅ APIRequestContext (Playwright API)
- ✅ Context Injection (for parallel-safe execution)

---

## 🎯 Scenarios Covered

| Feature | Scenario |
|--------|----------|
| Story Creation | Create a new story (positive + negative) |
| Story Update | Edit title and description |
| Story Movement | Move story across board columns |
| Story Deletion | Delete a story from the board |
| Commenting | Add a comment to a story |
| Filtering | Filter stories using search bar |

---

## 🧪 Framework Highlights

- 🔹 **Parallel Test Execution** (via Assembly-level config)  
  ⤷ Avg. runtime per scenario: ~15s  
- 🔹 **Scenario Tagging & Filtering** (`@Smoke`, `@Negative`, `@NeedSeedStory`)  
- 🔹 **Config-Driven Execution** via `config.json`  
  ⤷ Toggle features like board reset without code change  
- 🔹 **API-Based Preconditioning**  
  ⤷ Create, delete, or reset stories without relying on UI steps  
- 🔹 **Clean Context Injection**  
  ⤷ Each scenario runs isolated — browser-safe and scalable  
- 🔹 **Failure-Proof Reporting**  
  ⤷ Auto-screenshots + step logs via ExtentReports

---

## 🧾 Project Structure

Taiga-Automation-Suite/
│
├── Features/             → Gherkin-based .feature files
├── StepDefinitions/      → Scenario step implementations
├── Pages/                → Page Object Model classes
├── Utilities/            → API Helper, Config Reader, Extent Wrapper
├── Reports/              → Latest ExtentReport.html
├── Screenshots/          → Captures for failed tests
├── Docs/                 → Manual test cases + traceability matrix
└── config.json           → Test config controls

---

## ⚙️ How to Run

```bash
dotnet test
````

Run specific tags:

```bash
dotnet test --filter "TestCategory=Smoke"
```

Parallelism is handled via `[assembly: Parallelizable]` in `AssemblyInfo.cs`.

---


## 🧠 Future Enhancements

* 🔄 GitHub Actions / Jenkins CI Pipeline
* 📊 Real-time Test Dashboard Integration
* 🔁 Retry logic with intelligent failure capture

---
