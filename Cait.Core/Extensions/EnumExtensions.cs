using System;

namespace Cait.Core.Extensions
{
    public static class EnumExtensions
    {
        public static int CreateFlagsBitfield(this Enum[] flaggedEnums)
        {
            if (flaggedEnums == null)
                throw new ArgumentNullException(nameof(flaggedEnums), "Argument can not be null");

            if (flaggedEnums.Length == 0)
                return 0;

            int flagBitField = Convert.ToInt32(flaggedEnums[0]);
            for (int i = 1; i < flaggedEnums.Length; i++)
            {
                flagBitField = flagBitField | Convert.ToInt32(flaggedEnums[i]);
            }

            return flagBitField;
        }
    }
}