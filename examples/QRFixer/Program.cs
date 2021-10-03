using QRCodeFixer;

for (int i = 0; i < 4; i++)
{
    QRFixer.FixAndSaveAsPng(@$"source-damaged-{i}.png", @$"source-damaged-{i}-fixed.png");
}