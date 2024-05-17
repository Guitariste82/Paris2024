namespace Paris2024.Interfaces
{
    public interface IQrCodeGeneratorRepository
    {
        byte[] GetQrCodeToBitmap(string Text);
        string GetQrCodeToPng(string textToGenerate);
        string GetQrCodeToPngWithUrl(string url, string SecureKey);
    }
}
