# QRCode
Sources copyright by Uzi Granot using [CPOL](https://www.codeproject.com/info/cpol10.aspx). More details can be found in this [blog](https://www.codeproject.com/Articles/1250071/QR-Code-Encoder-and-Decoder-NET-Framework-Standard).

## NuGet packages

| Name | NuGet
| - | - |
| `QRCodeFixer` | [![NuGet Badge](https://buildstats.info/nuget/QRCodeFixer)](https://www.nuget.org/packages/QRCodeFixer)
| `QRCodeEncoder` | [![NuGet Badge](https://buildstats.info/nuget/QRCodeEncoder)](https://www.nuget.org/packages/QRCodeEncoder)
| `QRCodeDecoder` | [![NuGet Badge](https://buildstats.info/nuget/QRCodeDecoder)](https://www.nuget.org/packages/QRCodeDecoder)

## QRFixer
Use this to fix a QR Code.

#### QR Code (damaged)
![Damaged QRCode](https://github.com/StefH/QRCode/blob/main/examples/QRFixer/source-damaged-3.png)

#### QR Code (fixed)
![Fixed QRCode](https://github.com/StefH/QRCode/blob/main/examples/QRFixer/original.png)

### Configure Dependency Injection
``` csharp
...
services.AddQRCodeFixer();
...
```

### Usage
``` csharp
var fixer = serviceProvider.GetRequiredService<QRCodeFixer>();

var data = fixer.FixAndSaveAsPng("qrcode-damaged.png", "qrcode-fixed.png");
```

## QREncoder

### Configure Dependency Injection
``` csharp
...
services.AddQRCodeEncoder();
...
```

### Usage
``` csharp
var encoder = serviceProvider.GetRequiredService<QRCodeEncoder>();

var stringData = "test";
encoder.Encode(stringData);
encoder.SaveQRCodeToPngFile("qrcode.png");
```

## QRDecoder

### Configure Dependency Injection
``` csharp
...
services.AddQRCodeDecoder();
...
```

### Usage
``` csharp
var decoder = _serviceProvider.GetRequiredService<QRDecoder>();
byte[][] data = decoder.ImageDecoder(sourceBitmap);

var data = QRDecoder.ByteArrayToString(data[0]);
```

## References
- [Wounded QR codes](https://www.datagenetics.com/blog/november12013/index.html)
