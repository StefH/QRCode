# Usage
Use this to decode a QR Code to data or a string.

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