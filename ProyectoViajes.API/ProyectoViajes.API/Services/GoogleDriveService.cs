using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;

namespace ProyectoViajes.API.Services
{
    public class GoogleDriveService
    {
        private readonly DriveService _driveService;

        public GoogleDriveService()
        {
            // Inicializa el servicio de Google Drive
            var credential = GoogleCredential.FromFile("path/to/your/credentials.json")
                .CreateScoped(DriveService.Scope.DriveFile);
            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Your Application Name",
            });
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = file.FileName,
                MimeType = file.ContentType
            };

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                var request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);
                request.Fields = "id";
                var fileResponse = await request.UploadAsync();

                if (fileResponse.Status != UploadStatus.Completed)
                {
                    throw new Exception("Error uploading file to Google Drive");
                }

                return $"https://drive.google.com/uc?id={request.ResponseBody.Id}";
            }
        }
    }
}
