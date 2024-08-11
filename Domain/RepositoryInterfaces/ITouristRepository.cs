﻿using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristRepository
    {
        public List<Tourist> FindAll();

        public Tourist FindOne(int id);

        public void Save(Tourist tourist);

        public void Update(Tourist tourist);
    }
}
