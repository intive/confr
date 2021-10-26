using AutoMapper;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Domain.ValueObjects;
using System;

namespace Intive.ConfR.Application.Comments.Shared
{
    public static class Utility
    {
        public static Guid ToGuid(string commentId, IMapper mapper)
        {
            try
            {
                return mapper.Map<Guid>(commentId);
            }
            catch (AutoMapperMappingException e)
            {
                throw new InvalidFormatException("Invalid Guid format.");
            }
        }

        public static EMailAddress ToEMailAddress(string roomEmail, IMapper mapper)
        {
            try
            {
                if (roomEmail == null)
                {
                    throw new InvalidFormatException("Email is null!");
                }

                return mapper.Map<EMailAddress>(roomEmail);
            }
            catch (AutoMapperMappingException e)
            {
                throw new InvalidFormatException("Invalid Email format.");
            }
        }
    }
}
