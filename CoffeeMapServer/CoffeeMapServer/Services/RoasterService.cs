using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;
using CoffeeMapServer.Services.Interfaces;
using CoffeeMapServer.ViewModels;
using CoffeeMapServer.ViewModels.DTO;

namespace CoffeeMapServer.Services
{
    public class RoasterService : IRoasterService
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly IRoasterTagRepository _roasterTagRepository;

        public RoasterService(IRoasterRepository roasterRepository,
                              IRoasterTagRepository roasterTagRepository)
        {
            _roasterRepository = roasterRepository ?? throw new ArgumentNullException(nameof(roasterRepository));
            _roasterTagRepository = roasterTagRepository ?? throw new ArgumentNullException(nameof(roasterTagRepository));
        }

        public async Task<IList<RoasterInfoViewModel>> GetRoastersAsync()
        {
            var roasters = await _roasterRepository.GetListAsync();
            var roastertags = await _roasterTagRepository.GetListAsync();
            foreach (var i in roasters)
                i.RoasterTags = roastertags.Where(rt => rt.RoasterId == i.Id).ToList();
            var roastersViewModels = new List<RoasterInfoViewModel>();
            foreach (var i in roasters)
            {
                var tags = i.RoasterTags == null ?
                           new List<TagDT>() :
                           i.RoasterTags.Select(t => TagDT.New(t.Tag.Id, t.Tag.TagTitle)).ToList();

                roastersViewModels.Add(new RoasterInfoViewModel(RoasterDT.New(i.Id,
                                                                              i.ContactPersonName,
                                                                              i.ContactPersonPhone,
                                                                              i.Name,
                                                                              i.ContactNumber,
                                                                              i.ContactEmail,
                                                                              i.WebSiteLink,
                                                                              i.VkProfileLink,
                                                                              i.InstagramProfileLink,
                                                                              i.TelegramProfileLink,
                                                                              new byte[0],
                                                                              i.Description),
                                                                AddressDT.New(
                                                                    i.OfficeAddress.Id,
                                                                    i.OfficeAddress.AddressStr,
                                                                    i.OfficeAddress.OpeningHours,
                                                                    i.OfficeAddress.Latitude,
                                                                    i.OfficeAddress.Longitude),
                                                                tags));

            }
            return roastersViewModels;
        }

        public async Task<RoasterInfoViewModel> GetRoasterViewModel(Guid id)
        {
            var roaster = await _roasterRepository.GetSingleAsync(id);
            var tags = roaster.RoasterTags == null ?
                new List<TagDT>() :
                roaster.RoasterTags.Select(t => TagDT.New(t.Tag.Id, t.Tag.TagTitle)).ToList();

            if (roaster.Picture == null)
                roaster.Picture = Picture.New(new byte[0]);
            else if (roaster.Picture.Bytes == null)
                roaster.Picture.Bytes = new byte[0];

            return new RoasterInfoViewModel(RoasterDT.New(roaster.Id,
                                                          roaster.ContactPersonName,
                                                          roaster.ContactPersonPhone,
                                                          roaster.Name,
                                                          roaster.ContactNumber,
                                                          roaster.ContactEmail,
                                                          roaster.WebSiteLink,
                                                          roaster.VkProfileLink,
                                                          roaster.InstagramProfileLink,
                                                          roaster.TelegramProfileLink,
                                                          roaster.Picture.Bytes,
                                                          roaster.Description),
                                            AddressDT.New(roaster.OfficeAddress.Id,
                                                          roaster.OfficeAddress.AddressStr,
                                                          roaster.OfficeAddress.OpeningHours,
                                                          roaster.OfficeAddress.Latitude,
                                                          roaster.OfficeAddress.Longitude),
                                            tags);
        }
    }
}