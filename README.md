# QuickNotes

A terminal app for quick "write and forget" notes. Notes are plain `.txt` files in a single root folder. Each file's first line says which **namespace** (section) it belongs to, and each note is wrapped between a dated `-----` opener and a bare `-----` closer.

## File format (draft)

```
namespace: school
----- 2026-10-06 14:32
apply plastic cover on notebook
-----
----- 2026-10-06 15:10
buy A4 paper
also check printer ink
-----
```

- **Opener:** `-----` + space + date/time (`yyyy-MM-dd HH:mm`) → a note starts
- **Closer:** exactly `-----` and nothing else → the note ends
- Parse **line by line** with an "inside a note?" flag (a small state machine) instead of `Split`

### Open format decisions
- [ ] What if a note itself contains a line that is exactly `---`? (ignore / escape / different delimiter)
- [x] Add timestamps to the delimiter line? → yes, on the opener
- [x] Exact delimiter → exactly five dashes `-----`
- [x] Delimiter before and after each note? → yes: dated opener, bare closer
- [ ] One file per namespace, or several? (if several: which file gets new notes?)
- [ ] Default root folder location
- [ ] How does the user finish typing a note? (blank line / `.` / Ctrl+D; **not** Ctrl+S or Ctrl+Q, since terminals use those for flow control)

## Progress

### v1.0 (MVP)

- [X] **1. Write to a file (all hardcoded)**
  - Hardcoded file path, hardcoded note text, no user input yet.
  - Wrap the note with the delimiter + current date/time before and after it.
  - Goal: learn how writing/appending to a file works.

- [X] **2. Write user input to a file**
  - File path is still hardcoded.
  - Prompt the user to type a note, read it, wrap it with the delimiter + date/time, and append it to the file.

- [ ] **3. Read and parse the file**
  - Read the whole file's contents.
  - Split it into individual notes (one note per delimiter block).
  - Store each note as an element in a `List<string>` (or a list of a small note type later on).
  - Displaying them is optional at this stage. The goal is just to parse correctly.

- [ ] **4. Apply namespaces**
  - Start hardcoded: a file's first line declares its namespace (e.g. `namespace: school`).
  - Read the namespace back from the file.
  - Once understood, use it in creating/editing/deleting files.

- [ ] **5. Menu: browse, create, and add**
  - On startup: show the greeting, then list the available namespaces.
  - Choose a namespace → show its notes, presented clearly (numbered/spaced, so separate notes don't look like one big note). Notes have no titles.
  - Create a new namespace.
  - Inside a namespace: add a new note.

- [ ] **6. Edit and delete notes**
  - Choose the action (edit or delete).
  - Read and parse the file → select a note → edit or delete it.
  - Save safely: modify in memory → write everything to a temp file → replace the original with it.

**When 1 to 6 are done, the MVP is complete.**

### v1.x (Quality of life)
- [ ] **1.1 CLI arguments**: e.g. `qn school "apply plastic cover"` adds a note without opening the menu
- [ ] **1.2 Search**: search notes across all namespaces
- [ ] **1.3 Export**: a menu choice/command that combines everything into one file (for sharing with a person or an AI)

### Maybe later
- [ ] Key-by-key input with `Console.ReadKey` (custom shortcuts)

## APIs to look into

### Writing
| Need | API |
|---|---|
| Add text to the end of a file (creates it if missing) | `File.AppendAllText`, `File.AppendAllLines` |
| Overwrite a whole file | `File.WriteAllText`, `File.WriteAllLines` |
| Write piece by piece | `StreamWriter` |

### Reading
| Need | API |
|---|---|
| Read a whole file as one string | `File.ReadAllText` |
| Read all lines into an array | `File.ReadAllLines` |
| Read lines lazily, one at a time (good for big files / first line only) | `File.ReadLines` |
| Read piece by piece | `StreamReader` (`ReadLine`) |

### Files & folders
| Need | API |
|---|---|
| Does the file/folder exist? | `File.Exists`, `Directory.Exists` |
| Create a folder (safe if it already exists) | `Directory.CreateDirectory` |
| List `.txt` files in a folder | `Directory.EnumerateFiles(path, "*.txt")` |
| Rename / move a file | `File.Move` (look at the `overwrite` parameter) |
| Delete a file | `File.Delete` |
| Build paths safely | `Path.Combine` |
| Get file name pieces | `Path.GetFileName`, `Path.GetFileNameWithoutExtension`, `Path.GetExtension` |
| Temp file path | `Path.GetTempFileName` |
| User's home folder | `Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)` |

### Strings & parsing
| Need | API |
|---|---|
| Split notes on the delimiter | `string.Split(string, StringSplitOptions)` (look at `RemoveEmptyEntries`, `TrimEntries`) |
| Clean whitespace | `string.Trim` |
| Check a header line | `string.StartsWith`, `string.Substring` |
| Build multi-line text efficiently | `StringBuilder` |
| Platform-correct newline | `Environment.NewLine` |
| Timestamps | `DateTime.Now`, `.ToString("yyyy-MM-dd HH:mm")` |

### Console
| Need | API |
|---|---|
| Read a line (returns `null` on Ctrl+D) | `Console.ReadLine` |
| Read single key presses | `Console.ReadKey(intercept: true)`, `ConsoleKeyInfo`, `ConsoleModifiers` |
| Clear screen / colors | `Console.Clear`, `Console.ForegroundColor`, `Console.ResetColor` |
| Command-line arguments | `args` in top-level statements / `Main(string[] args)` |

### Collections (for grouping namespaces)
| Need | API |
|---|---|
| Namespace → list of files/notes | `Dictionary<string, List<string>>` |
| Grouping with LINQ | `GroupBy`, `Select`, `Where`, `ToList` |

### Concepts to read about
- [ ] The `using` statement / `IDisposable` (why streams must be closed)
- [ ] Exceptions from file I/O: `IOException`, `FileNotFoundException`, `UnauthorizedAccessException`
- [ ] Why you can't "insert" into the middle of a file (rewrite it instead)
- [ ] Atomic save: write to a temp file, then `File.Move` over the original
- [ ] Text encoding (UTF-8) and line endings (`\n` vs `\r\n`)
- [ ] Filesystem block size (why many tiny files waste space)

## Notes / learnings
<!-- Write down what you figure out as you go. -->
