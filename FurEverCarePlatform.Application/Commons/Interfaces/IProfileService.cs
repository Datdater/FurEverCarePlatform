﻿using FurEverCarePlatform.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Commons.Interfaces
{
    public interface IProfileService
    {
        Task<AppUserDto> GetProfileAsync(Guid userId);
        Task<AppUserDto> UpdateProfileAsync(Guid userId, AppUserDto updatedUser);
    }
}
