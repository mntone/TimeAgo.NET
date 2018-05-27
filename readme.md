# TimeAgo.NET

[![License](https://img.shields.io/github/license/mntone/TimeAgo.NET.svg?style=flat-square)](https://github.com/mntone/TimeAgo.NET/blob/master/LICENSE.txt)

The "time ago" API for .NET.

`TimeAgo.NET` currently supports the following languages:

- en (English)
- ja (Japanese)

## Usage

```csharp
using System;

static void Main(string[] args)
{
    var timeSpan = TimeSpan.FromSeconds(14);
    Console.WriteLine($"Short:  {timeSpan.ToShortTimeAgo()}");
    Console.WriteLine($"Medium: {timeSpan.ToMediumTimeAgo()}");
    Console.WriteLine($"Long:   {timeSpan.ToLongTimeAgo()}");
}
```

This is following (English):

```text
Short:  14s
Medium: 14secs
Long:   14 seconds ago
```

## LICENSE

[MIT License](https://github.com/mntone/TimeAgo.NET/blob/master/LICENSE.txt)


## Author

mntone
- GitHub: https://github.com/mntone
- Twitter: https://twitter.com/mntone (posted in Japanese; however, english is ok)
