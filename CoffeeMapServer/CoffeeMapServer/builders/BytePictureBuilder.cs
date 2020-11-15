using System.IO;
using Microsoft.AspNetCore.Http;

namespace CoffeeMapServer.builders
{
    public static class BytePictureBuilder
    {
        public static byte[] GetBytePicture(IFormFile picture)
        {
            if (picture != null)
            {
                byte[] bytePicture = null;
                using (var binaryReader = new BinaryReader(picture.OpenReadStream()))
                {
                    bytePicture = binaryReader.ReadBytes((int)picture.Length);
                }
                return bytePicture;
            }
            return null;
        }
    }
}
