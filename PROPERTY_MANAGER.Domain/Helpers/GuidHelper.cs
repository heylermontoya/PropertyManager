using PROPERTY_MANAGER.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace PROPERTY_MANAGER.Domain.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class GuidHelper
    {
        public static bool ValidateRequestGuid(this Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return false;
            }

            if (!Guid.TryParse(guid.ToString(), out _))
            {
                return false;
            }

            return true;
        }

        public static void CheckValidGuid(this Guid guid)
        {
            if (!guid.ValidateRequestGuid())
            {
                throw new AppException(MessagesExceptions.GuidNotValid);
            }
        }
    }
}
