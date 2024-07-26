# EscPosSharp

[![NuGet](https://img.shields.io/nuget/v/EscPosSharp.svg)](https://www.nuget.org/packages/EscPosSharp/)



Library for generating ESC/POS commands for thermal printers. Based on [escpos-coffee](https://github.com/anastaciocintra/escpos-coffee).

Supports styled text, images, barcodes, and QR codes.

> [!WARNING]  
> Tests indicate that ordered dithering is not working. Not using that myself, open for PRs.

> [!WARNING]  
> Depending on your OS not all necessary encodings are available. If you get an exception like `System.ArgumentException : 'cp437' is not a supported encoding name.` you can try to install the `System.Text.Encoding.CodePages` package and call `Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);` before using the library.

## Usage

See samples in [EscPosTests](https://github.com/flostellbrink/escpos-sharp/tree/main/EscPosSharp.Test) project.
