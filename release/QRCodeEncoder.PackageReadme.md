# Usage
Use this to encode a string to a QR Code.

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