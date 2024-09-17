namespace Demo.PL.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string FolderName)
        {
            //1-Get Located Folder Path
          

            if (file is not null)
            {
                //F:\Route\Route C42\Cycle42\05 MVC\G01\Session03\DemoMvc.G01\Demp.Pl\wwwroot\Images\
                string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderName);
                //2- Get File Name And Make It Unique
                string FileName = $"{Guid.NewGuid()}{file.FileName}";
                //3- Get File Path[Folder Path+ FileName]
                string FilePath = Path.Combine(FolderPath, FileName);
                //4- Save  File As Streams
                using var FileStream = new FileStream(FilePath, FileMode.Create);
                file.CopyTo(FileStream);
                //- Return File Name 
                return FileName;
            }
            return "";
            
        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            //1- Get File Path
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderName, FileName);
            //2- Check If File Exists Or Not 
            if (File.Exists(FilePath))
            {
                // If Exists Remove It
                File.Delete(FilePath);
            }

        }
    }
}
