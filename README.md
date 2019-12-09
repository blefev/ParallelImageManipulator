# ParallelImageManipulator
CSC-410 Final Project

# Usage
The best way to use the Image Manipulator is either by using the class in a C# application, or through the web server + API. All development work was done in Visual Studio 2019.

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
To run the API, build and run the "WebServer" project. To specify a different URI, modify it in `Program.cs`.
We recommend using Postman to play with the API. You can see live requests as they come into the API.

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
    "clockwise": true,
    "rotates": 1
  }
}
```
### Using in a website

The API returns Data URI images that are ready-to-use in HTML img tags. Simply use Javascript to perform an Ajax request to the API, and put the returned image string into the `src` of an HTML `img` element.

### Specification

All requests must use the `POST` method, and are sent to the base URI of the webserver. By default `http://localhost:9410`.

Here are the available transformations and required arguments:

| Transformation  |  Arguments |
|---|---|
| grayscale |   |
| negate |  |
| flip | horizontal (boolean) |
| rotate | clockwise (boolean), rotates (number of times to rotate - positive integer) |
| filter | color ("R", "G", or "B") |
| blur | neighborDist (positive integer. controls neighbor pixel distance to use for blur) |
| brightness | value (integer between -255 to 255) |

## Web UI

To launch the WebUI, simply build the PIMWebsite in Visual Studio project and Right Click -> View -> View in Browser. Follow the instructions to use the UI. Note that it is a very limited UI. You have to upload an image, then optionally enter parameters and select a single transformation to run. It will then display the result.

## Running Tests
Simply right-click the Tests project in Visual Studio and select "Run Tests". Note, some of the tests use ImageMagick to test against, and some use known-good results under `Tests\Resources\Answers`.

### Testing Performance
Use git to checkout to the `nonparallel` branch. You can then run the `TestPerformance` test and click "View More Output" when the test completes to see the average time of each operation. Then, repeat these steps with the `parallelspeedtest` branch.

~[img](https://raw.githubusercontent.com/blefev/ParallelImageManipulator/master/Tests/Resources/Square.png)

