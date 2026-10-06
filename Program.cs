// DisplayMenu();

// Get the user's Documents folder
string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
// the file in the temp folder
string filePath = Path.Combine(documentsPath, "temp-test", "test-file.txt");


if (File.Exists(filePath))
{
    // clearing (this line below is temp and can be removed anytime)
    // File.WriteAllText(filePath, "");


    // Get User's input
    Console.WriteLine("Note Entry:");
    Console.Write(">  ");
    string? inputString = Console.ReadLine();

    // Apply Delimiters
    inputString = $"\n-----{DateTime.Now}\n{inputString}\n-----";

    // Write
    File.AppendAllText(filePath, inputString);



    // Read
    string[] fileContents = File.ReadAllLines(filePath);
    foreach (var line in fileContents)
    {
        Console.WriteLine(line);
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

