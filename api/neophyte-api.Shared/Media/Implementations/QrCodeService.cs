using System;
using QRCoder;

namespace neophyte.api.Shared.Media.Implementations;

public static class QrCodeService
{
    private static readonly QRCodeGenerator Generator = new();

    public static string GenerateQrCode(string payload)
    {
        var data = Generator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(data);
        var binary = qrCode.GetGraphic(20);
        return Convert.ToBase64String(binary);
    }
}