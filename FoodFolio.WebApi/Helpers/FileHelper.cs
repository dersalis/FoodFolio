using FoodFolio.WebApi.Exceptions;

namespace FoodFolio.WebApi.Helpers;

public class FileHelper
{
    private const string DIRECTORY = "/wwwroot/pictures/";

    public static string GetFilePath(string host, string fileName)
    {
        return $"{host}/pictures/{fileName}";
    }

    public static string UploadFile(IFormFile file)
    {
        

        if (file is null || file.Length <= 0) throw new BadRequestException("Zły plik");

        string currentDirectory = Directory.GetCurrentDirectory();
        string fileName = $"{Guid.NewGuid().ToString()}.{file.FileName.Substring(file.FileName.Length - 3)}";
        string filePath = DIRECTORY + fileName;
        string fullPath = currentDirectory + filePath;

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return fileName;
    }

    public static string GetBase64(string fileName)
    {
        string fullPath = Directory.GetCurrentDirectory() + DIRECTORY + fileName;
        byte[] bytes = File.ReadAllBytes(fullPath);

        return "data:image/png;base64," + Convert.ToBase64String(bytes);
    }
}

