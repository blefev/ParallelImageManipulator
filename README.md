# ParallelImageManipulator
CSC-410 Final Project

# Usage
The best way to use the Image Manipulator is either by using the class in a C# application, or through the web server + API.

# API
The API runs on the custom, minimal webserver we created under the "WebServer" project. 

The API handles all requests at the base path. By default it runs at http://localhost:9410.

A request specifies a base64 data URI image, the transformation to apply, and arguments if required.

Here is a simple Grayscale request:

```javascript
{
  "image": "data:image/jpg;base64,<base64 image string>"
  "filter": "grayscale"
}```

These are the transformations that require arguments:
| Transformation  |  Arguments |
|---|---|
| grayscale |   |
| flip | horizontal (boolean) |
| rotate | clockwise (boolean), rotates (number of times to rotate - positive integer) |
| filter | color ("R", "G", or "B" |
| negate |  |
| blur | neighborDist (positive integer. controls neighbor pixel distance to us for blur) |
| brightness | value (integer between -255 to 255) |

