# cbr.net

`cbr.net` is a cross-platform .NET library for reading digital comic book
archives. The library targets **.NET Standard 2.0** so it can be used from
UWP, MAUI, Windows Forms and other frameworks running on Windows, Linux,
Android or iOS.

Supported container formats:

- **CBZ** (ZIP based) - fully supported using `System.IO.Compression`.
- **CBR**, **CBT**, **CB7**, **CBA** - stubs are included and can be
  implemented using additional archive libraries.

The main entry point is `ComicBook.Load(path)` which loads a comic book and
provides access to its entries (pages). The resulting `ComicBook` instance
exposes the container format through the `Format` property.

## License

This project is licensed under the [MIT License](LICENSE).
