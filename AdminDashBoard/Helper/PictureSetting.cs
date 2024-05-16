namespace AdminDashBoard.Helper
{
	public static class PictureSetting
	{
		public static string UploadPicture(IFormFile file)
		{
            // 1. Get Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images");
            // 2. Set FileName UINQUE
            var fileName = Guid.NewGuid() + file.FileName.Trim();
            // 3. Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            // 4. Save File as Streams
            var fs = new FileStream(filePath, FileMode.Create);
            // 5. Copy File Into Streams
            file.CopyTo(fs);
            // 6. Retun FileName

            return Path.Combine("Images", fileName);
        }

		public static void DeleteFile(string fileName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images", fileName);

			if (File.Exists(filePath))
				File.Delete(filePath);
		}
	}
}
