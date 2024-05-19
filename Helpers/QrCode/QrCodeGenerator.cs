using QRCoder;
using static QRCoder.PayloadGenerator;

namespace Paris2024.Helpers.QrCode;

public class QrCodeGenerator : IQrCodeGeneratorRepository

{
    public byte[] GetQrCodeToBitmap(string textToGenerate)
    {
        // Init empty field
        byte[] oQrCode = new byte[0];

        if (!string.IsNullOrEmpty(textToGenerate))
        {
            QRCodeGenerator codeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = codeGenerator.CreateQrCode(textToGenerate, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode bitmap = new BitmapByteQRCode();
            oQrCode = bitmap.GetGraphic(20);
        }
        return oQrCode;

    }
    public string GetQrCodeToPng(string textToGenerate)
    {
        // Init empty field
        byte[] oQrCode = new byte[0];

        if (!string.IsNullOrEmpty(textToGenerate))
        {
            QRCodeGenerator codeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = codeGenerator.CreateQrCode(textToGenerate, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode pngByteQRCode = new PngByteQRCode(qrCodeData);
            oQrCode = pngByteQRCode.GetGraphic(20);
            string base64String = Convert.ToBase64String(oQrCode);
            //model.QRImageURL = "data:image/png;base64," + base64String;
            return base64String;
        }

        return string.Empty;

    }

    public string GetQrCodeToPngWithUrl(string url, string SecureKey)
    {

        //if ((!string.IsNullOrEmpty(url) && (!string.IsNullOrEmpty(SecureKey))))
            if ((!string.IsNullOrEmpty(url) ))
            {
            var myDomain = url;
            Payload payload = new Url(myDomain + SecureKey);
            QRCodeGenerator codeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = codeGenerator.CreateQrCode(payload);
            PngByteQRCode pngByteQRCode = new PngByteQRCode(qrCodeData);
            var qrASByte = pngByteQRCode.GetGraphic(20);
            string base64String = Convert.ToBase64String(qrASByte);
            //model.QRImageURL = "data:image/png;base64," + base64String;
            return base64String;
        }
   
        return string.Empty;
    }



}