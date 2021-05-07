using System;
using System.IO;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructure.Interface;
using CoffeeMapServer.Models;
using Microsoft.AspNetCore.Http;

namespace CoffeeMapServer.builders
{
    public static class BytePictureBuilder
    {
        public static byte[] GetBytePicture(IFormFile picture)
        {
            if (picture == null)
                return null;

            using var binaryReader = new BinaryReader(picture.OpenReadStream());
            return binaryReader.ReadBytes((int)picture.Length);
        }

        public static void BindPicture(Guid roasterId, byte[] picture, IPictureRepository pictureRepository)
        {
            if (picture != null)
            {
                var bytePicture = Picture.New(picture);
                bytePicture.RoasterId = roasterId;
                pictureRepository.Add(bytePicture);
            }
        }

        public static void BindPictureRequest(Guid roasterId, IFormFile picture, IPictureRequestRepository pictureRequestRepository)
        {
            var bytes = GetBytePicture(picture);
            if (bytes != null)
            {
                var bytePicture = PictureRequest.New(bytes);
                bytePicture.RoasterRequestId = roasterId;
                pictureRequestRepository.Add(bytePicture);
            }
        }

        public static async Task ReplacePicture(Guid roasterId, IFormFile picture, IPictureRepository pictureRepository)
        {
            var bytes = GetBytePicture(picture);
            if (bytes != null)
            {
                var currentPic = await pictureRepository.GetPictureByRoasterIdAsyncAsNoTracking(roasterId);
                if (currentPic != null)
                    pictureRepository.Delete(currentPic);
                var bytePicture = Picture.New(bytes);
                bytePicture.RoasterId = roasterId;
                pictureRepository.Add(bytePicture);
            }
        }

        public static void BindPictureRequest(Guid roasterReqId, byte[] picture, IPictureRequestRepository pictureReqRepository)
        {
            if (picture != null)
            {
                var bytePicture = PictureRequest.New(picture);
                bytePicture.RoasterRequestId = roasterReqId;
                pictureReqRepository.Add(bytePicture);
            }
        }

        public static async Task ReplacePictureRequest(Guid roasterReqId, IFormFile picture, IPictureRequestRepository pictureReqRepository)
        {
            var bytes = GetBytePicture(picture);
            if (bytes != null)
            {
                var currentPic = await pictureReqRepository.GetPictureReqByRoasterReqIdAsyncAsNoTracking(roasterReqId);
                if (currentPic != null)
                    pictureReqRepository.Delete(currentPic);
                var bytePicture = PictureRequest.New(bytes);
                bytePicture.RoasterRequestId = roasterReqId;
                pictureReqRepository.Add(bytePicture);
            }
        }
    }
}