namespace WEB_TH1.Services.Interfaces
{
    public interface IBufferedFileUploadService
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
