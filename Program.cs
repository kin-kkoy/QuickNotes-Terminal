// DisplayMenu();

// Get the user's Documents folder
string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
// the file in the temp folder
string filePath = Path.Combine(documentsPath, "QuickNotes", "test-file.txt");


if (File.Exists(filePath))
{
    // // clearing (this line below is temp and can be removed anytime)
    // File.WriteAllText(filePath, "");


    // Get User's input
    Console.WriteLine("Note Entry:");
    Console.Write(">  ");
    string? inputString = Console.ReadLine();

    // Apply Delimiters
    inputString = $"\n----- {DateTime.Now.ToString("MMM. d, yyyy  [ hh:mm tt ]")}\n{inputString}\n-----";

    // Write
    File.AppendAllText(filePath, inputString);



    // Read
    string[] fileContents = File.ReadAllLines(filePath);
    List<string> notes = SectionDissector(fileContents);
    foreach (var line in notes)
    {
        Console.WriteLine($"Note:\t{line}");
    }
}
else
{
    Console.WriteLine("FILE DOES  NOTE ;]  EXIST!");    
}






// Methods ================================================

// void DisplayMenu()
// {
//     Console.WriteLine("================================================");
//     Console.WriteLine("                   QuickNotes                   ");
//     Console.WriteLine("    Write it down. Forget it or Remember it?    ");
//     Console.WriteLine("================================================");
//     Console.WriteLine();
//     Console.WriteLine("  [1] Select a namespace");
//     Console.WriteLine("  [2] Create a new namespace");
//     Console.WriteLine("  [3] List notes");
//     Console.WriteLine("  [q] Quit");
//     Console.WriteLine();
//     Console.Write(">  ");
// }

List<string> SectionDissector(string[] fileContents)
{
    List<string> notes = new List<string>();

    foreach (string line in fileContents)
    {
        // // namespace line
        // if(line.StartsWith("namespace:"))   continue;
        // // starting/opening delimiter
        // if(line.StartsWith($"----- "))  continue;
        // // closing delimiter
        // if(line == "-----")  continue;
        if(line.StartsWith("namespace:") ||
             line.StartsWith($"----- ") || 
             line == "-----" || 
             line.IsWhiteSpace())   
                continue;


        notes.Add(line);
    }

    return notes;
}