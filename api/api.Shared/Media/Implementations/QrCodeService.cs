using System;
using api.Shared.Media.Interfaces;
using QRCoder;

namespace api.Shared.Media.Implementations;

public class QrCodeService : IQrCodeService
{
    private readonly QRCodeGenerator _generator = new();

    public string CreateQrFromCode(string payload)
    {
        var data = _generator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(data);
        var binary = qrCode.GetGraphic(20);
        return Convert.ToBase64String(binary);
    }
}