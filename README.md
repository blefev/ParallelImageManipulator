# ParallelImageManipulator
CSC-410 Final Project

# Usage
The best way to use the Image Manipulator is either by using the class in a C# application, or through the web server + API.

## Class

Here is a simple example of using the C# class:

```csharp
using ParallelImageManipulator;
using System.Drawing;

[...namespace and class omitted...]
static void Main(string[] args)
{
  Bitmap bitmap = new Bitmap("path_to_bitmap.png"); // png, jpg, bmp all work
  ImageManipulator imageManipulator = new ImageManipulator(bitmap);
  imageManipulator.Grayscale(); // transforms to grayscale
  imageManipulator.ToBitmap().Save("path_to_new_bitmap.png");
}

```

## Web API
The API runs on the custom, minimal webserver we created under the "WebServer" project. 

The API handles all requests at the base path. By default it runs at `http://localhost:9410`.

A request specifies a base64 data URI image, the transformation to apply, and arguments if required.

Here is a simple Grayscale request:

```json
{
  "image": "data:image/jpg;base64,<base64 image string>",
  "filter": "grayscale"
}
```

Here is an example of a request with arguments:
```json
{
  "image": "data:image/jpg;base64,<base64 image string>",
  "filter": "rotate",
  "args": {
    "clockwise": true
    "rotates": 1
  }
}
```

### Specification

All requests must use the `POST` method, and are sent to the base URI of the webserver. By default `http://localhost:9410`.

Here are the available transformations and required arguments:

| Transformation  |  Arguments |
|---|---|
| grayscale |   |
| negate |  |
| flip | horizontal (boolean) |
| rotate | clockwise (boolean), rotates (number of times to rotate - positive integer) |
| filter | color ("R", "G", or "B" |
| blur | neighborDist (positive integer. controls neighbor pixel distance to us for blur) |
| brightness | value (integer between -255 to 255) |

