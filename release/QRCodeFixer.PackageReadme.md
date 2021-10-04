# Usage
Use this to fix a QR Code.

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