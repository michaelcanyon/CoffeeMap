using System;
using System.Collections.Generic;
using CoffeeMapServer.Infrastructures.IRepositories;
using CoffeeMapServer.Models;

namespace CoffeeMapServer.builders
{
    public static class RoasterTagsPairsBuilder
    {
        public static void BuildRoasterTags(IList<Tag> tags, Guid roasterId, IRoasterTagRepository roasterTagRepository)
        {
            foreach (var item in tags)
                roasterTagRepository.Add(new RoasterTag(roasterId, item.Id));
        }
    }
}