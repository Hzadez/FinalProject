using EventMenegmentSL.Services.Interfaces;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace EventMenegmentSL.Services.Implementation
{
    public class QrCodeService : IQrCodeService
    {
        public byte[] GenerateQrCode(string text)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            var base64Qr = new Base64QRCode(qrCodeData);
            string base64Image = base64Qr.GetGraphic(20); 
            return Convert.FromBase64String(base64Image);
        }
    }
}
