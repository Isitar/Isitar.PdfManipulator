using iText.Kernel.Pdf;
using iText.Kernel.Utils;

Console.WriteLine("Combine PDF - by isitar");

Console.WriteLine("Input Directory");
var path = Console.ReadLine();
var outputFile = Path.Combine(path, "output.pdf");

if (File.Exists(outputFile))
{
    Console.WriteLine("Output file already exists, do you want to overwrite it? [y/n]");
    if (Console.ReadLine()!.ToLower() == "y")
    {
        File.Delete(outputFile);
    }
    else
    {
        Console.WriteLine("Cancelled");
        return;
    }
}

try
{
    using (var writer = new PdfWriter(outputFile))
    using (var outputPdf = new PdfDocument(writer))
    {
        var merger = new PdfMerger(outputPdf);

        foreach (var file in Directory.GetFiles(path, "*.pdf"))
        {
            if (file == outputFile)
            {
                continue;
            }
            using var reader = new PdfReader(file);
            using var inputPdf = new PdfDocument(reader);
            // Extract only the first page
            merger.Merge(inputPdf, 1, 1);
        }
    }

    Console.WriteLine($"Extraction complete. Output saved as {outputFile}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}\n{ex.StackTrace}");
}